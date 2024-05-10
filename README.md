# FIAP - Tech Challenge 1 - Grupo 3

## Requisitos

A aplicação deve realizar o cadastro de contatos regionais, podendo:

- Permitir o cadastro de novos contatos, incluindo nome, telefone e e-mail. Associe cada contato a um DDD correspondente à região.
- Consultar e visualizar os contatos cadastrados, os quais podem ser filtrados pelo DDD da região.
- Possibilitar a atualização e exclusão de contatos previamente cadastrados.

## Solução proposta

![Tech Challenge - Fase 1_page-0001](https://github.com/NaluFigueira/TechChallenge1Grupo3/assets/24214761/87745589-427d-44e7-a84a-4df1f4e6614c)

### Validações dos campos

1. Todos os campos são obrigatórios para criação
2. É possível atualizar qualquer um dos campos após a criação
3. DDD deve ser válido dentro da área do Brasil
4. Campo telefone:
   - Permitido apenas carateres numéricos
   - Número mínimo de dígitos é 8, e máximo de 9
   - Caso o número de telefone seja de 9 dígitos, o primeiro deve ser 9
5. E-mail deve ser válido

## Como executar o projeto localmente

### Requerimentos

- Ter versões mais recentes do [Docker e docker-compose](https://docs.docker.com/manuals/) instaladas.

### Instruções

1. Baixar o projeto localmente.
2. No arquivo `docker-compose.yml` alterar os campos `<your_password>` para a senha que gostaria de colocar para o servidor do SQL Server. O acesso ao servidor é feito com o user padrão SA. É possível inserir outros usuários neste mesmo arquivo, caso seja necessário.
3. No arquivo `appsettings.json` alterar o campo `DefaultConnection.Password` para a mesma senha inserida no passo 2. Caso tenha sido inserido um novo usuário, é preciso alterar a chave em `DefaultConnection.User`.
4. Na raiz do projeto executar o comando:
```bash
docker-compose build
```

4. E, em seguida:

```bash
docker-compose up
```

5. Acessar a url http://localhost:8080/swagger/index.html para ter acesso a inteface do Swagger e fazer requisições.
6. Para encerrar a aplicação, basta cancelar o comando executado em 4.
