$(document).ready(function () {
    $("#txtQuantidade").maskMoney({ allowNegative: true, thousands: '.', decimal: ',', affixesStay: false, precision: 2 });
    $("#txtPreco").maskMoney({ allowNegative: true, thousands: '.', decimal: ',', affixesStay: false, precision: 2 });

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
        });
    });

    //Editar o item da tabela
    $('table#itens-pedido tr').each(function () {
        var tr = $(this);

        var botao = tr.find('td').find('#' + idEditar);

        $(botao).click(function () {
            var idProduto = tr.find('input[type="hidden"]').val();
            var quantidade = tr.find('span[id="quantidade"]').text();
            var preco = tr.find('span[id="preco"]').text();
            var total = tr.find('span[id="total"]').text();
            
            $('select#ddlProdutos option:selected').val(idProduto);
            $('#txtQuantidade').val(quantidade);
            $('#txtPreco').val(preco);
            $('#txtTotal').val(total);
            
        });
    });
}

//Insere um novo item na tabela de produtos
var inserirItem = function()
{
    $('#InserirItem').click(function () {
        
        var idProduto = $('select#ddlProdutos option:selected').val();
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
        .append($('<tr>')
            .append($('<td> ' + '<input type="hidden" name="' + idProduto + '_item" value="' + idProduto + '" />' + '</td>' +
                      '<td> ' + produto + '</td>' +
                      '<td> ' + '<span id="quantidade">' + quantidade + '</span></td>' +
                      '<td> ' + '<span id="preco">' + preco + '</span></td>' +
                      '<td> ' + '<span id="total">' + total + '</span></td>' +
                      '<td> ' + '<div class="pull-right"><button type="button" id="' + idProduto + '_editar" class="btn btn-primary" title="Editar"><i class="glyphicon glyphicon-edit"></i> </button> ' +
                      '<button type="button" id="' + idProduto + '_remover" class="btn btn-danger" title="Remover"><i class="glyphicon glyphicon-remove"></i></button></div>' + '</td>')
            )
        );

        var idRemover = idProduto + '_remover';
        var idEditar = idProduto + '_editar';
        var tr = $('#itens-pedido').find('tbody').find('tr');
        aplicarFuncoesTabela(idRemover, idEditar);
    });
}