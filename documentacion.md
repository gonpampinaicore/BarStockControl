
### [Flujos de formularios UI - Revisión y documentación]

A continuación se listan todos los formularios principales del sistema. Se irá marcando cuáles ya fueron revisados y se deja un resumen de su lógica principal (especialmente lo que ocurre en el constructor y por qué):

- [x] LoginForm:
- [x] MainMenuForm:
- [x] UserForm:
- [x] UserProfileForm:
- [x] RoleForm:
- [x] PermissionForm:
- [x] PermissionItemForm:
- [x] BackupForm:
- [x] UserManagementForm:
- [ ] ProductForm:
- [ ] StockMovementForm:
- [ ] StockForm:
- [ ] StatisticsForm:
- [ ] StationForm:
- [ ] ResourceAssignmentForm:
- [ ] RecipeForm:
- [ ] OrderForm:
- [ ] InvoiceForm:
- [ ] InventoryManagementForm:
- [ ] InfrastructureManagementForm:
- [ ] EventManagementForm:
- [ ] EventForm:
- [ ] DrinkForm:
- [ ] DepositForm:
- [ ] CashRegisterForm:
- [ ] BarForm:

Se irá completando y marcando cada uno a medida que se revisen sus flujos y lógica principal.

---

#### [LoginForm - Revisión completa y documentación]

**Estado:** Revisado y documentado

**Resumen del flujo y funcionamiento:**

- **Constructor:**
  - Llama a `InitializeComponent()` para inicializar todos los controles visuales del formulario.
  - Instancia el `UserService`, pasándole un `XmlDataManager` que apunta a `Xml/data.xml`. Esto permite que el servicio acceda a los datos de usuarios sin que el formulario se acople a la lógica de acceso a datos.

- **Controles visuales:**
  - Dos etiquetas: "Email:" y "Contraseña:".
  - Dos cajas de texto: una para el email y otra para la contraseña (con ocultamiento de caracteres).
  - Un botón "Iniciar sesión" que dispara la lógica de autenticación.
  - Todos los textos de la interfaz están en español, cumpliendo las reglas del proyecto.

- **Validaciones en el login:**
  - Se usa `Trim()` para eliminar espacios en blanco al principio y al final del email ingresado.
  - Se utiliza `string.IsNullOrWhiteSpace()` (método propio de .NET) para verificar que los campos no estén vacíos ni contengan solo espacios.
  - Si algún campo está vacío, se muestra un mensaje de error y no se continúa.

- **Autenticación:**
  - Se llama a `_userService.Authenticate(email, password)`, que valida las credenciales contra los datos almacenados.
  - Si el usuario es válido y está activo, se guarda en la sesión usando `SessionContext.Instance.SetUser(user)`.
  - No es necesario instanciar `SessionContext` porque es un Singleton: siempre se accede a la misma instancia global.
  - Si la autenticación falla, se muestra un mensaje de error.

- **Navegación:**
  - Si el login es exitoso, se abre el formulario principal (`MainMenuForm`) y se oculta el formulario de login.

- **Manejo de errores:**
  - Todo el proceso está envuelto en un bloque try-catch. Si ocurre cualquier excepción, se muestra un mensaje claro al usuario.

- **Arquitectura y flexibilidad:**
  - El formulario nunca accede directamente a los datos, siempre lo hace a través del servicio.
  - El `UserService` puede trabajar con distintos archivos XML (o incluso con otros gestores de datos en el futuro) simplemente cambiando la instancia de `XmlDataManager` que se le pasa.
  - Esto permite tener distintos entornos (producción, pruebas, etc.) y facilita el testing y el mantenimiento.

- **Aclaración sobre acceso a Data:**
  - El formulario importa el namespace `BarStockControl.Data` únicamente para poder instanciar `XmlDataManager` y pasárselo al `UserService`.
  - El formulario **no** accede directamente a los datos ni utiliza métodos de `.Data` para leer o escribir información.
  - Toda la lógica de acceso a datos sigue pasando exclusivamente por el servicio, respetando la arquitectura en capas.

