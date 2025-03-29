<template>
  <div class="screen-container">
    <!-- 顶部标题 -->
    <div class="header">
      <div class="title-container">
        <div class="title-bg"></div>
        <h1 class="main-title">看板</h1>
        <div class="time-display">{{ currentTime }}</div>
      </div>
    </div>

    <!-- 主体内容 -->
    <div class="content">
      <!-- 左侧面板 -->
      <div class="left-panel">
        <div class="panel-item">
          <div class="panel-header">
            <h3>任务执行情况</h3>
          </div>
          <div class="panel-content">
            <div class="task-progress">
              <div class="progress-item" v-for="(item, index) in taskProgress" :key="index">
                <div class="progress-info">
                  <span class="progress-name">{{ item.name }}</span>
                  <span class="progress-value">{{ item.value }}%</span>
                </div>
                <div class="progress-bar">
                  <div class="progress-inner" :style="{ width: item.value + '%', backgroundColor: item.color }"></div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="panel-item">
          <div class="panel-header">
            <h3>任务类型分布</h3>
          </div>
          <div class="panel-content chart-container">
            <div ref="taskTypeChart" class="chart"></div>
          </div>
        </div>
      </div>

      <!-- 中间3D库位展示 -->
      <div class="center-panel">
        <div class="panel-header">
          <h3>3D库位实时状态</h3>
          <div class="control-buttons">
            <el-button size="small" type="primary" @click="resetCamera">重置视角</el-button>
            <el-select v-model="currentArea" placeholder="选择区域" size="small" @change="changeArea">
              <el-option v-for="item in areaList" :key="item.value" :label="item.label" :value="item.value">
              </el-option>
            </el-select>
          </div>
        </div>
        <div class="panel-content">
          <div ref="threeContainer" class="three-container"></div>
          <div class="status-legend">
            <div class="legend-item">
              <div class="color-block" style="background-color: #67c23a;"></div>
              <span>空闲</span>
            </div>
            <div class="legend-item">
              <div class="color-block" style="background-color: #e6a23c;"></div>
              <span>占用</span>
            </div>
            <div class="legend-item">
              <div class="color-block" style="background-color: #f56c6c;"></div>
              <span>故障</span>
            </div>
          </div>
        </div>
      </div>

      <!-- 右侧面板 -->
      <div class="right-panel">
        <div class="panel-item">
          <div class="panel-header">
            <h3>库位使用率</h3>
          </div>
          <div class="panel-content chart-container">
            <div ref="storageUsageChart" class="chart"></div>
          </div>
        </div>
        <div class="panel-item">
          <div class="panel-header">
            <h3>实时任务</h3>
          </div>
          <div class="panel-content">
            <div class="task-list">
              <div class="task-item" v-for="(task, index) in realtimeTasks" :key="index"
                :class="{ 'warning': task.status === 'warning', 'success': task.status === 'success' }">
                <div class="task-icon">
                  <i :class="task.icon"></i>
                </div>
                <div class="task-info">
                  <div class="task-name">{{ task.name }}</div>
                  <div class="task-desc">{{ task.description }}</div>
                </div>
                <div class="task-status">
                  <span :class="'status-' + task.status">{{ getStatusText(task.status) }}</span>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- 底部数据统计 -->
    <div class="footer">
      <div class="data-block" v-for="(item, index) in statisticsData" :key="index">
        <div class="data-icon">
          <i :class="item.icon"></i>
        </div>
        <div class="data-info">
          <div class="data-value">{{ item.value }}</div>
          <div class="data-name">{{ item.name }}</div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, onBeforeUnmount } from 'vue';
import * as THREE from 'three';
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls.js';
import { CSS2DRenderer, CSS2DObject } from 'three/examples/jsm/renderers/CSS2DRenderer.js';
import * as echarts from 'echarts';
import { ElMessage } from 'element-plus';
// 导入Element Plus图标
import {
  Truck, Box, Location, Document, OfficeBuilding,
  Upload, Download, Cpu, RefreshRight, Setting
} from '@element-plus/icons-vue';

// 当前时间
const currentTime = ref('');
let timeInterval: any = null;

// 更新时间
const updateTime = () => {
  const now = new Date();
  const year = now.getFullYear();
  const month = String(now.getMonth() + 1).padStart(2, '0');
  const day = String(now.getDate()).padStart(2, '0');
  const hours = String(now.getHours()).padStart(2, '0');
  const minutes = String(now.getMinutes()).padStart(2, '0');
  const seconds = String(now.getSeconds()).padStart(2, '0');
  currentTime.value = `${year}-${month}-${day} ${hours}:${minutes}:${seconds}`;
};

