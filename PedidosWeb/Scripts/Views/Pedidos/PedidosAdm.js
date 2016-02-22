$(document).ready(function () {
    $("#txtQuantidade").maskMoney({ allowNegative: true, thousands: '.', decimal: ',', affixesStay: false, precision: 2 });
    $("#txtPreco").maskMoney({ allowNegative: true, thousands: '.', decimal: ',', affixesStay: false, precision: 2 });
    $('#txtQuantidade').focusout(function () {
        var idProduto = $('#ddlProdutos').val();

        calculaItem(idProduto);
    });

    inserirItem();
});

//Remove o item selecionado da tabela de produtos
var aplicarFuncoesTabela = function (idRemover, idEditar)
{
    //Deleta um item da tabela de produtos do pedido
    $('table#itens-pedido tr').each(function(){
        var tr = $(this);

        var botao = tr.find('td').find('#'+idRemover);

        $(botao).click(function () {
            tr.remove();
            calculaTotal();
        });
    });

    //Editar o item da tabela
    $('table#itens-pedido tr').each(function () {
        var tr = $(this);

        var botao = tr.find('td').find('#' + idEditar);

        $(botao).click(function () {
            var idProduto = tr.data('id');
            var quantidade = tr.data('quant');
            var preco = tr.data('preco');
            var total = tr.data('total');
            
            $('select#ddlProdutos option:selected').val(idProduto);
            $('#txtQuantidade').val(quantidade);
            $('#txtPreco').val(preco);
            $('#txtTotal').val(total);

            tr.remove();
            calculaTotal();
        });
    });
}

//Insere um novo item na tabela de produtos
var inserirItem = function()
{
    $('#InserirItem').click(function () {
        
        var idProduto = $('select#ddlProdutos option:selected').val();

        calculaItem(idProduto);

        var produto = $('select#ddlProdutos option:selected').text();
        var quantidade = $('#txtQuantidade').val();
        var preco = $('#txtPreco').val();
        var total = $('#txtTotal').val();
                        
        if (quantidade == '')
            quantidade = '0,00';

        if (preco == '')
            preco = '0,00';

        if (total == '')
            total = '0,00'

        $("#itens-pedido").find('tbody')
        .append($('<tr data-id="' + idProduto + '" data-quant="' + quantidade + '" data-preco="' + preco + '" data-total="' + total + '">')
            .append($('<td> ' + produto + '</td>' +
                      '<td> ' + quantidade + '</td>' +
                      '<td> ' + preco + '</td>' +
                      '<td> ' + total + '</td>' +
                      '<td> ' + '<div class="pull-right"><button type="button" id="' + idProduto + '_editar" class="btn btn-primary" title="Editar"><i class="glyphicon glyphicon-edit"></i> </button> ' +
                      '<button type="button" id="' + idProduto + '_remover" class="btn btn-danger" title="Remover"><i class="glyphicon glyphicon-remove"></i></button></div>' + '</td>')
            )
        );

        var idRemover = idProduto + '_remover';
        var idEditar = idProduto + '_editar';
        var tr = $('#itens-pedido').find('tbody').find('tr');

        aplicarFuncoesTabela(idRemover, idEditar);

        $("#txtQuantidade").attr("placeholder", "0,00").val("");
        $("#txtPreco").attr("placeholder", "0,00").val("");
        $("#txtTotal").attr("placeholder", "0,00").val("");

        calculaTotal();
    });
}

var calculaItem = function(idProduto)
{
    var quantidade = $('#txtQuantidade').val();

    $.ajax(
        {
            url: '/Pedidos/RetornarPreco',
            dataType: 'json',
            data: { id: idProduto, quant: quantidade },
            async: false,
            success: function (data) {
                $('#txtPreco').val(data.preco);
                $('#txtTotal').val(data.total);
            }
        });
}

var calculaTotal = function()
{
    var total = 0;

    $('table#itens-pedido tr').each(function () {
        var totalItem = parseFloat($(this).data('total'));
            });
    
    $("#ValorTotal").maskMoney({ allowNegative: true, thousands: '.', decimal: ',', affixesStay: false, precision: 2 });

    $("#ValorTotal").maskMoney('mask', total);
}

