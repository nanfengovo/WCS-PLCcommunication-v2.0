<template>
    <div class="S7autoTask">
        <div class="S7autoTask-content" v-if="isMounted">
            <H2>S7数据点定时任务</H2>
            <div class="content-top">
                <div class="content-top-left">
                    <el-button type="primary" icon="Refresh" @click="refresh">刷新</el-button>
                    <el-button type="primary" icon="Plus">添加</el-button>
                    <el-button type="danger" icon="delete">删除</el-button>
                </div>
                <div class="content-top-right">
                    <el-button type="success" icon="Upload">导入</el-button>
                    <el-button type="warning" icon="Download">导出</el-button>
                </div>
            </div>
            <div class="content">
                <el-scrollbar max-height="400px" class="scrollbar-demo-item">
                    <el-table border style="width: 100%" :data="currentPageData" ref="multipleTableRef">
                        <el-table-column align="center" type="selection" width="40px" />
                        <el-table-column align="center" label="操作" width="220">
                            <template #default="scope">
                                <el-button type="primary" size="small" text icon="Edit">编辑</el-button>
                                <el-button :type="scope.row.isOpen ? 'danger' : 'primary'" size="small" :text="true"
                                    :icon="scope.row.isOpen ? 'CircleCloseFilled' : 'SuccessFilled'">
                                    {{ scope.row.isOpen ? '关闭定时任务' : '开启定时任务' }}
                                </el-button>
                            </template>
                        </el-table-column>
                        <el-table-column align="center" prop="result" label="任务详情" width="200px" show-overflow-tooltip>
                            <template #default="scope">
                                <input type="text" v-model="scope.row.result" readonly
                                    style="width: 100%; height: 100%; border: none; outline: none; text-align: center; font-size: 14px; color: #606266; background-color: transparent;" />
                            </template>
                        </el-table-column>
                        <el-table-column align="center" type="index" label="序号" width="60px" />
                        <el-table-column align="center" prop="name" label="定时任务名" width="280" />
                        <el-table-column align="center" prop="ip" label="ip" width="150" />
                        <el-table-column align="center" prop="port" label="端口" width="80" />
                        <el-table-column align="center" prop="dBaddr" label="变量地址" width="180" />
                        <el-table-column align="center" prop="result" label="结果" width="80" />
                        <el-table-column align="center" prop="isOpen" label="状态" width="100">
                            <!-- 作用域插槽 -->
                            <template #default="scope">
                                <el-button size="small" :type="scope.row.isOpen ? 'success' : 'danger'" plain>
                                    {{ scope.row.isOpen ? '启用' : '禁用' }}
                                </el-button>
                            </template>
                        </el-table-column>
                        <el-table-column align="center" prop="createtime" label="创建时间" width="250">
                            <!-- 作用域插槽进行时间格式化(使用Day.js) -->
                            <template #default="scope">
                                {{ formatTime(scope.row.createtime) }}
                            </template>
                        </el-table-column>
                        <el-table-column align="center" prop="lastModified" label="最后修改时间" width="250">
                            <template #default="scope">
                                {{ formatTime(scope.row.lastModified) }}
                            </template>
                        </el-table-column>
                    </el-table>
                </el-scrollbar>


            </div>
            <div>
                <!-- 分页 -->
                <!-- 分页控件 -->
                <!-- arr.slice((当前页数-1)*每页条数，当前页数 *当前条数)-->
                <el-pagination :current-page="page" :page-size="limit" :page-sizes="[10, 20, 30, 40, 50]" background
                    layout="total, sizes, prev, pager, next, jumper" :total="total" @size-change="handleSizechange"
                    @current-change="handleCurrentChange" />
            </div>



        </div>

        <div class="ModbusTask-content" v-else>
            <el-alert title="组件已卸载" type="info" center show-icon />
            <h1>111</h1>
        </div>

    </div>
</template>
<script setup lang="ts">
import axios, { type CancelTokenSource } from 'axios';
import { nextTick, onMounted, onUnmounted, ref } from 'vue';
import { formatTime } from '@/utils/format'
import { ElMessage, type ElTable } from 'element-plus';



const isMounted = ref(true);

const multipleTableRef = ref<InstanceType<typeof ElTable>>(); // 明确组件类型

//#region ---刷新

const refresh = () => {
    isMounted.value = false;
    setTimeout(() => {
        fetchData();

    }, 500);
    ElMessage.success('刷新成功');
    nextTick(() => {
        isMounted.value = true;
    });
}
//#endregion




//#region  ---显示数据 
// 首先定义清晰的接口类型
interface S7Item {
    id: number;
    name: string;
    ip: string;
    port: number;
    dBaddr: string;
    result: string | null;  // 注意 result 可能为 null
    isOpen: boolean;
    isDeleted: boolean;
    createtime: Date;
    lastModified: Date;
}


//定义一个响应式的数组来存储后端返回的数据
// 使用 ref + 泛型声明
const S7Task = ref<S7Item[]>([]);  // 正确数组类型声明

