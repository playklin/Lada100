using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lada100.Models
{
    public class Spisok
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime RepairTime { get; set; } // дата ремонта

        public string data1 { get; set; }

        [Display(Name = "Roman")]
        public string WorkTotal { get; set; } // Общее название работы
        public int PriceTotal { get; set; } // Общая цена
        public string Spare { get; set; } // Название запчасти
        public int SparePrice { get; set; } // Цена на запчасть

        public string Spare1 { get; set; } // Название запчасти
        public int SparePrice1 { get; set; } // Цена на запчасть

        public string Spare2 { get; set; } // Название запчасти
        public int SparePrice2 { get; set; } // Цена на запчасть

        [DefaultValue("замена")]
        public string Work { get; set; } // Названия вида работы по ремонту
        public int WorkPrice { get; set; } // Цена за работу
        public string Img1 { get; set; }
        public int Img2 { get; set; }
    }
}
