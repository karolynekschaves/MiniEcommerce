namespace MiniEcommerce.Domain;

public class Pedido
{
    public IReadOnlyList<ItemPedido> Itens { get; }
    public decimal Total { get; }
    public DateTime CriadoEm { get; }

    private Pedido(IReadOnlyList<ItemPedido> itens)
    {
        Itens = itens;
        Total = itens.Sum(i => i.Subtotal);
        CriadoEm = DateTime.Now;
    }

    public static Pedido Finalizar(CarrinhoDeCompras carrinho)
    {
        if (carrinho.Itens.Count == 0)
            throw new InvalidOperationException("O carrinho está vazio.");

        // Valida tudo antes de baixar qualquer estoque
        foreach (var item in carrinho.Itens)
            if (item.Quantidade > item.Produto.Estoque)
                throw new InvalidOperationException($"Estoque insuficiente para {item.Produto.Nome}.");

        var itens = carrinho.Itens
            .Select(i => new ItemPedido(i.Produto.Nome, i.Produto.Preco, i.Quantidade))
            .ToList();

        foreach (var item in carrinho.Itens)
            item.Produto.BaixarEstoque(item.Quantidade);

        carrinho.Limpar();
        return new Pedido(itens);
    }
}

public record ItemPedido(string Nome, decimal PrecoUnitario, int Quantidade)
{
    public decimal Subtotal => PrecoUnitario * Quantidade;
}