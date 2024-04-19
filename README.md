# FIAP - Tech Challenge 1 - Grupo 3

## Requisitos

A aplicação deve realizar o cadastro de contatos regionais, podendo:
  -  Permitir o cadastro de novos contatos, incluindo nome, telefone e e-mail. Associe cada contato a um DDD correspondente à região.
  -  Consultar e visualizar os contatos cadastrados, os quais podem ser filtrados pelo DDD da região.
  -  Possibilitar a atualização e exclusão de contatos previamente cadastrados.
                      
## Solução proposta

![Tech Challenge - Fase 1](https://github.com/NaluFigueira/TechChallenge1Grupo3/assets/24214761/10ecfb21-98d2-4866-8d47-c8e9ab05b377)

### Validações dos campos

1. Todos os campos são obrigatórios para criação
2. É possível atualizar qualquer um dos campos após a criação
3. DDD deve ser válido dentro da área do Brasil
4. Campo telefone:
   - Permitido apenas carateres numéricos
   - Número mínimo de dígitos é 8, e máximo de 9
   - Caso o número de telefone seja de 9 dígitos, o primeiro deve ser 9
5. E-mail deve ser válido