- **Flujo de autenticación y relaciones entre clases:**
  - El LoginForm llama a `UserService.Authenticate(email, password)` pasando el email y la contraseña en texto plano.
  - El método Authenticate encripta la contraseña usando `PasswordEncryption.EncryptPassword` (Base64 con Unicode).
  - Luego busca el usuario en el XML usando `GetAll()` (heredado de BaseService), comparando email, contraseña encriptada y que esté activo.
  - Si encuentra un usuario válido, lo retorna; si no, retorna null.
  - El formulario nunca interactúa con la encriptación ni con el acceso a datos, solo con el servicio.
  - Solo UserService usa PasswordEncryption y los métodos de BaseService.

- **Observación sobre arquitectura:**
  - Actualmente, el método Authenticate retorna un objeto User (modelo de dominio).
  - Para cumplir estrictamente con la arquitectura en capas, lo correcto sería que el servicio retorne un UserDto, y que el formulario solo trabaje con DTOs, no con modelos de dominio.

---

#### [SessionContext - Revisión y documentación]

- **Patrón Singleton:**
  - La clase implementa el patrón Singleton, asegurando que solo exista una instancia global de SessionContext en toda la aplicación.
  - Se accede siempre mediante `SessionContext.Instance`. El getter instancia la clase solo la primera vez que se accede, y luego reutiliza la misma instancia.

- **Propiedad LoggedUser y private set:**
  - `LoggedUser` es pública para lectura, pero tiene un setter privado (`private set`).
  - Esto significa que cualquier parte del código puede consultar el usuario logueado, pero solo la propia clase puede modificarlo.
  - Así, se protege la integridad de la sesión: el usuario solo puede cambiarse usando el método público `SetUser(User user)`, evitando modificaciones accidentales o inseguras desde fuera.

- **Métodos principales:**
  - `SetUser(User user)`: establece el usuario logueado.
  - `Clear()`: limpia la sesión (por ejemplo, al cerrar sesión).
  - `IsLoggedIn`: propiedad que indica si hay un usuario logueado.

- **Utilidad en la arquitectura:**
  - Centraliza la gestión de la sesión del usuario.
  - Permite acceder al usuario logueado desde cualquier parte de la aplicación sin pasar referencias.
  - Facilita la validación de permisos, auditoría y otras operaciones relacionadas con la sesión.

---

#### [PasswordEncryption - Revisión y documentación]

- **Función principal:**
  - Clase estática que provee métodos para "encriptar" y "desencriptar" contraseñas de usuario usando Base64 y Unicode.

- **Métodos:**
  - `EncryptPassword(string plain)`: Convierte la contraseña en texto plano a un array de bytes usando `Encoding.Unicode.GetBytes`, luego la transforma a una cadena Base64 con `Convert.ToBase64String`. Devuelve la cadena resultante.
  - `DecryptPassword(string encrypted)`: Convierte la cadena Base64 de vuelta a bytes con `Convert.FromBase64String` y luego a texto plano con `Encoding.Unicode.GetString`. Si la cadena no es un Base64 válido, lanza una excepción.

- **Uso de string.Empty:**
  - Ambos métodos devuelven `string.Empty` si el parámetro recibido es nulo o vacío, evitando errores y asegurando que nunca se procese una cadena vacía.

- **Uso de Encoding y Convert:**
  - `Encoding.Unicode.GetBytes(string)`: Convierte una cadena de texto a un array de bytes usando codificación Unicode (UTF-16).
  - `Convert.ToBase64String(byte[])`: Convierte un array de bytes a una cadena en Base64, que es más fácil de almacenar y transmitir.
  - `Convert.FromBase64String(string)`: Convierte una cadena en Base64 de vuelta a un array de bytes.
  - `Encoding.Unicode.GetString(byte[])`: Convierte un array de bytes codificados en Unicode de vuelta a una cadena de texto.

- **Advertencia de seguridad:**
  - Este método NO es seguro para producción real. Solo ofusca la contraseña, pero cualquiera con acceso al XML puede decodificarla fácilmente.
  - En sistemas reales, se recomienda usar algoritmos de hash seguros (bcrypt, PBKDF2, Argon2, etc.) y nunca guardar contraseñas reversibles.

