<template>
  <el-card shadow="hover" class="search-card">
    <el-form label-width="80px" :model="searchForm" ref="formRef">
      <el-row :gutter="20" ref="formRef" justify="center">
        <el-col :span="8">
          <el-form-item label="用户名:" prop="name">
            <el-input type="text" v-model="searchForm.name" placeholder="请输入需要查询的用户名:" />
          </el-form-item>
        </el-col>
        <el-col :span="8">
          <el-form-item label="角色:" prop="role">
            <el-input type="text" v-model="searchForm.role" placeholder="请输入需要查询的角色:" />
          </el-form-item>
        </el-col>
        <el-col :span="8">
          <el-button type="primary" icon="search">
            查询
          </el-button>
          <el-button icon="Refresh" @click="headleResetClick">
            重置
          </el-button>
        </el-col>
      </el-row>
    </el-form>
  </el-card>


</template>
<script setup lang="ts">
import type { ElForm } from 'element-plus';
import { reactive, ref } from 'vue';

const formRef = ref<InstanceType<typeof ElForm>>()
const searchForm = reactive({
  'name': '',
  'role': ''
})

//重置
function headleResetClick() {
  formRef.value?.resetFields();
}
</script>

<style lang="less" scoped>
.search-card {
  border-radius: 20px;
}
</style>
