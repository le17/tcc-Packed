//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Packed_Lunch.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Entrega
    {
        public int Id_Entrega { get; set; }
        public Nullable<int> Id_pedido_fk { get; set; }
        public Nullable<int> Id_entregador_fk { get; set; }
    
        public virtual Entregador Entregador { get; set; }
        public virtual Pedido Pedido { get; set; }
    }
}
