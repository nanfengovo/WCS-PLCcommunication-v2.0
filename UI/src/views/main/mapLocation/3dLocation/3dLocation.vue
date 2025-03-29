<template>
  <div class="location-container">
    <div class="location-header">
      <h2>库位地图</h2>
      <div class="header-actions">
        <el-button size="small" type="primary" @click="refreshData">刷新</el-button>
        <el-select v-model="currentArea" placeholder="选择区域" size="small" @change="changeArea">
          <el-option v-for="item in areaList" :key="item.value" :label="item.label" :value="item.value">
          </el-option>
        </el-select>
      </div>
    </div>

    <div class="location-content">
      <!-- 左侧区域列表 -->
      <div class="location-sidebar">
        <div v-for="(area, index) in areaList" :key="index" class="area-item"
          :class="{ active: currentArea === area.value }" @click="changeArea(area.value)">
          {{ area.label }}
        </div>
      </div>

      <!-- 右侧库位地图 -->
      <div class="location-map">
        <div class="map-title">{{ getCurrentAreaLabel() }}</div>

        <!-- 1#粉料提升机 -->
        <div v-if="currentArea === 'elevator1'" class="map-layout elevator1-layout">
          <div class="location-row">
            <div class="location-cell empty"></div>
            <div class="location-cell" :class="getStatusClass('1004')" @click="selectLocation('1004', '1#粉料提升机')">1004
            </div>
            <div class="location-cell" :class="getStatusClass('1005')" @click="selectLocation('1005', '1#粉料提升机')">1005
            </div>
            <div class="location-cell empty"></div>
          </div>
          <div class="location-row">
            <div class="location-cell" :class="getStatusClass('1001')" @click="selectLocation('1001', '1#粉料提升机')">1001
            </div>
            <div class="location-cell empty"></div>
            <div class="location-cell empty"></div>
            <div class="location-cell empty"></div>
          </div>
          <div class="location-row">
            <div class="location-cell empty"></div>
            <div class="location-cell" :class="getStatusClass('1002')" @click="selectLocation('1002', '1#粉料提升机')">1002
            </div>
            <div class="location-cell" :class="getStatusClass('1003')" @click="selectLocation('1003', '1#粉料提升机')">1003
            </div>
            <div class="location-cell empty"></div>
          </div>
        </div>

        <!-- 2#辅料提升机 -->
        <div v-if="currentArea === 'elevator2'" class="map-layout elevator2-layout">
          <div class="location-row">
            <div class="location-cell empty"></div>
            <div class="location-cell" :class="getStatusClass('1006')" @click="selectLocation('1006', '2#辅料提升机')">1006
            </div>
            <div class="location-cell" :class="getStatusClass('1007')" @click="selectLocation('1007', '2#辅料提升机')">1007
            </div>
          </div>
          <div class="location-row">
            <div class="location-cell empty"></div>
            <div class="location-cell" :class="getStatusClass('1001')" @click="selectLocation('1001', '2#辅料提升机')">1001
            </div>
            <div class="location-cell" :class="getStatusClass('1004')" @click="selectLocation('1004', '2#辅料提升机')">1004
            </div>
            <div class="location-cell" :class="getStatusClass('1005')" @click="selectLocation('1005', '2#辅料提升机')">1005
            </div>
          </div>
          <div class="location-row">
            <div class="location-cell" :class="getStatusClass('1003')" @click="selectLocation('1003', '2#辅料提升机')">1003
            </div>
            <div class="location-cell" :class="getStatusClass('1002')" @click="selectLocation('1002', '2#辅料提升机')">1002
            </div>
          </div>
        </div>

        <!-- 3#正极卷提升机 -->
        <div v-if="currentArea === 'elevator3'" class="map-layout elevator3-layout">
          <div class="location-row">
            <div class="location-cell empty"></div>
            <div class="location-cell" :class="getStatusClass('1009')" @click="selectLocation('1009', '3#正极卷提升机')">1009
            </div>
            <div class="location-cell" :class="getStatusClass('1008')" @click="selectLocation('1008', '3#正极卷提升机')">1008
            </div>
            <div class="location-cell empty"></div>
          </div>
          <div class="location-row">
            <div class="location-cell" :class="getStatusClass('1001')" @click="selectLocation('1001', '3#正极卷提升机')">1001
            </div>
            <div class="location-cell empty"></div>
            <div class="location-cell empty"></div>
            <div class="location-cell empty"></div>
          </div>
          <div class="location-row">
            <div class="location-cell empty"></div>
            <div class="location-cell" :class="getStatusClass('1005')" @click="selectLocation('1005', '3#正极卷提升机')">1005
            </div>
            <div class="location-cell" :class="getStatusClass('1004')" @click="selectLocation('1004', '3#正极卷提升机')">1004
            </div>
            <div class="location-cell" :class="getStatusClass('1003')" @click="selectLocation('1003', '3#正极卷提升机')">1003
            </div>
            <div class="location-cell" :class="getStatusClass('1002')" @click="selectLocation('1002', '3#正极卷提升机')">1002
            </div>
          </div>
          <div class="location-row">
            <div class="location-cell empty"></div>
            <div class="location-cell" :class="getStatusClass('1007')" @click="selectLocation('1007', '3#正极卷提升机')">1007
            </div>
            <div class="location-cell" :class="getStatusClass('1006')" @click="selectLocation('1006', '3#正极卷提升机')">1006
            </div>
            <div class="location-cell empty"></div>
          </div>
        </div>

        <!-- 4#负极卷提升机 -->
        <div v-if="currentArea === 'elevator4'" class="map-layout elevator4-layout">
          <div class="location-row">
            <div class="location-cell empty"></div>
            <div class="location-cell" :class="getStatusClass('1009')" @click="selectLocation('1009', '4#负极卷提升机')">1009
            </div>
            <div class="location-cell" :class="getStatusClass('1008')" @click="selectLocation('1008', '4#负极卷提升机')">1008
            </div>
            <div class="location-cell empty"></div>
          </div>
          <div class="location-row">
            <div class="location-cell" :class="getStatusClass('1001')" @click="selectLocation('1001', '4#负极卷提升机')">1001
            </div>
            <div class="location-cell empty"></div>
            <div class="location-cell empty"></div>
            <div class="location-cell empty"></div>
          </div>
          <div class="location-row">
            <div class="location-cell empty"></div>
            <div class="location-cell" :class="getStatusClass('1005')" @click="selectLocation('1005', '4#负极卷提升机')">1005
            </div>
            <div class="location-cell" :class="getStatusClass('1004')" @click="selectLocation('1004', '4#负极卷提升机')">1004
            </div>
            <div class="location-cell" :class="getStatusClass('1003')" @click="selectLocation('1003', '4#负极卷提升机')">1003
            </div>
            <div class="location-cell" :class="getStatusClass('1002')" @click="selectLocation('1002', '4#负极卷提升机')">1002
            </div>
          </div>
          <div class="location-row">
            <div class="location-cell empty"></div>
            <div class="location-cell" :class="getStatusClass('1007')" @click="selectLocation('1007', '4#负极卷提升机')">1007
            </div>
            <div class="location-cell" :class="getStatusClass('1006')" @click="selectLocation('1006', '4#负极卷提升机')">1006
            </div>
            <div class="location-cell empty"></div>
          </div>
        </div>

        <!-- 5#粉料装配提升机 -->
        <div v-if="currentArea === 'elevator5'" class="map-layout elevator5-layout">
          <div class="location-row">
            <div class="location-cell empty"></div>
            <div class="location-cell" :class="getStatusClass('1004')" @click="selectLocation('1004', '5#粉料装配提升机')">1004
            </div>
            <div class="location-cell" :class="getStatusClass('1006')" @click="selectLocation('1006', '5#粉料装配提升机')">1006
            </div>
            <div class="location-cell" :class="getStatusClass('1008')" @click="selectLocation('1008', '5#粉料装配提升机')">1008
            </div>
            <div class="location-cell" :class="getStatusClass('1010')" @click="selectLocation('1010', '5#粉料装配提升机')">1010
            </div>
          </div>
          <div class="location-row">
            <div class="location-cell" :class="getStatusClass('1001')" @click="selectLocation('1001', '5#粉料装配提升机')">1001
            </div>
            <div class="location-cell" :class="getStatusClass('1005')" @click="selectLocation('1005', '5#粉料装配提升机')">1005
            </div>
            <div class="location-cell" :class="getStatusClass('1007')" @click="selectLocation('1007', '5#粉料装配提升机')">1007
            </div>
            <div class="location-cell" :class="getStatusClass('1009')" @click="selectLocation('1009', '5#粉料装配提升机')">1009
            </div>
          </div>
          <div class="location-row">
            <div class="location-cell empty"></div>
            <div class="location-cell" :class="getStatusClass('1002')" @click="selectLocation('1002', '5#粉料装配提升机')">1002
            </div>
            <div class="location-cell" :class="getStatusClass('1003')" @click="selectLocation('1003', '5#粉料装配提升机')">1003
            </div>
            <div class="location-cell empty"></div>
          </div>
        </div>

        <!-- 四向车 -->
        <div v-if="currentArea === 'agv'" class="map-layout agv-layout">
          <div class="location-row">
            <div class="location-cell empty"></div>
            <div class="location-cell empty"></div>
            <div class="location-cell empty"></div>
            <div class="location-cell empty"></div>
            <div class="location-cell empty"></div>
            <div class="location-cell empty"></div>
          </div>
          <div class="location-row highlight-row">
            <div class="location-cell empty"></div>
            <div class="location-cell" :class="getStatusClass('1010')" @click="selectLocation('1010', '四向车')">1010</div>
            <div class="location-cell" :class="getStatusClass('1011')" @click="selectLocation('1011', '四向车')">1011</div>
            <div class="location-cell empty"></div>
          </div>
          <div class="location-row">
            <div class="location-cell empty"></div>
            <div class="location-cell" :class="getStatusClass('9-8')" @click="selectLocation('9-8', '四向车')">9-8</div>
            <div class="location-cell" :class="getStatusClass('9-9')" @click="selectLocation('9-9', '四向车')">9-9</div>
            <div class="location-cell" :class="getStatusClass('9-10')" @click="selectLocation('9-10', '四向车')">9-10</div>
            <div class="location-cell" :class="getStatusClass('9-11')" @click="selectLocation('9-11', '四向车')">9-11</div>
            <div class="location-cell" :class="getStatusClass('9-12')" @click="selectLocation('9-12', '四向车')">9-12</div>
            <div class="location-cell" :class="getStatusClass('9-13')" @click="selectLocation('9-13', '四向车')">9-13</div>
          </div>
          <div class="location-row">
            <div class="location-cell" :class="getStatusClass('8-8')" @click="selectLocation('8-8', '四向车')">8-8</div>
            <div class="location-cell empty"></div>
          </div>
          <div class="location-row">
            <div class="location-cell" :class="getStatusClass('7-8')" @click="selectLocation('7-8', '四向车')">7-8</div>
            <div class="location-cell empty"></div>
          </div>
          <div class="location-row">
            <div class="location-cell" :class="getStatusClass('6-8')" @click="selectLocation('6-8', '四向车')">6-8</div>
            <div class="location-cell empty"></div>
          </div>
          <div class="location-row">
            <div class="location-cell" :class="getStatusClass('1008')" @click="selectLocation('1008', '四向车')">1008</div>
            <div class="location-cell" :class="getStatusClass('5-2')" @click="selectLocation('5-2', '四向车')">5-2</div>
            <div class="location-cell" :class="getStatusClass('5-3')" @click="selectLocation('5-3', '四向车')">5-3</div>
            <div class="location-cell" :class="getStatusClass('5-4')" @click="selectLocation('5-4', '四向车')">5-4</div>
            <div class="location-cell" :class="getStatusClass('5-5')" @click="selectLocation('5-5', '四向车')">5-5</div>
            <div class="location-cell" :class="getStatusClass('5-6')" @click="selectLocation('5-6', '四向车')">5-6</div>
            <div class="location-cell" :class="getStatusClass('5-7')" @click="selectLocation('5-7', '四向车')">5-7</div>
            <div class="location-cell" :class="getStatusClass('5-8')" @click="selectLocation('5-8', '四向车')">5-8</div>
          </div>
          <div class="location-row">
            <div class="location-cell empty"></div>
            <div class="location-cell" :class="getStatusClass('4-4')" @click="selectLocation('4-4', '四向车')">4-4</div>
            <div class="location-cell empty"></div>
          </div>
          <div class="location-row">
            <div class="location-cell empty"></div>
            <div class="location-cell" :class="getStatusClass('3-4')" @click="selectLocation('3-4', '四向车')">3-4</div>
            <div class="location-cell empty"></div>
          </div>
          <div class="location-row">
            <div class="location-cell empty"></div>
            <div class="location-cell" :class="getStatusClass('2-4')" @click="selectLocation('2-4', '四向车')">2-4</div>
            <div class="location-cell empty"></div>
          </div>
          <div class="location-row">
            <div class="location-cell empty"></div>
            <div class="location-cell" :class="getStatusClass('1009')" @click="selectLocation('1009', '四向车')">1009</div>
            <div class="location-cell empty"></div>
          </div>
        </div>
      </div>
    </div>

    <!-- 库位详情弹窗 -->
    <el-dialog title="库位详情" v-model="dialogVisible" width="30%">
      <div v-if="selectedLocation">
        <p><strong>区域:</strong> {{ selectedLocation.area }}</p>
        <p><strong>库位编号:</strong> {{ selectedLocation.id }}</p>
        <p><strong>状态:</strong> {{ statusText[selectedLocation.status] }}</p>
        <p v-if="selectedLocation.goodsId"><strong>货物ID:</strong> {{ selectedLocation.goodsId }}</p>
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
import { ref, reactive } from 'vue';
import { ElMessage } from 'element-plus';

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

