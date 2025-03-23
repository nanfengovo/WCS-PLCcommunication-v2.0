<template>
    <div class="modbus-content" v-loading="loading">
        <div class="content-top">
            <div class="content-top-left">
                <el-button type="primary" icon="Refresh" @click="refresh">刷新</el-button>
                <el-button type="primary" icon="Plus" @click="dialogOverflowVisible = true">添加</el-button>
                <el-button type="danger" icon="delete" @click="deleteSelectedRows">删除</el-button>
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
    <el-dialog v-model="dialogOverflowVisible" title="添加Modbus数据点" width="500" draggable overflow center>
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
            <el-form-item label="设备id:">
                <el-input v-model="form.slaveid" placeholder="请输入设备id" />
            </el-form-item>
            <el-form-item label="功能码:">
                <el-select v-model="form.functionCode" placeholder="请选择功能码" clearable>
                    <el-option label="读线圈寄存器" value="0x01" />
                    <el-option label="读离散输入寄存器" value="0x02" />
                    <el-option label="读保持寄存器" value="0x03" />
                    <el-option label="读输入寄存器" value="0x04" />
                    <el-option label="写单个线圈寄存器" value="0x05" />
                    <el-option label="写单个保持寄存器	" value="0x06" />
                    <el-option label="写多个线圈寄存器" value="0x0F" />
                    <el-option label="写多个保持寄存器" value="0x10" />
                </el-select>
            </el-form-item>
            <el-form-item label="是否启用:">
                <el-switch v-model="form.isOpen" active-color="#13ce66" inactive-color="#ff4949" />
            </el-form-item>
            <el-form-item label="变量地址:">
                <el-input v-model="form.staraddress" placeholder="请输入变量地址" />
            </el-form-item>
            <el-form-item label="个数:">
                <el-input v-model="form.num" placeholder="请输入个数" />
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
import { ElMessage, ElMessageBox, ElTable } from 'element-plus';
import { nextTick, onMounted, reactive, ref } from 'vue';
const dialogOverflowVisible = ref(false)
const loading = ref(false)
const error = ref<Error | null>(null);
const isMounted = ref(true);
//#region ---刷新

const refresh = () => {
    isMounted.value = false;
    setTimeout(() => {
        fetchData();
        loading.value = false; // 2秒后显示内容
    }, 500);
    loading.value = true;
    nextTick(() => {
        isMounted.value = true;
    });
}
//#endregion

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


//#region ---添加

//#endregion


//#region 删除
const multipleTableRef = ref<InstanceType<typeof ElTable>>(); // 明确组件类型
const isDeleting = ref(false);

const deleteSelectedRows = async () => {
    try {
        await ElMessageBox.confirm('确定删除选中项吗？', '警告', { type: 'warning' });
        // eslint-disable-next-line @typescript-eslint/no-unused-vars
    } catch (error) {
        return; // 用户取消
    }

    if (!multipleTableRef.value) {
        ElMessage.warning('表格未加载');
        return;
    }

    const selectedRows = multipleTableRef.value.getSelectionRows();
    if (selectedRows.length === 0) {
        ElMessage.warning('请至少选择一行数据');
        return;
    }

    const ids = selectedRows.map((row: { id: any; }) => row.id);
    try {
        isDeleting.value = true;
        const response = await axios.delete('http://localhost:8888/api/ModbusTCP/DeletedById', {
            data: ids,
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`,
            },
        });

        if (response.data?.code === 200) {
            ElMessage.success('删除成功');
            ModbusList.splice(0, ModbusList.length, ...ModbusList.filter(item => !ids.includes(item.id)));
        } else {
            throw new Error(response.data?.message || '删除失败');
        }
    } catch (error: any) {
        ElMessage.error(error.message || '删除请求失败');
    } finally {
        isDeleting.value = false;
    }
};
//#endregion
const form = ref({
    proxyName: '',
    ip: '',
    port: 502,
    slaveid: 1,
    functionCode: '',
    isOpen: true,
    staraddress: 0,
    num: 1,
})
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

.pagination {
    margin-top: 10px;
    display: flex;
    justify-content: flex-end;
}
</style>