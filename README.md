# api-loja
Uma api em desenvolvimento para um projeto de e-commerce completo: com produtos,categorias, pedidos, frete, pagamento e etc.


## Tecnologias Utilizadas

 + ASP.NET Core 5.0
 + Entity Framework Core
 + Microsoft SQL Server ou MySql
## Instalação
 + 1 - Abra seu Visual Studio
 + 2 - Modifique a ConnectionString em appsettings.json de acordo com seu banco de dados MsSQL ou MySQL

            "ConnectionStrings": {       
                 "DB" : "mysql", // mssql ou mysql
                 "MsSQLConnection": "Data Source=localhost;Initial Catalog=Loja;Persist Security Info=True;User ID=sa;Password=admin",
                 "MySqlConnection": "Data Source=localhost;Initial Catalog=Loja;Persist Security Info=True;User ID=root;Password="
            },

 + 3 - Execute os seguintes comandos para a criação do banco de dados via EF Core
    + Add-Migration Initial
    + Update-Database
 + 4 - Compile e Execute o projeto api-loja


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
| `401` | Usuário Não autorizado (Not Authorizated).|
## -------------------------------------------------------------------------------------
### OBS: Tinha escrito a documentação dos end-points, porém acabou ficando muito desatualizado. Irei manter apenas o Swagger e irei documentar com o Postman por ser prático.