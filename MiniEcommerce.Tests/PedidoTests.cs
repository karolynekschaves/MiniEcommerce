using MiniEcommerce.Domain;

namespace MiniEcommerce.Tests;

public class PedidoTests
{
    [Fact]
    public void Finalizar_DeveBaixarEstoqueECalcularTotal()
    {
        var mouse = new Produto(1, "Mouse", 50m, 10);
        var teclado = new Produto(2, "Teclado", 120m, 5);
        var carrinho = new CarrinhoDeCompras();
        carrinho.Adicionar(mouse, 2);
        carrinho.Adicionar(teclado, 1);

        var pedido = Pedido.Finalizar(carrinho);

        Assert.Equal(220m, pedido.Total);
        Assert.Equal(8, mouse.Estoque);
        Assert.Equal(4, teclado.Estoque);
        Assert.Equal(2, pedido.Itens.Count);
    }

    [Fact]
    public void Finalizar_CarrinhoVazio_DeveLancarExcecao()
        => Assert.Throws<InvalidOperationException>(() => Pedido.Finalizar(new CarrinhoDeCompras()));

    [Fact]
    public void Finalizar_DeveEsvaziarOCarrinho()
    {
        var carrinho = new CarrinhoDeCompras();
        carrinho.Adicionar(new Produto(1, "Mouse", 50m, 10), 1);
        Pedido.Finalizar(carrinho);
        Assert.Empty(carrinho.Itens);
    }

    [Fact]
    public void Finalizar_ComEstoqueQueAcabouNoMeioDoCaminho_NaoDeveBaixarNada()
    {
        var a = new Produto(1, "A", 10m, 5);
        var b = new Produto(2, "B", 10m, 5);
        var carrinho = new CarrinhoDeCompras();
        carrinho.Adicionar(a, 3);
        carrinho.Adicionar(b, 3);
        b.BaixarEstoque(4); // outro pedido consumiu o estoque

        Assert.Throws<InvalidOperationException>(() => Pedido.Finalizar(carrinho));
        Assert.Equal(5, a.Estoque); // nada foi baixado
    }
}