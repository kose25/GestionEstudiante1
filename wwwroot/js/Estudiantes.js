$.ajaxSetup({
    data: {
        __RequestVerificationToken: document.getElementsByName("__RequestVerificationToken")[0].value
    }
});


async function Salvar() {
    var form = $("#FormEstudiante").dxForm("instance");

    if (form.validate().isValid) {
        alert("formulario valido")
        var data = form.option("formData");
        console.log(data);
        await EnviarServidor(data);
    }
}

async function EnviarServidor(dataFormulario) {

    try {
        var respuesta = await $.ajax({
            method: "POST",
            url: "/Estudiantes?handle=CrearEstudiante",
            data: dataFormulario
        });
        alert("Usuario agregado")
        $("#FormEstudiante").dxForm("instance").resetValues();

    } catch (error) {
        alert("algo salio mal");
    }
    
}