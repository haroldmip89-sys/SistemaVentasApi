🚀 SistemaVentas API - Backend

Esta es la API robusta del Sistema de Ventas, construida bajo los principios de Clean Architecture y Domain-Driven Design (DDD) para garantizar un sistema escalable, mantenible y altamente desacoplado.
🛠️ Stack Tecnológico

    Framework: .NET 8 (ASP.NET Core)

    Base de Datos: PostgreSQL

    ORM: Entity Framework Core

    Patrones: CQRS, Repository Pattern, Mediator Pattern

    Librerías Clave:

        MediatR: Para el desacoplamiento de comandos y consultas.

        FluentValidation: Para una validación de datos limpia y robusta.

        MailKit & MimeKit: Para la gestión de servicios de correo electrónico.

        JWT: Para la seguridad y autorización de usuarios.

🏗️ Arquitectura del Proyecto

El proyecto se divide en capas siguiendo el modelo de Clean Architecture:

    Ventas.Dominio: Contiene las entidades, interfaces base y lógica pura del negocio.

    Ventas.Aplicacion: Define los casos de uso (Handlers), DTOs, validaciones y contratos de servicios. Aquí reside la implementación de MediatR.

    Ventas.Infraestructura: Implementación de acceso a datos (DbContext), Repositorios, Procedimientos Almacenados y servicios externos (Email).

    SistemaVentasAPI (Web API): Punto de entrada del sistema, controladores y configuración de la aplicación.

⚙️ Características Destacadas

    Gestión de Ventas: Registro de transacciones complejas utilizando Stored Procedures para asegurar la integridad de los datos.

    Generación de Comprobantes: Sistema automatizado para emitir comprobantes electrónicos en formato HTML.

    Seguridad: Implementación de autenticación basada en tokens JWT.

    Automatización: Procesos de backend para actualización de inventarios y validación de stock en tiempo real.