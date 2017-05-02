using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataArray _a;

        public MainWindow()
        {
            InitializeComponent();
            InitTable(3);
            radioButtonK.IsChecked = true;

        }

        private void InitTable(int n)
        {
            _a = new DataArray(n, n);
            dataGridMatrix.ItemsSource = _a.Data.DefaultView;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    _a[i][j] = 0;
            }
        }

        private void buttonCalc_Click(object sender, RoutedEventArgs e)
        {
            if (radioButtonK.IsChecked.Value)
            {
                Krilov kri = new Krilov();
                kri.SetAB(_a);
                kri.Calculate();

                string s = "";
                s += kri.GetKhaEqu();
                KhaEqu ke = new KhaEqu();
                ke.SetAB(_a);
                ke.P = kri.Res;
                ke.Calculate();   
                s += string.Format("\nСобственные числа:\n λ1 = {0}\n λ2 = {1}\n λ3 = {2}", ke.Lambda[0], ke.Lambda[1], ke.Lambda[2]);
                ResText.Text = s;
                s = "";
                DataArray ad = new DataArray(3, 3);
                dataGridOwn.ItemsSource = ad.Data.DefaultView;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                        ad[i][j] = Math.Round(ke.OwnMatrix[i][j], 2);
                }
            }
            else
            {
                Levere lev = new Levere();
                lev.SetAB(_a);
                lev.Calculate();
                string s = "";
                s += lev.GetKhaEqu();


                KhaEqu ke = new KhaEqu();
                ke.SetAB(_a);
                ke.P = lev.Res.ToArray();
                ke.Calculate();
                s += string.Format("\nСобственные числа:\n λ1 = {0}\n λ2 = {1}\n λ3 = {2}", ke.Lambda[0], ke.Lambda[1], ke.Lambda[2]);
                ResText.Text = s;
                s = "";
                DataArray ad = new DataArray(3, 3);
                dataGridOwn.ItemsSource = ad.Data.DefaultView;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                        ad[i][j] = Math.Round(ke.OwnMatrix[i][j], 2);
                }
            }

        }

        private void label1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int[,] a = new int[3, 3] { { -2, 4, 3 }, { 1, 5, 1 }, { 2, -4, -1 } };
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                    _a[i][j] = a[i, j];
            }
        }

        private void label4_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int[,] a = new int[3, 3] { { 7, 3, 5 }, { 3, -2, 1 }, { -1, 0, 4 } };
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                    _a[i][j] = a[i, j];
            }
        }
    }
}
