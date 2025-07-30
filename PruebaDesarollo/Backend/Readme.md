# Backend - API .NET 7.0

Este proyecto backend fue desarrollado en **.NET 7.0**, utilizando una arquitectura en capas y buenas prácticas de desarrollo para garantizar escalabilidad, mantenibilidad y seguridad.

## 🧱 Arquitectura

Se implementa una arquitectura por **capas**:

- **Capa de Datos**: Maneja la conexión a la base de datos, acceso mediante procedimientos almacenados y consultas directas.
- **Capa de Lógica**: Contiene los servicios, reglas de negocio y lógica de aplicación.
- **Modelado y DTOs**: 
  - Uso de modelos para reflejar entidades de la base de datos.
  - Uso de **DTOs** para controlar las respuestas hacia el cliente.

## 🧩 Patrón Repository con Generics

- Se implementa el **patrón IGenericRepository** para encapsular operaciones CRUD (`Insert`, `Select`, `Update`, `Delete`) de forma reutilizable.
- Este patrón permite desacoplar la lógica de acceso a datos de la lógica del negocio.

## 🔒 Seguridad

- **Cifrado de Contraseña**: SHA256 utilizado para proteger contraseñas de usuarios.
- **Autenticación JWT**:
  - El sistema utiliza **JSON Web Tokens**.
  - La información del usuario se filtra desde el `payload` del token.
  - Se aplica validación por roles para el acceso a recursos.

## ⚙️ Manejo de Procedimientos Almacenados

- Se usan procedimientos almacenados para manipulación de datos.
- Se implementa control transaccional mediante `ROLLBACK` y `COMMIT`.

## 🛠️ Inyección de Dependencias

- El sistema está configurado con **inyección de dependencias** para desacoplar y facilitar el mantenimiento de servicios y repositorios.

## 📦 Funcionalidades Adicionales

- Se planificó la integración de un **File Server** para el manejo de archivos (imágenes u otros), aunque no se finalizó por cuestiones de tiempo.

## ✅ Buenas Prácticas Aplicadas

- Uso de patrones como Repository y DTO.
- Separación de responsabilidades.
- Modelado estructurado para las entidades.
- Validación de datos de entrada y salidas controladas.