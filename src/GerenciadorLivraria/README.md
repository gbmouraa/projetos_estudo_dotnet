# README — GerenciadorLivraria

## 1. Visão geral

Projeto em **.NET 10** organizado em camadas para gerenciamento de livros via API REST.  
A solução segue arquitetura em camadas (layered architecture) com separação clara entre API, Application, Domain e Infrastructure, utilizando Entity Framework Core com SQLite como banco de dados.

## 2. Stack e dependências

| Camada | Tecnologia |
|---|---|
| **API** | ASP.NET Core Web API, Swagger (Swashbuckle), EF Core (registro de DbContext) |
| **Application** | C#, FluentValidation |
| **Domain** | C# (entidades e enums) |
| **Infrastructure** | EF Core + SQLite + Migrations |

### Dependências externas principais:
- `FluentValidation` — Validação fluente de modelos
- `Microsoft.EntityFrameworkCore` — ORM para acesso a dados
- `Microsoft.EntityFrameworkCore.Sqlite` — Provider SQLite
- `Swashbuckle.AspNetCore` — Documentação automática via Swagger

## 3. Estrutura da solução

```
GerenciadorLivraria.slnx
├── GerenciadorLivraria.API
│   ├── Controllers
│   ├── Filters
│   ├── Requests
│   ├── Responses
│   ├── Data
│   └── Program.cs
├── Application
│   ├── Book
│   │   ├── Services
│   │   ├── Validators
│   │   └── Models
│   └── Exceptions
├── GerenciadorLivraria.Domain
│   ├── Entities
│   └── Enums
└── GerenciadorLivraria.Infrastructure
    ├── Contexts
    └── Migrations
```

### Dependências entre projetos:

```
GerenciadorLivraria.Domain
       ↑            ↑
       │            └─ GerenciadorLivraria.Infrastructure
       │
Application ─────────────→ GerenciadorLivraria.Infrastructure
       ↑
GerenciadorLivraria.API ─→ GerenciadorLivraria.Infrastructure
```

## 4. Modelo de domínio

### 4.1 Entidade `BookEntity`

| Campo | Tipo | Descrição |
|---|---|---|
| `Id` | `Guid` | Identificador único |
| `Title` | `string` | Título do livro (obrigatório) |
| `Author` | `string` | Autor do livro (obrigatório) |
| `Genre` | `ICollection<Genre>` | Relação N:N com gêneros |
| `Price` | `decimal` | Preço do livro (obrigatório, > 0) |
| `Stock` | `int` | Quantidade em estoque (obrigatório) |
| `CreatedAt` | `DateTime` | Data de criação (somente leitura) |
| `UpdatedAt` | `DateTime` | Data de última atualização (set privado) |

### 4.2 Entidade `Genre`

| Campo | Tipo | Descrição |
|---|---|---|
| `Id` | `Guid` | Identificador único |
| `Name` | `string` | Nome do gênero |
| `Books` | `ICollection<BookEntity>` | Relação N:N com livros |
| `CreatedAt` | `DateTime` | Data de criação |
| `UpdatedAt` | `DateTime` | Data de última atualização |

### 4.3 Enum `EnumGenre`

Valores suportados:
1. `Romance`
2. `Filosofia`
3. `Terror`
4. `Tecnologia`

## 5. Persistência (Infrastructure)

- **DbContext:** `GerenciadorLivrariaDbContext` expõe `DbSet<BookEntity>` e `DbSet<Genre>`
- **Provider:** SQLite via Entity Framework Core
- **Migrations:** Migration inicial (`InitialCreate`) criando tabelas:
  - `Books`
  - `Genres`
  - `BookGenre` (tabela de junção N:N)
- **Constraints:** Chaves estrangeiras habilitadas via `PRAGMA foreign_keys = ON` em `OnModelCreating`

## 6. API HTTP

Base route do controller: `api/book`

### 6.1 `GET /api/book`
```
Resposta: 200 OK
Body: "Success my brudaa"
```
**Status:** Endpoint placeholder — não implementado

### 6.2 `GET /api/book/{id}`
```
Resposta: 200 OK
Body: "Success my brudaa"
```
**Status:** Endpoint placeholder — não implementado

### 6.3 `POST /api/book`
Cria um novo livro.

