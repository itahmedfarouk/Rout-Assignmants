namespace Demo.PL.ViewModels.DepartmentViewModels
{
    public class DepartmentEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; }
        public DateOnly DateOfCreation { get; set; }
    }
}
