# ClientAPI

ClientAPI é um projeto de exemplo para gerenciar clientes. Ele inclui recursos como criar, atualizar e excluir clientes, bem como consultar informações de clientes.

## Tecnologias Utilizadas

- .NET 6
- Entity Framework
- MongoDB
- MediatR
- RabbitMQ
- xUnit

## Visão Geral

O ClientAPI utiliza o padrão CQRS para separar comandos e consultas. Os comandos são tratados por um banco de dados em memória, enquanto as consultas são tratadas pelo MongoDB. O banco de dados em memória é usado por suas operações de escrita rápidas, enquanto o MongoDB é usado por sua flexibilidade e escalabilidade para operações de leitura.

Os dados gravados no banco de dados em memória são persistidos no MongoDB, garantindo durabilidade e consistência dos dados.

## Como Começar

Existe um arquivo de docker compose na raiz do projeto, nele contém o MongoDB, RabbitMQ e também o Mongo Express para visualização do Banco.

Para começar com este projeto, siga estas etapas:

1. Clone o repositório para sua máquina local.
2. Certifique-se de ter o Docker instalado e iniciado.
3. Abra um terminal e navegue até o diretório raiz do projeto.
4. Execute o seguinte comando para construir e executar os contêineres do Docker:

`docker-compose up -d`

5. Abra a solução do projeto.
6. Rode o projeto.

Visualizações do navegador:

RabbitMQ - http://localhost:15672/
MongoExpress - http://localhost:8081/

## Sobre

+ Projeto seguindo o padrão DDD, princípios SOLID, boas práticas e legibilidade.
+ Testes unitários foram realizados com xUnit, usando moq, padrão AAA e Fluent Assertions.

---
⌨️ por - [Thiago Borges Valadão](https://www.linkedin.com/in/thiagoborgesv/)
