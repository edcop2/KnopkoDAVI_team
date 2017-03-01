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
using rgz.Model;


namespace rgz
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        TableModel tModel;


        public MainWindow()
        {
            InitializeComponent();
            tModel = new TableModel(4, 5, TableGrid);
            int[] a = { 500, 500, 320, 880 };
            tModel.A = a;
            a = new int[] { 570, 838, 194, 534, 180 };
            tModel.B = a;
            int[,] r = { { 15, 4, 5, 18, 19 }, { 8, 10, 24, 7, 6 }, { 9, 23, 9, 8, 16 }, { 25, 14, 11, 25, 14 } };
            for (int i=0; i<4; i++)
            {
                for (int j = 0; j < 5; j++)
                    tModel.C[i][j] = r[i, j];
            }
            tModel.UpdateTable();
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            tModel.Balance();
            tModel.SevenEastAngle();
            IterationSlider.Maximum = tModel.Logs.Count-1;
            tModel.PotMeth();
            //MessageBox.Show(tModel.IsClosed().ToString());
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tModel.ShowHistory((int)IterationSlider.Value);
        }
    }
}
