using System.Collections.Generic;
using ToDoList;

internal class Program
{
    private static void Main(string[] args)
    {
        Categoria categoria = new Categoria();
        categoria.AdicionarCategoria("Pessoal");
        categoria.AdicionarCategoria("Profissional");
        categoria.AdicionarCategoria("Acadêmico");

        

        List<Pessoa> listaPessoa = new List<Pessoa>();
        List<ToDo> listaTarefas = new List<ToDo>();

        Pessoa pessoa = new Pessoa("Ari");
        listaPessoa.Add(pessoa);

        int opcao = 0;
        int opcao_interna = 0;
        ToDo tarefa = null;
        string? descricao = null;
        bool flag;

        while((opcao = Menu()) != 11)
        {
       
            switch (opcao)
            {
                case 1:
                    do
                    {
                        flag = false;
                        Console.WriteLine("Digite a descricao: ");
                        descricao = Console.ReadLine();
                        if( !(string.IsNullOrEmpty(descricao)) )
                        {
                            flag = true;
                            tarefa = CriacaoTarefa(descricao);
                            tarefa.SetCategoria(categoria);
                            tarefa.SetPessoa(listaPessoa[0]);   
                            listaTarefas.Add(tarefa);
                        }
                        else
                        {
                            Console.WriteLine("Nenhum caracter foi digitado !");
                        }


                    }while(flag == false);

                break;

                case 2:
                    flag = false;
                    tarefa = null;

                    while ((tarefa = ProcurarTarefa(listaTarefas)) == null) { }

                    Console.WriteLine("Digite a nova descrição da tarefa: ");
                    descricao = Console.ReadLine();
                    do
                    {
                        if (!(string.IsNullOrEmpty(descricao)))
                        {
                            flag = true;
                            tarefa.SetDescricao(descricao);
                            Console.WriteLine("Descrição alterada com sucesso !");
                        }
                        else
                        {
                            Console.WriteLine("Nenhum caracter foi digitado !");
                        }

                    }while(flag == false);

                break;

                case 3:

                    do
                    {
                        Console.WriteLine("[1] - INSERIR UMA NOVA CATEGORIA A LISTA DE CATEGORIAS");
                        Console.WriteLine("[2] - INSERIR OU ALTERAR UMA CATEGORIA EM UMA TAREFA CADASTRADA");
                        opcao_interna = int.Parse(Console.ReadLine());

                    }while(opcao_interna != 1 && opcao_interna != 2);

                    if(opcao_interna == 1) 
                    {
                        Console.WriteLine("Digite a nova categoria: ");
                        string categ = Console.ReadLine();
                        categoria.AdicionarCategoria(categ);    
                    }
                    else
                    {
                        tarefa = null;
                        while ((tarefa = ProcurarTarefa(listaTarefas)) == null) { }
                        Console.WriteLine("Digite a categoria: ");
                        string cat = Console.ReadLine();
                        tarefa.SetCategoriaEscolhida(cat);  
                    }

                break;

                case 4:

                    do
                    {
                        Console.WriteLine("[1] - INSERIR UMA NOVA PESSOA A LISTA DE PESSOAS");
                        Console.WriteLine("[2] - INSERIR OU ALTERAR UMA PESSOA EM UMA TAREFA CADASTRADA");
                        opcao_interna = int.Parse(Console.ReadLine());

                    }while((opcao_interna != 1) && (opcao_interna != 2));


                    flag = false;
                    string? nome = null;
                    do
                    {
                        Console.WriteLine("Digite o nome da pessoa: ");
                        nome = Console.ReadLine();
                        if (!(string.IsNullOrEmpty(nome)))
                        {
                            flag = true;
                        }       

                    }while(flag == false);

                    if (opcao_interna == 1)
                    {
                       listaPessoa.Add(new(nome));
                    }
                    else
                    {
                        flag = false;
                        tarefa = null;
                        pessoa = null;
                        while ((pessoa = ProcurarPessoa(listaPessoa,nome)) == null) 
                        {
                            Console.WriteLine("Pessoa não encontrada !");
                            Console.WriteLine("Digite novamente o nome");
                            nome = Console.ReadLine();

                            do
                            {
                                if (!(string.IsNullOrEmpty(nome)))
                                {
                                    flag = true;
                                }

                            }while(flag == false);
                        }
                        while ((tarefa = ProcurarTarefa(listaTarefas)) == null) { }
                        tarefa.SetPessoa(pessoa);
                    }
                break;

                case 5:
                    while ((tarefa = ProcurarTarefa(listaTarefas)) == null) { }
                    Console.WriteLine(tarefa.GetData_criacao());
                break;

                case 6:
                    Console.WriteLine("Método ainda não implementada");
                break;

                case 7:
                    while ((tarefa = ProcurarTarefa(listaTarefas)) == null) { }
                    tarefa.SetStatus();
                break;

                case 8:
                    ListarPessoas(listaPessoa);
                break;

                case 9:
                    categoria.ListarCategoria();
                break;

                case 10:
                    ListarTarefas(listaTarefas);
                break;

                default:
                    Console.WriteLine("Opção inválida !");
                break;
            }

            Console.WriteLine();

        }



    }

