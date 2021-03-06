﻿using lab4.Algorithms;
using lab4.Algoritms;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace lab4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public List<double> XPoints = new List<double>();
        public List<double> YPoints = new List<double>();

        public MainWindow()
        {
            InitializeComponent();

            for (double i = 0; i < 5; i += 1)
            {
                XPoints.Add(i);
                YPoints.Add(i * i * i);
            }

            radioButtonGold.IsChecked = true;
            xValues.ItemsSource = XPoints;
            yValues.ItemsSource = YPoints;
        }

        PolyFunc pf = new PolyFunc();
        Gauss ga = new Gauss();
        Newton ne = new Newton();
        List<double> solutions = new List<double>();
        Point mousePos = new Point();
        List<string> log = new List<string>();
        string filePath = null;
        List<Point> points = new List<Point>();

        #region Private Functions

        private double SafeParse(string text)
        {
            try
            {
                return double.Parse(text);
            }
            catch
            {
                MessageBox.Show(string.Format("Incorrect value ({0}) of number! ", text), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return double.NaN;
            }
        }



        #endregion

        #region Button Events

        private void buttonDraw_Click(object sender, RoutedEventArgs e)
        {

            double xMin = SafeParse(textBoxXMin.Text);
            double xMax = SafeParse(textBoxXMax.Text);

            if (SafeParse(textBoxYMin.Text) >= SafeParse(textBoxYMax.Text))
            {
                if (SafeParse(textBoxYMin.Text) == SafeParse(textBoxYMax.Text) || SafeParse(textBoxYMax.Text) - SafeParse(textBoxYMin.Text) < 1)
                {
                    MessageBox.Show("Incorrect drawing range by y-axis!\nNeed yMin < yMax and yMax - yMin >= 1", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string temp = textBoxYMin.Text;
                textBoxYMin.Text = textBoxYMax.Text;
                textBoxYMax.Text = temp;
            }
            double yMin = SafeParse(textBoxYMin.Text);
            double yMax = SafeParse(textBoxYMax.Text);

            CanvasBuilder.ClearCanvas(canvas);
            CanvasBuilder.DrawGrid(canvas, xMax, xMin, yMax, yMin);

            CanvasBuilder.DrawPoints(canvas, Brushes.Red, points);
        }

        private void buttonCalc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                points = new List<Point>();
                xValues.ItemsSource = XPoints;
                yValues.ItemsSource = YPoints;
                string s = "";
                solutions.Clear();
                if (radioButtonGold.IsChecked.Value)
                {
                    ga.Clear();
                    log = ga.Log;
                    for (double i = XPoints.First(); i <= XPoints.Last(); i += 0.1)
                        points.Add(new Point(i, ga.Calculate(i, XPoints, YPoints)));

                }
                else
                {
                    ne.Clear();
                    log = ne.Log;
                    for (double i = XPoints.First(); i <= XPoints.Last(); i += 0.1)
                        points.Add(new Point(i, ne.Calculate(i, XPoints, YPoints)));
                }

                s += "\nОтвет: \n";
                foreach (Point p in points)
                {
                    s += Math.Round(p.X, 2) + "      " + Math.Round(p.Y, 2) + "\n";
                }
                textBoxSols.Text = s;
                buttonDraw_Click(sender, new RoutedEventArgs());


            }
            catch
            {

            }

        }


        #endregion

        #region Canvas Events

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            buttonDraw_Click(sender, e);

        }

        private void canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            double xMin = SafeParse(textBoxXMin.Text);
            double xMax = SafeParse(textBoxXMax.Text);
            double yMin = SafeParse(textBoxYMin.Text);
            double yMax = SafeParse(textBoxYMax.Text);

            if (e.Delta > 0)
            {
                textBoxXMin.Text = Math.Round((xMin / 100 * 80), 3).ToString();
                textBoxXMax.Text = Math.Round((xMax / 100 * 80), 3).ToString();

                textBoxYMin.Text = Math.Round((yMin / 100 * 80), 3).ToString();
                textBoxYMax.Text = Math.Round((yMax / 100 * 80), 3).ToString();
            }
            else
            {
                textBoxXMin.Text = Math.Round((xMin / 100 * 120), 3).ToString();
                textBoxXMax.Text = Math.Round((xMax / 100 * 120), 3).ToString();

                textBoxYMin.Text = Math.Round((yMin / 100 * 120), 3).ToString();
                textBoxYMax.Text = Math.Round((yMax / 100 * 120), 3).ToString();
            }
            buttonDraw_Click(sender, new RoutedEventArgs());
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point Canvas_mouseDelta = new Point(0, 0);
                Point p = e.GetPosition(canvas);

                Canvas_mouseDelta.X = mousePos.X - p.X;
                Canvas_mouseDelta.Y = mousePos.Y - p.Y;

                mousePos = p;

                double xMin = double.Parse(textBoxXMin.Text);
                double xMax = double.Parse(textBoxXMax.Text);
                double yMin = double.Parse(textBoxYMin.Text);
                double yMax = double.Parse(textBoxYMax.Text);

                double xScale = canvas.ActualWidth / (xMax - xMin);
                double yScale = canvas.ActualHeight / (yMax - yMin);

                textBoxXMin.Text = (xMin + (Canvas_mouseDelta.X / xScale)).ToString();
                textBoxXMax.Text = (xMax + (Canvas_mouseDelta.X / xScale)).ToString();
                textBoxYMin.Text = (yMin - (Canvas_mouseDelta.Y / yScale)).ToString();
                textBoxYMax.Text = (yMax - (Canvas_mouseDelta.Y / yScale)).ToString();

                buttonDraw_Click(sender, new RoutedEventArgs());
            }
        }

        private void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mousePos = e.GetPosition(canvas);

            buttonDraw_Click(sender, new RoutedEventArgs());
        }

        #endregion

        #region Data Events

        private void textBoxFunc_TextChanged(object sender, TextChangedEventArgs e)
        {
            /// nm.Clear();
            solutions.Clear();
            textBoxSols.Text = "";
            buttonDraw_Click(sender, new RoutedEventArgs());
        }

        #endregion

        #region Menu Events

        private void menuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void menuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Multiselect = false;
                ofd.Filter = "XML data file (*.xml)|*.xml|All files|*.*";
                ofd.InitialDirectory = Environment.CurrentDirectory;
                ofd.DefaultExt = "*.xml";
                ofd.CheckPathExists = true;

                SerializedFunc sf = new SerializedFunc();
                if (ofd.ShowDialog() == true)
                {
                    try
                    {
                        sf.Read(ofd.FileName);
                        filePath = ofd.FileName;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка чтения с файла", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                menuItemClear_Click(sender, new RoutedEventArgs());
                pf = sf.pf;
                textBoxXMin.Text = sf.xMin;
                textBoxXMax.Text = sf.xMax;
                textBoxYMin.Text = sf.yMin;
                textBoxYMax.Text = sf.yMax;
                textBoxSols.Text = "";
                buttonDraw_Click(sender, new RoutedEventArgs());
            }
            catch
            {

            }
        }

        private void menuItemSave_Click(object sender, RoutedEventArgs e)
        {
            if (filePath != null && File.Exists(filePath))
            {


                try
                {
                    // sf.Write(filePath);
                    MessageBox.Show("Файл сохранено", "Сохранено", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка записи в файл", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                menuItemSaveAs_Click(sender, new RoutedEventArgs());
            }

        }

        private void menuItemSaveAs_Click(object sender, RoutedEventArgs e)
        {

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "points.xml";
            sfd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            sfd.Filter = "Файлs XML (*.xml)|*.xml|Все файлы (*.*)|*.*";
            sfd.DefaultExt = "*.xml";

            if (sfd.ShowDialog() == true)
            {
                try
                {
                    // sf.Write(sfd.FileName);
                    filePath = sfd.FileName;
                    MessageBox.Show("Файл сохранено");
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка записи в файл");
                }
            }
        }
        private void menuItemCalc_Click(object sender, RoutedEventArgs e)
        {
            buttonCalc_Click(sender, new RoutedEventArgs());

        }

        private void menuItemDraw_Click(object sender, RoutedEventArgs e)
        {
            buttonDraw_Click(sender, new RoutedEventArgs());
        }



        private void menuItemClear_Click(object sender, RoutedEventArgs e)
        {
            pf = new PolyFunc();
            ga = new Gauss();
            ne = new Newton();
            textBoxXMin.Text = "-10";
            textBoxXMax.Text = "10";
            textBoxYMin.Text = "-10";
            textBoxYMax.Text = "10";
            textBoxSols.Text = "";
        }

        private void label11_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            XPoints.Clear();
            YPoints.Clear();
            XPoints.AddRange(new double[] { -5, -3, -1, 1, 3 });
            YPoints.AddRange(new double[] { 4, -4, 0, 40, 140 });
            xValues.ItemsSource = XPoints;
            yValues.ItemsSource = YPoints;
            xValues.Items.Refresh();
            yValues.Items.Refresh();

        }

        private void label22_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            XPoints.Clear();
            YPoints.Clear();
            XPoints.AddRange(new double[] { -6, -3, 0, 3, 6 });
            YPoints.AddRange(new double[] { 5, 12, 7, 3, 2 });
            xValues.ItemsSource = XPoints;
            yValues.ItemsSource = YPoints;
            xValues.Items.Refresh();
            yValues.Items.Refresh();
        }
    }
    #endregion
}
