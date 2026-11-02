namespace MiniEcommerce.Domain;

public class CarrinhoDeCompras
{
    private readonly List<ItemCarrinho> _itens = new();
    public IReadOnlyList<ItemCarrinho> Itens => _itens.AsReadOnly();

    public void Adicionar(Produto produto, int quantidade)
    {
        if (quantidade <= 0)
            throw new ArgumentException("A quantidade deve ser maior que zero.", nameof(quantidade));

        var existente = _itens.FirstOrDefault(i => i.Produto.Id == produto.Id);
        var totalDesejado = quantidade + (existente?.Quantidade ?? 0);

        if (totalDesejado > produto.Estoque)
            throw new InvalidOperationException(
                $"Estoque insuficiente para {produto.Nome}. Disponível: {produto.Estoque}.");

        if (existente is null)
            _itens.Add(new ItemCarrinho(produto, quantidade));
        else
            existente.Quantidade = totalDesejado;
    }

    public bool Remover(int produtoId)
    {
        var item = _itens.FirstOrDefault(i => i.Produto.Id == produtoId);
        return item is not null && _itens.Remove(item);
    }

    public decimal CalcularSubtotal() => _itens.Sum(i => i.Subtotal);

    public void Limpar() => _itens.Clear();
}