- **Dónde se usa:**
  - En el flujo de login, para comparar la contraseña ingresada con la almacenada.
  - En el mapeo de modelos a DTOs, para mostrar la contraseña desencriptada si es necesario (aunque en la práctica, nunca deberías mostrar la contraseña original).

--- 

---

#### [UserForm y UserService - Revisión y documentación]

**UserForm:**
- El formulario gestiona la creación, edición, eliminación y visualización de usuarios.
- Trabaja exclusivamente con UserDto, nunca con modelos de dominio.
- Al crear o actualizar un usuario, arma un UserDto con los datos de la UI y los roles seleccionados.
- Llama a los métodos del UserService (`CreateUser`, `UpdateUser`) pasando el DTO.
- El formulario nunca llama a los mappers directamente; el mapeo a modelo se realiza en el servicio.
- El método `GetUserFromForm` devuelve un UserDto, incluyendo los RoleIds seleccionados.
- El DataGridView de roles se configura en el diseñador, y el método FilterRoles solo limpia y llena las filas según el filtro y los roles seleccionados.
- Los errores de validación se muestran al usuario si existen, y si no hay errores, se limpia el formulario y se recarga la lista de usuarios.

**UserService:**
- Expone métodos como `CreateUser(UserDto userDto)` y `UpdateUser(UserDto userDto)` que reciben DTOs.
- Realiza el mapeo de DTO a modelo usando UserMapper internamente.
- Valida los datos del usuario con `ValidateUser`, que retorna una lista de errores (`List<string>`).
- Si hay errores, los retorna y no realiza la operación; si no hay errores, agrega o actualiza el usuario y retorna una lista vacía.
- No retorna el usuario creado ni actualizado, solo la lista de errores.
- El método `GetAllUsers` retorna una lista de UserDto para que la UI nunca manipule modelos de dominio.

**Manejo de errores:**
- El flujo de creación y actualización siempre retorna una lista de errores (`List<string>`).
- Si la lista está vacía, la operación fue exitosa; si tiene elementos, se muestran al usuario.

**Desacoplamiento y arquitectura:**
- El formulario está completamente desacoplado de la lógica de negocio y de datos.
- Toda la lógica de mapeo y validación está en el servicio.
- Se cumple la arquitectura en capas: la UI solo usa DTOs y delega todo al servicio.

**Ejemplo de uso en el formulario:**
```csharp
var userDto = GetUserFromForm();
var errors = _userService.CreateUser(userDto);
if (errors.Any())
{
    MessageBox.Show(string.Join("\n", errors), "Errores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    return;
}
// Si no hay errores, limpiar y recargar
ClearForm();
LoadUsers();
```

--- 

---

#### [MainMenuForm - Revisión y documentación]

**Listado de métodos principales:**
- **MainMenuForm()** (constructor): Inicializa componentes, servicios y dependencias, configura tooltips, valida usuario logueado, muestra nombre y roles, y carga las categorías disponibles.
- **LoadAvailableCategories()**: Determina qué categorías de gestión mostrar según los permisos del usuario y agrega los botones correspondientes al menú.
- **AddCategoryButton(string label, Func<Form> formFactory)**: Crea y agrega un botón de categoría al menú, configurando su evento click para abrir el formulario correspondiente.
- **btnLogout_Click**: Cierra la sesión, limpia el contexto y muestra el formulario de login.
- **btnUserProfile_Click**: Abre el formulario de perfil de usuario y actualiza el saludo si se modifican los datos.
- **OnFormClosing**: Maneja el cierre del formulario, asegurando que se limpie la sesión y se muestre el login si corresponde.
- **CloseProgrammatically**: Permite cerrar el formulario de manera controlada desde el código.

