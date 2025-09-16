using Ludoteca;
public class Jogo
{
    public string? Nome { get; set; }

    public bool Emprestado { get; private set; } = false;

    public decimal ValorDoAluguel { get; set; }

    public Membro? MembroQuePegou { get; set; }

    public DateTime? DataEmprestimo { get; set; }

    public DateTime? DataDevolucaoEsperada { get; set; }

    public DateTime? DataDevolucao { get; set; }


    public void EmprestarPara(Membro membro)
    {
        if (Emprestado)
        {
            Console.WriteLine($"O jogo {Nome} já está emprestado");
        }
        else
        {
            Emprestado = true;
            MembroQuePegou = membro;
            DataEmprestimo = DateTime.Now;
            DataDevolucaoEsperada = DataEmprestimo.Value.AddDays(7);
            DataDevolucao = null;
            Console.WriteLine($"Jogo {Nome} emprestado para {MembroQuePegou}");
        }
    }

    public void Devolver()
    {
        if (!Emprestado)
        {
            Console.WriteLine($"O jogo {Nome} não está emprestado");
        }
        else
        {
            Membro? membro = MembroQuePegou;

            DataDevolucao = DateTime.Now;
            Emprestado = false;
            MembroQuePegou = null;
            DataEmprestimo = null;
            DataDevolucaoEsperada = null;

            Console.WriteLine($"Jogo {Nome} devolvido por {membro?.NomePessoa}");
        }
    }
}
