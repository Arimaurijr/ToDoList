using System.Collections.Generic;
using ToDoList;

internal class Program
{

    private static void Main(string[] args)
    {
        List<Pessoa> listaPessoa = new List<Pessoa>();
        List<ToDo> listaTarefas = new List<ToDo>();

        Pessoa pessoa = new Pessoa("Ari");
        listaPessoa.Add(pessoa);

        Categoria categoria = new Categoria();
        categoria.AdicionarCategoria("Pessoal");
        categoria.AdicionarCategoria("Profissional");
        categoria.AdicionarCategoria("Acadêmico");

        if (!File.Exists("ToDoTasks.txt"))
        {
            StreamWriter sw = new("ToDoTasks.txt");
            sw.Close();
        }
        else
        {
            listaTarefas = ReadFileListToDo("ToDoTasks.txt");

            Console.Write("Impressão teste");
            foreach(ToDo item in listaTarefas)
            {
                Console.WriteLine(item.ToString());
            }
        }
        

        string opcao = "";
        int opcao_interna = 0;
        ToDo tarefa = null;
        string? descricao = null;
        bool flag;

        while((opcao = Menu()) != "11")
        {
       
            switch (opcao)
            {
                case "1":
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
                            bool date_option = true;
                            do
                            {
                                string option;
                                Console.WriteLine("Deseja inserir uma data final:\n[1] - Sim\n[2] - Não");
                                option = Console.ReadLine();
                                switch (option)
                                {
                                    case "1":
                                        tarefa.SetDataVencimento(GetTempoFinal());
                                        date_option = false;
                                        break;
                                    case "2":
                                        date_option = false;
                                        break;
                                    default:
                                        Console.WriteLine("Valor inválido");
                                        break;
                                }
                            } while (date_option);

                            //inserir pessoa ou não para o padrão
                            tarefa.SetPessoa(listaPessoa[0]);
                            listaTarefas.Add(tarefa);
                            UpdateFile("ToDoTasks.txt", listaTarefas);
                        }
                        else
                        {
                            Console.WriteLine("Nenhum caracter foi digitado !");
                        }


                    }while(flag == false);

                break;

                case "2":
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

                case "3":

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

                case "4":

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

                case "5":
                    while ((tarefa = ProcurarTarefa(listaTarefas)) == null) { }
                    Console.WriteLine(tarefa.GetData_criacao());
                break;

                case "6":
                    Console.WriteLine("Método ainda não implementada");
                break;

                case "7":
                    while ((tarefa = ProcurarTarefa(listaTarefas)) == null) { }
                    tarefa.SetStatus();
                    UpdateFile("ToDoTasks.txt", listaTarefas);
                break;

                case "8":
                    ListarPessoas(listaPessoa);
                break;

                case "9":
                    categoria.ListarCategoria();
                break;

                case "10":
                    ListarTarefas(listaTarefas);
                break;

                default:
                    Console.WriteLine("Opção inválida !");
                break;
            }

            Console.WriteLine();

        }



    }

    private static DateTime GetTempoFinal()
    {
        int day = VerifyDate('o', "dia");
        int month = VerifyDate('o', "mês");
        int year = VerifyDate('o', "ano");
        int hour = VerifyDate('a', "hora");
        int minute = VerifyDate('o', "minuto");

        DateTime time = new(year,month,day,hour,minute, 0);

        return time;
    }

    private static int VerifyDate(char article, string variable)
    { 
        int correct;
        bool aux = true;

        do
        {
            Console.Write($"Informe {article} {variable} desejad{article}: ");
            aux = int.TryParse(Console.ReadLine(), out correct);
            if (!aux)
                Console.WriteLine($"{variable} inválid{article} deve ser um número inteiro positivo");
        } while (!aux && correct >= 0);

        return correct;
    }

    private static string Menu()
    {
        string opcao = "";

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
        opcao = Console.ReadLine();

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

        id = id.ToLower().Trim();

        int contador = 0;
        bool flag = true;

        while((contador < listaTarefas.Count) && (flag))
        {
            if (listaTarefas[contador].GetID().Equals(id)) 
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

    private static void UpdateFile(string file, List<ToDo> list)
    {
        try
        {
            StreamWriter sw = new(file);
            foreach (var item in list)
            {
                sw.WriteLine(item.ToFile());
            }
            sw.Close();
        }
        catch (Exception)
        {
            throw;
        }
    }
    private static List<ToDo> ReadFileListToDo(string file)
    {
        string item;

        string id_tarefa;
        string descricao_tarefa;
        string id_proprietario_tarefa;
        string nome_proprietario_tarefa;

        int dia_criacao;
        int mes_criacao;
        int ano_criacao;
        int hora_criacao;
        int minutos_criacao;

        int dia_vencimento;
        int mes_vencimento;
        int ano_vencimento;
        int hora_vencimento;
        int minutos_vencimento;

        bool status;
        string categoria;
        ToDo tarefa;
        Pessoa pessoa;
        DateTime data_criacao;
        DateTime data_vencimento;

        List<ToDo> retorno = new List<ToDo>();

        try
        {
            StreamReader sr = new StreamReader(file);
            while(!sr.EndOfStream)
            {
                item = sr.ReadLine();
                string[] linha = item.Split(";");

                id_tarefa = linha[0];
                descricao_tarefa = linha[1];

                id_proprietario_tarefa = linha[2];
                nome_proprietario_tarefa = linha[3];
                pessoa = new(id_proprietario_tarefa, nome_proprietario_tarefa);

                dia_criacao = int.Parse(linha[4]);
                mes_criacao = int.Parse(linha[5]);
                ano_criacao = int.Parse(linha[6]);
                hora_criacao = int.Parse(linha[7]);
                minutos_criacao = int.Parse(linha[8]);

                data_criacao = new(ano_criacao, mes_criacao, dia_criacao, hora_criacao, minutos_criacao, 0);

                dia_vencimento = int.Parse(linha[9]);
                mes_vencimento = int.Parse(linha[10]);
                ano_vencimento = int.Parse(linha[11]);
                hora_vencimento = int.Parse(linha[12]);
                minutos_vencimento = int.Parse(linha[13]);

                data_vencimento = new(ano_vencimento, mes_vencimento, dia_vencimento, hora_vencimento, minutos_vencimento, 0);

                status = bool.Parse(linha[14]);
                categoria = linha[15];

                tarefa = new ToDo(id_tarefa,descricao_tarefa,pessoa,data_criacao,data_vencimento,status,categoria);

                retorno.Add(tarefa);
                sr.Close();

            }

        }
        catch(Exception n)
        {

        }

        return retorno;
    }
    
}