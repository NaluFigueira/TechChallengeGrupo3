# FIAP - Tech Challenge - Grupo 3

## Requisitos

A aplicação set trata de uma lista compartilhada de contatos, com as seguintes funcionalidades:

- Permitir o cadastro de novos contatos, incluindo nome, telefone e e-mail. Associe cada contato a um DDD correspondente à região.
- Consultar e visualizar os contatos cadastrados, os quais podem ser filtrados pelo DDD da região.
- Possibilitar a atualização e exclusão de contatos previamente cadastrados.

## Solução proposta

### Arquitetura da solução

![Arquitetura de solução](https://github.com/user-attachments/assets/fbaf467b-cee9-4136-8685-252c48a9df10)

### Arquitetura técnica

![Arquitetura técnica](https://github.com/user-attachments/assets/587b6ad6-fc3e-4045-9b97-e6eb6127844d)

### Arquitetura de dados

![Arquitetura de dados](https://github.com/user-attachments/assets/08d87a62-6fdd-491b-88a3-67eb0595c13b)

### Processo de desenvolvimento

![Processo de desenvolvimento](https://github.com/user-attachments/assets/12190b5b-ef49-4e9b-bcc3-293b17c53425)

### Validações dos campos

1. Todos os campos são obrigatórios para criação
2. É possível atualizar qualquer um dos campos após a criação
3. DDD deve ser válido dentro da área do Brasil
4. Campo telefone:
   - Permitido apenas carateres numéricos
   - Número mínimo de dígitos é 8, e máximo de 9
   - Caso o número de telefone seja de 9 dígitos, o primeiro deve ser 9
5. E-mail deve ser válido

## Tecnologias e ferramentas

Desenvolvimento:

- .NET 8.0
- FluentResults
- FluentValidation
- OpenAPI
- EntityFrameworkCore
- ILogger
- Identity
- JWT Bearer Token

Testes:

- XUnit
- Bogus
- Moq
- Xunit.Gherkin
- Microsoft.AspNetCore.Mvc.Testing
- FluentAssertions

## Como executar o projeto localmente

### Requerimentos

- Ter versões mais recentes do [Docker e docker-compose](https://docs.docker.com/manuals/) instaladas.

### Instruções

1. Baixar o projeto localmente.
2. Na pasta raiz de cada MS (users, contacts-command, contacts-query) executar os comandos:

```bash
docker-compose build
```

3. E, em seguida:

```bash
docker-compose up -d
```

4. Acessar a url http://localhost:8080/swagger/index.html para ter acesso a inteface do Swagger do MS users e fazer requisições.
5. Acessar a url http://localhost:8081/swagger/index.html para ter acesso a inteface do Swagger do MS contacts-query e fazer requisições.
6. Acessar a url http://localhost:8082/swagger/index.html para ter acesso a inteface do Swagger do MS contacts-command e fazer requisições.
