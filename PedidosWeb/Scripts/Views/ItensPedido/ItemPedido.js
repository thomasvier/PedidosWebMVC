$(document).ready(function () {
    $("#Quantidade").maskMoney({ allowNegative: true, thousands: '.', decimal: ',', affixesStay: false, precision: 2 });
    $("#PrecoCompra").maskMoney({ allowNegative: true, thousands: '.', decimal: ',', affixesStay: false, precision: 2 });

    //$("#btnSalvarItem").click(function () {        
    //    $('#formItemPedido').submit();
    //});
});