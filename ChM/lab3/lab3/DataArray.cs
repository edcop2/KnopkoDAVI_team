using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace lab3
{
    /// <summary>
    /// Клас для представлення двовимірного масиву. Значення елементів 
    /// містяться в об'єкті System.Data.DataTable
    /// </summary>
    public class DataArray
    {
        /// <summary>
        /// Допоміжний клас, який представляє окремий рядок масиву
        /// </summary>
        public class DataArrayRow
        {
            DataTable data;  // посилання на DataTable
            int row;         // індекс рядку масиву

            /// <summary>
            /// Конструктор допоміжного класу
            /// </summary>
            /// <param name="data">посилання на DataTable</param>
            /// <param name="row">індекс рядку масиву</param>
            public DataArrayRow(DataTable data, int row)
            {
                this.row = row;
                this.data = data;
            }

            /// <summary>
            /// Індексатор для доступу до елементу рядка
            /// </summary>
            /// <param name="index">індекс елемента</param>
            /// <returns>елемент масиву</returns>
            public double this[int index]
            {
                get { return double.Parse(data.Rows[row][index] + ""); }
                set { data.Rows[row].SetField<double>(index, value); }
            }
        }

        DataTable data = new DataTable(); // Об'єкт, у якому зберігаються дані
        int m, n;                         // кількість рядків і стовпців

        /// <summary>
        /// Кількість рядків масиву
        /// </summary>
        public int M { get { return m; } }

        /// <summary>
        /// Кількість стовпців масиву
        /// </summary>
        public int N { get { return n; } }

        public DataTable Data
        {
            get { return data; }
        }

        /// <summary>
        /// Індексатор для доступу до рядка
        /// </summary>
        /// <param name="index">індекс рядка</param>
        /// <returns>рядок</returns>
        public DataArrayRow this[int index]
        {
            get { return new DataArrayRow(data, index); }
        }

        /// <summary>
        /// Конструктор, у якому налаштовується об'єкт DataTable
        /// </summary>
        /// <param name="m">кількість рядків</param>
        /// <param name="n">кількість стовпців</param>
        public DataArray(int m, int n)
        {
            this.m = m;
            this.n = n;
            // Додаємо колонки з назвою (номер):
            for (int j = 0; j < n; j++)
                data.Columns.Add((j + 1) + "");
            // Додаємо рядки:
            for (int i = 0; i < m; i++)
                data.Rows.Add();
        }
    }

}
