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
        public Guid Id { get; set; }

        public string ProjetName { get; set; }


        public double? Competitivness { get; set; }

        public double? DevCosts { get; set; }

        public double? OpCosts { get; set; }

        public double? EcoEffects { get; set; }




        public virtual ExpertEval ExpertEval { get; set; }

        public virtual DevCost DevCost { get; set; }

        public virtual OpCost OpCost { get; set; }

        public virtual EcoEffect EcoEffect { get; set; }



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
    }
}
