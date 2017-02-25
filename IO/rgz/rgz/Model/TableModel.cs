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


            for (int i = 0; i <= N + 1; i++)
            {
                for (int j = 0; j <= M + 1; j++)
                {
                    bor = new Border();
                    bor.BorderThickness = new Thickness(1);
                    bor.BorderBrush = Brushes.Black;
                    gr = new Grid();
                    if (i == 0)
                    {
                        if (j != 0 && j != M + 1)
                        {
                            tb = new TextBlock();
                            tb.VerticalAlignment = VerticalAlignment.Center;
                            tb.HorizontalAlignment = HorizontalAlignment.Center;
                            tb.Text = j.ToString();
                            gr.Children.Add(tb);
                            bor.Child = gr;
                        }
                    }
                    else if (j == 0)
                    {
                        if (i != 0 && i!=N+1)
                        {
                            tb = new TextBlock();
                            tb.VerticalAlignment = VerticalAlignment.Center;
                            tb.HorizontalAlignment = HorizontalAlignment.Center;
                            tb.Text = i.ToString();
                            gr.Children.Add(tb);
                            bor.Child = gr;
                        }

                    }
                    else if (i == N + 1)
                    {
                        if (j != M + 1)
                        {
                            tx1 = new TextBox();
                            tx1.VerticalAlignment = VerticalAlignment.Center;
                            tx1.HorizontalAlignment = HorizontalAlignment.Center;
                            tx1.BorderThickness = new Thickness(0);
                            tx1.Text = B[j - 1].ToString();
                            gr.Children.Add(tx1);
                            bor.Child = gr;
                        }

                    }
                    else if (j == M + 1)
                    {

                        tx1 = new TextBox();
                        tx1.VerticalAlignment = VerticalAlignment.Center;
                        tx1.HorizontalAlignment = HorizontalAlignment.Center;
                        tx1.BorderThickness = new Thickness(0);
                        tx1.Text = A[i - 1].ToString();
                        gr.Children.Add(tx1);
                        bor.Child = gr;
                    }
                    else
                    {
                        gr.RowDefinitions.Add(new RowDefinition());
                        gr.RowDefinitions.Add(new RowDefinition());
                        gr.ColumnDefinitions.Add(new ColumnDefinition());
                        gr.ColumnDefinitions.Add(new ColumnDefinition());
                        tx1 = new TextBox();
                        tx1.Text = C[i-1][j-1].ToString();
                        tx1.SetValue(Grid.RowProperty, 0);
                        tx1.SetValue(Grid.ColumnProperty, 1);
                        tx1.BorderThickness = new Thickness(0);
                        tx1.VerticalAlignment = VerticalAlignment.Center;
                        tx1.HorizontalAlignment = HorizontalAlignment.Center;
                        tx2 = new TextBox();
                        tx2.Text = X[i-1][j-1].ToString();
                        tx2.SetValue(Grid.RowProperty, 1);
                        tx2.SetValue(Grid.ColumnProperty, 0);
                        tx2.BorderThickness = new Thickness(0);
                        tx2.VerticalAlignment = VerticalAlignment.Center;
                        tx2.HorizontalAlignment = HorizontalAlignment.Center;
                        gr.Children.Add(tx1);
                        gr.Children.Add(tx2);
                        bor.Child = gr;
                    }
                    bor.SetValue(Grid.RowProperty, i);
                    bor.SetValue(Grid.ColumnProperty, j);
                    Table.Children.Add(bor);
                }
            }            
        }

        public int GetCElemAt(int i, int j)
        {
            int num;
            if (int.TryParse((((Table.Children[j + (M + 2) * i] as Border).Child as Grid).Children[0] as TextBox).Text.ToString(), out num))
                return num;
            else
                return -1;
        }

        public int GetXElemAt(int i, int j)
        {
            int num;
            if (int.TryParse((((Table.Children[j + (M + 2) * i] as Border).Child as Grid).Children[1] as TextBox).Text.ToString(), out num))
                return num;
            else
                return -1;
        }

        public int GetAElemAt(int i)
        {
            return int.Parse((((Table.Children[6+7*i] as Border).Child as Grid).Children[0] as TextBox).Text.ToString());

        }
        public int GetBElemAt(int i)
        {
            return int.Parse((((Table.Children[35+i] as Border).Child as Grid).Children[0] as TextBox).Text.ToString());

        }




    }
}
