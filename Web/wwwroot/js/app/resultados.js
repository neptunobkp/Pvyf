$(document).ready(function () {

    $("#rangecilindrada").kendoRangeSlider({
        min: 0,
        max: 10,
        smallStep: 1,
        largeStep: 5,
        tickPlacement: "both"
    });

    handleFiltrosNuevos();

    $(".kendoswitchs").kendoMobileSwitch({
        onLabel: "Si",
        offLabel: "No",
    });
    bindear();

    $('#formfiltros').on('submit', function (e) {
        e.preventDefault();
        var dataSource = new kendo.data.DataSource({
            transport: {
                read: function (options) {
                    $.post('/Nuevos/api/VehiculosNuevos',
                        $('#formfiltros').serialize(),
                        function (data) {
                            options.success(data);
                        });
                }
            },
            schema:
            {
                data: function (response) {
                    $('#spanresultado').html(response.length == 1 ? '1 Resultado' : `${response.length} Resultados`);
                    return response;
                }
            }
        });

        $("#listView").kendoListView({
            dataSource: dataSource,
            template: kendo.template($("#template").html())
        });
    });
});



function bindear() {
    console.log('bindeado');
    var busquedaId = $('#busquedaid').val();

    var dataSource = new kendo.data.DataSource({
        transport: {
            read: function (options) {
                $.get(`/Nuevos/api/VehiculosNuevos?id=${busquedaId}`,
                    function (data) {
                        options.success(data);
                    });
            }
        },
        schema:
        {
            data: function (response) {
                $('#spanresultado').html(response.length == 1 ? '1 Resultado' : `${response.length} Resultados`);
                console.log('data resultados get', response);
                return response;
            }
        }
    });

    $("#listView").kendoListView({
        dataSource: dataSource,
        template: kendo.template($("#template").html())
    });
}

function filtrarSubGrid(campoNombre, valor) {
    console.log('valor', valor);
    var grid = $('#listView').data('kendoListView');
    var field = campoNombre;
    var operator = 'contains';
    var value = valor;
    grid.dataSource.filter({
        field: field,
        operator: operator,
        value: value
    });
}