<template>
    <div class="s7-content" v-if="isMounted" v-loading="loading">
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
            <el-table border style="width: 100%" :data="S7List" ref="multipleTableRef">
                <el-table-column align="center" type="selection" width="40px" />
                <el-table-column align="center" label="操作" width="320">
                    <template #default="scope">
                        <el-button type="primary" size="small" text icon="Edit"
                            @click="EditS7(scope.row)">编辑</el-button>
                        <el-button @click="handleClick(scope.row)" :type="scope.row.isOpen ? 'danger' : 'primary'"
                            size="small" :text="true" :icon="scope.row.isOpen ? 'CircleCloseFilled' : 'SuccessFilled'">
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
                <el-table-column align="center" prop="proxyName" label="配置名" width="280" />
                <el-table-column align="center" prop="ip" label="ip" width="150" />
                <el-table-column align="center" prop="port" label="端口" width="80" />
                <el-table-column align="center" prop="dbid" label="DB块id" width="80" />
                <el-table-column align="center" prop="address" label="地址偏移" width="180" />
                <el-table-column align="center" prop="type" label="数据类型" width="100" />
                <el-table-column align="center" prop="length" label="数据长度" width="180" />
                <el-table-column align="center" prop="bit" label="位地址" width="80" />
                <el-table-column align="center" prop="remark" label="备注" width="80" />
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
        </div>
        <!-- 分页 -->
        <div class="pagination">
            <el-pagination v-model:current-page="currentPage" v-model:page-size="pageSize"
                :page-sizes="[10, 20, 30, 40, 50, 100]" background layout="total, sizes, prev, pager, next, jumper"
                :total=S7List.length @size-change="handleSizeChange" @current-change="handleCurrentChange" />
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


    <!-- 编辑 -->
    <el-dialog v-model="editDialogVisible" title={{title}} width="500" draggable overflow center>
        <el-form v-model="Editform" label-width="100px">
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
                <el-button type="primary" @click="saveS7Config">
                    保存
                </el-button>
            </div>
        </template>
    </el-dialog>
</template>
<script setup lang="ts">
import axios from 'axios';
import { nextTick, onMounted, reactive, ref } from 'vue';
import { formatTime } from '@/utils/format'
import { ElMessage, ElMessageBox, ElTable } from 'element-plus';

const isMounted = ref(true);
const loading = ref(false)
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



//#region  ---显示数据 
// 定义一个响应式的数组来存储后端返回的数据
interface S7Item {
    result: any;
    id: number;
    proxyName: string;
    ip: string;
    port: number;
    dbid: number;
    address: string;
    type: string;
    length: number;
    bit: number;
    remark: string;
    isOpen: boolean;
    isDeleted: boolean;
    createtime: Date;
    lastModified: Date;
}

