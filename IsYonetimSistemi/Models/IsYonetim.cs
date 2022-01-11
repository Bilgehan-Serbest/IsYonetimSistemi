using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IsYonetimSistemi.Models
{
    public class IsYonetim
    {
        public List<Personnel> personnelViewModels { get; set; }
        public Manager managerViewModel { get; set; }
        public Task taskViewModel { get; set; }
        public Leave leaveViewModel { get; set; }
        public List<int> personnelToTaskIdList { get; set; }

        public IsYonetim()
        {
            this.personnelViewModels = new List<Personnel>();
            this.managerViewModel = new Manager();
            this.taskViewModel = new Task();
            this.leaveViewModel = new Leave();
        }
    }    
}