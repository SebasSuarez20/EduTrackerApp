# Frontend - Angular > 16

Este proyecto frontend fue desarrollado en **Angular superior a la versión 16**, siguiendo una estructura modular y buenas prácticas para escalabilidad, seguridad y mantenibilidad.

## 📁 Estructura del Proyecto

El proyecto está organizado en carpetas como:

- `pages/`: Módulos de cada vista del sistema.
- `components/`: Componentes reutilizables de la interfaz.
- `services/`: Encapsulan la lógica de conexión con el backend.
- `models/`: Tipado TypeScript para las estructuras de datos.
- `guards/`: Validación de rutas y control de acceso.

## 🔐 Seguridad

- **Cifrado AES**:
  - Se implementó cifrado **AES con KEY y IV** para proteger el modelo de login antes de enviarlo al backend.
  - Esto impide que desde las herramientas del navegador (DevTools) se pueda visualizar el contenido sensible enviado.

- **Rutas Protegidas**:
  - Se utilizó `canActivate` para proteger rutas según el **rol del usuario**.
  - Actualmente hay 3 roles activos (por temas de tiempo no se completaron todos).

## 🌐 Comunicación con el Backend

- **Http Interceptors**:
  - Se implementó un **HttpInterceptor** que inyecta el **token JWT** en cada petición HTTP saliente.
  - Esto asegura que todas las llamadas autenticadas incluyan el token sin intervención manual.

- **Servicios con Observables**:
  - Se implementan servicios que retornan **Observables**.
  - Los componentes se **suscriben** para manejar la respuesta asíncrona desde el backend.
  
- **Tipado Genérico**:
  - Las peticiones están tipadas mediante **generics** en TypeScript para validar automáticamente los tipos de respuesta esperados.

## 💬 Librerías y Funcionalidades

- **SweetAlert2** para notificaciones elegantes al usuario.
- 90% del proyecto está modelado con interfaces y clases.
- El sistema está preparado para producción, pero aún **no se ejecuta el build final** ya que está en modo de **prueba técnica**.

## ✅ Funcionalidad Destacada

- **CRUD completo del módulo de estudiantes** finalizado y funcionando.