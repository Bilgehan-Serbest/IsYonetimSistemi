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

    public partial class Personel
    {
        public int kullanici_id { get; set; }

        [DisplayName("Kullanici Adi")]
        [Required(ErrorMessage = "Bu alan doldurulmalidir.")]
        public string kullanici_adi { get; set; }

        [DisplayName("Parola")]
        [Required(ErrorMessage = "Bu alan doldurulmalidir.")]
        [DataType(DataType.Password)]
        public string parola { get; set; }

        [DisplayName("Parolayi Dogrulayin")]
        [DataType(DataType.Password)]
        [Compare("parola")]
        public string parola_dogrula { get; set; }

        [DisplayName("Ad")]
        public string ad { get; set; }

        [DisplayName("Soyad")]
        public string soyad { get; set; }

        [DisplayName("E-Mail")]
        public string email { get; set; }

        [DisplayName("Maas")]
        public Nullable<int> maas { get; set; }
    }
}