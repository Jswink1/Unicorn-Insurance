// Initialize TinyMCE textarea plugin
tinymce.init({
    selector: 'textarea',
    plugins: 'lists',
    menubar: 'file edit format',
    force_br_newlines: false,
    force_p_newlines: false,
    forced_root_block: ''
});