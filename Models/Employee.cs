namespace ManageEmployee.Models
{
    public class Employee
    {
        public int Id { get; set; } 
        public string FullName { get; set; }
        public DateTime DOB {  get; set; }
        public int Age { get; set; }
        public string Ethnicity { get; set; }
        public string Job {  get; set; }
        public string CitizenId { get; set; }
        public string PhoneNumber { get; set; }

    }
}
