# DulcesChurrascosAPI

API .NET Minimal para gestionar productos de una tienda de dulces típicos y churrascos.

## 📦 Características

- CRUD de **Productos** (general, con soporte para churrascos y dulces)  
- CRUD de **Inventarios**  
- CRUD de **Combos**  
- CRUD de **Ventas**  
- CRUD de **Churrascos** con validaciones:  
  - Exactamente 2 guarniciones  
  - Modalidad automática según porciones (1, 3 o 5)  
- CRUD de **Dulces** con validación de cajas (6, 12 o 24 unidades)  
- Endpoint `/api/chat` que envía contexto de menú a un LLM vía OpenRouter  
- CORS configurado para tu app React en `http://localhost:5173`  
- Swagger UI en desarrollo  

## 🚀 Requisitos

- [.NET 7 SDK](https://dotnet.microsoft.com/download)  
- SQL Server (o LocalDB, Azure SQL…)  
- (Opcional) Postman / Insomnia para probar endpoints  

## 🔧 Instalación y ejecución

1. Clona el repositorio  
   ```bash
   git clone https://github.com/sestradapaggo/DulcesChurrascosApi.git
   cd DulcesChurrascosApi
Ajusta la cadena de conexión en appsettings.json

"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=DulcesChurrascos;Trusted_Connection=True;"
}

Crea y aplica las migraciones

dotnet tool install --global dotnet-ef          # si no tienes EF CLI
dotnet ef migrations add InitialCreate
dotnet ef database update
Corre la API

dotnet run
Quedará expuesta en https://localhost:5001 y http://localhost:5000

Swagger UI en https://localhost:5001/swagger

📖 Endpoints
Verbo	Ruta	Descripción
GET	/api/productos	Lista todos los productos
GET	/api/productos/{id}	Obtiene un producto por id
POST	/api/productos	Crea un producto
PUT	/api/productos/{id}	Actualiza un producto
DELETE	/api/productos/{id}	Elimina un producto
GET	/api/inventarios	Lista inventarios (incluye detalles)
GET	/api/inventarios/{id}	Obtiene inventario por id
POST	/api/inventarios	Crea inventario
PUT	/api/inventarios/{id}	Actualiza inventario
DELETE	/api/inventarios/{id}	Elimina inventario
GET	/api/combos	Lista combos
GET	/api/combos/{id}	Obtiene un combo por id
POST	/api/combos	Crea un combo
PUT	/api/combos/{id}	Actualiza un combo
DELETE	/api/combos/{id}	Elimina un combo
GET	/api/ventas	Lista ventas
GET	/api/ventas/{id}	Obtiene una venta por id
POST	/api/ventas	Registra una venta
PUT	/api/ventas/{id}	Actualiza una venta
DELETE	/api/ventas/{id}	Elimina una venta
GET	/api/churrascos	Lista churrascos
GET	/api/churrascos/{id}	Obtiene un churrasco por id
POST	/api/churrascos	Crea un churrasco (valida guarniciones)
PUT	/api/churrascos/{id}	Actualiza un churrasco
DELETE	/api/churrascos/{id}	Elimina un churrasco
GET	/api/dulces	Lista dulces
GET	/api/dulces/{id}	Obtiene un dulce por id
POST	/api/dulces	Crea un dulce (valida cajas)
PUT	/api/dulces/{id}	Actualiza un dulce
DELETE	/api/dulces/{id}	Elimina un dulce
POST	/api/chat	Envía contexto al LLM y devuelve respuesta
