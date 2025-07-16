# BITÁCORA DE CAMBIOS Y HITOS

Este archivo registra, en orden cronológico, los cambios realizados en el proyecto y los hitos alcanzados. Cada entrada incluye una explicación simple y la fecha.

---

- [2024-06-01] Se eliminó el método LoadDocumentWithValidation de XmlDataManager.cs porque el archivo DTD (data.dtd) no se utiliza para validación en tiempo de ejecución. Si en el futuro aparece un error relacionado con este método, es porque se decidió limpiar el código de funciones no usadas para simplificar el mantenimiento.

- [2024-06-01] Se migró el flujo de backup (BackupForm y BackupService) para que utilicen exclusivamente DTOs y el mapeo correspondiente con BackupMapper y UserMapper. Ahora el formulario solo expone y consume DTOs, y el servicio se encarga de la conversión entre entidad y DTO, cumpliendo la arquitectura y las reglas del proyecto.

- [2024-06-01] Se eliminó el método FromDto en BackupMapper porque no se utiliza en el flujo actual ni hay casos de uso previstos. Si en el futuro se requiere recibir BackupDto desde la UI o una API para crear o actualizar backups, se podrá volver a implementar.

---

- [2024-06-01] Se agregaron los enums ProductType y ProductQualityCategory para clasificar productos y su calidad. Se añadieron las propiedades Type, QualityCategory y IsImported a Product y ProductDto, y se actualizó el ProductMapper para mapearlas correctamente.

- [2024-06-01] Se adaptó el formulario ProductForm para permitir seleccionar el tipo, la calidad y si el producto es importado. Se agregaron controles nuevos, se actualizaron los métodos de carga, guardado y limpieza, y se mejoró la validación para evitar errores de referencia nula. Además, se reforzó el manejo de errores en todos los métodos, mostrando siempre un mensaje claro al usuario si ocurre un problema.

- [2024-06-01] Inconsistencia en borrado de productos: ahora al presionar Eliminar, el producto se marca como inactivo y se muestra un mensaje al usuario. Además, se detectó que la verificación de uso en stock podía arrojar un error si el XML de stocks estaba vacío o mal formado (atributo productId nulo). Se deja pendiente para revisión y refactor futuro si hay tiempo.

- [2024-06-01] Se crearon las clases Order y OrderItem en Models para gestionar pedidos y sus ítems. Se crearon los DTOs OrderDto y OrderItemDto en DTOs para transferir datos de pedidos y sus ítems. Se crearon los mappers OrderMapper y OrderItemMapper, incluyendo métodos para mapear entre entidades, DTOs y XML. Se crearon los servicios OrderService y OrderItemService en Services, con métodos públicos para crear, actualizar, eliminar, buscar por ID y obtener todos los registros. Se eliminaron los comentarios de los métodos de los servicios para cumplir con la regla de no dejar comentarios en el código. Se creó el DTO InvoiceDto y la clase InvoiceItemDto para representar facturas y sus ítems. Se creó el formulario InvoiceForm en la UI, junto con su archivo Designer y resx, para permitir la visualización y edición visual de facturas.
--- 

- [2024-07-05] Se agregaron las propiedades DrinkName a OrderItemDto e Id a InvoiceDto para soportar la impresión de facturas y visualización de ítems. Se corrigió el uso de XFontStyle.Normal en vez de Regular en la generación de PDF con PdfSharp. Se corrigió el método de creación de ítems en OrderForm para usar CreateOrderItem. Se agregó la declaración y creación básica de flowLayoutPanel1 en EventManagementForm para evitar errores de referencia.
- [2024-07-05] Se eliminó el método GenerateInvoice de OrderService y se trasladó la lógica de generación de factura a OrderForm, utilizando los servicios disponibles en el formulario, para mantener la coherencia arquitectónica con el resto de la solución.
- [2024-07-05] Se corrigió la lógica de alta de órdenes y orderItems para que siempre asignen un ID mayor a 0, evitando la creación de entidades con id=0 y el error asociado al buscar por ID.
- [2024-07-05] Se migró la generación de PDF de facturas a QuestPDF, eliminando problemas de fuentes y configurando la licencia Community para un flujo robusto y multiplataforma.

2024-06-07: Se eliminó el archivo Core/FormAccessMap.cs de la capa UI porque no se estaba usando en ningún lado. La lógica de acceso a formularios ya está implementada directamente en los formularios de gestión, así que no hacía falta mantener este archivo. ¡Un poco más limpio el proyecto!

2024-06-07: Se corrigió RoleForm para que, al asignar la lista de roles al DataSource, no vuelva a mapear a DTOs si ya son RoleDto. Ahora se asigna directamente la lista, lo que soluciona el error de tipos y deja el código más limpio y eficiente.

