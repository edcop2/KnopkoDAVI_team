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
        #region Fields

        delegate void Method();

        Method meth;

        TableModel tModel;

        private bool calculated;

        public bool AutoBalance
        {
            get
            {
                if (checkBoxBalance.IsChecked.HasValue)
                    return checkBoxBalance.IsChecked.Value;
                else
                    return false;
            }
            set
            {
                checkBoxBalance.IsChecked = value;
            }
        }

        #endregion
        

        public MainWindow()
        {
            InitializeComponent();
            tModel = new TableModel(4, 5, TableGrid);
            tModel.UpdateTable();
            IterationSlider.Maximum = 0;
            IterationSlider.Value = 0;
            radioButton.IsChecked = true;
            AutoBalance = true;
            calculated = false;
        }

        #region Button Events

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            if (tModel.IsClosed() != 0)
            {
                if (AutoBalance)
                    tModel.Balance();
                else
                {
                    MessageBox.Show("Данная задача не закрытая");
                    return;
                }
            }
            tModel.Clear();
            tModel.ReadTable();
            meth();
            IterationSlider.Maximum = tModel.Logs.Count - 1;
            IterationSlider.Value = tModel.Logs.Count - 1;
            calculated = true;
        }

        private void PotButton_Click(object sender, RoutedEventArgs e)
        {
            if (!calculated)
            {
                MessageBox.Show("Сначала найдите базис");
                return;
            }
            tModel.PotMeth();
            IterationSlider.Maximum = tModel.Logs.Count - 1;

        }

        private void BalanceButton_Click(object sender, RoutedEventArgs e)
        {
            tModel.ReadTable();
            if (tModel.IsClosed() != 0)
            {
                tModel.Balance();
            }
            else
            {
                MessageBox.Show("Данная задача и так уже закрытая");
            }
        }

        #endregion

        #region Control Events

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int v = (int)IterationSlider.Value;
            if (v == 0)
            {
                labelIteration.Content = "Начальные данные";
                mainText.Text = tModel.Writter[0];
            }
            else if (v == tModel.Logs.Count - 1)
            {
                labelIteration.Content = "Результат";
                mainText.Text = tModel.Writter.ToString();
            }
            else
            {
                labelIteration.Content = "Итерация #" + v;
                mainText.Text = tModel.Writter[v];
            }
            tModel.ShowHistory(v);
        }

        private void radioButton_Checked(object sender, RoutedEventArgs e)
        {
            meth = tModel.SevenEastAngle;
        }

        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            meth = tModel.MinElemMeth;
        }

        private void radioButton2_Checked(object sender, RoutedEventArgs e)
        {
            meth = tModel.FogelMeth;
        }

        #endregion

        #region Menu Events

        private void menuItemClear_Click(object sender, RoutedEventArgs e)
        {
            tModel.Clear();
            tModel.UpdateTable();
            ClearSlider();
            mainText.Text = "";
        }

        private void menuItemVar1_Click(object sender, RoutedEventArgs e)
        {
            tModel.Clear();
            tModel.ChangeColumnCount(5);
            tModel.ChangeRowCount(4);
            int[] a = { 500, 500, 320, 880 };
            tModel.A = a;
            a = new int[] { 570, 838, 194, 534, 180 };
            tModel.B = a;
            int[,] r = { { 15, 4, 5, 18, 19 }, { 8, 10, 24, 7, 6 }, { 9, 23, 9, 8, 16 }, { 25, 14, 11, 25, 14 } };
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 5; j++)
                    tModel.C[i][j] = r[i, j];
            }
            tModel.UpdateTable();
            ClearSlider();
        }

        private void menuItemVar2_Click(object sender, RoutedEventArgs e)
        {
            tModel.Clear();
            tModel.ChangeColumnCount(5);
            tModel.ChangeRowCount(4);
            int[] a = { 400, 600, 170, 680 };
            tModel.A = a;
            a = new int[] { 848, 250, 202, 280, 120 };
            tModel.B = a;
            int[,] r = { { 7, 23, 5, 11, 13 }, { 23, 8, 15, 19, 14 }, { 10, 0, 18, 17, 15 }, { 21, 26, 17, 11, 21 } };
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 5; j++)
                    tModel.C[i][j] = r[i, j];
            }
            tModel.UpdateTable();
            ClearSlider();
        }

        private void menuItemVar3_Click(object sender, RoutedEventArgs e)
        {
            tModel.Clear();
            tModel.ChangeColumnCount(5);
            tModel.ChangeRowCount(4);
            int[] a = { 800, 920, 200, 280 };
            tModel.A = a;
            a = new int[] { 860, 550, 534, 240, 216 };
            tModel.B = a;
            int[,] r = { { 7, 23, 5, 11, 13 }, { 23, 8, 15, 19, 14 }, { 10, 0, 18, 17, 15 }, { 21, 26, 17, 11, 21 } };
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 5; j++)
                    tModel.C[i][j] = r[i, j];
            }
            tModel.UpdateTable();
            ClearSlider();
        }

        private void menuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void menuItemHelp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Смотри в конспекте");
        }

        private void menuItemAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Примите ргз");
        }

        #endregion

        #region Other Functions

        private void ClearSlider()
        {
            IterationSlider.Maximum = 0;
            IterationSlider.Value = 0;
        }

        #endregion

     
    }
}
