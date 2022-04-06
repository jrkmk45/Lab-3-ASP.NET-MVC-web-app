using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3.Models
{
    public class Patient
    {

        public String Surname { get; set; }
        public String Diagnosis { get; set; }
        public String DoctorsSurname { get; set; }

        public DateTime? BirthDate { get; set; }
        public DateTime? DischargeDate { get; set; }

        public int DepartamentNumber { get; set; }


        internal Patient(String Surname, String Diagnosis, String DoctorsSurname, int DepartamentNumber, DateTime? BirthDate, DateTime? DischargeDate)
        {
            this.Surname = Surname;
            this.Diagnosis = Diagnosis;
            this.DoctorsSurname = DoctorsSurname;
            this.BirthDate = BirthDate;
            this.DepartamentNumber = DepartamentNumber;
            this.DischargeDate = DischargeDate;
        }
    }
}
