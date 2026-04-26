# 📋 Gerenciador de Tarefas

Um projeto de estudo em ASP.NET Core que implementa uma API RESTful para gerenciar tarefas. O projeto segue a arquitetura em camadas com separação clara de responsabilidades.

## 📑 Índice

- [Visão Geral](#visão-geral)
- [Pré-requisitos](#pré-requisitos)
- [Como Rodar](#como-rodar)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [API Endpoints](#api-endpoints)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)

## 🎯 Visão Geral

O **Gerenciador de Tarefas** é uma API RESTful desenvolvida em ASP.NET Core 10.0 que permite criar, atualizar, listar e deletar tarefas. O projeto utiliza Entity Framework Core com SQLite para persistência de dados e Swagger/OpenAPI para documentação interativa da API.

### Características Principais

- ✅ CRUD completo de tarefas
- 📊 API RESTful bem estruturada
- 🗄️ Persistência com Entity Framework Core + SQLite
- 📖 Documentação automática com Swagger/OpenAPI
- 🏗️ Arquitetura em camadas (Domain, Application, Infrastructure, API)
- 🚨 Tratamento centralizado de exceções

## 📦 Pré-requisitos

Antes de começar, certifique-se de que você tem os seguintes itens instalados:

- **.NET SDK 10.0** ou superior
  - [Download .NET SDK](https://dotnet.microsoft.com/pt-br/download)
- **Git** (opcional, para clonar o repositório)
- **Visual Studio 2022**, **Visual Studio Code** ou outro editor de sua preferência

### Verificar Instalação

Para verificar se você tem o .NET SDK instalado corretamente, execute:

```bash
dotnet --version
```

## 🚀 Como Rodar

### 1. Clonar o Repositório (se necessário)

```bash
git clone https://github.com/gbmouraa/projetos_estudo_dotnet.git
cd projetos_estudo_dotnet
```

### 2. Navegar até o Projeto

```bash
cd src/GerenciadorTarefas
```

### 3. Restaurar Dependências

```bash
dotnet restore
```

### 4. Compilar o Projeto

```bash
dotnet build
```

### 5. Executar as Migrations (criar/atualizar banco de dados)

```bash
dotnet ef database update --project GerenciadorTarefas.Infrastructure --startup-project GerenciadorTarefas.API
```

### 6. Rodar a Aplicação

```bash
dotnet run --project GerenciadorTarefas.API
```

A aplicação será iniciada e estará disponível em:

- **URL**: `https://localhost:7001` ou `http://localhost:5000`
- **Swagger UI**: `https://localhost:7001/swagger` ou `http://localhost:5000/swagger`

### 7. Acessar a Documentação da API

Abra seu navegador e acesse:

```
https://localhost:7001/swagger
```

Aqui você poderá visualizar todos os endpoints disponíveis, seus parâmetros e testar a API diretamente da interface.

## 🏗️ Estrutura do Projeto

O projeto está organizado em uma arquitetura em camadas com os seguintes projetos:

```
GerenciadorTarefas/
├── GerenciadorTarefas.API/              # Camada de Apresentação
│   ├── Controllers/                      # Controladores da API
│   ├── Filters/                          # Filtros (ex: tratamento de exceções)
│   ├── Program.cs                        # Configuração da aplicação
│   ├── appsettings.json                  # Configurações gerais
│   └── appsettings.Development.json      # Configurações de desenvolvimento
│
├── GerenciadorTarefas.Application/      # Camada de Aplicação
│   ├── Services/                         # Lógica de negócio
│
├── GerenciadorTarefas.Domain/           # Camada de Domínio
│   ├── Entities/                         # Entidades de negócio
│   └── Enums/                            # Enumerações
│
├── GerenciadorTarefas.Infrastructure/   # Camada de Infraestrutura
│   ├── Database/                         # Contexto do Entity Framework
│
├── GerenciadorTarefas.Communication/    # Camada de Comunicação
│   └── Requests/Responses/               # Modelos de requisição/resposta
│
├── GerenciadorTarefas.Exceptions/       # Camada de Exceções
│   └── Custom Exceptions/                # Exceções customizadas
│
└── GerenciadorTarefas.slnx               # Arquivo da solução
```

### Descrição das Camadas

#### 🎨 **GerenciadorTarefas.API** (Apresentação)

- Contém os controladores REST que recebem as requisições HTTP
- Implementa filtros para tratamento centralizado de erros
- Configura e inicializa a aplicação
- Integra Swagger para documentação automática

#### 📱 **GerenciadorTarefas.Application** (Aplicação)

- Implementa os serviços que contêm a lógica de negócio
- Orquestra operações entre o Domain e Infrastructure

#### 🎯 **GerenciadorTarefas.Domain** (Domínio)

- Define as entidades de negócio
- Contém enumerações relacionadas ao domínio
- Independente de tecnologia - não tem dependências externas

#### 🗄️ **GerenciadorTarefas.Infrastructure** (Infraestrutura)

- Implementa a persistência de dados com Entity Framework Core
- Configura o contexto do banco de dados (DbContext)
- Gerencia a conexão com SQLite

#### 💬 **GerenciadorTarefas.Communication** (Comunicação)

- Define os modelos para requisições e respostas da API
- Centraliza os contratos de comunicação

#### ⚠️ **GerenciadorTarefas.Exceptions** (Exceções)

- Define exceções customizadas do domínio
- Padroniza o tratamento de erros

## 📡 API Endpoints

### Tarefas

#### Listar Todas as Tarefas

```
GET /api/tasks
```

**Resposta (200 OK):**

```json
[
  {
    "id": 1,
    "title": "Completar projeto",
    "description": "Finalizar o desenvolvimento",
    "status": "InProgress",
    "createdAt": "2024-01-15T10:30:00",
    "updatedAt": "2024-01-15T10:30:00"
  }
]
```

#### Obter uma Tarefa por ID

```
GET /api/tasks/{id}
```

**Resposta (200 OK):**

```json
{
  "id": 1,
  "title": "Completar projeto",
  "description": "Finalizar o desenvolvimento",
  "status": "InProgress",
  "createdAt": "2024-01-15T10:30:00",
  "updatedAt": "2024-01-15T10:30:00"
}
```

#### Criar uma Nova Tarefa

```
POST /api/tasks
Content-Type: application/json

{
  "title": "Estudar C#",
  "description": "Aprender os conceitos avançados",
  "status": "Pending"
}
```

**Resposta (201 Created):**

```json
{
  "id": 2,
  "title": "Estudar C#",
  "description": "Aprender os conceitos avançados",
  "status": "Pending",
  "createdAt": "2024-01-15T11:00:00",
  "updatedAt": "2024-01-15T11:00:00"
}
```

#### Atualizar uma Tarefa

```
PUT /api/tasks/{id}
Content-Type: application/json

{
  "title": "Estudar C# avançado",
  "description": "Aprender os conceitos avançados de C#",
  "status": "InProgress"
}
```

**Resposta (200 OK):**

```json
{
  "id": 2,
  "title": "Estudar C# avançado",
  "description": "Aprender os conceitos avançados de C#",
  "status": "InProgress",
  "createdAt": "2024-01-15T11:00:00",
  "updatedAt": "2024-01-15T11:30:00"
}
```

#### Deletar uma Tarefa

```
DELETE /api/tasks/{id}
```

**Resposta (204 No Content)** - Sem corpo de resposta

### Status de Tarefas

Os status disponíveis para uma tarefa são:

- `Pending` - Pendente
- `InProgress` - Em progresso
- `Completed` - Concluída
- `Canceled` - Cancelada

## 🛠️ Tecnologias Utilizadas

| Tecnologia                | Versão | Descrição                      |
| ------------------------- | ------ | ------------------------------ |
| **.NET**                  | 10.0   | Framework de desenvolvimento   |
| **ASP.NET Core**          | 10.0   | Framework web                  |
| **Entity Framework Core** | 10.0   | ORM para acesso a dados        |
| **SQLite**                | -      | Banco de dados                 |
| **Swagger/OpenAPI**       | 10.1.7 | Documentação interativa da API |
| **C#**                    | Latest | Linguagem de programação       |

## 📝 Notas Adicionais

### Banco de Dados

O projeto utiliza **SQLite** como banco de dados. O arquivo do banco é armazenado em:

```
GerenciadorTarefas.API/Data/GerenciadorTarefas.db
```

Este arquivo é criado automaticamente quando você executa as migrations pela primeira vez.

### Desenvolvimento

Durante o desenvolvimento, a aplicação carrega configurações adicionais do arquivo `appsettings.Development.json`. Este arquivo não está versionado no Git por questões de segurança.

### Tratamento de Erros

A API implementa um sistema centralizado de tratamento de erros através do `ExceptionFilter`, que captura exceções e retorna respostas padronizadas ao cliente.

## 🤝 Contribuindo

Este é um projeto de estudo. Contribuições são bem-vindas! Sinta-se à vontade para abrir issues ou pull requests.

## 📄 Licença

Este projeto é parte de um repositório de estudo e pode ser utilizado para fins educacionais.

---