2024-06-07: Se refactorizó UserForm y UserService para que el formulario solo trabaje con UserDto y el mapeo a modelo de dominio se haga internamente en el servicio. Así el form queda desacoplado de la lógica de mapeo y se cumple la arquitectura en capas. ¡Mucho más limpio y mantenible!

2024-06-07: Se ajustó UserForm para que nunca llame al mapper directamente. Ahora obtiene los DTOs usando métodos del UserService, reforzando la arquitectura en capas y el desacoplamiento entre UI y lógica de negocio. ¡Más limpio y profesional!

2024-06-07: Se refactorizó el constructor de MainMenuForm para delegar la inicialización de la UI, la carga de usuario, roles y categorías a métodos auxiliares. Ahora el código es mucho más legible y fácil de mantener.

2024-06-08: Se refactorizó el formulario UserProfileForm para que utilice exclusivamente UserDto en vez de la entidad User. Ahora todos los datos y operaciones en el formulario trabajan con el DTO, nunca con el modelo de dominio, cumpliendo la arquitectura en capas y las buenas prácticas del proyecto.

2024-06-08: Se agregó el método ToDto(User user) en UserService para convertir un User a UserDto sin necesidad de buscar por ID. Se actualizó UserProfileForm para usar este método y evitar búsquedas redundantes, mejorando la eficiencia y claridad del código.

2024-06-08: Se agregó el método ToEntity(UserDto dto) en UserService para convertir un UserDto a User. Se actualizó UserProfileForm para que, al actualizar la sesión, convierta el DTO a entidad antes de llamar a SetUser, asegurando la coherencia con la arquitectura y evitando errores de tipo.

2024-06-08: Se refactorizó UserProfileForm para que, tras cualquier cambio en los datos del usuario (nombre, apellido, email, contraseña, etc.), siempre se fuerce el cierre de sesión y se solicite volver a loguearse. Esto asegura que la sesión esté siempre sincronizada con los datos actualizados y evita inconsistencias.

2024-06-08: Se refactorizó RoleForm y los servicios relacionados para que la UI trabaje exclusivamente con RoleDto y PermissionDto. Se adaptaron los métodos CRUD de RoleService para aceptar y devolver DTOs, y se agregaron métodos de mapeo en ambos servicios. Ahora la UI nunca manipula entidades directamente, cumpliendo la arquitectura en capas.

2024-06-08: Se sobrescribió el método ToString() en PermissionDto para que el CheckedListBox muestre el nombre del permiso (Name) en vez del nombre de la clase, mejorando la experiencia de usuario en la gestión de roles.

2024-06-08: Se refactorizó PermissionForm y PermissionService para que la UI trabaje exclusivamente con PermissionDto. Se adaptaron los métodos CRUD del servicio para aceptar y devolver DTOs, y se agregaron métodos de mapeo. Ahora la UI nunca manipula entidades directamente, cumpliendo la arquitectura en capas.

2024-06-08: Se refactorizó PermissionItemForm y PermissionItemService para que la UI trabaje exclusivamente con PermissionItemDto. Se adaptaron los métodos CRUD del servicio para aceptar y devolver DTOs, y se agregaron métodos de mapeo. Ahora la UI nunca manipula entidades directamente, cumpliendo la arquitectura en capas.

- 2024-07-08: Se agregaron validaciones en UserService y RoleService para impedir eliminar el rol AdminAdmin, eliminar usuarios con ese rol o ponerlos inactivos. Los mensajes de error ahora se muestran en español y de forma específica en la UI.
- 2024-07-08: Se agregó la sección <cashRegisters> al archivo data.xml para permitir la gestión de cajas registradoras y evitar errores al crear nuevas cajas.
- 2024-07-08: Se implementó la lógica de permisos para la asignación de recursos basada en roles y tipos de recurso. Ahora, al asignar un recurso, solo se muestran los usuarios que tienen permiso para gestionarlo según la configuración en <resourceRolePermissions> del XML de la UI. Se actualizó el XML para reflejar los permisos de cada rol sobre cada tipo de recurso.
- 2024-07-08: Se integró ResourceRolePermissionService en ResourceAssignmentForm para filtrar usuarios según permisos de roles y tipos de recurso, asegurando que la UI solo permita asignaciones válidas según la configuración en el XML.
- 2024-07-08: Se creó la estructura base del formulario LiveEvent. Al abrir, muestra el próximo evento, lista los usuarios asignados y sus sectores, y permite ingresar al sector solo al usuario logueado, con lógica de redirección según el tipo de recurso asignado. Todo el flujo usa DTOs y cumple la arquitectura.

- 2024-07-08: Se creó el archivo Designer (LiveEvent.Designer.cs) y el archivo de recursos (LiveEvent.resx) para el formulario LiveEvent, completando la estructura visual con etiquetas para mostrar información del evento y un DataGridView para listar usuarios asignados con botones de acción.