// 任务进度数据
const taskProgress = ref([
  { name: '入库任务', value: 85, color: '#409EFF' },
  { name: '出库任务', value: 65, color: '#67C23A' },
  { name: '移库任务', value: 45, color: '#E6A23C' },
  { name: '盘点任务', value: 90, color: '#909399' }
]);

// 实时任务数据
const realtimeTasks = ref([
  { name: '入库任务#1001', description: '正在将货物运送至A区1号库位', status: 'running', icon: 'Truck' },
  { name: '出库任务#1002', description: '从B区3号库位取出货物', status: 'success', icon: 'Box' },
  { name: '移库任务#1003', description: '将货物从C区移动至D区', status: 'warning', icon: 'Location' },
  { name: '盘点任务#1004', description: 'E区库位盘点进行中', status: 'running', icon: 'Document' },
  { name: '入库任务#1005', description: '等待AGV运送货物', status: 'waiting', icon: 'Truck' }
]);

// 统计数据
const statisticsData = ref([
  { name: '总库位数', value: 1280, icon: 'el-icon-office-building' },
  { name: '已使用库位', value: 876, icon: 'el-icon-box' },
  { name: '今日入库', value: 128, icon: 'el-icon-upload' },
  { name: '今日出库', value: 96, icon: 'el-icon-download' },
  { name: '运行设备', value: 24, icon: 'el-icon-cpu' }
]);

// 获取状态文本
const getStatusText = (status: string) => {
  switch (status) {
    case 'running':
      return '进行中';
    case 'success':
      return '已完成';
    case 'warning':
      return '异常';
    case 'waiting':
      return '等待中';
    default:
      return '未知';
  }
};

// 区域列表
const areaList = [
  { label: '1#粉料提升机', value: 'elevator1' },
  { label: '2#辅料提升机', value: 'elevator2' },
  { label: '3#正极卷提升机', value: 'elevator3' },
  { label: '4#负极卷提升机', value: 'elevator4' },
  { label: '5#粉料装配提升机', value: 'elevator5' },
  { label: '四向车', value: 'agv' },
];

// 当前选中的区域
const currentArea = ref('elevator1');

// Three.js相关变量
let scene: THREE.Scene;
let camera: THREE.PerspectiveCamera;
let renderer: THREE.WebGLRenderer;
let labelRenderer: CSS2DRenderer;
let controls: OrbitControls;
const locationObjects: THREE.Mesh[] = [];

// DOM引用
const threeContainer = ref(null);
const taskTypeChart = ref(null);
const storageUsageChart = ref(null);

// 图表实例
let taskTypeChartInstance: echarts.ECharts;
let storageUsageChartInstance: echarts.ECharts;

// 创建库位盒子
const createLocationBox = (x: number, y: number, z: number, id: string, status: number, area: string, width = 2, height = 1, depth = 2) => {
  // 根据状态设置颜色
  let color;
  switch (status) {
    case 0: // 空闲
      color = 0x67c23a;
      break;
    case 1: // 占用
      color = 0xe6a23c;
      break;
    case 2: // 故障
      color = 0xf56c6c;
      break;
    default:
      color = 0x67c23a;
  }

  // 创建库位盒子
  const geometry = new THREE.BoxGeometry(width, height, depth);
  const material = new THREE.MeshStandardMaterial({
    color: color,
    transparent: true,
    opacity: 0.8,
    metalness: 0.3,
    roughness: 0.4
  });
  const box = new THREE.Mesh(geometry, material);
  box.position.set(x, y, z);
  box.castShadow = true;
  box.receiveShadow = true;
  scene.add(box);
  locationObjects.push(box);

  // 添加边框
  const edges = new THREE.EdgesGeometry(geometry);
  const line = new THREE.LineSegments(
    edges,
    new THREE.LineBasicMaterial({ color: 0xffffff })
  );
  line.position.copy(box.position);
  scene.add(line);

  // 添加标签
  const div = document.createElement('div');
  div.className = 'label';
  div.textContent = id;
  div.style.backgroundColor = 'rgba(0, 0, 0, 0.6)';
  div.style.color = 'white';
  div.style.padding = '2px 6px';
  div.style.borderRadius = '2px';
  div.style.fontSize = '10px';
  div.style.pointerEvents = 'none';
  const label = new CSS2DObject(div);
  label.position.set(0, height / 2 + 0.5, 0);
  box.add(label);

  // 添加发光效果
  if (status === 1) {
    const glowMaterial = new THREE.MeshBasicMaterial({
      color: 0xffcc00,
      transparent: true,
      opacity: 0.2
    });
    const glowMesh = new THREE.Mesh(new THREE.BoxGeometry(width + 0.1, height + 0.1, depth + 0.1), glowMaterial);
    glowMesh.position.copy(box.position);
    scene.add(glowMesh);
  }
};

