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
        propiedadName: "nombrePaciente",
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

function GuardarFacturacion() {
    let form = document.getElementById("frmGuardarFacturacion");
    let frm = new FormData(form);
    const nombre = document.getElementById("nombrePaciente").value;

    fetchPost("Facturacion/GuardarFacturacion", "text", frm, function (res) {
        let resultado = parseInt(res);
        if (resultado > 0) {
            // Mostrar alerta de éxito
            Swal.fire({
                position: "top-end",
                icon: "success",
                title: `La factura de ${nombre} ha sido registrado correctamente.`,
                showConfirmButton: false,
                timer: 1500
            });
            LimpiarFacturacion();
            listarFacturacion();

        } else {
            // Mostrar alerta de error
            Swal.fire({
                icon: "error",
                title: "Error",
                text: `No se pudo guardar la factura de ${nombre}.`,
                confirmButtonColor: "#3085d6"
            });
            LimpiarFacturacion();
            listarFacturacion();
        }
    });
}

function Editar(id) {
    fetchGet(`Facturacion/recuperarFacturacion/?idFacturacion=${id}`, "json", function (data) {
        LimpiarDatos("frmGuardarFacturacion");

        document.getElementById("idFacturacion").value = data.idFacturacion;
        document.getElementById("idPaciente").value = data.idPaciente;
        document.getElementById("nombrePaciente").value = data.nombrePaciente;
        document.getElementById("monto").value = data.monto;
        document.getElementById("metodoPago").value = data.metodoPago;
        document.getElementById("fechaPago").value = data.fechaPago;

        let modal = new bootstrap.Modal(document.getElementById("FacturacionModal"));
        modal.show();
    });
}
function Eliminar(id, nombre) {
    Confirmacion(
        `¿Estás seguro de eliminar la facturacion de ${nombre}?`,
        "¡No podrás revertir esta acción!",
        "warning",
        "Sí, eliminar",
        function () {
            fetchGet("Facturacion/eliminarFacturacion/?idFacturacion=" + id, "text", function (data) {
                listarFacturacion();
            });
        }
    );
}





