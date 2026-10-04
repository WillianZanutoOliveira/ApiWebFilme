[🇺🇸 English](0003-startup-data-seeding.en.md)

# ADR-0003: Seed dos dados de referência no startup e requisições GET somente leitura

- **Status:** Aceito
- **Data:** 2026-10-03

## Contexto

O endpoint original de premiações reconstruía o banco local em toda requisição `GET /v1/api/filmes/premios`.

Isso fazia com que uma operação de leitura executasse escritas destrutivas antes de retornar os dados:

1. excluir todos os registros de filmes;
2. ler o dataset CSV;
3. repopular o banco;
4. calcular a resposta.

Isso gera trabalho desnecessário e faz com que uma requisição GET deixe de ser somente leitura, o que é indesejável para concorrência, observabilidade e semântica da API.

O construtor do DbContext também chamava `EnsureCreated()`, introduzindo um efeito colateral de infraestrutura durante a construção do objeto.

## Decisão

Mover a inicialização do banco e o seed dos dados de referência para o startup da aplicação.

Um `DatabaseSeeder` dedicado:

- verifica se os dados de filmes já existem;
- localiza o dataset CSV de referência;
- falha rapidamente se o dataset obrigatório estiver ausente;
- carrega os dados apenas quando o banco está vazio.

O controller agora executa somente a consulta necessária para retornar os resultados de premiação.

A criação do banco também é removida do construtor do DbContext e passa para o fluxo explícito de composição no startup.

## Consequências

### Positivas

- requisições GET não alteram mais o estado da persistência;
- requisições repetidas evitam parsing do CSV e reescrita completa do banco;
- separação mais clara entre startup/infraestrutura e comportamento de consulta HTTP;
- menor acoplamento no controller;
- comportamento mais seguro sob requisições concorrentes;
- raciocínio operacional mais simples.

### Trade-off

A aplicação atual de portfólio executa o seed de forma síncrona durante o startup. Em um dataset de produção maior, isso poderia ser movido para um processo de migration/deployment ou para um job dedicado de inicialização.