**Constructor (MainMenuForm):**
- Inicializa los componentes visuales y varias dependencias de servicio: `PermissionService`, `OrderService`, `DrinkService`, `OrderItemService`.
- Crea un `ToolTip` para el botón de perfil.
- Obtiene el usuario logueado desde `SessionContext`. Si no hay usuario, muestra un error y cierra el formulario.
- Muestra el nombre y apellido del usuario en el label de bienvenida.
- Instancia un `RoleService` para obtener los nombres de los roles del usuario y mostrarlos en el label correspondiente.
- Llama a `LoadAvailableCategories()` para determinar y mostrar los módulos a los que el usuario tiene acceso según sus permisos.
- Maneja errores de inicialización mostrando mensajes claros y cerrando el formulario si ocurre algún problema.

**Observación:**
- El constructor realiza muchas tareas: inicializa servicios, configura la UI, valida sesión, obtiene roles y permisos, y carga categorías. Esto puede dificultar el mantenimiento y la legibilidad.
- Una posible mejora sería delegar parte de la lógica (como la obtención de roles o la carga de categorías) a métodos auxiliares o servicios dedicados, manteniendo el constructor más limpio y enfocado solo en la inicialización básica.

- **Control de cierre programático en MainMenuForm:**
  - El método `CloseProgrammatically()` setea la bandera `_isClosingProgrammatically` en true y luego llama a `this.Close()`, permitiendo distinguir entre un cierre iniciado por el usuario (clic en la X) y uno iniciado desde el código (por ejemplo, al cerrar sesión).
  - En `OnFormClosing`, si el cierre es por el usuario y la bandera es false, se limpia la sesión y se muestra el login. Si la bandera es true, se omite esa lógica, permitiendo un flujo controlado.
  - Este patrón evita efectos secundarios no deseados y permite controlar el flujo de la aplicación de manera precisa.

- **Creación dinámica de botones de categorías:**
  - Los botones de categorías en el menú principal se crean dinámicamente en tiempo de ejecución según los permisos del usuario. Por este motivo, no pueden definirse en el diseñador, ya que el número y tipo de botones varía en cada sesión.
  - Solo los controles fijos (como el contenedor `flowLayoutPanel1` o botones siempre visibles) deberían estar en el diseñador.

- **Refactor del constructor de MainMenuForm:**
  - El constructor fue refactorizado para delegar la inicialización de la UI, la carga de usuario, roles y categorías a métodos auxiliares, mejorando la legibilidad y mantenibilidad del código.

--- 

### UserProfileForm
Formulario para que el usuario logueado visualice y edite sus propios datos (nombre, apellido, email, contraseña y estado). Solo permite modificar el usuario actualmente en sesión.

**Métodos principales:**
- `UserProfileForm()`: Constructor. Inicializa componentes, instancia el servicio de usuario y carga los datos del usuario logueado.
- `LoadUserData()`: Carga los datos del usuario actual desde la sesión y los muestra en los controles del formulario. Si no hay usuario logueado, muestra un mensaje y cierra el formulario.
- `btnSave_Click(object sender, EventArgs e)`: Valida los campos, construye un UserDto con los datos ingresados y llama a `UpdateUser` del servicio. Si el email o contraseña cambian, solicita al usuario reiniciar sesión; si no, actualiza la sesión y muestra confirmación.
- `btnCancel_Click(object sender, EventArgs e)`: Cierra el formulario sin guardar cambios.
- `OnFormClosing(FormClosingEventArgs e)`: Método sobrescrito, actualmente no implementa lógica adicional.

**Flujo principal:**
1. Al abrir el formulario, se cargan los datos del usuario logueado.
2. El usuario puede modificar sus datos y guardar.
3. Si cambia email o contraseña, se le pide volver a iniciar sesión; si no, se actualiza la sesión y se cierra el formulario.
4. Si cancela, simplemente se cierra el formulario. 

---

### RoleForm
Formulario para la gestión de roles: permite crear, editar, eliminar y visualizar roles, así como asignar permisos a cada rol. Trabaja exclusivamente con RoleDto y PermissionDto en la UI.

