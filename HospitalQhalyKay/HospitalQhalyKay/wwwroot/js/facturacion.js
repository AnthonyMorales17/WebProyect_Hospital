window.onload = function () {
    listarFacturacion();
}

async function listarFacturacion() {

    objFacturacion = {
        url: "Facturacion/listarFacturacion",
        cabeceras: ["Id Facturacion", "Id Paciente", "Nombre Paciente", "Monto Facturacion", "Metodo de Pago", "Fecha de Pago"],
        propiedades: ["idFacturacion", "idPaciente", "nombrePaciente", "monto", "metodoPago", "fechaPago"],
        divContenedorTabla: "divContenedorTabla",
        editar: true,
        eliminar: true,
        propiedadID: "idFacturacion",
        entidad: "Facturacion"
    };
    pintar(objFacturacion);
}


function filtrarFacturacion() {
    let forma = document.getElementById("frmBusqueda");
    let frm = new FormData(forma);

    fetchPost("Facturacion/filtrarFacturacion", "json", frm, function (res) {
        document.getElementById("divContenedor").innerHTML = generarTabla(res);
    })
}

function BuscarFacturacion() {
    let form = document.getElementById("frmGuardarFacturacion");

    let frm = new FormData(form);

    fetchPost("Facturacion/filtrarFacturacion", "json", frm, function (res) {
        document.getElementById("divContenedorTabla").innerHTML = generartabla(res)
    })
}

function LimpiarFacturacion() {
    LimpiarDatos("frmGuardarFacturacion");

    listarFacturacion("frmGuardarFacturacion");
}

function GuardarFacturacion() {
    let form = document.getElementById("frmGuardarFacturacion");
    let frm = new FormData(form);

    fetchPost("Facturacion/GuardarFacturacion", "json", frm, function (res) {
        if (res == "1") {
            LimpiarFacturacion();
            listarFacturacion();
            const modal = document.getElementById('modalGuardarFacturacion');
            const modalInstance = bootstrap.Modal.getInstance(modal);
            if (modalInstance) {
                modalInstance.hide();
            }

        }
    });

}

// En especialidades.js - Asegúrate de que estas funciones estén bien definidas
function Editar(id) {
    fetchGet("Facturacion/recuperarFacturacion/?Id=" + id, "json", function (data) {

        setN("idFacturacion", data.idFacturacion);
        setN("idPaciente", data.idPaciente);
        setN("nombrePaciente", data.nombrePaciente);
        setN("monto", data.monto);
        setN("metodoPago", data.metodoPago);
        setN("fechaPago", data.fechaPago);

        // Mostrar el modal
        var modal = new bootstrap.Modal(document.getElementById('modalGuardarFacturacion'));
        modal.show();
    });
}

function Eliminar(id) {
    Confirmacion("¿Está seguro de eliminar esta Facturacion?", "Esta acción no se puede deshacer",
        function () {
            fetchGet("Facturacion/EliminarFacturacion/?Id=" + id, "text", function (data) {
                // Recargar los datos
                listarFacturacion();
            });
        }
    );
}





