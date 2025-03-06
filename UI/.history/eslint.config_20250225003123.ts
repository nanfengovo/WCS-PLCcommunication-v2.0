import pluginVue from 'eslint-plugin-vue'
import {
    defineConfigWithVueTs,
    vueTsConfigs
} from '@vue/eslint-config-typescript'
import skipFormatting from '@vue/eslint-config-prettier/skip-formatting'

export default defineConfigWithVueTs(
    {
        name: 'app/files-to-lint',
        files: ['**/*.{ts,mts,tsx,vue}'],
        rules: {
            // 在这里直接覆盖规则
            'vue/multi-word-component-names': 'off'
        }
    },
    {
        name: 'app/files-to-ignore',
        ignores: ['**/dist/**', '**/dist-ssr/**', '**/coverage/**']
    },
    // 确保自定义规则覆盖默认规则
    {
        extends: [
            pluginVue.configs['flat/essential'],
            vueTsConfigs.recommended,
            skipFormatting
        ],
        rules: {
            // 再次确保自定义规则被应用
            'vue/multi-word-component-names': 'off'
        }
    }
)
