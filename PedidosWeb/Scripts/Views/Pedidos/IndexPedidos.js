$(document).ready(function () {
    var situacaoPedido = $('#situacaoPedidoFiltro').val();
    
    if (situacaoPedido != "")
        $('#selSituacaoPedido').val(situacaoPedido);

    $('#selSituacaoPedido').change(function () {
        var value = $('option:selected', $(this)).val();        
        $('#situacaoPedidoFiltro').val(value);
    });
});