<template>
    <div class="ModbusTCPautoTask">
        <div class="ModbusTCPautoTask-content" v-if="isMounted">
            <H2>ModbusTcp数据点定时任务(使用Quartz.NET)</H2>
            <div class="content-top">
                <div class="content-top-left">
                    <el-button type="primary" icon="Refresh" @click="refresh">刷新</el-button>
                    <el-button type="primary" icon="Plus" @click="ModbusTCPTaskDialogVisible = true">添加</el-button>

                </div>
                <div class="content-top-right">
                    <!-- 上传组件 -->
                    <el-upload ref="uploadRef" class="upload-demo" :action="uploadUrl" :auto-upload="true" :limit="1"
                        :before-upload="beforeUpload" :on-success="handleSuccess" :on-error="handleError"
                        :headers="headers" accept=".xlsx,.xls">
                        <template #trigger>
                            <el-button type="primary" icon="Upload" :loading="uploadLoading"
                                @click="ModbussubmitUpload">{{
                                    uploadLoading ? '正在导入...' : '导入' }}</el-button>
                        </template>
                    </el-upload>

                    <!-- 导出按钮 -->
                    <el-button type="warning" icon="Download" @click="ModbushandleExport" :loading="exportLoading">
                        {{ exportLoading ? '正在导出...' : '导出' }}
                    </el-button>
                </div>
            </div>
            <div class="content">
                <el-scrollbar max-height="200px">
                    <el-table border style="width: 100%" :data="currentPageData" ref="multipleTableRef" height="200">
                        <el-table-column align="center" type="selection" width="40px" />
                        <el-table-column align="center" label="操作" width="320">
                            <template #default="scope">
                                <el-button type="danger" icon="delete" size="small" text
                                    @click="deleteS7Task(scope.row)">删除</el-button>
                                <el-button @click="editS7Task(scope.row)" type="primary" size="small" text
                                    icon="Edit">编辑</el-button>
                                <el-button @click="changeTaskStatus(scope.row)"
                                    :type="scope.row.isOpen ? 'danger' : 'primary'" size="small" :text="true"
                                    :icon="scope.row.isOpen ? 'CircleCloseFilled' : 'SuccessFilled'">
                                    {{ scope.row.isOpen ? '关闭定时任务' : '开启定时任务' }}
                                </el-button>

                            </template>
                        </el-table-column>
                        <el-table-column align="center" prop="result" label="任务详情" width="200px" show-overflow-tooltip>
                            <template #default="scope">
                                <input type="text" v-model="scope.row.result" readonly
                                    style="width: 100%; height: 100%; border: none; outline: none; text-align: center; font-size: 14px; color: #606266; background-color: transparent;" />
                            </template>
                        </el-table-column>
                        <el-table-column align="center" type="index" label="序号" width="60px" />
                        <el-table-column align="center" prop="name" label="定时任务名" width="280" />
                        <el-table-column align="center" prop="ip" label="ip" width="150" />
                        <el-table-column align="center" prop="port" label="端口" width="80" />
                        <el-table-column align="center" prop="dBaddr" label="变量地址" width="180" />
                        <el-table-column align="center" prop="result" label="结果" width="80" />
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
                <!-- 分页 -->
                <!-- 分页控件 -->
                <!-- arr.slice((当前页数-1)*每页条数，当前页数 *当前条数)-->
                <el-pagination :current-page="page" :page-size="limit" :page-sizes="[10, 20, 30, 40, 50]" background
                    layout="total, sizes, prev, pager, next, jumper" :total="total" @size-change="handleSizechange"
                    @current-change="handleCurrentChange" class="pagination" />
            </div>
        </div>

    </div>
</template>
<script setup lang="ts">
</script>

<style lang="less" scoped>
.ModbusTCPautoTask-content {
    background-color: #fff;
    padding: 20px;
    border-radius: 10px;
    margin-top: 20px;

}
</style>