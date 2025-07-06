# Backend

Este proyecto corresponde al backend de una aplicación web desarrollada como parte del trabajo final del curso de Finanzas e Ingeniería Económica. El sistema permite proyectar el flujo de caja de un bono corporativo utilizando el **método alemán**, con soporte para plazos de gracia, configuraciones financieras y cálculos financieros clave.

## 📌 Funciones

- Autenticación de usuarios con usuario y contraseña.
- Registro, edición y eliminación de bonos corporativos.
- Proyección del flujo de caja de un bono con método alemán.
- Soporte para plazos de gracia totales o parciales.
- Cálculo de:
  - Duración
  - Duración modificada
  - Convexidad
  - TCEA (Tasa de Coste Efectivo Anual) desde el punto de vista del emisor.
  - TREA (Tasa de Rendimiento Efectivo Anual) desde el punto de vista del inversor.
  - Precio máximo de mercado que se estaría dispuesto a pagar por el bono.

## ⚙️ Tecnologías usadas
- C#
- ASP .Net Core 8.0
- Entity Framework Core
- MySQL
- OpenAPI (Swagger)