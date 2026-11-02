namespace MiniEcommerce.Domain;

public class Produto
{
    public int Id { get; }
    public string Nome { get; }
    public decimal Preco { get; }
    public int Estoque { get; private set; }

    public Produto(int id, string nome, decimal preco, int estoque)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome é obrigatório.", nameof(nome));
        if (preco < 0)
            throw new ArgumentException("O preço não pode ser negativo.", nameof(preco));
        if (estoque < 0)
            throw new ArgumentException("O estoque não pode ser menor que zero.", nameof(estoque));

        Id = id;
        Nome = nome.Trim();
        Preco = preco;
        Estoque = estoque;
    }

    public void BaixarEstoque(int quantidade)
    {
        if (quantidade <= 0)
            throw new ArgumentException("A quantidade deve ser maior que zero.", nameof(quantidade));
        if (quantidade > Estoque)
            throw new InvalidOperationException($"Estoque insuficiente para {Nome}.");
        Estoque -= quantidade;
    }
}