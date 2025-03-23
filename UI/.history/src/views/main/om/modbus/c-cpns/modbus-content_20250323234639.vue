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
                                @click="EditModbus(scope.row)">编辑</el-button>
                            <el-button @click="handleClick(scope.row)" :type="scope.row.isOpen ? 'danger' : 'primary'"
                                size="small" :text="true"
                                :icon="scope.row.isOpen ? 'CircleCloseFilled' : 'SuccessFilled'">
                                {{ scope.row.isOpen ? '禁用' : '启用' }}
                            </el-button>
                            <el-button type="success" size="small" text icon="View"
                                @click="ReadModbus(scope.row)">读取</el-button>
                            <el-button type="success" size="small" text icon="EditPen"
                                @click="WriteModbus(scope.row)">写入</el-button>
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
                <el-input v-model="form.ProxyName" placeholder="请输入配置名" />
            </el-form-item>
            <el-form-item label="ip:">
                <el-input v-model="form.IP" placeholder="请输入ip" />
            </el-form-item>
            <el-form-item label="端口:">
                <el-input v-model="form.Port" placeholder="请输入端口" />
            </el-form-item>
            <el-form-item label="设备id:">
                <el-input v-model="form.SlaveID" placeholder="请输入设备id" />
            </el-form-item>
            <el-form-item label="功能码:">
                <el-select v-model="form.FunctionCode" placeholder="请选择功能码" clearable>
                    <el-option label="读线圈寄存器" value=1 />
                    <el-option label="读离散输入寄存器" value=2 />
                    <el-option label="读保持寄存器" value=3 />
                    <el-option label="读输入寄存器" value=4 />
                    <el-option label="写单个线圈寄存器" value=5 />
                    <el-option label="写单个保持寄存器	" value=6 />
                    <el-option label="写多个线圈寄存器" value=15 />
                    <el-option label="写多个保持寄存器" value=16 />
                </el-select>
            </el-form-item>
            <el-form-item label="是否启用:">
                <el-switch v-model="form.IsOpen" active-color="#13ce66" inactive-color="#ff4949" />
            </el-form-item>
            <el-form-item label="变量地址:">
                <el-input v-model="form.StartAddress" placeholder="请输入变量地址" />
            </el-form-item>
            <el-form-item label="个数:">
                <el-input v-model="form.Num" placeholder="请输入个数" />
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




    <!-- 编辑 -->
    <el-dialog v-model="editDialogVisible" :title=title width="500" draggable overflow center>
        <el-form v-model="EditForm" label-width="100px">
            <el-form-item label="配置名:">
                <el-input v-model="EditForm.proxyName" placeholder="请输入配置名" />
            </el-form-item>
            <el-form-item label="ip:">
                <el-input v-model="EditForm.ip" placeholder="请输入ip" />
            </el-form-item>
            <el-form-item label="端口:">
                <el-input v-model="EditForm.port" placeholder="请输入端口" />
            </el-form-item>
            <el-form-item label="设备id:">
                <el-input v-model="EditForm.slaveID" placeholder="请输入设备id" />
            </el-form-item>
            <el-form-item label="功能码:">
                <el-select v-model="EditForm.functionCode" placeholder="请选择功能码" clearable>
                    <el-option label="读线圈寄存器" value=1 />
                    <el-option label="读离散输入寄存器" value=2 />
                    <el-option label="读保持寄存器" value=3 />
                    <el-option label="读输入寄存器" value=4 />
                    <el-option label="写单个线圈寄存器" value=5 />
                    <el-option label="写单个保持寄存器	" value=6 />
                    <el-option label="写多个线圈寄存器" value=15 />
                    <el-option label="写多个保持寄存器" value=16 />
                </el-select>
            </el-form-item>
            <el-form-item label="是否启用:">
                <el-switch v-model="EditForm.isOpen" active-color="#13ce66" inactive-color="#ff4949" />
            </el-form-item>
            <el-form-item label="变量地址:">
                <el-input v-model="EditForm.startAddress" placeholder="请输入变量地址" />
            </el-form-item>
            <el-form-item label="个数:">
                <el-input v-model="EditForm.num" placeholder="请输入个数" />
            </el-form-item>
        </el-form>
        <template #footer>
            <div class="dialog-footer">
                <el-button @click="editDialogVisible = false">重置</el-button>
                <el-button type="primary" @click="saveModbusConfig">
                    保存
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
const addS7Config = () => {
    axios.post('http://127.0.0.1:8888/api/ModbusTCP/CreateModbusTCPConfig', form.value, {
        // Add your post data here
        headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`, // 替换为实际 Token
            'Content-Type': 'application/x-www-form-urlencoded'
        },
    }).then(response => {
        if (response.data.code === 200) {
            ElMessage.success('添加成功');
            fetchData();
            dialogOverflowVisible.value = false;
        } else {
            ElMessage.error(response.data.message || '添加失败');
        }
    }).catch(error => {
        ElMessage.error(error.message || '添加请求失败');
    })
};

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
    ProxyName: '',
    IP: '',
    Port: 502,
    SlaveID: 1,
    FunctionCode: '',
    IsOpen: true,
    StartAddress: 0,
    Num: 1,
})
//#endregion


//#region ---编辑
const editDialogVisible = ref(false);
interface EditItem {
    proxyName: string;
    ip: string;
    port: number;
    slaveID: any;
    functionCode: any;
    isOpen: boolean;
    startAddress: any;
    num: any;
}
const EditForm = ref<EditItem>({
    proxyName: '',
    ip: '',
    port: 502,
    slaveID: 1,
    functionCode: '',
    isOpen: true,
    startAddress: 0,
    num: 1,
});
const title = ref('');
const id = ref(0);
const EditModbus = (row: any) => {
    editDialogVisible.value = true;
    EditForm.value = { ...row };
    title.value = EditForm.value.proxyName;
    id.value = row.id;
}


const saveModbusConfig = async () => {
    // 创建 FormData 对象
    const formData = new FormData();

    // 按 cURL 示例的字段名格式添加数据（注意大小写）
    formData.append('ProxyName', EditForm.value.proxyName); // 字段名必须完全匹配
    formData.append('IP', EditForm.value.ip);
    formData.append('Port', EditForm.value.port.toString()); // 确保转换为字符串
    formData.append('SlaveID', EditForm.value.slaveID.toString());
    formData.append('FunctionCode', EditForm.value.functionCode);
    formData.append('StarAddress', EditForm.value.startAddress.toString());
    formData.append('Num', EditForm.value.num.toString());
    formData.append('IsOpen', EditForm.value.isOpen ? 'true' : 'false');
    await axios.put(`http://localhost:8888/api/ModbusTCP/EditModbusTCPConfigById?id=${id.value}`, formData, {
        headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`,
        },
    }).then((response) => {
        if (response.data.code === 200) {
            ElMessage.success('修改成功');
            editDialogVisible.value = false;
            refresh();
        } else {
            ElMessage.error('修改失败');
        }
    })
}

//#endregion

//#region ---修改启用/禁用的状态
const handleClick = (row: any) => {
    axios.put(`http://localhost:8888/api/ModbusTCP/UpdateModbusTCPConfigIsOpenById?id=${row.id
        }&isOpen=${!row.isOpen}`, null, {
        headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`,
        },
    }).then((response) => {
        if (response.data.code === 200) {
            ElMessage.success('修改成功');
            refresh();
        } else {
            ElMessage.error('修改失败');
        }
    })
}
//#endregion


