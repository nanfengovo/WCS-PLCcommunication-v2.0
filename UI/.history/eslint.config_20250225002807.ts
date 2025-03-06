import pluginVue, { rules } from 'eslint-plugin-vue'
import {
    defineConfigWithVueTs,
    vueTsConfigs
} from '@vue/eslint-config-typescript'
import skipFormatting from '@vue/eslint-config-prettier/skip-formatting'

// To allow more languages other than `ts` in `.vue` files, uncomment the following lines:
// import { configureVueProject } from '@vue/eslint-config-typescript'
// configureVueProject({ scriptLangs: ['ts', 'tsx'] })
// More info at https://github.com/vuejs/eslint-config-typescript/#advanced-setup

export default defineConfigWithVueTs(
    {
        name: 'app/files-to-lint',
        files: ['**/*.{ts,mts,tsx,vue}']
    },

    {
        name: 'app/files-to-ignore',
        ignores: ['**/dist/**', '**/dist-ssr/**', '**/coverage/**']
    },
    {
        rules: {
            'vue/multi-word-component-names': 'off' // 设置为 "off" 完全禁用这个规则，或者设置为 "warn" 只发出警告
        }
    },
    pluginVue.configs['flat/essential'],
    vueTsConfigs.recommended,
    skipFormatting
)
