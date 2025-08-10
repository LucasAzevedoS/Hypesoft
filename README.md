# Hypesoft API

API RESTful para gerenciamento de produtos e categorias, construída com .NET 9 e seguindo os princípios da Clean Architecture.

## Visão Geral

Este projeto é uma API de back-end projetada para servir como um sistema de gerenciamento de inventário para produtos e suas respectivas categorias. Ele utiliza uma arquitetura robusta e moderna, garantindo escalabilidade e manutenibilidade. A autenticação é tratada por meio do Keycloak, e os dados são persistidos em um banco de dados MongoDB.

## Tecnologias Utilizadas

- **Framework:** .NET 9, ASP.NET Core
- **Linguagem:** C#
- **Banco de Dados:** MongoDB (utilizando `MongoDB.Driver`)
- **Arquitetura:** Clean Architecture
- **Padrões de Design:** CQRS (Command Query Responsibility Segregation) com a biblioteca `MediatR`
- **Autenticação:** Keycloak
- **Logging:** Serilog
- **Documentação da API:** Swagger (OpenAPI)
- **Validação:** FluentValidation
- **Containerização:** Docker

## Estrutura do Projeto

O projeto adota os princípios da **Clean Architecture**, dividindo as responsabilidades em quatro camadas principais:

-   **`Domain`**: Contém as entidades de negócio principais (ex: `Product`, `Category`) e as interfaces dos repositórios (`IProductRepository`). É o núcleo do sistema e não depende de nenhuma outra camada.
-   **`Application`**: Orquestra a lógica de negócio. Contém os `Commands` (operações de escrita), `Queries` (operações de leitura), `Handlers` (lógica de execução), DTOs (Data Transfer Objects) e validadores. Depende apenas da camada `Domain`.
-   **`Infrastructure`**: Implementa as interfaces definidas na camada de `Application` e `Domain`. É responsável por detalhes técnicos como acesso ao banco de dados (repositórios do MongoDB), injeção de dependência e outras configurações de serviços externos.
-   **`API`**: A camada de apresentação. Expõe os endpoints da API (Controllers) para o mundo externo. Recebe as requisições HTTP e as direciona para a camada de `Application` através do MediatR.

```
/Hypesoft
|-- API/                # Controladores da API (Endpoints)
|-- Application/        # Lógica de negócio (CQRS, DTOs, Validadores)
|   |-- Commands/
|   |-- Handlers/
|   |-- Queries/
|   `-- Validators/
|-- Domain/             # Entidades e Interfaces de Repositório
|   |-- Entities/
|   `-- Repositories/
|-- Infrastructure/     # Implementações (Banco de Dados, etc.)
|   |-- Data/
|   `-- Repositories/
`-- Program.cs          # Ponto de entrada e configuração da aplicação
```

## Como Rodar o Projeto

### Pré-requisitos

-   [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
-   [Docker](https://www.docker.com/products/docker-desktop/)
-   Uma instância do MongoDB (pode ser local ou em um container Docker)
-   Uma instância do Keycloak configurada (pode ser em um container Docker)

### Configuração

1.  **Clone o repositório:**
    ```bash
    git clone <URL_DO_SEU_REPOSITORIO>
    cd Hypesoft
    ```

2.  **Configure as variáveis de ambiente:**
    Renomeie `appsettings.Development.json` se necessário e preencha as seções `MongoDb` and `Keycloak` com suas configurações.

    **Exemplo (`appsettings.json`):**
    ```json
    {
      "MongoDb": {
        "ConnectionString": "mongodb://localhost:27017",
        "DatabaseName": "HypesoftDb"
      },
      "Keycloak": {
        "auth-server-url": "http://localhost:8080/auth/",
        "realm": "seu-realm",
        "resource": "seu-client-id",
        "credentials": {
          "secret": "seu-client-secret"
        }
      }
    }
    ```

### Rodando Localmente

1.  **Restaure as dependências:**
    ```bash
    dotnet restore
    ```

2.  **Execute a aplicação:**
    ```bash
    dotnet run --project Hypesoft
    ```

3.  A API estará disponível em `http://localhost:5100`.
4.  A documentação do Swagger pode ser acessada em `http://localhost:5100/swagger`.

### Rodando com Docker

O projeto já vem com um `docker-compose.yaml` e `Dockerfile` configurados.

1.  **Construa e inicie os containers:**
    ```bash
    docker-compose up --build -d
    ```
2.  A API estará disponível na porta definida no `docker-compose.yaml`.

## Endpoints da API

A API fornece endpoints para gerenciar `Produtos` e `Categorias`.

-   `api/Products`: Operações CRUD para produtos.
-   `api/Categories`: Operações CRUD para categorias.
-   `api/Auth`: Endpoint para autenticação (`login`).

Para uma lista completa de endpoints, modelos e exemplos, acesse a documentação interativa do Swagger em `/swagger` após iniciar a aplicação.

## Showcase das Funcionalidades Implementadas

A API oferece um conjunto rico de funcionalidades para gerenciamento de um sistema de inventário.

### Autenticação e Segurança
- **Login de Usuário:** Autenticação segura utilizando o padrão OpenID Connect via Keycloak.
- **Autorização baseada em Token:** Endpoints protegidos com tokens JWT, garantindo que apenas usuários autenticados possam acessar os recursos.

### Gerenciamento de Produtos
- **CRUD Completo:**
    - `POST /api/Products/Create`: Cria um novo produto.
    - `GET /api/Products/GetAll`: Lista todos os produtos.
    - `GET /api/Products/GetById/{id}`: Busca um produto específico pelo seu ID.
    - `PUT /api/Products/EditById/{id}`: Atualiza um produto existente.
    - `DELETE /api/Products/DeleteById/{id}`: Remove um produto.
- **Consultas Avançadas:**
    - `GET /api/Products/GetByCategory/{category}`: Retorna todos os produtos de uma determinada categoria.
    - `GET /api/Products/paged`: Lista produtos com suporte à paginação (`page` e `pageSize`).
    - `GET /api/Products/GetLast`: Exibe os últimos 5 produtos adicionados, ideal para dashboards.
    - `GET /api/Products/GetLowStock`: Retorna uma lista de produtos com baixo estoque (quantidade inferior a 10).
    - `GET /api/Products/GetAllDt`: Lista todos os produtos ordenados pela data de criação.

### Gerenciamento de Categorias
- **CRUD Completo:**
    - `POST /api/Categories/category`: Cria uma nova categoria.
    - `GET /api/Categories/categoryAll`: Lista todas as categorias.
    - `GET /api/Categories/category/{id}`: Busca uma categoria específica pelo seu ID.
    - `PUT /api/Categories/category/{id}`: Atualiza uma categoria existente.
    - `DELETE /api/Categories/{id}`: Remove uma categoria.

### Logging e Monitoramento
- **Logs Detalhados:** Utilização do Serilog para registrar informações importantes, avisos e erros, com saídas para o console e arquivos de log diários (`logs/log.txt`), facilitando o rastreamento e a depuração.
