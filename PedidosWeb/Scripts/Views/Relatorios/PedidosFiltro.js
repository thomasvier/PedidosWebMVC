$(document).ready(function()
{
    $('#btnGerarRelatorio').click(function () {
        var url = '';
        var parametros = false;

        if ($('#ddlClientes').val() != '')
        {
            url = 'idCliente=' + $('#ddlClientes').val();
            parametros = true;
        }

        if ($('#DataInicial').val() != '')
        {
            if (url != null) {
                url += '&dataInicial=' + $('#DataInicial').val();
            }
            else
            {
                url += 'dataInicial=' + $('#DataInicial').val();
            }

            parametros = true;
        }

        if($('#DataFinal').val() != '')
        {
            if (url != null) {
                url += '&dataFinal=' + $('#DataFinal').val();
            }
            else {
                url += 'dataFinal=' + $('#DataFinal').val();
            }
                
            parametros = true;
        }

        if (parametros) {
            window.open('/Relatorios/Pedidos?' + url);
        }
        else {
            window.open('/Relatorios/Pedidos');
        }
    });
})