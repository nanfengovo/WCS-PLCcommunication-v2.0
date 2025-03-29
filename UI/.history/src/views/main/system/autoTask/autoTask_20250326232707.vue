<template>
    <S7AutoTask />

</template>
<script setup lang="ts">
import S7AutoTask from '@/views/main/system/autoTask/c-cpns/S7Auto.vue';
import axios, { type CancelTokenSource } from 'axios';
import { nextTick, onMounted, onUnmounted, ref } from 'vue';
import { formatTime } from '@/utils/format'
import { ElMessage, ElUpload, type ElTable, type UploadFile, type UploadFiles, type UploadRawFile } from 'element-plus';



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
        total.value = response.data.data.length;
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

//#region  导入
const uploadUrl = 'http://localhost:8888/api/S7BackGroundService/Import';
const uploadRef = ref<InstanceType<typeof ElUpload>>();
const uploadLoading = ref(false);

// 请求头配置
const headers = {
    Authorization: `Bearer ${localStorage.getItem('token')}`,
    'X-Requested-With': 'XMLHttpRequest'  // 防止某些框架的CSRF过滤
}

// 手动提交上传（增加校验逻辑）
const submitUpload = () => {


    // 检查文件是否通过校验
    // @ts-expect-error  先忽略
    const validateStatus = uploadRef.value?.uploadFiles
        .every((file: { status: string; }) => file.status === 'ready')

    if (!validateStatus) {
        ElMessage.warning('存在不符合要求的文件')
        return
    }

    uploadLoading.value = true
    // @ts-expect-error  先忽略
    uploadRef.value.submit()
}

// 强化类型校验
const beforeUpload = (rawFile: UploadRawFile) => {
    const validTypes = [
        'application/vnd.ms-excel',
        'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
    ]

    // 双重校验：扩展名 + 实际MIME类型
    const isExcel = /\.(xls|xlsx)$/i.test(rawFile.name) &&
        validTypes.includes(rawFile.type)

    const isLt10M = rawFile.size / 1024 / 1024 < 10

    if (!isExcel) {
        ElMessage.error('仅支持.xls/.xlsx格式的Excel文件!')
        return false
    }

    if (!isLt10M) {
        ElMessage.error('文件大小不能超过10MB!')
        return false
    }

    return true
}

// 增强成功处理
const handleSuccess = (
    response: { code: number; msg?: string; data?: any },
    uploadFile: UploadFile,
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    uploadFiles: UploadFiles
) => {
    uploadLoading.value = false

    if (response.code === 200) {
        ElMessage.success(response.msg || '文件上传成功')
        // 清空文件列表
        uploadRef.value?.clearFiles()
        // 这里可以触发页面数据刷新
        // emit('upload-success')
        refresh();
    } else if (response.code === 201) {
        ElMessage.success(response.msg || '文件部分上传成功')
        // 清空文件列表
        uploadRef.value?.clearFiles()
        refresh();
    } else {
        ElMessage.error(response.msg || '文件处理失败')
        uploadRef.value?.abort(uploadFile)
        // 清空文件列表
        uploadRef.value?.clearFiles()
    }
}

// 增强错误处理
const handleError = (error: Error) => {
    uploadLoading.value = false
    console.error('上传错误:', error)

    const msg = error.message.includes('Network Error')
        ? '网络连接失败，请检查网络设置'
        : error.message

    ElMessage.error(`上传失败: ${msg}`)
}

// 文件超限处理
// eslint-disable-next-line @typescript-eslint/no-unused-vars
const handleExceed = (files: FileList) => {
    ElMessage.warning(`每次只能上传1个文件，已自动替换为最新文件`)
}

//#endregion

//#region 导出
const exportLoading = ref(false);

