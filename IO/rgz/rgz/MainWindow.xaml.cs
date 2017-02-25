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
            tModel.UpdateTable();            
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            string s = "";
            // MessageBox.Show(((TableGrid.Children[8] as Border).Child as Grid).Children[0].ToString());
            MessageBox.Show(tModel.GetBElemAt(3).ToString());

        }
    }
}
