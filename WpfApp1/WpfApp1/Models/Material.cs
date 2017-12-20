using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class Material
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Title { get; set; }
        public string Measure { get; set; }
        public double Count { get; set; }

        public double Price { get; set; }

        
        [ForeignKey("Project")]
        public Guid ProjectId { get; set; } = Models.Project.CurProjectId;
        public virtual Project Project { get; set; }



        public double Total
        {
            get
            {
                return Count * Price;

            }
            set
            {

            }
        }



        public static Material Create(Guid projectId, string title, double price = 1, double count = 1, string measure = "шт.")
        {
            if (count < 1 || price < 0)
                throw new ArgumentException("Wrong input");

            return new Material
            {
                Id = Guid.NewGuid(),
                ProjectId = projectId,
                Title = title,
                Count = count,
                Price = price,
                Measure = measure
            };
        }
    }
}
