# Catálogo API

API REST desenvolvida em **ASP.NET Core** utilizando **Entity Framework Core**, criada com o objetivo de estudar os principais conceitos envolvidos no desenvolvimento de APIs RESTful.

O projeto consiste em um catálogo de produtos e categorias, simulando uma aplicação utilizada por uma rede fictícia de supermercados.

![Build Status](https://github.com/Weenio/Catalogo-API/actions/workflows/dotnet.yml/badge.svg)

---

## 🚀 Tecnologias utilizadas

- C#
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger/OpenAPI
- LINQ
- Async/Await
- xUnit

---

## 📌 Funcionalidades implementadas

### Categorias

- Cadastro de categorias
- Consulta de categorias
- Atualização de categorias
- Exclusão de categorias

### Produtos

- Cadastro de produtos
- Consulta de produtos
- Atualização de produtos
- Exclusão de produtos
- Relacionamento entre produtos e categorias

---

## 🔎 Pesquisa e filtros

A API possui suporte a filtros utilizando **Model Binding** do ASP.NET Core.

É possível realizar pesquisas utilizando parâmetros enviados pela URL, permitindo consultas mais específicas dos produtos cadastrados.

Os filtros atuais são apenas para o endpoint ```GET``` de produtos. Atualmente, os filtros são para os seguintes itens:
 - nome
 - Preço (usando ```PrecoMax``` e ```PrecoMin``` na URI)

---

## 🛠️ Recursos implementados

- Persistência de dados utilizando Entity Framework Core
- Criação e gerenciamento de banco de dados através de Migrations
- Validação de dados utilizando Data Annotations
- Tratamento de problemas relacionados à serialização JSON
- Uso de métodos assíncronos para operações com banco de dados
- Padronização de respostas HTTP utilizando Status Codes adequados

---

## 📂 Estrutura do projeto

```
Catalogo-API
│
├── Context
│ └── Arquivo para conexão com o banco de dados
│
├── Controllers
│ └── Endpoints da API
│
├── Filters
│ └── DTOs que carregam as definições de filtros da API
│
├── Migrations
│ └── Histórico das alterações do banco de dados
│
├── Models
│ └── Entidades do sistema
│
└── Program.cs
└── Configuração da aplicação
```

---

## ▶️ Como executar o projeto

1. Clone o repositório:

```bash
git clone https://github.com/Weenio/Catalogo-API.git
```
2. Configure a conexão com o banco de dados no arquivo:
```appsettings.json```

3. Execute as migrations:
```bash
dotnet ef database update
```

4. Inicie a aplicação:
```bash
dotnet run
```

A documentação dos endpoints estará disponível através do Swagger.

---

## 📈 Evolução do projeto

O projeto foi desenvolvido de forma incremental, recebendo melhorias conforme novos conceitos foram estudados:

- Implementação inicial da API REST
- Configuração do banco de dados utilizando EF Core Migrations
- Criação do CRUD de Produtos e Categorias
- Implementação de validações
- Correções relacionadas à serialização JSON
- Refatoração para operações assíncronas
- Adição de filtros utilizando Model Binding

## 📚 Objetivo

Este projeto tem como objetivo consolidar conhecimentos relacionados ao desenvolvimento de APIs RESTful utilizando o ecossistema .NET, aplicando boas práticas de organização, persistência de dados e evolução contínua do software.