- 2024-07-08: Se agregó el formulario LiveEvent al EventManagementForm para que sea accesible desde la gestión de eventos. El botón "Evento Vivo" requiere el permiso "Event_full_access" y permite acceder al formulario de eventos en vivo.

- [2024-12-19] Se unificaron todos los namespaces de la UI a `BarStockControl.UI` para mantener coherencia en la arquitectura. Se cambiaron todos los formularios que usaban `BarStockControl.Forms`, `BarStockControl.Forms.[Subcategorías]` y `BarStockControl` sin subcategoría para que usen únicamente `BarStockControl.UI`. Se actualizaron todas las referencias `using` correspondientes. El proyecto compila correctamente y ahora tiene una estructura de namespaces consistente y profesional.

**PENDIENTE:**

- 2024-07-09: Se refactorizó completamente el alta de asignaciones de recursos. Se eliminó el método AddFromDto y se implementó CreateAssignment en ResourceAssignmentService, que valida, asigna id único y devuelve errores de validación. Ahora ResourceAssignmentForm y todo el flujo de asignaciones usan este método, garantizando ids válidos y mensajes claros de error. El sistema es más robusto y coherente con el resto de la arquitectura.

- 2024-07-09: Se resolvió el problema de configuración incorrecta del repositorio git. El repositorio estaba configurado en el directorio raíz del usuario (C:/Users/54113) en lugar del proyecto específico, causando que se rastrearan archivos de todo el sistema. Se inicializó un nuevo repositorio git específico para el proyecto, se creó un .gitignore apropiado para .NET, y se hizo el primer commit y push exitoso al repositorio remoto con todos los archivos del proyecto BarStockControl.

- 2024-07-09: Se mejoró el formulario ResourceAssignmentForm para cargar automáticamente las asignaciones existentes cuando se selecciona un evento. Se agregó el evento SelectedIndexChanged al combo de eventos, se implementó el método LoadExistingAssignments para cargar asignaciones guardadas, y se mejoró la lógica de guardado para eliminar asignaciones anteriores antes de crear las nuevas. También se agregaron los métodos GetAssignmentsByEventId y DeleteAssignment al ResourceAssignmentService para soportar esta funcionalidad.

- 2024-07-09: Se implementó el uso de atributos [Description] en español en el enum EventStatus y un método de extensión ToFriendlyString para mostrar el estado del evento en frases amigables y en español en el formulario LiveEvent. Se eliminó el switch anterior y se mejoró la mantenibilidad y escalabilidad del código para mostrar estados de eventos.

- 2024-07-09: Se integró la apertura del formulario LiveStationForm desde LiveEvent cuando el usuario hace clic en "Ingresar" para una estación. Se implementó la lógica para buscar una orden por ID, mostrar los tragos de la orden, la receta del trago seleccionado, y el stock de la estación. Se agregaron botones para marcar la orden como "En preparación" y "Entregado", actualizando el estado correctamente.

- 2024-07-09: Se creó el enum OrderStatus con descripciones en español y método de extensión. Se actualizó la entidad, DTO, mapeo y servicios para usar el enum en vez de string. Se adaptó el flujo: al crear la orden, estado = PendienteDePago; al facturar, estado = Pagado; el barman puede marcar como EnPreparacion y Entregado. Se actualizaron las órdenes existentes en el XML para que el status coincida con el enum.

- 2024-07-09: Se mejoró la experiencia de usuario en el formulario de facturación y en el PDF, mostrando el número de orden grande y claro. En LiveStationForm, se agregaron labels descriptivos arriba de cada DataGridView, y los grids muestran solo nombres y cantidades, no IDs. Se corrigieron errores de inicialización de controles y se agregaron try-catch para evitar excepciones por referencias nulas.

- 2024-07-09: Se corrigieron errores de compilación en LiveStationForm causados por declaraciones duplicadas de controles DataGridView. Se eliminaron las declaraciones manuales de dgvOrderItems, dgvRecipeItems y dgvStock del archivo .cs ya que estaban declarados en el .Designer.cs. Se movieron los botones btnPreparar y btnEntregar al Designer para que se inicialicen correctamente, y se simplificó el método SetupUI() para solo agregar eventos a los controles existentes.

- 2024-07-10: Se creó la entidad BarmanOrder (con DTO, Mapper y Service) para registrar qué barman preparó cada orden. Al marcar una orden como 'En preparación' en LiveStationForm, se guarda automáticamente el registro con el ID del barman y la orden. En LiveBarForm, se agregó un nuevo DataGridView que muestra, al seleccionar una estación, todas las órdenes preparadas por los barman asignados a esa estación, incluyendo número de orden, nombre del barman, fecha y estado. Esto permite trazabilidad total de la preparación de tragos por usuario y estación.

