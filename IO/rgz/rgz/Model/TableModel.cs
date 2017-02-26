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
        public string[][] X { get; set; }
        public string[][] Delt { get; set; }

        public int N { get; private set; }
        public int M { get; private set; }

        public string[] North { get; set; }
        public string[] West { get; set; }

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
            Delt = new string[N][];
            North = new string[M];
            West = new string[N];
            ComitColumn = new bool[M];
            ComitRow = new bool[N];
            Path = new bool[N][];
            Logs = new List<Memento>();
            for (int i = 0; i < N; i++)
            {
                C[i] = new int[M];
                X[i] = new string[M];
                Delt[i] = new string[M];
                Path[i] = new bool[M];
                Table.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    X[i][j] = "";
                    Delt[i][j] = "";
                }
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
            bool[] com = ComitRow;
            Array.Resize(ref com, n);
            ComitRow = com;
            Path = p;
            X = x;
            x = Delt;
            Array.Resize(ref x, n);
            Delt = x;
            string[] nw = West;
            Array.Resize(ref nw, n);
            West = nw;
            if (N < n)
            {
                for (int i = N; i < n; i++)
                {
                    X[i] = new string[M];
                    Delt[i] = new string[M];
                    C[i] = new int[M];
                    Path[i] = new bool[M];
                    for (int j = 0; j < M; j++)
                    {
                        X[i][j] = "";
                        Delt[i][j] = "";
                    }
                    North[i] = "";
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
            string[][] delt = Delt;
            bool[][] p = Path;
            bool[] com = ComitColumn;
            Array.Resize(ref com, m);
            ComitColumn = com;
            string[] nw = North;
            Array.Resize(ref nw, m);
            North = nw;
            for (int i = 0; i < N; i++)
            {
                Array.Resize(ref c[i], m);
                Array.Resize(ref x[i], m);
                Array.Resize(ref p[i], m);
                Array.Resize(ref Delt[i], m);
            }
            C = c;
            X = x;
            Path = p;
            if (M < m)
            {
                for (int i = 0; i < N; i++)
                {
                    for (int j = M; j < m; j++)
                    {
                        X[i][j] = "";
                        Delt[i][j] = "";
                    }
                }
                for (int i = M; i < m; i++)
                {
                    North[i] = "";
                }
            }
            M = m;
            UpdateTable();
        }


        public void UpdateTable()
        {
            Table.Children.Clear();
            Grid gr;
            TextBox tx1, tx2, tx3;
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
                            gr.RowDefinitions.Add(new RowDefinition());
                            gr.RowDefinitions.Add(new RowDefinition());
                            gr.ColumnDefinitions.Add(new ColumnDefinition());
                            gr.ColumnDefinitions.Add(new ColumnDefinition());
                            tx1 = new TextBox();
                            tx1.VerticalAlignment = VerticalAlignment.Center;
                            tx1.HorizontalAlignment = HorizontalAlignment.Center;
                            tx1.SetValue(Grid.RowProperty, 0);
                            tx1.SetValue(Grid.ColumnProperty, 1);
                            tx1.BorderThickness = new Thickness(0);
                            tx1.Text = j.ToString();
                            tx2 = new TextBox();
                            tx2.Text = North[j - 1];
                            tx2.SetValue(Grid.RowProperty, 1);
                            tx2.SetValue(Grid.ColumnProperty, 0);
                            tx2.Foreground = Brushes.Blue;
                            tx2.BorderThickness = new Thickness(0);
                            tx2.VerticalAlignment = VerticalAlignment.Center;
                            tx2.HorizontalAlignment = HorizontalAlignment.Center;
                            gr.Children.Add(tx1);
                            gr.Children.Add(tx2);
                            bor.Child = gr;
                        }
                    }
                    else if (j == 0)
                    {
                        if (i != 0 && i != N + 1)
                        {
                            gr.RowDefinitions.Add(new RowDefinition());
                            gr.RowDefinitions.Add(new RowDefinition());
                            gr.ColumnDefinitions.Add(new ColumnDefinition());
                            gr.ColumnDefinitions.Add(new ColumnDefinition());
                            tx1 = new TextBox();
                            tx1.VerticalAlignment = VerticalAlignment.Center;
                            tx1.HorizontalAlignment = HorizontalAlignment.Center;
                            tx1.SetValue(Grid.RowProperty, 1);
                            tx1.SetValue(Grid.ColumnProperty, 0);
                            tx1.BorderThickness = new Thickness(0);
                            tx1.Text = i.ToString();
                            tx2 = new TextBox();
                            tx2.Text = West[i - 1];
                            tx2.Foreground = Brushes.Blue;
                            tx2.SetValue(Grid.RowProperty, 0);
                            tx2.SetValue(Grid.ColumnProperty, 1);
                            tx2.BorderThickness = new Thickness(0);
                            tx2.VerticalAlignment = VerticalAlignment.Center;
                            tx2.HorizontalAlignment = HorizontalAlignment.Center;
                            gr.Children.Add(tx1);
                            gr.Children.Add(tx2);
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
                        tx3 = new TextBox();
                        tx3.Text = Delt[i - 1][j - 1];
                        tx3.SetValue(Grid.RowProperty, 0);
                        tx3.SetValue(Grid.ColumnProperty, 0);
                        tx3.BorderThickness = new Thickness(0);
                        tx3.VerticalAlignment = VerticalAlignment.Center;
                        tx3.HorizontalAlignment = HorizontalAlignment.Center;
                        if ((ComitRow[i - 1] || ComitColumn[j - 1]) && !Final)
                        {
                            gr.Background = Brushes.LightGray;
                            tx1.Background = Brushes.LightGray;
                            tx2.Background = Brushes.LightGray;
                            tx3.Background = Brushes.LightGray; 
                        }
                        if (Path[i - 1][j - 1])
                        {
                            gr.Background = Brushes.Azure;
                            tx1.Background = Brushes.Azure;
                            tx2.Background = Brushes.Azure;
                            tx3.Background = Brushes.Azure;
                        }
                        gr.Children.Add(tx1);
                        gr.Children.Add(tx2);
                        gr.Children.Add(tx3);
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
            Memento meme;
            for (;;)
            {
                meme = new Memento(C, X, Path, ComitRow, ComitColumn, North, West, Delt, Final);
                Logs.Add(meme);
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
                if (i == N || j == M)
                    break;
            }
            Final = true;
            meme = new Memento(C, X, Path, ComitRow, ComitColumn, North, West, Delt, Final);
            Logs.Add(meme);
            UpdateTable();
        }

        public void MinElemMeth()
        {
            Logs.Clear();
            int i = 0, j = 0;
            int[] a = new int[N];
            int[] b = new int[M];
            Array.Copy(A, a, N);
            Array.Copy(B, b, M);
            int temp;
            Memento meme;
            List<int> rows = new List<int>();
            List<int> columns = new List<int>();
            for (;;)
            {
                meme = new Memento(C, X, Path, ComitRow, ComitColumn, North, West, Delt, Final);
                Logs.Add(meme);
                FindMinElem(rows, columns, ref i, ref j);
                Path[i][j] = true;
                if (a[i] > b[j])
                {
                    ComitColumn[j] = true;
                    temp = b[j];
                    a[i] -= b[j];
                    X[i][j] = temp.ToString();
                    columns.Add(j);
                }
                else
                {
                    ComitRow[i] = true;
                    temp = a[i];
                    b[j] -= a[i];
                    X[i][j] = temp.ToString();
                    rows.Add(i);
                }
                if (rows.Count == N || columns.Count == M)
                    break;
            }
            Final = true;
            meme = new Memento(C, X, Path, ComitRow, ComitColumn, North, West, Delt, Final);
            Logs.Add(meme);
            UpdateTable();

        }

        private void FindMinElem(List<int> rows, List<int> columns, ref int i, ref int j)
        {
            int min = int.MaxValue;
            for (int a = 0; a < N; a++)
            {
                for (int b = 0; b < M; b++)
                {
                    if (!rows.Contains(a) && !columns.Contains(b))
                    {
                        if (min > C[a][b])
                        {
                            min = C[a][b];
                            i = a;
                            j = b;
                        }
                    }
                }
            }
        }



        public void FogelMeth()
        {
            Logs.Clear();
            int i1 = 0, i2 = 0, j1 = 0, j2 = 0;
            int[] a = new int[N];
            int[] b = new int[M];
            Array.Copy(A, a, N);
            Array.Copy(B, b, M);
            int temp;
            Memento meme;
            List<int> rows = new List<int>();
            List<int> columns = new List<int>();
            for (;;)
            {
                meme = new Memento(C, X, Path, ComitRow, ComitColumn, North, West, Delt, Final);
                Logs.Add(meme);
                ClearNW();
                Find2Min(rows, columns);
                j1 = int.Parse(North.Min());
                i1 = int.Parse(West.Min());
                if (j1 < i1)
                {
                    j2 = MinNorth(columns);
                    i2 = MinInColumn(j2, rows);
                }
                else
                {
                    i2 = MinWest(rows);
                    j2 = MinInRow(i2, columns);
                }
                Path[i2][j2] = true;
                if (a[i2] > b[j2])
                {
                    ComitColumn[j2] = true;
                    temp = b[j2];
                    a[i2] -= b[j2];
                    X[i2][j2] = temp.ToString();
                    columns.Add(j2);
                }
                else
                {
                    ComitRow[i2] = true;
                    temp = a[i2];
                    b[j2] -= a[i2];
                    X[i2][j2] = temp.ToString();
                    rows.Add(i2);
                }
                if (rows.Count == N || columns.Count == M)
                    break;
            }
            Final = true;
            ClearNW();
            meme = new Memento(C, X, Path, ComitRow, ComitColumn, North, West, Delt, Final);
            Logs.Add(meme);
            UpdateTable();
        }

        public void ClearNW()
        {
            Array.Clear(North, 0, North.Length);
            Array.Clear(West, 0, West.Length);
        }

        public int MinInColumn(int j, List<int> rows)
        {
            int min = int.MaxValue;
            int k = -1;
            for (int i = 0; i < N; i++)
            {
                if (!rows.Contains(i))
                {
                    if (min > C[i][j])
                    {
                        min = C[i][j];
                        k = i;
                    }
                }
            }
            return k;
        }

        public int MinNorth(List<int> columns)
        {
            int min = int.MaxValue;
            int k = -1;
            for (int i = 0; i < M; i++)
            {
                if (!columns.Contains(i))
                {
                    if (min > int.Parse(North[i]))
                    {
                        k = i;
                        min = int.Parse(North[i]);
                    }
                }
            }
            return k;
        }

        public int MinInRow(int i, List<int> columns)
        {
            int min = int.MaxValue;
            int k = -1;
            for (int j = 0; j < M; j++)
            {
                if (!columns.Contains(j))
                {
                    if (min > C[i][j])
                    {
                        min = C[i][j];
                        k = j;
                    }
                }
            }
            return k;

        }

        public int MinWest(List<int> rows)
        {
            int min = int.MaxValue;
            int k = -1;
            for (int i = 0; i < N; i++)
            {
                if (!rows.Contains(i))
                {
                    if (min > int.Parse(West[i]))
                    {
                        k = i;
                        min = int.Parse(West[i]);
                    }
                }
            }
            return k;
        }


        public void Find2Min(List<int> rows, List<int> columns)
        {
            int min1 = int.MaxValue, min2 = int.MaxValue;
            for (int i = 0; i < N; i++)
            {
                if (!rows.Contains(i))
                {
                    min1 = int.MaxValue;
                    min2 = int.MaxValue;
                    for (int j = 0; j < M; j++)
                    {
                        if (!columns.Contains(j))
                        {
                            if (min2 > C[i][j])
                            {
                                min2 = C[i][j];
                                if (min1 > C[i][j])
                                {
                                    min2 = min1;
                                    min1 = C[i][j];
                                }
                            }
                        }
                    }
                    West[i] = (min2 - min1).ToString();
                }
            }
            for (int j = 0; j < M; j++)
            {
                if (!columns.Contains(j))
                {
                    min1 = int.MaxValue;
                    min2 = int.MaxValue;
                    for (int i = 0; i < N; i++)
                    {
                        if (!rows.Contains(i))
                        {
                            if (min2 > C[i][j])
                            {
                                min2 = C[i][j];
                                if (min1 > C[i][j])
                                {
                                    min2 = min1;
                                    min1 = C[i][j];
                                }
                            }
                        }
                    }
                    North[j] = (min2 - min1).ToString();
                }
            }
        }


        public void PotMeth()
        {
            Logs.Clear();
            int i1 = 0, i2 = 0, j1 = 0, j2 = 0;
            int[] a = new int[N];
            int[] b = new int[M];
            Array.Copy(A, a, N);
            Array.Copy(B, b, M);
            int temp;
            Memento meme;
            List<int> rows = new List<int>();
            List<int> columns = new List<int>();

            CalcPot();
            UpdateTable();

        }


        private void CalcPot()
        {
            int[] u = new int[N];
            int[] v = new int[M];
            List<int> rows = new List<int>();
            List<int> columns = new List<int>();
            for (int i=0; i<N; i++)
            {
                u[i] = 666;
            }
            for (int j = 0; j < M; j++)
                v[j] = 666;
            u[0] = 0;
            for (int j = 0; j < M; j++)
            {
                if (Path[0][j])
                {
                    v[j] = C[0][j];
                    columns.Add(j);
                }
            }
            for (int k = 0; k < 6; k++)
            {
                for (int i = 0; i < N; i++)
                {
                    foreach (int j in columns)
                    {
                        if (Path[i][j])
                        {
                            u[i] = C[i][j] - v[j];
                            rows.Add(i);
                        }
                    }
                }
                for (int j = 0; j < M; j++)
                {
                    foreach (int i in rows)
                    {
                        if (Path[i][j])
                        {
                            v[j] = C[i][j] - u[i];
                            columns.Add(i);
                        }
                    }
                }

            }


            for (int i=0; i<N; i++)
            {
                if (u[i] != 666)
                    West[i] = u[i].ToString();
            }
            for (int j=0; j<M; j++)
            {
                if (v[j] != 666)
                    North[j] = v[j].ToString();
            }
        }
        


        public void ShowHistory(int i)
        {
            if (i < 0 || i >= Logs.Count)
                return;
            ReadMeme(Logs[i]);
            UpdateTable();

        }

        private void ReadMeme(Memento meme)
        {
            X = meme.X;
            Path = meme.Path; ;
            C = meme.C;
            ComitRow = meme.ComitRow;
            ComitColumn = meme.ComitColumn;
            North = meme.North;
            West = meme.West;
            Final = meme.Final;
        }

    }


    public class Memento
    {
        public int[][] C { get; set; }

        public string[][] X { get; set; }

        public string[][] Delt { get; set; }

        public bool[][] Path { get; set; }

        public bool[] ComitRow { get; set; }

        public bool[] ComitColumn { get; set; }

        public string[] North { get; set; }
        public string[] West { get; set; }

        public bool Final { get; set; }


        public Memento(int[][] c, string[][] x, bool[][] path, bool[] cr, bool[] cc, string[] north, string[] west, string[][] delt, bool final)
        {
            int n = cr.Length;
            int m = cc.Length;
            ComitRow = new bool[n];
            ComitColumn = new bool[m];
            North = new string[m];
            West = new string[n];
            Array.Copy(cc, ComitColumn, m);
            Array.Copy(cr, ComitRow, n);
            Array.Copy(north, North, m);
            Array.Copy(west, West, n);
            X = new string[n][];
            C = new int[n][];
            Path = new bool[n][];
            Delt = new string[n][];
            for (int i = 0; i < n; i++)
            {
                X[i] = new string[m];
                C[i] = new int[m];
                Path[i] = new bool[m];
                Delt[i] = new string[m];
                for (int j = 0; j < m; j++)
                {
                    X[i][j] = x[i][j];
                    C[i][j] = c[i][j];
                    Path[i][j] = path[i][j];
                    Delt[i][j] = delt[i][j];
                }
            }
            Final = final;
        }


    }


}
