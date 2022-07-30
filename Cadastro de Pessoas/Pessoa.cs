using System;
using System.Collections.Generic;
using System.Text;

namespace Cadastro_de_Pessoas
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public DateTime DatadeNascimento { get; set; }
        public string Naturalidade { get; set; }


        public Pessoa()
        {
        }
        public Pessoa(string nome, string cpf, string rg, DateTime datadeNascimento, string naturalidade)
        {
            
            Nome = nome;
            Cpf = cpf;
            Rg = rg;
            DatadeNascimento = datadeNascimento;
            Naturalidade = naturalidade;
        }

        public Pessoa(int id, string nome, string cpf, string rg, DateTime datadeNascimento, string naturalidade)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            Rg = rg;
            DatadeNascimento = datadeNascimento;
            Naturalidade = naturalidade;
        }

        public Pessoa(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public Pessoa(int id)
        {
            Id = id;
        }
    }
}
