namespace PIMTool.Dtos
{
    public class GroupDto
    {
        public int Id { get; set; }

        public int Group_Leader_Id { get; set; }
        public IList<int> Projects { get; set; }
    }
}
