namespace Ludoteca;

public class Jogo
{
    public string? Nome { get; set; }

    public bool Emprestado { get; private set; } = false;

    public decimal ValorDoAluguel { get; set; }

}
