function get(idControl) {
    return document.getElementById(idControl).value;
}

function set(idControl, valor) {
    document.getElementById(idControl).value = valor;
}

function setN(namecontrol, valor) {
    document.getElementsByName(namecontrol)[0].value = valor;
}

function getN(namecontrol) {
    return document.getElementsByName(namecontrol)[0].value;
}
function LimpiarDatos(idFormulario) {
    let elementosName = document.querySelectorAll("#" + idFormulario + " [name]");
    let elementoActual;
    let elementoName;

    for (let i = 0; i < elementosName.length; i++) {
        elementoActual = elementosName[i];
        elementoName = elementoActual.name;
        setN(elementoName, "");
    }
}

async function fetchGet(url, tiporespuesta, callback) {
    try {

        //http://
        let urlCompleta = window.location.protocol + "//" + window.location.host + "/" + url;

        let res = await fetch(urlCompleta);
        if (tiporespuesta == "json")
            res = await res.json();
        else if (tiporespuesta == "text")
            res = await res.text();

        //JSON objeto
        callback(res);

    } catch (e) {
        alert(e);
    }
}

async function fetchPost(url, tiporespuesta, frm, callback) {
    try {
        let urlCompleta = window.location.protocol + "//" + window.location.host + "/" + url;

        let res = await fetch(urlCompleta, {
            method: "POST",
            body: frm
        });

        if (tiporespuesta == "json")
            res = await res.json();
        else if (tiporespuesta == "text")
            res = await res.text();
        //JSON objeto
        callback(res);

    } catch (e) {
        alert("Ocurrio un problema en POST");
    }
}

let objConfiguracionGlobal;

//{url: "":, cabeceras:[], nombrepropiedades:[]}
function pintar(objConfiguracion) {
    objConfiguracionGlobal = objConfiguracion;

    if (objConfiguracionGlobal.divContenedorTabla == undefined) {
        objConfiguracionGlobal.divContenedorTabla = "divContenedorTabla";
    }
    if (objConfiguracionGlobal.editar == undefined) {
        objConfiguracionGlobal.editar = false;
    }
    if (objConfiguracionGlobal.eliminar == undefined) {
        objConfiguracionGlobal.eliminar = false;
    }
    if (objConfiguracionGlobal.propiedadID == undefined) {
        objConfiguracionGlobal.propiedadID = "";
    }

    fetchGet(objConfiguracion.url, "json", function (res) {
        let contenido = "";
        contenido += "<div id='divContenedorTabla'>";
        contenido += generartabla(res, objConfiguracion.entidad);
        document.getElementById("divTabla").innerHTML = contenido;
        AgStarTable();
    });



}

function AgStarTable() {
    new DataTable("#idTabla", {
        language: {
            "decimal": "",
            "emptyTable": "No hay datos disponible",
            "info": "Mostrando Inicio a Fin de TOTAL entradas",
            "infoEmpty": "Mostrando 0 a 0 de 0 entradas",
            "infoFiltered": "(filtrado de MAX entradas en total)",
            "infoPostFix": "",
            "thousands": ",",
            "cellDataType": 'text',
            "lengthMenu": "Mostrar entradas",
            "loadingRecords": "Cargando...",
            "processing": "",
            "search": "Buscar:",
            "zeroRecords": "No se encontraron registros coincidentes",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        }
    });
}

function generartabla(res, entidad) {

    let contenido = "";


    //["Id Tipo Medicamento", "Nombre", "Descripcion"]
    let cabeceras = objConfiguracionGlobal.cabeceras;
    let nombrepropiedades = objConfiguracionGlobal.propiedades;

    contenido += `<button type="button" class="btn btn-success mt-3" data-bs-toggle="modal" data-bs-target="#${entidad}Modal">
                    Crear ${entidad}
                  </button>`;

    contenido += "<table id='idTabla' class='table' >";

    contenido += "<thead>";
    contenido += "<tr>";
    for (let i = 0; i < cabeceras.length; i++) {
        contenido += "<td>" + cabeceras[i] + "</td>";
    }

    if (objConfiguracionGlobal.editar == true || objConfiguracionGlobal.eliminar == true) {
        contenido += "<td>Operaciones</td>";

    }
    contenido += "</tr>";
    contenido += "</thead>";


    let nroRegistros = res.length;
    let obj;
    let propiedadActual;

    contenido += "<tbody>";

    for (let i = 0; i < nroRegistros; i++) {
        obj = res[i];
        contenido += "<tr>";
        for (let j = 0; j < nombrepropiedades.length; j++) {
            propiedadActual = nombrepropiedades[j]
            contenido += "<td>" + obj[propiedadActual] + "</td>";

        }
        if (objConfiguracionGlobal.editar == true || objConfiguracionGlobal.eliminar == true) {

            let propiedadID = objConfiguracionGlobal.propiedadID;

            contenido += "<td>";

            if (objConfiguracionGlobal.editar == true) {
                contenido += `<i onclick="Editar(${obj[propiedadID]})" class="btn btn-primary"><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16">
                  <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
                  <path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5z"/>
                </svg></i > `
            }
            if (objConfiguracionGlobal.eliminar == true) {
                contenido += `<i onclick="Eliminar(${obj[propiedadID]})" class="btn btn-danger"><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash" viewBox="0 0 16 16">
                <path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5m2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5m3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0z"/>
                <path d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1zM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4zM2.5 3h11V2h-11z"/></svg></i>`
            }
            contenido += "</td>";
        }

        contenido += "</tr>";
    }

    contenido += "</tbody>";
    contenido += "</table>";
    return contenido;
}

function Confirmacion(titulo = "Confirmación", texto = "¿Desea guardar los cambios?", icono = "warning", confirmButtonText = "Sí", callback) {
    Swal.fire({
        title: titulo,
        text: texto,
        icon: icono,
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: confirmButtonText,
        cancelButtonText: "No"
    }).then((result) => {
        if (result.isConfirmed) {
            callback();
        }
    });
}

