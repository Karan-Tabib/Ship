namespace ShipAPI.Models
{
    public class Student
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Address {  get; set; }
        public required DateTime AdmissionDate { get; set; }
        public Student()
        {
            
        }
        public Student(int id, string name, string address,DateTime date)
        {
            this.Id = id;
            this.Name = name;
            this.Address = address;
            this.AdmissionDate = date;
        }
    }
}
