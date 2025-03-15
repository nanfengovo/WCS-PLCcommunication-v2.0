<template>
    <div>
        <el-card shadow="hover" class="modbus-search">
            <el-form label-width="180px" class="searchForm" v-model="modbusSearchForm">
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
                        <el-form-item label="Activity zone">
                            <el-select v-model="modbusSearchForm.search" placeholder="Activity zone" clearable>
                                <el-option label="Zone one" value="shanghai" />
                                <el-option label="Zone two" value="beijing" />
                            </el-select>
                        </el-form-item>
                    </el-clo>
                </el-row>
            </el-form>
        </el-card>
    </div>
</template>
<script setup lang="ts">
import { reactive } from 'vue';


const modbusSearchForm = reactive({
    Search: ''
})


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
</style>