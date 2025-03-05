# Proyecto de Gestión de Citas Médicas

## Introducción

Este proyecto es una aplicación diseñada para la gestión eficiente de citas médicas, permitiendo a los usuarios programar, modificar y cancelar citas con médicos de distintas especialidades. La aplicación está desarrollada en **C#**, utilizando **SQL Server** como base de datos y **JavaScript** para mejorar la experiencia en la interfaz de usuario.

El sistema sigue una arquitectura en capas, lo que facilita la escalabilidad, mantenimiento y reutilización del código. Estas capas son:

- **Capa de Entidad (CapaEntidad)**: Define las estructuras de datos que representan los modelos de la base de datos.
- **Capa de Datos (CapaDatos)**: Maneja la conexión con SQL Server y ejecuta consultas y procedimientos almacenados.
- **Capa de Negocio (CapaNegocio)**: Implementa la lógica de negocio y valida las operaciones antes de interactuar con la base de datos.
- **Capa de Presentación**: Es la interfaz de usuario, donde los usuarios pueden gestionar citas a través de un sistema web con JavaScript y HTML.

La aplicación soporta múltiples funcionalidades clave, tales como:
- **Registro de citas médicas**: Permite a los pacientes agendar citas con los médicos disponibles.
- **Gestión de pacientes y médicos**: Administración de la información personal de los pacientes y médicos, incluyendo especialidades.
- **Facturación de consultas**: Registro de pagos y métodos de facturación asociados a cada cita médica.
- **Módulo de especialidades médicas**: Organización y categorización de los médicos según su especialidad.
- **Tratamientos médicos**: Seguimiento de tratamientos realizados a los pacientes.

El objetivo principal de este sistema es mejorar la administración de citas médicas y optimizar la relación entre pacientes y profesionales de la salud, asegurando un servicio eficiente y bien estructurado.


## Capa de Datos (CapaDatos)

La **Capa de Datos** es responsable de la comunicación con la base de datos SQL Server. Esta capa permite realizar operaciones **CRUD (Crear, Leer, Actualizar y Eliminar)** a través de procedimientos almacenados.

### 1. Funcionalidad

| Función              | Descripción |
|----------------------|-------------|
| `listarCitas()`     | Obtiene todas las citas registradas en la base de datos. |
| `recuperarCita(id)` | Recupera la información de una cita específica según su ID. |
| `FiltrarCita(obj)`  | Filtra las citas según criterios como fecha o paciente. |
| `GuardarCita(obj)`  | Inserta o actualiza una cita en la base de datos. |
| `EliminarCita(id)`  | Elimina una cita específica según su ID. |

### 2. Conexión a la Base de Datos

La conexión a SQL Server se gestiona a través de la clase `ConexionDAL`, que obtiene la cadena de conexión desde el archivo `appsettings.json`. 

```csharp
public class ConexionDAL {
    public string cadenaConexion { get; set; }
    public ConexionDAL() {
        IConfigurationBuilder builder = new ConfigurationBuilder();
        builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
        var root = builder.Build();
        cadenaConexion = root.GetConnectionString("cn");
    }
}
```

### 3. Clases y Métodos Claves

Cada entidad tiene una clase DAL correspondiente que maneja la interacción con la base de datos. 

#### a) Gestión de Citas (`CitaDAL`)

| Método              | Función |
|----------------------|---------|
| `listarCitas()`     | Obtiene todas las citas desde la BD. |
| `recuperarCita(id)` | Retorna una cita según su ID. |
| `FiltrarCita(obj)`  | Filtra citas por paciente o médico. |
| `GuardarCita(obj)`  | Guarda una nueva cita o actualiza una existente. |
| `EliminarCita(id)`  | Elimina una cita según el ID. |

#### b) Gestión de Pacientes (`PacienteDAL`)

| Método               | Función |
|----------------------|---------|
| `listarPacientes()`  | Obtiene todos los pacientes registrados. |
| `recuperarPaciente(id)` | Retorna un paciente según su ID. |
| `FiltrarPaciente(obj)` | Filtra pacientes según nombre o email. |
| `GuardarPaciente(obj)` | Registra un nuevo paciente o actualiza datos existentes. |
| `EliminarPaciente(id)` | Elimina un paciente de la base de datos. |

#### c) Gestión de Médicos (`MedicoDAL`)

| Método            | Función |
|-------------------|---------|
| `listarMedicos()` | Obtiene todos los médicos registrados. |
| `recuperarMedico(id)` | Retorna un médico según su ID. |
| `FiltrarMedico(obj)` | Filtra médicos por especialidad o nombre. |
| `GuardarMedico(obj)` | Registra un nuevo médico o actualiza datos existentes. |
| `EliminarMedico(id)` | Elimina un médico de la base de datos. |

