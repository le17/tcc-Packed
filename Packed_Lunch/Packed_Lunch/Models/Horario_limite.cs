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
    
    public partial class Horario_limite
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Horario_limite()
        {
            this.Restaurantes = new HashSet<Restaurante>();
        }
    
        public int Id_Horario { get; set; }
        public Nullable<System.TimeSpan> hora_Limite { get; set; }
        public Nullable<System.TimeSpan> hora_entrega { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Restaurante> Restaurantes { get; set; }
    }
}
