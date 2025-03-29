<template>
  <div class="dashboard-container">
    <!-- 顶部卡片统计 -->
    <div class="top-cards">
      <el-row :gutter="20">
        <el-col :span="6" v-for="(item, index) in statisticsData" :key="index">
          <el-card class="statistic-card" :body-style="{ padding: '0px' }">
            <div class="card-content">
              <div class="icon-container" :style="{ backgroundColor: item.bgColor }">
                <el-icon>
                  <component :is="item.icon"></component>
                </el-icon>
              </div>
              <div class="data-container">
                <div class="data-value">{{ item.value }}</div>
                <div class="data-title">{{ item.title }}</div>
              </div>
            </div>
            <div class="card-footer" :style="{ backgroundColor: item.bgColor }">
              <span>{{ item.footerText }}</span>
              <el-icon>
                <ArrowRight />
              </el-icon>
            </div>
          </el-card>
        </el-col>
      </el-row>
    </div>

    <!-- 中间图表区域 -->
    <div class="middle-charts">
      <el-row :gutter="20">
        <el-col :span="16">
          <el-card class="chart-card">
            <template #header>
              <div class="card-header">
                <span>系统运行状态</span>
                <div class="header-right">
                  <el-radio-group v-model="timeRange" size="small">
                    <el-radio-button label="today">今日</el-radio-button>
                    <el-radio-button label="week">本周</el-radio-button>
                    <el-radio-button label="month">本月</el-radio-button>
                  </el-radio-group>
                </div>
              </div>
            </template>
            <div class="chart-container" ref="systemStatusChart"></div>
          </el-card>
        </el-col>
        <el-col :span="8">
          <el-card class="chart-card">
            <template #header>
              <div class="card-header">
                <span>任务类型分布</span>
                <el-button type="text">
                  <el-icon>
                    <Refresh />
                  </el-icon>
                </el-button>
              </div>
            </template>
            <div class="chart-container" ref="taskTypeChart"></div>
          </el-card>
        </el-col>
      </el-row>
    </div>

    <!-- 设备状态表格 -->
    <el-card class="device-table-card">
      <template #header>
        <div class="card-header">
          <span>设备运行状态</span>
          <div class="header-right">
            <el-input v-model="searchKeyword" placeholder="搜索设备" prefix-icon="Search" style="width: 200px" />
            <el-button type="primary" size="default">
              <el-icon>
                <Plus />
              </el-icon>
              添加设备
            </el-button>
          </div>
        </div>
      </template>
      <el-table
        :data="deviceList.filter(data => !searchKeyword || data.name.toLowerCase().includes(searchKeyword.toLowerCase()))"
        style="width: 100%">
        <el-table-column prop="id" label="设备ID" width="100" />
        <el-table-column prop="name" label="设备名称" width="180" />
        <el-table-column prop="type" label="设备类型" width="120" />
        <el-table-column prop="location" label="位置" width="150" />
        <el-table-column prop="lastActive" label="最后活动时间" width="180" />
        <el-table-column prop="status" label="状态">
          <template #default="scope">
            <el-tag :type="getStatusType(scope.row.status)">{{ scope.row.status }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column label="CPU使用率" width="180">
          <template #default="scope">
            <el-progress :percentage="scope.row.cpu" :color="getCpuColor(scope.row.cpu)" />
          </template>
        </el-table-column>
        <el-table-column label="操作">
          <template #default="scope">
            <el-button size="small" @click="handleView(scope.row)">查看</el-button>
            <el-button size="small" type="primary" @click="handleEdit(scope.row)">编辑</el-button>
            <el-button size="small" type="danger" @click="handleDelete(scope.row)">删除</el-button>
          </template>
        </el-table-column>
      </el-table>
      <div class="pagination-container">
        <el-pagination v-model:current-page="currentPage" v-model:page-size="pageSize" :page-sizes="[10, 20, 30, 50]"
          layout="total, sizes, prev, pager, next, jumper" :total="totalDevices" @size-change="handleSizeChange"
          @current-change="handleCurrentChange" />
      </div>
    </el-card>

    <!-- 最近告警信息 -->
    <el-card class="alert-card">
      <template #header>
        <div class="card-header">
          <span>最近告警信息</span>
          <el-button type="text">查看全部</el-button>
        </div>
      </template>
      <el-timeline>
        <el-timeline-item v-for="(alert, index) in alertList" :key="index" :type="getAlertType(alert.level)"
          :timestamp="alert.time" :hollow="alert.hollow">
          <el-card class="alert-item-card">
            <h4>{{ alert.title }}</h4>
            <p>{{ alert.content }}</p>
            <div class="alert-footer">
              <span>设备: {{ alert.device }}</span>
              <el-button size="small" type="primary" plain>处理</el-button>
            </div>
          </el-card>
        </el-timeline-item>
      </el-timeline>
    </el-card>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, onUnmounted } from 'vue';