// 创建连接器
const createConnector = (x1: number, y1: number, z1: number, x2: number, y2: number, z2: number, thickness: number, color: number) => {
  // 计算两点之间的中点和距离
  const midX = (x1 + x2) / 2;
  const midY = (y1 + y2) / 2;
  const midZ = (z1 + z2) / 2;

  const dx = x2 - x1;
  const dy = y2 - y1;
  const dz = z2 - z1;
  const distance = Math.sqrt(dx * dx + dy * dy + dz * dz);

  // 创建连接器几何体
  const geometry = new THREE.BoxGeometry(distance, thickness, thickness);
  const material = new THREE.MeshStandardMaterial({
    color: color,
    transparent: true,
    opacity: 0.7
  });
  const connector = new THREE.Mesh(geometry, material);

  // 设置位置
  connector.position.set(midX, midY, midZ);

  // 计算旋转角度
  if (Math.abs(dx) > Math.abs(dz)) {
    // 主要是x方向的连接
    connector.rotation.y = 0;
  } else {
    // 主要是z方向的连接
    connector.rotation.y = Math.PI / 2;
  }

  scene.add(connector);
};

// 创建AGV主体
const createAgvBody = (x: number, y: number, z: number, name: string) => {
  const geometry = new THREE.BoxGeometry(4, 1, 6);
  const material = new THREE.MeshStandardMaterial({
    color: 0xff4500,
    metalness: 0.5,
    roughness: 0.5
  });
  const agv = new THREE.Mesh(geometry, material);
  agv.position.set(x, y + 0.5, z);
  agv.castShadow = true;
  agv.receiveShadow = true;
  scene.add(agv);

  // 添加轮子
  const wheelGeometry = new THREE.CylinderGeometry(0.5, 0.5, 0.3, 16);
  const wheelMaterial = new THREE.MeshStandardMaterial({ color: 0x333333 });

  const wheel1 = new THREE.Mesh(wheelGeometry, wheelMaterial);
  wheel1.position.set(x - 1.5, y + 0.5, z - 2);
  wheel1.rotation.x = Math.PI / 2;
  scene.add(wheel1);

  const wheel2 = new THREE.Mesh(wheelGeometry, wheelMaterial);
  wheel2.position.set(x + 1.5, y + 0.5, z - 2);
  wheel2.rotation.x = Math.PI / 2;
  scene.add(wheel2);

  const wheel3 = new THREE.Mesh(wheelGeometry, wheelMaterial);
  wheel3.position.set(x - 1.5, y + 0.5, z + 2);
  wheel3.rotation.x = Math.PI / 2;
  scene.add(wheel3);

  const wheel4 = new THREE.Mesh(wheelGeometry, wheelMaterial);
  wheel4.position.set(x + 1.5, y + 0.5, z + 2);
  wheel4.rotation.x = Math.PI / 2;
  scene.add(wheel4);

  // 添加标签
  const div = document.createElement('div');
  div.className = 'label';
  div.textContent = name;
  div.style.backgroundColor = 'rgba(0, 0, 0, 0.6)';
  div.style.color = 'white';
  div.style.padding = '2px 6px';
  div.style.borderRadius = '2px';
  div.style.fontSize = '12px';
  div.style.pointerEvents = 'none';
  const label = new CSS2DObject(div);
  label.position.set(0, 1.5, 0);
  agv.add(label);
};

// 创建提升机主体
const createElevatorBody = (x: number, y: number, z: number, name: string) => {
  const geometry = new THREE.BoxGeometry(3, 8, 3);
  const material = new THREE.MeshStandardMaterial({
    color: 0x4682b4,
    transparent: true,
    opacity: 0.7,
    metalness: 0.8,
    roughness: 0.2
  });
  const elevator = new THREE.Mesh(geometry, material);
  elevator.position.set(x, y + 4, z);
  elevator.castShadow = true;
  elevator.receiveShadow = true;
  scene.add(elevator);

  // 添加发光效果
  const glowMaterial = new THREE.MeshBasicMaterial({
    color: 0x00aaff,
    transparent: true,
    opacity: 0.2
  });
  const glowMesh = new THREE.Mesh(new THREE.BoxGeometry(3.5, 8.5, 3.5), glowMaterial);
  glowMesh.position.copy(elevator.position);
  scene.add(glowMesh);

  // 添加标签
  const div = document.createElement('div');
  div.className = 'label';
  div.textContent = name;
  div.style.backgroundColor = 'rgba(0, 0, 0, 0.6)';
  div.style.color = 'white';
  div.style.padding = '2px 6px';
  div.style.borderRadius = '2px';
  div.style.fontSize = '12px';
  div.style.pointerEvents = 'none';
  const label = new CSS2DObject(div);
  label.position.set(0, 5, 0);
  elevator.add(label);
};

