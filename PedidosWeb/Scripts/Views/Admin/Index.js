$(document).ready(function(){
    var filtro = $('#TiposTitularFiltro').val();
    alert(filtro);
    if (filtro != "")
        $('#selTiposTitularFiltro').val(filtro);

    $('#selTiposTitularFiltro').change(function () {        
        var value = $('option:selected', $(this)).val();        
        $('#TiposTitularFiltro').val(value);        
    });
})