import * as echarts from 'echarts';
import {
  Connection,
  CpuChip,
  Odometer,
  TrendCharts,
  ArrowRight,
  Refresh,
  Search,
  Plus
} from '@element-plus/icons-vue';

// 统计数据
const statisticsData = ref([
  {
    title: '设备总数',
    value: 128,
    icon: 'CpuChip',
    bgColor: '#409EFF',
    footerText: '查看详情'
  },
  {
    title: '在线设备',
    value: 112,
    icon: 'Connection',
    bgColor: '#67C23A',
    footerText: '在线率: 87.5%'
  },
  {
    title: '今日任务数',
    value: 256,
    icon: 'Odometer',
    bgColor: '#E6A23C',
    footerText: '完成率: 92%'
  },
  {
    title: '系统性能',
    value: '良好',
    icon: 'TrendCharts',
    bgColor: '#909399',
    footerText: 'CPU: 32% | 内存: 45%'
  }
]);

// 时间范围选择
const timeRange = ref('today');

// 图表引用
const systemStatusChart = ref(null);
const taskTypeChart = ref(null);
let systemStatusChartInstance: echarts.ECharts | null = null;
let taskTypeChartInstance: echarts.ECharts | null = null;

// 设备列表数据
const searchKeyword = ref('');
const currentPage = ref(1);
const pageSize = ref(10);
const totalDevices = ref(100);

const deviceList = ref([
  { id: 'DEV001', name: '1#提升机', type: '提升机', location: 'A区', lastActive: '2023-06-15 14:30:22', status: '运行中', cpu: 45 },
  { id: 'DEV002', name: '2#提升机', type: '提升机', location: 'A区', lastActive: '2023-06-15 14:28:15', status: '运行中', cpu: 38 },
  { id: 'DEV003', name: '3#提升机', type: '提升机', location: 'B区', lastActive: '2023-06-15 14:25:10', status: '运行中', cpu: 42 },
  { id: 'DEV004', name: '1#四向车', type: 'AGV', location: 'C区', lastActive: '2023-06-15 14:20:05', status: '空闲', cpu: 12 },
  { id: 'DEV005', name: '2#四向车', type: 'AGV', location: 'D区', lastActive: '2023-06-15 14:15:30', status: '运行中', cpu: 65 },
  { id: 'DEV006', name: '3#四向车', type: 'AGV', location: 'E区', lastActive: '2023-06-15 14:10:45', status: '故障', cpu: 88 },
  { id: 'DEV007', name: '1#堆垛机', type: '堆垛机', location: 'F区', lastActive: '2023-06-15 14:05:20', status: '运行中', cpu: 56 },
  { id: 'DEV008', name: '2#堆垛机', type: '堆垛机', location: 'F区', lastActive: '2023-06-15 14:00:10', status: '维护中', cpu: 0 },
  { id: 'DEV009', name: '1#输送线', type: '输送线', location: 'G区', lastActive: '2023-06-15 13:55:05', status: '运行中', cpu: 32 },
  { id: 'DEV010', name: '2#输送线', type: '输送线', location: 'G区', lastActive: '2023-06-15 13:50:30', status: '运行中', cpu: 28 }
]);

