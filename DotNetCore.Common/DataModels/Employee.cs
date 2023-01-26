namespace DotNetCore.Common.DataModels
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public bool IsActive { get; set; }
    }
}
