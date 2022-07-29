using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Cadastro_de_Pessoas
{
    public class Cadastrar
    {
        SqlConnection Nome = new SqlConnection();


        public Cadastrar()
        {
            Nome.ConnectionString = @"Data Source=ITELABD04\SQLEXPRESS;Initial Catalog=Cadastro_de_Pessoas;Integrated Security=True";
        }
    }
}
