interface IBreadcrumbs {
    name: string
    path: string
}
export function mapPathToBreadcrumbs(path: string, menus: any[]) {
    const breadcrumbs: IBreadcrumbs[] = []
    for (const menuitem of menus) {
        for (const submenu of menuitem.children) {
            if (submenu.path === path) {
                breadcrumbs.push({ name: menuitem.name, path: menuitem.url })
                breadcrumbs.push({ name: submenu.name, path: submenu.url })
                return breadcrumbs
            }
        }
    }
}
