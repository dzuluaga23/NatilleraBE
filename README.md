# 💰 NatilleraBE

### API REST para la gestión integral de una natillera

**NatilleraBE** es un backend desarrollado para gestionar de forma centralizada las operaciones financieras de una **natillera**, permitiendo administrar socios, préstamos, pagos, abonos, intereses y demás movimientos relacionados con la gestión del sistema.

El proyecto fue construido como una **API REST con ASP.NET Core .NET 8**, utilizando **Entity Framework Core** para la persistencia de datos y **SQL Server** como base de datos.

Además, incorpora **autenticación mediante JWT**, separación entre DTOs, servicios y controladores, y documentación interactiva mediante Swagger/OpenAPI.

> 💡 El objetivo principal del proyecto es transformar la administración tradicional de una natillera en un sistema digital, centralizado y preparado para ser consumido por cualquier aplicación frontend.

---

## 🚀 ¿Qué problema resuelve?

La administración de una natillera puede involucrar una gran cantidad de información:

* 👥 Socios y participantes.
* 💰 Aportes y abonos.
* 🏦 Movimientos bancarios.
* 💳 Préstamos.
* 📈 Cálculo y gestión de intereses.
* 💵 Pagos.
* 🎯 Juegos o actividades como pollas.
* 🔐 Usuarios y roles.

Cuando esta información se maneja manualmente, es fácil cometer errores, perder información o dificultar el seguimiento financiero.

**NatilleraBE centraliza estas operaciones mediante una API**, permitiendo que diferentes clientes —por ejemplo, una aplicación web o móvil— puedan consumir los mismos servicios.

---

# ✨ Funcionalidades

### 👥 Gestión de socios

Permite administrar la información de los integrantes de la natillera.

```text
Socios
├── Crear
├── Consultar
├── Actualizar
└── Eliminar
```

### 💳 Gestión de préstamos

El sistema contempla operaciones relacionadas con los préstamos realizados dentro de la natillera.

```text
Préstamo
   │
   ├── Información del préstamo
   ├── Intereses
   ├── Pagos
   └── Abonos
```

### 💰 Gestión de pagos y abonos

Permite registrar y consultar movimientos relacionados con los pagos y abonos realizados por los participantes.

### 📈 Gestión de intereses

El backend cuenta con componentes específicos para manejar intereses relacionados tanto con pagos como con préstamos.

### 🏦 Gestión bancaria

Incluye operaciones relacionadas con la información bancaria y movimientos asociados al sistema.

### 🎯 Gestión de "Pollas"

El proyecto incorpora funcionalidades específicas para gestionar las denominadas **pollas**, integrándolas como parte del sistema de administración de la natillera.

### 🔐 Autenticación y roles

La API utiliza **JWT Bearer Authentication**, permitiendo proteger los recursos y establecer mecanismos de autenticación y autorización.

---

# 🧠 Arquitectura

El proyecto está organizado siguiendo una separación de responsabilidades entre las diferentes capas de la aplicación.

```text
                         CLIENTE
                           │
                           │ HTTP / JSON
                           ▼
                  ┌──────────────────┐
                  │   Controllers    │
                  │                  │
                  │ Endpoints REST   │
                  └────────┬─────────┘
                           │
                           ▼
                  ┌──────────────────┐
                  │      DTOs        │
                  │                  │
                  │ Datos de entrada │
                  │ y salida         │
                  └────────┬─────────┘
                           │
                           ▼
                  ┌──────────────────┐
                  │     Services     │
                  │                  │
                  │ Lógica de negocio│
                  └────────┬─────────┘
                           │
                           ▼
                  ┌──────────────────┐
                  │      Data        │
                  │                  │
                  │ Entity Framework │
                  └────────┬─────────┘
                           │
                           ▼
                  ┌──────────────────┐
                  │    SQL Server    │
                  └──────────────────┘
```

Esta organización permite mantener separadas las responsabilidades principales de la aplicación y facilita la evolución del backend.

