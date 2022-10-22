function ToogleGridSize() {
    if ($("#divGrid").hasClass("col-md-12")) {
        ShowForm();
    } else {
        ExpandGrid();
    }
}

function ShowForm() {
    $(".divAgregar").show("slow");
    $("#divGrid").removeClass("col-md-12");
    $("#divGrid").addClass("col-md-8");
}

function ExpandGrid() {
    $(".divAgregar").hide();
    $("#divGrid").removeClass("col-md-8");
    $("#divGrid").addClass("col-md-12");
}