// 告警信息
const alertList = ref([
  {
    level: 'error',
    time: '2023-06-15 14:22:33',
    title: '设备故障告警',
    content: '3#四向车电机过热，已自动停机，请检查。',
    device: '3#四向车',
    hollow: false
  },
  {
    level: 'warning',
    time: '2023-06-15 13:45:12',
    title: '系统警告',
    content: '2#堆垛机传感器数据异常，请注意检查。',
    device: '2#堆垛机',
    hollow: false
  },
  {
    level: 'info',
    time: '2023-06-15 12:30:45',
    title: '系统通知',
    content: '系统将于今晚22:00进行例行维护，预计持续2小时。',
    device: '系统',
    hollow: true
  },
  {
    level: 'success',
    time: '2023-06-15 11:15:22',
    title: '维护完成',
    content: '2#输送线维护工作已完成，系统已恢复正常运行。',
    device: '2#输送线',
    hollow: false
  }
]);

// 获取状态类型
const getStatusType = (status: string) => {
  switch (status) {
    case '运行中':
      return 'success';
    case '空闲':
      return 'info';
    case '故障':
      return 'danger';
    case '维护中':
      return 'warning';
    default:
      return '';
  }
};

// 获取CPU颜色
const getCpuColor = (cpu: number) => {
  if (cpu < 50) return '#67C23A';
  if (cpu < 80) return '#E6A23C';
  return '#F56C6C';
};

// 获取告警类型
const getAlertType = (level: string) => {
  switch (level) {
    case 'error':
      return 'danger';
    case 'warning':
      return 'warning';
    case 'info':
      return 'info';
    case 'success':
      return 'success';
    default:
      return '';
  }
};

// 表格操作方法
const handleView = (row: any) => {
  console.log('查看设备', row);
};

const handleEdit = (row: any) => {
  console.log('编辑设备', row);
};

const handleDelete = (row: any) => {
  console.log('删除设备', row);
};

// 分页方法
const handleSizeChange = (val: number) => {
  pageSize.value = val;
  // 这里应该重新加载数据
  console.log('每页显示数量变更为:', val);
};

const handleCurrentChange = (val: number) => {
  currentPage.value = val;
  // 这里应该重新加载数据
  console.log('当前页变更为:', val);
};

// 初始化系统状态图表
const initSystemStatusChart = () => {
  if (!systemStatusChart.value) return;

  systemStatusChartInstance = echarts.init(systemStatusChart.value);

  const option = {
    tooltip: {
      trigger: 'axis',
      axisPointer: {
        type: 'cross',
        label: {
          backgroundColor: '#6a7985'
        }
      }
    },
    legend: {
      data: ['CPU使用率', '内存使用率', '网络流量', '磁盘I/O']
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
        boundaryGap: false,
        data: ['00:00', '03:00', '06:00', '09:00', '12:00', '15:00', '18:00', '21:00']
      }
    ],
    yAxis: [
      {
        type: 'value',
        name: '百分比',
        min: 0,
        max: 100,
        interval: 20
      },
      {
        type: 'value',
        name: 'MB/s',
        min: 0,
        max: 200,
        interval: 50
      }
    ],
    series: [
      {
        name: 'CPU使用率',
        type: 'line',
        stack: 'Total',
        areaStyle: {},
        emphasis: {
          focus: 'series'
        },
        data: [30, 28, 25, 45, 60, 52, 38, 32]
      },
      {
        name: '内存使用率',
        type: 'line',
        stack: 'Total',
        areaStyle: {},
        emphasis: {
          focus: 'series'
        },
        data: [45, 42, 40, 55, 65, 60, 50, 48]
      },
      {
        name: '网络流量',
        type: 'line',
        yAxisIndex: 1,
        emphasis: {
          focus: 'series'
        },
        data: [120, 82, 65, 130, 180, 150, 110, 95]
      },
      {
        name: '磁盘I/O',
        type: 'line',
        yAxisIndex: 1,
        emphasis: {
          focus: 'series'
        },
        data: [45, 38, 32, 68, 90, 75, 60, 52]
      }
    ]
  };

  systemStatusChartInstance.setOption(option);
};

