<template>
  <div class="role-content">
    <div class="content-top">
      <div class="title">
        <el-button type="primary" icon="Refresh">刷新</el-button>
        <el-button type="danger" icon="delete">删除</el-button>
      </div>
      <el-button icon="Plus" type="primary">新增角色</el-button>
    </div>
    <div class="content">
      <el-scrollbar max-height="550px">
        <el-table :data=data border style="width: auto;" stripe>
          <el-table-column align="center" type="selection" width="40px" />
          <el-table-column align="center" type="index" label="序号" width="60px" />
          <el-table-column align="center" prop="role" label="角色名" width="180" />
          <el-table-column align="center" prop="Permissions" label="权限" width="auto" />
          <el-table-column align="center" label="操作" width="230">
            <template #default="scope">
              <el-button type="primary" size="small" text icon="Edit">编辑</el-button>
              <el-button :type="scope.row.isOpen ? 'danger' : 'primary'" size="small" :text="true"
                :icon="scope.row.isOpen ? 'CircleCloseFilled' : 'SuccessFilled'">
                {{ scope.row.isOpen ? '禁用' : '启用' }}
              </el-button>
            </template>
          </el-table-column>
        </el-table>
      </el-scrollbar>
    </div>


  </div>
</template>
<script setup lang="ts">
// import { ref } from 'vue'

const data = [{
  role: '管理员',
  Permissions: '系统管理,用户管理,角色管理'
}
]
</script>

<style lang="less" scoped>
.role-content {
  margin-top: 20px;
  background-color: #fff;
  border-radius: 20px;
}

.content-top {
  display: flex;
  justify-content: space-between;
  padding: 20px;
  align-items: flex-end;
  margin-bottom: 15px;

  .title {
    display: flex;
    align-items: center;

    h2 {
      font-size: 18px;
      margin-right: 20px;
    }
  }
}

.content {
  min-height: 550px;
}
</style>
