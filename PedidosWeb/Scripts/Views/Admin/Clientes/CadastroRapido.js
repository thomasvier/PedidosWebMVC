$(document).ready(function () {
    //MascaraCampos();
    //AplicaFuncoes();
});

//Método que mascara os campos do form
var MascaraCampos = function () {
    $('#Cep').mask('00000-000');
    $('#Numero').mask('000000');
    $('#Telefone').mask('(00) 0000-00000')
    $('#Celular').mask('(00) 0000-00000')

    var tipo = $('#Tipo').val();

    if (tipo == 0) {
        $('#CPFCNPJ').mask('000.000.000-00');
        $('#lblCPFCNPJ').text('CPF');
    }
    else {
        $('#lblCPFCNPJ').text('CNPJ');
        $('#CPFCNPJ').mask('00.000.000/0000-00');
    }
}

var AplicaFuncoes = function () {
    $('#Tipo').change(function () {
        var value = $('option:selected', $(this)).val();

        if (value == 0) {
            $('#CPFCNPJ').mask('000.000.000-00');
            $('#lblCPFCNPJ').text('CPF');
        }
        else {
            $('#CPFCNPJ').mask('00.000.000/0000-00');
            $('#lblCPFCNPJ').text('CNPJ');
        }
    })
}