**Métodos principales:**
- `RoleForm()`: Constructor. Inicializa componentes, instancia los servicios y carga la lista de permisos y roles.
- `LoadRoles()`: Carga los roles desde el servicio, aplicando filtros de búsqueda y de activos si corresponde, y los muestra en el DataGridView.
- `LoadPermissions()`: Carga los permisos desde el servicio, los convierte a DTO y los muestra en el CheckedListBox.
- `btnCreate_Click(object sender, EventArgs e)`: Toma los datos del formulario, arma un RoleDto y lo envía al servicio para crear un nuevo rol. Muestra errores si los hay.
- `btnUpdate_Click(object sender, EventArgs e)`: Similar a crear, pero actualiza el rol seleccionado.
- `btnDelete_Click(object sender, EventArgs e)`: Elimina el rol seleccionado tras confirmación.
- `dgvRoles_CellClick(object sender, DataGridViewCellEventArgs e)`: Al seleccionar un rol, carga sus datos en el formulario y marca los permisos asignados.
- `GetRoleFromForm()`: Construye un RoleDto con los datos actuales del formulario y los permisos seleccionados.
- `ClearForm()`: Limpia los campos del formulario y desmarca los permisos.

**Flujo principal:**
1. Al abrir el formulario, se cargan los roles y permisos disponibles.
2. El usuario puede crear, editar o eliminar roles, y asignar permisos a cada uno.
3. Todas las operaciones se realizan usando DTOs, nunca entidades.
4. Los cambios se reflejan inmediatamente en la lista de roles tras cada operación. 

---

### PermissionForm
Formulario para la gestión de permisos: permite crear, editar, eliminar y visualizar permisos, así como asignar elementos de permiso (PermissionItem) a cada permiso. Trabaja exclusivamente con PermissionDto y PermissionItemDto en la UI.

**Métodos principales:**
- `PermissionForm()`: Constructor. Inicializa componentes, instancia los servicios y carga la lista de elementos de permiso y permisos.
- `LoadPermissions()`: Carga los permisos desde el servicio como DTOs, aplicando filtros de búsqueda y de activos si corresponde, y los muestra en el DataGridView.
- `LoadPermissionItems()`: Carga los elementos de permiso desde el servicio y los muestra en el CheckedListBox.
- `btnCreate_Click(object sender, EventArgs e)`: Toma los datos del formulario, arma un PermissionDto y lo envía al servicio para crear un nuevo permiso. Muestra errores si los hay.
- `btnUpdate_Click(object sender, EventArgs e)`: Similar a crear, pero actualiza el permiso seleccionado.
- `btnDelete_Click(object sender, EventArgs e)`: Elimina el permiso seleccionado tras confirmación.
- `dgvPermissions_CellClick(object sender, DataGridViewCellEventArgs e)`: Al seleccionar un permiso, carga sus datos en el formulario y marca los elementos de permiso asignados.
- `GetPermissionFromForm()`: Construye un PermissionDto con los datos actuales del formulario y los elementos seleccionados.
- `ClearForm()`: Limpia los campos del formulario y desmarca los elementos.

**Flujo principal:**
1. Al abrir el formulario, se cargan los permisos y elementos de permiso disponibles.
2. El usuario puede crear, editar o eliminar permisos, y asignar elementos a cada uno.
3. Todas las operaciones se realizan usando DTOs, nunca entidades.
4. Los cambios se reflejan inmediatamente en la lista de permisos tras cada operación. 

---

### PermissionItemForm
Formulario para la gestión de elementos de permiso (PermissionItem): permite crear, editar, eliminar y visualizar elementos atómicos de permiso. Trabaja exclusivamente con PermissionItemDto en la UI.

**Métodos principales:**
- `PermissionItemForm()`: Constructor. Inicializa componentes, instancia el servicio y carga la lista de elementos de permiso.
- `LoadItems()`: Carga los elementos de permiso desde el servicio como DTOs, aplicando filtros de búsqueda y de activos si corresponde, y los muestra en el DataGridView.
- `btnCreate_Click(object sender, EventArgs e)`: Toma los datos del formulario, arma un PermissionItemDto y lo envía al servicio para crear un nuevo elemento. Muestra errores si los hay.
- `btnUpdate_Click(object sender, EventArgs e)`: Similar a crear, pero actualiza el elemento seleccionado.
- `btnDelete_Click(object sender, EventArgs e)`: Elimina el elemento seleccionado tras confirmación.
- `dgvPermissionItems_CellClick(object sender, DataGridViewCellEventArgs e)`: Al seleccionar un elemento, carga sus datos en el formulario.
- `GetItemFromForm()`: Construye un PermissionItemDto con los datos actuales del formulario.
- `ClearForm()`: Limpia los campos del formulario.

