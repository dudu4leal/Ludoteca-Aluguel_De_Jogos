using System.Text.Json;
using Ludoteca;


namespace Ludoteca
{
    public class ListaDeMembros
    {
        public List<Membro> MembrosCadastrados { get; set; } = new List<Membro>();

        public const string CaminhoArquivo = "listaDeMembrosCadastrados.json";


        public void CadastrarMembro()
        {
            Membro novo = new Membro();

            Console.WriteLine("\nDigite o nome do(a) membro: ");
            novo.NomePessoa = Console.ReadLine();

            Console.WriteLine($"\nDigite o CPF do(a) {novo.NomePessoa}: ");
            novo.Cpf = Console.ReadLine();

            MembrosCadastrados.Add(novo);
            Console.WriteLine("Membro cadastrado com sucesso!");
            SalvarEmJson();
        }

        public void RemoverMembro()
        {
            if (MembrosCadastrados.Count == 0)
            {
                Console.WriteLine("Não há MembrosCadastrados cadastrados");
                return;
            }

            Console.WriteLine("Qual membro deseja remover?");
            ListarMembros();

            while (true)
            {
                try
                {

                    string opcao = Console.ReadLine();
                    int opcaoInt;

                    if (int.TryParse(opcao, out opcaoInt))
                    {
                        if (opcaoInt <= MembrosCadastrados.Count && opcaoInt > 0)
                        {
                            MembrosCadastrados.RemoveAt(opcaoInt - 1);
                            Console.WriteLine("Membro removido com sucesso!");
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

        public void SalvarEmJson()
        {
            string json = JsonSerializer.Serialize(MembrosCadastrados, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(CaminhoArquivo, json);

            Console.WriteLine("MembrosCadastrados salvos com sucesso!\n");
        }

        public void CarregarDeJson()
        {
            if (File.Exists(CaminhoArquivo))
            {
                try
                {
                    string json = File.ReadAllText(CaminhoArquivo);
                    MembrosCadastrados = JsonSerializer.Deserialize<List<Membro>>(json) ?? new List<Membro>();



                }
                catch (Exception erro)
                {
                    Console.WriteLine($"Algum erro aconteceu | {erro.Message}");
                    MembrosCadastrados = new List<Membro>();
                }
            }
            else
            {
                Console.WriteLine("Não existe uma lista de MembrosCadastrados. Criando nova lista.");
                MembrosCadastrados = new List<Membro>();

            }
        }

        public void ListarMembros()
        {
            if ((MembrosCadastrados.Count) == 0)
            {
                Console.WriteLine("Não há MembrosCadastrados");
            }
            else
            {
                for (int i = 0; i < MembrosCadastrados.Count; i++)
                {
                    Console.WriteLine($"[{i+1}] {MembrosCadastrados[i].NomePessoa}");
                }
            }
        }

    }
}