### 4. Procedimientos Almacenados en SQL Server

Los métodos en la Capa de Datos interactúan con la base de datos mediante procedimientos almacenados. Ejemplo:

```sql
CREATE PROCEDURE uspListarCitas
AS
BEGIN
    SELECT idCita, idPaciente, nombrePaciente, idMedico, nombreMedico, fechaHora, estado
    FROM Citas
END
```

Este procedimiento es ejecutado en `CitaDAL` de la siguiente forma:

```csharp
public List<CitaCLS> listarCitas() {
    List<CitaCLS> listarCitas = new List<CitaCLS>();
    using (SqlConnection cn = new SqlConnection(cadenaConexion)) {
        cn.Open();
        using (SqlCommand cmd = new SqlCommand("uspListarCitas", cn)) {
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read()) {
                CitaCLS oCita = new CitaCLS() {
                    idCita = dr.GetInt32(0),
                    idPaciente = dr.GetInt32(1),
                    nombrePaciente = dr.GetString(2),
                    idMedico = dr.GetInt32(3),
                    nombreMedico = dr.GetString(4),
                    fechaHora = dr.GetDateTime(5),
                    estado = dr.GetString(6)
                };
                listarCitas.Add(oCita);
            }
        }
    }
    return listarCitas;
}
```



## Capa de Entidad (CapaEntidad)

La **Capa de Entidad** es la base del modelo de datos del sistema. Define las estructuras de datos que representan los objetos del negocio y su relación con la base de datos. Cada entidad se representa como una clase con atributos que reflejan los campos de la base de datos.

### 1. Definición de Entidades

| Entidad            | Descripción |
|--------------------|-------------|
| `CitaCLS`         | Representa una cita médica programada. |
| `PacienteCLS`     | Contiene la información de los pacientes. |
| `MedicoCLS`       | Almacena datos de los médicos, incluyendo su especialidad. |
| `EspecialidadCLS` | Define las distintas especialidades médicas disponibles. |
| `TratamientoCLS`  | Registra los tratamientos aplicados a los pacientes. |
| `FacturacionCLS`  | Administra los pagos y facturación de citas médicas. |

### 2. Atributos Clave de Cada Entidad

#### a) `CitaCLS` (Citas médicas)
```plaintext
- idCita (int)
- idPaciente (int)
- nombrePaciente (string)
- idMedico (int)
- nombreMedico (string)
- fechaHora (DateTime)
- estado (string)
```

#### b) `PacienteCLS` (Pacientes)
```plaintext
- idPaciente (int)
- nombre (string)
- apellido (string)
- fechaNacimiento (DateOnly)
- email (string)
- direccion (string)
```

#### c) `MedicoCLS` (Médicos)
```plaintext
- idMedico (int)
- Nombre (string)
- Apellido (string)
- EspecialidadId (int)
- Telefono (string)
- Email (string)
```

#### d) `EspecialidadCLS` (Especialidades médicas)
```plaintext
- idEspecialidad (int)
- nombre (string)
```

#### e) `TratamientoCLS` (Tratamientos médicos)
```plaintext
- idTratamiento (int)
- idPaciente (int)
- nombrePaciente (string)
- descripcion (string)
- fecha (DateOnly)
- costo (float)
```

#### f) `FacturacionCLS` (Facturación y pagos)
```plaintext
- idFacturacion (int)
- idPaciente (int)
- nombrePaciente (string)
- monto (float)
- metodoPago (string)
- fechaPago (DateOnly)
```

### 3. Importancia de la Capa de Entidad

La Capa de Entidad es esencial porque:
- **Define la estructura del sistema** en términos de objetos de negocio.
- **Facilita la transferencia de datos** entre capas sin exponer detalles de implementación.
- **Mejora la organización y el mantenimiento del código** al centralizar la definición de los modelos de datos.
- **Asegura la coherencia en la manipulación de datos**, evitando inconsistencias.

### 4. Uso de la Capa de Entidad en el Sistema

Cada entidad es utilizada en diferentes partes del sistema:
- **Capa de Datos**: Mapea los datos recuperados de SQL Server a objetos de estas clases.
- **Capa de Negocio**: Procesa la lógica de negocio utilizando objetos de entidad.
- **Capa de Presentación**: Interactúa con las entidades para mostrar información en la interfaz de usuario.


## Capa de Negocio (CapaNegocio)

La **Capa de Negocio** es la encargada de procesar la lógica empresarial del sistema. Actúa como intermediaria entre la **Capa de Datos** y la **Capa de Presentación**, asegurando que se cumplan las reglas del negocio antes de interactuar con la base de datos.

