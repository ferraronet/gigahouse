Projeto Giga House
Bem-vindo ao repositório do projeto Giga House! Este projeto consiste em uma API desenvolvida em .NET Core que permite a criação de projetos e a adição de tarefas associadas a esses projetos. Além disso, inclui uma integração com Docker para facilitar o gerenciamento e a implantação do ambiente de desenvolvimento.

Funcionalidades
API de Projetos e Tarefas: A API permite operações CRUD (Criar, Ler, Atualizar, Deletar) para gerenciar projetos e tarefas associadas.
Docker Compose: Facilita a configuração do ambiente de desenvolvimento com Docker, incluindo a configuração de um banco de dados MySQL.
Tecnologias Utilizadas
.NET Core: Framework utilizado para desenvolver a API.
MySQL: Banco de dados utilizado para armazenar informações sobre projetos e tarefas.
Docker: Utilizado para a criação de containers que encapsulam a aplicação e o banco de dados, facilitando a distribuição e o gerenciamento do ambiente de desenvolvimento.
Configuração do Ambiente com Docker Compose
Para executar o projeto localmente, siga os passos abaixo:

Clone o repositório:

bash
Copiar código
git clone https://github.com/ferraronet/gigahouse.git
cd giga-house
Configuração do Docker Compose:

Certifique-se de ter o Docker e o Docker Compose instalados em seu ambiente de desenvolvimento.
Arquivo docker-compose.yml:

O arquivo docker-compose.yml está configurado para levantar dois serviços:
app: Contém a aplicação .NET Core exposta nas portas 8080 e 8081.
mysql: Banco de dados MySQL exposto na porta 3306.
Exemplo de docker-compose.yml:

yaml
Copiar código
version: '3.8'

services:
  app:
    image: giga-house-app:latest 
    build:
      context: .
      dockerfile: Dockerfile
    environment:
      ASPNETCORE_ENVIRONMENT: Production
      ASPNETCORE_CONNECTIONSTRING: Server=mysql;Port=3306;Database=gigahouse;Uid=root;Pwd=@Gigahouse123;
    ports:
      - "8080:8080"
      - "8081:8081"
    depends_on:
      - mysql

  mysql:
    image: mysql:latest
    environment:
      MYSQL_DATABASE: gigahouse
      MYSQL_USER: gigahouse
      MYSQL_PASSWORD: "@Gigahouse123"
      MYSQL_ROOT_PASSWORD: "@Gigahouse123"
    ports:
      - "3306:3306"
    volumes:
      - mysql_data:/var/lib/mysql

volumes:
  mysql_data:
    driver: local
Executando o Docker Compose:

No diretório raiz do projeto (onde está o arquivo docker-compose.yml), execute o comando:
Copiar código
docker-compose up -d
Isso irá construir as imagens Docker e iniciar os containers especificados no arquivo docker-compose.yml.
Acessando a API:

Após iniciar os containers, você pode acessar a API através do Swagger em http://localhost:8080/swagger.
Utilize o Postman ou outra ferramenta para fazer requisições HTTP à API para criar projetos, adicionar tarefas, etc.
Contribuindo
Se você deseja contribuir com melhorias ou correções para o projeto Giga House, sinta-se à vontade para abrir uma issue ou enviar um pull request. Agradecemos sua colaboração!

Este projeto foi desenvolvido com o intuito de demonstrar boas práticas de desenvolvimento e facilitar o gerenciamento de projetos através de uma API simples e eficiente.