# api-produtos
Está api tem o intuito de ser um CRUD simples para manipulação de dados referente apenas a produtos e categorias, devido ao curto tempo para desenvolve-la. E no futuro evoluir para um sistema de e-commerce, para o meu repositório pessoal.

## Tecnologias Utilizadas

 + ASP.NET Core 5.0
 + Entity Framework
 + Microsoft SQL Server
## Instalação
 + 1 - Abra seu Visual Studio
 + 2 - Modifique a ConnectionString em appsettings.json de acordo com seu banco de dados SQL Server

            "ConnectionStrings": {       
                "DefaultConnection":  "Data Source=localhost;Initial Catalog=dbProdutos;Persist Security Info=True;User ID=sa;Password=admin"  
            },

 + 3 - Execute os seguintes comandos
    + Add-Migration Initial
    + Update-Database
 + 4 - Compile e Execute o projeto api-produtos

## Requisições
Requisições para a API segue os seguintes padrões:
| Tipo | Descrição |
|---|---|
| `GET` | Retorna informações de um ou mais registros. |
| `POST` | Utilizado para criar um novo registro. |
| `PUT` | Atualiza dados de um registro. |
| `DELETE` | Remove um registro. |

## Respostas

| Código | Descrição |
|---|---|
| `200` | Requisição executada com sucesso (success).|
| `400` | Genérico: qualquer excessão na execução da requisição.|
| `404` | Registro pesquisado não encontrado (Not found).|

# Endpoints

## Categoria [api/categoria]
As categorias tem um relacionamento de 1:N com produtos e tem o intuito de organizar, uma categoria possui vários produtos, mas um produto pertence a uma unica categoria.

### Listar todos [GET api/categoria]

+ Request

        GET /api/Categoria/

+ Response 200: sucesso

        [
            {
                "id": 1,
                "nome": "Hardware"
            },
            {
                "id": 2,
                "nome": "Teclado"
            },
        ]

    Caso não encontre nenhum registro irá retornar um array vazio []

### Listar a partir do ID [GET api/categoria/{id}]

+ Params
    + id (required, number) -> id da Categoria

+ Request

            GET /api/Categoria/1

+ Response 200: sucesso

            {
                "id": 1,
                "nome": "Hardware"
            }


+ Response 404: não foi encontrado nenhum registro

          {
              "error": "Categoria não encontrada" 
          }

### Listar a partir do Nome [GET api/categoria/{nome}]

+ Params
    + nome (required, string) -> nome da Categoria

+ Request

            GET /api/Categoria/Hardware

+ Response 200: sucesso

        {
            "id": 1,
            "nome": "Hardware"
        }

    Caso não encontre nenhum registro irá retornar um array vazio []
### Registrar Categoria [POST api/categoria]

+ Request

        POST /api/Categoria/
            Body:
            {
                "nome" : "Placa Mae"
            }

+ Response 200: registrado com sucesso

        {
            "id": 2,
            "nome": "Placa Mãe"
        },

+ Response 400: Quando ocorrer algum erro

        {
            "error": "Não foi possivel cadastrar a categoria: "
        }

### Editar uma Categoria [PUT api/categoria]
+ Params
    + id (required, number) --> id da cetegória

+ Request
        
        PUT /api/Categoria/1
            Body:
            {
                "nome" : "Placa Mae"
            }

+ Response 200: registrado com sucesso

        {
            "id": 2,
            "nome": "Placa Mãe"
        },

+ Response 404: não foi encontrado nenhum registro

          {
              "error": "Categoria não encontrada" 
          }

+ Response 400: Quando ocorrer algum erro

        {
            "error": "Não foi possivel editar a categoria: "
        }
### Deletar uma Categoria [DELETE api/categoria/{id}]
+ Params
    + id (required, number) --> id da cetegória

+ Request
        
        DELETE /api/Categoria/2

+ Response 200: registrado com sucesso

        OK

+ Response 404: não foi encontrado nenhum registro

          {
              "error": "Categoria não encontrada" 
          }

+ Response 400: Quando ocorrer algum erro

        {
            "error": "Não foi possivel deletar a categoria: "
        }