// 对话框显示状态
const dialogVisible = ref(false);

// 当前选中的库位
const selectedLocation = ref(null);

// 库位状态文本
const statusText = {
  0: '空闲',
  1: '占用',
  2: '故障',
  3: '预留'
};

// 库位数据
const locationData = reactive({
  'elevator1': {
    '1001': { status: 1, goodsId: 'D00153' },
    '1002': { status: 0, goodsId: '' },
    '1003': { status: 0, goodsId: '' },
    '1004': { status: 1, goodsId: 'D00075' },
    '1005': { status: 0, goodsId: '' },
  },
  'elevator2': {
    '1001': { status: 0, goodsId: '' },
    '1002': { status: 1, goodsId: 'D00127' },
    '1003': { status: 1, goodsId: 'D00069' },
    '1004': { status: 1, goodsId: 'D00005' },
    '1005': { status: 0, goodsId: '' },
    '1006': { status: 1, goodsId: 'D00006' },
    '1007': { status: 1, goodsId: 'D00007' },
  },
  'elevator3': {
    '1001': { status: 0, goodsId: '' },
    '1002': { status: 1, goodsId: 'D00127' },
    '1003': { status: 0, goodsId: '' },
    '1004': { status: 1, goodsId: 'D00075' },
    '1005': { status: 0, goodsId: '' },
    '1006': { status: 1, goodsId: 'D00069' },
    '1007': { status: 0, goodsId: '' },
    '1008': { status: 1, goodsId: 'D00336' },
    '1009': { status: 0, goodsId: '' },
  },
  'elevator4': {
    '1001': { status: 0, goodsId: '' },
    '1002': { status: 1, goodsId: 'D00127' },
    '1003': { status: 0, goodsId: '' },
    '1004': { status: 1, goodsId: 'D00075' },
    '1005': { status: 0, goodsId: '' },
    '1006': { status: 1, goodsId: 'D00069' },
    '1007': { status: 0, goodsId: '' },
    '1008': { status: 1, goodsId: 'D00336' },
    '1009': { status: 0, goodsId: '' },
  },
  'elevator5': {
    '1001': { status: 0, goodsId: '' },
    '1002': { status: 1, goodsId: 'D00127' },
    '1003': { status: 0, goodsId: '' },
    '1004': { status: 1, goodsId: 'D00075' },
    '1005': { status: 0, goodsId: '' },
    '1006': { status: 1, goodsId: 'D00069' },
    '1007': { status: 0, goodsId: '' },
    '1008': { status: 1, goodsId: 'D00336' },
    '1009': { status: 0, goodsId: '' },
    '1010': { status: 1, goodsId: 'D00337' },
  },
  'agv': {
    '1008': { status: 1, goodsId: 'D00336' },
    '1009': { status: 0, goodsId: '' },
    '1010': { status: 0, goodsId: '' },
    '1011': { status: 1, goodsId: 'D00337' },
    '5-2': { status: 0, goodsId: '' },
    '5-3': { status: 0, goodsId: '' },
    '5-4': { status: 0, goodsId: '' },
    '5-5': { status: 0, goodsId: '' },
    '5-6': { status: 0, goodsId: '' },
    '5-7': { status: 0, goodsId: '' },
    '5-8': { status: 0, goodsId: '' },
    '6-8': { status: 0, goodsId: '' },
    '7-8': { status: 0, goodsId: '' },
    '8-8': { status: 0, goodsId: '' },
    '9-8': { status: 0, goodsId: '' },
    '9-9': { status: 0, goodsId: '' },
    '9-10': { status: 0, goodsId: '' },
    '9-11': { status: 0, goodsId: '' },
    '9-12': { status: 0, goodsId: '' },
    '9-13': { status: 0, goodsId: '' },
    '2-4': { status: 0, goodsId: '' },
    '3-4': { status: 0, goodsId: '' },
    '4-4': { status: 0, goodsId: '' },
  }
});

