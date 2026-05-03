# GerenciadorLivraria

Sistema de gerenciamento de livros construído com **ASP.NET Core 10** seguindo arquitetura em camadas (Layered Architecture) com padrão CQRS (Command Query Responsibility Segregation) utilizando MediatR. Utiliza Entity Framework Core com SQLite como banco de dados.

## 📋 Índice

- [Visão Geral](#visão-geral)
- [Tecnologias](#tecnologias)
- [Arquitetura](#arquitetura)
- [Como Rodar](#como-rodar)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Modelo de Domínio](#modelo-de-domínio)
- [API REST](#api-rest)
- [Fluxo de Funcionamento](#fluxo-de-funcionamento)
- [Tratamento de Erros](#tratamento-de-erros)

## 🎯 Visão Geral

O **GerenciadorLivraria** é uma API REST que permite:
- ✅ Listar todos os livros
- ✅ Buscar livro por ID
- ✅ Criar novo livro
- ✅ Atualizar livro
- ✅ Deletar livro

Cada livro possui informações como título, autor, preço, estoque e pode estar associado a múltiplos gêneros.

> **Nota:** O projeto inclui um frontend React (GerenciadorLivraria.FrontEnd) em desenvolvimento para apresentação visual, porém o foco principal do projeto é o backend API.

## 🛠️ Tecnologias

| Camada | Tecnologia |
|--------|-----------|
| **Runtime** | .NET 10 |
| **Framework Web** | ASP.NET Core |
| **Padrão de Arquitetura** | CQRS com MediatR |
| **Banco de Dados** | SQLite |
| **ORM** | Entity Framework Core 10.0.6 |
| **Validação** | FluentValidation 12.1.1 |
| **Documentação API** | Swagger / Swashbuckle 10.1.7 |

### Dependências por Projeto

**GerenciadorLivraria.API:**
- MediatR (12.4.1)
- Microsoft.AspNetCore.OpenApi (10.0.5)
- Microsoft.EntityFrameworkCore (10.0.6)
- Microsoft.EntityFrameworkCore.Design (10.0.6)
- Microsoft.EntityFrameworkCore.Tools (10.0.6)
- Swashbuckle.AspNetCore.SwaggerGen (10.1.7)
- Swashbuckle.AspNetCore.SwaggerUI (10.1.7)

**GerenciadorLivraria.Application:**
- FluentValidation (12.1.1)
- MediatR (12.4.1)
- Microsoft.EntityFrameworkCore (10.0.6)

**GerenciadorLivraria.Communication:**
- (Camada leve para Requests e Responses)

**GerenciadorLivraria.Infrastructure:**
- Microsoft.EntityFrameworkCore (10.0.6)
- Microsoft.EntityFrameworkCore.Design (10.0.6)
- Microsoft.EntityFrameworkCore.Sqlite (10.0.6)
- Microsoft.EntityFrameworkCore.Tools (10.0.6)

**GerenciadorLivraria.Domain:**
- (Camada de entidades, sem dependências externas)

## 🏗️ Arquitetura

### Estrutura de Camadas

```
GerenciadorLivraria.API (Apresentação)
    ↓ depende de
GerenciadorLivraria.Application (Regras de Negócio - CQRS)
    ↓ depende de
GerenciadorLivraria.Domain (Entidades)
    ↓ depende de
GerenciadorLivraria.Infrastructure (Persistência)
    ↑
GerenciadorLivraria.Communication (Requests/Responses)
```

### Padrão CQRS com MediatR

O projeto implementa o padrão CQRS (Command Query Responsibility Segregation) utilizando a biblioteca MediatR:

- **Commands** (Write): RegisterBookCommand, UpdateBookCommand, DeleteBookCommand
- **Queries** (Read): GetAllBooksQuery, GetBookByIdQuery
- **Handlers**: Processam cada Command/Query separadamente

### Organização de Pastas

```
GerenciadorLivraria/
├── GerenciadorLivraria.API/
│   ├── Controllers/
│   │   └── Book/
│   │       └── BookController.cs
│   ├── Filters/
│   │   └── ExceptionFilter.cs
│   ├── Responses/
│   │   └── ErrorMessageResponse.cs
│   ├── Data/                    (pasta para banco de dados)
│   ├── Program.cs
│   ├── appsettings.json
│   └── GerenciadorLivraria.API.csproj
│
├── GerenciadorLivraria.Application/
│   ├── Book/
│   │   ├── Register/
│   │   │   ├── RegisterBookCommand.cs
│   │   │   ├── RegisterBookHandler.cs
│   │   │   └── RegisterBookValidator.cs
│   │   ├── GetAll/
│   │   │   ├── GetAllBooksQuery.cs
│   │   │   └── GetAllBooksHandler.cs
│   │   ├── GetById/
│   │   │   ├── GetBookByIdQuery.cs
│   │   │   └── GetBookByIdHandler.cs
│   │   ├── Update/
│   │   │   ├── UpdateBookCommand.cs
│   │   │   ├── UpdateBookHandler.cs
│   │   │   └── UpdateBookValidator.cs
│   │   └── Delete/
│   │       ├── DeleteBookCommand.cs
│   │       └── DeleteBookHandler.cs
│   ├── Common/
│   │   └── Exceptions/
│   │       ├── GerenciadorLivrariaException.cs
│   │       ├── ErrorOnValidationException.cs
│   │       └── NotFoundException.cs
│   └── GerenciadorLivraria.Application.csproj
│
├── GerenciadorLivraria.Communication/
│   ├── Requests/
│   │   ├── BaseRequest.cs
│   │   ├── RegisterBookRequest.cs
│   │   └── UpdateBookRequest.cs
│   ├── Responses/
│   │   ├── BaseResponse.cs
│   │   ├── BookResponse.cs
│   │   ├── RegisterBookResponse.cs
│   │   └── ErrorMessageResponse.cs
│   └── GerenciadorLivraria.Communication.csproj
│
├── GerenciadorLivraria.Domain/
│   ├── Entities/
│   │   ├── BookEntity.cs
│   │   └── GenreEntity.cs
│   ├── Enums/
│   │   └── EnumGenre.cs
│   └── GerenciadorLivraria.Domain.csproj
│
├── GerenciadorLivraria.Infrastructure/
│   ├── DataBase/
│   │   ├── GerenciadorLivrariaDbContext.cs
│   │   └── GerenciadorLivrariaDbContextFactory.cs
│   ├── Migrations/
│   │   ├── 20260421001658_InitialCreate.cs
│   │   ├── 20260421001658_InitialCreate.Designer.cs
│   │   └── GerenciadorLivrariaDbContextModelSnapshot.cs
│   └── GerenciadorLivraria.Infrastructure.csproj
│
├── GerenciadorLivraria.FrontEnd/          (Em desenvolvimento)
│   ├── src/
│   │   ├── services/
│   │   │   ├── api.ts
│   │   │   └── books.ts
│   │   └── main.tsx
│   ├── package.json
│   ├── tsconfig.json
│   ├── vite.config.ts
│   └── README.md
│
├── GerenciadorLivraria.slnx
└── README.md
```

## 🚀 Como Rodar

### Pré-requisitos

- **.NET 10 SDK** instalado ([Download](https://dotnet.microsoft.com/download))
- **Node.js** (para o frontend, opcional)
- **Git** (para clonar o repositório)

### Passo 1: Clonar o Repositório

```bash
git clone https://github.com/seu-usuario/projetos_estudo_dotnet.git
cd src/GerenciadorLivraria
```

### Passo 2: Restaurar Dependências

```bash
dotnet restore
```

### Passo 3: Compilar a Solução

```bash
dotnet build GerenciadorLivraria.slnx
```

### Passo 4: Executar a API

```bash
dotnet run --project GerenciadorLivraria.API
```

A API estará disponível em:
- **HTTP:** `http://localhost:5139`
- **Swagger UI:** `http://localhost:5139/swagger/index.html`

### Passo 5: Testar os Endpoints (Opcional)

Você pode usar ferramentas como:
- **Swagger UI** (abrir no navegador)
- **Postman** ([Download](https://www.postman.com/downloads/))
- **cURL** (via terminal)
- **VSCode REST Client** (usar arquivo `.http`)

## 📚 Estrutura do Projeto

### Modelo de Domínio

#### BookEntity

Representa um livro no sistema.

| Campo | Tipo | Obrigatório | Descrição |
|-------|------|------------|-----------|
| `Id` | `Guid` | Sim | Identificador único (gerado automaticamente) |
| `Title` | `string` | Sim | Título do livro |
| `Author` | `string` | Sim | Autor do livro |
| `Genre` | `ICollection<GenreEntity>` | Sim | Lista de gêneros (N:N) |
| `Price` | `decimal` | Sim | Preço do livro (deve ser > 0) |
| `Stock` | `int` | Sim | Quantidade em estoque (>= 0) |
| `CreatedAt` | `DateTime` | Sim | Data de criação (preenchida automaticamente) |
| `UpdatedAt` | `DateTime?` | Não | Data da última atualização |

#### GenreEntity

Representa um gênero literário.

| Campo | Tipo | Descrição |
|-------|------|-----------|
| `Id` | `Guid` | Identificador único |
| `Name` | `string` | Nome do gênero |
| `TypeIdentifier` | `int` | Identificador numérico (mapeia para `EnumGenre`) |
| `Books` | `ICollection<BookEntity>` | Livros associados (N:N) |
| `CreatedAt` | `DateTime` | Data de criação |
| `UpdatedAt` | `DateTime?` | Data da última atualização |

#### EnumGenre

Enum com valores predefinidos de gêneros:

```csharp
public enum EnumGenre
{
    Romance = 1,
    Filosofia = 2,
    Terror = 3,
    Tecnologia = 4,
}
```

#### Gêneros Seeded

O banco de dados é populado automaticamente com os gêneros ao criar o banco:

| ID | Name | TypeIdentifier |
|----|------|----------------|
| `11111111-1111-1111-1111-111111111111` | Romance | 1 |
| `22222222-2222-2222-2222-222222222222` | Filosofia | 2 |
| `33333333-3333-3333-3333-333333333333` | Terror | 3 |
| `44444444-4444-4444-4444-444444444444` | Tecnologia | 4 |

## 📡 API REST

### Base URL
```
http://localhost:5139/api/book
```

### Endpoints

#### 1. Listar Todos os Livros

```http
GET /api/book
```

**Resposta (200 OK):**
```json
[
  {
    "title": "Clean Code",
    "author": "Robert C. Martin",
    "genre": [4]
  }
]
```

**Possíveis Status:**
- `200 OK` - Lista retornada com sucesso

---

#### 2. Buscar Livro por ID

```http
GET /api/book/{id}
```

**Parâmetros:**
- `id` (path, obrigatório): UUID do livro

**Exemplo:**
```http
GET /api/book/550e8400-e29b-41d4-a716-446655440000
```

**Resposta (200 OK):**
```json
{
  "title": "Clean Code",
  "author": "Robert C. Martin",
  "genre": [4]
}
```

**Possíveis Status:**
- `200 OK` - Livro encontrado
- `404 Not Found` - Livro não encontrado

---

#### 3. Criar Novo Livro

```http
POST /api/book
Content-Type: application/json

{
  "title": "Clean Code",
  "author": "Robert C. Martin",
  "genre": [1, 4],
  "price": 120.50,
  "stock": 10
}
```

**Parâmetros no Body:**
- `title` (string, obrigatório): Título do livro
- `author` (string, obrigatório): Autor do livro
- `genre` (array de integers, obrigatório): IDs dos gêneros (1-4)
- `price` (decimal, obrigatório): Preço (> 0)
- `stock` (integer, obrigatório): Estoque (>= 0)

**Resposta (201 Created):**
```json
{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "title": "Clean Code"
}
```

**Resposta de Erro (400 Bad Request):**
```json
{
  "errors": [
    "O titulo do livro não pode ser vazio."
  ]
}
```

**Possíveis Status:**
- `201 Created` - Livro criado com sucesso
- `400 Bad Request` - Validação falhou

---

#### 4. Atualizar Livro

```http
PUT /api/book/{id}
Content-Type: application/json

{
  "title": "Clean Code (2ª Edição)",
  "author": "Robert C. Martin",
  "genre": [4],
  "price": 125.00,
  "stock": 15
}
```

**Parâmetros:**
- `id` (path, obrigatório): UUID do livro
- Body: mesmo formato de criação

**Resposta (204 No Content):**
```
[Sem corpo]
```

**Possíveis Status:**
- `204 No Content` - Livro atualizado com sucesso
- `400 Bad Request` - Validação falhou
- `404 Not Found` - Livro não encontrado

---

#### 5. Deletar Livro

```http
DELETE /api/book/{id}
```

**Parâmetros:**
- `id` (path, obrigatório): UUID do livro

**Resposta (204 No Content):**
```
[Sem corpo]
```

**Possíveis Status:**
- `204 No Content` - Livro deletado com sucesso
- `404 Not Found` - Livro não encontrado

---

### Regras de Validação

A validação é realizada automaticamente ao criar ou atualizar um livro:

| Campo | Regra | Mensagem de Erro |
|-------|-------|------------------|
| `Title` | Não pode ser vazio | "O titulo do livro não pode ser vazio." |
| `Author` | Não pode ser vazio | "O nome do autor não pode ser vazio." |
| `Genre` | Deve ter pelo menos 1 | "Adicione pelo menos um genero para o livro." |
| `Genre` | Cada item deve ser válido (1-4) | "O gênero '{PropertyValue}' é inválido." |
| `Price` | Deve ser > 0 | "Insira um preço válido." |
| `Stock` | Deve ser >= 0 | "Estoque deve ser maior que zero." |

### Tratamento de Erros

#### Resposta de Erro Padrão

```json
{
  "errors": [
    "Mensagem de erro 1",
    "Mensagem de erro 2"
  ]
}
```

#### Códigos HTTP

| Status | Significado | Causa |
|--------|------------|-------|
| `200` | OK | Requisição bem-sucedida (GET, etc) |
| `201` | Created | Recurso criado com sucesso |
| `204` | No Content | Requisição bem-sucedida sem conteúdo (PUT, DELETE) |
| `400` | Bad Request | Erro de validação |
| `404` | Not Found | Livro não encontrado |
| `500` | Internal Server Error | Erro desconhecido no servidor |

## 🔄 Fluxo de Funcionamento

### Fluxo CQRS (Command Query Responsibility Segregation)

```
Cliente envia Requisição HTTP
                ↓
    ┌───────────┴───────────┐
    ↓                       ↓
  GET (Query)          POST/PUT/DELETE
    ↓                       ↓
  Query Handler        Command Handler
    ↓                       ↓
  MediatR.Send()      MediatR.Send()
    ↓                       ↓
  Retorna Dados       Retorna Resultado
```

### Fluxo de Criação de Livro (RegisterBookCommand)

```
1. Cliente envia POST /api/book com RegisterBookRequest
                            ↓
2. BookController.Register recebe a requisição
                            ↓
3. Cria RegisterBookCommand e envia via MediatR
                            ↓
4. RegisterBookHandler.Execute() é chamado
                            ↓
5. Valida RegisterBookCommand usando RegisterBookValidator
                            ↓
6. Se inválido → Lança ErrorOnValidationException
                            ↓
7. Se válido → Cria BookEntity e adiciona ao DbContext
                            ↓
8. Chama _dbContext.SaveChanges() para persistir
                            ↓
9. Retorna RegisterBookResponse com Id e Title
                            ↓
10. Controller retorna 201 Created com o response
```

### Fluxo de Leitura (GetAllBooksQuery/GetBookByIdQuery)

```
1. Cliente envia GET /api/book ou GET /api/book/{id}
                            ↓
2. BookController.GetAll() ou GetById(id)
                            ↓
3. Cria Query e envia via MediatR
                            ↓
4. Query Handler busca livros com Include(b => b.Genre)
                            ↓
5. Se não encontrado → Lança NotFoundException
                            ↓
6. Se encontrado → Mapeia para BookResponse
                            ↓
7. Retorna 200 OK com lista ou objeto único
```

### Fluxo de Tratamento de Exceções

```
Handler executa lógica
                ↓
            ┌───┴───┐
            ↓       ↓
    GerenciadorLivrariaException   Outras exceções
            ↓                            ↓
    ErrorOnValidationException     ExceptionFilter
    NotFoundException              (ThrowUnknowError)
            ↓                            ↓
    ExceptionFilter captura       Retorna 500
            ↓                       "ERRO DESCONHECIDO"
    Define StatusCode
    Retorna ErrorMessageResponseJson
```

## ❌ Tratamento de Erros

### Hierarquia de Exceções

```csharp
SystemException
    ↓
GerenciadorLivrariaException (base abstrata)
    ├─ ErrorOnValidationException (400 Bad Request)
    └─ NotFoundException (404 Not Found)
```

### Comportamento do ExceptionFilter

O `ExceptionFilter` intercepta todas as exceções não capturadas:

- **Se for `GerenciadorLivrariaException`:**
  - Define `StatusCode` conforme `GetHttpStatusCode()`
  - Retorna `ErrorMessageResponseJson` com lista de erros

- **Se for outra exceção:**
  - Define `StatusCode` como 500
  - Retorna erro genérico `"ERRO DESCONHECIDO"`

## 🗄️ Banco de Dados

### Configuração

- **Tipo:** SQLite
- **Localização:** `GerenciadorLivraria.API/Data/GerenciadorLivraria.db`
- **Criação:** Automática na primeira execução
- **Migrations:** Aplicadas automaticamente via EF Core

### Tabelas

#### `Books`
```sql
CREATE TABLE Books (
    Id TEXT PRIMARY KEY,
    Title TEXT NOT NULL,
    Author TEXT NOT NULL,
    Price NUMERIC NOT NULL,
    Stock INTEGER NOT NULL,
    CreatedAt TEXT NOT NULL,
    UpdatedAt TEXT NULL
);
```

#### `Genres`
```sql
CREATE TABLE Genres (
    Id TEXT PRIMARY KEY,
    Name TEXT NOT NULL,
    TypeIdentifier INTEGER NOT NULL,
    CreatedAt TEXT NOT NULL,
    UpdatedAt TEXT NULL
);
```

#### `BookGenre` (Tabela de Junção N:N)
```sql
CREATE TABLE BookGenre (
    BooksId TEXT NOT NULL,
    GenreId TEXT NOT NULL,
    PRIMARY KEY (BooksId, GenreId),
    FOREIGN KEY (BooksId) REFERENCES Books(Id),
    FOREIGN KEY (GenreId) REFERENCES Genres(Id)
);
```

### Constraints

- Chaves estrangeiras habilitadas via `PRAGMA foreign_keys = ON;`
- Relacionamento N:N entre Books e Genres

## 📝 Exemplo de Uso com cURL

### Criar Livro

```bash
curl -X POST http://localhost:5139/api/book \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Clean Code",
    "author": "Robert C. Martin",
    "genre": [4],
    "price": 120.50,
    "stock": 10
  }'
```

### Listar Todos os Livros

```bash
curl -X GET http://localhost:5139/api/book
```

### Buscar Livro por ID

```bash
curl -X GET http://localhost:5139/api/book/550e8400-e29b-41d4-a716-446655440000
```

### Atualizar Livro

```bash
curl -X PUT http://localhost:5139/api/book/550e8400-e29b-41d4-a716-446655440000 \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Clean Code (2ª Edição)",
    "author": "Robert C. Martin",
    "genre": [4],
    "price": 125.00,
    "stock": 15
  }'
```

### Deletar Livro

```bash
curl -X DELETE http://localhost:5139/api/book/550e8400-e29b-41d4-a716-446655440000
```

## 🔧 Troubleshooting

### Erro: "Database file is locked"
**Solução:** Feche qualquer processo que esteja acessando o banco de dados e tente novamente.

### Erro: ".NET 10 SDK not found"
**Solução:** Instale o .NET 10 SDK do [site oficial](https://dotnet.microsoft.com/download)

### Erro: "Port already in use"
**Solução:** Mude a porta no `launchSettings.json` ou interrompa o processo usando a porta 5139.

### Swagger não carrega
**Solução:** Certifique-se que você está em ambiente `Development`. Edite `appsettings.json` se necessário.

---

**Última atualização:** 2026-05-02  
**Versão:** 2.0.0  
**Status:** ✅ Funcional (com CQRS + MediatR)