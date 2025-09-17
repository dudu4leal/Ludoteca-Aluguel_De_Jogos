using Ludoteca;
using System;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Text.Json;


public class Emprestimo
{

    public Emprestimo(BibliotecaDeJogos biblioteca, ListaDeMembros lista)
    {
        bibliotecaDeJogos = biblioteca;
        listaDeMembros = lista;
    }

    private BibliotecaDeJogos bibliotecaDeJogos;
    private ListaDeMembros listaDeMembros;

    public void Emprestar()
    {
        bibliotecaDeJogos.ListarJogos();
        int jogoIndx = Utilitarios.EscolherOpcao(bibliotecaDeJogos.ListaDeJogos.Count, "Qual jogo será emprestado?");

        listaDeMembros.ListarMembros();
        int membroIndx = Utilitarios.EscolherOpcao(listaDeMembros.MembrosCadastrados.Count, "Quem está pegando o jogo?");


        Jogo jogo = bibliotecaDeJogos.ListaDeJogos[jogoIndx];
        Membro membro = listaDeMembros.MembrosCadastrados[membroIndx];

        try
        {
            jogo.EmprestarPara(membro);
            bibliotecaDeJogos.Salvar();
            listaDeMembros.SalvarEmJson();
        }
        catch (Exception erro)
        {
            Console.WriteLine($"Erro inesperado | {erro.Message}");
        }
    }

    public void Devolver()
    {

        bibliotecaDeJogos.ListarJogos();
        int jogoIndx = Utilitarios.EscolherOpcao(bibliotecaDeJogos.ListaDeJogos.Count, "Qual jogo está sendo devolvido?");

        Jogo jogo = bibliotecaDeJogos.ListaDeJogos[jogoIndx];

        try
        {

            jogo.Devolver();
            bibliotecaDeJogos.Salvar();
            listaDeMembros.SalvarEmJson();
        }
        catch (Exception erro)
        {
            Console.WriteLine($"Erro inesperado | {erro.Message}");
        }
    }

    public decimal GerarMulta(Jogo jogo)
    {
        if (jogo.DataEmprestimo == null || jogo.DataDevolucaoEsperada == null || jogo.DataDevolucao == null)
        {
            return 0;
        }

        TimeSpan diferenca = jogo.DataDevolucao.Value - jogo.DataDevolucaoEsperada.Value;

        decimal diasAtraso = (decimal)diferenca.TotalDays;

        if (diasAtraso <= 0)
        {
            return 0;
        }

        decimal valorMulta = diasAtraso * 5;
        return valorMulta;
    }

    public void GerarRelatorio(Jogo jogo, Membro membro)
    {
        decimal valorMulta = GerarMulta(jogo);

        string caminho = "Relatorio_" + jogo.Nome + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";


        string texto =
            "------------Relatório do Empréstimo-------------\n" +
            $"\n" +
            $"Jogo emprestado: {jogo.Nome}\n" +
            $"Quem pegou: {jogo.UltimoMembroPegou.NomePessoa}\n" +
            $"\n" +
            $"-----------------------------------------------\n" +
            $"\n" +
            $"Data do empréstimo: {jogo.UltimoEmprestimo?.ToString("dd/MM/yyyy HH:mm") ?? "Não registrado"}\n" +
            $"Data da devolução: {jogo.DataDevolucao?.ToString("dd/MM/yyyy HH:mm") ?? "Não devolvido"}\n" +
            $"\n" +
            $"-----------------------------------------------\n" +
            $"\n" +
            $"Valor do aluguel: R${jogo.ValorDoAluguel.ToString("F2", CultureInfo.GetCultureInfo("pt-BR"))}\n" +
            $"Valor da multa: R${valorMulta.ToString("F2", CultureInfo.GetCultureInfo("pt-BR"))}\n" +
            $"\n" +
            $"-----------------------------------------------\n" +
            $"\n" +
            $"Total a pagar: R${(jogo.ValorDoAluguel + valorMulta).ToString("F2", CultureInfo.GetCultureInfo("pt-BR"))}\n" +
            $"\n" +
            $"-----------------------------------------------\n";

        File.WriteAllText(caminho, texto);

        Console.WriteLine("RELATÓRIO GERADO:");
        Console.WriteLine(texto);
    }

}

