using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Packed_Lunch.Models
{
    public class cardapioViewModel
    {
        //Itens tabela cardapio;
        public DateTime? Data_Fim { get; set; }
        public DateTime? Data_ini { get; set; }
        public Int32 Id_Restaurante_fk { get; set; }

        //Tabela compõe cardapio;
        public Int32 Id_Cardapio_fk { get; set; }
        public Int32 Id_Produto_Fk { get; set; }
        
        //Tabela produto
        public string Descricao { get; set; }
        public string Nome { get; set; }
        public Nullable<decimal> Valor { get; set; }
                     
    }
}