## Produto [api/produto]

Os produtos são relacionados com uma categoria e contem as seguintes informações: Nome, Descricao, Valor, Quantidade, Ativo e CategoriaId

### Listar todos [GET api/produto]

+ Request

        GET /api/Produto

+ Response 200: sucesso

        [
            {
                "id": 1,
                "nome": "Geforce RTX",
                "descricao": "Uma placa de vídeo",
                "valor": 5999.99,
                "quantidade": 5,
                "ativo": true,
                "categoriaId": 1,
                "categoria": {
                    "id": 1,
                    "nome": "Hardware"
                }
            }
        ]

    Caso não encontre nenhum registro irá retornar um array vazio []

### Listar a partir do ID [GET api/produto/{id}]

+ Params
    + id (required, number) -> id do produto

+ Request

            GET /api/Produto/1

+ Response 200: sucesso

        {
            "id": 1,
            "nome": "Geforce RTX",
            "descricao": "Uma placa de vídeo",
            "valor": 5999.99,
            "quantidade": 5,
            "ativo": true,
            "categoriaId": 1,
            "categoria": {
                "id": 1,
                "nome": "Hardware"
            }
        }


+ Response 404: não foi encontrado nenhum registro

          {
              "error": "Produto não encontrado" 
          }

### Listar a partir do Nome [GET api/produto/{nome}]

+ Params
    + nome (required, string) -> nome da Categoria

+ Request

            GET /api/produto/geforce

+ Response 200: sucesso

        {
            "id": 1,
            "nome": "Geforce RTX",
            "descricao": "Uma placa de vídeo",
            "valor": 5999.99,
            "quantidade": 5,
            "ativo": true,
            "categoriaId": 1,
            "categoria": {
                "id": 1,
                "nome": "Hardware"
            }
        }

    Caso não encontre nenhum registro irá retornar um array vazio []
### Registrar Produto [POST api/produto]

+ Request

        POST /api/Produto/
            Body:
            {            
                "nome": "Geforce RTX",
                "descricao": "Uma placa de vídeo",
                "valor": 5999.99,
                "quantidade": 5,
                "ativo": true,
                "categoriaId": 1,
            }

+ Response 200: registrado com sucesso

        {
            "id": 1,
            "nome": "Geforce RTX",
            "descricao": "Uma placa de vídeo",
            "valor": 5999.99,
            "quantidade": 5,
            "ativo": true,
            "categoriaId": 1,
            "categoria": {
                "id": 1,
                "nome": "Hardware"
            }
        }

+ Response 400: Quando ocorrer algum erro

        {
            "error": "Não foi possivel cadastrar o produto: "
        }

### Editar um Produto [PUT api/produto]
+ Params
    + id (required, number) --> id do produto

+ Request
        
        PUT /api/Categoria/1
            {            
                "nome": "Geforce RTX",
                "descricao": "Uma placa de vídeo",
                "valor": 5999.99, 
                "quantidade": 5, 
                "ativo": true, 
                "categoriaId": 1, 
            }
        // Todos os parametros são opcionais, caso esteja vazio, 0 ou null serão ignorados.

+ Response 200: registrado com sucesso

        {
            "id": 1,
            "nome": "Geforce RTX",
            "descricao": "Uma placa de vídeo",
            "valor": 5999.99,
            "quantidade": 5,
            "ativo": true,
            "categoriaId": 1,
            "categoria": {
                "id": 1,
                "nome": "Hardware"
            }
        }

+ Response 404: não foi encontrado nenhum registro

          {
              "error": "produto não encontrado" 
          }

+ Response 400: Quando ocorrer algum erro

        {
            "error": "Não foi possivel editar o produto: "
        }
### Deletar uma Produto [DELETE api/produto/{id}]
+ Params
    + id (required, number) --> id da cetegória

+ Request
        
        DELETE /api/Produto/2

+ Response 200: Deletado com sucesso

        OK

+ Response 404: não foi encontrado nenhum registro

          {
              "error": "Produto não encontrado" 
          }

+ Response 400: Quando ocorrer algum erro

        {
            "error": "Não foi possivel deletar o produto: "
        }