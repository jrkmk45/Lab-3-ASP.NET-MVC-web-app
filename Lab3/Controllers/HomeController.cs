using Lab3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Lab3.DataProvider;
using System.Dynamic;
using System.Text;

namespace Lab3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            dynamic viewsData = new ExpandoObject();

            viewsData.Patients = HospitalData.Patients;
            viewsData.Doctors = HospitalData.Doctors;
            viewsData.DoctorsSpecializations = HospitalData.Doctors.Select(x=>x.Specialization).Distinct();
            viewsData.DepartamentNumbers = HospitalData.Patients.Select(x => x.DepartamentNumber).Where(x=>x>0).Distinct().OrderBy(x => x);

            return View(viewsData);
        }

        [HttpGet]
        public IActionResult Task1(string specialization)
        {
            dynamic viewsData = new ExpandoObject();

            HashSet<string> diagnoses = new HashSet<string>();
            HashSet<string> doctors = new HashSet<string>();
            foreach (var doctor in HospitalData.Doctors)
            {
                if (doctor.Specialization == specialization)
                    doctors.Add(doctor.Surname);
            }
            foreach (var patient in HospitalData.Patients)
            {
                if (doctors.Contains(patient.DoctorsSurname))
                    diagnoses.Add(patient.Diagnosis);
            }
            viewsData.result = diagnoses.Count;
            viewsData.specialization = specialization;
            return View(viewsData);
        }

        [HttpGet]
        public IActionResult Task2(int requiredDepartamentNumber)
        {
            dynamic viewsData = new ExpandoObject();

            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (var patient in HospitalData.Patients)
            {
                if (patient.DepartamentNumber == requiredDepartamentNumber && patient.DischargeDate == null)
                    if (dict.ContainsKey(patient.DoctorsSurname))
                        dict[patient.DoctorsSurname] += 1;
                    else dict[patient.DoctorsSurname] = 1;
            }
            int max = dict.Max(x=>x.Value);
            StringBuilder doctorsSurnames = new StringBuilder();
            foreach (var pair in dict)
            {
                if (pair.Value == max)
                    doctorsSurnames.Append(pair.Key).Append(", ");
            }
            viewsData.doctorsSurnames = doctorsSurnames.ToString().Remove(doctorsSurnames.Length - 2);
            viewsData.departamentNumber = requiredDepartamentNumber;

            return View(viewsData);
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
