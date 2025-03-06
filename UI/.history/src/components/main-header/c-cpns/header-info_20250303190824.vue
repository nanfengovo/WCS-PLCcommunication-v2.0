<template>
    <div class="header-info">
        <!--1.操作小图标-->
        <div class="operation">
            <span>
                <el-icon>
                    <Message />
                </el-icon>
            </span>
            <span>
                <span class="dot"></span>
                <el-icon>
                    <ChatDotRound />
                </el-icon>
            </span>
            <span>
                <el-icon>
                    <Search />
                </el-icon>
            </span>
        </div>

        <!--2.头像&&个人信息-->
        <div class="info">
            <el-dropdown>
                <span class="user-info">
                    <span>
                        <el-avatar :size="30"
                            :src="https://cn.bing.com/images/search?view=detailV2&ccid=QvwcVgXK&id=6355CDA992943423B78C61737DAEA6C204DEA24D&thid=OIP.QvwcVgXKx3jOesIln34ggwHaHa&mediaurl=https%3a%2f%2fc-ssl.dtstatic.com%2fuploads%2fitem%2f201606%2f09%2f20160609131917_zktdB.thumb.1000_0.jpeg&exph=1000&expw=1000&q=%e5%9c%86%e5%bd%a2%e7%9a%84%e5%a4%b4%e5%83%8f%e5%9b%be%e7%89%87&simid=608035171032325587&FORM=IRPRST&ck=4FCC46D2D749E652F1552A3C7FA95A31&selectedIndex=78&itb=0" />
                        <span class="naem"> codewhy</span>
                    </span>
                </span>
                <template #dropdown>
                    <el-dropdown-menu>
                        <el-dropdown-item>Action 1</el-dropdown-item>
                        <el-dropdown-item>Action 2</el-dropdown-item>
                        <el-dropdown-item>Action 3</el-dropdown-item>
                        <el-dropdown-item disabled>Action 4</el-dropdown-item>
                        <el-dropdown-item divided>Action 5</el-dropdown-item>
                    </el-dropdown-menu>
                </template>
            </el-dropdown>
        </div>
    </div>
</template>
<script setup lang="ts">
</script>

<style lang="less" scoped>
.header-info {
    display: flex;
    align-items: center;
}

.operation {
    display: inline-flex;
    margin-right: 20px;

    span {
        position: relative;
        display: flex;
        align-items: center;
        justify-content: center;
        width: 40px;
        height: 35px;
        cursor: pointer;

        &:hover {
            background-color: #f2f2f2;
        }

        i {
            font-size: 20px;
        }

        .dot {
            position: absolute;
            top: 3px;
            right: 3px;
            z-index: 10;
            width: 6px;
            height: 6px;
            border-radius: 100%;
            background-color: red;
        }
    }
}
</style>