using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    internal class Pessoa
    {
        private string _id;
        private string _nome;

        public Pessoa(string nome)
        {
            var temp = Guid.NewGuid();
            this._id = temp.ToString().Substring(0, 3);
            this._nome = nome;
        }
        public Pessoa(string id, string nome) 
        {
            _id = id;
            _nome = nome;
        }

        public string GetID()
        {
            return _id; 
        }
        public string GetNome()
        {
            return this._nome;
        }
        public void SetNome(string nome)
        {
            this._nome = nome;
        }
        public override string ToString()
        {
            return "ID: " + GetID() + " Nome: " + GetNome(); 
        }
        public string PersonToFile()
        {
            return $"{this._id};{this._nome}";
        }

    }
}
