# Hyperativa API

API desenvolvida em .NET 10.0 para gerenciamento de cartões de crédito com autenticação JWT e criptografia de dados.

## Pré-requisitos

- .NET 10.0 SDK
- PostgreSQL
- Visual Studio 2022, Visual Studio Code ou outro IDE de sua preferência

## Configuração do Ambiente

### 1. Banco de Dados PostgreSQL

Certifique-se de ter o PostgreSQL instalado e em execução. Crie um banco de dados para o projeto.

### 2. Configuração das Variáveis de Ambiente

Configure as seguintes variáveis no arquivo `appsettings.json` ou `appsettings.Development.json`:

#### Connection String
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=hyperativa;User Id=seu_usuario;Password=sua_senha;"
}
```

#### JWT Settings
```json
"JwtSettings": {
  "Secret": "sua-chave-secreta-super-segura-com-no-minimo-32-caracteres",
  "AccessTokenExpireInMinutes": 2,
  "RefreshTokenExpireInMinutes": 30,
  "Issuer": "hyperativa-api",
  "Audience": "hyperativa-client"
}
```

#### Crypto Settings
```json
"CryptoSettings": {
  "Key": "sua-chave-de-32-caracteres-aqui!",
  "IV": "seu-vetor-de-16-caracteres!"
}
```

## Passos para Executar o Projeto

### 1. Clonar o Repositório
```bash
git clone https://github.com/gianlucaborelli/Hyperativa.git
cd Hyperativa
```

### 2. Restaurar Dependências
```bash
dotnet restore
```

### 3. Configurar o Banco de Dados

#### Atualizar o banco de dados com as migrations
```bash
cd Hyperativa.Api
dotnet ef database update
```


### 4. Executar o Projeto

#### Via .NET CLI
```bash
cd Hyperativa.Api
dotnet run
```

#### Via Visual Studio
1. Abra o arquivo `Hyperativa.slnx`
2. Defina `Hyperativa.Api` como projeto de inicialização
3. Pressione F5 ou clique em "Start"

### 5. Acessar a Aplicação

- **API**: http://localhost:5183 ou https://localhost:7197
- **Swagger UI**: https://localhost:7197/swagger

