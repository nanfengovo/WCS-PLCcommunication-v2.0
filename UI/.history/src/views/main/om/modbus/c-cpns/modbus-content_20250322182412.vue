<template>
    <div class="modbus-content" v-loading="loading">
        <div class="content-top">
            <div class="content-top-left">
                <el-button type="primary" icon="Refresh">刷新</el-button>
                <el-button type="primary" icon="Plus" @click="dialogOverflowVisible = true">添加</el-button>
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
                    <el-table-column align="center" label="操作" width="320">
                        <template #default="scope">
                            <el-button type="primary" size="small" text icon="Edit"
                                @click="EditS7(scope.row)">编辑</el-button>
                            <el-button @click="handleClick(scope.row)" :type="scope.row.isOpen ? 'danger' : 'primary'"
                                size="small" :text="true"
                                :icon="scope.row.isOpen ? 'CircleCloseFilled' : 'SuccessFilled'">
                                {{ scope.row.isOpen ? '禁用' : '启用' }}
                            </el-button>
                            <el-button type="success" size="small" text icon="View"
                                @click="ReadS7(scope.row)">读取</el-button>
                            <el-button type="success" size="small" text icon="EditPen"
                                @click="WriteS7(scope.row)">写入</el-button>
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

    <!-- 添加 -->
    <el-dialog v-model="dialogOverflowVisible" title="添加S7数据点运维" width="500" draggable overflow center>
        <el-form v-model="form" label-width="100px">
            <el-form-item label="配置名:">
                <el-input v-model="form.proxyName" placeholder="请输入配置名" />
            </el-form-item>
            <el-form-item label="ip:">
                <el-input v-model="form.ip" placeholder="请输入ip" />
            </el-form-item>
            <el-form-item label="端口:">
                <el-input v-model="form.port" placeholder="请输入端口" />
            </el-form-item>
            <el-form-item label="DB块id:">
                <el-input v-model="form.dbid" placeholder="请输入DB块id" />
            </el-form-item>
            <el-form-item label="地址偏移:">
                <el-input v-model="form.address" placeholder="请输入地址偏移" />
            </el-form-item>
            <el-form-item label="数据类型:">
                <el-select v-model="form.type" placeholder="请选择数据类型">
                    <el-option label="int" value="int" />
                    <el-option label="short" value="short" />
                    <el-option label="bool" value="bool" />
                </el-select>
            </el-form-item>
            <el-form-item label="数据长度:">
                <el-input v-model="form.length" placeholder="请输入数据长度" />
            </el-form-item>
            <el-form-item label="位地址:">
                <el-input v-model="form.bit" placeholder="请输入位地址" />
            </el-form-item>
            <el-form-item label="备注:">
                <el-input v-model="form.remark" placeholder="请输入备注" />
            </el-form-item>
            <el-form-item label="是否启用:">
                <el-switch v-model="form.isOpen" active-color="#13ce66" inactive-color="#ff4949" />
            </el-form-item>
        </el-form>
        <template #footer>
            <div class="dialog-footer">
                <el-button @click="dialogOverflowVisible = false">重置</el-button>
                <el-button type="primary" @click="addS7Config">
                    添加
                </el-button>
            </div>
        </template>
    </el-dialog>

</template>
<script setup lang="ts">
import { formatTime } from '@/utils/format'
import axios from 'axios';
import { onMounted, reactive, ref } from 'vue';
const dialogOverflowVisible = ref(false)
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
        //console.log("@@" + response.data);
        const data: ModbusItem[] = response.data.data;
        ModbusList.length = 0;
        ModbusList.push(...data);
        total.value = response.data.data.length;
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