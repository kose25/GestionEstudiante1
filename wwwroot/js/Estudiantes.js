$.ajaxSetup({
    data: {
        __RequestVerificationToken: document.getElementsByName("__RequestVerificationToken")[0].value
    }
});

var isEdition = false;

async function Salvar() {
    var form = $("#FormEstudiante").dxForm("instance");

    if (form.validate().isValid) {
        alert("formulario valido")
        var data = form.option("formData");
        console.log(data);
        await EnviarServidor(data);
        await Swal.fire(
            'Estudiante agregado',
            'se agrego correctamente',
            'success'
        )
        $("#FormEstudiante").dxForm("instance").option("readOnly", false);
        isEdition = false;
    }
}

async function EnviarServidor(dataFormulario) {

    try {
        var respuesta = await $.ajax({
            method: "POST",
            url: "/Estudiantes?handler=CrearEstudiante",
            data: dataFormulario
        });
        alert("Usuario agregado")
        $("#FormEstudiante").dxForm("instance").resetValues();
        $("#TablaEstudiantes").dxDataGrid("instance").refresh();

    } catch (error) {
        Swal.fire(
            'Atencion',
            'Ocurrio un errro inesperado',
            'error'
        )
    }
    
}


async function ValidarIdentificacionUnica(e) {
    try {
        if (isEdition) {
            return true;
        }
        var identificacion = e.value;
        var idTipoDocumento = $("#FormEstudiante").dxForm("instance").getEditor("TipoDocumento").option("value").id;
        if (idTipoDocumento>0) {
            var valido = await $.ajax({
                method: "GET",
                url: "/Estudiantes?handler=VerificarIdentificacion",
                data: { Idtipodocumento: idTipoDocumento, documento: identificacion }
            });
        }

        return valido;
    } catch (error) {

    }
}

async function EditarEstudianteFromGrid(e) {

    try {
        var idEstudiante = e.row.data.id;
        var estudiante = await $.ajax({
            method: "GET",
            url: "/Estudiantes?handler=ObtenerEstudiante",
            data: { idEstudiante: idEstudiante }
        });
        if (estudiante == null) {
            Swal.fire(
                'Atencion',
                'Estudiante no encontrado',
                'info'
            )
        } else {
            $("#divgrid").hide();
            $("#divformulario").show();
            $("#FormEstudiante").dxForm("instance").resetValues();
            $("#FormEstudiante").dxForm("instance").option("formData", estudiante);
            //$("#FormEstudiante").dxForm("instance").option("readOnly", false);
            $("#FormEstudiante").dxForm("instance").getEditor("TipoDocumento").option("readOnly", true);
            $("#FormEstudiante").dxForm("instance").getEditor("documento").option("readOnly", true);
            isEdition = true;
        }
        console.log(e);
    } catch (e) {
        Swal.fire(
            'Atencion',
            'Ocurrio un errro inesperado',
            'error'
        )

        console.log(e);
    }
    
}

async function VerEstudianteFromGrid(e) {
    EditarEstudianteFromGrid(e);
    $("#FormEstudiante").dxForm("instance").option("readOnly", true);
    $("#btnsalvar").dxButton("instance").option("disabled", true);
    isEdition = true;
}

function esconderGrid() {
    $("#divgrid").hide();
    $("#divformulario").show();
    $("#FormEstudiante").dxForm("instance").resetValues();
    $("#FormEstudiante").dxForm("instance").option("formData", null);
    $("#FormEstudiante").dxForm("instance").option("readOnly", false);
}

function esconderFormulario() {    
    $("#divformulario").hide();
    $("#divgrid").show();
    $("#FormEstudiante").dxForm("instance").resetValues();
    $("#FormEstudiante").dxForm("instance").option("formData", null);
    //$("#FormEstudiante").dxForm("instance").option("readOnly", false);
    $("#btnsalvar").dxButton("instance").option("disabled", false);
}