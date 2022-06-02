namespace Employees.DataStore.Entities
{
    /// <summary>
    /// Employee entity
    /// </summary>
    public class Employee
    {
        public long   Id          { get; set; }
        public string Surname     { get; set; }
        public string Name        { get; set; }
        public string FathersName { get; set; }
        public string Phone       { get; set; }
        public string Email       { get; set; }
    }
}