### 1. Funcionalidad

| Función               | Descripción |
|----------------------|-------------|
| `listarCitas()`     | Obtiene todas las citas aplicando reglas de negocio. |
| `filtrarCita(obj)`  | Aplica filtros específicos antes de devolver los resultados. |
| `GuardarCita(obj)`  | Valida los datos antes de guardarlos en la base de datos. |
| `recuperarCita(id)` | Recupera información asegurando integridad de los datos. |
| `EliminarCita(id)`  | Verifica dependencias antes de eliminar una cita. |

### 2. Clases y Métodos Claves

Cada entidad tiene una clase de negocio correspondiente que implementa la lógica antes de llamar a la Capa de Datos.

#### a) Gestión de Citas (`CitaBL`)

| Método              | Función |
|----------------------|---------|
| `listarCitas()`     | Recupera todas las citas aplicando validaciones. |
| `filtrarCita(obj)`  | Aplica reglas de negocio para filtrar citas. |
| `GuardarCita(obj)`  | Valida que la cita no se cruce con otra antes de guardarla. |
| `recuperarCita(id)` | Obtiene una cita garantizando integridad de datos. |
| `EliminarCita(id)`  | Revisa si la cita puede ser eliminada antes de proceder. |

#### b) Gestión de Pacientes (`PacienteBL`)

| Método               | Función |
|----------------------|---------|
| `listarPacientes()`  | Obtiene todos los pacientes registrados. |
| `filtrarPaciente(obj)` | Aplica validaciones en la búsqueda de pacientes. |
| `GuardarPaciente(obj)` | Verifica que los datos sean correctos antes de guardarlos. |
| `recuperarPaciente(id)` | Recupera un paciente asegurando coherencia en los datos. |
| `EliminarPaciente(id)` | Valida que el paciente no tenga citas activas antes de eliminarlo. |

#### c) Gestión de Médicos (`MedicoBL`)

| Método            | Función |
|-------------------|---------|
| `listarMedicos()` | Obtiene todos los médicos registrados. |
| `filtrarMedico(obj)` | Aplica reglas de negocio antes de devolver los resultados. |
| `GuardarMedico(obj)` | Valida la información antes de registrar un médico. |
| `recuperarMedico(id)` | Recupera un médico garantizando integridad de datos. |
| `EliminarMedico(id)` | Verifica que el médico no tenga citas pendientes antes de eliminarlo. |

### 3. Validaciones y Reglas de Negocio

La **Capa de Negocio** es responsable de aplicar reglas para garantizar el correcto funcionamiento del sistema. Algunas validaciones clave incluyen:

- **Evitar citas duplicadas**: Se verifica que un paciente no tenga una cita en la misma fecha y hora con el mismo médico.
- **Validar datos antes de guardarlos**: Se revisan campos obligatorios antes de enviar la información a la base de datos.
- **Control de eliminación de datos**: Antes de eliminar un médico o paciente, se revisa que no tenga citas pendientes.
- **Formato correcto de fechas y montos**: Se asegura que las fechas sean válidas y los montos estén en un rango aceptable.

### 4. Uso de la Capa de Negocio en el Sistema

- **Recibe datos desde la Capa de Presentación** y aplica reglas de validación.
- **Llama a la Capa de Datos** únicamente si los datos son correctos.
- **Devuelve los resultados procesados** a la Capa de Presentación.
- **Centraliza la lógica del sistema**, evitando que esté dispersa en diferentes capas.



## Capa de Presentación (JavaScript y Frontend)

La **Capa de Presentación** es la interfaz con la que los usuarios interactúan para gestionar citas médicas, pacientes, médicos y facturación. Utiliza **JavaScript** para la manipulación del DOM y la comunicación con el backend mediante llamadas **fetch** a los controladores en C#.

### 1. Funcionalidad

| Archivo JavaScript | Descripción |
|--------------------|-------------|
| `citas.js`        | Maneja la gestión de citas médicas. |
| `pacientes.js`    | Administra la información de pacientes. |
| `medicos.js`      | Gestiona los datos de médicos. |
| `facturacion.js`  | Controla el registro y consulta de facturación. |
| `especialidades.js` | Administra las especialidades médicas. |
| `generic.js`        | Contiene funciones reutilizables para validaciones y peticiones HTTP. |

### 2. Métodos y Funcionalidad Clave

#### a) Gestión de Citas (`citas.js`)

