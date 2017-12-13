using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class DataArray
    {
        /// <summary>
        /// Допоміжний клас, який представляє окремий рядок масиву
        /// </summary>
        public class DataArrayRow
        {
            DataTable data;  
            int row;         
            
            public DataArrayRow(DataTable data, int row)
            {
                this.row = row;
                this.data = data;
            }
            
            public int this[int index]
            {
                get => int.Parse(data.Rows[row][index] + "");
                set => data.Rows[row].SetField(index, value);
            }
        }

        DataTable data = new DataTable(); 
        int m, n;                         
        
        public int M { get { return m; } }
        
        public int N { get { return n; } }

        public DataTable Data
        {
            get { return data; }
        }
        
        public DataArrayRow this[int index]
        {
            get { return new DataArrayRow(data, index); }
        }
        
        public DataArray(int m, int n)
        {
            this.m = m;
            this.n = n;
            for (int j = 0; j < n; j++)
                data.Columns.Add((j + 1) + "");
            for (int i = 0; i < m; i++)
                data.Rows.Add();
        }
    }
}
