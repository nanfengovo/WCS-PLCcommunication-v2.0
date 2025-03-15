<template>
    <div class="modbus-content">
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
            <el-table border style="width: 100%">
                <el-table-column align="center" type="selection" width="40px" />
                <el-table-column align="center" type="index" label="序号" width="60px" />
                <el-table-column align="center" prop="ProxyName" label="配置名" width="180" />
                <el-table-column align="center" prop="IP" label="ip" width="150" />
                <el-table-column align="center" prop="Port" label="端口" width="80" />
                <el-table-column align="center" prop="SlaveID" label="设备id" width="80" />
                <el-table-column align="center" prop="FunctionCode" label="功能码" width="180" />
                <el-table-column align="center" prop="IsOpen" label="是否启用" width="100" />
                <el-table-column align="center" prop="StartAddress" label="变量地址" width="180" />
                <el-table-column align="center" prop="Num" label="数量" width="80" />
                <el-table-column align="center" label="操作" width="180">
                    <template>
                        <el-button type="primary" size="small" text>编辑</el-button>
                        <el-button type="danger" size="small">删除</el-button>
                    </template>
                </el-table-column>
            </el-table>
        </div>
    </div>
</template>
<script setup lang="ts">
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
}
</style>