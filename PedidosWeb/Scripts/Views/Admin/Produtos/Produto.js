$(document).ready(function () {
    MascaraCampos();
})

var MascaraCampos = function ()
{
    $("#PrecoUnitario").maskMoney({ allowNegative: true, thousands: '.', decimal: ',', affixesStay: false, precision: 2 });
    $("#PrecoQuantidade").maskMoney({ allowNegative: true, thousands: '.', decimal: ',', affixesStay: false, precision: 2 });
    $("#QuantidadePreco").maskMoney({ allowNegative: true, thousands: '', decimal: ',', affixesStay: false, precision: 3 });
}