- 2024-07-10: Se mejoró el dashboard (StatisticsForm) agregando un ComboBox selector de evento en la parte superior. Ahora, al seleccionar un evento, los gráficos y los totales de ventas y tragos vendidos se filtran dinámicamente por ese evento. Si no se selecciona ningún evento, se muestran los totales generales de todos los eventos. El refresco es inmediato y visualmente integrado al diseño del dashboard.

- 2024-07-10: Se implementó un gráfico de barras horizontales en el dashboard (StatisticsForm) con un ComboBox selector que permite elegir entre "Ventas por estación", "Ventas por barra" y "Ventas por barman". El gráfico se actualiza dinámicamente al cambiar la opción seleccionada y respeta el filtro de evento activo. Se agregaron los servicios BarService y BarmanOrderService para obtener los datos necesarios, y se reorganizó el layout del formulario para acomodar el nuevo gráfico ocupando todo el ancho en la parte inferior.

- 2024-07-10: Se refactorizó el gráfico de ventas por evento en StatisticsForm. Ahora muestra solo los eventos cuyo rango de fechas intersecta el mes seleccionado (o los últimos 30 días), y la barra de cada evento representa la suma total de todas las órdenes asociadas a ese evento, sin importar la fecha de la orden. El eje X está ordenado cronológicamente por fecha de inicio del evento. Se corrigió el cálculo del total de ventas para que muestre la suma de los eventos visibles en el gráfico. Además, se reforzó el manejo de errores para evitar cualquier excepción por null o datos inconsistentes en los selectores.

- [2024-07-10] Se corrigió el selector de meses en StatisticsForm para que muestre todos los meses del año actual (enero a diciembre) en orden descendente, más la opción "Últimos 30 días" por defecto. Se simplificó la lógica de SetupMesesSelector() eliminando la dependencia de las fechas de los eventos, y se ajustó el título del gráfico para mostrar "período seleccionado" en lugar de "mes seleccionado" para mayor claridad.

- [2024-07-10] Se corrigió el filtrado de órdenes en LoadSalesChart para incluir todos los estados válidos (Pagado, PendienteDePago, EnPreparacion, Entregado) en lugar de solo Pagado y PendienteDePago. Se agregó diagnóstico para mostrar mensajes específicos cuando no hay eventos en el período o cuando no hay ventas registradas, facilitando la identificación de problemas de datos.

- [2024-07-10] Se corrigió el filtrado en LoadPieChart para que el total de tragos vendidos respete el selector de eventos, eliminando el filtro de mes actual y manteniendo solo el filtro por evento seleccionado. Se actualizó el título del gráfico para indicar si muestra datos de un evento específico o de todos los eventos.

- [2024-07-10] En LiveStationForm ahora se muestra el stock de todos los productos de la estación al abrir el formulario. Al buscar una orden, si la cantidad de tragos de algún ítem supera el stock estimado, se muestra un mensaje y se limpia la orden cargada. Solo se puede preparar la orden si está en estado Pagada. Los botones de preparar y entregar se habilitan o deshabilitan según corresponda para evitar errores de operación.

- [2024-07-15] Se corrigió un problema crítico de recursión infinita en RoleService.MapFromXml que causaba que la aplicación se colgara al cargar roles con jerarquía. Se modificó el método para cargar solo IDs sin objetos completos, evitando llamadas recursivas a GetById.

- [2024-07-15] Se actualizó MainMenuForm para usar ComponentService en lugar del método problemático GetPermissionNamesByRoleIds del PermissionService. Se agregó ComponentService como dependencia y se modificaron LoadAvailableCategories y LoadUserRoles para usar los métodos recursivos del ComponentService.

- [2024-07-15] Se corrigió el problema de que no se mostraban botones en MainMenuForm al agregar la llamada a BuildPermissions en LoginForm después de la autenticación exitosa. Esto asegura que los permisos del usuario se construyan correctamente antes de mostrar el menú principal.

- [2024-07-15] Se eliminaron todos los mensajes de debug del flujo de login y MainMenuForm para que la aplicación funcione normalmente sin interrupciones.

- [2024-07-15] Se corrigió el problema de carga de permisos en la jerarquía de roles. Se creó el método GetByIdWithHierarchy en RoleService que carga roles con sus permisos reales (nombres como "UserFullAccess") en lugar de nombres temporales ("Permiso 1"). Se actualizó ComponentService para usar este método y construir correctamente la jerarquía completa de permisos del usuario.

- [2024-07-15] Se resolvió el problema de que no se mostraban botones en MainMenuForm. El problema era que los permisos se cargaban con nombres temporales que no coincidían con los PermissionType requeridos. Ahora los permisos se cargan con sus nombres reales y la comparación funciona correctamente, mostrando los botones correspondientes según los permisos del usuario.