**Request body** (`RequestCreateBookJson`):
```json
{
  "title": "Clean Code",
  "author": "Robert C. Martin",
  "genre": [4],
  "price": 120.50,
  "stock": 10
}
```

**Resposta de sucesso** (`201 Created`):
```json
{
  "id": "00000000-0000-0000-0000-000000000001",
  "title": "Clean Code"
}
```

**Resposta de erro** (`400 Bad Request`):
```json
{
  "errors": [
    "O titulo do livro não pode ser vazio."
  ]
}
```

## 7. Fluxo de criação de livro

1. `BookController.Create` recebe `RequestCreateBookJson`
2. Controller instancia `CreateBookService` manualmente
3. Controller mapeia `Title`, `Author`, `Price` e `Stock` para `CreateBookModel`
4. `CreateBookService.Execute` valida o model via `CreateBookValidator`
5. Em erro de validação, lança `ErrorOnValidationException`
6. Sem erros, persiste o livro via EF Core
7. `ExceptionFilter` padroniza resposta de exceções

## 8. Regras de validação (`CreateBookValidator`)

- ✓ `Title` obrigatório
- ✓ `Author` obrigatório
- ✓ `Genre` deve conter ao menos um item
- ✓ Cada item de `Genre` deve ser valor válido do enum
- ✓ `Price` deve ser `> 0`
- ✓ `Stock` não pode ser negativo

### Inconsistências conhecidas:
- ⚠️ `Genre` não é mapeado no controller → validação de gênero tende a falhar
- ⚠️ Campo `_database` em `CreateBookService` sem injeção de dependência
- ⚠️ Alguns erros de validação podem estar com mensagens incorretas

## 9. Tratamento de exceções

Hierarquia de exceções:
```
GerenciadorLivrariaException (base)
├── ErrorOnValidationException (400 Bad Request)
└── [outras exceções de negócio]
```

Mapeamento:
- `ErrorOnValidationException` → `400 Bad Request`
- Exceções não mapeadas → `500 Internal Server Error` com mensagem genérica

Implementado via `ExceptionFilter` para padronização de respostas.

## 10. Configuração e execução local

### Pré-requisitos:
- .NET 10 SDK instalado

### Passos para execução:

```bash
# Restaurar dependências
dotnet restore

# Build da solução
dotnet build GerenciadorLivraria.slnx

# Executar API
dotnet run --project GerenciadorLivraria.API
```

A API estará disponível em `https://localhost:5000` (ou porta configurada).  
Swagger está habilitado em ambiente `Development` via `/swagger/index.html`.

### Configuração de banco de dados:
- String de conexão configurada em `appsettings.json`
- Banco SQLite criado automaticamente na primeira execução
- Migrations aplicadas automaticamente via `Program.cs`

## 11. Estado atual do projeto

### ✅ Implementado:
- Estrutura de projeto em camadas
- Modelo de domínio (entidades e enums)
- DbContext com migrations iniciais
- Controller com endpoint POST básico
- Validação fluente com FluentValidation
- Tratamento de exceções com exception filter
- Swagger/OpenAPI configurado

### ⚠️ Parcialmente implementado:
- POST /api/book (sem mapeamento completo de gêneros)
- Injeção de dependência do DbContext

### ❌ Não implementado:
- GET /api/book (lista todos)
- GET /api/book/{id} (detalhes)
- PUT /api/book/{id} (atualizar)
- DELETE /api/book/{id} (deletar)
- Testes automatizados
- Seed de dados

## 12. Próximos passos recomendados

1. **Completar CRUD básico:** Implementar endpoints GET, PUT e DELETE
2. **Corrigir mapeamento:** Garantir que gêneros sejam corretamente mapeados no fluxo POST
3. **Injeção de dependência:** Configurar DI apropriadamente para CreateBookService
4. **Testes:** Adicionar testes unitários e de integração
5. **Validação:** Revisar e corrigir mensagens de validação
6. **Documentação API:** Adicionar atributos XML para melhor documentação Swagger

## 13. Referência rápida

```yaml
Projeto: GerenciadorLivraria
Linguagem: C#
Runtime: .NET 10
Arquitetura: Layered (API → Application → Domain + Infrastructure)
DB: SQLite via EF Core
Validação: FluentValidation
Documentação: Swagger/OpenAPI
Status: Em desenvolvimento
```

---

**Última atualização:** 2026-04-21