//const S7Task = reactive<S7Item[]>([]);
const error = ref<Error | null>(null);
// 从后端获取数据
// const S7Task =
//     [
//         {
//             "id": 31,
//             "name": "Test",
//             "ip": "127.0.0.1",
//             "port": 102,
//             "dBaddr": "DB100.DBD1",
//             "result": "3",
//             "isOpen": true,
//             "isDeleted": false,
//             "createtime": "2025-03-20T20:20:19.8747141",
//             "lastModified": "2025-03-20T20:20:19.9107915"
//         },
//         {
//             "id": 32,
//             "name": "测试编辑",
//             "ip": "127.0.0.1",
//             "port": 100,
//             "dBaddr": "DB100.DBD1",
//             "result": "0",
//             "isOpen": true,
//             "isDeleted": false,
//             "createtime": "2025-03-20T20:20:25.0206342",
//             "lastModified": "2025-03-20T20:20:25.0556311"
//         },
//         {
//             "id": 33,
//             "name": "test1",
//             "ip": "127.0.0.1",
//             "port": 102,
//             "dBaddr": "DB100.DBX100",
//             "result": null,
//             "isOpen": true,
//             "isDeleted": false,
//             "createtime": "2025-03-20T20:20:28.8797273",
//             "lastModified": "2025-03-20T20:20:28.9179382"
//         },
//         {
//             "id": 34,
//             "name": "粉料提升机-1001-任务号-写",
//             "ip": "127.0.0.1",
//             "port": 100,
//             "dBaddr": "DB100.DBD0",
//             "result": "0",
//             "isOpen": true,
//             "isDeleted": false,
//             "createtime": "2025-03-20T20:20:54.9337042",
//             "lastModified": "2025-03-20T20:20:54.9693883"
//         },
//         {
//             "id": 35,
//             "name": "粉料提升机-1001-起始位置-写",
//             "ip": "127.0.0.1",
//             "port": 100,
//             "dBaddr": "DB100.DBD4",
//             "result": "0",
//             "isOpen": true,
//             "isDeleted": false,
//             "createtime": "2025-03-20T20:20:58.1613424",
//             "lastModified": "2025-03-20T20:20:58.1978131"
//         },
//         {
//             "id": 36,
//             "name": "测试",
//             "ip": "127.0.0.1",
//             "port": 100,
//             "dBaddr": "DB100.DBD1",
//             "result": "0",
//             "isOpen": true,
//             "isDeleted": false,
//             "createtime": "2025-03-20T20:21:02.9937668",
//             "lastModified": "2025-03-20T20:21:03.0196241"
//         }
//     ]

let refreshTimer: number | null = null;
let currentRequestSource: CancelTokenSource | null = null;

async function fetchData() {
    error.value = null;

    try {
        // 取消之前的未完成请求
        if (currentRequestSource) {
            currentRequestSource.cancel('取消旧请求');
        }

        // 创建新的取消令牌
        currentRequestSource = axios.CancelToken.source();
        const response = await axios.get('http://127.0.0.1:8888/api/S7BackGroundService/GetAllTask', {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`, // 替换为实际 Token
            },
        });
        S7Task.value = response.data.data;
        total.value = response.data.length;
    } catch (err) {
        error.value = err instanceof Error ? err : new Error('请求失败');
        console.error('Error fetching data:', err);
    }
}

// 启动定时刷新
function startAutoRefresh() {
    // 先立即获取一次
    fetchData();

    // 设置定时器
    refreshTimer = window.setInterval(() => {
        fetchData();
    }, 500);
}

// 停止定时刷新
function stopAutoRefresh() {
    if (refreshTimer !== null) {
        clearInterval(refreshTimer);
        refreshTimer = null;
    }

    // 取消当前请求
    if (currentRequestSource) {
        currentRequestSource.cancel('手动停止刷新');
        currentRequestSource = null;
    }
}

onMounted(() => {
    startAutoRefresh();
});

onUnmounted(() => {
    stopAutoRefresh(); // 组件卸载时清理
});




//#region ---页码相关

//分页
const page = ref(1);
const limit = ref(10);
const total = ref(0);

import { computed } from 'vue';

const currentPageData = computed(() => {
    const start = (page.value - 1) * limit.value;
    const end = page.value * limit.value;
    return S7Task.value.slice(start, end);
});

//每页条数改变
const handleSizechange = (val: number) => {
    limit.value = val;
};
//当前页码改变
const handleCurrentChange = (val: number) => {
    page.value = val;
};
//#endregion

</script>

<style lang="less" scoped>
.S7autoTask-content {
    background-color: #fff;
    padding: 20px;
    border-radius: 10px;
    margin-top: 20px;
    height: 50%;

}

.ModbusTask-content {
    background-color: #fff;
    padding: 20px;
    border-radius: 10px;
    margin-top: 20px;
    height: 50%;
}


.content {
    margin-top: 20px;
}

.content-top {
    display: flex;
    justify-content: space-between;
}

.pagination {
    margin-top: 10px;
    display: flex;
    justify-content: flex-end;
}

.scrollbar-demo-item {
    display: flex;
    align-items: center;
    justify-content: center;
    height: 400px;
    margin: 10px;
    text-align: center;
    border-radius: 4px;
    background: var(--el-color-primary-light-9);
    color: var(--el-color-primary);
}
</style>