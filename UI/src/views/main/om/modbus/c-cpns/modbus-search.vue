<template>
    <div>
        <el-card shadow="hover" class="modbus-search">
            <el-form label-width="180px" class="searchForm" ref="formRef" v-model="modbusSearchForm">
                <el-row :gutter="20">
                    <el-clo :span="8">
                        <el-form-item label="配置名:">
                            <el-input type="text" placeholder="请输入要搜索的配置名" />
                        </el-form-item>
                    </el-clo>
                    <el-clo :span="8">
                        <el-form-item label="IP:">
                            <el-input type="text" placeholder="请输入要搜索的IP" />
                        </el-form-item>
                    </el-clo>
                    <el-clo :span="8">
                        <el-form-item label="端口:">
                            <el-input type="text" placeholder="请输入要搜索的端口" />
                        </el-form-item>
                    </el-clo>
                    <el-clo :span="8">
                        <el-form-item label="设备Id:">
                            <el-input type="text" placeholder="请输入要搜索的设备Id" />
                        </el-form-item>
                    </el-clo>
                    <el-clo :span="8">
                        <el-form-item label="功能码:">
                            <el-select v-model="modbusSearchForm.Search" placeholder="请选择功能码" clearable>
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
                    </el-clo><el-clo :span="8">
                        <el-form-item label="是否启用:">
                            <el-select v-model="modbusSearchForm.IsOpen" placeholder="请选择状态" clearable>
                                <el-option label="启用" value="1" />
                                <el-option label="禁用" value="0" />
                            </el-select>
                        </el-form-item>
                    </el-clo>
                    <el-col :span="24">
                        <div class="btn">
                            <el-button type="primary" icon="search">
                                查询
                            </el-button>
                            <el-button icon="Refresh" @click="headleResetClick">
                                重置
                            </el-button>
                        </div>
                    </el-col>
                </el-row>
            </el-form>
        </el-card>
    </div>
</template>
<script setup lang="ts">
import type { ElForm } from 'element-plus';
import { reactive, ref } from 'vue';
const formRef = ref<InstanceType<typeof ElForm>>()

const modbusSearchForm = reactive({
    Search: '',
    IsOpen: ''
})


//重置
function headleResetClick() {
    formRef.value?.resetFields();
}
</script>

<style lang="less" scoped>
.modbus-search {
    background-color: #fff;
    padding: 20px;
    border-radius: 10px;
}

.searchForm .el-select {
    --el-select-width: 180px;
}

.btn {
    text-align: right;
    margin-right: 50px;
}
</style>