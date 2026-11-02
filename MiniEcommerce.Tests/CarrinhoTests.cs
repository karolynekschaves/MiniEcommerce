using MiniEcommerce.Domain;

namespace MiniEcommerce.Tests;

public class CarrinhoTests
{
    private static Produto NovoProduto(int estoque = 10) => new(1, "Mouse", 50m, estoque);

    [Fact]
    public void Adicionar_ComEstoque_DeveAdicionarItem()
    {
        var carrinho = new CarrinhoDeCompras();
        carrinho.Adicionar(NovoProduto(), 2);
        Assert.Single(carrinho.Itens);
        Assert.Equal(2, carrinho.Itens[0].Quantidade);
    }

    [Fact]
    public void Adicionar_QuantidadeMaiorQueEstoque_DeveLancarExcecao()
    {
        var carrinho = new CarrinhoDeCompras();
        Assert.Throws<InvalidOperationException>(() => carrinho.Adicionar(NovoProduto(3), 4));
    }

    [Fact]
    public void Adicionar_MesmoProdutoDuasVezes_DeveSomarELimitarPeloEstoque()
    {
        var carrinho = new CarrinhoDeCompras();
        var p = NovoProduto(5);
        carrinho.Adicionar(p, 3);
        Assert.Throws<InvalidOperationException>(() => carrinho.Adicionar(p, 3));
    }

    [Fact]
    public void Adicionar_QuantidadeZeroOuNegativa_DeveLancarExcecao()
        => Assert.Throws<ArgumentException>(() => new CarrinhoDeCompras().Adicionar(NovoProduto(), 0));

    [Fact]
    public void CalcularSubtotal_DeveSomarPrecoVezesQuantidade()
    {
        var carrinho = new CarrinhoDeCompras();
        carrinho.Adicionar(new Produto(1, "Mouse", 50m, 10), 2);
        carrinho.Adicionar(new Produto(2, "Teclado", 120m, 10), 1);
        Assert.Equal(220m, carrinho.CalcularSubtotal());
    }

    [Fact]
    public void Remover_ItemExistente_DeveRemoverDoCarrinho()
    {
        var carrinho = new CarrinhoDeCompras();
        carrinho.Adicionar(NovoProduto(), 1);
        Assert.True(carrinho.Remover(1));
        Assert.Empty(carrinho.Itens);
    }

    [Fact]
    public void Remover_ItemInexistente_DeveRetornarFalse()
        => Assert.False(new CarrinhoDeCompras().Remover(99));
}