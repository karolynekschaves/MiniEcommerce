# MiniEcommerce

Aplicação de console em C# que simula uma pequena loja virtual. Permite cadastrar produtos, consultar o catálogo, montar um carrinho de compras e finalizar o pedido com baixa automática no estoque.

O projeto foi desenvolvido seguindo **TDD básico** (testes escritos antes da implementação) e o padrão **Conventional Commits**.

## Funcionalidades

- **Cadastro de produtos**: nome, preço e quantidade em estoque, com validação (não aceita preço negativo, estoque menor que zero ou nome vazio).
- **Carrinho de compras**: adiciona produtos informando a quantidade, valida o estoque disponível (inclusive quando o mesmo produto é adicionado mais de uma vez) e permite remover itens.
- **Checkout**: processa a compra, dá baixa automática no estoque e gera o resumo com o valor total do pedido.
- **Catálogo**: lista os produtos com preço e estoque atual.

## Tecnologias

- C# / .NET
- xUnit (testes unitários)
- Git e GitHub

## Estrutura do projeto

```
MiniEcommerce/
├── MiniEcommerce.slnx
├── MiniEcommerce.Console/
│   ├── Produto.cs
│   ├── Catalogo.cs
│   ├── ItemCarrinho.cs
│   ├── CarrinhoDeCompras.cs
│   ├── Pedido.cs
│   └── Program.cs            # menu interativo
└── MiniEcommerce.Tests/
    ├── ProdutoTests.cs
    ├── CatalogoTests.cs
    ├── CarrinhoTests.cs
    └── PedidoTests.cs
```

As classes de domínio ficam no namespace `MiniEcommerce.Domain`.

## Como executar

**Pré-requisito:** [.NET SDK](https://dotnet.microsoft.com/download) instalado.

```bash
git clone https://github.com/karolynekschaves/MiniEcommerce.git
cd MiniEcommerce
dotnet run --project MiniEcommerce.Console/MiniEcommerce.Console.csproj
```

## Como rodar os testes

```bash
dotnet test
```

## Menu da aplicação

```
=== MINI E-COMMERCE ===
1 - Cadastrar produto
2 - Listar catálogo
3 - Adicionar ao carrinho
4 - Remover do carrinho
5 - Ver carrinho
6 - Finalizar pedido
0 - Sair
```

### Exemplo de uso

1. Escolha `1` e cadastre um produto (ex.: Mouse, preço `50,00`, estoque `10`).
2. Escolha `2` para conferir o catálogo.
3. Escolha `3`, informe o ID do produto e a quantidade desejada.
4. Escolha `5` para ver o carrinho e o subtotal.
5. Escolha `6` para finalizar o pedido. O estoque é atualizado e o resumo é exibido.

## Regras de negócio

| Regra | Comportamento |
|---|---|
| Preço negativo | Cadastro recusado |
| Estoque menor que zero | Cadastro recusado |
| Nome vazio | Cadastro recusado |
| Quantidade maior que o estoque | Item não é adicionado ao carrinho |
| Quantidade zero ou negativa | Operação recusada |
| Carrinho vazio no checkout | Pedido não é finalizado |
| Estoque insuficiente no checkout | Nada é baixado do estoque |
| Checkout concluído | Estoque decrementado e carrinho esvaziado |

## Padrão de commits

O histórico segue o [Conventional Commits](https://www.conventionalcommits.org/pt-br/):

- `feat`: nova funcionalidade
- `fix`: correção de bug
- `test`: criação ou ajuste de testes
- `refactor`: refatoração sem mudar comportamento
- `docs`: documentação
- `chore`: tarefas de manutenção

Exemplo: `feat(carrinho): implement shopping cart logic and stock verification`

## Autora

Karolyne Chaves — [@karolynekschaves](https://github.com/karolynekschaves)
