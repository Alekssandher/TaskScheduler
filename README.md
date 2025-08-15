# Minimal API - Sistema de Gerenciamento de Veículos

Uma API RESTful desenvolvida em .NET 9 usando Minimal API para gerenciamento de veículos e administradores.

## 📋 Funcionalidades

### Administradores
- Login com autenticação JWT
- Listagem de administradores (paginada)
- Busca por ID
- Criação de novos administradores
- Dois perfis: Admin e Editor

### Veículos
- CRUD completo (Create, Read, Update, Delete)
- Listagem paginada com filtros por nome e marca
- Busca por ID
- Validações de dados

## 🛠️ Tecnologias Utilizadas

- **Framework**: .NET 9
- **Banco de Dados**: MySQL
- **ORM**: Entity Framework Core 9.0.8
- **Autenticação**: JWT Bearer
- **Documentação**: Scalar/OpenAPI
- **Arquitetura**: Clean Architecture com Domain-Driven Design

## 📁 Estrutura do Projeto

```
API/
├── Domain/
│   ├── DTOs/              # Data Transfer Objects
│   ├── Entities/          # Entidades do domínio
│   ├── Enums/             # Enumerações
│   ├── Interfaces/        # Interfaces dos serviços
│   ├── ModelViews/        # ViewModels
│   └── Services/          # Implementação dos serviços
├── Infrastructure/
│   └── Db/               # Contexto do Entity Framework
├── Migrations/           # Migrações do banco de dados
├── Properties/           # Configurações do projeto
├── Program.cs           # Ponto de entrada da aplicação
├── Startup.cs           # Configuração da aplicação
└── appsettings.json     # Configurações da aplicação
```

## ⚙️ Configuração

### Pré-requisitos

- .NET 9 SDK
- MySQL Server
- IDE (Visual Studio, VS Code, Rider, etc.)

### Configuração do Banco de Dados

1. Configure a string de conexão no `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "mysql": "Server=localhost;Database=minimal_api;Uid=seu_usuario;Pwd=sua_senha;"
  }
}
```

2. Execute as migrações para criar o banco:
```bash
dotnet ef database update
```

### Configuração JWT

A chave JWT está configurada no `appsettings.json`:
```json
{
  "JwtSettings": {
    "Key": "rieladoravantenestantefoi-separaummundodistante"
  }
}
```

## 🚀 Como Executar

1. Clone o repositório
2. Navegue até a pasta do projeto
3. Restaure as dependências:
```bash
dotnet restore
```

4. Execute o projeto:
```bash
dotnet run
```

5. Acesse a documentação em: `http://localhost:5278/docs`

## 🔐 Autenticação

O sistema utiliza JWT Bearer Token para autenticação. 

### Login Padrão
- **Email**: admin@admin.com
- **Senha**: 1234

### Como usar:
1. Faça login através do endpoint `/admin/login`
2. Copie o token retornado
3. Use o token no header Authorization: `Bearer {seu_token}`

## 📚 Endpoints da API

### Administradores

| Método | Endpoint | Descrição | Autenticação |
|--------|----------|-----------|--------------|
| POST | `/admin/login` | Login do administrador | Não |
| GET | `/admin` | Listar administradores | Admin |
| GET | `/admin/{id}` | Buscar admin por ID | Admin |
| POST | `/admin/create` | Criar novo administrador | Admin |

### Veículos

| Método | Endpoint | Descrição | Autenticação |
|--------|----------|-----------|--------------|
| POST | `/vehicles` | Criar veículo | Editor |
| GET | `/vehicles` | Listar veículos | Editor |
| GET | `/vehicles/{id}` | Buscar veículo por ID | Editor |
| PUT | `/vehicles/{id}` | Atualizar veículo | Admin |
| DELETE | `/vehicles/{id}` | Excluir veículo | Admin |

### Filtros para Listagem de Veículos

- `page`: Número da página (padrão: 1)
- `name`: Filtro por nome do veículo
- `brand`: Filtro por marca do veículo

Exemplo: `/vehicles?page=1&name=civic&brand=honda`

## 🏗️ Modelos de Dados

### Admin
```json
{
  "email": "admin@exemplo.com",
  "password": "senha123",
  "profile": "Admin" // ou "Editor"
}
```

### Vehicle
```json
{
  "name": "Civic",
  "brand": "Honda",
  "year": 2020
}
```

## 🔒 Perfis de Usuário

- **Admin**: Acesso completo (CRUD de veículos e administradores)
- **Editor**: Pode criar e visualizar veículos

## ✅ Validações

### Administrador
- Email obrigatório e deve ser um endereço válido
- Senha obrigatória (máx. 100 caracteres)
- Perfil obrigatório (Admin ou Editor)

### Veículo
- Nome obrigatório (máx. 150 caracteres)
- Marca opcional (máx. 100 caracteres)
- Ano obrigatório e deve ser >= 1950

## 📋 Status Codes

- `200 OK`: Sucesso
- `201 Created`: Recurso criado com sucesso
- `400 Bad Request`: Dados inválidos
- `401 Unauthorized`: Token inválido ou ausente
- `403 Forbidden`: Sem permissão para acessar o recurso
- `404 Not Found`: Recurso não encontrado

## 🔧 Scripts Úteis

```bash
# Restaurar dependências
dotnet restore

# Compilar o projeto
dotnet build

# Executar o projeto
dotnet run

# Executar em modo de desenvolvimento
dotnet watch run

# Criar nova migração
dotnet ef migrations add NomeDaMigracao

# Atualizar banco de dados
dotnet ef database update
```

## 📝 Licença

Este projeto está sob a licença MIT.
