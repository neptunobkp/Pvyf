$(document).ready(function () {
    bindear();
});

function bindear() {
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: function (options) {
                $.get(`/Opiniones/api/Primeros`,
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