<template>
  <div class="location-container">
    <div class="location-header">
      <h2>库位管理</h2>
      <div class="header-actions">
        <el-button size="small" type="primary" @click="refreshData">刷新</el-button>
        <el-button size="small" @click="toggleLegend">图例 {{ showLegend ? '隐藏' : '显示' }}</el-button>
      </div>
    </div>

    <div class="location-content">
      <!-- 左侧区域列表 -->
      <div class="location-sidebar">
        <div v-for="(area, index) in areaList" :key="index" class="area-item"
          :class="{ active: currentArea === area.value }" @click="selectArea(area.value)">
          {{ area.label }}
        </div>
      </div>

      <!-- 右侧库位地图 -->
      <div class="location-map">
        <div class="map-legend" v-if="showLegend">
          <div class="legend-item">
            <div class="legend-color status-0"></div>
            <span>空闲</span>
          </div>
          <div class="legend-item">
            <div class="legend-color status-1"></div>
            <span>占用</span>
          </div>
          <div class="legend-item">
            <div class="legend-color status-2"></div>
            <span>故障</span>
          </div>
          <div class="legend-item">
            <div class="legend-color status-3"></div>
            <span>预留</span>
          </div>
        </div>

        <div class="map-grid">
          <div class="map-row" v-for="(row, rowIndex) in locationMap" :key="rowIndex">
            <div v-for="(cell, colIndex) in row" :key="colIndex" class="map-cell" :class="[
          `status-${cell.status}`,
          (cell as any).selected ? 'selected' : ''
        ]" @click="selectLocation(cell)">
              <div class="cell-code">{{ cell.code }}</div>
              <div class="cell-id" v-if="cell.id">{{ cell.id }}</div>
              <div class="cell-status-icon" v-if="cell.status === 2">
                <i class="el-icon-warning"></i>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- 库位详情弹窗 -->
    <el-dialog title="库位详情" v-model="dialogVisible" width="30%">
      <div v-if="selectedLocation">
        <p><strong>库位编号:</strong> {{ selectedLocation?.code }}</p>
        <p><strong>货物ID:</strong> {{ selectedLocation.id || '无' }}</p>
        <p><strong>状态:</strong> {{ selectedLocation && statusText[selectedLocation.status as keyof typeof statusText] }}
        </p>
      </div>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="dialogVisible = false">关闭</el-button>
          <el-button type="primary" @click="handleOperation">操作</el-button>
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted } from 'vue';
import { ElMessage } from 'element-plus';

// 区域列表
const areaList = reactive([
  { label: '小件库存储区', value: 'small' },
  { label: '网络干扰仿真测试', value: 'network' },
  { label: '网络和UDP通讯测试', value: 'udp' },
  { label: 'A4纸仿真测试区域', value: 'a4' },
  { label: '包裹和ECD测试区域', value: 'package' },
  { label: '2D和1D扫码测试区', value: '2d1d' },
  { label: '3D和cup测试区域', value: '3dcup' },
  { label: '长尺上料测试区', value: 'long' },
  { label: '2kg下料测试区', value: '2kg' },
  { label: '复合下料测试区', value: 'composite' },
  { label: '梯形下料测试区', value: 'trapezoid' },
  { label: '土豆测试区域', value: 'potato' },
  { label: '梯形上料测试区', value: 'trapezoidUp' },
  { label: '梯形上料测试区2', value: 'trapezoidUp2' },
  { label: '梯形上料测试区3', value: 'trapezoidUp3' },
  { label: '一体产线生产测试区', value: 'integrated' },
]);

// 当前选中的区域
const currentArea = ref('small');

// 库位状态文本
const statusText = {
  0: '空闲',
  1: '占用',
  2: '故障',
  3: '预留'
};

// 库位地图数据
const locationMap = reactive([
  [
    { code: 'JRZC01Z', id: 'D00153', status: 1 },
    { code: 'JRZC02Z', id: 'D00127', status: 1 },
    { code: 'JRZC03Z', id: '', status: 0 },
    { code: 'JRZC04Z', id: 'D00075', status: 1 },
    { code: 'JRZC05Z', id: 'D00094', status: 2 },
    { code: 'JRZC06Z', id: 'D00069', status: 1 },
    { code: 'JRZC07Z', id: '', status: 0 },
    { code: 'JRZC08Z', id: '', status: 2 },
    { code: 'JRZC09Z', id: 'D00336', status: 1 },
    { code: 'JRZC10Z', id: '', status: 0 },
    { code: 'JRZC11Z', id: '', status: 0 },
    { code: 'JRZC12Z', id: '', status: 0 },
  ],
  [
    { code: 'JRZC13Z', id: 'D00005', status: 1 },
    { code: 'JRZC31F', id: 'D00147', status: 1 },
    { code: 'JRZC02F', id: '', status: 0 },
    { code: 'JRZC03F', id: 'D00244', status: 1 },
    { code: 'JRZC04F', id: 'D00071', status: 1 },
    { code: 'JRZC05F', id: '', status: 0 },
    { code: 'JRZC06F', id: 'D00337', status: 1 },
    { code: 'JRZC07F', id: 'D00026', status: 1 },
    { code: 'JRZC08F', id: 'D00148', status: 1 },
    { code: 'JRZC09F', id: 'D00033', status: 1 },
    { code: 'JRZC10F', id: '', status: 0 },
    { code: 'JRZC11F', id: '', status: 0 },
  ]
]);