// 创建高亮框
// 创建高亮框
const createHighlightBox = (x: number, y: number, z: number, width: number, height: number, color: number) => {
  const geometry = new THREE.BoxGeometry(width, height, 4);
  const material = new THREE.MeshBasicMaterial({
    color: color,
    wireframe: true,
    transparent: true,
    opacity: 0.7,
  });
  const box = new THREE.Mesh(geometry, material);
  box.position.set(x, y, z);
  scene.add(box);
};

// 加载库位数据
const loadLocationData = (area: string) => {
  // 清除之前的库位对象
  for (const obj of locationObjects) {
    scene.remove(obj);
  }
  locationObjects.length = 0;

  // 根据不同区域加载不同的库位数据
  switch (area) {
    case 'elevator1':
      // 1#粉料提升机
      createElevatorBody(0, 0, 0, '1#粉料提升机');
      // 创建周围的库位
      for (let i = 0; i < 5; i++) {
        createLocationBox(-6, 0, -5 + i * 2.5, `A-${i + 1}`, i % 3, area);
        createLocationBox(6, 0, -5 + i * 2.5, `B-${i + 1}`, (i + 1) % 3, area);
      }
      // 创建连接器
      createConnector(-3, 0.5, 0, 0, 0.5, 0, 0.3, 0x4682b4);
      createConnector(3, 0.5, 0, 0, 0.5, 0, 0.3, 0x4682b4);
      break;

    case 'elevator2':
      // 2#辅料提升机
      createElevatorBody(0, 0, 0, '2#辅料提升机');
      // 创建周围的库位
      for (let i = 0; i < 4; i++) {
        createLocationBox(-7, 0, -4 + i * 2.5, `C-${i + 1}`, i % 3, area);
        createLocationBox(7, 0, -4 + i * 2.5, `D-${i + 1}`, (i + 2) % 3, area);
      }
      // 创建高亮框
      createHighlightBox(0, 4, 0, 10, 10, 0x00ffff);
      break;

    case 'elevator3':
      // 3#正极卷提升机
      createElevatorBody(0, 0, 0, '3#正极卷提升机');
      // 创建周围的库位 - 双层结构
      for (let i = 0; i < 3; i++) {
        for (let j = 0; j < 2; j++) {
          createLocationBox(-5, j * 1.5, -3 + i * 3, `E-${j + 1}-${i + 1}`, (i + j) % 3, area);
          createLocationBox(5, j * 1.5, -3 + i * 3, `F-${j + 1}-${i + 1}`, (i + j + 1) % 3, area);
        }
      }
      break;

    case 'elevator4':
      // 4#负极卷提升机
      createElevatorBody(0, 0, 0, '4#负极卷提升机');
      // 创建环形库位
      const radius = 8;
      const count = 8;
      for (let i = 0; i < count; i++) {
        const angle = (i / count) * Math.PI * 2;
        const x = Math.cos(angle) * radius;
        const z = Math.sin(angle) * radius;
        createLocationBox(x, 0, z, `G-${i + 1}`, i % 3, area);
      }
      break;

    case 'elevator5':
      // 5#粉料装配提升机
      createElevatorBody(0, 0, 0, '5#粉料装配提升机');
      // 创建网格状库位
      for (let i = 0; i < 3; i++) {
        for (let j = 0; j < 3; j++) {
          createLocationBox(-6 + i * 6, 0, -6 + j * 6, `H-${i + 1}-${j + 1}`, (i + j) % 3, area);
        }
      }
      break;

    case 'agv':
      // 四向车
      createAgvBody(0, 0, 0, '四向车#1');
      createAgvBody(-10, 0, 5, '四向车#2');
      createAgvBody(10, 0, -5, '四向车#3');

      // 创建轨道路线
      const trackMaterial = new THREE.LineBasicMaterial({ color: 0x4682b4 });
      const trackPoints = [];
      trackPoints.push(new THREE.Vector3(-15, 0.1, -15));
      trackPoints.push(new THREE.Vector3(15, 0.1, -15));
      trackPoints.push(new THREE.Vector3(15, 0.1, 15));
      trackPoints.push(new THREE.Vector3(-15, 0.1, 15));
      trackPoints.push(new THREE.Vector3(-15, 0.1, -15));

      const trackGeometry = new THREE.BufferGeometry().setFromPoints(trackPoints);
      const track = new THREE.Line(trackGeometry, trackMaterial);
      scene.add(track);

      // 创建一些随机库位
      for (let i = 0; i < 10; i++) {
        const x = Math.random() * 30 - 15;
        const z = Math.random() * 30 - 15;
        if (Math.abs(x) > 5 || Math.abs(z) > 5) { // 避免与AGV重叠
          createLocationBox(x, 0, z, `AGV-${i + 1}`, Math.floor(Math.random() * 3), area);
        }
      }
      break;

    default:
      ElMessage.warning('未知区域');
      break;
  }
};



