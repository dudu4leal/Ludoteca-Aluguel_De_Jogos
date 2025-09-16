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
        }
        catch (Exception erro)
        {
            Console.WriteLine($"Erro inesperado | {erro.Message}");
        }
    }

    public decimal GerarMulta(Jogo jogo)
    {

        if (jogo.DataDevolucao == jogo.DataDevolucaoEsperada)
        {
            decimal valorMulta = 0;
            return valorMulta;
        }
        else
        {
            if (jogo.DataDevolucao == null)
            {
                Console.WriteLine($"{jogo.Nome} ainda não foi devolvido");
                return 0;
            }
            else
            {
                TimeSpan diferenca = jogo.DataDevolucao.Value - jogo.DataEmprestimo.Value;

                decimal dias = (decimal)diferenca.TotalDays;

                decimal valorMulta = dias * 5;

                return valorMulta;
            }
        }
    }

    public void GerarRelatorio(Jogo jogo, Membro membro)
    {
        decimal valorMulta = GerarMulta(jogo);

        string texto = 
            "-------------------------\n" +
            "Relatório do Empréstimo:\n" +
            $"Jogo emprestado: {jogo.Nome}\n" +
            $"Quem pegou: {membro.NomePessoa}\n" +
            $"Data do empréstimo: {jogo.DataEmprestimo?.ToString("dd/MM/yyyy HH:mm") ?? "Não registrado"}\n" +
            $"Data da devolução: {jogo.DataDevolucao?.ToString("dd/MM/yyyy HH:mm") ?? "Não devolvido"}\n" +
            $"Valor do aluguel: R${jogo.ValorDoAluguel.ToString("F2", CultureInfo.GetCultureInfo("pt-BR"))}\n" +
            $"Valor da multa: R${valorMulta.ToString("F2", CultureInfo.GetCultureInfo("pt-BR"))}\n" +
            $"Total a pagar: R${(jogo.ValorDoAluguel + valorMulta).ToString("F2", CultureInfo.GetCultureInfo("pt-BR"))}\n" +
            "-------------------------\n";

        File.AppendAllText("relatorio.txt", texto);

        Console.WriteLine("RELATÓRIO GERADO:");
        Console.WriteLine(texto);
    }

}

