# 🇺🇸 ENGLISH

> 📘 **This README is available in English and Portuguese.**  
> Scroll down for the [Portuguese version](#-português).

# 🏦 Banking System API & Dashboard

API developed in .NET 9 for managing a robust banking event system.

The project follows Clean Architecture principles, prioritizing testability, maintainability, and best development practices. Fully containerized with Docker.

---

## 📚 Table of Contents

1. [📦 Overview](#-overview)
2. [🧩 Project Structure](#-project-structure)
3. [🚀 Running the Project](#-running-the-project)
4. [🌐 Main Endpoints](#-main-endpoints)
5. [📄 Conventions and Best Practices](#-conventions-and-best-practices)
6. [🚀 CI/CD Pipeline & Deployment](#-cicd-pipeline--deployment)
7. [🌍 Live Demo](#-live-demo)
8. [🧾 Final Considerations](#-final-considerations)

---

## 📦 Overview

The Banking System is a solution for managing accounts and financial transactions. Beyond functional requirements (deposit, withdraw, and transfer), the project focuses on Clean Architecture, Network Resilience, and Scalability.

### Main Features
- ✅ Financial Event Engine: Process Deposits, Withdrawals, and Transfers through a unified transaction system.
- ✅ Real-time Balance Management: Instant balance inquiries and state updates for existing and new accounts.
- ✅ Atomic P2P Transfers: Secure transfers ensuring both origin and destination are processed in a single unit of work.
- ✅ Infrastructure as Config: Dynamic CORS and API settings managed entirely via appsettings.json.
- ✅ Clean Architecture: Strict separation between WebApi, Application, Domain, SharedKernel and Infrastructure layers.
- ✅ Database Resiliency: Self-healing startup logic with automatic migrations and retry policies for containerized environments.

---

## 🧩 Project Structure

```text
BankingSystem/
├── src/
│   ├── BankingSystem.WebApi/           → Presentation layer (Controllers, Dockerfile)
│   ├── BankingSystem.Application/      → Use cases, DTOs and Handlers
│   ├── BankingSystem.Domain/           → Domain entities and enums
│   ├── BankingSystem.Infrastructure/   → EF Context, Repositories, Migrations
│   └── BankingSystem.SharedKernel/     → Domain base (Entity, UnitOfWork, etc.)
│
├── tests/
│   └── BankingSystem.Tests/            → Unit tests and Integration tests
│
├── docker-compose.yml                  → Orchestrates the API and database containers
└── .dockerignore                       → Excludes unnecessary files from the Docker image
```

---

## 🚀 Running the Project

### 1️⃣ Option — With Docker (Recommended)

```bash
docker-compose up --build banking-api
```

📍 Swagger UI: http://localhost:8080/index.html

### 2️⃣ Option — Local (Without Docker)

Update appsettings.json with your local PostgreSQL credentials.
Run the application:

```bash
cd src/BankingSystem.WebApi/
dotnet run
```

## 🌐 Main Endpoints

After starting the API, you can access the interactive documentation at:
📍 **Swagger UI:** [http://localhost:8080/index.html](http://localhost:8080/index.html)

| Method | Endpoint | Description |
|:-------|:---------|:------------|
| `POST` | `/reset` | **System Reset:** Wipes all data to clear the state for new tests. |
| `GET`  | `/balance`| **Balance Inquiry:** Returns the current balance for an `account_id`. |
| `POST` | `/event` | **Transaction Engine:** Unified endpoint for Deposit, Withdraw, and Transfer. |

---

## 📄 Conventions and Best Practices

- **Clean Architecture:** Strict separation between domain, application, and infrastructure layers.
- **Early Return Pattern:** Codebase avoids nested if/else blocks for better readability.
- **Auto-Auditing:** Transactions are tracked and managed via UnitOfWork.
- **Unit Testing:** Implemented with xUnit, Moq, and FluentAssertions.
- **Decoupled Configuration:** All infrastructure and security settings are modularized using Extension Methods to keep `Program.cs` lean.
- **Dynamic Infrastructure-as-Config:** CORS policies and metadata are injected via `appsettings.json`, enabling seamless environment transitions.
- **Resilient Database Startup:** Built-in retry policies ensure migrations are applied only after the database container is fully operational.

---

## 🚀 CI/CD Pipeline & Deployment

The project follows **GitOps** principles for automated deployment:

- **Backend (Railway):** Every push to the `main` branch triggers an automated build via **Docker**. The pipeline executes database migrations and updates the API service.
- **Frontend (Vercel):** Automated deployment for the Angular application, ensuring that the latest production-ready UI is always in sync with the API.
- **Environment Synchronization:** Production URLs are dynamically injected into the Backend's CORS policy via Railway environment variables, maintaining security without code changes.

---

## 🌍 Live Demo

The application is deployed and ready for testing:
- **Frontend (UI):** [https://banking-front-lime.vercel.app](https://banking-front-nix06h3od-theonicolelis-projects.vercel.app/)
- **API Documentation (Swagger):** [http://bankingsystem-production-d907.up.railway.app/index.html](http://bankingsystem-production-d907.up.railway.app/index.html)

---

## 🧾 Final Considerations

This project was developed for **technical evaluation** purposes, demonstrating proficiency in modern **.NET 9** development, architecture patterns, and DevOps best practices.

I would like to thank you for the opportunity to participate in this technical challenge and for the chance to demonstrate my development skills and architectural decisions.

---

# 🇧🇷 PORTUGUÊS

> 📘 **Este README está disponível em Inglês e Português.** > Suba a página para a [versão em Inglês](#-english).

# 🏦 Banking System API & Dashboard

API desenvolvida em **.NET 9** para gerenciar um sistema robusto de eventos bancários.

O projeto segue os princípios de **Arquitetura Limpa (Clean Architecture)**, priorizando testabilidade, manutenibilidade e as melhores práticas de desenvolvimento. Totalmente containerizado com **Docker**.

---

## 📚 Sumário

1. [📦 Visão Geral](#-visão-geral)
2. [🧩 Estrutura do Projeto](#-estrutura-do-projeto)
3. [🚀 Executando o Projeto](#-executando-o-projeto)
4. [🌐 Endpoints Principais](#-endpoints-principais)
5. [📄 Convenções e Boas Práticas](#-convenções-e-boas-práticas)
6. [🚀 Pipeline de CI/CD e Deployment](#-pipeline-de-cicd-e-deployment)
7. [🌍 Demonstração ao Vivo](#-demonstração-ao-vivo)
8. [🧾 Considerações Finais](#-considerações-finais)

---

## 📦 Visão Geral

O Banking System é uma solução para gerenciamento de contas e transações financeiras. Além dos requisitos funcionais (depósito, saque e transferência), o projeto foca em **Arquitetura Limpa**, **Resiliência de Rede** e **Escalabilidade**.

### Funcionalidades Principais
- ✅ **Motor de Eventos Financeiros:** Processa Depósitos, Saques e Transferências através de um sistema unificado.
- ✅ **Gestão de Saldo em Tempo Real:** Consultas instantâneas de saldo e atualizações de estado para contas novas e existentes.
- ✅ **Transferências P2P Atômicas:** Transferências seguras garantindo que as contas de origem e destino sejam processadas em uma única unidade de trabalho.
- ✅ **Infraestrutura como Configuração:** Configurações de CORS e API gerenciadas inteiramente via `appsettings.json`.
- ✅ **Arquitetura Limpa:** Separação rigorosa entre as camadas WebApi, Application, Domain, SharedKernel e Infrastructure.
- ✅ **Resiliência de Banco de Dados:** Lógica de inicialização automática com migrations e políticas de retry para ambientes em contêineres.

---

## 🧩 Estrutura do Projeto

```text
BankingSystem/
├── src/
│   ├── BankingSystem.WebApi/           → Camada de Apresentação (Controllers, CORS/Swagger)
│   ├── BankingSystem.Application/      → Casos de Uso, DTOs e Handlers
│   ├── BankingSystem.Domain/           → Entidades e Lógica de Negócio
│   ├── BankingSystem.Infrastructure/   → EF Core, Migrations e Contexto de Banco
│   └── BankingSystem.SharedKernel/     → Base de Domínio (Entity, UnitOfWork, etc.)
│
├── BankingFront/                       → Projeto Angular 18 (Tratamento de erros e UI)
│
├── tests/
│   └── BankingSystem.Tests/            → Testes Unitários e de Integração
│
├── docker-compose.yml                  → Orquestra os contêineres da API e do banco
└── .dockerignore                       → Exclui arquivos desnecessários da imagem Docker
```

---

## 🚀 Executando o Projeto

### 1️⃣ Opção — Com Docker (Recomendado)

```bash
docker-compose up --build banking-api
```

📍 Swagger UI: http://localhost:8080/index.html

### 2️⃣ Opção — Local (Sem Docker)

Atualize o arquivo appsettings.json com suas credenciais locais do PostgreSQL.
Execute a aplicação:

```bash
cd src/BankingSystem.WebApi/
dotnet run
```

## 🌐 Endpoints Principais

Após iniciar a API, você pode acessar a documentação interativa em:
📍 **Swagger UI:** [http://localhost:8080/index.html](http://localhost:8080/index.html)

| Method | Endpoint | Description |
|:-------|:---------|:------------|
| `POST` | `/reset` | **System Reset:** Limpa todos os dados para garantir testes do zero com facilidade. |
| `GET`  | `/balance`| **Balance Inquiry:** Retorna o saldo atual para um account_id. |
| `POST` | `/event` | **Transaction Engine:** Endpoint unificado para Depósito, Saque e Transferência. |

---

## 📄 Convenções e boas práticas

- **Arquitetura Limpa:** Separação clara entre as camadas de domínio, aplicação e infraestrutura.
- **Early Return Pattern:** O código evita blocos if/else aninhados para melhorar a legibilidade.
- **Auditoria Automática:** Transações são rastreadas e gerenciadas via padrão UnitOfWork.
- **Testes Unitários:** Implementados com xUnit, Moq e FluentAssertions.
- **Configuração Desacoplada:** Todas as configurações de infraestrutura e segurança são modularizadas via Extension Methods para manter o Program.cs limpo.
- **Infraestrutura como Configuração:** Políticas de CORS e metadados injetados via appsettings.json, permitindo transições suaves entre ambientes (Local/Produção).
- **Resiliência no Banco de Dados:** Políticas de retry garantem que as migrations sejam aplicadas apenas quando o banco estiver pronto.

---

## 🚀 Pipeline de CI/CD e Deployment

O projeto segue os princípios de **GitOps** para deploy automatizado:

- **Backend (Railway):** Cada push na branch `main` dispara um build automatizado via **Docker**. A pipeline executa as migrations do banco de dados e atualiza o serviço da API.
- **Frontend (Vercel):** Deploy automatizado da aplicação Angular, garantindo que a interface mais recente esteja sempre sincronizada com a API.
- **Sincronização de Ambiente:** As URLs de produção são injetadas dinamicamente na política de CORS do Backend via variáveis de ambiente no Railway, mantendo a segurança sem alterações no código.

---

## 🌍 Demonstração ao Vivo

A aplicação está publicada e pronta para testes:
- **Interface (Frontend):** [https://banking-front-lime.vercel.app](https://banking-front-nix06h3od-theonicolelis-projects.vercel.app/)
- **Documentação da API (Swagger):** [http://bankingsystem-production-d907.up.railway.app/index.html](http://bankingsystem-production-d907.up.railway.app/index.html)

---

## 🧾 Considerações Finais

Este projeto foi desenvolvido para fins de **avaliação técnica**, demonstrando proficiência no desenvolvimento moderno com **.NET 9**, padrões de arquitetura e melhores práticas de DevOps.

Agradeço a oportunidade de participar deste desafio técnico e a chance de demonstrar minhas habilidades de desenvolvimento e decisões arquiteturais.