// 初始化Three.js场景
const initThree = () => {
  // 创建场景
  scene = new THREE.Scene();
  scene.background = new THREE.Color(0x0a1a2a);

  // 创建相机
  camera = new THREE.PerspectiveCamera(
    60,
    threeContainer.value.clientWidth / threeContainer.value.clientHeight,
    0.1,
    1000
  );
  camera.position.set(0, 15, 25);

  // 创建渲染器
  renderer = new THREE.WebGLRenderer({ antialias: true, alpha: true });
  renderer.setSize(threeContainer.value.clientWidth, threeContainer.value.clientHeight);
  renderer.shadowMap.enabled = true;
  threeContainer.value.appendChild(renderer.domElement);

  // 创建标签渲染器
  labelRenderer = new CSS2DRenderer();
  labelRenderer.setSize(threeContainer.value.clientWidth, threeContainer.value.clientHeight);
  labelRenderer.domElement.style.position = 'absolute';
  labelRenderer.domElement.style.top = '0';
  labelRenderer.domElement.style.pointerEvents = 'none';
  threeContainer.value.appendChild(labelRenderer.domElement);

  // 创建控制器
  controls = new OrbitControls(camera, renderer.domElement);
  controls.enableDamping = true;
  controls.dampingFactor = 0.05;
  controls.minDistance = 5;
  controls.maxDistance = 100;
  controls.maxPolarAngle = Math.PI / 2 - 0.1; // 限制相机不要移到地面以下

  // 创建光源
  const ambientLight = new THREE.AmbientLight(0xffffff, 0.4);
  scene.add(ambientLight);

  const directionalLight = new THREE.DirectionalLight(0xffffff, 0.8);
  directionalLight.position.set(10, 20, 10);
  directionalLight.castShadow = true;
  directionalLight.shadow.mapSize.width = 2048;
  directionalLight.shadow.mapSize.height = 2048;
  scene.add(directionalLight);

  // 添加点光源
  const pointLight1 = new THREE.PointLight(0x3388ff, 1, 50);
  pointLight1.position.set(-15, 10, -15);
  scene.add(pointLight1);

  const pointLight2 = new THREE.PointLight(0xff8833, 1, 50);
  pointLight2.position.set(15, 10, 15);
  scene.add(pointLight2);

  // 创建地面
  const groundGeometry = new THREE.PlaneGeometry(100, 100);
  const groundMaterial = new THREE.MeshStandardMaterial({
    color: 0x111a2a,
    side: THREE.DoubleSide,
    metalness: 0.5,
    roughness: 0.5
  });
  const ground = new THREE.Mesh(groundGeometry, groundMaterial);
  ground.rotation.x = -Math.PI / 2;
  ground.receiveShadow = true;
  scene.add(ground);

  // 创建网格辅助线
  const gridHelper = new THREE.GridHelper(100, 100, 0x444444, 0x222222);
  scene.add(gridHelper);

  // 加载库位数据
  loadLocationData(currentArea.value);

  // 动画循环
  animate();
};