    private static int Menu()
    {
        int opcao = 0;

        Console.WriteLine("OPERAÇÕES SOBREA TAREFA: ");
        Console.WriteLine("1 - CRIAR TAREFA");
        Console.WriteLine("2 - ALTERAR DESCRIÇÃO");
        Console.WriteLine("3 - CATEGORIA");
        Console.WriteLine("4 - PESSOA");
        Console.WriteLine("5 - DATA DE CRIAÇÃO");
        Console.WriteLine("6 - DATA DE VENCIMENTO");
        Console.WriteLine("7 - MUDAR STATUS");
        Console.WriteLine("8 - LISTAR PESSOAS");
        Console.WriteLine("9 - LISTAR CATEGORIAS");
        Console.WriteLine("10 - LISTAS TAREFAS");
        Console.WriteLine("11 - SAIR");
        opcao = int.Parse(Console.ReadLine());

        return opcao;
    }
    private static ToDo CriacaoTarefa(string descricao)
    {
        ToDo tarefa = new ToDo(descricao);

        return tarefa;
    }
    private static ToDo ProcurarTarefa(List<ToDo> listaTarefas) 
    {
        ToDo tarefa = null;

        Console.WriteLine("Digite o ID da tarefa: ");
        string id = Console.ReadLine(); 

        id = id.ToUpper().Trim();

        int contador = 0;
        bool flag = true;

        while((contador < listaTarefas.Count) && (flag))
        {
            if (listaTarefas[contador].GetID().ToUpper().Equals(id)) 
            {
                flag = false;
                tarefa = listaTarefas[contador];
            }

            contador++;
        }

        return tarefa;
    }
    private static Pessoa ProcurarPessoa(List<Pessoa> listaPessoa, string nome)
    {
        Pessoa pessoa = null;
        nome = nome.ToUpper().Trim();

        int contador = 0;
        bool flag = true;
        while((contador < listaPessoa.Count) && (flag))
        {
            if(listaPessoa[contador].GetNome().ToUpper().Equals(nome))
            {
                flag = false;
                pessoa = listaPessoa[contador];
            }
            contador++;
        }
        return pessoa;
    }
    private static void ListarPessoas(List<Pessoa> listaPessoa)
    {
        if(listaPessoa.Count == 0)
        {
            Console.WriteLine("Não há nenhuma pessoa cadastrada");
        }
        else
        {
            Console.WriteLine("Lista de pessoas cadastradas: ");
            foreach(Pessoa item_pessoa in listaPessoa)
            {
                Console.WriteLine(item_pessoa.ToString());
            }
        }
    }
    private static void ListarTarefas(List<ToDo> listasTarefas)
    {
        if(listasTarefas.Count == 0)
        {
            Console.WriteLine("Não há nenhum tarefa cadastrada !");
        }
        else
        {
            Console.WriteLine("Lista de tarefas cadastradas: ");
            foreach(ToDo item_tarefa in listasTarefas)
            {
                Console.WriteLine(item_tarefa.ToString());
            }
        }
    }
}