$(document).ready(function () {
    $("#Quantidade").maskMoney({ allowNegative: true, thousands: '.', decimal: ',', affixesStay: false, precision: 3 });
    $("#PrecoCompra").maskMoney({ allowNegative: true, thousands: '.', decimal: ',', affixesStay: false, precision: 2 });

    $('#Quantidade').focusout(function () {
        var idProduto = $('#IDProduto').val()

        if (idProduto > 0) {
            calcularTotal();
        }
    });
    
    //if($('#IDProduto').val() != null)
    //{
    //    $('#IDProduto').attr('disabled', 'disabled');
    //}

    //$("#btnSalvarItem").click(function () {        
    //    $('#formItemPedido').submit();
    //});
});

var calcularTotal = function(idProduto)
{
    var idProduto = $('#IDProduto').val()
    var quantidade = $('#Quantidade').val();

    $.ajax(
        {
            url: '/Uteis/RetornarPreco',
            dataType: 'json',
            data: { id: idProduto, quant: quantidade },
            async: false,
            success: function (data) {
                $('#PrecoCompra').val(data.preco);
                $('#TotalItem').val(data.total);
            }
        });
}