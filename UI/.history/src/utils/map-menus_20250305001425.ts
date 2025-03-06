export function mapPathToBreadcrumbs(path: string, menus: any[]) {
    const breadcrumbs: any[] = []
    for (const menuitem of menus) {
        for (const submenu of menuitem.children) {
            if (submenu.path === path) {
                breadcrumbs.push(menuitem)
                breadcrumbs.push(submenu)
                return breadcrumbs
            }
        }
    }
}
