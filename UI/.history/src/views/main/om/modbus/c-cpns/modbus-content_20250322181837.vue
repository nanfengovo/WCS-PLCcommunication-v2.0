<template>
    <div class="modbus-content" v-loading="loading">
        <div class="content-top">
            <div class="content-top-left">
                <el-button type="primary" icon="Refresh">刷新</el-button>
                <el-button type="primary" icon="Plus">添加</el-button>
                <el-button type="danger" icon="delete">删除</el-button>
            </div>
            <div class="content-top-right">
                <el-button type="success" icon="Upload">导入</el-button>
                <el-button type="warning" icon="Download">导出</el-button>
            </div>
        </div>
        <div class="content">
            <el-scrollbar max-height="400px">
                <el-table border style="width: 100%" :data="currentPageData" ref="multipleTableRef">
                    <el-table-column align="center" type="selection" width="40px" />
                    <el-table-column align="center" label="操作" width="180">
                        <template>
                            <el-button type="primary" size="small" text>编辑</el-button>
                            <el-button type="danger" size="small">删除</el-button>
                            <el-button type="success" size="small" text icon="View">读取</el-button>
                            <el-button type="success" size="small" text icon="EditPen">写入</el-button>
                        </template>
                    </el-table-column>
                    <el-table-column align="center" prop="result" label="读写详情" width="400px">
                        <template #default="scope">
                            <input type="text" v-model="scope.row.result"
                                style="width: 100%; height: 100%; border: none; outline: none; text-align: center; font-size: 14px; color: #606266; background-color: transparent;" />
                        </template>
                    </el-table-column>
                    <el-table-column align="center" type="index" label="序号" width="60px" />
                    <el-table-column align="center" prop="proxyName" label="配置名" width="180" />
                    <el-table-column align="center" prop="ip" label="ip" width="150" />
                    <el-table-column align="center" prop="port" label="端口" width="80" />
                    <el-table-column align="center" prop="slaveID" label="设备id" width="80" />
                    <el-table-column align="center" prop="functionCode" label="功能码" width="180" />
                    <el-table-column align="center" prop="startAddress" label="变量地址" width="180" />
                    <el-table-column align="center" prop="num" label="数量" width="80" />
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
            <!-- 分页 -->
            <!-- 分页控件 -->
            <!-- arr.slice((当前页数-1)*每页条数，当前页数 *当前条数)-->
            <el-pagination :current-page="page" :page-size="limit" :page-sizes="[10, 20, 30, 40, 50]" background
                layout="total, sizes, prev, pager, next, jumper" :total="total" @size-change="handleSizechange"
                @current-change="handleCurrentChange" class="pagination" />
        </div>
    </div>
</template>
<script setup lang="ts">
import { formatTime } from '@/utils/format'
import axios from 'axios';
import { onMounted, reactive, ref } from 'vue';

const loading = ref(false)
const error = ref<Error | null>(null);
//# region 获取数据
// 从后端获取数据
// 定义一个响应式的数组来存储后端返回的数据
interface ModbusItem {
    result: any;
    id: number;
    proxyName: string;
    ip: string;
    port: number;
    SlaveID: any;
    FunctionCode: any;
    isOpen: boolean;
    StartAddress: any;
    num: any;
    createtime: Date;
    lastModified: Date;
}

const ModbusList = reactive<ModbusItem[]>([]);
async function fetchData() {
    loading.value = true;
    error.value = null;

    try {
        const response = await axios.get('http://127.0.0.1:8888/api/ModbusTCP/GetAllConfig', {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`, // 替换为实际 Token
            },
        });
        console.log("@@" + response.data);
        const data: ModbusItem[] = response.data;
        ModbusList.length = 0;
        ModbusList.push(...data);
        total.value = response.data.length;
    } catch (err) {
        error.value = err instanceof Error ? err : new Error('请求失败');
        console.error('Error fetching data:', err);
    } finally {
        loading.value = false;
    }
}

// 在组件挂载后调用 fetchData 函数
onMounted(() => {
    fetchData();

    //getTotal();
})
//# endregion

//#region ---页码相关

//分页
const page = ref(1);
const limit = ref(10);
const total = ref(0);

import { computed } from 'vue';

const currentPageData = computed(() => {
    const start = (page.value - 1) * limit.value;
    const end = page.value * limit.value;
    return ModbusList.slice(start, end);
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
.modbus-content {
    background-color: #fff;
    padding: 20px;
    border-radius: 10px;
    margin-top: 20px;
    height: 500px;
}

.content {
    margin-top: 20px;
}

.content-top {
    display: flex;
    justify-content: space-between;
}
</style>