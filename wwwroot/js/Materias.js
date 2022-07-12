$.ajaxSetup({
    data: {
        __RequestVerificationToken: document.getElementsByName("__RequestVerificationToken")[0].value
    }
});

var isEdition = false;

function AddModeGrid() {
    $("#TablaMateria").dxDataGrid("instance").addRow();
    isEdition = false;
}

async function CodigoUnico(e) {

    var codigo = e.value
    var valido = true;
    if (!isEdition) {
        if (codigo != null && codigo != "") {
            valido = await $.ajax({
                method: "GET",
                url: "/Materias?handler=VerificarCodigoUnico",
                data: { Materiacode: codigo }
            });
        }
    }   

    return valido;
}

function SetEditMode() {
    isEdition = true;
    $("#TablaMateria").dxDataGrid("instance").columnOption("codigo", "allowEditing", false);
}