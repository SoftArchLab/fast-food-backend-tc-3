
# FastFoodProject

O FastFoodProject visa realizar o controle de pedidos de lanchonetes, sendo um sistema de autoatendimento de fast food. 
Pois, um sistema de controle de pedidos é essencial para garantir que a lanchonete possa atender os clientes de maneira eficiente, gerenciando seus pedidos e estoques de forma adequada. 

## Instalação

Para a execução correta do sistema é necessário a utilização do Docker. 
Com a instalação e configuração correta do docker, abrindo o terminal na raiz do projeto, executaremos os seguintes comandos:

```bash
  docker compose up -d
```

Esse comando subirá todos os containers necessários para o funcionamento correto do sistema. Porém, ainda é necessário executar a atualização do banco de dados com as configurações iniciais.
Para isso, execute o seguinte comando:

```bash
  dotnet ef database update --project FastFood.Infra.Data/FastFood.Infra.Data.csproj --startup-project FastFood.API
```

Com isso a configuração foi finalizada, se tudo correu como esperado teremos os containers fastfood_app e fastfood_db rodando corretamente. 

Por fim, basta acessar realizar as requisições para a API, pela rota https://localhost:8080/.

Obs: Para facilitar temos um script de inicialização do banco de dados com dados, se quiser execute eles, está no final deste Readme.


## Documentação da API

#### Coleção Postman

Você pode baixar a coleção de requisições da API FastFood clicando no link abaixo:

