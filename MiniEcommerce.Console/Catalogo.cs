namespace MiniEcommerce.Domain;

public class Catalogo
{
    private readonly List<Produto> _produtos = new();
    private int _proximoId = 1;

    public Produto Cadastrar(string nome, decimal preco, int estoque)
    {
        var produto = new Produto(_proximoId, nome, preco, estoque);
        _produtos.Add(produto);
        _proximoId++;
        return produto;
    }

    public IReadOnlyList<Produto> Listar() => _produtos.AsReadOnly();

    public Produto? BuscarPorId(int id) => _produtos.FirstOrDefault(p => p.Id == id);
}