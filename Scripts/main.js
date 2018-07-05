$(document).ready(function () {

    /* 
		Aqui menu

	*/

    $('[data-toggle="tooltip"]').tooltip()

    $("#menu-toggle").click(function (e) {
        e.preventDefault();

        $("#wrapper").toggleClass("toggled");

        if (!$("#wrapper").hasClass("toggled")) {
            $(".sidebar-nav-subitem").collapse('hide');
        }

    });

    $("#sidebar-wrapper").on("click", ".sidebar-nav-icon", function () {
        if (!$("#wrapper").hasClass("toggled")) {
            $("#wrapper").addClass("toggled");
        }
    });


    $('#comboboxselect').multiselect({
        buttonText: function (options, select) {
            return 'Selecione o colaborador';
        },
        buttonWidth: '100%',
        includeSelectAllOption: true,
        selectAllText: 'Selecionar todos'
    });



    var docBody = $(document.body);

    docBody.on('click', '.js-container-datepicker', function () {
        datepickers = function () {
            if ($('.js-container-datepicker').length > 0) {
                $.datepicker.regional['pt-BR'] = {
                    closeText: 'Fechar',
                    prevText: '&#x3c;Anterior',
                    nextText: 'Pr&oacute;ximo&#x3e;',
                    currentText: 'Hoje',
                    monthNames: ['Janeiro', 'Fevereiro', 'Mar&ccedil;o', 'Abril', 'Maio', 'Junho',
                    'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
                    monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun',
                    'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
                    dayNames: ['Domingo', 'Segunda-feira', 'Ter&ccedil;a-feira', 'Quarta-feira', 'Quinta-feira', 'Sexta-feira', 'Sabado'],
                    dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sab'],
                    dayNamesMin: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sab'],
                    weekHeader: 'Sm',
                    dateFormat: 'dd/mm/yy',
                    firstDay: 0,
                    isRTL: false,
                    showMonthAfterYear: false,
                    yearSuffix: ''
                };
                $.datepicker.setDefaults($.datepicker.regional['pt-BR']);

                $('.js-is-datepicker').datepicker({
                    showOn: "button",
                    buttonText: "",
                    defaultDate: "+1w",
                    changeMonth: true,
                    numberOfMonths: 1,
                    onClose: function (selectedDate) {
                        $("#to").datepicker("option", "minDate", selectedDate);
                    }
                });
            }
        };
    });
});