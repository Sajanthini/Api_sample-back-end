namespace API_Sample.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public String EmployeeName { get; set; } = default!; 
        public string Department { get; set; } = default!;
        public string DateofJoining { get; set; } = default!;

        public string PhotoFileName { get; set; } = default!;

    }
}
