# 🧑‍💻 GestaoClientes - CRUD WPF + REST API

Projeto acadêmico para estudo de arquitetura em camadas e consumo de API REST em uma aplicação desktop WPF, utilizando o padrão MVVM.

---

## 🔧 Tecnologias Utilizadas

- [.NET 9](https://dotnet.microsoft.com)
- WPF
- MVVM Pattern
- ASP.NET Core Web API
- Docker + Docker Compose
- SQL Server 2022

---

## 📡 Endpoints da API

| Método | Rota                                | Descrição                    |
|--------|--------------------------------------|------------------------------|
| GET    | `/api/Cliente/ListarClientes`       | Lista todos os clientes      |
| POST   | `/api/Cliente/AdicionarCliente`     | Adiciona um novo cliente     |
| PUT    | `/api/Cliente/AtualizarCliente`     | Atualiza um cliente existente |
| DELETE | `/api/Cliente/ExcluirCliente`       | Remove um cliente            |

🔗 **Swagger UI**: [https://127.0.0.1:5231/swagger/index.html](https://127.0.0.1:5231/swagger/index.html)

---

## 🚀 Rodando Localmente

### Pré-requisitos

- [.NET SDK 9.0+](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (com Docker Compose habilitado)

### Passo a passo

1. Clone o repositório:

```bash
git clone https://github.com/MichelSoares/GestaoClientes.git
cd GestaoClientes
```

2. Execute o script de inicialização:

```bash
build_app_e_compose.bat
```

Esse script realiza:

- Desativação da conversão automática de CRLF/LF no Git
- Conversão de arquivos `.sh` para LF
- Compilação da aplicação WPF (`GestaoClientes.APP`)
- Inicialização dos containers com Docker Compose (`API + SQL Server`)
- Execução automática do script SQL para criação do banco

3. Execute aplicação:

Após rodar o script, abra a pasta:

```bash
GestaoClientes/GestaoClientes.APP/bin/Debug/net9.0-windows/
```

Execute o arquivo:

GestaoClientes.APP.exe

---

## 📁 Estrutura do Projeto

```bash
GestaoClientes/
├── GestaoClientes.APP      # Aplicação WPF (interface)
├── GestaoClientes.API      # API REST .NET
├── GestaoClientes.Domain   # Camada de domínio (entidades, interfaces)
├── GestaoClientes.Infra    # Camada de infraestrutura (EF Core)
├── scripts/                # Scripts de banco (SQL + shell)
└── build_app_e_compose.bat # Script automatizado de build + docker
```

---

## 👨‍💻 Autor

- [@MichelSoares](https://github.com/MichelSoares)

---
