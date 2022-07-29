using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace Cadastro_de_Pessoas
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string connection = @"Data Source=ITELABD04\SQLEXPRESS;Initial Catalog=Cadastro de Pessoas;Integrated Security=True;";
            List<Pessoa> listaCadastro = new List<Pessoa>();

            Menu.MostarMenu();

            int opcao = Convert.ToInt32(Console.ReadLine());

            while (opcao != 0)
            {
                if (opcao == 1)
                {
                    Console.WriteLine("Nome:");
                    string nome = Console.ReadLine(); 
                    Console.WriteLine("CPF: ");
                    string cpf = Console.ReadLine();
                    Console.WriteLine("RG:");
                    string rg = Console.ReadLine();
                    Console.WriteLine("Data de Nascimento:");
                    DateTime datanascimento = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Naturalidade:");
                    string naturalidade = Console.ReadLine();

                    Pessoa pessoa = new Pessoa( nome, cpf, rg, datanascimento, naturalidade);

                    try
                    {
                        var query = "insert into Pessoa " + 
                            "(Nome, Cpf, Rg, DatadeNascimento, Naturalidade)" + 
                            "values (@nome,@cpf,@rg,@datanascimento,@naturalidade)";

                        using (var sql = new SqlConnection(connection))
                        {
                            SqlCommand command = new SqlCommand(query, sql);
                            command.Parameters.AddWithValue("@nome", pessoa.Nome);
                            command.Parameters.AddWithValue("@cpf", pessoa.Cpf);
                            command.Parameters.AddWithValue("@rg", pessoa.Rg);
                            command.Parameters.AddWithValue("@datanascimento", pessoa.DatadeNascimento);
                            command.Parameters.AddWithValue("@naturalidade", pessoa.Naturalidade);
                            command.Connection.Open();
                            command.ExecuteNonQuery();
                        }
                        Console.WriteLine("Cadastrado com Sucesso");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro: " + ex.Message);
                    }
                    Menu.MostarMenu();
                    opcao = Convert.ToInt32(Console.ReadLine());

                }
                else if (opcao == 2)
                {
                    try
                    {
                        SqlDataReader resultado;
                        var query = "SELECT Id, Nome, Cpf, Rg, DatadeNascimento, Naturalidade FROM Pessoa ";

                        using (var sql = new SqlConnection(connection))
                        {
                            SqlCommand command = new SqlCommand(query, sql);
                            command.Connection.Open();
                            resultado = command.ExecuteReader();

                            while (resultado.Read())
                            {
                                listaCadastro.Add(new Pessoa(resultado.GetInt32(resultado.GetOrdinal("Id")),
                                                             resultado.GetString(resultado.GetOrdinal("Nome")),
                                                             resultado.GetString(resultado.GetOrdinal("Cpf")),
                                                             resultado.GetString(resultado.GetOrdinal("Rg")),
                                                             resultado.GetDateTime(resultado.GetOrdinal("DatadeNascimento")),
                                                             resultado.GetString(resultado.GetOrdinal("Naturalidade"))));
                            }
                        }
                        Console.WriteLine("========Listagem========");
                        foreach (Pessoa p in listaCadastro)
                        {
                            Console.WriteLine("========Inicio========");
                            Console.WriteLine("Nome: " + p.Nome);
                            Console.WriteLine("CPF: " + p.Cpf);
                            Console.WriteLine("Rg: " + p.Rg);
                            Console.WriteLine("Data de Nascimento: " + p.DatadeNascimento);
                            Console.WriteLine("Naturalidade: " + p.Naturalidade);
                            Console.WriteLine("========Fim========");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro: " + ex.Message);
                    }
                    Menu.MostarMenu();
                    opcao = Convert.ToInt32(Console.ReadLine());
                }
                else if (opcao == 3)
                {
                    Console.WriteLine("Qual cadastro deseja Atualizar? ");
                    try
                    {
                        SqlDataReader resultado;
                        var query = "SELECT Id, Nome FROM Pessoa ";

                        using (var sql = new SqlConnection(connection))
                        {
                            SqlCommand command = new SqlCommand(query, sql);
                            command.Connection.Open();
                            resultado = command.ExecuteReader();

                            while (resultado.Read())
                            {
                                listaCadastro.Add(new Pessoa(resultado.GetInt32(resultado.GetOrdinal("Id")),
                                                             resultado.GetString(resultado.GetOrdinal("Nome"))));
                            }
                        }
                        Console.WriteLine("========Listagem========");
                        foreach (Pessoa p in listaCadastro)
                        {
                            Console.WriteLine("---------------------------");
                            Console.WriteLine("Id: " + p.Id);
                            Console.WriteLine("Nome: " + p.Nome);
                            
                        } 
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro: " + ex.Message);
                    }
                    try
                    {
                        var query = "UPDATE Pessoa SET Nome = @nome Where Id = @id";
                        //Cpf = @cpf, Rg = @rg, Datadenascimento = @datadeNascimento, Naturalidade = @naturalidade

                        using (var sql = new SqlConnection(connection))
                        {
                            SqlCommand command = new SqlCommand(query, sql);
                            command.Connection.Open();

                            Console.WriteLine("---------------------------");
                            Console.WriteLine("Digite o Id que deseja Alterar: ");
                            int resp = Convert.ToInt32(Console.ReadLine());
                            command.Parameters.AddWithValue("@id", resp);

                            Console.WriteLine("Digite o novo nome: ");
                            string nvnome = Console.ReadLine();
                            command.Parameters.AddWithValue("@nome", nvnome);

                            //Console.WriteLine("Digite o novo CPF: ");
                            //string nvcpf = Console.ReadLine();
                            //command.Parameters.AddWithValue("@cpf", nvcpf);

                            command.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro: " + ex.Message);
                    }
                    Menu.MostarMenu();
                    opcao = Convert.ToInt32(Console.ReadLine());
                }
                else if (opcao == 4)
                {
                    Console.WriteLine("Qual Cadastro deseja DELETAR ??");

                    try
                    {
                        SqlDataReader resultado;
                        var query = "SELECT Id, Nome FROM Pessoa ";

                        using (var sql = new SqlConnection(connection))
                        {
                            SqlCommand command = new SqlCommand(query, sql);
                            command.Connection.Open();
                            resultado = command.ExecuteReader();
                            while (resultado.Read())
                            {
                                listaCadastro.Add(new Pessoa(resultado.GetInt32(resultado.GetOrdinal("Id")),
                                                             resultado.GetString(resultado.GetOrdinal("Nome"))));
                            }
                        }
                        Console.WriteLine("========Listagem========");
                        foreach (Pessoa p in listaCadastro)
                        {
                            Console.WriteLine("---------------------------");
                            Console.WriteLine("Id: " + p.Id);
                            Console.WriteLine("Nome: " + p.Nome);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro: " + ex.Message);
                    }
                    
                    try
                    { 
                        var query = "DELETE from Pessoa WHERE Id = @Id";
                        using (var sql = new SqlConnection(connection))
                        {
                            SqlCommand command = new SqlCommand(query, sql);
                            command.Connection.Open();

                            Console.WriteLine("---------------------------");
                            Console.WriteLine("Digite o Id para DELETAR: ");
                            int resp = Convert.ToInt32(Console.ReadLine());
                            command.Parameters.AddWithValue("@id", resp);

                            command.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro: " + ex.Message);
                    }

                    Menu.MostarMenu();
                    opcao = Convert.ToInt32(Console.ReadLine());
                }
                else if (opcao == 5) 
                {
                    
                    try
                    {
                        SqlDataReader resultado;
                        var query = "SELECT Id, Nome FROM Pessoa ";

                        using (var sql = new SqlConnection(connection))
                        {
                            SqlCommand command = new SqlCommand(query, sql);
                            command.Connection.Open();
                            resultado = command.ExecuteReader();
                            while (resultado.Read())
                            {
                                listaCadastro.Add(new Pessoa(resultado.GetInt32(resultado.GetOrdinal("Id")),
                                                             resultado.GetString(resultado.GetOrdinal("Nome"))));
                            }
                        }
                        Console.WriteLine("========Listagem========");
                        foreach (Pessoa p in listaCadastro)
                        {
                            Console.WriteLine("---------------------------");
                            Console.WriteLine("Id: " + p.Id);
                            Console.WriteLine("Nome: " + p.Nome);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro: " + ex.Message);
                    }

                    try
                    {
                        var query = "insert into Telefone " +
                            "(Ddd, Numero, IdPessoa)" +
                            "values (@ddd, @numero, @idPessoa)";

                        using (var sql = new SqlConnection(connection))
                        {
                            SqlCommand command = new SqlCommand(query, sql);
                            command.Connection.Open();

                            Console.WriteLine("Digite o Id da Pessoa para Cadastro");
                            int resp = Convert.ToInt32(Console.ReadLine());
                            command.Parameters.AddWithValue("@idPessoa", resp);

                            Console.WriteLine("DDD:");
                            string ddd1 = Console.ReadLine();
                            command.Parameters.AddWithValue("@ddd", ddd1);

                            Console.WriteLine("Telefone: ");
                            string numero1 = Console.ReadLine();
                            command.Parameters.AddWithValue("@numero", numero1);

                            command.ExecuteNonQuery();
                        }
                        Console.WriteLine("Cadastrado com Sucesso");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro: " + ex.Message);
                    }
                    Menu.MostarMenu();
                    opcao = Convert.ToInt32(Console.ReadLine());

                }
            }  
        }
    }
}
