using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apbd10.Models;

[PrimaryKey(nameof(IdMedicament), nameof(IdPrescription))]
public class Prescription_Medicament
{
    public int IdMedicament { get; set; }
    [ForeignKey(nameof(IdMedicament))]
    public int IdPrescription { get; set; }
    [ForeignKey(nameof(IdPrescription))]
    public int Dose { get; set; }
    [MaxLength(100)]
    public string Details { get; set; }
}