<template>
    <div>
        <el-card shadow="hover" class="s7-search">
            <el-form label-width="180px" class="searchForm" ref="formRef" v-model="s7SearchForm">
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
                        <el-form-item label="DB Id:">
                            <el-input type="text" placeholder="请输入要搜索的DB Id" />
                        </el-form-item>
                    </el-clo>
                    <el-clo :span="8">
                        <el-form-item label="提升机类别:">
                            <el-select v-model="s7SearchForm.Search" placeholder="请选择类别" clearable>
                                <el-option label="辅料提升机" value="辅料提升机" />
                                <el-option label="粉料提升机" value="粉料提升机" />
                                <el-option label="正极极卷提升机" value="正极极卷提升机" />
                                <el-option label="负极极卷提升机" value="负极极卷提升机" />
                                <el-option label="装配提升机" value="装配提升机" />
                            </el-select>
                        </el-form-item>
                    </el-clo><el-clo :span="8">
                        <el-form-item label="是否启用:">
                            <el-select v-model="s7SearchForm.IsOpen" placeholder="请选择状态" clearable>
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

const s7SearchForm = reactive({
    Search: '',
    IsOpen: ''
})


//重置
function headleResetClick() {
    formRef.value?.resetFields();
}
</script>

<style lang="less" scoped>
.s7-search {
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