namespace PIMTool.AddingAndUpdatingDtos
{
    public class AddingAndUpdatingEmployeeDto
    {
        public string? Visa { get; set; }
        public string First_Name { get; set; } = null!;
        public string Last_Name { get; set; } = null!;
        public DateTime Birth_Date { get; set; }
    }
}
