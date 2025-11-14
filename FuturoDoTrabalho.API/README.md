# 🧠 Futuro do Trabalho - API C#

## 👥 Integrantes do Grupo

- **Pedro Moura Barros** – RM550260  
- **Rodrigo Fernandes dos Santos** – RM98896

---

## 🚀 Descrição do Projeto

Este projeto faz parte da **Global Solution FIAP 2025** e foi desenvolvido como uma solução tecnológica voltada ao tema **“O Futuro do Trabalho”**.

A API tem como objetivo **conectar profissionais às novas oportunidades do mercado digital**, permitindo que **recrutadores publiquem vagas** e que **candidatos se inscrevam nelas**, apresentando suas habilidades técnicas.

Ela resolve um desafio real: o mercado de trabalho está evoluindo rapidamente com a transformação digital — e essa solução facilita a integração entre empresas e talentos preparados para as profissões do futuro.

---

## ⚙️ Funcionalidades

A API oferece operações CRUD completas para três entidades principais:

### 👤 Usuários (`/api/v1/Usuarios`)
- **GET** `/api/v1/Usuarios` — Lista todos os usuários cadastrados  
- **GET** `/api/v1/Usuarios/{id}` — Retorna um usuário específico  
- **POST** `/api/v1/Usuarios` — Cria um novo usuário (ex: Recrutador)  
- **PUT** `/api/v1/Usuarios/{id}` — Atualiza um usuário existente  
- **DELETE** `/api/v1/Usuarios/{id}` — Remove um usuário  

### 💼 Vagas (`/api/v1/Vagas`)
- **GET** `/api/v1/Vagas` — Lista todas as vagas com seus candidatos  
- **GET** `/api/v1/Vagas/{id}` — Retorna uma vaga específica  
- **POST** `/api/v1/Vagas` — Cria uma nova vaga  
- **PUT** `/api/v1/Vagas/{id}` — Atualiza uma vaga existente  
- **DELETE** `/api/v1/Vagas/{id}` — Remove uma vaga e seus candidatos  

### 👨‍💻 Candidatos (`/api/v1/Candidatos`)
- **GET** `/api/v1/Candidatos` — Lista todos os candidatos  
- **GET** `/api/v1/Candidatos/{id}` — Retorna um candidato específico  
- **GET** `/api/v1/Candidatos/vaga/{vagaId}` — Candidatos de uma vaga  
- **POST** `/api/v1/Candidatos` — Registra um novo candidato  
- **PUT** `/api/v1/Candidatos/{id}` — Atualiza dados de um candidato  
- **DELETE** `/api/v1/Candidatos/{id}` — Remove um candidato  

---

## 📌 Versionamento da API

A API utiliza versionamento através de rotas, seguindo o padrão `/api/v{versão}/[controller]`.

- **Versão atual:** `v1`
- **Estrutura:** Todos os endpoints estão organizados sob `/api/v1/`
- **Exemplos:**
  - `/api/v1/Usuarios`
  - `/api/v1/Vagas`
  - `/api/v1/Candidatos`

Esta estrutura permite evoluir a API criando novas versões (ex: `/api/v2/`) sem quebrar compatibilidade com clientes que utilizam versões anteriores.

---

## 🧩 Forma de Funcionamento

