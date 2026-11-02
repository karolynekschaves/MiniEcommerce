using System.Globalization;
using MiniEcommerce.Domain;

var catalogo = new Catalogo();
var carrinho = new CarrinhoDeCompras();
var cultura = new CultureInfo("pt-BR");

while (true)
{
    Console.WriteLine("\n=== MINI E-COMMERCE ===");
    Console.WriteLine("1 - Cadastrar produto");
    Console.WriteLine("2 - Listar catálogo");
    Console.WriteLine("3 - Adicionar ao carrinho");
    Console.WriteLine("4 - Remover do carrinho");
    Console.WriteLine("5 - Ver carrinho");
    Console.WriteLine("6 - Finalizar pedido");
    Console.WriteLine("0 - Sair");
    Console.Write("Opção: ");

    try
    {
        switch (Console.ReadLine())
        {
            case "1":
                Console.Write("Nome: ");
                var nome = Console.ReadLine() ?? "";
                Console.Write("Preço: ");
                var preco = decimal.Parse(Console.ReadLine() ?? "", cultura);
                Console.Write("Estoque: ");
                var estoque = int.Parse(Console.ReadLine() ?? "");
                var p = catalogo.Cadastrar(nome, preco, estoque);
                Console.WriteLine($"Produto #{p.Id} cadastrado.");
                break;

            case "2":
                if (catalogo.Listar().Count == 0) { Console.WriteLine("Catálogo vazio."); break; }
                foreach (var prod in catalogo.Listar())
                    Console.WriteLine($"#{prod.Id} {prod.Nome} | {prod.Preco.ToString("C", cultura)} | Estoque: {prod.Estoque}");
                break;

            case "3":
                Console.Write("ID do produto: ");
                var idAdd = int.Parse(Console.ReadLine() ?? "");
                var alvo = catalogo.BuscarPorId(idAdd) ?? throw new InvalidOperationException("Produto não encontrado.");
                Console.Write("Quantidade: ");
                carrinho.Adicionar(alvo, int.Parse(Console.ReadLine() ?? ""));
                Console.WriteLine("Adicionado ao carrinho.");
                break;

            case "4":
                Console.Write("ID do produto a remover: ");
                var idRem = int.Parse(Console.ReadLine() ?? "");
                Console.WriteLine(carrinho.Remover(idRem) ? "Removido." : "Item não está no carrinho.");
                break;

            case "5":
                if (carrinho.Itens.Count == 0) { Console.WriteLine("Carrinho vazio."); break; }
                foreach (var i in carrinho.Itens)
                    Console.WriteLine($"{i.Produto.Nome} x{i.Quantidade} = {i.Subtotal.ToString("C", cultura)}");
                Console.WriteLine($"Subtotal: {carrinho.CalcularSubtotal().ToString("C", cultura)}");
                break;

            case "6":
                var pedido = Pedido.Finalizar(carrinho);
                Console.WriteLine("\n--- RESUMO DO PEDIDO ---");
                foreach (var i in pedido.Itens)
                    Console.WriteLine($"{i.Nome} x{i.Quantidade} = {i.Subtotal.ToString("C", cultura)}");
                Console.WriteLine($"TOTAL: {pedido.Total.ToString("C", cultura)}");
                break;

            case "0":
                return;

            default:
                Console.WriteLine("Opção inválida.");
                break;
        }
    }
    catch (FormatException) { Console.WriteLine("Valor inválido. Digite um número válido."); }
    catch (ArgumentException ex) { Console.WriteLine($"Erro: {ex.Message}"); }
    catch (InvalidOperationException ex) { Console.WriteLine($"Erro: {ex.Message}"); }
}