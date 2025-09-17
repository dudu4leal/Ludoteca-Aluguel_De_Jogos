
using Ludoteca;

BibliotecaDeJogos bibliotecaDeJogos = new BibliotecaDeJogos();
bibliotecaDeJogos.Carregar();
ListaDeMembros listaDeMembros = new ListaDeMembros();
listaDeMembros.CarregarDeJson();
Emprestimo emprestimo = new Emprestimo(bibliotecaDeJogos, listaDeMembros);



Console.WriteLine("==LUDOTECA==");

while (true)
{
    Console.WriteLine("[1] Cadastrar jogo");
    Console.WriteLine("[2] Cadastrar membro");
    Console.WriteLine("[3] Listar jogos");
    Console.WriteLine("[4] Emprestar jogo");
    Console.WriteLine("[5] Devolver jogo");
    Console.WriteLine("[6] Gerar relatório");
    Console.WriteLine("[0] Sair");

    Console.WriteLine("Selecione sua opção: ");
    string opcao = Console.ReadLine();
    int intOpcao;

    while (!int.TryParse(opcao, out intOpcao) || intOpcao < 0 || intOpcao > 6)
    {
        Console.WriteLine("Opção inválida. Digite novamente");
        opcao = Console.ReadLine();
    }


    if (intOpcao == 1)
    {
        bibliotecaDeJogos.AdicionarJogo();
    }
    else if (intOpcao == 2)
    {
        listaDeMembros.CadastrarMembro();
    }
    else if (intOpcao == 3)
    {
        bibliotecaDeJogos.ListarJogos();
    }
    else if (intOpcao == 4)
    {
        emprestimo.Emprestar();
    }
    else if (intOpcao == 5)
    {
        emprestimo.Devolver();

    }
    else if (intOpcao == 6)
    {

        bibliotecaDeJogos.ListarJogos();

        int opcaoRelatorio = Utilitarios.EscolherOpcao(bibliotecaDeJogos.ListaDeJogos.Count, "Deseja gerar relatório de qual jogo?");
        Jogo jogo = bibliotecaDeJogos.ListaDeJogos[opcaoRelatorio];

        if (jogo.UltimoMembroPegou != null)
        {
            emprestimo.GerarRelatorio(jogo, jogo.UltimoMembroPegou);
        }
        else
        {
            Console.WriteLine("Esse jogo ainda não foi emprestado");
        }

    }
    else if (intOpcao == 0)
    {
        bibliotecaDeJogos.Salvar();
        listaDeMembros.SalvarEmJson();
        break;
        
    }
}







