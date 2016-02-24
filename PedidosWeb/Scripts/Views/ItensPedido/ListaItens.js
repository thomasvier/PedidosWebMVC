$(document).ready(function()
{   
    $(".details").click(function () {
        var id = $(this).attr("data-id");
        $("#modalPedidoItem").load("/ItensPedido/ItemPedido?ID=" + id, function () {
            $("#modalPedidoItem").modal();
        })
    });
})