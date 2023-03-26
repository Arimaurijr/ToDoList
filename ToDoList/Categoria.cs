using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    internal class Categoria
    {
        private List<string> _lista_categoria;

        public Categoria()
        {
            _lista_categoria = new List<string>();
        }
        public void AdicionarCategoria(string categoria)
        {
             categoria = categoria.Trim().ToUpper();
             _lista_categoria.Add(categoria);
        }
        public string ProcurarCategoria(string categoria)
        {
            categoria = categoria.Trim().ToUpper();
            string? categoria_encontrada = null;

            bool achar =  false;
            int i = 0;

            while((i < _lista_categoria.Count) && (achar == false))
            {
                if (_lista_categoria[i].Equals(categoria)) 
                {
                    achar = true;
                    categoria_encontrada = categoria;
                }

                i++;
            }
           
            return categoria_encontrada;

        }
        public void ListarCategoria()
        {
            if (_lista_categoria.Count == 0)
            {
                Console.WriteLine("Não há nenhuma categoria cadastrada !");
            }
            else
            {
                foreach (string item in _lista_categoria)
                {
                    Console.WriteLine(item);
                }
            }
        }

    }
}
