using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Linq;

namespace Cadastro_de_Pessoas
{
    public class Menu
    {
        public static void MostarMenu()
        {
            Console.WriteLine("******  Menu  ******");
            Console.WriteLine("[1] - Cadastrar");
            Console.WriteLine("[2] - Mostrar Cadastros");
            Console.WriteLine("[3] - Atualizar Cadastro");
            Console.WriteLine("[4] - Deletar Cadastro");
            Console.WriteLine("[5] - Cadastrar Telefone");
            Console.WriteLine("[6] - Deletar Telefone");
            Console.WriteLine("[7] - Mostar Telefones");
            Console.WriteLine("[8] - Mostrar Telefone/Nome");
            Console.WriteLine("[0] - Sair");
            Console.WriteLine("______________________________");
        }
        public static void Mostarcadastros()
        {
            string connection = @"Data Source=DESKTOP-IR1AB95;Initial Catalog=cadastropessoas;Integrated Security=True;";
            List<Pessoa> listaCadastro = new List<Pessoa>();
            List<Telefone> listaCadastro1 = new List<Telefone>();
            try
            {
                SqlDataReader resultado;
                //var query = "SELECT p.Id as IdPessoa, p.Nome as Nome, p.Cpf as Cpf, p.Rg as p.Rg, p.DatadeNascimento as DatadeNascimento, p.Naturalidade as Naturalidade, t.Numero as Numero, t.Ddd as Ddd FROM Pessoa p LEFT JOIN Telefone t ON p.Id = t.IdPessoa ";
                var query = "SELECT p.Id, p.Nome, p.Cpf, p.Rg, p.DatadeNascimento, p.Naturalidade, t.Numero, t.Ddd FROM Pessoa p LEFT JOIN Telefone t ON p.Id = t.IdPessoa ";
                // @"SELECT p.Id as IdPessoa, p.Nome as Nome, p.Cpf as Cpf, t.DDD as DDD, t.Numero as Numero FROM Pessoa p INNER JOIN Telefone t ON p.Id = @id";
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
                        listaCadastro1.Add(new Telefone(resultado.GetString(resultado.GetOrdinal("Ddd")),
                                                        (resultado.GetString(resultado.GetOrdinal("Numero")))));
                    }
                }
                Console.WriteLine("========Listagem========");
                for (int i = 0; i < listaCadastro.Count; i++)
                {
                    Console.WriteLine("========Inicio========");
                    Console.WriteLine("Nome: " + listaCadastro[i].Id);
                    Console.WriteLine("Nome: " + listaCadastro[i].Nome);
                    Console.WriteLine("CPF: " + listaCadastro[i].Cpf);
                    Console.WriteLine("Rg: " + listaCadastro[i].Rg);
                    Console.WriteLine("Data de Nascimento: " + listaCadastro[i].DatadeNascimento);
                    Console.WriteLine("Naturalidade: " + listaCadastro[i].Naturalidade);
                    Console.WriteLine("Ddd: " + listaCadastro1[i].Ddd);
                    Console.WriteLine("Numero: " + listaCadastro1[i].Numero);
                    Console.WriteLine("========Fim========");
                    
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }

            //try
            //{
            //    SqlDataReader resultado;
            //    var query = "SELECT Id, Nome FROM Pessoa ";

            //    using (var sql = new SqlConnection(connection))
            //    {
            //        SqlCommand command = new SqlCommand(query, sql);
            //        command.Connection.Open();
            //        resultado = command.ExecuteReader();

            //        while (resultado.Read())
            //        {
            //            listaCadastro.Add(new Pessoa(resultado.GetInt32(resultado.GetOrdinal("Id")),
            //                                         resultado.GetString(resultado.GetOrdinal("Nome"))));
            //        }
            //    }
            //    Console.WriteLine("========Listagem========");
            //    foreach (Pessoa p in listaCadastro)
            //    {
            //        Console.WriteLine("---------------------------");
            //        Console.WriteLine("Id: " + p.Id);
            //        Console.WriteLine("Nome: " + p.Nome);

            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Erro: " + ex.Message);
            //}
        }

    }
}
