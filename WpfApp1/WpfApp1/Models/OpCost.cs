using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class OpCost
    {

        [Key]
        [ForeignKey("Project")]
        public Guid ProjectId { get; set; }


        public int EmpCount { get; set; }
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmpTime> EmpTimes { get; set; }


        public int EqCount { get; set; }
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Equip> Equips { get; set; }

        public virtual Project Project { get; set; }

        public static OpCost Create(Guid projectId, int empCount, double[] timeT, double[] timeZ, int eqCount,
            double[] yearNorm, double[] price, double[] count, double[] workTime, double[] power)
        {
            if (empCount < 1 || eqCount < 1 || empCount != timeT.Length || timeT.Length != timeZ.Length ||
                eqCount != yearNorm.Length || yearNorm.Length != price.Length || yearNorm.Length != count.Length ||
                yearNorm.Length != workTime.Length || yearNorm.Length != power.Length)
                throw new ArgumentException("Wrong Input");

            var times = new List<EmpTime>();
            for (int i = 0; i < empCount; i++)
                times.Add(EmpTime.Create(timeT[i], timeZ[i], projectId));

            var equips = new List<Equip>();
            for (int i = 0; i < eqCount; i++)
                equips.Add(Equip.Create(yearNorm[i], price[i], count[i], workTime[i], power[i], projectId));

            return new OpCost
            {
                ProjectId = projectId,
                EmpCount = empCount,
                EmpTimes = times,
                EqCount = eqCount,
                Equips = equips
            };
        }

        public void Update(int empCount, double[] timeT, double[] timeZ, int eqCount, double[] yearNorm, 
            double[] price, double[] count, double[] workTime, double[] power)
        {
            if (empCount < 1 || eqCount < 1 || empCount != timeT.Length || timeT.Length != timeZ.Length ||
                eqCount != yearNorm.Length || yearNorm.Length != price.Length || yearNorm.Length != count.Length ||
                yearNorm.Length != workTime.Length || yearNorm.Length != power.Length)
                throw new ArgumentException("Wrong Input");

            ICollection<EmpTime> times = new List<EmpTime>();
            for (int i = 0; i < empCount; i++)
                times.Add(EmpTime.Create(timeT[i], timeZ[i], ProjectId));
            ICollection<Equip> equips = new List<Equip>();
            for (int i = 0; i < eqCount; i++)
                equips.Add(Equip.Create(yearNorm[i], price[i], count[i], workTime[i], power[i], ProjectId));

            EmpCount = empCount;
            EqCount = eqCount;
            EmpTimes = times;
            Equips = equips;
        }

        public static OpCost NullOpCost(Guid projectId)
        {
            return new OpCost
            {
                ProjectId = projectId,
                EmpTimes = new List<EmpTime>(),
                Equips = new List<Equip>()
            };
        }

    }

    public class EmpTime
    {
        [Key]
        public Guid Id { get; set; }

        public double TimeT { get; set; }
        public double TimeZ { get; set; }


        [ForeignKey("OpCost")]
        public Guid OpCostId { get; set; }
        public virtual OpCost OpCost { get; set; }


        public static EmpTime Create(double timeT, double timeZ, Guid opCostId)
        {
            if (timeT < 20 || timeT > 60 || timeZ < 100 || timeZ > 250)
                throw new ArgumentException("Wrong Input");
            return new EmpTime
            {
                Id = Guid.NewGuid(),
                OpCostId = opCostId,
                TimeT = timeT,
                TimeZ = timeZ
            };
        }
    }

    public class Equip
    {
        [Key]
        public Guid Id { get; set; }

        public double YearNorm { get; set; }
        public double Price { get; set; }
        public double Count { get; set; }
        public double WorkTime { get; set; }
        public double Power { get; set; }

        [ForeignKey("OpCost")]
        public Guid OpCostId { get; set; }
        public virtual OpCost OpCost { get; set; }

        public static Equip Create(double yearNorm, double price, double count, double workTime, double power,
            Guid opCostId)
        {
            if (yearNorm < 0 || price < 0 || count < 0 || workTime < 0 || power < 0)
                throw new ArgumentException("Wrong Input");

            return new Equip
            {
                Id = Guid.NewGuid(),
                OpCostId = opCostId,
                YearNorm = yearNorm,
                Price = price,
                Power = power,
                WorkTime = workTime,
                Count = count
            };
        }

    }
}
