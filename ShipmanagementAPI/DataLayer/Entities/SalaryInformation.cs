using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    public class SalaryInformation
    {
        public int Id { get; set; }

        public Decimal TotalSalary { get; set; }
        public int ForYear { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }

        // Navigation properties

        #region Crew Member to SalaryInformation(one-to-one)
        /* Crew Member to SalaryInformation(one-to-one)
         * A Single crew menber can have only one Salary record.
         * Each salary record is associated with single Crew Member
         * This is classic one-to-one relationship.
         */
        [ForeignKey("CrewId")]
        public int CrewId { get; set; }
        public CrewInformation? CrewInfos { get; set; }  // This will hold the related Crew Member info
        #endregion


        public SalaryInformation()
        {

        }

    }
}
