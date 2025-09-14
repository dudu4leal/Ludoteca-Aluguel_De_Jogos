using System;
using System.Text.Json;
using Ludoteca;


namespace Ludoteca
{
    public class ListaDeMembros
    {
        public List<Membro> membros;

        public const string CaminhoArquivo = "listaDeMembros.json";

        public ListaDeMembros()
        {
            membros = new List<Membro>();
        }

        public void CadastrarMembro()
        {
            Membro novo = new Membro();

            Console.WriteLine("Digite o nome do membro: ");
            novo.NomePessoa = Console.ReadLine();

            Console.WriteLine($"Digite o CPF do {novo.NomePessoa}: ");
            novo.Cpf = Console.ReadLine();

            membros.Add(novo);
        }

        public bool RemoverMembro(string nome)
        {
            var membro = BuscarMembro(nome);
            if (membro != null)
            {
                membros.Remove(membro);
                SalvarEmJson(CaminhoArquivo);
                return true;
            }
            return false;
        }

        public Membro BuscarMembro(string nome)
        {
            if (membros == null)
            {
                return null;
            }
            else
            {
                return membros.Find(m => m.NomePessoa == nome);
            }
        }

        public void SalvarEmJson(string caminho)
        {
            string json = JsonSerializer.Serialize(membros, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(CaminhoArquivo, json);

            Console.WriteLine("Membros salvos com sucesso!");
        }

        public void CarregarDeJson(string caminho)
        {
            if (File.Exists(CaminhoArquivo))
            {
                try
                {
                    string json = File.ReadAllText(CaminhoArquivo);
                    membros = JsonSerializer.Deserialize<List<Membro>>(json) ?? new List<Membro>();



                }
                catch (Exception erro)
                {
                    Console.WriteLine($"Algum erro aconteceu | {erro.Message}");
                    membros = new List<Membro>();
                }
            }
            else
            {
                Console.WriteLine("Não existe uma lista de membros. Criando nova lista.");
                membros = new List<Membro>();

            }
        }
    }
}
