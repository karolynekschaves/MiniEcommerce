using MiniEcommerce.Domain;

namespace MiniEcommerce.Tests;

public class ProdutoTests
{
    [Fact]
    public void Criar_ComDadosValidos_DeveCriarProduto()
    {
        var p = new Produto(1, "Mouse", 50m, 10);
        Assert.Equal("Mouse", p.Nome);
        Assert.Equal(50m, p.Preco);
        Assert.Equal(10, p.Estoque);
    }

    [Fact]
    public void Criar_ComPrecoNegativo_DeveLancarExcecao()
        => Assert.Throws<ArgumentException>(() => new Produto(1, "Mouse", -1m, 10));

    [Fact]
    public void Criar_ComEstoqueNegativo_DeveLancarExcecao()
        => Assert.Throws<ArgumentException>(() => new Produto(1, "Mouse", 10m, -1));

    [Fact]
    public void Criar_SemNome_DeveLancarExcecao()
        => Assert.Throws<ArgumentException>(() => new Produto(1, " ", 10m, 1));

    [Fact]
    public void BaixarEstoque_QuantidadeMaiorQueEstoque_DeveLancarExcecao()
    {
        var p = new Produto(1, "Mouse", 50m, 2);
        Assert.Throws<InvalidOperationException>(() => p.BaixarEstoque(3));
    }
}