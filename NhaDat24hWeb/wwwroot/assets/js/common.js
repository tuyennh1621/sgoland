function menuActive() {
    const menu_item_id_active = localStorage.getItem('menu-active');
    if (menu_item_id_active) {
        const element = $('#' + menu_item_id_active);
        element.addClass('active');
        element.parents('.collapse').addClass('show')
        const parent_id = element.data('parent')
        if (parent_id) {
            $('#' + parent_id).attr('aria-expanded', 'true')
        }
    }
}
menuActive();




