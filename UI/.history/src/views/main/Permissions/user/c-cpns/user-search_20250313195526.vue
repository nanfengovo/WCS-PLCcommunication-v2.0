<template>
  <div class="userSearch">
    <el-card shadow="hover">
      <el-form label-width="80px">
        <el-row :gutter="20">
          <el-col :span="8">
            <el-form-item label="用户名">
              <input type="text">
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
    </el-card>

  </div>
</template>
<script setup lang="ts">
</script>

<style lang="less" scoped>
.userSearch {
  width: 100%;
  padding: 0px;
  // border-radius: 20px; //整个组件都有圆角
  background-color: #fff;
}
</style>
