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
        #region Properties


        private int[] _a;
        public int[] A
        {
            get
            {
                return _a;
            }
            set
            {
                _a = value;
                At = new int[A.Length];
                Array.Copy(_a, At, A.Length);
            }
        }

        private int[] _b;
        public int[] B
        {
            get
            {
                return _b;
            }
            set
            {
                _b = value;
                Bt = new int[B.Length];
                Array.Copy(_b, Bt, B.Length);
            }
        }

        public int[] At { get; set; }
        public int[] Bt { get; set; }

        public int[][] C { get; set; }
        public string[][] X { get; set; }
        public string[][] Delt { get; set; }
        public string[][] O { get; set; }

        public int N { get; private set; }
        public int M { get; private set; }

        public string[] North { get; set; }
        public string[] West { get; set; }

        public bool[][] Path { get; private set; }
        public bool[] ComitRow { get; private set; }
        public bool[] ComitColumn { get; private set; }

        public Grid Table { get; set; }

        public List<Memento> Logs { get; private set; }

        private Selectr AddCell { get; set; }
        private Selectr DelCell { get; set; }

        private bool Final { get; set; }

        public Writer Writter { get; set; }

        #endregion


        public TableModel(int n, int m, Grid table)
        {
            Table = table;
            Table.RowDefinitions.Clear();
            Table.ColumnDefinitions.Clear();
            N = n;
            M = m;
            A = new int[N];
            B = new int[M];
            At = new int[N];
            Bt = new int[M];
            C = new int[N][];
            X = new string[N][];
            O = new string[N][];
            Delt = new string[N][];
            North = new string[M];
            West = new string[N];
            ComitColumn = new bool[M];
            ComitRow = new bool[N];
            Path = new bool[N][];
            Logs = new List<Memento>();
            AddCell = new Selectr();
            DelCell = new Selectr();
            for (int i = 0; i < N; i++)
            {
                C[i] = new int[M];
                X[i] = new string[M];
                O[i] = new string[M];
                Delt[i] = new string[M];
                Path[i] = new bool[M];
                Table.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    X[i][j] = "";
                    O[i][j] = "";
                    Delt[i][j] = "";
                }
            }
            Table.RowDefinitions.Add(new RowDefinition());
            Table.RowDefinitions.Add(new RowDefinition());
            for (int i = 0; i < M + 2; i++)
            {
                Table.ColumnDefinitions.Add(new ColumnDefinition());
            }
            Writter = new Writer();
        }


        #region Resize Methods

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
            a = At;
            Array.Resize(ref a, n);
            At = a;
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
            x = O;
            Array.Resize(ref x, n);
            O = x;
            string[] nw = West;
            Array.Resize(ref nw, n);
            West = nw;
            if (N < n)
            {
                for (int i = N; i < n; i++)
                {
                    X[i] = new string[M];
                    O[i] = new string[M];
                    Delt[i] = new string[M];
                    C[i] = new int[M];
                    Path[i] = new bool[M];
                    for (int j = 0; j < M; j++)
                    {
                        X[i][j] = "";
                        O[i][j] = "";
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
            b = Bt;
            Array.Resize(ref b, m);
            Bt = b;
            int[][] c = C;
            string[][] x = X;
            string[][] delt = Delt;
            string[][] o = O;
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
                Array.Resize(ref o[i], m);
                Array.Resize(ref p[i], m);
                Array.Resize(ref Delt[i], m);
            }
            C = c;
            X = x;
            O = o;
            Path = p;
            if (M < m)
            {
                for (int i = 0; i < N; i++)
                {
                    for (int j = M; j < m; j++)
                    {
                        X[i][j] = "";
                        O[i][j] = "";
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


        #endregion

        #region Logs Methods

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
            O = meme.O;
            Path = meme.Path; ;
            C = meme.C;
            ComitRow = meme.ComitRow;
            ComitColumn = meme.ComitColumn;
            North = meme.North;
            West = meme.West;
            Final = meme.Final;
            Delt = meme.Delt;
            AddCell = meme.AddedCell;
            DelCell = meme.DeletedCell;
            At = meme.At;
            Bt = meme.Bt;
        }

        #endregion

        #region Read and Update

        public void UpdateTable()
        {
            Table.Children.Clear();
            Grid gr;
            TextBox tx1, tx2, tx3, tx4;
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
                            tx1.IsReadOnly = true;
                            tx2 = new TextBox();
                            tx2.Text = North[j - 1];
                            tx2.SetValue(Grid.RowProperty, 1);
                            tx2.SetValue(Grid.ColumnProperty, 0);
                            tx2.Foreground = Brushes.Blue;
                            tx2.BorderThickness = new Thickness(0);
                            tx2.VerticalAlignment = VerticalAlignment.Center;
                            tx2.HorizontalAlignment = HorizontalAlignment.Center;
                            tx2.IsReadOnly = true;
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
                            tx1.IsReadOnly = true;
                            tx2 = new TextBox();
                            tx2.Text = West[i - 1];
                            tx2.Foreground = Brushes.Blue;
                            tx2.SetValue(Grid.RowProperty, 0);
                            tx2.SetValue(Grid.ColumnProperty, 1);
                            tx2.BorderThickness = new Thickness(0);
                            tx2.VerticalAlignment = VerticalAlignment.Center;
                            tx2.HorizontalAlignment = HorizontalAlignment.Center;
                            tx2.IsReadOnly = true;
                            gr.Children.Add(tx1);
                            gr.Children.Add(tx2);
                            bor.Child = gr;
                        }

                    }
                    else if (i == N + 1)
                    {
                        if (j != M + 1)
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
                            tx1.Text = B[j - 1].ToString();
                            tx2 = new TextBox();
                            tx2.VerticalAlignment = VerticalAlignment.Center;
                            tx2.HorizontalAlignment = HorizontalAlignment.Center;
                            tx2.SetValue(Grid.RowProperty, 0);
                            tx2.SetValue(Grid.ColumnProperty, 1);
                            tx2.BorderThickness = new Thickness(0);
                            tx2.IsReadOnly = true;
                            tx2.Text = Bt[j - 1].ToString();
                            tx2.Foreground = Brushes.Gray;
                            gr.Children.Add(tx1);
                            gr.Children.Add(tx2);
                            bor.Child = gr;
                        }

                    }
                    else if (j == M + 1)
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
                        tx1.Text = A[i - 1].ToString();
                        tx2 = new TextBox();
                        tx2.VerticalAlignment = VerticalAlignment.Center;
                        tx2.HorizontalAlignment = HorizontalAlignment.Center;
                        tx2.SetValue(Grid.RowProperty, 1);
                        tx2.SetValue(Grid.ColumnProperty, 0);
                        tx2.BorderThickness = new Thickness(0);
                        tx2.IsReadOnly = true;
                        tx2.Text = At[i - 1].ToString();
                        tx2.Foreground = Brushes.Gray;
                        gr.Children.Add(tx1);
                        gr.Children.Add(tx2);
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
                        tx2.IsReadOnly = true;
                        tx3 = new TextBox();
                        tx3.Text = Delt[i - 1][j - 1];
                        tx3.SetValue(Grid.RowProperty, 0);
                        tx3.SetValue(Grid.ColumnProperty, 0);
                        tx3.BorderThickness = new Thickness(0);
                        tx3.Foreground = Brushes.Blue;
                        tx3.VerticalAlignment = VerticalAlignment.Center;
                        tx3.HorizontalAlignment = HorizontalAlignment.Center;
                        tx3.IsReadOnly = true;
                        tx4 = new TextBox();
                        tx4.Text = O[i - 1][j - 1];
                        tx4.SetValue(Grid.RowProperty, 1);
                        tx4.SetValue(Grid.ColumnProperty, 1);
                        tx4.BorderThickness = new Thickness(0);
                        tx4.Foreground = Brushes.Red;
                        tx4.VerticalAlignment = VerticalAlignment.Center;
                        tx4.HorizontalAlignment = HorizontalAlignment.Center;
                        tx4.IsReadOnly = true;
                        if (DelCell.Is && DelCell.I == (i - 1) && DelCell.J == (j - 1))
                        {
                            gr.Background = Brushes.LightBlue;
                            tx1.Background = Brushes.LightBlue;
                            tx2.Background = Brushes.LightBlue;
                            tx3.Background = Brushes.LightBlue;
                            tx4.Background = Brushes.LightBlue;
                        }
                        else if (AddCell.Is && AddCell.I == (i - 1) && AddCell.J == (j - 1))
                        {
                            gr.Background = Brushes.LightPink;
                            tx1.Background = Brushes.LightPink;
                            tx2.Background = Brushes.LightPink;
                            tx3.Background = Brushes.LightPink;
                            tx4.Background = Brushes.LightPink;
                        }
                        else if (Path[i - 1][j - 1])
                        {
                            gr.Background = Brushes.Azure;
                            tx1.Background = Brushes.Azure;
                            tx2.Background = Brushes.Azure;
                            tx3.Background = Brushes.Azure;
                            tx4.Background = Brushes.Azure;
                        }
                        else if ((ComitRow[i - 1] || ComitColumn[j - 1]) && !Final)
                        {
                            gr.Background = Brushes.LightGray;
                            tx1.Background = Brushes.LightGray;
                            tx2.Background = Brushes.LightGray;
                            tx3.Background = Brushes.LightGray;
                            tx4.Background = Brushes.LightGray;

                        }
                        gr.Children.Add(tx1);
                        gr.Children.Add(tx2);
                        gr.Children.Add(tx3);
                        gr.Children.Add(tx4);
                        bor.Child = gr;
                    }
                    bor.SetValue(Grid.RowProperty, i);
                    bor.SetValue(Grid.ColumnProperty, j);
                    Table.Children.Add(bor);
                }
            }
        }

        public bool ReadTable()
        {
            try
            {
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < M; j++)
                        C[i][j] = GetCElemAt(i + 1, j + 1);
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

        #endregion

        #region Getters

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
            return int.Parse((((Table.Children[(M + 1) + (M + 2) * i] as Border).Child as Grid).Children[0] as TextBox).Text);

        }

        public int GetBElemAt(int i)
        {
            return int.Parse((((Table.Children[(N + 1) * (M + 2) + i] as Border).Child as Grid).Children[0] as TextBox).Text);
        }

        public Grid GetGridElemAt(int i, int j)
        {
            return (Table.Children[j + (M + 2) * i] as Border).Child as Grid;
        }

        #endregion

        #region Other Methods

        public void Balance()
        {
            int isClosed = IsClosed();
            if (isClosed == 2)
            {
                ChangeRowCount(N + 1);
                A[N - 1] = B.Sum() - A.Sum();
                At[N - 1] = A[N - 1];
                UpdateTable();
            }
            else if (isClosed == 1)
            {
                ChangeColumnCount(M + 1);
                B[M - 1] = A.Sum() - B.Sum();
                Bt[M - 1] = B[M - 1];
                UpdateTable();
            }

        }

        public int IsClosed()
        {
            int sum1 = A.Sum();
            int sum2 = B.Sum();
            return sum1 > sum2 ? 1 : sum1 < sum2 ? 2 : 0;
        }

        #endregion

        #region Cleaning Method

        private void ClearDelt()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    Delt[i][j] = "";
                    O[i][j] = "";
                }
                West[i] = "";
            }
            for (int j = 0; j < M; j++)
                North[j] = "";
        }

        public void ClearNW()
        {
            Array.Clear(North, 0, North.Length);
            Array.Clear(West, 0, West.Length);
        }


        public void Clear()
        {
            ClearNW();
            ClearDelt();
            AddCell.Is = false;
            DelCell.Is = false;
            Array.Clear(ComitRow, 0, N);
            Array.Clear(ComitColumn, 0, M);
            Logs.Clear();
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    X[i][j] = "";
                    Path[i][j] = false;
                }
            }
            Final = true;
            Writter.Clear();
        }


        #endregion

        #region NorthWest Angle

        public void SevenEastAngle()
        {
            Logs.Clear();
            Writter.Clear();
            int i = 0, j = 0;
            A = A;
            B = B;
            int[] a = At;
            int[] b = Bt;
            int temp;
            Memento meme;
            Writter.Write("Метод Северо-Западного Угла\n\n");
            for (int qr = 1; ; qr++)
            {
                meme = new Memento(C, X, Path, ComitRow, ComitColumn, North, West, Delt, O, Final, AddCell, DelCell, At, Bt);
                Logs.Add(meme);
                Final = false;
                AddCell.Is = true;
                if (i == N || j == M)
                {
                    break;
                }
                Path[i][j] = true;
                AddCell.I = i;
                AddCell.J = j;
                Writter.WriteIterationNumber(qr);
                Writter.WriteAboutSelectedPath(i + 1, j + 1);
                Writter.WriteAboutAB(a[i], b[j]);
                if (a[i] > b[j])
                {
                    ComitColumn[j] = true;
                    temp = b[j];
                    a[i] -= b[j];
                    b[j] = 0;
                    X[i][j] = temp.ToString();
                    j++;
                }
                else
                {
                    ComitRow[i] = true;
                    temp = a[i];
                    b[j] -= a[i];
                    a[i] = 0;
                    X[i][j] = temp.ToString();
                    i++;
                }
            }
            Writter.Write("\nПроцесс завершен. Найден базис:\n");
            Writter.WriteAboutPath(Path);
            Final = true;
            AddCell.Is = false;
            meme = new Memento(C, X, Path, ComitRow, ComitColumn, North, West, Delt, O, Final, AddCell, DelCell, At, Bt);
            Logs.Add(meme);
            UpdateTable();
        }

        #endregion

        #region Minimal Elememt Metod

        public void MinElemMeth()
        {
            Logs.Clear();
            Writter.Clear();
            int i = 0, j = 0;
            A = A;
            B = B;
            int[] a = At;
            int[] b = Bt;
            int temp;
            Memento meme;
            List<int> rows = new List<int>();
            List<int> columns = new List<int>();
            Writter.Write("Метод Минимального эл-та\n\n");
            for (int qr = 1; ; qr++)
            {
                meme = new Memento(C, X, Path, ComitRow, ComitColumn, North, West, Delt, O, Final, AddCell, DelCell, At, Bt);
                Logs.Add(meme);
                Final = false;
                if (rows.Count == N || columns.Count == M)
                    break;
                AddCell.Is = true;
                FindMinElem(rows, columns, ref i, ref j);
                Path[i][j] = true;
                Writter.WriteIterationNumber(qr);
                Writter.Write("Минимальные издержки: " + C[i][j] + "\n");
                Writter.WriteAboutSelectedPath(i + 1, j + 1);
                Writter.WriteAboutAB(a[i], b[j]);
                if (a[i] > b[j])
                {
                    ComitColumn[j] = true;
                    temp = b[j];
                    a[i] -= b[j];
                    b[j] = 0;
                    X[i][j] = temp.ToString();
                    columns.Add(j);
                }
                else
                {
                    ComitRow[i] = true;
                    temp = a[i];
                    b[j] -= a[i];
                    a[i] = 0;
                    X[i][j] = temp.ToString();
                    rows.Add(i);
                }
                AddCell.I = i;
                AddCell.J = j;
            }
            Writter.Write("\nПроцесс завершен. Найден базис:\n");
            Writter.WriteAboutPath(Path);
            Final = true;
            AddCell.Is = false;
            meme = new Memento(C, X, Path, ComitRow, ComitColumn, North, West, Delt, O, Final, AddCell, DelCell, At, Bt);
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

        #endregion

        #region Fogel Method

        public void FogelMeth()
        {
            Logs.Clear();
            Writter.Clear();
            int i1 = 0, i2 = 0, j1 = 0, j2 = 0;
            A = A;
            B = B;
            int[] a = At;
            int[] b = Bt;
            int temp;
            Memento meme;
            List<int> rows = new List<int>();
            List<int> columns = new List<int>();
            Writter.Write("Метод Фогеля\n\n");
            for (int qr = 1; ; qr++)
            {
                meme = new Memento(C, X, Path, ComitRow, ComitColumn, North, West, Delt, O, Final, AddCell, DelCell, At, Bt);
                Logs.Add(meme);
                if (rows.Count == N || columns.Count == M)
                    break;
                Writter.WriteIterationNumber(qr);
                Final = false;
                AddCell.Is = true;
                ClearNW();
                Find2Min(rows, columns);
                j1 = int.Parse(North.Min());
                i1 = int.Parse(West.Min());
                if (j1 < i1)
                {
                    j2 = MinNorth(columns);
                    i2 = MinInColumn(j2, rows);
                    if (j1 <= int.MaxValue - 10 * C[i2][j2])
                        Writter.Append("Минимальный штраф: " + j1);
                    Writter.Append(string.Format("Минимальная издержка  в стобце#{0}: {1}\n", j2 + 1, C[i2][j2]));

                }
                else
                {
                    i2 = MinWest(rows);
                    j2 = MinInRow(i2, columns);
                    if (i1 <= int.MaxValue - 10 * C[i2][j2])
                        Writter.Append("Минимальный штраф: " + i1 + "\n");
                    Writter.Append(string.Format("Минимальная издержка в строке#{0}: {1}\n", i2 + 1, C[i2][j2]));
                }
                Path[i2][j2] = true;
                if (columns.Count >= (M - 1))
                {
                    for (int i = 0; i < N; i++)
                    {
                        West[i] = "";
                    }
                }
                if (rows.Count >= (N - 1))
                {
                    for (int j = 0; j < M; j++)
                    {
                        North[j] = "";
                    }
                }
                Writter.WriteLine();
                Writter.WriteAboutSelectedPath(i2 + 1, j2 + 1);
                Writter.WriteAboutAB(a[i2], b[j2]);
                if (a[i2] > b[j2])
                {
                    ComitColumn[j2] = true;
                    temp = b[j2];
                    a[i2] -= b[j2];
                    b[j2] = 0;
                    X[i2][j2] = temp.ToString();
                    columns.Add(j2);
                }
                else
                {
                    ComitRow[i2] = true;
                    temp = a[i2];
                    b[j2] -= a[i2];
                    a[i2] = 0;
                    X[i2][j2] = temp.ToString();
                    rows.Add(i2);
                }
                AddCell.I = i2;
                AddCell.J = j2;

            }
            Writter.Write("\nПроцесс завершен. Найден базис:\n");
            Writter.WriteAboutPath(Path);
            Final = true;
            AddCell.Is = false;
            ClearNW();
            meme = new Memento(C, X, Path, ComitRow, ComitColumn, North, West, Delt, O, Final, AddCell, DelCell, At, Bt);
            Logs.Add(meme);
            UpdateTable();
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
                    if (min2 != int.MaxValue)
                        Writter.WriteAbout2Min(min2, min1, i, true);
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
                    if (min2 != int.MaxValue)
                        Writter.WriteAbout2Min(min2, min1, j, false);
                    North[j] = (min2 - min1).ToString();
                }
            }
        }

        #endregion

        #region Potential Method

        public void PotMeth()
        {
            if (Logs.Count < 1)
                return;
            Writter.Clear();
            ShowHistory(Logs.Count - 1);
            Logs.Clear();
            int i1 = 0, j1 = 0;
            int[] a = new int[N];
            int[] b = new int[M];
            Graph g;
            Array.Copy(A, a, N);
            Array.Copy(B, b, M);
            int maxDelt, im = 0, jm = 0;
            Memento meme;
            List<int> rows = new List<int>();
            List<int> columns = new List<int>();
            meme = new Memento(C, X, Path, ComitRow, ComitColumn, North, West, Delt, O, Final, AddCell, DelCell, At, Bt);
            Logs.Add(meme);
            AddCell.Is = true;
            Writter.Write("Метод Потециалов\n\nНачальный базис:\n");
            Writter.WriteAboutPath(Path);
            for (int qr = 1; ; qr++)
            {
                Writter.WriteIterationNumber(qr);
                CalcPot();
                maxDelt = int.MinValue;
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < M; j++)
                        if (!Path[i][j])
                        {
                            Delt[i][j] = (int.Parse(West[i]) + int.Parse(North[j]) - C[i][j]).ToString();
                            Writter.Append(string.Format("Δ({0},{1}) = {2}\n", i, j, Delt[i][j]));
                            if (maxDelt < int.Parse(Delt[i][j]))
                            {
                                maxDelt = int.Parse(Delt[i][j]);
                                im = i;
                                jm = j;
                            }
                        }
                }
                Writter.WriteLine();
                if (maxDelt <= 0)
                    break;
                Writter.Append(string.Format("Максимальная дельта: {0}\n Вводим в базис ({1},{2})\n", maxDelt, im + 1, jm + 1));
                Writter.Append("Строим замкнутый цикл\n");
                g = new Graph();
                Path[im][jm] = true;
                g.BuildGraph(Path);
                g.Purge();
                g.SetRoot(im, jm);
                g.Sort();
                bool tb = true;
                int min = int.MaxValue;
                foreach (Node v in g.SortedNodes)
                {
                    if (tb)
                    {
                        Writter.Append(string.Format("({0},{1}) + θ", v.I + 1, v.J + 1));
                        O[v.I][v.J] = "+";
                    }
                    else
                    {
                        Writter.Append(string.Format("({0},{1}) - θ", v.I + 1, v.J + 1));
                        O[v.I][v.J] = "-";
                        if (int.Parse(X[v.I][v.J]) < min)
                        {
                            min = int.Parse(X[v.I][v.J]);
                            i1 = v.I;
                            j1 = v.J;
                        }
                    }
                    if (v != g.SortedNodes.Last())
                        Writter.Append(" => ");
                    tb = !tb;
                }
                Writter.WriteLine();
                Writter.Append("θ = " + min + "\n");
                Writter.Append("Новые значения X:\n");
                foreach (Node v in g.SortedNodes)
                {
                    if (v.I == im && v.J == jm)
                    {
                        X[v.I][v.J] = min.ToString();
                        O[v.I][v.J] += " " + min;
                    }
                    else if (O[v.I][v.J] == "+")
                    {
                        X[v.I][v.J] = (int.Parse(X[v.I][v.J]) + min).ToString();
                        O[v.I][v.J] += " " + min;
                    }
                    else if (O[v.I][v.J] == "-")
                    {
                        X[v.I][v.J] = (int.Parse(X[v.I][v.J]) - min).ToString();
                        O[v.I][v.J] += " " + min;
                    }
                    Writter.Append(string.Format("X({0},{1}) = {2}\n", v.I + 1, v.J + 1, X[v.I][v.J]));
                }
                Writter.WriteLine();
                Writter.Append(string.Format("Удаляем с базиса ({0},{1})\n\n", i1, j1));
                Writter.Append("Новый базис:\n");
                Writter.WriteAboutPath(Path);
                X[i1][j1] = "";
                DelCell.Is = true;
                Path[i1][j1] = false;
                DelCell.I = i1;
                DelCell.J = j1;
                AddCell.I = im;
                AddCell.J = jm;
                meme = new Memento(C, X, Path, ComitRow, ComitColumn, North, West, Delt, O, Final, AddCell, DelCell, At, Bt);
                Logs.Add(meme);

                ClearDelt();
            }
            Writter.Write("\nПроцесс завершен. Оптимальный базис:\n");
            Writter.WriteAboutPath(Path);
            ClearDelt();
            AddCell.Is = false;
            DelCell.Is = false;
            meme = new Memento(C, X, Path, ComitRow, ComitColumn, North, West, Delt, O, Final, AddCell, DelCell, At, Bt);
            Logs.Add(meme);
            UpdateTable();

        }

        private void CalcPot()
        {
            int[] u = new int[N];
            int[] v = new int[M];
            List<int> rows = new List<int>();
            List<int> columns = new List<int>();
            int exit = int.MaxValue;
            for (int i = 0; i < N; i++)
            {
                u[i] = exit;
            }
            for (int j = 0; j < M; j++)
            {
                v[j] = exit;
            }
            u[0] = 0;
            for (int j = 0; j < M; j++)
            {
                if (Path[0][j])
                {
                    v[j] = C[0][j];
                    columns.Add(j);
                }
            }
            for (;;)
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
                columns.Clear();
                for (int j = 0; j < M; j++)
                {
                    foreach (int i in rows)
                    {
                        if (Path[i][j])
                        {
                            v[j] = C[i][j] - u[i];
                            columns.Add(j);
                        }
                    }
                }
                rows.Clear();
                if (u.Max() != exit && v.Max() != exit)
                    break;

            }

            Writter.WriteAboutPot(Path, C, u, v);


            for (int i = 0; i < N; i++)
            {
                West[i] = u[i].ToString();
            }
            for (int j = 0; j < M; j++)
            {
                North[j] = v[j].ToString();
            }
        }

        #endregion

    }



    #region Other classes


    public class Memento
    {

        public int[] At { get; set; }
        public int[] Bt { get; set; }

        public int[][] C { get; set; }

        public string[][] X { get; set; }

        public string[][] Delt { get; set; }
        public string[][] O { get; set; }

        public bool[][] Path { get; set; }

        public bool[] ComitRow { get; set; }

        public bool[] ComitColumn { get; set; }

        public string[] North { get; set; }
        public string[] West { get; set; }

        public Selectr AddedCell { get; set; }
        public Selectr DeletedCell { get; set; }

        public bool Final { get; set; }


        public Memento(int[][] c, string[][] x, bool[][] path, bool[] cr, bool[] cc, string[] north, string[] west,
            string[][] delt, string[][] o, bool final, Selectr added, Selectr deleted, int[] at, int[] bt)
        {
            AddedCell = new Selectr() { I = added.I, J = added.J, Is = added.Is };
            DeletedCell = new Selectr() { I = deleted.I, J = deleted.J, Is = deleted.Is };
            int n = cr.Length;
            int m = cc.Length;
            At = new int[n];
            Bt = new int[m];
            ComitRow = new bool[n];
            ComitColumn = new bool[m];
            North = new string[m];
            West = new string[n];
            Array.Copy(cc, ComitColumn, m);
            Array.Copy(cr, ComitRow, n);
            Array.Copy(north, North, m);
            Array.Copy(west, West, n);
            Array.Copy(at, At, n);
            Array.Copy(bt, Bt, m);
            X = new string[n][];
            O = new string[n][];
            C = new int[n][];
            Path = new bool[n][];
            Delt = new string[n][];
            for (int i = 0; i < n; i++)
            {
                X[i] = new string[m];
                O[i] = new string[m];
                C[i] = new int[m];
                Path[i] = new bool[m];
                Delt[i] = new string[m];
                for (int j = 0; j < m; j++)
                {
                    X[i][j] = x[i][j];
                    O[i][j] = o[i][j];
                    C[i][j] = c[i][j];
                    Path[i][j] = path[i][j];
                    Delt[i][j] = delt[i][j];
                }
            }
            Final = final;
        }
    }


    public class Graph
    {
        public Node Root { get; set; }

        public List<Node> Nodes { get; set; }

        public HashSet<Node> SortedNodes { get; set; }

        public bool[][] Path { get; set; }

        public Graph()
        {
            Root = new Node();
            Nodes = new List<Node>();
            SortedNodes = new HashSet<Node>();
        }

        public void BuildGraph(bool[][] path)
        {
            Nodes.Clear();
            int n = path.Length;
            int m = path[0].Length;
            Path = new bool[n][];
            for (int i = 0; i < n; i++)
            {
                Path[i] = new bool[m];
                for (int j = 0; j < m; j++)
                {
                    Path[i][j] = path[i][j];
                    if (path[i][j])
                    {
                        Nodes.Add(new Node(i, j));
                    }
                }
            }
            BuildLinks();
        }

        public void Sort()
        {
            Root.IsBlack = true;
            SortedNodes.Add(Root);
            bool k = false;
            int i0 = Root.I, j0 = Root.J;
            Node v;
            for (int i = 0; i < Nodes.Count; i++)
            {
                if (k)
                {
                    v = Nodes.Find((c) => c.I == i0 && c.J != j0);
                    v.IsBlack = true;
                    SortedNodes.Add(v);
                    k = false;
                    i0 = v.I;
                    j0 = v.J;
                }
                else
                {
                    v = Nodes.Find((c) => c.I != i0 && c.J == j0);
                    v.IsBlack = false;
                    SortedNodes.Add(v);
                    k = true;
                    i0 = v.I;
                    j0 = v.J;
                }
            }
        }

        public void Purge()
        {
            int k1, k2;
            List<Node> removedNodes = new List<Node>();
            for (int i = 0; i < Nodes.Count; i++)
            {
                k1 = 0;
                k2 = 0;
                for (int j = 0; j < Nodes[i].Leafs.Count; j++)
                {
                    if (Nodes[i].I == Nodes[i].Leafs[j].I)
                        k1++;
                    if (Nodes[i].J == Nodes[i].Leafs[j].J)
                        k2++;
                }
                if (k1 < 1 || k2 < 1)
                {
                    Path[Nodes[i].I][Nodes[i].J] = false;
                    BuildGraph(Path);
                    Purge();
                }

            }

        }

        private void RemoveNode(Node n)
        {
            Path[n.I][n.J] = false;
            BuildGraph(Path);
            Purge();
            foreach (Node e in Nodes)
                MessageBox.Show(e + "   " + e.Leafs.Count);
        }

        public void SetRoot(int i, int j)
        {
            Root = Nodes.Find((c) => c.I == i && c.J == j);
        }



        private void BuildLinks()
        {
            int n = Nodes.Count;
            Nodes.Sort((c1, c2) => c1.I.CompareTo(c2.I));
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (Nodes[i].I == Nodes[j].I || Nodes[i].J == Nodes[j].J)
                    {
                        Nodes[i].Leafs.Add(Nodes[j]);
                        Nodes[j].Leafs.Add(Nodes[i]);
                    }
                }
            }
            Nodes.Sort((c1, c2) => c1.Leafs.Count.CompareTo(c2.Leafs.Count));
        }
    }


    public class Node
    {
        public int I { get; set; }

        public int J { get; set; }

        public bool IsBlack { get; set; }

        public List<Node> Leafs { get; set; }


        public Node()
        {
            Leafs = new List<Node>();
            IsBlack = false;
        }

        public Node(int i, int j)
        {
            I = i;
            J = j;
            Leafs = new List<Node>();
            IsBlack = false;
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", I + 1, J + 1);
        }
    }



    public class Writer
    {
        public List<string> Novels { get; set; }

        private int CurChapter { get; set; }



        public Writer()
        {
            Novels = new List<string>();
        }

        public string this[int i]
        {
            get
            {
                if (Novels.Count > i)
                    return Novels[i];
                else
                    return "";
            }
        }

        public void Clear()
        {
            Novels.Clear();
            CurChapter = -1;
        }


        public void WriteAboutPot(bool[][] path, int[][] c, int[] u, int[] v)
        {
            string s = "";
            s += "Найдем u и v:\n";
            for (int i = 0; i < path.Length; i++)
            {
                for (int j = 0; j < path[i].Length; j++)
                {
                    if (path[i][j])
                        s += string.Format("u{0} + v{1} = {2}\n", i + 1, j + 1, c[i][j]);
                }
            }
            s += "\n";
            for (int i = 0; i < path.Length; i++)
                s += string.Format("u{0} = {1}\n", i + 1, u[i]);
            for (int j = 0; j < path[0].Length; j++)
                s += string.Format("v{0} = {1}\n", j + 1, v[j]);
            s += "\n";
            Novels[CurChapter] += s;
        }


        public void WriteLine()
        {
            Novels[CurChapter] += "\n";
        }

        public void Write(string novel)
        {
            Novels.Add(novel);
            CurChapter++;
        }

        public void Append(string text)
        {
            Novels[CurChapter] += text;
        }

        public void WriteAboutSelectedPath(int i, int j)
        {
            Novels[CurChapter] += string.Format("Выбираем ячейку ({0},{1})\n", i, j);
        }


        public void WriteAboutPath(bool[][] p)
        {
            List<string> nodes = new List<string>();
            for (int i = 0; i < p.Length; i++)
            {
                for (int j = 0; j < p[i].Length; j++)
                {
                    if (p[i][j])
                        nodes.Add(string.Format("({0},{1})", i + 1, j + 1));
                }
            }
            string s = "";
            for (int i = 0; i < nodes.Count - 1; i++)
                s += nodes[i] + " -> ";
            s += nodes[nodes.Count - 1];
            Novels[CurChapter] += s;
        }

        public void WriteAbout2Min(int m1, int m2, int i, bool row)
        {
            i++;
            string s = "";
            if (row)
                s += "Минимальные значения в строке #" + i + ": ";
            else
                s += "Минимальные значения в стобце #" + i + ": ";
            s += string.Format("{0}; {1}\n", m1, m2);
            s += "Штраф равен: " + (m1 - m2) + "\n\n";
            Novels[CurChapter] += s;
        }

        public void WriteIterationNumber(int i)
        {
            CurChapter = i;
            Novels.Add("");
            Novels[CurChapter] += ("\n==" + i + "==\n");
        }

        public void WriteAboutAB(int a, int b)
        {
            int temp;
            string s = "";
            if (a > b)
            {
                s += "a > b\n";
                temp = b;
                s += "Записываем значение " + temp + "\n";
                a -= b;
                s += "Новое значение a: " + a + "\n";

            }
            else
            {
                s += "a < b\n";
                temp = a;
                s += "Записываем значение " + temp + "\n";
                b -= a;
                s += "Новое значение b: " + b + "\n";
            }
            Novels[CurChapter] += s;
        }


        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < Novels.Count; i++)
                s += Novels[i];
            return s;
        }

    }




    public class Selectr
    {
        public int I { get; set; }
        public int J { get; set; }
        public bool Is { get; set; }

        public Selectr()
        {
            Is = false;
        }
    }
    #endregion

}

