window.onload = function () {
    listarTratamientos();
}

async function listarTratamientos() {

    objTratamiento = {
        url: "Tratamiento/listarTratamientos",
        cabeceras: ["Id Tratamiento", "Nombre del Paciente", "Descripcion", "Fecha", "Costo"],
        propiedades: ["idTratamiento", "nombrePaciente", "descripcion", "fecha", "costo"],
        divContenedorTabla: "divContenedorTabla",
        editar: true,
        eliminar: true,
        propiedadID: "idTratamiento",
        entidad: "Tratamiento"
    };
    pintar(objTratamiento);
}

function filtrarTratamiento() {
    let forma = document.getElementById("frmBusqueda");
    let frm = new FormData(forma);

    fetchPost("Tratamiento/filtrarTratamiento", "json", frm, function (res) {
        document.getElementById("divContenedor").innerHTML = generarTabla(res);
    })
}

function BuscarTratamiento() {
    let form = document.getElementById("frmGuardarTratamiento");

    let frm = new FormData(form);

    fetchPost("Tratamiento/filtrarTratamiento", "json", frm, function (res) {
        document.getElementById("divContenedorTabla").innerHTML = generartabla(res)
    })
}

function LimpiarTratamiento() {
    LimpiarDatos("frmGuardarTratamiento");

    listarEspecialidades("frmGuardarTratamiento");
}

function GuardarTratamiento() {
    let form = document.getElementById("frmGuardarTratamiento");
    let frm = new FormData(form);

    fetchPost("Tratamiento/GuardarTratamiento", "json", frm, function (res) {
        if (res == "1") {
            LimpiarTratamiento();
            listarTratamientos();
            const modal = document.getElementById('modalGuardarTratamiento');
            const modalInstance = bootstrap.Modal.getInstance(modal);
            if (modalInstance) {
                modalInstance.hide();
            }

        }
    });

}

// En especialidades.js - Asegúrate de que estas funciones estén bien definidas
function Editar(id) {
    fetchGet("Tratamiento/recuperarTratamiento/?Id=" + id, "json", function (data) {
        setN("idTratamiento", data.idTratamiento);
        setN("idPaciente"), data.idPaciente;
        setN("nombrePaciente", data.nombrePaciente);
        setN("descripcion", data.descripcion);
        setN("fecha", data.fecha);
        setN("costo", data.costo);


        // Mostrar el modal
        var modal = new bootstrap.Modal(document.getElementById('modalGuardarTratamiento'));
        modal.show();
    });
}

function Eliminar(id) {
    Confirmacion("¿Está seguro de eliminar este Tratamiento?", "Esta acción no se puede deshacer",
        function () {
            fetchGet("Tratamiento/EliminarTratamiento/?Id=" + id, "text", function (data) {
                // Recargar los datos
                listarTratamientos();
            });
        }
    );
}