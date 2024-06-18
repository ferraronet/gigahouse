<h1>Projeto Giga House</h1>

Bem-vindo ao repositório do projeto Giga House! 
Este projeto consiste em uma API desenvolvida em .NET Core que permite a criação de projetos e a adição de tarefas associadas a esses projetos. 
Além disso, inclui uma integração com Docker para facilitar o gerenciamento e a implantação do ambiente de desenvolvimento.

<h3>Funcionalidades</h3>
API que gerencia Projetos e suas Tarefas
A API permite operações CRUD (Criar, Ler, Atualizar, Deletar) para gerenciar projetos e tarefas associadas.

<h3>Tecnologias Utilizadas</h3>
.NET Core 8<br>
Swagger<br>
Nlog<br>
MemoryCache<br>
AutoMapper<br>
MySQL<br>
Docker<br>
Docker Compose<br>

<h3>Configurando o Projeto</h3>

Clone o repositório:

```bash
git clone https://github.com/ferraronet/gigahouse.git
```

Certifique-se de ter o Docker e o Docker Compose instalados em seu ambiente de desenvolvimento.

Executando o Docker Compose:

No diretório raiz do projeto (onde está o arquivo docker-compose.yml), execute o comando:

```bash
docker-compose up -d
```

Isso irá construir as imagens Docker e iniciar os containers especificados no arquivo docker-compose.yml.<br>
**app**: Contém a aplicação .NET Core exposta nas portas 8080 e 8082.<br>
**mysql**: Banco de dados MySQL exposto na porta 3306.<br>

<h3>Acessando a API</h3>

Após iniciar os containers, você pode acessar a API através do Swagger em http://localhost:8080/swagger/index.html.<br>
Utilize o Postman ou outra ferramenta para fazer requisições HTTP à API para criar projetos, adicionar tarefas, etc.

<h3>Contribuindo</h3>
Se você deseja contribuir com melhorias ou correções para o projeto Giga House, sinta-se à vontade para abrir uma issue ou enviar um pull request. <br>
Agradecemos sua colaboração!<br><br>

Este projeto foi desenvolvido com o intuito de demonstrar boas práticas de desenvolvimento e facilitar o gerenciamento de projetos através de uma API simples e eficiente.
