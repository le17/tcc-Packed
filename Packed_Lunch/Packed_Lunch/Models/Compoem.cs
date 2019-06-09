using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Packed_Lunch.Models
{
    public partial class Compoem
    {
        public int Id_Cardapio_fk { get; set; }
        public int Id_Produto_fk {get;set;}

        public virtual Cardapio Cardapios { get; set; }
        public virtual Produto Produtoes { get; set; }

    }
}