---

# 🛠️ Tecnologías

| Tecnología                     | Uso                            |
| ------------------------------ | ------------------------------ |
| 🟣 **C#**                      | Lenguaje principal             |
| 🟦 **.NET 8**                  | Framework                      |
| 🌐 **ASP.NET Core Web API**    | Construcción de la API REST    |
| 🗄️ **SQL Server**             | Base de datos                  |
| 🔷 **Entity Framework Core 8** | ORM y acceso a datos           |
| 🔐 **JWT Bearer**              | Autenticación                  |
| 📦 **DTOs**                    | Transferencia de datos         |
| ⚙️ **Services**                | Lógica de negocio              |
| 📚 **Swagger / OpenAPI**       | Documentación y pruebas        |
| 🔄 **Newtonsoft.Json**         | Serialización y manejo de JSON |

Las dependencias principales del proyecto incluyen ASP.NET Core JWT Bearer, Entity Framework Core para SQL Server, Newtonsoft.Json y Swashbuckle para Swagger.

---

# 📁 Estructura del proyecto

```text
NatilleraBE/
│
├── Controllers/
│   ├── AbonosController.cs
│   ├── BancoController.cs
│   ├── InteresPagoController.cs
│   ├── InteresPrestamoController.cs
│   ├── PagosController.cs
│   ├── PollasController.cs
│   ├── PrestamosController.cs
│   ├── RolesController.cs
│   └── SociosController.cs
│
├── DTOs/
│   └── Objetos para transferencia de datos
│
├── Data/
│   └── Contexto y acceso a datos
│
├── Models/
│   └── Entidades del sistema
│
├── Services/
│   ├── clsAbonos.cs
│   ├── clsBanco.cs
│   ├── clsInteresPago.cs
│   ├── clsInteresPrestamo.cs
│   ├── clsPago.cs
│   ├── clsPolla.cs
│   ├── clsPrestamo.cs
│   └── clsSocio.cs
│
├── Utils/
│   └── Utilidades auxiliares
│
├── Properties/
│
├── Program.cs
├── appsettings.json
├── appsettings.Development.json
└── NatilleraBE.csproj
```

La estructura actual del repositorio separa explícitamente `Controllers`, `DTOs`, `Data`, `Models`, `Services` y `Utils`, además de la configuración principal de ASP.NET Core.

---

# 🔐 Autenticación

La API utiliza **JSON Web Tokens (JWT)** para la autenticación.

El flujo general es:

```text
             Usuario
                │
                │ Credenciales
                ▼
        ┌───────────────┐
        │ Authentication│
        └───────┬───────┘
                │
                │ JWT
                ▼
        ┌───────────────┐
        │  Authorization │
        └───────┬───────┘
                │
                ▼
        ┌───────────────┐
        │   Protected   │
        │   Endpoints   │
        └───────────────┘
```

Esto permite que los endpoints que requieren autenticación puedan validar la identidad del cliente antes de ejecutar determinadas operaciones.

---

# 🔌 API REST

Los recursos principales de la API están organizados mediante diferentes controladores:

| Controller                  | Responsabilidad                 |
| --------------------------- | ------------------------------- |
| `SociosController`          | Gestión de socios               |
| `PrestamosController`       | Gestión de préstamos            |
| `PagosController`           | Gestión de pagos                |
| `AbonosController`          | Gestión de abonos               |
| `BancoController`           | Operaciones bancarias           |
| `InteresPagoController`     | Intereses asociados a pagos     |
| `InteresPrestamoController` | Intereses asociados a préstamos |
| `PollasController`          | Gestión de pollas               |
| `RolesController`           | Gestión de roles                |

Estos controladores representan los principales recursos expuestos por el backend.

---

# 📚 Swagger

El proyecto utiliza **Swagger/OpenAPI** para documentar y probar la API.

Una vez ejecutado el proyecto, puedes acceder a:

