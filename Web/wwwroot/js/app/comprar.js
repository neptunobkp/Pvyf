$(document).ready(function () {

    

    $('#filtrov').change(function (ev) { filtrarSubGrid('detalle', this.value); });
    $('#Vm_Anio').change(function (ev) { filtrarSubGrid('anio', this.value); });
    $('#Vm_MarcaId').change(function (ev) { bindear(); });
    $('#Vm_ModeloId').change(function (ev) { filtrarSubGrid('modeloId', this.value); });

    bindear();
});

function bindear() {
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: function (options) {
                $.post('/Ofertas/api/Ofertas',
                    $('#formfiltros').serialize(),
                    function (data) {
                        console.log('data', data);
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