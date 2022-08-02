using System;
using System.Collections.Generic;
using System.Text;

namespace Cadastro_de_Pessoas
{
    public class Telefone
    {
        public int Id { get; set; }
        public string Ddd { get; set; }
        public string Numero { get; set; }

        public Telefone()
        {
        }

        public Telefone(int id, string ddd, string numero)
        {
            Id = id;
            Ddd = ddd;
            Numero = numero;
        }

        public Telefone(string ddd, string numero)
        {
            Ddd = ddd;
            Numero = numero;
        }

        public Telefone(int id)
        {
            Id = id;
        }
    }
}
