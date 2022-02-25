// TODO: check if the TinyMCE plugin even works correctly or if it just looks stupid when displaying customized text
// Initialize TinyMCE textarea plugin
tinymce.init({
    selector: 'textarea',
    plugins: 'lists',
    menubar: 'file edit format',
    force_br_newlines: false,
    force_p_newlines: false,
    forced_root_block: ''
});