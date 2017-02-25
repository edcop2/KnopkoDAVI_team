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

namespace rgz.Model
{
    public class TableModel
    {

        public int[] A { get; set; }
        public int[] B { get; set; }

        public int[][] C { get; set; }
        public int[][] X { get; set; }

        public int N { get; private set; }
        public int M { get; private set; }

        public InnerCell[][] Cells { get; set; }
        public OuterCell[] East { get; set; }
        public OuterCell[] North { get; set; }
        public OuterCell[] West { get; set; }
        public OuterCell[] South { get; set; }

        public Grid Table { get; set; }

        public TableModel(int n, int m, Grid table)
        {
            Table = table;
            Table.RowDefinitions.Clear();
            Table.ColumnDefinitions.Clear();
            N = n;
            M = m;
            A = new int[N];
            B = new int[M];
            C = new int[N][];
            X = new int[N][];
            Cells = new InnerCell[N][];
            for (int i = 0; i < N; i++)
            {
                Cells[i] = new InnerCell[M];
                C[i] = new int[M];
                X[i] = new int[M];
                Table.RowDefinitions.Add(new RowDefinition());
            }
            Table.RowDefinitions.Add(new RowDefinition());
            Table.RowDefinitions.Add(new RowDefinition());
            for (int i = 0; i < M + 2; i++)
            {
                Table.ColumnDefinitions.Add(new ColumnDefinition());
            }
            East = new OuterCell[N];
            North = new OuterCell[M];
            West = new OuterCell[N];
            South = new OuterCell[M];
        }


        public void UpdateTable()
        {
            Table.Children.Clear();
            Grid gr;
            TextBox tx1, tx2;
            TextBlock tb;
            Border bor;


            /// углы
            for (int i = 0; i < 4; i++)
            {
                bor = new Border();
                bor.BorderThickness = new Thickness(1);
                bor.BorderBrush = Brushes.Black;
                gr = new Grid();
                tb = new TextBlock();
                tb.VerticalAlignment = VerticalAlignment.Center;
                tb.HorizontalAlignment = HorizontalAlignment.Center;
                gr.Children.Add(tb);
                bor.Child = gr;
                if (i == 0)
                {
                    bor.SetValue(Grid.RowProperty, 0);
                    bor.SetValue(Grid.ColumnProperty, 0);
                }
                else if (i==1)
                {
                    bor.SetValue(Grid.RowProperty, N+1);
                    bor.SetValue(Grid.ColumnProperty, 0);
                }
                else if (i == 2)
                {
                    bor.SetValue(Grid.RowProperty, 0);
                    bor.SetValue(Grid.ColumnProperty, M+1);
                }
                else if (i == 3)
                {
                    bor.SetValue(Grid.RowProperty, N+1);
                    bor.SetValue(Grid.ColumnProperty, M+1);
                }
                Table.Children.Add(bor);
            }

            ///границы
            for (int i = 1; i <= M; i++)
            {
                bor = new Border();
                bor.BorderThickness = new Thickness(1);
                bor.BorderBrush = Brushes.Black;
                gr = new Grid();
                tb = new TextBlock();
                tb.Text = i.ToString();
                tb.VerticalAlignment = VerticalAlignment.Center;
                tb.HorizontalAlignment = HorizontalAlignment.Center;
                gr.Children.Add(tb);
                bor.Child = gr;
                bor.SetValue(Grid.RowProperty, 0);
                bor.SetValue(Grid.ColumnProperty, i);
                Table.Children.Add(bor);
            }
            for (int i = 1; i <= N; i++)
            {
                bor = new Border();
                bor.BorderThickness = new Thickness(1);
                bor.BorderBrush = Brushes.Black;
                gr = new Grid();
                tx1 = new TextBox();
                tx1.Text = A[i - 1].ToString();
                tx1.BorderThickness = new Thickness(0);
                tx1.VerticalAlignment = VerticalAlignment.Center;
                tx1.HorizontalAlignment = HorizontalAlignment.Center;
                gr.Children.Add(tx1);
                bor.Child = gr;
                bor.SetValue(Grid.RowProperty, i);
                bor.SetValue(Grid.ColumnProperty, M + 1);
                Table.Children.Add(bor);
            }
            for (int i = 1; i <= N; i++)
            {
                bor = new Border();
                bor.BorderThickness = new Thickness(1);
                bor.BorderBrush = Brushes.Black;
                gr = new Grid();
                tb = new TextBlock();
                tb.Text = i.ToString();
                tb.VerticalAlignment = VerticalAlignment.Center;
                tb.HorizontalAlignment = HorizontalAlignment.Center;
                gr.Children.Add(tb);
                bor.Child = gr;
                bor.SetValue(Grid.RowProperty, i);
                bor.SetValue(Grid.ColumnProperty, 0);
                Table.Children.Add(bor);
            }
            for (int i = 1; i <= M; i++)
            {
                bor = new Border();
                bor.BorderThickness = new Thickness(1);
                bor.BorderBrush = Brushes.Black;
                gr = new Grid();
                tx1 = new TextBox();
                tx1.Text = B[i - 1].ToString();
                tx1.BorderThickness = new Thickness(0);
                tx1.VerticalAlignment = VerticalAlignment.Center;
                tx1.HorizontalAlignment = HorizontalAlignment.Center;
                gr.Children.Add(tx1);
                bor.Child = gr;
                bor.SetValue(Grid.RowProperty, M + 1);
                bor.SetValue(Grid.ColumnProperty, i);
                Table.Children.Add(bor);
            }
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    gr = new Grid();
                    gr.RowDefinitions.Add(new RowDefinition());
                    gr.RowDefinitions.Add(new RowDefinition());
                    gr.ColumnDefinitions.Add(new ColumnDefinition());
                    gr.ColumnDefinitions.Add(new ColumnDefinition());
                    tx1 = new TextBox();
                    tx1.Text = C[i][j].ToString();  
                    tx1.SetValue(Grid.RowProperty, 0);
                    tx1.SetValue(Grid.ColumnProperty, 1);
                    tx1.BorderThickness = new Thickness(0);
                    tx1.VerticalAlignment = VerticalAlignment.Center;
                    tx1.HorizontalAlignment = HorizontalAlignment.Center;
                    tx2 = new TextBox();
                    tx2.Text = X[i][j].ToString();
                    tx2.SetValue(Grid.RowProperty, 1);
                    tx2.SetValue(Grid.ColumnProperty, 0);
                    tx2.BorderThickness = new Thickness(0);
                    tx2.VerticalAlignment = VerticalAlignment.Center;
                    tx2.HorizontalAlignment = HorizontalAlignment.Center;
                    gr.Children.Add(tx1);
                    gr.Children.Add(tx2);
                    bor = new Border();
                    bor.BorderThickness = new Thickness(1);
                    bor.BorderBrush = Brushes.Black;
                    bor.Child = gr;
                    bor.SetValue(Grid.RowProperty, i+1);
                    bor.SetValue(Grid.ColumnProperty, j+1);
                    Table.Children.Add(bor);
                }
            }
        }




    }
}
