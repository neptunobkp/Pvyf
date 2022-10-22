$(document).ready(function () {
    $("#rangeslider").kendoRangeSlider({
        min: 0,
        max: 999,
        smallStep: 10,
        largeStep: 100,
        tickPlacement: "both"
    });
    bindear();
    handleFiltrosNuevos();
});

function bindear() {
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: function (options) {
                $.get(`/Nuevos/api/Destacados`,
                    function (data) {
                        options.success(data);
                    });
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