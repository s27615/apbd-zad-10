using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.EntityFrameworkCore;

namespace apbd10.Models;

[PrimaryKey(nameof(IdPatient), nameof(IdDoctor))]
public class Prescription
{
    [Key]
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    [ForeignKey(nameof(IdPatient))]
    public int IdPatient { get; set; }
    [ForeignKey(nameof(IdDoctor))]
    public int IdDoctor { get; set; }
    
}