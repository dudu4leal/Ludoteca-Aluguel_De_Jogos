using Ludoteca;
public class Jogo
{
    public string? Nome { get; set; }

    public bool Emprestado { get; set; } = false;

    public decimal ValorDoAluguel { get; set; }

    public Membro? MembroQuePegou { get; set; }
}