| Método              | Descripción |
|----------------------|-------------|
| `listarCitas()`     | Obtiene y muestra todas las citas médicas en la interfaz. |
| `filtrarCita()`     | Permite buscar citas médicas aplicando filtros. |
| `GuardarCita()`     | Envia los datos de una nueva cita al backend. |
| `EditarCita(id)`    | Recupera los datos de una cita y permite editarlos. |
| `EliminarCita(id)`  | Envía una solicitud para eliminar una cita específica. |

#### b) Gestión de Pacientes (`pacientes.js`)

| Método              | Descripción |
|----------------------|-------------|
| `listarPacientes()` | Carga y muestra la lista de pacientes en la interfaz. |
| `GuardarPaciente()` | Guarda un nuevo paciente o edita uno existente. |
| `EditarPaciente(id)`| Recupera y permite modificar la información de un paciente. |
| `EliminarPaciente(id)` | Elimina un paciente verificando dependencias. |

#### c) Gestión de Médicos (`medicos.js`)

| Método              | Descripción |
|----------------------|-------------|
| `listarMedicos()`   | Muestra la lista de médicos disponibles. |
| `GuardarMedico()`   | Registra o edita un médico en la base de datos. |
| `EditarMedico(id)`  | Obtiene los datos de un médico para su edición. |
| `EliminarMedico(id)` | Borra un médico después de verificar que no tenga citas asignadas. |

### 3. Comunicación con el Backend

Las funciones de la capa de presentación utilizan `fetch` para interactuar con la API del backend.

Ejemplo de una petición `fetch` para obtener citas médicas:

```javascript
async function listarCitas() {
    let response = await fetch("Cita/listarCitas");
    let data = await response.json();
    document.getElementById("divContenedorTabla").innerHTML = generarTabla(data);
}
```

Ejemplo de una petición `fetch` para guardar una cita:

```javascript
async function GuardarCita() {
    let form = document.getElementById("frmGuardarCita");
    let frm = new FormData(form);

    let response = await fetch("Cita/GuardarCita", {
        method: "POST",
        body: frm
    });

    let result = await response.text();
    if (result == "1") {
        alert("Cita guardada correctamente");
        listarCitas();
    }
}
```

### 4. Funciones Auxiliares (`generic.js`)

El archivo `utils.js` contiene funciones reutilizables, como:

- **`fetchGet(url, tipo, callback)`**: Realiza peticiones `GET` y ejecuta un callback con los datos obtenidos.
- **`fetchPost(url, tipo, frm, callback)`**: Envía datos con `POST` y maneja la respuesta.
- **`generarTabla(res, entidad)`**: Genera dinámicamente una tabla HTML con los datos obtenidos del backend.
- **`Confirmacion(mensaje, detalle, callback)`**: Muestra un mensaje de confirmación antes de ejecutar una acción.

Ejemplo de `fetchPost`:

```javascript
async function fetchPost(url, tipo, frm, callback) {
    let response = await fetch(url, {
        method: "POST",
        body: frm
    });
    let result = tipo === "json" ? await response.json() : await response.text();
    callback(result);
}
```

## Controladores y Vistas

La capa de controladores y vistas en este sistema MVC (Modelo-Vista-Controlador) maneja la interacción entre el usuario y la lógica de negocio. 

### 1. Controladores (Controllers)
Los controladores actúan como intermediarios entre la **Capa de Presentación** y la **Capa de Negocio**, recibiendo peticiones del frontend, procesándolas y enviando las respuestas adecuadas.

| Controlador        | Funcionalidad |
|--------------------|--------------|
| `CitaController`  | Gestiona la creación, modificación, eliminación y listado de citas. |
| `PacienteController` | Administra los datos de los pacientes. |
| `MedicoController` | Maneja la información de los médicos. |
| `EspecialidadController` | Gestiona las especialidades médicas disponibles. |
| `FacturacionController` | Controla la facturación y los pagos de las citas. |

Ejemplo de un controlador en C#:
```csharp
[HttpPost]
public IActionResult GuardarCita(CitaCLS cita) {
    if (!ModelState.IsValid) {
        return BadRequest("Datos inválidos");
    }
    CitaBL obj = new CitaBL();
    int respuesta = obj.GuardarCita(cita);
    return Ok(respuesta);
}
```

### 2. Vistas (Views)
Las vistas son responsables de mostrar la información al usuario mediante HTML, CSS y JavaScript. Estas vistas reciben datos desde los controladores y los presentan de manera estructurada.

| Vista              | Funcionalidad |
|--------------------|--------------|
| `Index.cshtml`     | Página principal del sistema. |
| `Citas.cshtml`     | Vista para gestionar citas médicas. |
| `Pacientes.cshtml` | Vista para administrar pacientes. |
| `Medicos.cshtml`   | Vista para gestionar médicos. |
| `Facturacion.cshtml` | Vista para ver y registrar facturación. |