// 初始化任务类型图表
const initTaskTypeChart = () => {
  if (!taskTypeChart.value) return;

  taskTypeChartInstance = echarts.init(taskTypeChart.value);

  const option = {
    tooltip: {
      trigger: 'item'
    },
    legend: {
      orient: 'vertical',
      left: 'left'
    },
    series: [
      {
        name: '任务类型',
        type: 'pie',
        radius: '70%',
        data: [
          { value: 1048, name: '入库任务' },
          { value: 735, name: '出库任务' },
          { value: 580, name: '移库任务' },
          { value: 484, name: '盘点任务' },
          { value: 300, name: '其他任务' }
        ],
        emphasis: {
          itemStyle: {
            shadowBlur: 10,
            shadowOffsetX: 0,
            shadowColor: 'rgba(0, 0, 0, 0.5)'
          }
        }
      }
    ]
  };

  taskTypeChartInstance.setOption(option);
};

// 窗口大小变化时调整图表大小
const handleResize = () => {
  systemStatusChartInstance?.resize();
  taskTypeChartInstance?.resize();
};

// 组件挂载时初始化
onMounted(() => {
  // 初始化图表
  setTimeout(() => {
    initSystemStatusChart();
    initTaskTypeChart();
  }, 100);

  // 添加窗口大小变化监听
  window.addEventListener('resize', handleResize);
});

// 组件卸载前清理
onUnmounted(() => {
  // 移除事件监听
  window.removeEventListener('resize', handleResize);

  // 销毁图表实例
  systemStatusChartInstance?.dispose();
  taskTypeChartInstance?.dispose();
});
</script>

<style lang="less" scoped>
.dashboard-container {
  padding: 20px;
  background-color: #f5f7fa;
  min-height: calc(100vh - 60px);

  .top-cards {
    margin-bottom: 20px;

    .statistic-card {
      height: 120px;
      overflow: hidden;
      border-radius: 8px;
      transition: all 0.3s;

      &:hover {
        transform: translateY(-5px);
        box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
      }

      .card-content {
        display: flex;
        height: 80px;

        .icon-container {
          width: 80px;
          height: 80px;
          display: flex;
          align-items: center;
          justify-content: center;

          .el-icon {
            font-size: 32px;
            color: white;
          }
        }

        .data-container {
          flex: 1;
          padding: 15px;
          display: flex;
          flex-direction: column;
          justify-content: center;

          .data-value {
            font-size: 24px;
            font-weight: bold;
            margin-bottom: 5px;
          }

          .data-title {
            font-size: 14px;
            color: #909399;
          }
        }
      }

      .card-footer {
        height: 40px;
        display: flex;
        align-items: center;
        justify-content: space-between;
        padding: 0 15px;
        color: white;
        font-size: 12px;
      }
    }
  }

  .middle-charts {
    margin-bottom: 20px;

    .chart-card {
      margin-bottom: 20px;

      .card-header {
        display: flex;
        justify-content: space-between;
        align-items: center;
      }

      .chart-container {
        height: 350px;
      }
    }
  }

  .device-table-card {
    margin-bottom: 20px;

    .card-header {
      display: flex;
      justify-content: space-between;
      align-items: center;

      .header-right {
        display: flex;
        gap: 10px;
      }
    }

    .pagination-container {
      margin-top: 20px;
      display: flex;
      justify-content: flex-end;
    }
  }

  .alert-card {
    .card-header {
      display: flex;
      justify-content: space-between;
      align-items: center;
    }

    .alert-item-card {
      margin-bottom: 10px;

      h4 {
        margin: 0 0 10px 0;
      }

      p {
        margin: 0 0 10px 0;
        color: #606266;
      }

      .alert-footer {
        display: flex;
        justify-content: space-between;
        align-items: center;
        color: #909399;
        font-size: 12px;
      }
    }
  }
}
</style>