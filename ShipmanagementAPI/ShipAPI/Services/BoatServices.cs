using DTOs.Models;

namespace ShipAPI.Services
{
    public class BoatServices
    {
        public static List<BoatInformation> BoatInfomations { get; private set; } = new List<BoatInformation>();

        public BoatServices()
        {
            //BoatInfomations.Add(new BoatInfo());
        }
    }
}
