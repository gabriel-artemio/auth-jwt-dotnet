# 🔐 Auth JWT API - .NET 8

API de autenticação utilizando JWT (JSON Web Token) com ASP.NET Core.

## 🚀 Tecnologias

- .NET 8
- JWT Authentication
- Swagger
- C#

---

## 📦 Funcionalidades

- 🔐 Login com geração de token JWT
- 👤 Controle de acesso por Roles (Admin/User)
- 🔒 Rotas protegidas
- 📄 Swagger integrado

---

## ⚙️ Configuração

No `appsettings.json`:

```json
"Jwt": {
  "Key": "SUA_CHAVE_SECRETA",
  "Issuer": "AuthApi",
  "Audience": "AuthApiUsers",
  "ExpireMinutes": 60
}
