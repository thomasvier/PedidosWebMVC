$(document).ready(function () {
    $('.datepicker').datepicker({
        format: 'dd/mm/yyyy'
    });
});

var mostrarMensagem = function()
{

}

function formatarMoeda(valor, casas, separdor_decimal, separador_milhar) {

    var valor_total = parseInt(valor * (Math.pow(10, casas)));
    var inteiros = parseInt(parseInt(valor * (Math.pow(10, casas))) / parseFloat(Math.pow(10, casas)));
    var centavos = parseInt(parseInt(valor * (Math.pow(10, casas))) % parseFloat(Math.pow(10, casas)));


    if (centavos % 10 == 0 && centavos + "".length < 2) {
        centavos = centavos + "0";
    } else if (centavos < 10) {
        centavos = "0" + centavos;
    }

    var milhoes = parseInt(inteiros / 1000000);
    if (milhoes > 0) {
        var milhares = parseInt((inteiros - (milhoes * 1000000)) / 1000);
        inteiros = (inteiros - (milhoes * 1000000)) % 1000;
    } else {
        var milhares = parseInt(inteiros / 1000);
        inteiros = inteiros % 1000;
    }

    var retorno = "";

    if (milhoes > 0) {
        retorno = milhoes + "" + separador_milhar;

        if (milhares > 0) {
            retorno += milhares + "" + separador_milhar;
        } else if (milhares == 0) {
            milhares = "000";
        } else if (milhares < 10) {
            milhares = "00" + milhares;
        } else if (milhares < 100) {
            milhares = "0" + milhares;
        }

        if (inteiros == 0) {
            inteiros = "000";
        } else if (inteiros < 10) {
            inteiros = "00" + inteiros;
        } else if (inteiros < 100) {
            inteiros = "0" + inteiros;
        }

    } else if (milhares > 0) {
        retorno = milhares + "" + separador_milhar + "" + retorno
        if (inteiros == 0) {
            inteiros = "000";
        } else if (inteiros < 10) {
            inteiros = "00" + inteiros;
        } else if (inteiros < 100) {
            inteiros = "0" + inteiros;
        }
    }


    retorno += inteiros + "" + separdor_decimal + "" + centavos;

    return retorno;

}