
using Ludoteca;

BibliotecaDeJogos bibliotecaDeJogos = new BibliotecaDeJogos();
bibliotecaDeJogos.Carregar();
ListaDeMembros listaDeMembros = new ListaDeMembros();
listaDeMembros.CarregarDeJson();
Emprestimo emprestimo = new Emprestimo(bibliotecaDeJogos, listaDeMembros);




while (true)
{
    Console.Clear();

    Console.WriteLine("==LUDOTECA==");

    Console.WriteLine("[1] Cadastrar jogo");
    Console.WriteLine("[2] Cadastrar membro");
    Console.WriteLine("[3] Listar jogos");
    Console.WriteLine("[4] Emprestar jogo");
    Console.WriteLine("[5] Devolver jogo");
    Console.WriteLine("[6] Gerar relatório");
    Console.WriteLine("[0] Sair");

    Console.WriteLine("\nSelecione sua opção: ");
    string opcao = Console.ReadLine();
    int intOpcao;

    while (!int.TryParse(opcao, out intOpcao) || intOpcao < 0 || intOpcao > 6)
    {
        Console.WriteLine("Opção inválida. Digite novamente");
        opcao = Console.ReadLine();
    }


    if (intOpcao != 0)
    {
        Console.Clear();
    }


    if (intOpcao == 1)
    {
        Console.Clear();
        bibliotecaDeJogos.AdicionarJogo();

        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
    }
    else if (intOpcao == 2)
    {
        Console.Clear();
        listaDeMembros.CadastrarMembro();

        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
    }
    else if (intOpcao == 3)
    {
        Console.Clear();
        bibliotecaDeJogos.ListarJogos();

        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
    }
    else if (intOpcao == 4)
    {
        Console.Clear();
        emprestimo.Emprestar();

        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
        
    }
    else if (intOpcao == 5)
    {
        Console.Clear();
        emprestimo.Devolver();

        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
    }
    else if (intOpcao == 6)
    {
        Console.Clear();

        bibliotecaDeJogos.ListarJogos();

        int opcaoRelatorio = Utilitarios.EscolherOpcao(bibliotecaDeJogos.ListaDeJogos.Count, "Deseja gerar relatório de qual jogo?");

        Console.Clear();

        Jogo jogo = bibliotecaDeJogos.ListaDeJogos[opcaoRelatorio];

        if (jogo.UltimoMembroPegou != null)
        {
            emprestimo.GerarRelatorio(jogo, jogo.UltimoMembroPegou);
        }
        else
        {
            Console.WriteLine("Esse jogo ainda não foi emprestado");
        }

        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();

    }
    else if (intOpcao == 0)
    {
        bibliotecaDeJogos.Salvar();
        listaDeMembros.SalvarEmJson();
        break;

    }

}