**Flujo principal:**
1. Al abrir el formulario, se cargan los elementos de permiso disponibles.
2. El usuario puede crear, editar o eliminar elementos de permiso.
3. Todas las operaciones se realizan usando DTOs, nunca entidades.
4. Los cambios se reflejan inmediatamente en la lista tras cada operación. 

### UserManagementForm

**Función:**
Formulario principal de gestión de usuarios, roles, permisos y respaldos. Muestra dinámicamente los accesos disponibles según los permisos del usuario logueado.

**Arquitectura y buenas prácticas:**
- La UI nunca accede directamente a entidades de dominio ni utiliza mappers; solo consume servicios y trabaja con datos expuestos por estos.
- Utiliza el `SessionContext` para obtener el usuario logueado y el `PermissionService` para consultar los permisos asociados a sus roles.
- Los botones de acceso a los distintos módulos (Usuarios, Roles, Permisos, Elementos de Permiso, Respaldo) se crean dinámicamente según los permisos del usuario.
- Si el usuario no tiene permisos para ningún módulo, se muestra un mensaje informativo en gris.
- Todos los textos y mensajes están en español.
- El control visual principal es un `FlowLayoutPanel` (`flowLayoutPanel1`), que organiza los botones de acceso de manera flexible y automática.
- El método `AddFormButton` encapsula la lógica de creación y manejo de eventos de los botones.
- El formulario está protegido contra errores mediante bloques try-catch y muestra mensajes claros al usuario.

**Reglas cumplidas:**
- La UI solo consume DTOs y servicios, nunca entidades ni mappers.
- El acceso a la sesión y permisos está centralizado y desacoplado.
- El código es claro, mantenible y sigue las buenas prácticas de la arquitectura en capas.

--- 

### BackupForm

**Función:**
Formulario encargado de la gestión de copias de seguridad (backups) y restauraciones del archivo de datos principal del sistema.

**Responsabilidades:**
- Permite crear backups, restaurar el sistema a partir de un backup y eliminar archivos de backup.
- Muestra la lista de backups y restauraciones realizadas, permitiendo filtrado por tipo.
- La UI solo interactúa con DTOs y servicios, nunca con entidades ni lógica de negocio directa.
- Toda la lógica de backup/restauración/eliminación está delegada al `BackupService`.
- Utiliza controles visuales como DataGridView para mostrar la información y botones para las acciones.

**Flujo principal:**
- Al abrir el formulario, se carga la lista de backups usando el servicio.
- Al crear un backup, se valida el usuario y se solicita al servicio la creación y registro del backup.
- Al restaurar, se selecciona un backup y se solicita al servicio la restauración y registro.
- Al eliminar, se selecciona un backup y se solicita al servicio la eliminación física del archivo.

**Buenas prácticas:**
- La UI nunca accede directamente a archivos ni entidades, solo a través de servicios y DTOs.
- El formulario está desacoplado de la lógica de negocio y de acceso a datos.

---

### BackupService

**Función:**
Servicio encargado de toda la lógica de negocio relacionada con la creación, restauración y eliminación de backups.

**Responsabilidades:**
- Crear backups: valida usuario, verifica espacio, genera nombre único, copia el archivo de datos y registra el backup.
- Restaurar backups: valida usuario y archivo, realiza backup previo, restaura el archivo de datos y registra la restauración.
- Eliminar backups: valida y elimina archivos de backup, evitando borrar el archivo principal.
- Proveer la lista de backups y restauraciones como DTOs para la UI.

**Relación con la arquitectura:**
- Utiliza la capa `.Data` solo para acceder a los datos, nunca para lógica de negocio.
- Centraliza todas las reglas y procesos relacionados con backups, manteniendo la UI simple y desacoplada.
- Permite reutilización y testeo independiente de la lógica de backup.

