# Ploomes Challenge - .NET(C#)

## Conteúdo

- [Informações gerais](#informações-gerais)
- [Acesso a ploomesAPI](#execução-e-testes)
- [Estrutura da api](#estrutura-da-api)
- [Estrutura do projeto](#estrutura-do-projeto)
- [Possíveis melhorias](#possíveis-melhorias)

## Informações gerais

Esse projeto é a implementação de uma API para postagem de mensagens, onde nele é possível logar com um usuário criado e adicionar mensagens ao sistema, sendo posteriormente possível ler, editar e deletar. Além disso foi implementado a autenticação por JWT com o objetivo de trazer mais segurança para dentro da API. Ademais, foram realizados testes unitários dos métodos presentes na API, com o objetivo de trazer maior qualidade ao código.
<br/>
<br/>

### Tecnologias

As principais ferramentas que possibilitaram esse projeto são:

- ASP .NET Core 7;
- Entity Framework Core 7;
- AutoMapper;
- SQL Server;
- JWT;
- Swagger;
- XUnit;
- Moq;
- Azure DevOps;

## Acesso a API

A API está hospedada no Azure e pode ser acessada através do link: <a href="https://ploomes-challenge.azurewebsites.net/" target="_blank">ploomesAPI</a>

<br/>
<br/>

## Estrutura da API

A fim de permitir requisições a API, aqui contém uma breve explicação das rotas.

#### **/api/users**:

Rota responsável pela criação e realizar login do usuário, retornando um token JWT para ser utilizado nas demais requisições.

- Em caso de sucesso o retorno será:
  - **200** "Ok";
- Em caso de falha o retorno será:
  - **500** "Internal server error" com status 500 quando a API está com algum problema. <br/>

#### **/api/messages**:

Rota responsável por realizar as operações de CRUD das mensagens, sendo necessário o token JWT para realizar as requisições.

- Em caso de sucesso o retorno será:
  - **200** "Ok";
- Em caso de falha na autenticação o retorno será:
  - **401** "Unauthorized" com status 401 quando o token JWT não é válido ou não foi informado;
- Em caso de falha o retorno será:
    - **500** "Internal server error" com status 500 quando a API está com algum problema. <br/>

#### **/api/experimental**:    

Rota que tinha como objetivo trazer outras funcionalidades para a API, entretanto são funcionalidades a parte e que não fazem parte do contexto da API em si.

- Em caso de sucesso o retorno será:
  - **200** "Ok";
- Em caso de falha na autenticação o retorno será:
    - **400** "Bad Request" tendo em vista que não foi feita uma requisição da maneira correta;
- Em caso de falha o retorno será:
    - **500** "Internal server error" com status 500 quando a API está com algum problema. <br/>


## Estrutura do projeto

O projeto foi estruturado utilizando uma arquitetura de _Aplication, Domain, Infrastructure, e Entities_, onde cada camada tenta isolar suas responsabilidades e se manter o mais desacoplada possível.

### Considerações sobre a implementação:

- Foram criados testes unitários para boa parte da aplicação, o framework utilizado para _mock_ e asserções foi o **xUnit**.
- A fim de trazer maior segurança, foi criado variáveis de ambiente para utilização do banco de dados, dessa forma impedindo a necessidade de expor dados sensíveis no código.

## Possíveis melhorias

Seria interessante a utilização de uma query no banco de dados para a busca de mensagens, ao invés de realizar a busca através do método _List()_, tendo em vista que em situações onde seja necessária fazer um filtro, eu estaria passando passando um _Where()_ em cima de uma lista que já foi pega no banco de dados e em quantidade muito altas de mensagens isso geraria grande desperdício de memoria.
Outro ponto seria o termino de testes unitários em todos os métodos da API, tendo em vista que a cobertura de testes ainda não é 100%.
Utilização do BlobStorage para trabalhar com arquivos, tendo em vista que o armazenamento em memória não é uma boa prática para arquivos.