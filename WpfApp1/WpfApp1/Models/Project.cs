using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class Project
    {
        [Key]
        public Guid Id { get; set; } = CurProjectId;

        public string ProjetName { get; set; }


        //Implement Costs
        public double BuildRepairCost { get; set; } = 0;
        public double TipicalPacketsCost { get; set; } = 0;
        public double CommunicationLinesCost { get; set; } = 0;
        public double InfoDbCost { get; set; } = 0;
        public double SlaveTrainingCost { get; set; } = 0;

        //Machine Time Costs
        public double Time { get; set; } = 0;
        public double Price { get; set; } = 0;
        public int Multi { get; set; } = 1;


        // Other
        public double ElectroTarif { get; set; } = 0.57;
        public double AddPayment { get; set; } = 0.4;
        public double SocPayment { get; set; } = 0.22;

        //CapitalData
        public double DevCost { get; set; } = 0;
        public double ImpCost { get; set; } = 0;
        public double CapCost { get; set; } = 0;
        public double ExploitCost { get; set; } = 0;

        
        public double ProjectTechLevel { get; set; }

        
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QualityAttribute> QualityAttributes { get; set; }



        public double OneCost => ExploitCost + 0.33 * CapCost;

        public static Project Create(string projectName)
        {
            return new Project
            {
                Id = Guid.NewGuid(),
                ProjetName = projectName
            };
        }


        public override string ToString()
        {
            return ProjetName;
        }

        public static Guid CurProjectId { get; set; } = Guid.NewGuid();

    }
}
