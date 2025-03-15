<template>

  <el-card shadow="hover" class="search-card">
    <el-form label-width="80px" :model="searchForm">
      <el-row :gutter="20" ref="formRef">
        <el-col :span="8">
          <el-form-item label="用户名:" v-model="searchForm.name">
            <input type="text" placeholder="请输入需要查询的用户名:">
          </el-form-item>
        </el-col>
        <el-col :span="8">
          <el-form-item label="角色:">
            <input type="text" placeholder="请输入需要查询的角色:">
          </el-form-item>
        </el-col>
        <el-col :span="8">
          <el-button type="primary">
            查询
          </el-button>
          <el-button @click="headleResetClick">
            重置
          </el-button>
        </el-col>
      </el-row>
    </el-form>
  </el-card>


</template>
<script setup lang="ts">
import { reactive, ref } from 'vue';

const formRef = ref<any>()
const searchForm = reactive({
  'name': '',
})
</script>

<style lang="less" scoped>
.search-card {
  border-radius: 20px;
}
</style>
