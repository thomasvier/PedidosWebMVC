$(document).ready(function()
{
    $('#btnGerarRelatorio').click(function () {
        var url = '';
        var parametros = false;

        if ($('#ddlClientes').val() != null)
        {
            url = 'idCliente=' + $('#ddlClientes').val();
            parametros = true;
        }

        if ($('#DataInicial').val() != null)
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

        if($('#DataFinal').val() != null)
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