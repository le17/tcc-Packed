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
    
    public partial class Pessoa
    {
        public int Id_Pessoa { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public Nullable<int> Id_empresa_fk { get; set; }
    
        public virtual Empresa Empresa { get; set; }
    }
}