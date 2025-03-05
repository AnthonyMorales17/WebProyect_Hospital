window.onload = function () {
    listarEspecialidades();
}

let objEspecialidad;

async function listarEspecialidades() {

    objEspecialidad = {
        url: "Especialidad/listarEspecialidades",
        cabeceras: ["Id Especialidad", "Nombre"],
        propiedades: ["idEspecialidad", "nombre"],
        divContenedorTabla: "divContenedorTabla",
        editar: true,
        eliminar: true,
        propiedadID: "idEspecialidad",
        propiedadName: "nombre",
        entidad: "Especialidad"
    };
    pintar(objEspecialidad);
}

function filtrarEspecialidad() {
    let forma = document.getElementById("frmGuardarEspecialidades");
    let frm = new FormData(forma);

    fetchPost("Especialidad/filtrarEspecialidad", "json", frm, function (res) {
        document.getElementById("divContenedor").innerHTML = generarTabla(res);
    })
}

function BuscarEspecialidad() {
    let form = document.getElementById("frmGuardarEspecialidades");

    let frm = new FormData(form);

    fetchPost("Especialidad/filtrarEspecialidad", "json", frm, function (res) {
        document.getElementById("divContenedorTabla").innerHTML = generartabla(res)
    })
}

function LimpiarEspecialidad() {
    LimpiarDatos("frmGuardarEspecialidades");
}

function GuardarEspecialidad() {
    let form = document.getElementById("frmGuardarEspecialidades");
    let frm = new FormData(form);
    const nombre = document.getElementById("nombre").value;

    fetchPost("Especialidad/GuardarEspecialidad", "text", frm, function (res) {
        let resultado = parseInt(res);
        if (resultado > 0) {
            // Mostrar alerta de éxito
            Swal.fire({
                position: "top-end",
                icon: "success",
                title: `Especialidad ${nombre} guardada correctamente.`,
                showConfirmButton: false,
                timer: 1500
            });
            LimpiarEspecialidad();
            listarEspecialidades();

        } else {
            // Mostrar alerta de error
            Swal.fire({
                icon: "error",
                title: "Error",
                text: `No se pudo guardar la Especialidad de ${nombre}.`,
                confirmButtonColor: "#3085d6"
            });
            LimpiarEspecialidad();
            listarEspecialidades();
        }
    });
}

function Editar(id) {
    fetchGet(`Especialidad/recuperarEspecialidad/?idEspecialidad=${id}`, "json", function (data) {
        LimpiarDatos("frmGuardarEspecialidades");

        document.getElementById("idEspecialidad").value = data.idEspecialidad;
        document.getElementById("nombre").value = data.nombre;

        // Mostrar el modal manualmente usando Bootstrap 5
        let modal = new bootstrap.Modal(document.getElementById("EspecialidadModal"));
        modal.show();
    });   
}

function Eliminar(id, nombre) {
    Confirmacion(
        `¿Estás seguro de eliminar la Especialidad de ${nombre}?`,
        "¡No podrás revertir esta acción!",
        "warning",
        "Sí, eliminar",
        function () {
            fetchGet("Especialidad/eliminarEspecialidad/?idEspecialidad=" + id, "text", function (data) {
                listarEspecialidades("frmGuardarEspecialidades");
            });
        }
    );
}
