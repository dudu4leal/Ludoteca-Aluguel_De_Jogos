namespace Ludoteca;

public static class Utilitarios
{
    public static int EscolherOpcao(int max, string mensagem)
    {
        Console.WriteLine(mensagem);
        string? opcao = Console.ReadLine();
        int opcaoInt;

        while (!int.TryParse(opcao, out opcaoInt) || opcaoInt < 1 || opcaoInt > max)
        {
            Console.WriteLine("Opção inválida. Digite novamente");
            opcao = Console.ReadLine();
        }

        return opcaoInt - 1;
    }
}
