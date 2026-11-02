using MiniEcommerce.Domain;

namespace MiniEcommerce.Tests;

public class CatalogoTests
{
    [Fact]
    public void Cadastrar_DeveAdicionarProdutoComIdIncremental()
    {
        var c = new Catalogo();
        var a = c.Cadastrar("Mouse", 50m, 10);
        var b = c.Cadastrar("Teclado", 120m, 5);
        Assert.Equal(1, a.Id);
        Assert.Equal(2, b.Id);
        Assert.Equal(2, c.Listar().Count);
    }

    [Fact]
    public void Cadastrar_ComPrecoNegativo_DeveLancarExcecao()
        => Assert.Throws<ArgumentException>(() => new Catalogo().Cadastrar("X", -5m, 1));

    [Fact]
    public void BuscarPorId_Inexistente_DeveRetornarNull()
        => Assert.Null(new Catalogo().BuscarPorId(99));
}