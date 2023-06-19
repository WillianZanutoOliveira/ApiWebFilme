# API RESTful do Golden Raspberry Awards

Esta é uma API RESTful desenvolvida em C# .NET para obter informações sobre os indicados e vencedores da categoria Pior Filme do Golden Raspberry Awards.

## Retorno da API
- Obter o produtor com maior intervalo entre dois prêmios consecutivos, e o que obteve dois prêmios mais rápido.

## Requisitos

- .NET 7.0 SDK instalado ([Download](https://dotnet.microsoft.com/pt-br/download/dotnet/7.0))
- Visual Studio ou Visual Studio Code (opcional)

## Configuração

1. Clone o repositório para o seu ambiente local:
   ```bash
   git clone https://github.com/WillianZanutoOliveira/ApiWebFilme.git
   ```
   
2. Abra o projeto no Visual Studio ou Visual Studio Code.

3. Certifique-se de que o arquivo CSV está na pasta Assets com o nome do arquivo "movies.csv" esteja presente na raiz do projeto da API, contendo os dados dos filmes.

4. O banco de dados em memória será utilizado automaticamente pelo projeto, não sendo necessária nenhuma instalação externa.

## Executando o projeto

### Visual Studio

- Clique em "Start" ou pressione F5 para iniciar a aplicação.

### Visual Studio Code

- Abra um terminal na pasta do projeto e execute o seguinte comando:

```bash
dotnet run --project ApiWebFilme.csproj
```

## Executando o projeto

- O projeto tem o swagger implementado da API será executado e estará disponível na URL [https://localhost:7017;http://localhost:5159] ou verifique no cmd qual é a porta que ele está rodando o projeto.

## Testes de Integração

- Os testes de integração são executados para verificar se os endpoints da API estão retornando os resultados corretos.

### Visual Studio

- Abra o Test Explorer (Ctrl+R, A) e clique em "Run All" para executar os testes de integração.

### Visual Studio Code

- Abra um terminal na pasta do projeto de testes e execute o seguinte comando:

```bash
dotnet test
```

- Os resultados dos testes serão exibidos no console.