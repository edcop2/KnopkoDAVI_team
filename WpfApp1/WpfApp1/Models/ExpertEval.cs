using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace WpfApp1.Models
{
    [Table("ExpertEval")]
    public class ExpertEval
    {
        [Key]
        [ForeignKey("Project")]
        public Guid ProjectId { get; set; }

        
        public virtual Project Project { get; set; }


        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Eval> Evals { get; set; }


        public static ExpertEval Create(Guid projectId, params int[] vals)
        {
            if (vals.Length < 1)
            {
                return new ExpertEval
                {
                    ProjectId = projectId,
                    Evals = Eval.DefaultEvals(projectId)
                };
            }
            return new ExpertEval
            {
                ProjectId = projectId,
                Evals = vals.Select(e => Eval.Create(e, projectId)).ToList()
            };
        }
        public bool Any() => Evals.Any();
    }


    [Table("Eval")]
    public class Eval
    {
        [Key]
        public Guid Id { get; set; }

        [Range(1, 5)]
        [DefaultValue(1)]
        public int Val { get; set; }

        [ForeignKey("ExpertEval")]
        public Guid ExpertEvalId { get; set; }

        public virtual ExpertEval ExpertEval { get; set; }

        private Eval() { }

        public static Eval Create(int val, Guid expId)
        {
            if (val <= 0 || val > 5)
                throw new ArgumentOutOfRangeException(nameof(val));

            return new Eval
            {
                Id = Guid.NewGuid(),
                Val = val,
                ExpertEvalId = expId
            };
        }

        public static Eval[] DefaultEvals(Guid expId)
        {
            var evals = new Eval[9];
            for (int i = 0; i < 9; i++)
                evals[i] = Create(1, expId);
            return evals;
        }
    }
}
