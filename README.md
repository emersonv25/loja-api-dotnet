# api-produtos
Está api tem o intuito de ser um CRUD simples para manipulação de dados referente apenas a produtos e categorias, devido ao curto tempo para desenvolve-la. E no futuro evoluir para um sistema de e-commerce, para o meu repositório pessoal.

## Métodos
Requisições para a API segue os seguintes padrões:
| Método | Descrição |
|---|---|
| `GET` | Retorna informações de um ou mais registros. |
| `POST` | Utilizado para criar um novo registro. |
| `PUT` | Atualiza dados de um registro. |
| `DELETE` | Remove um registro do sistema. |

## Respostas

| Código | Descrição |
|---|---|
| `200` | Requisição executada com sucesso (success).|
| `400` | Genérico: qualquer excessão na execução da requisição.|
| `404` | Registro pesquisado não encontrado (Not found).|

# Group Resources

## Categoria [api/categoria]
As categorias tem um relacionamento de 1:N com produtos e tem o intuito de organizar, uma categoria possui vários produtos, mas um produto pertence a uma unica categória.

### Listar todos [GET api/categoria]

+ Request

        GET /api/Categoria/

+ Response 200 (application/json)

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
    + id (required, number) -> id da Categória

+ Request
            GET /api/Categoria/1

+ Response 200 (application/json)

        GET /api/Categoria/1
            {
                "id": 1,
                "nome": "Hardware"
            }


+ Response 404 (application/json) não foi encontrado nenhum registro

          {
              "error": "Categoria não encontrada" 
          }

### Listar a partir do Nome [GET api/categoria/{nome}]

+ Params
    + nome (required, string) -> nome da Categória

+ Request

            GET /api/Categoria/Hardware

+ Response 200 (application/json) sucesso

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

+ Response 200 (application/json) registrado com sucesso

        {
            "id": 2,
            "nome": "Placa Mãe"
        },

+ Response 400 (application/json) Quando ocorrer algum erro

        {
            "errors": "Não foi possivel cadastrar a categoria: "
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

+ Response 200 (application/json) registrado com sucesso

        {
            "id": 2,
            "nome": "Placa Mãe"
        },

+ Response 404 (application/json) não foi encontrado nenhum registro

          {
              "error": "Categoria não encontrada" 
          }

+ Response 400 (application/json) Quando ocorrer algum erro

        {
            "errors": "Não foi possivel editar a categoria: "
        }
### Deletar uma Categoria [DELETE api/categoria/{id}]
+ Params
    + id (required, number) --> id da cetegória

+ Request
        
        PUT /api/Categoria/2

+ Response 200 (application/json) registrado com sucesso

        OK

+ Response 404 (application/json) não foi encontrado nenhum registro

          {
              "error": "Categoria não encontrada" 
          }

+ Response 400 (application/json) Quando ocorrer algum erro

        {
            "errors": "Não foi possivel deletar a categoria: "
        }


## Categoria [api/categoria]

Os produtos são, bem, produtos, eles são relacionados com uma catégoria e contem as seguintes informações: Nome, Descricao, Valor, Quantidade, Ativo e CategoriaId