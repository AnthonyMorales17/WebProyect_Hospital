window.onload = function () {
    listarPacientes();
}

async function listarPacientes() {

    objEspecialidad = {
        url: "Paciente/listarPacientes",
        cabeceras: ["Id Paciente", "Nombre", "Apellido", "Fecha de Nacimiento", "Email", "Direccion"],
        propiedades: ["idPaciente", "nombre", "apellido", "fechaNacimiento", "email", "direccion"],
        divContenedorTabla: "divContenedorTabla",
        editar: true,
        eliminar: true,
        propiedadID: "idPaciente",
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

    listarPacientes("frmGuardarPaciente");
}

function GuardarPaciente() {
    let form = document.getElementById("frmGuardarPaciente");
    let frm = new FormData(form);

    fetchPost("Paciente/GuardarPaciente", "json", frm, function (res) {
        if (res == "1") {
            LimpiarPaciente();
            listarPacientes();
            const modal = document.getElementById('modalGuardarPaciente');
            const modalInstance = bootstrap.Modal.getInstance(modal);
            if (modalInstance) {
                modalInstance.hide();
            }

        }
    });

}

// En especialidades.js - Asegúrate de que estas funciones estén bien definidas
function Editar(id) {
    fetchGet("Paciente/recuperarPaciente/?Id=" + id, "json", function (data) {
        setN("idPaciente", data.idPaciente);
        setN("nombre", data.nombre);
        setN("apellido", data.apellido);
        setN("fechaNacimiento", data.fechaNacimiento);
        setN("email", data.email);
        setN("direccion", data.direccion);

        // Mostrar el modal
        var modal = new bootstrap.Modal(document.getElementById('modalGuardaPaciente'));
        modal.show();
    });
}

function Eliminar(id) {
    Confirmacion("¿Está seguro de eliminar este Paciente?", "Esta acción no se puede deshacer",
        function () {
            fetchGet("Especialidad/EliminarPaciente/?Id=" + id, "text", function (data) {
                // Recargar los datos
                listarPacientes();
            });
        }
    );
}