Ejemplo de una vista en Razor:
```html
@model List<CitaCLS>
<table>
    <tr>
        <th>Paciente</th>
        <th>Médico</th>
        <th>Fecha</th>
        <th>Estado</th>
    </tr>
    @foreach (var cita in Model) {
        <tr>
            <td>@cita.nombrePaciente</td>
            <td>@cita.nombreMedico</td>
            <td>@cita.fechaHora</td>
            <td>@cita.estado</td>
        </tr>
    }
</table>
```

### 3. Comunicación entre Controladores y Vistas

La comunicación entre los controladores y las vistas se realiza a través de **ViewModel** o **ViewData**. Los controladores envían datos a las vistas, que los renderizan para su visualización.

Ejemplo en C# para enviar datos a una vista:
```csharp
public IActionResult ListarCitas() {
    CitaBL obj = new CitaBL();
    List<CitaCLS> lista = obj.listarCitas();
    return View(lista);
}
```

### 4. Validaciones en los Controladores

Para garantizar la integridad de los datos, los controladores incluyen validaciones antes de enviar información a la base de datos.

Ejemplo de validación:
```csharp
[HttpPost]
public IActionResult GuardarPaciente(PacienteCLS paciente) {
    if (string.IsNullOrEmpty(paciente.nombre) || string.IsNullOrEmpty(paciente.apellido)) {
        return BadRequest("El nombre y apellido son obligatorios");
    }
    PacienteBL obj = new PacienteBL();
    int resultado = obj.GuardarPaciente(paciente);
    return Ok(resultado);
}
```

## Base de Datos (SQL Server)

El sistema utiliza **SQL Server** como motor de base de datos para almacenar la información de citas, pacientes, médicos y facturación.

### 1. Tablas principales

| Tabla              | Descripción |
|--------------------|-------------|
| `Citas`           | Almacena las citas médicas registradas. |
| `Pacientes`       | Contiene información de los pacientes. |
| `Medicos`         | Guarda los datos de los médicos y sus especialidades. |
| `Especialidades`  | Lista las especialidades médicas disponibles. |
| `Facturacion`     | Registra los pagos y métodos de facturación. |

### 2. Procedimientos Almacenados

| Procedimiento          | Funcionalidad |
|------------------------|--------------|
| `uspListarCitas`      | Obtiene todas las citas registradas. |
| `uspRecuperarCita`    | Recupera los datos de una cita específica. |
| `uspGuardarCita`      | Inserta o actualiza una cita en la base de datos. |
| `uspEliminarCita`     | Elimina una cita según su ID. |
| `uspListarPacientes`  | Obtiene todos los pacientes registrados. |
| `uspGuardarPaciente`  | Guarda o actualiza un paciente. |
| `uspEliminarPaciente` | Elimina un paciente si no tiene citas activas. |
| `uspListarFacturacion`| Obtiene registros de facturación. |
| `uspGuardarFacturacion`| Registra un nuevo pago. |
| `uspEliminarFacturacion`| Elimina un registro de facturación. |

Ejemplo de procedimiento almacenado para listar citas:
```sql
CREATE PROCEDURE uspListarCitas
AS
BEGIN
    SELECT idCita, idPaciente, nombrePaciente, idMedico, nombreMedico, fechaHora, estado
    FROM Citas
END
```

### 3. Seguridad y Optimización

Para garantizar un rendimiento óptimo, la base de datos incluye:
- **Índices** en los campos más consultados.
- **Llaves foráneas** para mantener la integridad de los datos.
- **Procedimientos almacenados** para optimizar consultas y evitar SQL Injection.
- **Copias de seguridad automáticas** para prevenir pérdida de datos.

---

## Puntos a Resaltar del Proyecto

- **Arquitectura en capas:** Facilita el mantenimiento y escalabilidad del sistema.
- **Uso de MVC:** Organiza de manera eficiente la interacción entre controladores, vistas y lógica de negocio.
- **Base de datos optimizada:** Con procedimientos almacenados y validaciones para asegurar la integridad de los datos.
- **Interfaz dinámica con JavaScript:** Permite una mejor experiencia de usuario mediante AJAX y `fetch`.
- **Validaciones en cada capa:** Seguridad y consistencia de datos mediante reglas de negocio y validaciones en frontend y backend.
- **Uso de `async/await` en JavaScript:** Mejora el rendimiento en la comunicación con el backend.
- **Módulo de facturación:** Integrado para gestionar pagos y generar reportes financieros.
