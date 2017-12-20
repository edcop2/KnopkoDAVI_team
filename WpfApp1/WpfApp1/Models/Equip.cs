using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{

    public class Equip
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Title { get; set; }

        public double Price { get; set; }
        public int Count { get; set; }

        public double DayNorm { get; set; }


        public double WorkTime { get; set; }

        public double AmoKoef { get; set; }

        public double ElectroPower { get; set; }

        public double RepairNorm { get; set; }

        public double ElectroTarif { get; set; } = 0.57;

        public double EffectFond
        {
            get
            {
                return DayNorm * 251;
            }
            set { }
        }


        public double LoadCoef
        {
            get
            {
                return Math.Round(WorkTime / EffectFond, 2);

            }
            set { }
        }

        public double Total
        {
            get
            {
                return Math.Round(Count * Price * LoadCoef, 2);
            }
            set { }
        }

        public double TotalAmo
        {
            get
            {
                return Math.Round(Total * AmoKoef, 2);
            }
            set { }
        }

        public double TotalElectro
        {
            get
            {
                return Math.Round(ElectroPower * WorkTime * ElectroTarif, 2);

            }
            set { }
        }

        public double TotalRepair
        {
            get
            {
                return Math.Round(RepairNorm * Total, 2);
            }
            set { }
        }

        [ForeignKey("Project")]
        public Guid ProjectId { get; set; } = Models.Project.CurProjectId;

        public virtual Project Project { get; set; }

        public static Equip Create(Guid projectId, string title, double price, double dayNorm, double workTime, double amoKoef, double elctroPowe, double repairNorm, double electroTarif = 0.57, int count = 1)
        {
            if (price < 0 || count < 1 || dayNorm < 0 || workTime < 0 || amoKoef < 0 || electroTarif < 0 || elctroPowe < 0 || repairNorm < 0)
                throw new ArgumentException("Wrong Input");

            return new Equip
            {
                Id = Guid.NewGuid(),
                ProjectId = projectId,
                Title = title,
                Price = price,
                Count = count,
                DayNorm = dayNorm,
                WorkTime = workTime,
                AmoKoef = amoKoef,
                ElectroPower = elctroPowe,
                RepairNorm = repairNorm,
                ElectroTarif = electroTarif
            };
        }


    }
}
