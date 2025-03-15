<template>
    <div class="s7-content">
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
            <el-table border style="width: 100%" stripe="true" fit="true" :data="S7List">
                <el-table-column align="center" type="selection" width="40px" />
                <el-table-column align="center" type="index" label="序号" width="60px" />
                <el-table-column align="center" prop="proxyName" label="配置名" width="180" />
                <el-table-column align="center" prop="iP" label="ip" width="150" />
                <el-table-column align="center" prop="port" label="端口" width="80" />
                <el-table-column align="center" prop="dbid" label="DB块id" width="80" />
                <el-table-column align="center" prop="address" label="地址偏移" width="180" />
                <el-table-column align="center" prop="type" label="数据类型" width="100" />
                <el-table-column align="center" prop="length" label="数据长度" width="180" />
                <el-table-column align="center" prop="bit" label="位地址" width="80" />
                <el-table-column align="center" prop="remark" label="备注" width="80" />
                <el-table-column align="center" prop="isOpen" label="是否启用" width="100" />
                <el-table-column align="center" prop="createtime" label="创建时间" />
                <el-table-column align="center" prop="lastModified" label="最后修改时间" />
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
</template>
<script setup lang="ts">


const S7List = [
    {
        "id": 1,
        "proxyName": "Test",
        "ip": "127.0.0.1",
        "port": 102,
        "dbid": 100,
        "address": "1",
        "type": "int",
        "length": 1,
        "bit": 0,
        "remark": "测试",
        "isOpen": true,
        "isDeleted": false,
        "createtime": "2025-03-15T20:32:05.2548312",
        "lastModified": "2025-03-15T20:32:05.3059042"
    }
]
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