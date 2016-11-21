using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvcotel.Models
{
   public class RezervasyonM
    {
        [Key]
        public int RezervasyonMID { get; set; }
        public string UserID { get; set; }
        public int OdaID { get; set; }
        public DateTime GirisTarih { get; set; }
        public DateTime CikisTarih { get; set; }
        public decimal Fiyat { get; set; }
        public int KacKisi { get; set; }
    }
}
