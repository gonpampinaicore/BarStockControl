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

