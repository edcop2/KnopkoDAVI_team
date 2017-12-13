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
    public class DevCost
    {

        [Key]
        [ForeignKey("Project")]
        public Guid ProjectId { get; set; }

        public double ChiefCost { get; set; }
        public double SlaveCost { get; set; }
        public double MaterialCost { get; set; }
        public double DevTime { get; set; }

        public int EqCount { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CbQj> CbQjs { get; set; }

        public double RoomRecCost { get; set; }
        public double PacketCost { get; set; }
        public double ComLinesCost { get; set; }
        public double InfoDbCost { get; set; }
        public double SlaveTrainingCost { get; set; }


        public virtual Project Project { get; set; }

        public static DevCost Create(Guid projectId, double zdn1, double zdn2, double cm, double tmv, int n,
            double[] cbj, double[] qj, double kzd, double kaa, double kcb, double kib, double kpn)
        {
            if (zdn1 <= 0 || zdn2 <= 0 || cm <= 0 || tmv <= 0 || n <= 0 || cbj.Length != qj.Length || cbj.Length != n
                || kzd <= 0 || kaa <= 0 || kcb <= 0 || kib <= 0 || kpn <= 0)
                throw new ArgumentException("Wrong Input");


            var cbQjs = new List<CbQj>();
            for (int i = 0; i < n; i++)
                cbQjs.Add(CbQj.Create(cbj[i], qj[i], projectId));

            return new DevCost
            {
                ProjectId = projectId,
                ChiefCost = zdn1,
                SlaveCost = zdn2,
                MaterialCost = cm,
                DevTime = tmv,
                EqCount = n,
                CbQjs = cbQjs,
                RoomRecCost = kzd,
                PacketCost = kaa,
                ComLinesCost = kcb,
                InfoDbCost = kib,
                SlaveTrainingCost = kpn
            };
        }

        public void Update(double zdn1, double zdn2, double cm, double tmv, int n,
            double[] cbj, double[] qj, double kzd, double kaa, double kcb, double kib, double kpn)
        {
            if (zdn1 <= 0 || zdn2 <= 0 || cm <= 0 || tmv <= 0 || n <= 0 || cbj.Length != qj.Length || cbj.Length != n
                || kzd <= 0 || kaa <= 0 || kcb <= 0 || kib <= 0 || kpn <= 0)
                throw new ArgumentException("Wrong Input");

            ICollection<CbQj> cbQjs = new List<CbQj>();
            for (int i = 0; i < n; i++)
                cbQjs.Add(CbQj.Create(cbj[i], qj[i], ProjectId));

            ChiefCost = zdn1;
            SlaveCost = zdn2;
            MaterialCost = cm;
            DevTime = tmv;
            EqCount = n;
            CbQjs = cbQjs;
            RoomRecCost = kzd;
            PacketCost = kaa;
            ComLinesCost = kcb;
            InfoDbCost = kib;
            SlaveTrainingCost = kpn;
        }

        public static DevCost NullDevCost(Guid projectId)
        {
            return new DevCost
            {
                ProjectId = projectId,
                CbQjs = new List<CbQj>()
            };
        }
    }

    public class CbQj
    {
        [Key]
        public Guid Id { get; set; }

        public double C { get; set; }
        public double Q { get; set; }

        [ForeignKey("DevCost")]
        public Guid DevCostId { get; set; }

        public virtual DevCost DevCost { get; set; }



        public static CbQj Create(double c, double q, Guid devCostId)
        {
            return new CbQj
            {
                Id = Guid.NewGuid(),
                DevCostId = devCostId,
                C = c,
                Q = q
            };
        }
    }
}