// 获取当前区域标签
const getCurrentAreaLabel = () => {
  const area = areaList.find(item => item.value === currentArea.value);
  return area ? area.label : '';
};

// 获取库位状态样式类
const getStatusClass = (id: string) => {
  const area = currentArea.value;
  const locationInfo = locationData[area] && locationData[area][id];

  if (!locationInfo) return 'status-unknown';

  return `status-${locationInfo.status}`;
};

// 选择库位
const selectLocation = (id: string, area: string) => {
  const locationInfo = locationData[currentArea.value] && locationData[currentArea.value][id];

  if (!locationInfo) return;

  selectedLocation.value = {
    id,
    area,
    status: locationInfo.status,
    goodsId: locationInfo.goodsId
  };

  dialogVisible.value = true;
};

// 切换区域
const changeArea = (area: string) => {
  currentArea.value = area;
  ElMessage.success(`已切换到${areaList.find(item => item.value === area)?.label}`);
};

// 刷新数据
const refreshData = () => {
  ElMessage.success('数据已刷新');
  // 这里可以调用API重新获取库位数据
};

// 处理库位操作
const handleOperation = () => {
  if (!selectedLocation.value) return;

  ElMessage.success(`对库位 ${selectedLocation.value.id} 执行操作成功`);
  dialogVisible.value = false;
};
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

  .map-title {
    font-size: 16px;
    font-weight: bold;
    margin-bottom: 20px;
    padding-bottom: 10px;
    border-bottom: 1px solid #ebeef5;
  }

  .map-layout {
    display: flex;
    flex-direction: column;
    gap: 10px;
  }

  .location-row {
    display: flex;
    gap: 10px;

    &.highlight-row {
      position: relative;

      &::after {
        content: '';
        position: absolute;
        top: -5px;
        left: -5px;
        right: -5px;
        bottom: -5px;
        border: 2px solid #f56c6c;
        pointer-events: none;
      }
    }
  }

  .location-cell {
    width: 60px;
    height: 60px;
    display: flex;
    justify-content: center;
    align-items: center;
    border-radius: 4px;
    cursor: pointer;
    font-size: 14px;
    font-weight: bold;
    transition: all 0.3s;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);

    &:hover {
      transform: translateY(-2px);
      box-shadow: 0 4px 8px rgba(0, 0, 0, 0.15);
    }

    &.empty {
      background-color: transparent;
      box-shadow: none;
      cursor: default;

      &:hover {
        transform: none;
      }
    }

    &.status-0 {
      background-color: #67c23a;
      color: #fff;
    }

    &.status-1 {
      background-color: #e6a23c;
      color: #fff;
    }

    &.status-2 {
      background-color: #f56c6c;
      color: #fff;
    }

    &.status-3 {
      background-color: #909399;
      color: #fff;
    }

    &.status-unknown {
      background-color: #dcdfe6;
      color: #606266;
    }
  }
}

:deep(.el-dialog__body) {
  padding: 20px;
}
</style>