const handleExport = async () => {
    try {
        exportLoading.value = true;
        const response = await axios.get('http://127.0.0.1:8888/api/S7BackGroundService/Export', {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`,
                'Content-Type': 'application/x-www-form-urlencoded'
            },
            responseType: 'blob'
        });

        // 2. 创建 Blob 对象并下载
        const blob = new Blob([response.data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
        const downloadUrl = window.URL.createObjectURL(blob);
        const link = document.createElement('a');
        link.href = downloadUrl;
        link.setAttribute('download', 'S7后台任务配置.xlsx');  // 设置文件名
        document.body.appendChild(link);
        link.click();

        // 3. 清理资源
        window.URL.revokeObjectURL(downloadUrl);
        document.body.removeChild(link);

    } catch (error) {
        ElMessage.error('导出失败: ' + error!);
    } finally {
        exportLoading.value = false;
    }
}

//#endregion

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

//#region 删除
const deleteS7Task = async (row: S7Item) => {
    try {
        const response = await axios.delete(
            `http://127.0.0.1:8888/api/S7BackGroundService/DelById?id=${row.id}`,
            {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`,
                },
            }
        );
        if (response.data.code === 200) {
            ElMessage.success('删除成功');
            fetchData();
        } else {
            ElMessage.error(response.data.message || '删除失败');
        }
    } catch (error) {
        console.error('删除请求失败:', error);
        ElMessage.error('删除失败，请检查网络或权限');
    }
};
//#endregion

//#region 添加
const S7TaskDialogVisible = ref(false); // 添加对话框是否显示
const S7Taskform = ref({
    name: '',
    ip: '',
    port: 102,
    dBaddr: '',
    isOpen: true,
});
const addS7Task = async () => {
    S7TaskDialogVisible.value = true;
    try {
        const response = await axios.post('http://127.0.0.1:8888/api/S7BackGroundService/AddS7Task', S7Taskform.value, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`,
                'Content-Type': 'application/x-www-form-urlencoded'
            },
        });
        if (response.data.code === 200) {
            ElMessage.success('添加成功');
            S7Taskform.value = {
                name: '',
                ip: '',
                port: 102,
                dBaddr: '',
                isOpen: true,
            };
            S7TaskDialogVisible.value = false;
            fetchData();
        } else {
            ElMessage.error('添加失败');
        }
    } catch (error) {
        console.error('添加请求失败:', error);
        ElMessage.error('添加失败，请检查网络或权限');
    }


};

//#endregion

//#region  修改任务的状态
const changeTaskStatus = async (row: S7Item) => {
    try {
        const response = await axios.post(
            `http://127.0.0.1:8888/api/S7BackGroundService/ChangeS7TaskStatus?id=${row.id}`, null,
            {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`,
                },
            }
        );
        if (response.data.code === 200) {
            ElMessage.success('修改成功');
            fetchData();
        } else {
            ElMessage.error('修改失败');
        }
    } catch (error) {
        console.error('修改请求失败:', error);
        ElMessage.error('修改失败，请检查网络或权限');
    }
};
//#endregion

//#region  编辑
const editS7TaskDialogVisible = ref(false); // 编辑对话框是否显示
const title = ref(""); // 编辑对话框标题
const id = ref(0);
const EditS7TaskForm = ref({
    name: '',
    ip: '',
    port: 102,
    dBaddr: '',
    result: null,
    isOpen: true,
    isDeleted: false,
});
const editS7Task = (row: any) => {
    editS7TaskDialogVisible.value = true;
    EditS7TaskForm.value = { ...row };
    title.value = row.name;
    id.value = row.id;
    console.log(id.value);
};
const saveS7Task = async () => {
    console.log(id.value);
    try {
        // 创建 FormData 对象
        const formData = new FormData();

        // 按 cURL 示例的字段名格式添加数据（注意大小写）
        formData.append('Name', EditS7TaskForm.value.name); // 字段名必须完全匹配
        formData.append('IP', EditS7TaskForm.value.ip);
        formData.append('Port', EditS7TaskForm.value.port.toString()); // 确保转换为字符串
        formData.append('DBaddr', EditS7TaskForm.value.dBaddr);
        formData.append('IsOpen', EditS7TaskForm.value.isOpen.toString()); // 布尔值转字符串
        const response = await axios.put(`http://127.0.0.1:8888/api/S7BackGroundService/EditS7Task?id=${id.value}`, formData, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`,

            },
        });
        if (response.data.code === 200) {
            ElMessage.success('修改成功');
            fetchData();
            editS7TaskDialogVisible.value = false;
        }
    } catch (error) {
        console.error('修改请求失败:', error);
        ElMessage.error('修改失败，请检查网络或权限');
    }
};

//#endregion

</script>

<style lang="less" scoped>
.S7autoTask-content {
    background-color: #fff;
    padding: 20px;
    border-radius: 10px;
    margin-top: 20px;

}

.ModbusTCPautoTask-content {
    background-color: #fff;
    padding: 20px;
    border-radius: 10px;
    margin-top: 20px;

}

.ModbusTask-content {
    background-color: #fff;
    padding: 20px;
    border-radius: 10px;
    margin-top: 20px;
}


.content {
    margin-top: 20px;
}

.content-top {
    display: flex;
    justify-content: space-between;
}

.content-top-right {
    display: flex;
}

.upload-demo {
    margin-right: 10px;
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