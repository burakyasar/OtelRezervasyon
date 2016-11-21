using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvcotel.Models
{
    public class Galeri
    {
        [Key]
        public int ResimID { get; set; }
        public string Baslik { get; set; }
        public string ResimYolu { get; set; }

    }
}
