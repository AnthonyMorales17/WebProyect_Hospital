window.onload = function () {
    listarPacientes();
}

async function listarPacientes() {

    objEspecialidad = {
        url: "Paciente/listarPacientes",
        cabeceras: ["Id Paciente", "Nombre", "Apellido", "Fecha de Nacimiento", "Telefono", "Email", "Direccion"],
        propiedades: ["idPaciente", "nombre", "apellido", "fechaNacimiento", "telefono" ,"email", "direccion"],
        divContenedorTabla: "divContenedorTabla",
        editar: true,
        eliminar: true,
        propiedadID: "idPaciente",
        propiedadName: "nombre",
        entidad: "Paciente"
    };
    pintar(objEspecialidad);
}



function filtrarPaciente() {
    let forma = document.getElementById("frmBusqueda");
    let frm = new FormData(forma);

    fetchPost("Paciente/filtrarPaciente", "json", frm, function (res) {
        document.getElementById("divContenedor").innerHTML = generarTabla(res);
    })
}

function BuscarPaciente() {
    let form = document.getElementById("frmGuardarPaciente");

    let frm = new FormData(form);

    fetchPost("Paciente/filtrarPaciente", "json", frm, function (res) {
        document.getElementById("divContenedorTabla").innerHTML = generartabla(res)
    })
}

function LimpiarPaciente() {
    LimpiarDatos("frmGuardarPaciente");
}

function GuardarPaciente() {
    let form = document.getElementById("frmGuardarPaciente");
    let frm = new FormData(form);
    const nombre = document.getElementById("nombre").value;

    fetchPost("Paciente/GuardarPaciente", "text", frm, function (res) {
        let resultado = parseInt(res);
        if (resultado > 0) {
            // Mostrar alerta de éxito
            Swal.fire({
                position: "top-end",
                icon: "success",
                title: `El paciente ${nombre} ha sido registrado correctamente.`,
                showConfirmButton: false,
                timer: 1500
            });
            LimpiarPaciente();
            listarPacientes();

        } else {
            // Mostrar alerta de error
            Swal.fire({
                icon: "error",
                title: "Error",
                text: `No se pudo guardar la información del paciente ${nombre}.`,
                confirmButtonColor: "#3085d6"
            });
            LimpiarPaciente();
            listarPacientes();
        }
    });
}

function Editar(id) {
    fetchGet(`Paciente/recuperarPaciente/?idPaciente=${id}`, "json", function (data) {
        LimpiarDatos("frmGuardarPaciente");

        document.getElementById("idPaciente").value = data.idPaciente;
        document.getElementById("nombre").value = data.nombre;
        document.getElementById("apellido").value = data.apellido;
        document.getElementById("fechaNacimiento").value = data.fechaNacimiento;
        document.getElementById("telefono").value = data.telefono;
        document.getElementById("email").value = data.email;
        document.getElementById("direccion").value = data.direccion;

        let modal = new bootstrap.Modal(document.getElementById("PacienteModal"));
        modal.show();
    });
}

function Eliminar(id, nombre) {
    Confirmacion(
        `¿Estás seguro de eliminar la información del paciente ${nombre}?`,
        "¡No podrás revertir esta acción!",
        "warning",
        "Sí, eliminar",
        function () {
            fetchGet("Paciente/eliminarPaciente/?idPaciente=" + id, "text", function (data) {
                listarPacientes("frmGuardarPaciente");
            });
        }
    );
}

