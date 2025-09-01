using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    public class FishInformation
    {
        public int FishId { get; set; }
        public required string FishName { get; set;}

        public FishInformation()
        {
             
        }
    }
}
