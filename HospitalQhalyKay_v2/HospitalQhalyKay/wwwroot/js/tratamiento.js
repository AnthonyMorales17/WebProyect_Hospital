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
        propiedadName: "nombrePaciente",
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
}

function GuardarTratamiento() {
    let form = document.getElementById("frmGuardarTratamiento");
    let frm = new FormData(form);
    frm.delete('nombrePaciente');

    fetchPost("Tratamiento/GuardarTratamiento", "text", frm, function (res) {
        let resultado = parseInt(res);
        if (resultado > 0) {
            // Mostrar alerta de éxito
            Swal.fire({
                position: "top-end",
                icon: "success",
                title: `El tratamiento ha sido registrado correctamente.`,
                showConfirmButton: false,
                timer: 1500
            });
            LimpiarTratamiento();
            listarTratamientos();

        } else {
            // Mostrar alerta de error
            Swal.fire({
                icon: "error",
                title: "Error",
                text: `No se pudo guardar la información del tratamiento.`,
                confirmButtonColor: "#3085d6"
            });
            LimpiarTratamiento();
            listarTratamientos();
        }
    });
}

function Editar(id) {
    fetchGet(`Tratamiento/recuperarTratamiento/?idTratamiento=${id}`, "json", function (data) {
        if (data) {
            LimpiarDatos("frmGuardarTratamiento");

            // Conversión de fecha para input type="date"
            const fechaFormateada = data.fecha
                ? new Date(data.fecha).toISOString().split('T')[0]
                : '';

            const elementos = [
                { id: "idTratamiento", valor: data.idTratamiento },
                { id: "idPaciente", valor: data.idPaciente },
                { id: "nombrePaciente", valor: data.nombrePaciente },
                { id: "descripcion", valor: data.descripcion },
                { id: "fecha", valor: fechaFormateada },
                { id: "costo", valor: data.costo }
            ];

            elementos.forEach(elem => {
                const elemento = document.getElementById(elem.id);
                if (elemento) {
                    elemento.value = elem.valor || '';
                } else {
                    console.warn(`Elemento ${elem.id} no encontrado`);
                }
            });

            let modal = new bootstrap.Modal(document.getElementById("TratamientoModal"));
            modal.show();
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'No se pudo recuperar la información del tratamiento'
            });
        }
    }).catch(error => {
        console.error('Error en recuperar tratamiento:', error);
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Ocurrió un problema al recuperar los datos'
        });
    });
}

function Eliminar(id, nombre) {
    Confirmacion(
        `¿Estás seguro de eliminar el tratamiento ${nombre}?`,
        "¡No podrás revertir esta acción!",
        "warning",
        "Sí, eliminar",
        function () {
            fetchGet("Tratamiento/eliminarTratamiento/?idTratamiento=" + id, "text", function (data) {
                listarTratamientos();
            });
        }
    );
}