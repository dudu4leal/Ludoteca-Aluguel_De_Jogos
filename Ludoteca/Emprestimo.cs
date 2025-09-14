using Ludoteca;
using System;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Text.Json;

namespace Emprestimo
{
    public class Emprestimo
    {
        public DateTime DataEmprestimo { get; set; }
        public DateTime? DataDevolucao { get; set; }

        public void emprestar(string nomeJogo, Membro membroPessoa, BibliotecaDeJogos biblioteca, string CaminhoArquivo, ListaDeMembros lista)
        {
            var jogo = biblioteca.ListaDeJogos.FirstOrDefault(j => j.Nome == nomeJogo);
            if (jogo == null)
            {
                Console.WriteLine("O jogo não foi encontrado em nossa LUDOTECA.");
                return;
            }

            var integrante = lista.membros.FirstOrDefault(m => m.NomePessoa == membroPessoa.NomePessoa);
            if (integrante == null)
            {
                Console.WriteLine("Membro não cadastrado na lista de membros.");
                return;
            }

            if (!jogo.Emprestado)
            {
                jogo.Emprestado = true;
                DataEmprestimo = DateTime.Now;
                jogo.MembroQuePegou = integrante;

                string json = JsonSerializer.Serialize(biblioteca.ListaDeJogos, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(CaminhoArquivo, json);

                Console.WriteLine("Jogo emprestado com sucesso");
            }
            else
            {
                Console.WriteLine("Esse jogo já foi emprestado...");
            }
        }

        public void Devolver(string nomeJogo, BibliotecaDeJogos biblioteca, string CaminhoArquivo, Membro membro, ListaDeMembros lista)
        {
            var jogo = biblioteca.ListaDeJogos.FirstOrDefault(j => j.Nome == nomeJogo);
            if (jogo == null)
            {
                Console.WriteLine("Jogo não encontrado");
                return;
            }

            var integrante = lista.membros.FirstOrDefault(m => m.NomePessoa == membro.NomePessoa);
            if (integrante == null)
            {
                Console.WriteLine("Membro não encontrado no cadastro");
                return;
            }

            if (jogo.MembroQuePegou == null || jogo.MembroQuePegou.NomePessoa != integrante.NomePessoa)
            {
                Console.WriteLine("Esse jogo não foi emprestado para esse membro");
                return;
            }

            if (jogo.Emprestado)
            {
                jogo.Emprestado = false;
                DataDevolucao = DateTime.Now;
                jogo.MembroQuePegou = null;

                string json = JsonSerializer.Serialize(biblioteca.ListaDeJogos, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(CaminhoArquivo, json);

                decimal valor = GerarMulta(jogo.ValorDoAluguel);
                GerarRelatorio(nomeJogo, integrante.NomePessoa, valor);

                Console.WriteLine("Obrigado por devolver o jogo");
            }
            else
            {
                Console.WriteLine("Esse jogo já foi devolvido");
            }
        }

        public decimal GerarMulta(decimal ValorDoAluguel)
        {
            if (!DataDevolucao.HasValue)
                throw new InvalidOperationException("Esse jogo ainda não foi devolvido");

            var DiasDeAluguel = (DataDevolucao.Value - DataEmprestimo).TotalDays;
            if (DiasDeAluguel <= 10)
                return ValorDoAluguel;

            decimal DiasDeAtraso = (decimal)DiasDeAluguel - 10;
            decimal Valormulta = (DiasDeAtraso * 1.50m) + ValorDoAluguel;
            return Valormulta;
        }

        public void GerarRelatorio(string nomeJogo, string MembroQuePegou, decimal Valormulta)
        {
            string linha = $"O jogo {nomeJogo} foi devolvido por {MembroQuePegou} pagando o valor de R$ {Valormulta.ToString("F2", CultureInfo.GetCultureInfo("pt-BR"))} no dia {DataDevolucao?.ToString("dd/MM/yyyy HH:mm")}\n";
            File.AppendAllText("relatorio.txt", linha);

            Console.WriteLine("RELATÓRIO GERADO:");
            Console.WriteLine(linha);
        }
    }
}
