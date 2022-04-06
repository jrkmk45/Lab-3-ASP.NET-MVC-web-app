using Lab3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3.DataProvider
{

    public static class HospitalData
    {

        public static List<Doctor> Doctors { get; set; }
        public static List<Patient> Patients { get; set; }
    }
}
