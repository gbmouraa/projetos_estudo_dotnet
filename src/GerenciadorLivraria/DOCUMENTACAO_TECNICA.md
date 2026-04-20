# Documentação Técnica — GerenciadorLivraria

## 1. Visão geral

Projeto em **.NET 10** organizado em camadas para gerenciamento de livros via API REST.  
O repositório já possui API, camada de aplicação, domínio e infraestrutura com EF Core (SQLite + migration inicial), porém o fluxo de criação ainda tem bloqueios de implementação.

## 2. Stack e dependências

| Camada | Tecnologia |
|---|---|
| API | ASP.NET Core Web API, Swagger (Swashbuckle), EF Core (registro de DbContext) |
| Application | C#, FluentValidation |
| Domain | C# (entidades e enum) |
| Infrastructure | EF Core + SQLite + Migrations |

Dependências externas relevantes:
- `FluentValidation` (Application)
- `Microsoft.EntityFrameworkCore`, `Microsoft.EntityFrameworkCore.Sqlite` (API/Infrastructure)
- `Swashbuckle.AspNetCore.SwaggerGen` e `Swashbuckle.AspNetCore.SwaggerUI` (API)
- `Fluent.Infrastructure` (Application, pacote legado com incompatibilidades no build atual)

## 3. Estrutura da solução

```text
GerenciadorLivraria.slnx
├── GerenciadorLivraria.API
├── Application
├── GerenciadorLivraria.Domain
└── GerenciadorLivraria.Infrastructure
```

Dependências entre projetos (estado atual):

```text
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

Campos:
- `Id: Guid` (inicializado como `new Guid()`, resultando em `Guid.Empty` por padrão)
- `Title: string` (`[Required]`)
- `Author: string` (`[Required]`)
- `Genre: ICollection<Genre>` (relação N:N)
- `Price: decimal` (`[Required]`)
- `Stock: int` (`[Required]`)
- `CreatedAt: DateTime` (somente leitura, `DateTime.Now`)
- `UpdatedAt: DateTime` (set privado)

### 4.2 Entidade `Genre`

Campos:
- `Id: Guid`
- `Name: string`
- `Books: ICollection<BookEntity>`
- `CreatedAt: DateTime`
- `UpdatedAt: DateTime`

### 4.3 Enum `EnumGenre`

Valores:
1. `Romance`
2. `Filosofia`
3. `Terror`
4. `Tecnologia`

## 5. Persistência (Infrastructure)

- `GerenciadorLivrariaDbContext` expõe `DbSet<BookEntity>` e `DbSet<Genre>`.
- O `Program.cs` da API registra o DbContext com `UseSqlite(...)`.
- Existe migration inicial (`InitialCreate`) com criação das tabelas:
  - `Books`
  - `Genres`
  - `BookGenre` (tabela de junção N:N)
- O contexto executa `PRAGMA foreign_keys = ON;` em `OnModelCreating`.

## 6. API HTTP

Base route do controller: `api/book`

### 6.1 `GET /api/book`
Retorna `200 OK` com string fixa (`"Success my brudaa"`).  
**Observação:** endpoint placeholder.

### 6.2 `GET /api/book/{id}`
Retorna `200 OK` com string fixa (`"Success my brudaa"`).  
**Observação:** endpoint placeholder.

### 6.3 `POST /api/book`
Recebe `RequestCreateBookJson`:

```json
{
  "title": "Clean Code",
  "author": "Robert C. Martin",
  "genre": [4],
  "price": 120.50,
  "stock": 10
}
```

Resposta de sucesso prevista (`201 Created`, `ResponseCreateBookJson`):

```json
{
  "id": "00000000-0000-0000-0000-000000000001",
  "title": "Clean Code"
}
```

Resposta de erro (`400 Bad Request`, `ResponseErrorMessageJson`):

```json
{
  "errors": [
    "O titulo do livro não pode ser vazio."
  ]
}
```

## 7. Fluxo atual de criação de livro

1. `BookController.Create` recebe `RequestCreateBookJson`.
2. Controller instancia `CreateBookService` manualmente.
3. Controller mapeia apenas `Title`, `Author`, `Price` e `Stock` (não mapeia `Genre`).
4. `CreateBookService.Execute` valida o model via `CreateBookValidator`.
5. Em erro de validação, lança `ErrorOnValidationException`.
6. Sem erro de validação, tenta persistir via `_database.Add(book)` e `_database.SaveChanges()`.
7. `ExceptionFilter` padroniza resposta de exceções da aplicação.

## 8. Regras de validação (`CreateBookValidator`)

- `Title` obrigatório.
- `Author` obrigatório.
- `Genre` deve conter ao menos um item.
- Cada item de `Genre` deve ser valor válido do enum.
- `Price` deve ser `> 0`.
- `Price` também possui regra `>= 0` com mensagem referente a estoque.

**Inconsistências atuais observadas no fluxo:**
- `Genre` não é mapeado no controller para `CreateBookModel`, portanto a validação de gênero tende a falhar.
- A mensagem `"Estoque deve ser maior que zero."` está vinculada a regra de `Price`, não de `Stock`.
- `CreateBookService` possui campo `_database` sem construtor ativo para injeção, causando falha em runtime ao tentar persistir.

## 9. Tratamento de exceções

Exceções de negócio herdam de `GerenciadorLivrariaException`, que define:
- `GetErrors(): List<string>`
- `GetHttpStatusCode(): HttpStatusCode`

Mapeamento atual:
- `ErrorOnValidationException` → `400 Bad Request`.
- Exceções não mapeadas → `500 Internal Server Error` com mensagem `"ERRO DESCONHECIDO"`.

## 10. Configuração e execução local

Pré-requisitos:
- SDK .NET 10

Comandos:

```bash
dotnet restore
dotnet build GerenciadorLivraria.slnx
dotnet run --project GerenciadorLivraria.API
```

Em ambiente `Development`, Swagger é habilitado via `Program.cs`.

**Observações importantes do estado atual:**
- O build da solução está falhando por incompatibilidades trazidas pelo pacote `Fluent.Infrastructure` (warnings `NU1701`, vulnerabilidades e erros de compilação do CSC).
- A string de conexão configurada para SQLite usa formato não usual para o provider (`Server=...;Database=...`), demandando ajuste para ambiente real.

## 11. Estado atual e lacunas

- Endpoints GET ainda são placeholders.
- Fluxo POST não está pronto para uso em produção devido a mapeamento incompleto e falta de injeção do DbContext no serviço.
- Não há testes automatizados no repositório.
- Mapeamentos request/model/response permanecem manuais.

## 12. Referência rápida para modelos de IA

```yaml
project_name: GerenciadorLivraria
language: C#
runtime: .NET 10
architecture: layered (API -> Application -> Domain + Infrastructure)
http_routes:
  - GET /api/book (placeholder)
  - GET /api/book/{id} (placeholder)
  - POST /api/book (parcial, com inconsistências)
validation_engine: FluentValidation
persistence:
  provider: EF Core SQLite
  migration_status: initial migration created
error_contract:
  shape: { errors: string[] }
build_status: failing due to package/tooling incompatibilities in Application
tests_status: not implemented
```