//#region ---读取
const ReadModbus = async (row: any) => {
    if (row.functionCode === 1) {
        //读取线圈
        await axios.get(`http://localhost:8888/api/ModbusTCP/ReadCoils?id=${row.id}`, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`,
            },
        }).then((response) => {
            if (response.data.code === 200) {
                ElMessage.success('读取成功');
                row.result = response.data.data;
            } else {
                ElMessage.error('读取失败');
            }
        })
    }
    else if (row.functionCode === 2) {
        //读取离散输入
        await axios.get(`http://localhost:8888/api/ModbusTCP/ReadDiscreteInputs?id=${row.id}`, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`,
            },
        }).then((response) => {
            if (response.data.code === 200) {
                ElMessage.success('读取成功');
                row.result = response.data.data;
            } else {
                ElMessage.error('读取失败');
            }
        })
    }
}
//#endregion

//#region ---写入
const WriteModbus = async (row: any) => {
    if (row.functionCode === 1) {
        //写入单个线圈
        await axios.post(`http://localhost:8888/api/ModbusTCP/WriteSingleCoil?id=${row.id}&value=${row.result}`, null, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`,
            },
        }).then((response) => {
            if (response.data.code === 200) {
                ElMessage.success('写入成功');
            } else {
                ElMessage.error('写入失败');
            }
        })
    }
}
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