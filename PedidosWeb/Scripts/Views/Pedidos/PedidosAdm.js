﻿$(document).ready(function () {
    $("#txtQuantidade").maskMoney({ allowNegative: true, thousands: '.', decimal: ',', affixesStay: false, precision: 2 });
    $("#txtPreco").maskMoney({ allowNegative: true, thousands: '.', decimal: ',', affixesStay: false, precision: 2 });
    $("#ValorFrete").maskMoney({ allowNegative: true, thousands: '.', decimal: ',', affixesStay: false, precision: 2 });
    
    submeterFormulario();
    inserirItem();
    
    $('#ClienteID').change(function () {
        var value = $('option:selected', $(this)).val();
        
        if(value == '0')
        {
            $('#btnModalPedidoItem').addClass('disabled');
        }
        else
        {
            $('#Permanecer').val('1');
            $('#pedidosCreate').submit();
            $('#btnModalPedidoItem').removeClass('disabled');
        }
    });

    //eventoAutcomplete();

    //$('#listarClientes').click(function () {
    //    $("#Cliente_RazaoSocial").focus();
    //    $('#Cliente_RazaoSocial').autocomplete("search", "");        
        
    //})
    
    if ($('#ID').val() != '0') {
        $('#btnModalPedidoItem').removeClass('disabled');
    }

    if ($('#ClienteID').val() != '') {
        $('#btnModalPedidoItem').removeClass('disabled');
    }

    //$('#btnCadastroRapido').click(function () {
    //    var ID = $('#ID').val();
        
    //    $('#cadastroRapidoClientes').load("/Clientes/CadastroRapido?a=Pedidos&v=Pedido&idc="+ ID, function () {
    //        $('#cadastroRapidoClientes').modal();
    //    })

    //});

    $("#btnModalPedidoItem").click(function () {
        $("#modalPedidoItem").load("/ItensPedido/ItemPedido", function () {
            
            $("#modalPedidoItem").fadeIn().modal();

            $("#IDPedido").val($("#ID").val());
        })
    });
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

var eventoAutcomplete = function()
{
    var InserirPedido = false;

    $('#Cliente_RazaoSocial').autocomplete({
        minLength: 0,
        source: function (request, response) {
            var customer = new Array();
            $.ajax({
                async: false,
                cache: false,                
                type: "GET",
                url: "/Pedidos/RetornarClientes",
                data: { "term": request.term },
                success: function (data) {
                    for (var i = 0; i < data.length ; i++) {
                        customer[i] = { label: data[i].RazaoSocial, Id: data[i].ID };

                    }
                },
                
            });
            if (customer.length > 0)
                response(customer);
            else {
                $('#ClienteID').val('');
                $('#btnSalvar').addClass('disabled');
            }
        },
        select: function (event, ui) {
            //fill selected customer details on form
            $.ajax({
                cache: false,
                async: false,
                type: "POST",
                url: "/Pedidos/ClienteSelecionado",
                data: { id: ui.item.Id },
                success: function (data) {
                    $('#ClienteID').val(data.ID);                    

                    InserirPedido = true;

                    $('#btnSalvar').removeClass('disabled');
                    action = data.Action;

                    $('#Permanecer').val('1');
                    $('#pedidosCreate').submit();
                    //$.ajax({
                    //    type: "POST",
                    //    url: "/Pedidos/SalvarPedido",
                    //    data: { ClienteID: data.ID }
                    //});
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert('Failed to retrieve states.');
                }
            });
        }        
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

//calcular total dos itens
var calculaTotal = function()
{
    var total = '';

    $('table#itens-pedido tr').each(function () {
        var item = parseFloat($(this).data('total'));
        
        

        if (!isNaN(item))
        {            
            if (!total)
            {
                total = $(this).data('total').toString();
            }
            else
            {
                total += ':' + $(this).data('total').toString();
            }            
        }
    });    

    $.ajax(
        {
            url: '/Pedidos/CalcularTotal',
            dataType: 'json',
            data: { valores: total },
            success: function (data) {
                $("#ValorTotal").val('R$ ' + data);
            }
        });
}

var submeterFormulario = function()
{
    $('#btnSalvar').click(function () {        
        $('#pedidosCreate').submit();        
    });
}



