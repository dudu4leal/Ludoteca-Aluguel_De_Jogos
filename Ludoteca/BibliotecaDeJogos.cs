using Ludoteca;

using System.Text.Json;

public class BibliotecaDeJogos
{

    public List<Jogo> ListaDeJogos { get; set; } = new List<Jogo>();

    public const string CaminhoArquivo = "listaDeJogos.json";


    public void Salvar()
    {
        string json = JsonSerializer.Serialize(ListaDeJogos, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(CaminhoArquivo, json);

        Console.WriteLine("Jogos salvos com sucesso!");
    }

    public void Carregar()
    {

        if (File.Exists(CaminhoArquivo))
        {

            try
            {
                string json = File.ReadAllText(CaminhoArquivo);
                ListaDeJogos = JsonSerializer.Deserialize<List<Jogo>>(json) ?? new List<Jogo>();

                Console.WriteLine("Lista de jogos carregada com sucesso!");

            }
            catch (Exception erro)
            {
                Console.WriteLine($"Algum erro aconteceu | {erro.Message}");
                ListaDeJogos = new List<Jogo>();
            }
        }
        else
        {
            Console.WriteLine("Não há uma lista de jogos. Criando nova lista.");
            ListaDeJogos = new List<Jogo>();
        }
    }

    public void AdicionarJogo()
    {

        Jogo jogo = new Jogo();

        Console.WriteLine("\nDigite o nome do novo jogo: ");
        jogo.Nome = Console.ReadLine();

        while (true)
        {
            Console.WriteLine("\nDigite o valor do seu aluguel: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal valor))
            {
                jogo.ValorDoAluguel = valor;
                break;
            }
            else
            {
                Console.WriteLine("Digite um valor válido.");
            }

        }

        ListaDeJogos.Add(jogo);
        Console.WriteLine($"{jogo.Nome} adicionado com sucesso!");
        Salvar();
    }

    public void RemoverJogo()
    {

        if (ListaDeJogos.Count == 0)
        {
            Console.WriteLine("Não há jogos cadastrados");
            return;
        }

        Console.WriteLine("Qual jogo deseja remover?");
        ListarJogos();

        while (true)
        {
            try
            {
                string opcao = Console.ReadLine();
                int opcaoInt;

                if (int.TryParse(opcao, out opcaoInt))
                {
                    if (opcaoInt <= ListaDeJogos.Count && opcaoInt > 0)
                    {
                        ListaDeJogos.RemoveAt(opcaoInt - 1);
                        Console.WriteLine("Jogo removido com sucesso!");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Digite uma opção válida");
                    }

                }
                else
                {
                    Console.WriteLine("Digite um número inteiro válido");
                }

            }
            catch (Exception erro)
            {
                Console.WriteLine($"Erro inesperado | {erro.Message}");
            }
        }
    }

    public void ListarJogos()
    {

        if (ListaDeJogos.Count == 0)
        {
            Console.WriteLine("Nenhum jogo cadastrado.");
            return;
        }
        else
        {

            Console.WriteLine("");
            Console.WriteLine("----------");

            for (int i = 0; i < (ListaDeJogos.Count); i++)
            {
                Console.WriteLine($"[{i + 1}] {ListaDeJogos[i].Nome}");
            }

            Console.WriteLine("----------");
            Console.WriteLine("");
        }

    }
}