### 🧰 Pré-requisitos
- .NET 8.0 SDK  
- Visual Studio 2022 / VS Code
- **SQL Server** (Express, LocalDB ou Developer Edition) - **OBRIGATÓRIO**
  - ⚠️ **Importante:** O SQL Server deve estar instalado antes de executar a aplicação
  - Veja a seção [Banco de Dados](#-banco-de-dados) abaixo para instruções de instalação  

### ▶️ Execução

1. Clone o repositório:
   ```bash
   git clone https://github.com/pedromourabarros/GS_C_SHARP.git
   ```
2. Entre na pasta:
   ```bash
   cd FuturoDoTrabalho.API
   ```
3. Restaure as dependências:
   ```bash
   dotnet restore
   ```
4. Rode a aplicação:
   ```bash
   dotnet run
   ```
5. Acesse no navegador:  
   👉 `https://localhost:5001/swagger`  
   ou  
   👉 `http://localhost:5000/swagger`

---

## 🗄️ Banco de Dados

O projeto usa **SQL Server** como banco de dados relacional, atendendo aos requisitos do projeto.  
O banco de dados é criado automaticamente na primeira execução através do Entity Framework Core.

### 🔄 Como Funciona

1. **Criação Automática:**
   - Na primeira execução do projeto (`dotnet run`), o Entity Framework Core detecta que o banco não existe
   - Automaticamente cria o banco de dados `FuturoDoTrabalhoDB` no SQL Server
   - Cria todas as tabelas necessárias: `Usuarios`, `Vagas`, `Candidatos`
   - Configura relacionamentos e índices automaticamente

2. **Estrutura do Banco:**
   - **Tabela Usuarios:** Armazena usuários do sistema (recrutadores, administradores)
   - **Tabela Vagas:** Armazena vagas de emprego publicadas
   - **Tabela Candidatos:** Armazena candidatos inscritos nas vagas
   - **Relacionamento:** Uma vaga pode ter vários candidatos (1:N)
   - **Cascade Delete:** Ao deletar uma vaga, os candidatos associados são removidos automaticamente

3. **Persistência:**
   - Todos os dados são salvos permanentemente no SQL Server
   - O banco fica disponível mesmo após fechar a aplicação
   - Os dados persistem entre execuções do projeto

### 📋 Pré-requisitos do Banco de Dados

**É necessário ter o SQL Server instalado.** Você pode usar uma das seguintes opções:

1. **SQL Server Express** (Recomendado - Gratuito)
   - Download: [SQL Server Express](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)
   - Instale a versão Express (gratuita)
   - Durante a instalação, escolha "Instância Padrão" ou "Instância Nomeada"

2. **SQL Server LocalDB** (Incluído no Visual Studio)
   - Se você tem Visual Studio instalado, o LocalDB geralmente já está incluído
   - Se não tiver, pode ser instalado separadamente

3. **SQL Server Developer Edition** (Gratuito para desenvolvimento)
   - Disponível no mesmo link acima

### ⚙️ Configuração da Connection String

A connection string está configurada no arquivo `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=FuturoDoTrabalhoDB;Trusted_Connection=true;TrustServerCertificate=true;Integrated Security=true"
}
```

**Opções de Connection String:**

- **Para SQL Server Express (instância padrão):**
  ```
  Server=localhost;Database=FuturoDoTrabalhoDB;Trusted_Connection=true;TrustServerCertificate=true
  ```

- **Para SQL Server Express (instância nomeada):**
  ```
  Server=localhost\SQLEXPRESS;Database=FuturoDoTrabalhoDB;Trusted_Connection=true;TrustServerCertificate=true
  ```

- **Para SQL Server LocalDB:**
  ```
  Server=(localdb)\\mssqllocaldb;Database=FuturoDoTrabalhoDB;Trusted_Connection=true;TrustServerCertificate=true
  ```

### 🔧 Troubleshooting

**Erro: "Unable to locate a Local Database Runtime installation"**

Este erro ocorre quando o SQL Server não está instalado. Soluções:

1. **Instalar SQL Server Express:**
   - Baixe e instale o SQL Server Express (gratuito)
   - Link: https://www.microsoft.com/pt-br/sql-server/sql-server-downloads
   - Após instalar, reinicie o Visual Studio/terminal

2. **Verificar se o SQL Server está rodando:**
   - Abra o "SQL Server Configuration Manager"
   - Verifique se o serviço "SQL Server (MSSQLSERVER)" ou "SQL Server (SQLEXPRESS)" está rodando

3. **Verificar a connection string:**
   - Se instalou uma instância nomeada (ex: SQLEXPRESS), atualize a connection string no `appsettings.json`
   - Exemplo: `Server=localhost\SQLEXPRESS;...`

---

## 🔄 Fluxo de Dados

O diagrama abaixo mostra a relação entre as entidades do sistema. O diagrama foi criado utilizando **Draw.io** e está disponível no arquivo `diagrama-fluxo-dados.drawio` na raiz do repositório (fora da pasta `FuturoDoTrabalho.API/`).

![Fluxo de Dados](images/fluxo-de-dados.png)

> 📌 **Nota:** O arquivo fonte do diagrama (`diagrama-fluxo-dados.drawio`) está na raiz do repositório para facilitar o acesso e edição. A imagem exportada (`fluxo-de-dados.png`) está na pasta `images/` dentro do projeto.

### 🧱 Estrutura das Entidades

#### 🧍‍♂️ Usuario
- Id  
- Nome  
- Email (único)  
- Tipo (ex: Recrutador/Admin)

#### 💼 Vaga
- Id  
- Título  
- Descrição  
- Área  
- DataPublicacao  
- **Relacionamento:** 1 vaga → vários candidatos

#### 👨‍💻 Candidato
- Id  
- Nome  
- Habilidades  
- VagaId  
- **Relacionamento:** cada candidato pertence a uma vaga

---

## 📘 Documentação Swagger

A API possui documentação interativa via **Swagger UI**:  
🔗 `https://localhost:5001/swagger` ou `http://localhost:5000/swagger`

Lá você pode:
- Ver todos os endpoints  
- Testar requisições diretamente  
- Visualizar modelos e respostas em tempo real  

---

## ▶️ Exemplo Rápido no Swagger

1. Acesse `POST /api/v1/Usuarios`
2. Clique em **Try it out**
3. Envie o JSON:
   ```json
   {
     "nome": "João Silva",
     "email": "joao@empresa.com",
     "tipo": "Recrutador"
   }
   ```
4. Clique em **Execute**  
✅ Resultado: Status `201 Created`

---

## 🎥 Vídeo de Demonstração

[🎬 Assista ao vídeo no YouTube](https://youtu.be/5_N0rsl_7rM)

> Demonstração completa da API em funcionamento, testando todos os endpoints no Swagger.  
> Duração: ~5 minutos.

---

## 📁 Estrutura do Projeto

### 📂 Estrutura do Repositório

```
GS-CSHARP/
├── FuturoDoTrabalho.API/          # Projeto principal da API
│   ├── Controllers/
│   │   └── V1/                    # Controllers versionados
│   │       ├── UsuariosController.cs
│   │       ├── VagasController.cs
│   │       └── CandidatosController.cs
│   ├── Data/
│   │   └── ApplicationDbContext.cs
│   ├── DTOs/                      # Data Transfer Objects
│   │   ├── UsuarioDTO.cs
│   │   ├── VagaDTO.cs
│   │   └── CandidatoDTO.cs
│   ├── Models/                    # Entidades do domínio
│   │   ├── Usuario.cs
│   │   ├── Vaga.cs
│   │   └── Candidato.cs
│   ├── images/
│   │   └── fluxo-de-dados.png     # Imagem do diagrama de fluxo
│   ├── Properties/
│   │   └── launchSettings.json
│   ├── Program.cs                 # Configuração principal
│   ├── appsettings.json           # Configurações (connection string)
│   ├── appsettings.Development.json
│   ├── FuturoDoTrabalho.API.csproj
│   └── README.md                  # Este arquivo
├── diagrama-fluxo-dados.drawio    # Diagrama do Draw.io (requisito)
└── GS - 2o Semestre de 2025.pdf   # Documento com requisitos do projeto
```

### 📝 Arquivos Importantes

- **`diagrama-fluxo-dados.drawio`**: Diagrama do fluxo de dados criado no Draw.io (requisito do projeto)
- **`FuturoDoTrabalho.API/README.md`**: Documentação completa do projeto
- **`FuturoDoTrabalho.API/images/fluxo-de-dados.png`**: Imagem exportada do diagrama para visualização no README
- **`GS - 2o Semestre de 2025.pdf`**: Documento com os requisitos do projeto (referência)

### 🗑️ Arquivos que Podem Ser Ignorados

Os seguintes arquivos/pastas são gerados automaticamente e não precisam ser versionados:

- **`bin/`** e **`obj/`**: Pastas geradas durante a compilação (devem estar no `.gitignore`)
- **`*.db`**: Arquivos de banco de dados locais (se houver algum arquivo SQLite antigo, pode ser removido)
- **`*.pdb`**: Arquivos de debug gerados durante a compilação

> 💡 **Dica:** Certifique-se de que o arquivo `.gitignore` está configurado para ignorar esses arquivos.

---

## 🛠️ Tecnologias Utilizadas

| Tecnologia | Função |
|-------------|--------|
| **ASP.NET Core 8.0** | Framework principal da API |
| **Entity Framework Core 8.0** | ORM para o banco de dados |
| **SQL Server** | Banco de dados relacional (LocalDB/Express) |
| **Swagger / OpenAPI** | Documentação interativa |
| **C#** | Linguagem de programação |

---

## ✅ Boas Práticas Implementadas

- API RESTful estruturada  
- Versionamento (`/api/v1/`)  
- Status codes corretos (200, 201, 400, 404, etc.)  
- DTOs para transferência de dados  
- Relacionamentos e **Cascade Delete**  
- Documentação detalhada com **Swagger**  
- Validações básicas implementadas  

---

## 🔬 Passos de Teste (Resumo)

1. Criar Usuário → `POST /api/v1/Usuarios`  
2. Criar Vaga → `POST /api/v1/Vagas`  
3. Criar Candidato → `POST /api/v1/Candidatos`  
4. Listar tudo → `GET /api/v1/Vagas` e `GET /api/v1/Candidatos`  
5. Atualizar / Deletar e validar respostas no Swagger  

---

## 🌟 Melhorias Futuras

- Autenticação e autorização (JWT)  
- Paginação e filtros avançados  
- Upload de currículos  
- Sistema de notificações  
- Avaliação de candidatos  
- Relatórios e métricas  

---

**Desenvolvido para Global Solution FIAP 2025**  
*Conectando profissionais ao futuro do trabalho*
