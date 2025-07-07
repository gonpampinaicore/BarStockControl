
### [Flujos de formularios UI - Revisión y documentación]

A continuación se listan todos los formularios principales del sistema. Se irá marcando cuáles ya fueron revisados y se deja un resumen de su lógica principal (especialmente lo que ocurre en el constructor y por qué):

- [x] LoginForm:
- [x] UserForm:
- [ ] UserProfileForm:
- [ ] UserManagementForm:
- [ ] StockMovementForm:
- [ ] StockForm:
- [ ] StatisticsForm:
- [ ] StationForm:
- [ ] RoleForm:
- [ ] ResourceAssignmentForm:
- [ ] RecipeForm:
- [ ] ProductForm:
- [ ] PermissionItemForm:
- [ ] PermissionForm:
- [ ] OrderForm:
- [ ] MainMenuForm:
- [ ] InvoiceForm:
- [ ] InventoryManagementForm:
- [ ] InfrastructureManagementForm:
- [ ] EventManagementForm:
- [ ] EventForm:
- [ ] DrinkForm:
- [ ] DepositForm:
- [ ] CashRegisterForm:
- [ ] BarForm:
- [ ] BackupForm:

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
