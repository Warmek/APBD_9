using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using APBD_Zadanie_6.Models;
using Microsoft.VisualBasic;

namespace APBD_Zadanie_6.Controllers
{
    public class PrescriptionsController : Controller
    {
        private readonly Context _context;

        public PrescriptionsController(Context context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("api/prescription")]
        public async Task<IActionResult> Create(PatientPrescriptionDTO prescriptionDTO)
        {
            if (_context.Patients.Where(e => e.IdPatient == prescriptionDTO.Patient.IdPatient).Any())
            {
                Patient patient = new Patient() { 
                    IdPatient = prescriptionDTO.Patient.IdPatient,
                    FirstName = prescriptionDTO.Patient.FirstName,
                    LastName = prescriptionDTO.Patient.LastName,
                    BirthDate = prescriptionDTO.Patient.BirthDate,
                };
                _context.Patients.Add(patient);
            };

            var medIds = prescriptionDTO.Medicaments.Select(e => e.IdMedicament);

            if (!_context.Medicaments.Where(e => medIds.Contains(e.IdMedicament)).Any())
            {
                return BadRequest("Lek podany na recepcie nie istnieje");
            };

            if (prescriptionDTO.Medicaments.Count>10)
            {
                return BadRequest("Recepta może obejmować maksymalnie 10 leków");
            };

            if (prescriptionDTO.Date <= prescriptionDTO.DueDate)
            {
                return BadRequest("DueDate nie może być mniejsze od Date");
            }

            int prescriptionId = _context.Prescriptions.Max(e => e.IdPrescription) + 1;

            Prescription prescription = new Prescription()
            {
                Date = prescriptionDTO.Date,
                DueDate = prescriptionDTO.DueDate,
                IdDoctor = prescriptionDTO.DoctorId,
                IdPatient = prescriptionDTO.Patient.IdPatient,
                IdPrescription = prescriptionId
            };

            foreach (MedicamentDTO medicament in prescriptionDTO.Medicaments)
            {
                PrescriptionMedicament prescriptionMedicament = new PrescriptionMedicament()
                {
                    IdMedicament = medicament.IdMedicament,
                    IdPrescription = prescriptionId,
                    Dose = medicament.Dose,
                    Details = medicament.Description,
                };
                _context.PrescriptionMedicaments.Add(prescriptionMedicament);
            }




            _context.Prescriptions.Add(prescription);

            

            return Ok(prescription);
        }

        [HttpGet]
        [Route("api/patient")]
        public async Task<IActionResult> Get(int PatientId)
        {
            Patient patient = _context.Patients.Where(e => e.IdPatient == PatientId).FirstOrDefault();

            return Ok(patient);
        }
    }
}
