using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using lab1.Model;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using lab1.Algorithms;
using System.Windows;
using System;

namespace lab1.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        Gauss ga;



        private ObservableCollection<Matrix> _matrix;

        public ObservableCollection<Matrix> Matrix
        {
            get
            {
                if (_matrix == null)
                    _matrix = new ObservableCollection<Model.Matrix>() { new Model.Matrix(), new Model.Matrix(), new Model.Matrix(), new Model.Matrix() };
                return _matrix;
            }
            set
            {
                _matrix = value;
                RaisePropertyChanged("Matrix");
            }
        }



        private RelayCommand _calculate;

        public ICommand Calculate
        {
            get
            {
                if (_calculate == null)
                    _calculate = new RelayCommand(ExecuteCalculate);
                return _calculate;
            }
        }

        public void ExecuteCalculate()
        {
            Text = "";
            ResX = "";
            double[][] a;
            double[] b;
            ToArrays(out a, out b);

            int n = Matrix.Count;

            for (int i = 0; i < n; i++)
            {
                if (a[i][i] == 0)
                {
                    Text = "Эл-ты на главной диагонали не должны быть нулевыми";
                    return;
                }
            }

            if (IsIt)
            {

                SimpleIterations si = new SimpleIterations(n);

                si.A = a;
                si.B = b;

                double eps = TextEps.HasValue ? TextEps.Value : 0.001;

                double[] res = si.Calculate(eps);

                if (res == null)
                {
                    Text = si.Log[0];
                    return;
                }

                for (int i = 0; i < si.Log.Count; i++)
                {
                    Text += si.Log[i];
                }

                for (int i = 0; i < n; i++)
                    ResX += "x" + (i + 1) + " = " + Math.Round(res[i], (eps % 1).ToString().Length - 2) + '\n';
            }
            else
            {
                ga = new Gauss(n);
                ga.A = a;
                ga.B = b;

                double eps = TextEps.HasValue ? TextEps.Value : 0.001;

                double[] res = ga.Calculate();

                for (int i = 0; i < n; i++)
                    ResX += "x" + (i + 1) + " = " + Math.Round(res[i], (eps % 1).ToString().Length - 2) + '\n';



            }


        }

        private void ToArrays(out double[][] a, out double[] b)
        {
            int n = Matrix.Count;
            a = new double[n][];
            b = new double[n];
            for (int i = 0; i < n; i++)
            {
                a[i] = new double[i];
                a[i] = Matrix[i].ToArray();
                b[i] = Matrix[i].B;
            }

        }


        private string _text;

        public string Text
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_text))
                    _text = "";
                return _text;
            }
            set
            {
                _text = value;
                RaisePropertyChanged("Text");
            }
        }

        private string _resX;

        public string ResX
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_resX))
                    _resX = "";
                return _resX;
            }
            set
            {
                _resX = value;
                RaisePropertyChanged("ResX");
            }
        }

        private double? _epsText;

        public double? TextEps
        {
            get
            {
                if (_epsText == null)
                    _epsText = 0.001;
                return _epsText;
            }
            set
            {
                if (value > 0)
                    _epsText = value;
                else
                    _epsText = null;
                RaisePropertyChanged("TextEps");
            }
        }

        private bool _isIt = true;

        public bool IsIt
        {
            get
            {
                return _isIt;
            }
            set
            {
                _isIt = value;
                if (IsIt)
                    ItVisibility = Visibility.Visible;
                else
                    ItVisibility = Visibility.Hidden;
                RaisePropertyChanged("IsIt");
            }
        }

        private bool _isGaus = false;

        public bool IsGaus
        {
            get
            {
                return _isGaus;
            }
            set
            {
                _isGaus = value;
                if (IsGaus)
                    GausVisibility = Visibility.Visible;
                else
                    GausVisibility = Visibility.Hidden;
                RaisePropertyChanged("IsGaus");
            }
        }


        private Visibility _itVisibility;

        public Visibility ItVisibility
        {
            get
            {
                return _itVisibility;
            }
            set
            {
                _itVisibility = value;
                RaisePropertyChanged("ItVisibility");
            }
        }


        private Visibility _gausVisibility;

        public Visibility GausVisibility
        {
            get
            {
                return _gausVisibility;
            }
            set
            {
                _gausVisibility = value;
                RaisePropertyChanged("GausVisibility");
            }
        }

        private ObservableCollection<Matrix> _gausStep;

        public ObservableCollection<Matrix> GausMatrix
        {
            get
            {
                if (_gausStep == null)
                    _gausStep = new ObservableCollection<Model.Matrix>();
                return _gausStep;
            }
            set
            {
                _gausStep = value;
             //   MessageBox.Show("hekk");
                RaisePropertyChanged("GausMatrix");
            }
        }

        private int _curStep;

        public int CurStep
        {
            get
            {
                return _curStep;
            }
            set
            {
                _curStep = value;
                GausMatrix = ga.Steps[CurStep].ToMatrix();
                //MessageBox.Show
                RaisePropertyChanged("CurStep");
            }
        }


    }
}