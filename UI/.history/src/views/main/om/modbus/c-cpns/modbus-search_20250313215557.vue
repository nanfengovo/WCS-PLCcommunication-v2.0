<template>
    <div>
        <el-card shadow="hover" class="modbus-search">
            <el-form label-width="80px">
                <el-row :gutter="20">
                    <el-clo :span="8">
                        <el-form-item label="配置名:">
                            <el-input type="text" placeholder="请输入要搜索的配置名" />
                        </el-form-item>
                    </el-clo>
                </el-row>
            </el-form>
        </el-card>
    </div>
</template>
<script setup lang="ts">
</script>

<style lang="less" scoped>
.modbus-search {
    background-color: #fff;
    padding: 20px;
    border-radius: 10px;
}
</style>