// 初始化图表
const initCharts = () => {
  // 任务类型分布图表
  taskTypeChartInstance = echarts.init(taskTypeChart.value);
  const taskTypeOption = {
    tooltip: {
      trigger: 'item',
      formatter: '{a} <br/>{b}: {c} ({d}%)'
    },
    legend: {
      orient: 'vertical',
      right: 10,
      top: 'center',
      textStyle: {
        color: '#fff'
      }
    },
    series: [
      {
        name: '任务类型',
        type: 'pie',
        radius: ['50%', '70%'],
        avoidLabelOverlap: false,
        itemStyle: {
          borderRadius: 10,
          borderColor: '#fff',
          borderWidth: 2
        },
        label: {
          show: false,
          position: 'center'
        },
        emphasis: {
          label: {
            show: true,
            fontSize: '18',
            fontWeight: 'bold'
          }
        },
        labelLine: {
          show: false
        },
        data: [
          { value: 1048, name: '入库任务' },
          { value: 735, name: '出库任务' },
          { value: 580, name: '移库任务' },
          { value: 484, name: '盘点任务' },
          { value: 300, name: '其他任务' }
        ]
      }
    ]
  };
  taskTypeChartInstance.setOption(taskTypeOption);

  // 库位使用率图表
  storageUsageChartInstance = echarts.init(storageUsageChart.value);
  const storageUsageOption = {
    tooltip: {
      trigger: 'axis',
      axisPointer: {
        type: 'shadow'
      }
    },
    grid: {
      left: '3%',
      right: '4%',
      bottom: '3%',
      containLabel: true
    },
    xAxis: [
      {
        type: 'category',
        data: ['1#区', '2#区', '3#区', '4#区', '5#区', '6#区', '7#区'],
        axisTick: {
          alignWithLabel: true
        },
        axisLabel: {
          color: '#fff'
        }
      }
    ],
    yAxis: [
      {
        type: 'value',
        axisLabel: {
          color: '#fff'
        }
      }
    ],
    series: [
      {
        name: '使用率',
        type: 'bar',
        barWidth: '60%',
        data: [
          {
            value: 78,
            itemStyle: {
              color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
                { offset: 0, color: '#83bff6' },
                { offset: 0.5, color: '#188df0' },
                { offset: 1, color: '#188df0' }
              ])
            }
          },
          {
            value: 65,
            itemStyle: {
              color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
                { offset: 0, color: '#83bff6' },
                { offset: 0.5, color: '#188df0' },
                { offset: 1, color: '#188df0' }
              ])
            }
          },
          {
            value: 92,
            itemStyle: {
              color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
                { offset: 0, color: '#f5a97f' },
                { offset: 0.5, color: '#f56c6c' },
                { offset: 1, color: '#f56c6c' }
              ])
            }
          },
          {
            value: 45,
            itemStyle: {
              color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
                { offset: 0, color: '#83bff6' },
                { offset: 0.5, color: '#188df0' },
                { offset: 1, color: '#188df0' }
              ])
            }
          },
          {
            value: 88,
            itemStyle: {
              color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
                { offset: 0, color: '#f5a97f' },
                { offset: 0.5, color: '#f56c6c' },
                { offset: 1, color: '#f56c6c' }
              ])
            }
          },
          {
            value: 32,
            itemStyle: {
              color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
                { offset: 0, color: '#83bff6' },
                { offset: 0.5, color: '#188df0' },
                { offset: 1, color: '#188df0' }
              ])
            }
          },
          {
            value: 56,
            itemStyle: {
              color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
                { offset: 0, color: '#83bff6' },
                { offset: 0.5, color: '#188df0' },
                { offset: 1, color: '#188df0' }
              ])
            }
          }
        ]
      }
    ]
  };
  storageUsageChartInstance.setOption(storageUsageOption);
};

// 窗口大小变化时调整
const onWindowResize = () => {
  if (!threeContainer.value) return;

  camera.aspect = threeContainer.value.clientWidth / threeContainer.value.clientHeight;
  camera.updateProjectionMatrix();
  renderer.setSize(threeContainer.value.clientWidth, threeContainer.value.clientHeight);
  labelRenderer.setSize(threeContainer.value.clientWidth, threeContainer.value.clientHeight);

  // 重新调整图表大小
  if (taskTypeChartInstance) {
    taskTypeChartInstance.resize();
  }
  if (storageUsageChartInstance) {
    storageUsageChartInstance.resize();
  }
};

// 动画循环
const animate = () => {
  requestAnimationFrame(animate);
  controls.update();
  renderer.render(scene, camera);
  labelRenderer.render(scene, camera);
};

// 重置相机位置
const resetCamera = () => {
  camera.position.set(0, 15, 25);
  camera.lookAt(0, 0, 0);
  controls.update();
};

// 切换区域
const changeArea = (area: string) => {
  currentArea.value = area;
  loadLocationData(area);
  ElMessage.success(`已切换到${areaList.find(item => item.value === area)?.label}`);
};

// 组件挂载时初始化
onMounted(() => {
  // 初始化时间
  updateTime();
  timeInterval = setInterval(updateTime, 1000);

  // 初始化Three.js
  if (threeContainer.value) {
    initThree();
  }

  // 初始化图表
  if (taskTypeChart.value && storageUsageChart.value) {
    initCharts();
  }

  // 添加窗口大小变化监听
  window.addEventListener('resize', onWindowResize);
});

