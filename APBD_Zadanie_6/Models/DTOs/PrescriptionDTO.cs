using APBD_Zadanie_6.Models;

public class PrescriptionDTO
{
    public int IdPrescriptionId {  get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public ICollection<MedicamentDTO> Medicaments { get; set; }
    public DoctorDTO Doctor { get; set; }

}