const S7List = reactive<S7Item[]>([]);
const error = ref<Error | null>(null);
// 从后端获取数据
async function fetchData() {
    loading.value = true;
    error.value = null;
    try {
        const response = await axios.get('http://127.0.0.1:8888/api/S7/GetAllS7Configs', {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`, // 替换为实际 Token
            },
        });
        //console.log("@@" + response.data);
        const data: S7Item[] = response.data;
        S7List.length = 0;
        S7List.push(...data);
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


//测试数据
// const S7List = [
//     {
//         "id": 1,
//         "proxyName": "Test",
//         "ip": "127.0.0.1",
//         "port": 102,
//         "dbid": 100,
//         "address": "1",
//         "type": "int",
//         "length": 1,
//         "bit": 0,
//         "remark": "测试",
//         "isOpen": true,
//         "isDeleted": false,
//         "createtime": "2025-03-15T20:32:05.2548312",
//         "lastModified": "2025-03-15T20:32:05.3059042"
//     }
// ]
//#endregion




//#region ---页码相关
//当前页
const currentPage = ref(1);
//默认一页多少条
const pageSize = ref(10);
//每页多少条改变
function handleSizeChange(newSize: number) {
    pageSize.value = newSize;
    currentPage.value = 1; // 重置到第一页
}
//页码改变
function handleCurrentChange(newPage: number) {
    currentPage.value = newPage;
}
//总条数
//const total = ref(0);
//获取总条数
// function getTotal() {
//     total.value = S7List.length;
// }
//页面改变

//#endregion

const form = ref({
    proxyName: '',
    ip: '',
    port: 102,
    dbid: 100,
    address: '',
    type: '',
    length: 0,
    bit: 0,
    remark: '',
    isOpen: true,
})

const dialogOverflowVisible = ref(false)



//#region  添加新的配置
const addS7Config = () => {
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    const response = axios.post('http://127.0.0.1:8888/api/S7/AddS7Config', form.value, {
        headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`, // 替换为实际 Token

            'Content-Type': 'application/x-www-form-urlencoded'

        },
    }).then(response => {
        if (response.data.code === 200) {
            dialogOverflowVisible.value = false;
            form.value = {
                proxyName: '',
                ip: '',
                port: 102,
                dbid: 100,
                address: '',
                type: '',
                length: 0,
                bit: 0,
                remark: '',
                isOpen: true,
            }
            ElMessage({
                message: '添加成功',
                type: 'success',
                plain: true,
            })
            refresh();
        }
    })


}
//#endregion

//#region ---删除配置
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
        const response = await axios.delete('http://localhost:8888/api/S7/DeleteById', {
            data: ids,
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`,
            },
        });

        if (response.data?.code === 200) {
            ElMessage.success('删除成功');
            S7List.splice(0, S7List.length, ...S7List.filter(item => !ids.includes(item.id)));
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


//#region  --启用/禁用
const handleClick = async (row: any) => {
    if (row.isOpen) {
        await axios.put(`http://localhost:8888/api/S7/Disable?id=${row.id}`, null, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`,
            }
        }).then((response) => {
            if (response.data.code === 200) {
                ElMessage.success('禁用成功');
                row.isOpen = false;
            } else {
                ElMessage.error('禁用失败');
            }
            // eslint-disable-next-line @typescript-eslint/no-unused-vars
        }).catch((error) => {
            ElMessage.error('禁用失败');
        })
    }
    else {
        await axios.put(`http://localhost:8888/api/S7/Enable?id=${row.id}`, null, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`, // 替换为实际 Token
            },
        }).then((response) => {
            if (response.data.code === 200) {
                ElMessage.success('启用成功');
                row.isOpen = true;
            } else {
                ElMessage.error('启用失败');
            }
            // eslint-disable-next-line @typescript-eslint/no-unused-vars
        }).catch((error) => {
            ElMessage.error('启用失败');
        })
    }
};
//#endregion

//#region  --修改
const title = ref('');
const editDialogVisible = ref(false);

const EditS7 = (row: any) => {
    editDialogVisible.value = true;
    editForm.value = { ...row };
    title.value = editForm.value.name;
}

const editForm = ref({
    id: 0,
    name: '',
    type: '',
    length: 0,
    bit: 0,
    remark: '',
    isOpen: true,
});






//#endregion



//#region  读取

const ReadS7 = async (row: any) => {
    // const result = ref('');
    await axios.get(`http://localhost:8888/api/S7ReadWrite/Read?id=${row.id}`, {
        headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`,
        },
    }).then((response) => {
        if (response.data.code === 200) {
            const index = S7List.findIndex((item) => item.id === row.id);
            S7List[index].result = response.data.data;
            //console.log(result.value);
            ElMessage.success('读取成功');
        } else {
            ElMessage.error('读取失败');
        }
    })
}
//#endregion


//#region --写入
const WriteS7 = async (row: any) => {
    await axios.post(`http://localhost:8888/api/S7ReadWrite/Write?id=${row.id}&input=${row.result}`, null, {
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

</script>

<style lang="less" scoped>
.s7-content {
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