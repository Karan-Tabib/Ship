using DTOs.Models;

namespace ShipAPI.Services
{
    public static class CUserService
    {
        public static List<User> UserDefinition { get; private set; } = new List<User>();

        static CUserService()
        {
            UserDefinition.Add(new User() {UserId=1, Email = "karan93@gmail.com", Firstname = "karan",Middlename="bhagwan", Lastname = "tabib", Password = "1234" ,Phone="1234567891",Address="Pune"});
        }

    }
}
