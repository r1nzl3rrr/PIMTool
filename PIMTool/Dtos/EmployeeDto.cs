namespace PIMTool.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string? Visa { get; set; }
        public string First_Name { get; set; } = null!;
        public string Last_Name { get; set; } = null!;
        public DateTime Birth_Date { get; set; }
    }
}