// 组件卸载前清理
onBeforeUnmount(() => {
  // 清除定时器
  if (timeInterval) {
    clearInterval(timeInterval);
  }

  // 移除事件监听
  window.removeEventListener('resize', onWindowResize);

  // 销毁图表实例
  if (taskTypeChartInstance) {
    taskTypeChartInstance.dispose();
  }
  if (storageUsageChartInstance) {
    storageUsageChartInstance.dispose();
  }

  // 清理Three.js资源
  if (renderer) {
    renderer.dispose();
    scene.clear();
  }
});
</script>

<style lang="less" scoped>
.screen-container {
  width: 100%;
  height: 100vh;
  background-color: #0a1a2a;
  color: #fff;
  overflow: hidden;
  display: flex;
  flex-direction: column;
  font-family: "Microsoft YaHei", Arial, sans-serif;

  // 顶部标题样式
  .header {
    height: 80px;
    padding: 0 20px;
    display: flex;
    align-items: center;
    justify-content: center;
    background: linear-gradient(to right, rgba(0, 0, 0, 0.5), rgba(0, 40, 80, 0.5), rgba(0, 0, 0, 0.5));
    border-bottom: 1px solid rgba(64, 158, 255, 0.3);
    box-shadow: 0 2px 10px rgba(0, 0, 0, 0.3);

    .title-container {
      position: relative;
      display: flex;
      align-items: center;
      justify-content: center;
      width: 100%;

      .title-bg {
        position: absolute;
        top: 0;
        left: 50%;
        transform: translateX(-50%);
        width: 500px;
        height: 60px;
        background: linear-gradient(to right, rgba(0, 0, 0, 0), rgba(64, 158, 255, 0.2), rgba(0, 0, 0, 0));
        border-radius: 0 0 50% 50%;
        z-index: 0;
      }

      .main-title {
        font-size: 32px;
        font-weight: bold;
        color: #fff;
        text-shadow: 0 0 10px rgba(64, 158, 255, 0.8);
        margin: 0;
        z-index: 1;
      }

      .time-display {
        position: absolute;
        right: 20px;
        font-size: 18px;
        color: #e6e6e6;
      }
    }
  }

  // 主体内容样式
  .content {
    flex: 1;
    display: flex;
    padding: 10px;
    gap: 10px;

    // 左侧面板
    .left-panel {
      width: 25%;
      display: flex;
      flex-direction: column;
      gap: 10px;
    }

    // 中间面板
    .center-panel {
      width: 50%;
      display: flex;
      flex-direction: column;
      gap: 10px;
    }

    // 右侧面板
    .right-panel {
      width: 25%;
      display: flex;
      flex-direction: column;
      gap: 10px;
    }

    // 面板通用样式
    .panel-item {
      flex: 1;
      background-color: rgba(0, 20, 40, 0.5);
      border: 1px solid rgba(64, 158, 255, 0.3);
      border-radius: 5px;
      box-shadow: 0 0 10px rgba(0, 0, 0, 0.3);
      display: flex;
      flex-direction: column;
      overflow: hidden;

      .panel-header {
        height: 40px;
        display: flex;
        align-items: center;
        justify-content: space-between;
        padding: 0 15px;
        background: linear-gradient(to right, rgba(0, 40, 80, 0.8), rgba(0, 20, 40, 0.5));
        border-bottom: 1px solid rgba(64, 158, 255, 0.3);

        h3 {
          margin: 0;
          font-size: 16px;
          font-weight: bold;
          color: #fff;
        }

        .control-buttons {
          display: flex;
          gap: 10px;
        }
      }

      .panel-content {
        flex: 1;
        padding: 15px;
        overflow: auto;

        &.chart-container {
          height: 100%;

          .chart {
            width: 100%;
            height: 100%;
          }
        }
      }
    }

    // 任务进度条样式
    .task-progress {
      display: flex;
      flex-direction: column;
      gap: 15px;

      .progress-item {
        .progress-info {
          display: flex;
          justify-content: space-between;
          margin-bottom: 5px;

          .progress-name {
            font-size: 14px;
            color: #e6e6e6;
          }

          .progress-value {
            font-size: 14px;
            color: #fff;
            font-weight: bold;
          }
        }

        .progress-bar {
          height: 8px;
          background-color: rgba(255, 255, 255, 0.1);
          border-radius: 4px;
          overflow: hidden;

          .progress-inner {
            height: 100%;
            border-radius: 4px;
            transition: width 0.3s ease;
          }
        }
      }
    }

    // 任务列表样式
    .task-list {
      display: flex;
      flex-direction: column;
      gap: 10px;

      .task-item {
        display: flex;
        align-items: center;
        gap: 10px;
        padding: 10px;
        background-color: rgba(0, 0, 0, 0.2);
        border-radius: 5px;
        border-left: 3px solid #409EFF;
        transition: all 0.3s ease;

        &.warning {
          border-left-color: #E6A23C;
          background-color: rgba(230, 162, 60, 0.1);
        }

        &.success {
          border-left-color: #67C23A;
          background-color: rgba(103, 194, 58, 0.1);
        }

        &:hover {
          transform: translateX(5px);
          box-shadow: 0 0 10px rgba(64, 158, 255, 0.3);
        }

        .task-icon {
          width: 40px;
          height: 40px;
          display: flex;
          align-items: center;
          justify-content: center;
          background-color: rgba(64, 158, 255, 0.1);
          border-radius: 50%;

          i {
            font-size: 20px;
            color: #409EFF;
          }
        }

        .task-info {
          flex: 1;

          .task-name {
            font-size: 14px;
            font-weight: bold;
            color: #fff;
            margin-bottom: 5px;
          }

          .task-desc {
            font-size: 12px;
            color: #a0a0a0;
          }
        }

        .task-status {
          span {
            padding: 2px 8px;
            border-radius: 10px;
            font-size: 12px;

            &.status-running {
              background-color: rgba(64, 158, 255, 0.2);
              color: #409EFF;
            }

            &.status-success {
              background-color: rgba(103, 194, 58, 0.2);
              color: #67C23A;
            }

            &.status-warning {
              background-color: rgba(230, 162, 60, 0.2);
              color: #E6A23C;
            }

            &.status-waiting {
              background-color: rgba(144, 147, 153, 0.2);
              color: #909399;
            }
          }
        }
      }
    }

    // 3D容器样式
    .three-container {
      width: 100%;
      height: 100%;
      position: relative;

      canvas {
        width: 100%;
        height: 100%;
        outline: none;
      }
    }

    // 状态图例样式
    .status-legend {
      position: absolute;
      bottom: 20px;
      right: 20px;
      display: flex;
      gap: 15px;
      background-color: rgba(0, 0, 0, 0.6);
      padding: 8px 15px;
      border-radius: 5px;

      .legend-item {
        display: flex;
        align-items: center;
        gap: 5px;

        .color-block {
          width: 15px;
          height: 15px;
          border-radius: 3px;
        }

        span {
          font-size: 12px;
          color: #fff;
        }
      }
    }
  }

  // 底部数据统计样式
  .footer {
    height: 100px;
    display: flex;
    justify-content: space-around;
    align-items: center;
    background: linear-gradient(to bottom, rgba(0, 20, 40, 0.5), rgba(0, 40, 80, 0.8));
    border-top: 1px solid rgba(64, 158, 255, 0.3);

    .data-block {
      display: flex;
      align-items: center;
      gap: 15px;

      .data-icon {
        width: 50px;
        height: 50px;
        display: flex;
        align-items: center;
        justify-content: center;
        background: linear-gradient(135deg, rgba(64, 158, 255, 0.3), rgba(0, 40, 80, 0.5));
        border-radius: 10px;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.3);

        i {
          font-size: 24px;
          color: #fff;
        }
      }

      .data-info {
        .data-value {
          font-size: 24px;
          font-weight: bold;
          color: #fff;
          margin-bottom: 5px;
        }

        .data-name {
          font-size: 14px;
          color: #a0a0a0;
        }
      }
    }
  }
}

