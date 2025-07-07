# REGLAS PARA LA IA

- Siempre responde en español.

- Mantener actualizada una bitácora (log) en donde se registre, de forma automática, cada cambio realizado en el código (agregar, quitar o modificar), explicando el cambio de manera simplificada y coloquial, con la fecha y respetando el orden cronológico. Cada vez que el usuario acepte un cambio, la IA debe registrar la explicación en la bitácora.
- El registro de hitos alcanzados lo avisará el usuario, y la IA los agregará a la bitácora cuando se le indique.

- Todo método o archivo que ejecute una acción o que pueda fallar, generar un error o provocar que el sistema se caiga, debe llevar un bloque try-catch para manejar posibles errores y evitar que el sistema falle inesperadamente.

- El proyecto utiliza una arquitectura en capas: forms, dto, service, mapper, data, models y security. Siempre se debe respetar y aprovechar esta estructura en el desarrollo y las modificaciones. **Es obligatorio respetar y utilizar la arquitectura base definida en el proyecto en todo momento.**

- Los Forms (UI) solo deben trabajar con DTOs y comunicarse exclusivamente con la capa Service. Nunca deben acceder directamente a la capa Data ni trabajar con entidades del modelo. La capa Service debe utilizar los Mappers para convertir entre DTOs y entidades del modelo, y comunicarse con la capa Data para el acceso a datos.

- Nota: La carpeta BarStockControl solo contiene archivos de configuración necesarios para el funcionamiento del proyecto, pero no debe ser tenida en cuenta para el desarrollo. Las carpetas relevantes para la arquitectura y el desarrollo son: BarStockControl.UI, BarStockControl.DTOs, BarStockControl.Services, BarStockControl.Mappers, BarStockControl.Data, BarStockControl.Models y BarStockControl.Security.

- Cada vez que se cree una nueva clase o entidad, se debe actualizar el archivo de estructura correspondiente en BarStockControl.Data/Xml (data.xml, data.dtd, backup_log.xml) para reflejar el nuevo formato y estructura. Estos archivos funcionan como plantilla o referencia de la estructura de datos.
- Los datos reales de la aplicación se guardan y actualizan en tiempo de ejecución en la carpeta BarStockControl.UI/bin/Debug/net8.0-windows/Xml (o la carpeta de salida correspondiente según la configuración y build).

- Todos los servicios (Service) que se creen deben extender la clase BaseService<T> ubicada en BarStockControl.Services/BaseService.cs y aprovechar sus métodos para las operaciones CRUD y de manejo de datos.

- Siempre se debe analizar si el archivo DTD (data.dtd) tiene uso real en el sistema. Si no se utiliza para validación en tiempo de ejecución, se deben eliminar los métodos relacionados, como LoadDocumentWithValidation.

- Al trabajar en un Form, si no está implementando el uso de DTOs, se debe corregir para que lo haga y así respetar la arquitectura definida.

- No se debe dejar ningún comentario en el código, salvo que el usuario lo indique explícitamente. Eliminar todos los comentarios por defecto.

- El código fuente, nombres de variables, métodos y comentarios (si los hubiera) deben estar en inglés. Sin embargo, todo texto visible para el usuario final (labels, botones, títulos, mensajes, descripciones, alertas, etc.) debe estar en español.

- Siempre que se realice un repaso o revisión, se debe buscar y reportar: métodos no usados, métodos sin try-catch, comentarios innecesarios y cualquier texto visible para el usuario en inglés (botones, labels, alertas, etc.), ya que todos los mensajes al usuario deben estar en español.

(Agrega aquí nuevas reglas a medida que las necesites) 
