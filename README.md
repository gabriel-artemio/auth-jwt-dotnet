# 🔐 AuthJwtWebApi

API de autenticação desenvolvida em **.NET** com suporte a:

* ✅ JWT (JSON Web Token)
* 🔐 Hash de senha com BCrypt
* 👮 Controle de acesso por Roles (Admin/User)
* 📄 Swagger para testes
* 🗄️ Banco SQLite

---

## 🚀 Funcionalidades

* 🔑 Login com geração de token JWT
* 👤 Cadastro de usuários (somente Admin)
* 🔐 Senhas protegidas com hash (BCrypt)
* 👮 Autorização baseada em Roles
* 📦 API REST simples e organizada

---

## 🧱 Tecnologias utilizadas

* .NET Web API
* SQLite
* JWT Bearer Authentication
* BCrypt.Net
* Swagger (OpenAPI)

---

## ⚙️ Configuração

### Configurar `appsettings.json`

```json
"Jwt": {
  "Key": "SUA_CHAVE_SECRETA_AQUI",
  "Issuer": "sua-api",
  "Audience": "sua-api-client"
},
"ConnectionStrings": {
  "DefaultConnection": "Data Source=auth.db"
}
```

---

## 🔑 Autenticação (JWT)

### ▶️ Login

```http
POST /api/auth/login
```

### 📥 Body

```json
{
  "email": "admin@admin.com",
  "senha": "123"
}
```

### 📤 Response

```json
{
  "token": "SEU_TOKEN_JWT"
}
```

---

## 🔒 Autorização

### Usando no Postman

1. Clique em **Authorize 🔒**
2. Insira:

```
Bearer SEU_TOKEN_AQUI
```

---

## 👮 Roles

| Role  | Permissões     |
| ----- | -------------- |
| Admin | Criar usuários |
| User  | Acesso básico  |

---

## 👤 Criar usuário (Admin)

```http
POST /api/user
```

🔐 Requer token de Admin

### 📥 Body

```json
{
  "nome": "Gabriel",
  "email": "gabriel@gabriel.com",
  "senha": "123",
  "role": "User"
}
```

---

## 🛡️ Segurança

* 🔐 Senhas armazenadas com BCrypt (hash)
* 🚫 Não armazena senha em texto puro
* 🔑 Token JWT com validação de:

  * Issuer
  * Audience
  * Signing Key

---

## 📌 Estrutura do Projeto

```
Controllers/
  AuthController.cs
  UserController.cs
  DashboardController.cs

DAL/
  UserDAL.cs

Models/
  User.cs

Service/
  TokenService.cs
  PasswordHelper.cs
```

---

## 🧪 Endpoints protegidos

```http
GET /api/dashboard
GET /api/dashboard/admin (Admin only)
```

---

## 🚀 Melhorias futuras

* 🔁 Refresh Token
* 📧 Recuperação de senha
* 🧪 Testes automatizados
* 🔐 Policies avançadas

---

## 👨‍💻 Autor

Desenvolvido por Gabriel Artemio