$(document).ready(function(){
    var tiposTitularFiltro = $('#tiposTitularFiltro').val();
    
    $("#IDRepresentante").attr('selectedIndex', 0);

    if (tiposTitularFiltro != "")
        $('#selTiposTitularFiltro').val(tiposTitularFiltro);

    $('#selTiposTitularFiltro').change(function () {
        var value = $('option:selected', $(this)).val();
        $('#tiposTitularFiltro').val(value);
    });

    var ativoFiltro = $('#ativoFiltro').val();

    if (ativoFiltro != "")
        $('#selAtivoFiltro').val(ativoFiltro);

    $('#selAtivoFiltro').change(function () {
        var value = $('option:selected', $(this)).val();
        $('#ativoFiltro').val(value);
    });
})


