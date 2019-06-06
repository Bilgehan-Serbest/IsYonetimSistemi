using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IsYonetimSistemi.Models
{
    public class IsYonetim
    {
        public List<Personel> personelViewModels { get; set; }
        public Yonetici yoneticiViewModel { get; set; }
        public Gorevlendirme gorevlendirmeViewModel { get; set; }
        public Izin izinViewModel { get; set; }
        public List<int> gorevlendirilecekPersonelIdList { get; set; }

        public IsYonetim()
        {
            this.personelViewModels = new List<Personel>();
            this.yoneticiViewModel = new Yonetici();
            this.gorevlendirmeViewModel = new Gorevlendirme();
            this.izinViewModel = new Izin();
        }
    }    
}