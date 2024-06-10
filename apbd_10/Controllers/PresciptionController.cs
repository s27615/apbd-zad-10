
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apbd_10.DTOs;
using apbd10.Context;
using apbd10.Models;

namespace apbd_10.Controllers
{
    [ApiController]
    [Route("api")]
    public class PrescriptionController : ControllerBase
    {
        private readonly Apbd10Context _context;

        public PrescriptionController(Apbd10Context context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("prescriptions")]
        public async Task<IActionResult> AddPrescription([FromBody] PrescriptionRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request");
            }

            var patient = request.Patient;
            var medicaments = request.Medicaments;
            var date = request.Date;
            var dueDate = request.DueDate;
            var doctorId = request.DoctorId;

            var existingPatient = await _context.Patients.FirstOrDefaultAsync(p =>
                p.IdPatient == patient.IdPatient &&
                p.FirstName == patient.FirstName &&
                p.LastName == patient.LastName &&
                p.Birthdate == patient.Birthdate);

            if (existingPatient == null)
            {
                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();
            }

            var existingDoctor = await _context.Students.FirstOrDefaultAsync(d => d.IdDoctor == doctorId);
            if (existingDoctor == null)
            {
                return BadRequest("No doctor with given Id exists");
            }

            var medicamentIds = _context.Medicaments.Select(e => e.IdMedicament);
            var missingMedicaments = medicaments.Where(e => !medicamentIds.Contains(e.IdMedicament)).ToList();
            if (missingMedicaments.Any())
            {
                return BadRequest($"One or more medicaments are not in the database: {string.Join(",", missingMedicaments.Select(m => m.IdMedicament))}");
            }

            if (medicaments.Count() > 10)
            {
                return BadRequest("Maximum of 10 medicaments allowed per prescription");
            }

            var prescription = new Prescription
            {
                IdPatient = existingPatient?.IdPatient ?? patient.IdPatient,
                IdDoctor = existingDoctor.IdDoctor,
                Date = date,
                DueDate = dueDate
            };

            _context.Prescriptions.Add(prescription);

            foreach (var medicament in medicaments)
            {
                _context.PrescriptionMedicaments.Add(new Prescription_Medicament
                {
                    IdMedicament = medicament.IdMedicament,
                    IdPrescription = prescription.IdPrescription,
                    Dose = medicament.Dose,
                    Details = medicament.Descritpion
                });
            }

            await _context.SaveChangesAsync();

            return StatusCode(201, "Object created successfully");
        }
    }
}

namespace apbd_10.DTOs
{
    public class PrescriptionRequest
    {
        public Patient Patient { get; set; }
        public IEnumerable<PrescribedMedicament> Medicaments { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public int DoctorId { get; set; }
    }
}