[📥 Baixar coleção (visualizar no GitHub)](https://github.com/SoftArchLab/fast-food-backend-tc-2/blob/feature/migration-clean-architecture/Collections/FastFood_Collections_API.json)

#### Autenticação

Para utilizar os endpoits é necessário um token, para garantir o nível de acesso a cada endpoint.

Sendo:

```
  Administrador: "Admin"
  Cliente: "Customer"
  Visitante: "Guest"
```

#### Autenticar Usuário.

Endpoint que autentica usuários e retornar o token para utilização da API.
Esse token tem duração de 1 hora.

```
  POST /Login
```
| Parâmetro   | Tipo       | Descrição                           | 
| :---------- | :--------- | :---------------------------------- |
| `email` | `string` | **Opcional**. email do usuário. |
| `taxId` | `string` | **Opcional**. CPF ou CNPJ do usuário. |
| `password` | `string` | **Opcional**. Senha do usuário. |
| `isGuest` | `boolean` | **Obrigatório**. Verificador de nivel de acesso token. |

Caso o Parâmetro isGuest seja informado com o valor true, os demais campos serão ignorados. Porém, se tornam obrigatórios para a Autenticação do usuário com niveis de acesso superiores.

*Obs*: Todos parâmetros no Body da requisição. 


## Fluxo de compra

Tendo acesso de Customer ou Guest, o usuário tem acesso aos endpoints de visualização e alteração de seu usuário, visualização de categorias e produtos, além de acesso a gerencimento de seu carrinho e geração de pedidos.

### Usuários

#### Retornar Usuário

Endpoint que retorna o usuário.

```
  GET User/GetUserById/{id}
```

#### Adicionar Usuário.

Endpoint que adiciona usuários com permissão de Customer.

```
  POST User/AddCustomerUser
```
| Parâmetro   | Tipo       | Descrição                           | 
| :---------- | :--------- | :---------------------------------- |
| `name` | `string` | **Obrigatório**. Nome do usuário. |
| `taxId` | `string` | **Obrigatório**. CPF ou CNPJ do usuário. |
| `email` | `string` | **Obrigatório**. Email do usuário. |
| `password` | `string` | **Obrigatório**. Senha do usuário. |

*Obs*: Todos parâmetros no Body da requisição. 

#### Atualizar Usuário.

Endpoint que adiciona usuários com permissão de Customer.

```
  POST User/UpdateUser/{id}
```
| Parâmetro   | Tipo       | Descrição                           | 
| :---------- | :--------- | :---------------------------------- |
| `id` | `int` | **Obrigatório**. Identificador do usuário. |
| `name` | `string` | **Obrigatório**. Nome do usuário. |
| `taxId` | `string` | **Obrigatório**. CPF ou CNPJ do usuário. |
| `email` | `string` | **Obrigatório**. Email do usuário. |
| `password` | `string` | **Obrigatório**. Senha do usuário. |
| `role` | `string` | **Obrigatório**. Senha do usuário. |

*Obs*: Apenas id na rota demais parâmetros no Body da requisição. 

#### Remover Usuário.

Endpoint que remove usuários.

```
  POST User/RemoveUser/{id}
```
| Parâmetro   | Tipo       | Descrição                           | 
| :---------- | :--------- | :---------------------------------- |
| `id` | `int` | **Obrigatório**. Identificador do usuário. |

*Obs*: Parâmetro na rota da requisição. 
Apenas para niveis de acesso Customer e admin. 

### Categorias

#### Retornar todas as categorias.

Endpoint que retorna todas categorias cadastradas.

```
  GET Category/GetCategories/
```

#### Retornar uma categoria específica.

Endpoint que retorna a categoria pelo id.

```
  GET Category/GetCategoryById/{id}
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `id` | `int` | **Obrigatório**. Identificador da categoria. |

*Obs*: Parâmetro na rota da requisição. 


### Produtos

#### Retornar todos os produtos

Endpoint que retorna todos produtos cadastrados.

```
  GET Product/GetProducts/
```

#### Retornar um produto específico

Endpoint que retorna o produto pelo id.

```
  GET Product/GetProductById/{id}
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `id` | `int` | **Obrigatório**. Identificador do produto. |

*Obs*: Parâmetro na rota da requisição. 


### Carrinho

#### Retornar o carrinho do usuário

Endpoint que retorna o carrinho pelo id do usuário.

```
  GET Cart/GetUserCart/{userId}
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `userId` | `string` | **Obrigatório**. Identificador do usuário. |

*Obs*: Parâmetro na rota da requisição. 


#### Adicionar item ao carrinho do 

Endpoint que adiciona item ao carrinho.
Esse endpoint também é responsável por criar o carrinho, portanto, para adicionar os demais itens ao mesmo carrinho é necessário informar o cartId.

```
  POST Cart/AddCartItem/
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `userId` | `guid` | **Obrigatório**. Identificador do usuário. |
| `productId` | `int` | **Obrigatório**. Identificador do produto. |
| `quantity` | `guid` | **Obrigatório**. Quantidade desejada do produto. |

*Obs*: Todos parâmetros no Body da requisição. 

#### Remover item do carrinho.

Endpoint que remove item do carrinho.

```
  DELETE Cart/RemoveCartItem/
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `userId` | `guid` | **Obrigatório**. Identificador do usuário. |
| `productId` | `int` | **Obrigatório**. Identificador do produto. |
| `quantity` | `guid` | **Obrigatório**. Quantidade desejada do produto. |

*Obs*: Todos parâmetros no Body da requisição. 

#### Gerar pedido do carrinho 

Endpoint que finaliza o carrinho e gera o pedido.

```
  POST Cart/GenerateOrderFromCart/{id}
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `cartId` | `int` | **Obrigatório**. Identificador do carrinho. |

*Obs*: Todos parâmetros no Body da requisição. 


### Monitoramento de pedidos

#### Listar pedidos em preparação 

Endpoint que exibe os pedidos em preparação em ordem de criação.

```
  POST /Order/GetInPreparationOrders/
```

#### Listar pedidos prontos 

Endpoint que exibe os pedidos prontos para retirada em ordem de criação.

```
  POST /Order/GetInPreparationOrders/
```


## Fluxo de gerenciamento

Tendo acesso de administrador, o usuário tem acesso aos endpoints de gerencimento de administadores, categorias, produtos e atualização de pedidos.

### Usuários

#### Retornar todos os Usuários

Endpoint que retorna todos os usuários.

```
  GET /User/GetUsers/
```

#### Retornar Usuário específico.

Endpoint que retorna o usuário.

```
  GET /User/GetUserById/{id}
```

#### Adicionar Usuário administrador.

Endpoint que adiciona usuários com permissão de administrador.

```
  POST /User/AddAdminUser/
```
| Parâmetro   | Tipo       | Descrição                           | 
| :---------- | :--------- | :---------------------------------- |
| `name` | `string` | **Obrigatório**. Nome do usuário. |
| `taxId` | `string` | **Obrigatório**. CPF ou CNPJ do usuário. |
| `email` | `string` | **Obrigatório**. Email do usuário. |
| `password` | `string` | **Obrigatório**. Senha do usuário. |

*Obs*: Todos parâmetros no Body da requisição. 


### Categorias

#### Adicionar uma categoria.

Endpoint para criar uma categoria.

```
  POST /Category/AddCategory/
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `name` | `string` | **Obrigatório**. Nome da categoria. |

*Obs*: Todos parâmetros no Body da requisição. 

#### Atualizar uma categoria.

Endpoint para atualizar uma categoria.

```
  PUT /Category/UpdateCategory/{id}
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `id` | `int` | **Obrigatório**. Identificador da categoria. |
| `name` | `string` | **Obrigatório**. Nome da categoria. |

*Obs*: Apenas id na rota demais parâmetros no Body da requisição. 

#### Remover uma categoria.

Endpoint para remover uma categoria.

```
  DELETE /Category/RemoveCategory/{id}
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `id` | `int` | **Obrigatório**. Identificador da categoria. |

*Obs*: Parâmetro na rota da requisição. 

### Produtos

#### Adicionar um produto

Endpoint para criar um produto.

```
  POST /Product/AddProduct/
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `name` | `string` | **Obrigatório**. Nome do produto. |
| `description` | `string` | **Obrigatório**. Descrição do produto. |
| `price` | `double` | **Obrigatório**. Preço do produto. |
| `stockQuantity` | `int` | **Obrigatório**. Quantidade no estoque do produto. |
| `isActive` | `boolean` | **Obrigatório**. Disponibilidade do produto. |
| `categoryId` | `int` | **Obrigatório**. Identificador da categoria. |

*Obs*: Todos parâmetros no Body da requisição. 

#### Atualizar um produto

Endpoint para atualizar um produto.

```
  PUT /Product/UpdateProduct/{id}
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `id` | `int` | **Obrigatório**. Identificador do produto. |
| `name` | `string` | **Obrigatório**. Nome do produto. |
| `description` | `string` | **Obrigatório**. Descrição do produto. |
| `price` | `double` | **Obrigatório**. Preço do produto. |
| `stockQuantity` | `int` | **Obrigatório**. Quantidade no estoque do produto. |
| `isActive` | `boolean` | **Obrigatório**. Disponibilidade do produto. |
| `categoryId` | `int` | **Obrigatório**. Identificador da categoria. |

*Obs*: Apenas id na rota demais parâmetros no Body da requisição. 

#### Remover um produto

Endpoint para remover um produto.

```
  DELETE /Product/RemoveProduct/{id}
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `id` | `int` | **Obrigatório**. Identificador do produto. |

*Obs*: Parâmetro na rota da requisição. 

### Pedidos

#### Retornar todos os pedidos.

Endpoint que retorna todos pedidos efetuados.

```
  GET /Order/GetOrders/
```

#### Atualizar status do pedido. 

Endpoint que atualiza um pedido para o proximo status, sendo:


```
Received: Recebido.
InPreparation: Em preparação.
Ready: Pronto para entrega.
Finished: Pedido entregue e finalizado.
```

Importante ressaltar que o pedido é criado com o status recebido. E seguirá o fluxo sem a possibilidade de retrocesso.

```
  PUT /Order/UpdateOrderStatus/{id}
```
| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `id` | `int` | **Obrigatório**. Identificador do pedido. |

*Obs*: Parâmetro na rota da requisição. 

## Ordem de Execução dos endpoints

Para utilizar os endpoints da API, é necessário realizar login e obter o token de autenticação via header Authorization.

### Administrador

#### Criar Categoria

```
  POST /Category/AddCategory
```
| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `name` | `string` | **Obrigatório**. Nome da categoria. |

#### Criar Produto

```
  POST /Product/AddProduct/
```
| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `name` | `string` | **Obrigatório**. Nome do produto. |
| `description` | `string` | **Obrigatório**. Descrição do produto. |
| `price` | `double` | **Obrigatório**. Preço do produto. |
| `stockQuantity` | `int` | **Obrigatório**. Quantidade no estoque do produto. |
| `isActive` | `boolean` | **Obrigatório**. Disponibilidade do produto. |
| `categoryId` | `int` | **Obrigatório**. Identificador da categoria. |

#### Atualizar Status do Pedido (Ordem)

Utilizado pelo administrador para atualizar o status das ordens de clientes.

```
  PUT /Order/UpdateOrderStatus
```
| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `id` | `int` | **Obrigatório**. ID da ordem. |

### Customer e Guest

#### Listar Categorias e Produtos

```
  POST /Category/GetCategories
```

```
  POST /Product/GetProducts
```

#### Carrinho de Compras

Visualizar Carrinho do Usuário

```
  Get /Cart/GetUserCart
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `userId` | `guid` | **Obrigatório**. Identificador do usuário. |

Adicionar Item ao Carrinho

```
  POST /Cart/AddCartItem/
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `cartId` | `int` | **Obrigatório**. Identificador do carrinho. |
| `userId` | `guid` | **Obrigatório**. Identificador do usuário. |
| `productId` | `int` | **Obrigatório**. Identificador do produto. |
| `quantity` | `int` | **Obrigatório**. Quantidade desejada do produto. |

Gerar Pedido a partir do Carrinho

```
  POST /Cart/GenerateOrderFromCart/{id}
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `cartId` | `int` | **Obrigatório**. Identificador do carrinho. |

#### Pagamento

Criar Pagamento

```
  POST /Payment/CreatePayment/
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `orderId` | `int` | **Obrigatório**. Identificador da ordem. |
| `idEmpotencyKey` | `guid` | **Obrigatório**. Identificador para não gerar o mesmo pagamento. |
| `quantity` | `int` | **Obrigatório**. Quantidade de itens adicionado no carrinho. |
| `description` | `string` | **Obrigatório**. Descrição do carrinho. |
| `payerEmail` | `string` | **Obrigatório**. Email do cliente. |
| `price` | `double` | **Obrigatório**. Preço do carrinho. |

Verificar Status do Pagamento

```
  GET /Payment/GetStatusPaymentByOrderId/
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `orderId` | `int` | **Obrigatório**. Identificador da ordem. |

#### Acompanhar Status da Ordem

Em Preparação

```
  GET /Order/GetInPreparationOrders/
```

Pronto para Entrega

```
  GET /Order/GetReadyOrders/
```

## Inicialização do Banco de dados
Scripts para inicializar os dados no banco de dados para testes. Conecte no container e utilizando o client desejádo.

```

-- Inserir ou atualizar usuários
INSERT INTO tb_user (Id, Email, Name, Password, UserRole, TaxId) VALUES
('11111111-1111-1111-1111-111111111111', 'customer@email.com', 'customer', 'customer', 1, '88888888888')
ON DUPLICATE KEY UPDATE
    Email=VALUES(Email),
    Name=VALUES(Name),
    Password=VALUES(Password),
    UserRole=VALUES(UserRole),
    TaxId=VALUES(TaxId);

INSERT INTO tb_user (Id, Email, Name, Password, UserRole, TaxId) VALUES
('d52947e6-2001-4a63-9377-b645568f59b5', 'admin@email.com', 'admin', 'admin', 0, '99999999999')
ON DUPLICATE KEY UPDATE
    Email=VALUES(Email),
    Name=VALUES(Name),
    Password=VALUES(Password),
    UserRole=VALUES(UserRole),
    TaxId=VALUES(TaxId);

-- Inserir ou atualizar categorias
INSERT INTO tb_category (Id, Name) VALUES
(1, 'Lanche')
ON DUPLICATE KEY UPDATE
    Name=VALUES(Name);

INSERT INTO tb_category (Id, Name) VALUES
(2, 'Acompanhamento')
ON DUPLICATE KEY UPDATE
    Name=VALUES(Name);

INSERT INTO tb_category (Id, Name) VALUES
(3, 'Bebida')
ON DUPLICATE KEY UPDATE
    Name=VALUES(Name);

INSERT INTO tb_category (Id, Name) VALUES
(4, 'Sobremesa')
ON DUPLICATE KEY UPDATE
    Name=VALUES(Name);

-- Inserir ou atualizar produtos
INSERT INTO tb_product (Id, CategoryId, Description, Name, Price, StockQuantity) VALUES
(1, 1, 'Pão, carne, queijo, alface e tomate', 'X-Burguer', 15, 100)
ON DUPLICATE KEY UPDATE
    CategoryId=VALUES(CategoryId),
    Description=VALUES(Description),
    Name=VALUES(Name),
    Price=VALUES(Price),
    StockQuantity=VALUES(StockQuantity);

INSERT INTO tb_product (Id, CategoryId, Description, Name, Price, StockQuantity) VALUES
(2, 2, 'Batata frita crocante', 'Batata Frita', 10, 50)
ON DUPLICATE KEY UPDATE
    CategoryId=VALUES(CategoryId),
    Description=VALUES(Description),
    Name=VALUES(Name),
    Price=VALUES(Price),
    StockQuantity=VALUES(StockQuantity);

-- Inserir ou atualizar carrinho
INSERT INTO tb_cart (Id, SubTotal, UserId) VALUES
(1, 25, '11111111-1111-1111-1111-111111111111')
ON DUPLICATE KEY UPDATE
    SubTotal=VALUES(SubTotal),
    UserId=VALUES(UserId);

-- Inserir ou atualizar itens do carrinho
INSERT INTO tb_cart_item (Id, CartId, ProductId, Quantity) VALUES
(1, 1, 1, 1),
(2, 1, 2, 1)
ON DUPLICATE KEY UPDATE
    CartId=VALUES(CartId),
    ProductId=VALUES(ProductId),
    Quantity=VALUES(Quantity);

-- Inserir ou atualizar pedido
INSERT INTO tb_order(Id, UserId, CartId, PaymentId, Total, CreatedDate, CompletionDate)
VALUES(1, '11111111-1111-1111-1111-111111111111', 1, 1, 0, '2025-06-01 22:00:00', '2025-06-01 22:00:00')
ON DUPLICATE KEY UPDATE
    UserId=VALUES(UserId),
    CartId=VALUES(CartId),
    PaymentId=VALUES(PaymentId),
    Total=VALUES(Total),
    CreatedDate=VALUES(CreatedDate),
    CompletionDate=VALUES(CompletionDate);

-- Inserir ou atualizar status do pedido
INSERT INTO tb_order_status (Id, OrderStatus)
VALUES(1, 'Recebido')
ON DUPLICATE KEY UPDATE
    OrderStatus=VALUES(OrderStatus);


-- Inserir ou atualizar pagamento
INSERT INTO tb_payment(Id, `Method`, PaymentDate, OrderId, PaymentStatusId, PaymentIdMP, Price)
VALUES(1, 'PIX', '2025-06-02 21:00:00', 1, 1, 1, 15.0)
ON DUPLICATE KEY UPDATE
    Method=VALUES(Method),
    PaymentDate=VALUES(PaymentDate),
    OrderId=VALUES(OrderId),
    PaymentStatusId=VALUES(PaymentStatusId),
	PaymentIdMP=VALUES(PaymentIdMP),
	Price=VALUES(Price);

-- Inserir ou atualizar status do pagamento
INSERT INTO tb_payment_status (Id, PaymentStatus)
VALUES(1, 'Pendente')
ON DUPLICATE KEY UPDATE
    PaymentStatus=VALUES(PaymentStatus);

```

## FunctionAPP

```
using System.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Data.SqlClient;

namespace AuthFunction
{
    public class AuthByCpf
    {
        [Function("AuthByCpf")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "auth/{cpf}")] HttpRequestData req, string cpf)
        {
            try
            {
                //string connectionString = "Server=MARCEL\\SQLEXPRESS;Database=CRUD;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False;";
                string connectionString = "Server=tcp:fastfooddb.database.windows.net,1433;Initial Catalog=FastFoodDb;Persist Security Info=False;User ID=adminDB;Password=Techfiap2025@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                string token = string.Empty;
                bool clienteValido = false;
                var response = req.CreateResponse(HttpStatusCode.OK);

                using (SqlConnection conn = new(connectionString))
                {
                    await conn.OpenAsync();
                    string sql = "SELECT COUNT(1) FROM TB_USER WHERE TAXID = @TaxId";
                    using (SqlCommand cmd = new(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TaxId", cpf);
                        int count = (int)await cmd.ExecuteScalarAsync();
                        clienteValido = count > 0;
                    }
                }
                if (clienteValido)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes("X9v!3kL2m#Q8zT6rP0sW@bY5dH1jF7aU");
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[] { new Claim("cpf", cpf) }),
                        Expires = DateTime.UtcNow.AddHours(1),
                        SigningCredentials = new SigningCredentials(
                            new SymmetricSecurityKey(key),
                            SecurityAlgorithms.HmacSha256Signature)
                    };
                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    token = tokenHandler.WriteToken(securityToken);
                }

                var result = new
                {
                    Cpf = cpf,
                    Valid = clienteValido,
                    Token = token
                };

                await response.WriteAsJsonAsync(result);
                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
                await errorResponse.WriteStringAsync($"Erro: {ex.Message}");
                return errorResponse;
            }
        }
    }
}