// 对话框显示状态
const dialogVisible = ref(false);
// 当前选中的库位
const selectedLocation = ref(null);
// 是否显示图例
const showLegend = ref(true);

// 选择区域
const selectArea = (area: string) => {
  currentArea.value = area;
  // 这里可以根据选择的区域加载对应的库位数据
  // loadLocationData(area);
  ElMessage.success(`已切换到${areaList.find(item => item.value === area)?.label}`);
};

// 选择库位
const selectLocation = (cell: any) => {
  // 清除之前的选择
  locationMap.forEach(row => {
    row.forEach(item => {
      (item as any).selected = false;
    });
  });

  // 设置当前选中
  cell.selected = true;
  selectedLocation.value = cell;
  dialogVisible.value = true;
};

// 刷新数据
const refreshData = () => {
  ElMessage.success('数据已刷新');
  // 这里可以调用API重新获取库位数据
};

// 切换图例显示
const toggleLegend = () => {
  showLegend.value = !showLegend.value;
};

// 处理库位操作
const handleOperation = () => {
  if (!selectedLocation.value) return;

  const location = selectedLocation.value;
  ElMessage.success(`对库位 ${(location as any).code} 执行操作成功`);
  dialogVisible.value = false;
};

onMounted(() => {
  // 初始化加载数据
  // loadLocationData(currentArea.value);
});
</script>

<style lang="less" scoped>
.location-container {
  height: 100%;
  display: flex;
  flex-direction: column;
  background-color: #f5f7fa;
}

.location-header {
  padding: 10px 20px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  border-bottom: 1px solid #e4e7ed;

  h2 {
    margin: 0;
    font-size: 18px;
  }

  .header-actions {
    display: flex;
    gap: 10px;
  }
}

.location-content {
  flex: 1;
  display: flex;
  overflow: hidden;
}

.location-sidebar {
  width: 200px;
  border-right: 1px solid #e4e7ed;
  overflow-y: auto;
  background-color: #fff;

  .area-item {
    padding: 12px 15px;
    cursor: pointer;
    border-bottom: 1px solid #f0f0f0;

    &:hover {
      background-color: #f5f7fa;
    }

    &.active {
      background-color: #ecf5ff;
      color: #409eff;
      font-weight: bold;
    }
  }
}

.location-map {
  flex: 1;
  padding: 20px;
  overflow: auto;
  position: relative;

  .map-legend {
    position: absolute;
    top: 10px;
    right: 20px;
    background-color: rgba(255, 255, 255, 0.9);
    border: 1px solid #e4e7ed;
    border-radius: 4px;
    padding: 10px;
    display: flex;
    gap: 15px;

    .legend-item {
      display: flex;
      align-items: center;
      gap: 5px;

      .legend-color {
        width: 16px;
        height: 16px;
        border-radius: 3px;
      }
    }
  }

  .map-grid {
    display: flex;
    flex-direction: column;
    gap: 10px;
    margin-top: 40px;

    .map-row {
      display: flex;
      gap: 10px;
    }

    .map-cell {
      width: 80px;
      height: 80px;
      border-radius: 4px;
      display: flex;
      flex-direction: column;
      justify-content: center;
      align-items: center;
      cursor: pointer;
      position: relative;
      transition: all 0.3s;
      box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);

      &:hover {
        transform: translateY(-2px);
        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.15);
      }

      &.selected {
        border: 2px solid #409eff;
      }

      .cell-code {
        font-size: 12px;
        font-weight: bold;
        margin-bottom: 5px;
      }

      .cell-id {
        font-size: 12px;
      }

      .cell-status-icon {
        position: absolute;
        top: 5px;
        right: 5px;
        color: #f56c6c;
      }
    }

    // 状态颜色
    .status-0 {
      background-color: #67c23a;
      color: #fff;
    }

    .status-1 {
      background-color: #e6a23c;
      color: #fff;
    }

    .status-2 {
      background-color: #f56c6c;
      color: #fff;
    }

    .status-3 {
      background-color: #909399;
      color: #fff;
    }
  }
}

.legend-color {
  &.status-0 {
    background-color: #67c23a;
  }

  &.status-1 {
    background-color: #e6a23c;
  }

  &.status-2 {
    background-color: #f56c6c;
  }

  &.status-3 {
    background-color: #909399;
  }
}
</style>