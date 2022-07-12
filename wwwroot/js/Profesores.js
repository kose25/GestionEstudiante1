$.ajaxSetup({
    data: {
        __RequestVerificationToken: document.getElementsByName("__RequestVerificationToken")[0].value
    }
});

var isEdition = false;

function AddModeGrid() {
    $("#TablaProfesor").dxDataGrid("instance").addRow();
    isEdition = false;
}

async function DocumentoUnico(e) {
    var codigo = e.value
    var valido = true;
    if (!isEdition) {
        if (codigo != null && codigo != "") {
            valido = await $.ajax({
                method: "GET",
                url: "/Profesores?handler=VerificarIdentificacion",
                data: { DocumentoUnico: codigo }
            });
        }
    }

    return valido;
}

function SetEditMode() {
    isEdition = false;
}