**Buenas prácticas:**
- Separa claramente la lógica de negocio del acceso a datos y de la UI.
- Maneja validaciones, nombres de archivos, espacio en disco y registro de operaciones.

--- 

### Patrón de servicios y mapeo XML

**Arquitectura de servicios:**
- Todos los servicios de acceso a datos heredan de la clase genérica `BaseService<T>`, que centraliza la lógica de lectura, escritura y actualización de entidades en el XML.
- `BaseService<T>` define dos métodos abstractos:
  - `protected abstract T MapFromXml(XElement element);`
  - `protected abstract XElement MapToXml(T entity);`
- Cada servicio concreto (por ejemplo, `UserService`, `RoleService`, `ResourceRolePermissionService`) **debe sobrescribir** estos métodos para explicar cómo convertir entre su entidad y el XML.

**¿Cómo funciona?**
- Cuando se llama a `GetAll()` en un servicio, la clase base lee los elementos XML de la sección correspondiente y llama a `MapFromXml` para convertir cada elemento en una entidad.
- Cuando se guarda o actualiza una entidad, la base llama a `MapToXml` para convertir la entidad a XML antes de escribirla.

**Ventajas:**
- Permite reutilizar la lógica de acceso a datos para cualquier entidad, solo implementando el mapeo específico.
- Mantiene el código desacoplado, limpio y fácil de mantener.
- Facilita la extensión del sistema: para agregar una nueva entidad, solo se crea el servicio y se implementan los métodos de mapeo.

**Ejemplo aplicado:**
- `ResourceRolePermissionService` hereda de `BaseService<ResourceRolePermission>` y sobrescribe los métodos de mapeo para convertir entre `<resourceRolePermission>` en XML y la entidad `ResourceRolePermission`.
- Así, puede usar todos los métodos CRUD y de consulta de la base, y solo se preocupa por su lógica de negocio específica (por ejemplo, filtrar usuarios por permisos).

**Resumen:**
- El patrón asegura coherencia, reutilización y robustez en el acceso a datos y la lógica de negocio en todo el sistema.

--- 

### Uso de ResourceRolePermissionService en ResourceAssignmentForm

En el formulario de asignación de recursos (`ResourceAssignmentForm`):
- Se instancia `ResourceRolePermissionService` con el mismo XmlDataManager que el resto de los servicios.
- Al seleccionar un tipo de recurso, se llama a `LoadUsers(selectedType)`, que utiliza el método `GetUsersForResourceType` del servicio para filtrar y mostrar solo los usuarios que tienen permiso (por sus roles) para gestionar ese tipo de recurso.
- Esto asegura que la UI solo permita asignar recursos a usuarios válidos según la configuración de permisos en el XML.

--- 

### ResourceAssignmentForm

Formulario para la asignación de recursos (depósitos, barras, estaciones, cajas) a usuarios para un evento determinado.

**Flujo principal:**
- El usuario selecciona el evento, el tipo de recurso, el recurso específico y el usuario a asignar.
- Al guardar, el formulario recorre todas las asignaciones pendientes y llama a `CreateAssignment` del `ResourceAssignmentService` para cada una.
- El método `CreateAssignment` valida la asignación, asigna un id único y devuelve una lista de errores si los hay.
- Si hay errores de validación, se muestran al usuario y solo se guardan las asignaciones válidas.
- Si todas las asignaciones son válidas, se guardan y se muestra un mensaje de éxito.
- El método antiguo `AddFromDto` fue eliminado y ya no se utiliza en ningún flujo.

**Buenas prácticas y arquitectura:**
- La UI solo trabaja con DTOs y nunca con entidades ni mappers.
- Toda la lógica de validación, asignación de id y persistencia está en el servicio.
- El formulario está desacoplado de la lógica de negocio y de acceso a datos.
- Se cumple la arquitectura en capas y las reglas del proyecto.

**Integración con permisos:**
- Al seleccionar el tipo de recurso, solo se muestran los usuarios que tienen permiso para ese tipo, usando `ResourceRolePermissionService`.
- Esto asegura que solo se puedan hacer asignaciones válidas según la configuración de roles y permisos en el XML.

--- 
