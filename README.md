# 🌐 Taller 1: Introducción al desarrollo web/móvil

## 📌 Integrantes

- Nathalia Olivarez Bugueño (21376495-0) nathalia.olivarez@alumnos.ucn.cl
- Alexander Rivera Velasquez (21354394-6) alexander.rivera01@alumnos.ucn.cl

## 📖 Descripción

Este repositorio contiene un proyecto escrito utilizando el lenguaje de programación C# con el entorno de .NET el cual posee un conjunto de controladores, entidades y un conjunto de endpoints para administrar un servicio de comercio electrónico.

---

## Tecnologías

El proyecto utiliza las siguientes librerías y tecnologías:

- **C#**: Lenguaje de programación.
- **.NET 8**: Framework para construir la API REST.
- **SQLite**: Base de datos para almacenar los datos del proyecto.
- **JWT**: Autenticación mediante tokens seguros.
- **Postman**: Herramienta para pruebas y documentación de los endpoints.

## ⚙️ Requisitos Previos

Asegúrate de tener instalado:

1. [.NET SDK 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
2. [SQLite](https://www.sqlite.org/download.html)
3. **Git** para clonar el repositorio.
4. **Postman** (opcional, para probar los endpoints)

---

## 🚀 Construcción

### 1️⃣ Clonar el Repositorio

Clonar el repositorio utilizando git

```bash
  git clone https://github.com/nathyxddd/TallerWebMov
```

### 2️⃣ Ir a la carpeta que contiene el proyecto

```bash
  cd TallerWebMov
```

---

### 4️⃣ Migraciones de Base de Datos

Este comando generará un archivo de migración que define la estructura de la base de datos.

1. **Aplicar las migraciones para crear la base de datos:**
   Ejecuta el siguiente comando para aplicar la migración y crear la base de datos:
   ```bash
   dotnet ef database update
   ```

---

### 2 Ejecutar el Proyecto

Una vez completados los pasos anteriores, puedes iniciar el servidor localmente con el siguiente comando:

```bash
  dotnet run
```
