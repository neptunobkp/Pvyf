

function editableGrid(configuracion) {
    console.log(configuracion);

    var myColumns = [
        ...configuracion.columnas,
        {
            command: [
                { name: "edit", text: { edit: " ", update: " ", cancel: " " } }
            ],
            title: "Acctiones",
            width: "140px"
        }
    ];


    $("#grid").kendoGrid({
        toolbar: configuracion.toolbar,
        excel: {
            fileName: `PostVenta_${configuracion.nombre}.xlsx`,
            filterable: true
        },
        dataSource: {
            transport: {
                read: {
                    url: `/${configuracion.nombre}/api/${configuracion.nombre}`
                },
                update: {
                    url: `/${configuracion.nombre}/api/${configuracion.nombre}`,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    type: "PUT",
                    complete: function (e) {
                        notificar("Registro actualizado");
                    }
                },
                destroy: {
                    url: function (options) {
                        return `/${configuracion.nombre}/Ui/${options.id}`;
                    },
                    type: "DELETE",
                    dataType: "json",
                    complete: function (e) {
                        var grid = $('#grid').data("kendoGrid");
                        grid.dataSource.read();
                        notificar("Registro eliminado");
                    }
                },
                create: {
                    url: `/${configuracion.nombre}/api/${configuracion.nombre}`,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    type: "POST",
                    complete: function (e) {
                        var grid = $('#grid').data("kendoGrid");
                        grid.dataSource.read();
                        notificar("Registro guardado");
                    }
                },
                parameterMap: function (data, operation) {
                    return kendo.stringify(data);
                }
            },
            schema: {
                model: configuracion.modelo
            }
        },
        autoBind: configuracion.autoBind ? configuracion.autoBind : true,
        columns: myColumns,
        editable: "popup",
        filterable:true,
        pageable: {
            pageSize: 10
        },
        scrollable: false,
        edit: function (e) {
            e.container.data("kendoWindow").title(configuracion.nombre);
        }
    });
    $("#grid").find(".k-grid-toolbar").insertAfter($("#tbcont"));
}

function multilineEditor(container, options) {
    $('<textarea required class="k-textbox" name="' + options.field + '" style="width:100%;height:100%;" />')
        .appendTo(container);
}

