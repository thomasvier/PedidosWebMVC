$(document).ready(function()
{   
    $("#btnModalPedidoItem").click(function () {        
        $("#modalPedidoItem").load("/ItensPedido/CadastrarItem", function () {
            $("#modalPedidoItem").modal();
        })
    });
    
})