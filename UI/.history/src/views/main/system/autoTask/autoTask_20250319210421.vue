<template>
    <div class="S7autoTask">
        <div class="s7-content" v-if="isMounted" v-loading="loading">
            <H2>S7数据点定时任务</H2>
            <div class="content-top">
                <div class="content-top-left">
                    <el-button type="primary" icon="Refresh" @click="refresh">刷新</el-button>
                    <el-button type="primary" icon="Plus" @click="dialogOverflowVisible = true">添加</el-button>
                    <el-button type="danger" icon="delete" @click="deleteSelectedRows">删除</el-button>
                </div>
                <div class="content-top-right">
                    <el-button type="success" icon="Upload">导入</el-button>
                    <el-button type="warning" icon="Download">导出</el-button>
                </div>
            </div>
            <div class="content">
                <el-scrollbar max-height="400px" class="scrollbar-demo-item">
                    <el-table border style="width: 100%" :data="currentPageData" ref="multipleTableRef">
                        <el-table-column align="center" type="selection" width="40px" />
                        <el-table-column align="center" label="操作" width="320">
                            <template #default="scope">
                                <el-button type="primary" size="small" text icon="Edit"
                                    @click="EditS7(scope.row)">编辑</el-button>
                                <el-button @click="handleClick(scope.row)"
                                    :type="scope.row.isOpen ? 'danger' : 'primary'" size="small" :text="true"
                                    :icon="scope.row.isOpen ? 'CircleCloseFilled' : 'SuccessFilled'">
                                    {{ scope.row.isOpen ? '禁用' : '启用' }}
                                </el-button>
                            </template>
                        </el-table-column>
                        <el-table-column align="center" prop="result" label="读写详情" width="400px">
                            <template #default="scope">
                                <input type="text" v-model="scope.row.result"
                                    style="width: 100%; height: 100%; border: none; outline: none; text-align: center; font-size: 14px; color: #606266; background-color: transparent;" />
                            </template>
                        </el-table-column>
                        <el-table-column align="center" type="index" label="序号" width="60px" />
                        <el-table-column align="center" prop="proxyName" label="配置名" width="280" />
                        <el-table-column align="center" prop="ip" label="ip" width="150" />
                        <el-table-column align="center" prop="port" label="端口" width="80" />
                        <el-table-column align="center" prop="dbid" label="DB块id" width="80" />
                        <el-table-column align="center" prop="address" label="地址偏移" width="180" />
                        <el-table-column align="center" prop="type" label="数据类型" width="100" />
                        <el-table-column align="center" prop="length" label="数据长度" width="180" />
                        <el-table-column align="center" prop="bit" label="位地址" width="80" />
                        <el-table-column align="center" prop="remark" label="备注" width="180" />
                        <el-table-column align="center" prop="isOpen" label="状态" width="100">
                            <!-- 作用域插槽 -->
                            <template #default="scope">
                                <el-button size="small" :type="scope.row.isOpen ? 'success' : 'danger'" plain>
                                    {{ scope.row.isOpen ? '启用' : '禁用' }}
                                </el-button>
                            </template>
                        </el-table-column>
                        <el-table-column align="center" prop="createtime" label="创建时间" width="250">
                            <!-- 作用域插槽进行时间格式化(使用Day.js) -->
                            <template #default="scope">
                                {{ formatTime(scope.row.createtime) }}
                            </template>
                        </el-table-column>
                        <el-table-column align="center" prop="lastModified" label="最后修改时间" width="250">
                            <template #default="scope">
                                {{ formatTime(scope.row.lastModified) }}
                            </template>
                        </el-table-column>
                    </el-table>
                </el-scrollbar>


            </div>


            <!-- 分页 -->
            <!-- 分页控件 -->
            <!-- arr.slice((当前页数-1)*每页条数，当前页数 *当前条数)-->
            <el-pagination :current-page="page" :page-size="limit" :page-sizes="[10, 20, 30, 40, 50]" background
                layout="total, sizes, prev, pager, next, jumper" :total="total" @size-change="handleSizechange"
                @current-change="handleCurrentChange" />

        </div>
    </div>
</template>
<script setup lang="ts">
import { ref } from 'vue';



const isMounted = ref(true);
const loading = ref(false)

</script>

<style lang="less" scoped>
.s7-content {
    background-color: #fff;
    padding: 20px;
    border-radius: 10px;
    margin-top: 20px;

}

.content {
    margin-top: 20px;
}

.content-top {
    display: flex;
    justify-content: space-between;
}

.pagination {
    margin-top: 10px;
    display: flex;
    justify-content: flex-end;
}

.scrollbar-demo-item {
    display: flex;
    align-items: center;
    justify-content: center;
    height: 400px;
    margin: 10px;
    text-align: center;
    border-radius: 4px;
    background: var(--el-color-primary-light-9);
    color: var(--el-color-primary);
}
</style>