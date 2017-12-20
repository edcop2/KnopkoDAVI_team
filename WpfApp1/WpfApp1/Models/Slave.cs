using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class Slave
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Position { get; set; }

        public double Salary { get; set; }

        public int WorkDays { get; set; }

        public int ExploitDays { get; set; }

        
        [ForeignKey("Project")]
        public Guid ProjectId { get; set; } = Models.Project.CurProjectId;
        public virtual Project Project { get; set; }




        public double OneDaySalary
        {
            get
            {
                return Math.Round(Salary / 21.0, 2);
            }
            set
            {

            }
        }


        public double TotalPayment
        {
            get
            {
                return OneDaySalary * WorkDays;
            }
            set
            {

            }
        }

        public double ExploitPayment
        {
            get
            {
                return OneDaySalary * ExploitDays;
            }
            set
            {

            }
        }



        public static Slave Create(Guid projectId, string position, double salary, int workDays)
        {
            if (salary < 0 || workDays < 0)
                throw new ArgumentException("Wrong input");

            return new Slave
            {
                Id = Guid.NewGuid(),
                ProjectId = projectId,
                Position = position,
                Salary = salary,
                WorkDays = workDays
            };
        }
    }
}
