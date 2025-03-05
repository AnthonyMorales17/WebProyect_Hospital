window.onload = function () {
    listarMedicos();
}

async function listarMedicos() {

    objMedico = {
        url: "Medico/listarMedicos",
        cabeceras: ["Id Medico", "Nombre", "Apellido", "EspecialidadId", "Telefono", "Email"],
        propiedades: ["idMedico", "nombre", "apellido", "especialidadId", "telefono", "email"],
        divContenedorTabla: "divContenedorTabla",
        editar: true,
        eliminar: true,
        propiedadID: "idMedico",
        propiedadName: "nombre",
        entidad: "Medico"
    };
    pintar(objMedico);
}

function filtrarMedico() {
    let forma = document.getElementById("frmBusqueda");
    let frm = new FormData(forma);

    fetchPost("Medico/filtrarMedico", "json", frm, function (res) {
        document.getElementById("divContenedor").innerHTML = generarTabla(res);
    })
}

function BuscarMedico() {
    let form = document.getElementById("frmGuardarMedico");

    let frm = new FormData(form);

    fetchPost("Medico/filtrarMedico", "json", frm, function (res) {
        document.getElementById("divContenedorTabla").innerHTML = generartabla(res)
    })
}

function LimpiarMedico() {
    LimpiarDatos("frmGuardarMedico");
}

function GuardarMedico() {
    let form = document.getElementById("frmGuardarMedico");
    let frm = new FormData(form);
    const nombre = document.getElementById("nombre").value;

    fetchPost("Medico/GuardarMedico", "text", frm, function (res) {
        let resultado = parseInt(res);
        if (resultado > 0) {
            // Mostrar alerta de éxito
            Swal.fire({
                position: "top-end",
                icon: "success",
                title: `El medico ${nombre} ha sido registrado correctamente.`,
                showConfirmButton: false,
                timer: 1500
            });
            LimpiarMedico();
            listarMedicos();

        } else {
            // Mostrar alerta de error
            Swal.fire({
                icon: "error",
                title: "Error",
                text: `No se pudo guardar la información del Medico ${nombre}.`,
                confirmButtonColor: "#3085d6"
            });
            LimpiarMedico();
            listarMedicos();
        }
    });
}

function Editar(id) {
    fetchGet(`Medico/recuperarMedico/?idMedico=${id}`, "json", function (data) {
        LimpiarDatos("frmGuardarMedico");

        document.getElementById("idMedico").value = data.idMedico;
        document.getElementById("nombre").value = data.nombre;
        document.getElementById("apellido").value = data.apellido;
        document.getElementById("especialidadId").value = data.especialidadId;
        document.getElementById("telefono").value = data.telefono;
        document.getElementById("email").value = data.email;

        let modal = new bootstrap.Modal(document.getElementById("MedicoModal"));
        modal.show();
    });
}

function Eliminar(id, nombre) {
    Confirmacion(
        `¿Estás seguro de eliminar la información del Medico ${nombre}?`,
        "¡No podrás revertir esta acción!",
        "warning",
        "Sí, eliminar",
        function () {
            fetchGet("Medico/eliminarMedico/?idMedico=" + id, "text", function (data) {
                listarMedicos("frmGuardarMedico");
            });
        }
    );
}
