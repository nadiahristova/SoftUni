namespace SU_Learning_System.Interfaces
{
    public interface IStudent : IPerson
    {
        string StudentNumber { get; set; }

        float? AvgGrade { get; set; }
    }
}
