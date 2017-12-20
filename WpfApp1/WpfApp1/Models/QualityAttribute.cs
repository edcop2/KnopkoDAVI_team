using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.DataGrid;

namespace WpfApp1.Models
{
    public class QualityAttribute
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Title { get; set; }
        public double Coef { get; set; }

        public int MyValue { get; set; }

        public double MyQu
        {
            get
            {
                return Coef * MyValue;
            }
            set { }

        }

        public static QualityAttribute Create(string title, double coef, int value)
        {
            if (coef < 0 || coef > 1 || value < 1 || value > 5)
                throw new ArgumentException("WrongInput");

            return new QualityAttributeView
            {
                Title = title,
                Coef = coef,
                MyValue = value
            };
        }

        [ForeignKey("Project")]
        public Guid ProjectId { get; set; } = Models.Project.CurProjectId;
        public virtual Project Project { get; set; }

    }

    public class QualityAttributeView : QualityAttribute
    {

        public int OtherValue { get; set; }

        public double OtherQu
        {
            get
            {
                return Coef * OtherValue;
            }
            set { }
        }

        public static QualityAttributeView Create(string titele, double value)
        {
            return new QualityAttributeView
            {
                Title = titele,
                Coef = value
            };
        }


        public static List<QualityAttributeView> DefaultData()
        {
            return new List<QualityAttributeView>
            {
                QualityAttributeView.Create("Удобство использования", 0.14),
                QualityAttributeView.Create("Новизна", 0.1),
                QualityAttributeView.Create("Соответствие профилю деятельности заказчика", 0.2),
                QualityAttributeView.Create("Рекурсивная эффективность", 0.05),
                QualityAttributeView.Create("Надежность", 0.13),
                QualityAttributeView.Create("Скороть доступа к данным", 0.1),
                QualityAttributeView.Create("Гибкость настройки", 0.06),
                QualityAttributeView.Create("Обучаемость персонала", 0.13),
                QualityAttributeView.Create("Соотношение стоимость/возможности", 0.09)
            };
        }
    }
}
