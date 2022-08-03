using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Cadastro_de_Pessoas
{
    public class Program
    {

        public static void Main(string[] args)
        {
            string connection = @"Data Source=ITELABD04\SQLEXPRESS;Initial Catalog=Cadastro de Pessoas;Integrated Security=True;";
            //string connection = @"Data Source=DESKTOP-IR1AB95;Initial Catalog=cadastropessoas;Integrated Security=True;";
            Console.WriteLine("|======================== Cadastro de Pessoas =========================|");
            Menu.MostarMenu();
            Console.Write("Digite o que deseja fazer: ");
            int opcao = Convert.ToInt32(Console.ReadLine());

            while (opcao != 0)
            {
                // Cadastrar
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
                    Console.Write("Digite o que deseja fazer: ");
                    opcao = Convert.ToInt32(Console.ReadLine());

                }
                // Mostrar Cadastros
                else if (opcao == 2)
                {
                    Menu.Mostarcadastros();
                    Menu.MostarMenu();
                    Console.Write("Digite o que deseja fazer: ");
                    opcao = Convert.ToInt32(Console.ReadLine());
                }
                // Atualizar Cadastro
                else if (opcao == 3)
                {
                    Console.WriteLine("Qual cadastro deseja Atualizar? ");
                    Menu.Mostarcadastros();

                    try
                    {
                        var query = "UPDATE Pessoa SET Nome = @nome Where Id = @id";
                        //Cpf = @cpf, Rg = @rg, Datadenascimento = @datadeNascimento, Naturalidade = @naturalidade

                        using (var sql = new SqlConnection(connection))
                        {
                            SqlCommand command = new SqlCommand(query, sql);
                            command.Connection.Open();

                            Console.WriteLine("|_____________________________________________________________________|");
                            Console.Write("Digite o Id que deseja Alterar: ");
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
                    Console.Write("Digite o que deseja fazer: ");
                    opcao = Convert.ToInt32(Console.ReadLine());
                }
                //  Deletar Cadastro Completo
                else if (opcao == 4)
                {
                    Console.WriteLine("Qual Cadastro deseja DELETAR ??");

                    Menu.Mostarcadastros();
                    
                    try
                    {
                        var query = "DELETE from Telefone WHERE IdPessoa = @Id DELETE from Pessoa WHERE Id = @Id";
                        using (var sql = new SqlConnection(connection))
                        {
                            SqlCommand command = new SqlCommand(query, sql);
                            command.Connection.Open();

                            Console.WriteLine("|_____________________________________________________________________|");
                            Console.Write(" Digite o Id para DELETAR: ");
                            int resp = Convert.ToInt32(Console.ReadLine());
                            command.Parameters.AddWithValue("@id", resp);

                            command.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro: " + ex.Message);
                    }

                    Console.WriteLine(" CADASTRO DELETADO!!!");

                    Menu.MostarMenu();
                    Console.Write(" Digite o que deseja fazer: ");
                    opcao = Convert.ToInt32(Console.ReadLine());
                }
                // Cadastrar Telefone
                else if (opcao == 5) 
                {
                    Menu.Mostarcadastros();

                    try
                    {
                        var query = "insert into Telefone " +
                            "(Ddd, Numero, IdPessoa)" +
                            "values (@ddd, @numero, @idPessoa)";

                        using (var sql = new SqlConnection(connection))
                        {
                            SqlCommand command = new SqlCommand(query, sql);
                            command.Connection.Open();

                            Console.WriteLine(" Digite o Id da Pessoa para Cadastro");
                            int termo = Convert.ToInt32(Console.ReadLine());

                            //----
                            List<Pessoa> pessoasEncontradas = EncontrarPessoa(termo);
                            while (pessoasEncontradas.Count() > 1 || pessoasEncontradas.Count == 0)
                            {
                                if (pessoasEncontradas.Count() == 0)
                                {
                                    Console.WriteLine(" Nenhuma pessoa encontrada. Insira um termo válido");
                                    termo = Convert.ToInt32(Console.ReadLine());
                                    pessoasEncontradas = EncontrarPessoa(termo);
                                }
                                else if (pessoasEncontradas.Count() > 1)
                                {
                                    Console.WriteLine(" Mais de uma pessoa encontrada. Insira um termo mais preciso:");
                                    termo = Convert.ToInt32(Console.ReadLine());
                                    pessoasEncontradas = EncontrarPessoa(termo);
                                }
                            }
                            //-----
                            command.Parameters.AddWithValue("@idPessoa", termo);

                            Console.WriteLine(" DDD: ");
                            string ddd1 = Console.ReadLine();
                            command.Parameters.AddWithValue("@ddd", ddd1);

                            Console.WriteLine(" Telefone: ");
                            string numero1 = Console.ReadLine();
                            command.Parameters.AddWithValue("@numero", numero1);

                            command.ExecuteNonQuery();
                        }
                        Console.WriteLine(" Cadastrado com Sucesso");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(" Erro: " + ex.Message);
                    }
                    Menu.MostarMenu();
                    Console.Write(" Digite o que deseja fazer: ");
                    opcao = Convert.ToInt32(Console.ReadLine());

                }
                // Deletar Telefone
                else if (opcao == 6)
                {
                    Console.WriteLine(" Qual Cadastro deseja DELETAR ??");

                    Menu.Mostarcadastros();

                    try
                    {
                        var query = "DELETE from Telefone WHERE IdPessoa = @Id";
                        using (var sql = new SqlConnection(connection))
                        {
                            SqlCommand command = new SqlCommand(query, sql);
                            command.Connection.Open();

                            Console.WriteLine("|_____________________________________________________________________|");
                            Console.Write(" Digite o Id para DELETAR: ");
                            int resp = Convert.ToInt32(Console.ReadLine());
                            command.Parameters.AddWithValue("@id", resp);

                            command.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(" Erro: " + ex.Message);
                    }
                    Menu.MostarMenu();
                    Console.Write(" Digite o que deseja fazer: ");
                    opcao = Convert.ToInt32(Console.ReadLine());
                }
                // Mostar Telefones
                else if (opcao == 7)
                {
                    Menu.MostrarTelefones();
                    Menu.MostarMenu();
                    Console.Write(" Digite o que deseja fazer: ");
                    opcao = Convert.ToInt32(Console.ReadLine());
                }
                // Mostrar Telefone/Nome
                else if (opcao == 8)
                {
                    Console.WriteLine(" Mostrar Telefone/Nome");


                    Menu.MostrarNumerosPorNome();


                    Menu.MostarMenu();
                    Console.Write(" Digite o que deseja fazer: ");
                    opcao = Convert.ToInt32(Console.ReadLine());
                }
                // Quantidade de Telefones
                else if (opcao == 9)
                {
                    List<Telefone> listaCadastro1 = new List<Telefone>();
                    SqlDataReader resultado;
                    var query = "SELECT t.Id FROM Telefone t ";

                    using (var sql = new SqlConnection(connection))
                    {
                        SqlCommand command = new SqlCommand(query, sql);
                        command.Connection.Open();
                        resultado = command.ExecuteReader();

                        while (resultado.Read())
                        {
                            listaCadastro1.Add(new Telefone(resultado.GetInt32(resultado.GetOrdinal("Id"))));
                        }
                    }
                    int quantnumeros = listaCadastro1.Count;
                    Console.WriteLine($"{quantnumeros} - Telefones Cadastrados");

                    Menu.MostarMenu();
                    Console.Write(" Digite o que deseja fazer: ");
                    opcao = Convert.ToInt32(Console.ReadLine());

                }
                else if (opcao == 10)
                {
                    Menu.QuantidadeTefefonesPorNome();
                    //List<Telefone> listaCadastro1 = new List<Telefone>();
                    //SqlDataReader resultado;
                    //var query = "SELECT t.Id FROM Telefone t WHERE p.Nome = @nome";

                    //using (var sql = new SqlConnection(connection))
                    //{
                    //    SqlCommand command = new SqlCommand(query, sql);
                    //    command.Connection.Open();
                    //    resultado = command.ExecuteReader();

                    //    while (resultado.Read())
                    //    {
                    //        listaCadastro1.Add(new Telefone(resultado.GetInt32(resultado.GetOrdinal("Id"))));
                    //    }
                    //}
                    //int quantnumeros = listaCadastro1.Count;
                    //Console.WriteLine($"{quantnumeros} - Telefones Cadastrados");

                    Menu.MostarMenu();
                    Console.Write(" Digite o que deseja fazer: ");
                    opcao = Convert.ToInt32(Console.ReadLine());
                }
                // Opção Invalida
                else
                {
                    Console.WriteLine("|________________________________________ ");
                    Console.WriteLine(" Opção Invalida!!! - Digite Novamente.: ");
                    Console.WriteLine("|________________________________________ ");
                    Menu.MostarMenu();
                    Console.Write( " Digite o que deseja fazer: ");
                    opcao = Convert.ToInt32(Console.ReadLine());
                }
            }  
        }
        static List<Pessoa> EncontrarPessoa(int termo)
        {
            string connection = @"Data Source=ITELABD04\SQLEXPRESS;Initial Catalog=Cadastro de Pessoas;Integrated Security=True;";
            //string connection = @"Data Source=DESKTOP-IR1AB95;Initial Catalog=cadastropessoas;Integrated Security=True;";
            List<Pessoa> pessoas2 = new List<Pessoa>();
            SqlDataReader resultado;
            try
            {
                var query = @"SELECT Id FROM Pessoa
                                      WHERE Id like CONCAT('%',@termo,'%')";
                using (var sql = new SqlConnection(connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@termo", termo);
                    command.Connection.Open();
                    resultado = command.ExecuteReader();

                    while (resultado.Read())
                    {
                        pessoas2.Add(new Pessoa(resultado.GetInt32(resultado.GetOrdinal("Id"))));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Erro: " + ex.Message);
            }
            return pessoas2;
        }
    }
}
