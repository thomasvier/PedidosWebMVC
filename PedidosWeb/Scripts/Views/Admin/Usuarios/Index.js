$(document).ready(function () {

    var ativoFiltro = $('#ativoFiltro').val();

    if (ativoFiltro != "")
        $('#selAtivoFiltro').val(ativoFiltro);

    $('#selAtivoFiltro').change(function () {
        var value = $('option:selected', $(this)).val();
        $('#ativoFiltro').val(value);
    });

    var tipoUsuarioFiltro = $('#tipoUsuarioFiltro').val();

    if (tipoUsuarioFiltro != "")
        $('#selTiposTitularFiltro').val(tipoUsuarioFiltro);

    $('#selTiposTitularFiltro').change(function () {
        var value = $('option:selected', $(this)).val();
        $('#tipoUsuarioFiltro').val(value);
    });
})
