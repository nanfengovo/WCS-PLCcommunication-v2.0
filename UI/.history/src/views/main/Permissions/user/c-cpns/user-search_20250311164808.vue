<template>
  <div class="userSearch">
    <el-card shadow=hover>
      <el-form>
        <el-row>
          <el-col :span="12">
            <el-form-item label="用户名:">
              <input type="text" placeholder="请输入用户名">
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Activity zone">
              <el-select placeholder="请选择角色">
                <el-option label="Zone one" value="shanghai" />
                <el-option label="Zone two" value="beijing" />
              </el-select>
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
  padding: 10px;
}
</style>