```text
https://localhost:<puerto>/swagger
```

Desde Swagger es posible:

* 📖 Consultar los endpoints.
* 🔎 Revisar parámetros.
* 📦 Visualizar modelos.
* 🧪 Ejecutar solicitudes.
* 🔐 Probar endpoints protegidos.

---

# ⚙️ Requisitos

Para ejecutar el proyecto necesitas:

* [.NET 8 SDK](https://dotnet.microsoft.com/)
* SQL Server
* Git
* Visual Studio, Visual Studio Code o cualquier IDE compatible con .NET.

---

# 🚀 Instalación

## 1. Clonar el repositorio

```bash
git clone https://github.com/dzuluaga23/NatilleraBE.git
```

## 2. Entrar al proyecto

```bash
cd NatilleraBE
```

## 3. Restaurar dependencias

```bash
dotnet restore
```

## 4. Configurar SQL Server

Configura la cadena de conexión en:

```text
NatilleraBE/appsettings.json
```

Ejemplo:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=TU_SERVIDOR;Database=Natillera;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

> ⚠️ No incluyas credenciales reales ni información sensible en el repositorio.

## 5. Ejecutar la aplicación

```bash
dotnet run
```

La consola mostrará las URLs donde estará disponible la API.

---

# 🗄️ Base de datos

El acceso a los datos se realiza mediante:

```text
ASP.NET Core
      │
      ▼
Entity Framework Core
      │
      ▼
SQL Server
```

Entity Framework Core permite trabajar con las entidades del dominio desde C# y gestionar las operaciones de persistencia hacia SQL Server.

---

# 🔄 Flujo de una operación

Por ejemplo, una solicitud para registrar un préstamo puede seguir un flujo similar a:

```text
Cliente
   │
   │ POST /Prestamos
   ▼
PrestamosController
   │
   ▼
DTO
   │
   ▼
Prestamo Service
   │
   ▼
Entity Framework Core
   │
   ▼
SQL Server
   │
   ▼
Response JSON
   │
   ▼
Cliente
```

Este enfoque permite mantener la API organizada y separar la recepción de solicitudes, la lógica de negocio y el acceso a los datos.

---

# 🎯 Objetivos del proyecto

NatilleraBE fue desarrollado para construir una solución backend capaz de centralizar la administración financiera de una natillera.

Durante el desarrollo se trabajaron conceptos como:

* Desarrollo de APIs REST.
* Programación orientada a objetos con C#.
* ASP.NET Core.
* Entity Framework Core.
* SQL Server.
* Autenticación mediante JWT.
* Diseño y consumo de endpoints.
* DTOs.
* Separación de responsabilidades.
* Servicios para lógica de negocio.
* Serialización JSON.
* Documentación de APIs.
* Arquitectura cliente-servidor.

---

# 💡 ¿Qué demuestra este proyecto?

NatilleraBE representa un proyecto donde el backend no se limita a realizar operaciones CRUD simples.

La aplicación requiere manejar diferentes entidades y relaciones financieras, además de operaciones relacionadas con préstamos, intereses, pagos y abonos.

Esto permite demostrar experiencia práctica trabajando con:

```text
             API REST
                │
       ┌────────┼────────┐
       ▼        ▼        ▼
     Auth    Business   Data
     JWT      Logic    Access
       │        │        │
       └────────┼────────┘
                ▼
           SQL Server
```

El proyecto también puede funcionar como backend para diferentes clientes, como una aplicación web, móvil o escritorio.

---

# 🔗 Repositorio

**GitHub**

https://github.com/dzuluaga23/NatilleraBE

---

# 👨‍💻 Autor

## David Zuluaga

**Software Developer | Full-Stack Developer**

Interesado en el desarrollo backend, aplicaciones web y construcción de soluciones de software.

### GitHub

https://github.com/dzuluaga23

---

## 📄 Licencia

Proyecto desarrollado con fines académicos y de aprendizaje.
