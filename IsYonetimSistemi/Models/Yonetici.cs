//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IsYonetimSistemi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class Yonetici
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Yonetici()
        {
            this.Gorevlendirmes = new HashSet<Gorevlendirme>();
            this.Izins = new HashSet<Izin>();
        }

        public int kullanici_id { get; set; }
        [DisplayName("Kullanici Adi")]
        [Required(ErrorMessage = "Bu alanin doldurulmasi gereklidir.")]
        public string kullanici_adi { get; set; }
        [DisplayName("Parola")]
        [Required(ErrorMessage = "Bu alanin doldurulmasi gereklidir.")]
        public string parola { get; set; }
        [DisplayName("Parolayi Dogrulayin")]
        [DataType(DataType.Password)]
        [Compare("Parola")]
        public string parola_dogrula { get; set; }
        [DisplayName("Ad")]
        [Required(ErrorMessage = "Bu alanin doldurulmasi gereklidir.")]
        public string ad { get; set; }
        [DisplayName("Soyad")]
        [Required(ErrorMessage = "Bu alanin doldurulmasi gereklidir.")]
        public string soyad { get; set; }
        [DisplayName("E-Mail")]
        [Required(ErrorMessage = "Bu alanin doldurulmasi gereklidir.")]
        public string email { get; set; }
        [DisplayName("Maas")]
        [Required(ErrorMessage = "Bu alanin doldurulmasi gereklidir.")]
        public Nullable<int> maas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gorevlendirme> Gorevlendirmes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Izin> Izins { get; set; }
    }
}
