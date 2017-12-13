using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class EcoEffect
    {
        [Key]
        [ForeignKey("Project")]
        public Guid ProjectId { get; set; }

        public double NetCost1 { get; set; }
        public double NetCost2 { get; set; }
        public double ImplementCost1 { get; set; }
        public double ImplementCost2 { get; set; }


        public virtual Project Project { get; set; }


        public static EcoEffect Create(Guid projectId, double netCost1, double netCost2, double implementCost1,
            double implementCost2)
        {
            if (netCost1 < 0 || netCost2 < 0 || implementCost1 < 0 || implementCost2 < 0)
                throw new ArgumentException("Wrong Input");

            return new EcoEffect
            {
                ProjectId = projectId,
                NetCost1 = netCost1,
                NetCost2 = netCost2,
                ImplementCost1 = implementCost1,
                ImplementCost2 = implementCost2
            };
        }

        public void Update(double netCost1, double netCost2, double implementCost1, double implementCost2)
        {
            if (netCost1 < 0 || netCost2 < 0 || implementCost1 < 0 || implementCost2 < 0)
                throw new ArgumentException("Wrong Input");

            NetCost1 = netCost1;
            NetCost2 = netCost2;
            ImplementCost1 = implementCost1;
            ImplementCost2 = implementCost2;
        }

        public static EcoEffect NullEcoEffect(Guid projectId)
        {
            return new EcoEffect
            {
                ProjectId = projectId
            };
        }

    }
}
