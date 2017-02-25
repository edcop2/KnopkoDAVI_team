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

    public class Memento
    {
        public int[][]C { get; set; }
        
        public string[][]X { get; set; }

        public bool[][]Path { get; set; }

        public bool[] ComitRow { get; set; }

        public bool[] ComitColumn { get; set; }

        public bool Final { get; set; }
    }


    public class TableModel
    {
        public int[] A { get; set; }
        public int[] B { get; set; }

        public int[][] C { get; set; }
        public string[][] X { get; set; }

        public int N { get; private set; }
        public int M { get; private set; }

        public bool[][] Path { get; private set; }
        public bool[] ComitRow { get; private set; }
        public bool[] ComitColumn { get; private set; }

        public Grid Table { get; set; }

        public List<Memento> Logs { get; private set; }

        private bool Final { get; set; }


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
            X = new string[N][];
            ComitColumn = new bool[M];
            ComitRow = new bool[N];
            Path = new bool[N][];
            Logs = new List<Memento>();
            for (int i = 0; i < N; i++)
            {
                C[i] = new int[M];
                X[i] = new string[M];
                Path[i] = new bool[M];
                Table.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                    X[i][j] = "";
            }
            Table.RowDefinitions.Add(new RowDefinition());
            Table.RowDefinitions.Add(new RowDefinition());
            for (int i = 0; i < M + 2; i++)
            {
                Table.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }


        public void ChangeRowCount(int n)
        {
            if (n <= 0)
                return;
            if (N == n)
                return;
            Table.RowDefinitions.Clear();
            for (int i = 0; i < n + 2; i++)
            {
                Table.RowDefinitions.Add(new RowDefinition());
            }
            int[] a = A;
            Array.Resize(ref a, n);
            A = a;
            int[][] c = C;
            Array.Resize(ref c, n);
            C = c;
            string[][] x = X;
            Array.Resize(ref x, n);
            bool[][] p = Path;
            Array.Resize(ref p, n);
            Path = p;
            X = x;
            if (N < n)
            {
                for (int i = N; i < n; i++)
                {
                    X[i] = new string[M];
                    C[i] = new int[M];
                    Path[i] = new bool[M];
                    for (int j = 0; j < M; j++)
                        X[i][j] = "";
                }
            }
            N = n;
            UpdateTable();
        }

        public void ChangeColumnCount(int m)
        {
            if (m <= 0)
                return;
            if (M == m)
                return;
            Table.ColumnDefinitions.Clear();
            for (int i = 0; i < m + 2; i++)
            {
                Table.ColumnDefinitions.Add(new ColumnDefinition());
            }
            int[] b = B;
            Array.Resize(ref b, m);
            B = b;
            int[][] c = C;
            string[][] x = X;
            bool[][] p = Path;
            for (int i = 0; i < N; i++)
            {
                Array.Resize(ref c[i], m);
                Array.Resize(ref x[i], m);
                Array.Resize(ref p[i], m);
            }
            C = c;
            X = x;
            Path = p;
            if (M < m)
            {
                for (int i = 0; i < N; i++)
                {
                    for (int j = M; j < m; j++)
                        X[i][j] = "";
                }
            }
            M = m;
            UpdateTable();
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
                        if (i != 0 && i != N + 1)
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
                        tx1.Text = C[i - 1][j - 1].ToString();
                        tx1.SetValue(Grid.RowProperty, 0);
                        tx1.SetValue(Grid.ColumnProperty, 1);
                        tx1.BorderThickness = new Thickness(0);
                        tx1.VerticalAlignment = VerticalAlignment.Center;
                        tx1.HorizontalAlignment = HorizontalAlignment.Center;
                        tx2 = new TextBox();
                        tx2.Text = X[i - 1][j - 1].ToString();
                        tx2.SetValue(Grid.RowProperty, 1);
                        tx2.SetValue(Grid.ColumnProperty, 0);
                        tx2.BorderThickness = new Thickness(0);
                        tx2.VerticalAlignment = VerticalAlignment.Center;
                        tx2.HorizontalAlignment = HorizontalAlignment.Center;
                        if ((ComitRow[i=1] || ComitColumn[j-1] )&& !Final)
                        {
                            gr.Background = Brushes.Gray;
                            tx1.Background = Brushes.Gray;
                            tx2.Background = Brushes.Gray;
                        }
                        if (Path[i - 1][j - 1])
                        {
                            gr.Background = Brushes.Azure;
                            tx1.Background = Brushes.Azure;
                            tx2.Background = Brushes.Azure;
                        }
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
            return int.Parse((((Table.Children[j + (M + 2) * i] as Border).Child as Grid).Children[0] as TextBox).Text.ToString());
        }

        public string GetXElemAt(int i, int j)
        {
            return (((Table.Children[j + (M + 2) * i] as Border).Child as Grid).Children[1] as TextBox).Text.ToString();
        }

        public int GetAElemAt(int i)
        {
            return int.Parse((((Table.Children[6 + 7 * i] as Border).Child as Grid).Children[0] as TextBox).Text.ToString());

        }
        public int GetBElemAt(int i)
        {
            return int.Parse((((Table.Children[35 + i] as Border).Child as Grid).Children[0] as TextBox).Text.ToString());
        }

        public Grid GetGridElemAt(int i, int j)
        {
            return (Table.Children[j + (M + 2) * i] as Border).Child as Grid;
        }

        public bool ReadTable()
        {
            try
            {
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < M; j++)
                    {
                        C[i][j] = GetCElemAt(i + 1, j + 1);
                        X[i][j] = GetXElemAt(i + 1, j + 1);
                    }
                    A[i] = GetAElemAt(i + 1);
                }
                for (int i = 0; i < M; i++)
                {
                    B[i] = GetBElemAt(i + 1);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Balance()
        {
            int isClosed = IsClosed();
            if (isClosed == 2)
            {
                ChangeRowCount(N + 1);
                A[N - 1] = B.Sum() - A.Sum();
                UpdateTable();
            }
            else if (isClosed == 2)
            {
                ChangeColumnCount(M + 1);
                B[M - 1] = A.Sum() - B.Sum();
                UpdateTable();
            }

        }

        public int IsClosed()
        {
            int sum1 = A.Sum();
            int sum2 = B.Sum();
            return sum1 > sum2 ? 0 : sum1 < sum2 ? 2 : 0;
        }

        public void SevenEastAngle()
        {
            Logs.Clear();
            int i = 0, j = 0;
            int[] a = new int[N];
            int[] b = new int[M];
            Array.Copy(A, a, N);
            Array.Copy(B, b, M);
            int temp;
            Grid gr;
            Memento meme;
            for (;;)
            {
                gr = new Grid();
                Path[i][j] = true;
                if (a[i] > b[j])
                {
                    ComitColumn[j] = true;
                    temp = b[j];
                    a[i] -= b[j];
                    X[i][j] = temp.ToString();
                    j++;
                }
                else
                {
                    ComitRow[i] = true;
                    temp = a[i];
                    b[j] -= a[i];
                    X[i][j] = temp.ToString();
                    i++;
                }
               meme = new Memento() { C = C, X = X, Path = Path, ComitRow = ComitRow, ComitColumn = ComitColumn, Final=Final };
                Logs.Add(meme);
                if (i == N || j == M)
                    break;
               // MessageBox.Show(uie.Length+"   "+ gr.Children.Count + "   " + Table.Children.Count+"     "+k);
       //         Logs.Add(gr);
            }
            UpdateTable();
        }

        public void ShowHistory(int i)
        {
            if (i < 0 || i > Logs.Count)
                return;
            ReadMeme(Logs[i]);
            UpdateTable();
               
        }

        private void ReadMeme(Memento meme)
        {
            X = meme.X;
            Path = meme.Path;
            C = meme.C;
            ComitRow = meme.ComitRow;
            ComitColumn = meme.ComitColumn;
            Final = Final;
        }

    }
}
