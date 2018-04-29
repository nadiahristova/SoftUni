namespace SU_Learning_System.Interfaces
{
    public interface ICurrentStudent : IStudent
    {
        string CurrentCourse { get; set; }
    }
}
