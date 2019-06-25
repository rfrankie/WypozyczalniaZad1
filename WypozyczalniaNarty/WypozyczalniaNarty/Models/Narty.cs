//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WypozyczalniaNarty.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Narty
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Narty()
        {
            this.Transakcjes = new HashSet<Transakcje>();
        }
    
        public int NartaId { get; set; }

        [Required(ErrorMessage = "*")]
        public string NartaProducent { get; set; }

        [Required(ErrorMessage = "*")]
        public string NartaModel { get; set; }
        public decimal NartaCena { get; set; }
        public bool NartaStatus { get; set; }
        public string NartaZdjecie { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transakcje> Transakcjes { get; set; }
    }
}