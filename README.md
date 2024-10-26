# Obligatorio - Parte II

## Descripción
Este proyecto expande el sistema desarrollado en el primer obligatorio, implementando una aplicación web en **ASP.NET 8.0 MVC** con autenticación de usuario y control de acceso basado en roles. La aplicación permite funcionalidades específicas dependiendo del rol de usuario, asegurando que solo los usuarios autorizados puedan acceder a ciertas secciones.

## Requisitos
Se requiere implementar las siguientes funcionalidades:

### 1. Diseño
- **Diagrama de Clases**: Un modelo completo en UML que refleje todos los cambios necesarios para cumplir con los nuevos requerimientos.
- **Diagrama de Casos de Uso**: Incluido en formato **Astah** y en la documentación digital.

### 2. Implementación
Desarrollar las siguientes funcionalidades usando **ASP.NET 8.0 MVC** con **C#** en **Visual Studio 2022**:

#### Usuario Anónimo
1. **Registro**: Permite registrarse como cliente con una contraseña alfanumérica de al menos 8 caracteres.
2. **Login**: Acceso mediante email y contraseña.

#### Usuario Cliente
3. **Ver Publicaciones**: Visualizar todas las publicaciones, con opción de comprar o realizar una oferta en publicaciones abiertas.
4. **Compra de Publicación**: Posibilidad de realizar una compra si dispone de saldo suficiente.
5. **Ofertas en Subastas**: Realizar ofertas en subastas con valores superiores a la oferta actual más alta.
6. **Cargar Saldo**: Agregar saldo en su billetera electrónica (valor positivo).
7. **Logout**: Cerrar sesión.

#### Usuario Administrador
8. **Ver Subastas**: Listar todas las subastas, con opción de cerrar subastas abiertas.
9. **Cerrar Subasta**: Adjudicar una subasta al mejor oferente.
10. **Logout**: Cerrar sesión.

### 3. Documentación
Incluir un documento PDF con:
- Portada con nombre, número y foto de cada integrante, grupo, y docente.
- Diagrama de Clases del Dominio.
- Tabla de datos precargados en el sistema.
- Código fuente comentado.
- Planilla con casos de prueba y evidencia de testing.
- Diagrama de Casos de Uso de todas las funcionalidades.
- Evidencias de consultas a IA para la precarga.

### 4. Despliegue en Azure
El proyecto debe desplegarse en **Azure** y ser accesible mediante una URL, la cual debe estar indicada en la documentación.

## Herramientas Requeridas
- **Visual Studio 2022**
- **ASP.NET 8.0**
- **Azure** (para despliegue)

## Integración Continua y Despliegue
Para facilitar el despliegue en **Azure** y asegurar la calidad del código.
