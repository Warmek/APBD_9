using APBD_Zadanie_6.Models;

public class PatientPrescriptionDTO
{
    public PatientDTO Patient { get; set; }
    public ICollection<MedicamentDTO> Medicaments { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public int DoctorId { get; set; }
}