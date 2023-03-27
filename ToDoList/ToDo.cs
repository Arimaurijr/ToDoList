using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    internal class ToDo
    {
        private string _id;
        private string _descricao;
        private Categoria _categoria;
        private Pessoa _proprietario;
        private DateTime _data_criacao;
        private DateTime _data_vencimento;
        private bool _status;
        private string _categoria_escolhida;

        public ToDo(string descricao)
        {
            var temp = Guid.NewGuid();
            this._id = temp.ToString().Substring(0, 3);
            this._descricao = descricao;
            this._data_criacao = DateTime.Now;
            this._status = false;
        }

        public string GetID()
        {
            return this._id; 
        }
        public string GetDescricao()
        {
            return this._descricao;
        }
        public void SetDescricao(string descricao)
        {
            this._descricao = descricao;
        }
        public void SetCategoria(Categoria categoria)
        {
            this._categoria = categoria;
        }
        public bool GetStatus()
        {
            return this._status;
        }
        public void SetStatus() 
        {
            if(this._status == false) 
            {
                this._status = true;
            }
            else
            {
                this._status = false;
            }
        }
        public string GetCategoriaEscolhida()
        {
            return this._categoria_escolhida;
        }
        public void SetCategoriaEscolhida(string cte)
        {
            if(_categoria.ProcurarCategoria(cte) == null)
            {
                Console.WriteLine("Categoria não encontrada !");
            }
            else
            {
                cte = cte.Trim().ToUpper();
                this._categoria_escolhida = cte;
                Console.WriteLine("Categoria adicionada com sucesso !");
            } 
        }
        public DateTime GetData_criacao() 
        {
            return _data_criacao;
        }
        public DateTime GetData_vencimento() 
        {
            return this._data_vencimento;
        }
        public void SetDataVencimento(DateTime dataVencimento)
        {
            this._data_vencimento = dataVencimento;
        }
        public Pessoa GetPessoa()
        {
            return this._proprietario;
        }
        public void SetPessoa(Pessoa pessoa)
        {
            this._proprietario = pessoa;
        }

        public override string ToString()
        {
            return "ID: " + GetID() + "\n" +
                   "Descrição: " + GetDescricao() + "\n" +
                   "Categoria: " + GetCategoriaEscolhida() + "\n" +
                   "Pessoa: " + GetPessoa() + "\n" +
                   "Data criação: " + GetData_criacao() + "\n" +
                   "Data de vencimento: " + GetData_vencimento() + "\n" +
                   "Status: " + GetStatus();
        }

        public string ToFile()
        {
            return  $"{GetID()};{GetDescricao()};{GetCategoriaEscolhida()};{GetPessoa()};{GetData_criacao()};{GetData_vencimento()};{GetStatus()}";
        }

    }
}
