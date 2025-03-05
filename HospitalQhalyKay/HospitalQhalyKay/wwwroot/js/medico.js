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

    listarEspecialidades("frmGuardarMedico");
}

function GuardarMedico() {
    let form = document.getElementById("frmGuardarMedico");
    let frm = new FormData(form);

    fetchPost("Medico/GuardarMedico", "json", frm, function (res) {
        if (res == "1") {
            LimpiarMedico();
            listarMedicos();
            const modal = document.getElementById('modalGuardarMedico');
            const modalInstance = bootstrap.Modal.getInstance(modal);
            if (modalInstance) {
                modalInstance.hide();
            }

        }
    });

}

// En especialidades.js - Asegúrate de que estas funciones estén bien definidas
function Editar(id) {
    fetchGet("Medico/recuperarMedico/?Id=" + id, "json", function (data) {
        setN("idMedico", data.idMedico);
        setN("Nombre", data.Nombre);
        setN("Apellido", data.Apellido);
        setN("EspecialidadId", data.EspecialidadId);
        setN("Telefono", data.Telefono);
        setN("Email", data.Email);
        // Mostrar el modal
        var modal = new bootstrap.Modal(document.getElementById('modalGuardarMedico'));
        modal.show();
    });
}

function Eliminar(id) {
    Confirmacion("¿Está seguro de eliminar esta medico?", "Esta acción no se puede deshacer",
        function () {
            fetchGet("Medico/EliminarMedico/?Id=" + id, "text", function (data) {
                // Recargar los datos
                listarMedicos();
            });
        }
    );
}
