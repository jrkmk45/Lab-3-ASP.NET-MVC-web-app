using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3.Models
{
    public class Doctor
    {
        public String Surname { set; get; }
        public String Specialization { set; get; }

        internal Doctor(String Surname, String Specialization)
        {
            this.Surname = Surname;
            this.Specialization = Specialization;
        }
    }
}
