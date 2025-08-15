# TaskScheduler API

Uma API RESTful para gerenciamento de tarefas com autenticação JWT, desenvolvida em .NET 9 com Entity Framework Core e MySQL.

## 🚀 Funcionalidades

- **Autenticação JWT**: Sistema completo de registro e login de usuários
- **Gerenciamento de Tarefas**: CRUD completo para tarefas pessoais
- **Filtros Avançados**: Busca por status, data, título com paginação
- **Segurança**: Senhas criptografadas com BCrypt
- **Documentação**: API documentada com OpenAPI/Swagger
- **Middleware de Exceções**: Tratamento centralizado de erros
- **Validação**: Validação robusta de dados de entrada

## 🛠️ Tecnologias Utilizadas

- **.NET 9**: Framework principal
- **Entity Framework Core**: ORM para acesso a dados
- **MySQL**: Banco de dados relacional
- **JWT Bearer**: Autenticação e autorização
- **BCrypt.Net**: Criptografia de senhas
- **Scalar**: Documentação interativa da API
- **AutoMapper**: Mapeamento de objetos (via extensões customizadas)

## 📋 Pré-requisitos

- .NET 9 SDK
- MySQL Server
- IDE de sua preferência (Visual Studio, VS Code, Rider)

## ⚙️ Configuração

### 1. Clone o repositório
```bash
git clone <url-do-repositorio>
cd TaskScheduler.API
```

### 2. Configure o banco de dados
Edite o arquivo `appsettings.json` ou `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "mysql": "Server=localhost;Database=task_api;Uid=seu_usuario;Pwd=sua_senha;"
  },
  "JwtSettings": {
    "Key": "sua-chave-secreta-muito-longa-e-segura",
    "Issuer": "seu-issuer",
    "Audience": "sua-audience"
  }
}
```

### 3. Execute as migrações
```bash
dotnet ef database update
```

### 4. Execute a aplicação
```bash
dotnet run
```

A API estará disponível em:
- HTTP: `http://localhost:5036`
- HTTPS: `https://localhost:7066`
- Documentação: `https://localhost:7066/docs`

## 🔗 Endpoints

### Autenticação
| Método | Endpoint | Descrição |
|--------|----------|-----------|
| POST | `/register` | Registrar novo usuário |
| POST | `/login` | Fazer login e obter token JWT |

### Tarefas (Requer autenticação)
| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/task` | Listar tarefas com filtros |
| POST | `/api/task` | Criar nova tarefa |
| PUT | `/api/task` | Atualizar tarefa existente |
| DELETE | `/api/task/{id}` | Deletar tarefa |

### Filtros Disponíveis (GET /api/task)
- `status`: Filtrar por status (ToDo, InProgress, Completed, Canceled)
- `fromDate`: Data inicial
- `toDate`: Data final
- `titleContains`: Buscar por título
- `page`: Número da página (padrão: 1)
- `pageSize`: Itens por página (padrão: 10, máximo: 100)

## 📝 Exemplos de Uso

### Registrar usuário
```bash
curl -X POST https://localhost:7066/register \
  -H "Content-Type: application/json" \
  -d '{
    "email": "usuario@exemplo.com",
    "password": "MinhaSenh@123",
    "username": "MeuUsuario"
  }'
```

### Fazer login
```bash
curl -X POST https://localhost:7066/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "usuario@exemplo.com",
    "password": "MinhaSenh@123"
  }'
```

### Criar tarefa
```bash
curl -X POST https://localhost:7066/api/task \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer SEU_TOKEN_JWT" \
  -d '{
    "title": "Minha primeira tarefa",
    "description": "Descrição da tarefa",
    "finishDate": "2024-12-31T23:59:59",
    "status": "ToDo"
  }'
```

### Listar tarefas com filtros
```bash
curl "https://localhost:7066/api/task?status=ToDo&page=1&pageSize=10" \
  -H "Authorization: Bearer SEU_TOKEN_JWT"
```

## 🏗️ Estrutura do Projeto

```
TaskScheduler.API/
├── Domain/
│   ├── Controllers/     # Controladores da API
│   ├── DTOs/           # Data Transfer Objects
│   ├── Enums/          # Enumerações
│   ├── Exceptions/     # Exceções customizadas
│   ├── Interfaces/     # Interfaces de serviços e repositórios
│   ├── Mappers/        # Mapeadores de objetos
│   ├── Models/         # Modelos de entidades
│   ├── Repositories/   # Implementações de repositórios
│   └── Services/       # Lógica de negócio
├── Extensions/         # Extensões para configuração
├── Infrastructure/     # Configuração de banco de dados
├── Middlewares/        # Middlewares customizados
├── Migrations/         # Migrações do Entity Framework
└── ModelViews/         # Modelos de resposta da API
```

## 🔒 Status das Tarefas

- `ToDo`: Tarefa pendente
- `InProgress`: Tarefa em andamento
- `Completed`: Tarefa concluída
- `Canceled`: Tarefa cancelada

## 🚀 Modelo de Resposta

A API segue o padrão RFC 7807 para respostas de erro e inclui metadados nas respostas de sucesso:

### Resposta de Sucesso
```json
{
  "type": "https://datatracker.ietf.org/doc/html/rfc9110#name-200-ok",
  "status": 200,
  "title": "Tasks retrieved",
  "detail": "Tasks fetched successfully",
  "instance": "/api/task",
  "data": [...]
}
```

### Resposta de Erro
```json
{
  "type": "https://datatracker.ietf.org/doc/html/rfc9110#status.400",
  "status": 400,
  "title": "Bad Request",
  "detail": "Email Field Is Required.",
  "instance": "/register"
}
```

## 🔧 Health Check

A API inclui um endpoint de health check:
- `GET /health` - Verifica o status da aplicação

## 📚 Documentação

A documentação interativa da API está disponível em `/docs` quando executada em ambiente de desenvolvimento.

## 🤝 Contribuindo

1. Faça um fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/MinhaFeature`)
3. Commit suas mudanças (`git commit -m 'Adiciona nova feature'`)
4. Push para a branch (`git push origin feature/MinhaFeature`)
5. Abra um Pull Request

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.
