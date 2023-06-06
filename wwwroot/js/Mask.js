document.addEventListener('DOMContentLoaded', function () {
    var telefoneInputs = document.querySelectorAll('.mascara-telefone');
    telefoneInputs.forEach(function (input) {

        IMask(input, {
            mask: '(00) 00000-0000'

        });
    });
});

document.addEventListener('DOMContentLoaded', function () {
    var cnpjInputs = document.querySelectorAll('.mascara-cnpj');
    cnpjInputs.forEach(function (input) {

        IMask(input, {
            mask: '00000000000000'

        });
    });
});