// 自定义滚动条样式
::-webkit-scrollbar {
  width: 6px;
  height: 6px;
}

::-webkit-scrollbar-track {
  background: rgba(0, 0, 0, 0.1);
  border-radius: 3px;
}

::-webkit-scrollbar-thumb {
  background: rgba(64, 158, 255, 0.5);
  border-radius: 3px;
}

::-webkit-scrollbar-thumb:hover {
  background: rgba(64, 158, 255, 0.8);
}

// 适配不同屏幕尺寸
@media screen and (max-width: 1600px) {
  .screen-container {
    .header {
      height: 60px;

      .title-container {
        .main-title {
          font-size: 28px;
        }
      }
    }

    .footer {
      height: 80px;

      .data-block {
        .data-icon {
          width: 40px;
          height: 40px;

          i {
            font-size: 20px;
          }
        }

        .data-info {
          .data-value {
            font-size: 20px;
          }

          .data-name {
            font-size: 12px;
          }
        }
      }
    }
  }
}

@media screen and (max-width: 1200px) {
  .screen-container {
    .content {
      flex-direction: column;

      .left-panel,
      .center-panel,
      .right-panel {
        width: 100%;
        height: auto;
      }

      .panel-item {
        min-height: 300px;
      }
    }
  }
}
</style>