<template>
    <div class="s7-content" v-if="isMounted" v-loading="loading">
        <div class="content-top">
            <div class="content-top-left">
                <el-button type="primary" icon="Refresh" @click="refresh">刷新</el-button>
                <el-button type="primary" icon="Plus" plain @click="dialogOverflowVisible = true">添加</el-button>
                <el-button type="danger" icon="delete">删除</el-button>
            </div>
            <div class="content-top-right">
                <el-button type="success" icon="Upload">导入</el-button>
                <el-button type="warning" icon="Download">导出</el-button>
            </div>
        </div>
        <div class="content">
            <el-table border style="width: 100%" stripe="true" fit="true" :data="S7List">
                <el-table-column align="center" type="selection" width="40px" />
                <el-table-column align="center" type="index" label="序号" width="60px" />
                <el-table-column align="center" prop="proxyName" label="配置名" width="180" />
                <el-table-column align="center" prop="ip" label="ip" width="150" />
                <el-table-column align="center" prop="port" label="端口" width="80" />
                <el-table-column align="center" prop="dbid" label="DB块id" width="80" />
                <el-table-column align="center" prop="address" label="地址偏移" width="180" />
                <el-table-column align="center" prop="type" label="数据类型" width="100" />
                <el-table-column align="center" prop="length" label="数据长度" width="180" />
                <el-table-column align="center" prop="bit" label="位地址" width="80" />
                <el-table-column align="center" prop="remark" label="备注" width="80" />
                <el-table-column align="center" prop="isOpen" label="是否启用" width="100" />
                <el-table-column align="center" prop="createtime" label="创建时间" width="250" />
                <el-table-column align="center" prop="lastModified" label="最后修改时间" width="250" />
                <el-table-column align="center" label="操作" width="180">
                    <template>
                        <el-button type="primary" size="small" text>编辑</el-button>
                        <el-button type="primary" size="small" text>启用</el-button>
                        <el-button type="danger" size="small">删除</el-button>
                    </template>
                </el-table-column>
            </el-table>
        </div>
    </div>



    <!-- 添加 -->
    <el-dialog v-model="dialogOverflowVisible" title="添加S7数据点运维" width="500" draggable overflow>
        <el-form v-model="form" label-width="100px">
            <el-form-item label="配置名">
                <el-input v-model="form.proxyName" placeholder="请输入配置名" />
            </el-form-item>
            <el-form-item label="ip">
                <el-input v-model="form.ip" placeholder="请输入ip" />
            </el-form-item>
            <el-form-item label="端口">
                <el-input v-model="form.port" placeholder="请输入端口" />
            </el-form-item>
            <el-form-item label="DB块id">
                <el-input v-model="form.dbid" placeholder="请输入DB块id" />
            </el-form-item>
            <el-form-item label="地址偏移">
                <el-input v-model="form.address" placeholder="请输入地址偏移" />
            </el-form-item>
            <el-form-item label="数据类型">
                <el-input v-model="form.type" placeholder="请输入数据类型" />
            </el-form-item>
            <el-form-item label="数据长度">
                <el-input v-model="form.length" placeholder="请输入数据长度" />
            </el-form-item>
            <el-form-item label="位地址">
                <el-input v-model="form.bit" placeholder="请输入位地址" />
            </el-form-item>
            <el-form-item label="备注">
                <el-input v-model="form.remark" placeholder="请输入备注" />
            </el-form-item>
            <el-form-item label="是否启用">
                <el-switch v-model="form.isOpen" active-color="#13ce66" inactive-color="#ff4949" />
            </el-form-item>
        </el-form>
        <template #footer>
            <div class="dialog-footer">
                <el-button @click="dialogOverflowVisible = false">重置</el-button>
                <el-button type="primary" @click="dialogOverflowVisible = false">
                    添加
                </el-button>
            </div>
        </template>
    </el-dialog>
</template>
<script setup lang="ts">
import axios from 'axios';
import { nextTick, onMounted, reactive, ref } from 'vue';

const isMounted = ref(true);
const loading = ref(false)
//#region ---刷新

const refresh = () => {
    isMounted.value = false;
    setTimeout(() => {
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
onMounted(fetchData);

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
</style>