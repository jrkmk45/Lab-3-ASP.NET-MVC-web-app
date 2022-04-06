using Lab3.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3.DBHelper
{
    public static class DataLoader
    {
        private const string doctorsFileName = "Doctors.txt";
        private const string patientsFileName = "Patients.txt";
        private const int patientsFileRowsCount = 6;
        private const int doctorsFileRowsCount = 2;
        public static List<Patient> GetPatients()
        {
            List<Patient> Patients = new List<Patient>();
            string filePath = Directory.GetCurrentDirectory()+"\\App_Data\\"+patientsFileName;

            if (!File.Exists(filePath))
            {
                Trace.TraceError($"No file {filePath}");
                return Patients;
            }
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                string line;
                int lineNumber = 0;
                while ((line = streamReader.ReadLine()) != null)
                {
                    lineNumber++;
                    String[] data = line.Split(" ", System.StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length!=patientsFileRowsCount)
                    {
                        Trace.TraceError($"The numbers of rows in {patientsFileName} is not equal to {patientsFileRowsCount}");
                        continue;
                    }
                    int departamentNumber;
                    if (!int.TryParse(data[3], out departamentNumber))
                    {
                        Trace.TraceError($"Non integer value departamentNumber at line {lineNumber}");
                        departamentNumber = -1;
                    }

                    DateTime? dischargeDate = null;
                    if (data[5] != "-")
                        dischargeDate = ParseDate(data[5], "Discharge Date", lineNumber);

                    DateTime? birthDate = ParseDate(data[4], "Birth date", lineNumber);

                    Patients.Add(new Patient(data[0], data[1], data[2], departamentNumber, birthDate, dischargeDate));
                }
            }
            return Patients.OrderBy(x => x.Surname).ToList<Patient>();
        }
        public static List<Doctor> GetDoctors()
        {
            List<Doctor> Doctors = new List<Doctor>();
            string filePath = Directory.GetCurrentDirectory() + "\\App_Data\\" + doctorsFileName;
            if (!File.Exists(filePath))
            {
                Trace.TraceError($"No file {filePath}");
                return Doctors;
            }
            
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                int lineNumber = 0;
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    lineNumber++;
                    String[] data = line.Split(" ", System.StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length != doctorsFileRowsCount)
                    {
                        Trace.TraceError($"The numbers of rows in {doctorsFileName} is not equal to {doctorsFileRowsCount}");
                        continue;
                    }

                    Doctors.Add(new Doctor(data[0], data[1]));
                }
            }
            return Doctors.OrderBy(x => x.Surname).ToList<Doctor>();
        }
           
        private static DateTime? ParseDate(string date, string dateType, int lineNumber)
        {
            DateTime? parsedDate = null;
            try
            {
                parsedDate = DateTime.Parse(date.Replace(".", "-"));
            } catch (FormatException)
            {
                Trace.WriteLine($"Invalid {dateType} at line {lineNumber}");
            }
            return parsedDate;
        }
       
    }
}
