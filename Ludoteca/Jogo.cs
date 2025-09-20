using Ludoteca;
public class Jogo
{
    public string? Nome { get; set; }

    public bool Emprestado { get; set; } = false;

    public decimal ValorDoAluguel { get; set; }

    public Membro? MembroQuePegou { get; set; }

    public Membro? UltimoMembroPegou { get; set; }

    public DateTime? DataEmprestimo { get; set; }

    public DateTime? UltimoEmprestimo { get; set; }

    public DateTime? DataDevolucaoEsperada { get; set; }

    public DateTime? DataDevolucao { get; set; }


    public void EmprestarPara(Membro membro)
    {
        if (Emprestado == true)
        {
            Console.WriteLine($"O jogo {Nome} já está emprestado\n");
        }
        else
        {
            Emprestado = true;
            MembroQuePegou = membro;
            DataEmprestimo = DateTime.Now;
            UltimoEmprestimo = DataEmprestimo;
            UltimoMembroPegou = membro;
            DataDevolucaoEsperada = DataEmprestimo.Value.AddDays(7);
            DataDevolucao = null;
            Console.WriteLine($"Jogo {Nome} emprestado para {MembroQuePegou.NomePessoa}\n");
        }
    }

    public void Devolver()
    {
        if (Emprestado == false)
        {
            Console.WriteLine($"O jogo {Nome} não está emprestado\n");
        }
        else
        {
            //Membro? membro = MembroQuePegou;

            UltimoMembroPegou = MembroQuePegou;
            UltimoEmprestimo = DataEmprestimo;
            DataDevolucao = DateTime.Now;
            Emprestado = false;
            MembroQuePegou = null;

            Console.WriteLine($"Jogo {Nome} devolvido por {UltimoMembroPegou?.NomePessoa}\n");
        }
    }
}
