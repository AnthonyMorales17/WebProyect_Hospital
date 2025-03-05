window.onload = function () {
    listarCitas();
}

async function listarCitas() {

    objCita = {
        url: "Cita/listarCitas",
        cabeceras: ["Id Cita", "Id Paciente", "Nombre Paciente", "Id Medico", "Nombre Medico", "Fecha/Hora Cita", "Estado Cita"],
        propiedades: ["idCita", "idPaciente", "nombrePaciente", "idMedico", "nombreMedico", "fechaHora", "estado"],
        divContenedorTabla: "divContenedorTabla",
        editar: true,
        eliminar: true,
        propiedadID: "idCita",
        entidad: "Cita"
    };
    pintar(objCita);
}





function filtrarCita() {
    let forma = document.getElementById("frmBusqueda");
    let frm = new FormData(forma);

    fetchPost("Cita/filtrarCita", "json", frm, function (res) {
        document.getElementById("divContenedor").innerHTML = generarTabla(res);
    })
}

function BuscarCita() {
    let form = document.getElementById("frmGuardarCita");

    let frm = new FormData(form);

    fetchPost("Cita/filtrarCita", "json", frm, function (res) {
        document.getElementById("divContenedorTabla").innerHTML = generartabla(res)
    })
}

function LimpiarCita() {
    LimpiarDatos("frmGuardarCita");

    listarCitas("frmGuardarCita");
}

function GuardarCita() {
    let form = document.getElementById("frmGuardarCita");
    let frm = new FormData(form);

    fetchPost("Cita/GuardarCita", "json", frm, function (res) {
        if (res == "1") {
            LimpiarCita();
            listarCitas();
            const modal = document.getElementById('modalGuardarCita');
            const modalInstance = bootstrap.Modal.getInstance(modal);
            if (modalInstance) {
                modalInstance.hide();
            }

        }
    });

}

// En especialidades.js - Asegúrate de que estas funciones estén bien definidas
function Editar(id) {
    fetchGet("Cita/recuperarCita/?Id=" + id, "json", function (data) {

        setN("idCita", data.idCita);
        setN("idPaciente", data.idPaciente);
        setN("idMedico", data.idMedico);
        setN("nombreMedico", data.nombreMedico);
        setN("fechaHora", data.fechaHora);
        setN("estado", data.estado);

        var modal = new bootstrap.Modal(document.getElementById('modalGuardarCita'));
        modal.show();
    });
}

function Eliminar(id) {
    Confirmacion("¿Está seguro de eliminar esta Cita?", "Esta acción no se puede deshacer",
        function () {
            fetchGet("Cita/EliminarCita/?Id=" + id, "text", function (data) {
                // Recargar los datos
                listarCitas();
            });
        }
    );
}