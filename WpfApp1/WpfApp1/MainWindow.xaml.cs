using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Algorithms;
using WpfApp1.Data;
using WpfApp1.Data.Repos;
using WpfApp1.Models;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly ProjectRepo _repo;
        private DataArray _projectEvalDataArray;
        private DataArray _projectEvalOtherDataArray;
        private readonly Project _curProject;
        private ExpertEval _curExpertEval;
        private DevCost _curDevCost;
        private OpCost _curOpCost;
        private EcoEffect _curEcoEffect;

        private int _key;





        public MainWindow()
        {
            InitializeComponent();
            var context = AppDbContext.Create();
            _repo = new ProjectRepo(context);
            _curProject = Project.Create(null);
            _key = 0;
            _curProject.ExpertEval = ExpertEval.Create(_curProject.Id);
            _curProject.DevCost = DevCost.NullDevCost(_curProject.Id);
            _curProject.OpCost = OpCost.NullOpCost(_curProject.Id);
            _curProject.EcoEffect = EcoEffect.NullEcoEffect(_curProject.Id);
            SetUp();
            ShowColumnChart();
        }
        


        private void ShowColumnChart()
        {
            var valueList = new List<KeyValuePair<string, double>>();
            if (_key == 0)
            {
                if (_curProject.Competitivness != null)
                    valueList.Add(new KeyValuePair<string, double>(_curProject.ProjetName, _curProject.Competitivness.Value));
                foreach (var project in _repo.Projects.FindAll(u => u.Competitivness != null && u.Id != _curProject.Id))
                    valueList.Add(new KeyValuePair<string, double>(project.ProjetName, project.Competitivness.Value));
            }
            else if (_key == 1)
            {
                if (_curProject.DevCosts != null)
                    valueList.Add(new KeyValuePair<string, double>(_curProject.ProjetName, _curProject.DevCosts.Value));
                foreach (var project in _repo.Projects.FindAll(u => u.DevCosts != null && u.Id != _curProject.Id))
                    valueList.Add(new KeyValuePair<string, double>(project.ProjetName, project.DevCosts.Value));
            }
            else if (_key == 2)
            {
                if (_curProject.OpCosts != null)
                    valueList.Add(new KeyValuePair<string, double>(_curProject.ProjetName, _curProject.OpCosts.Value));
                foreach (var project in _repo.Projects.FindAll(u => u.OpCosts != null && u.Id != _curProject.Id))
                    valueList.Add(new KeyValuePair<string, double>(project.ProjetName, project.OpCosts.Value));
            }
            else if (_key == 3)
            {
                if (_curProject.EcoEffects != null)
                    valueList.Add(new KeyValuePair<string, double>(_curProject.ProjetName, _curProject.EcoEffects.Value));
                foreach (var project in _repo.Projects.FindAll(u => u.EcoEffects != null && u.Id != _curProject.Id))
                    valueList.Add(new KeyValuePair<string, double>(project.ProjetName, project.EcoEffects.Value));
            }
            ColumnChart.DataContext = valueList;
        }


        private void SetUp()
        {
            _curDevCost = _curProject.DevCost;
            _curExpertEval = _curProject.ExpertEval;
            _curOpCost = _curProject.OpCost;
            _curEcoEffect = _curProject.EcoEffect;
            CompetSetUp();
        }

        private void CompetSetUp()
        {
            ProjectNameTextBox.Text = _curProject.ProjetName;
            _projectEvalDataArray = new DataArray(9, 1);
            DataGridProjectEvals.ItemsSource = _projectEvalDataArray.Data.DefaultView;
            if (_curExpertEval.Any())
            {
                for (int i = 0; i < 9; i++)
                    _projectEvalDataArray[i][0] = _curExpertEval.Evals.ElementAt(i).Val;
            }
            else
            {
                for (int i = 0; i < 9; i++)
                    _projectEvalDataArray[i][0] = 0;
            }
            DataGridProjectEvals.CanUserAddRows = false;
            var otherProjects = _repo.Projects;
            ProjectEvalOtherComboBox.ItemsSource = otherProjects;
        }

        private void ProjectEvalOtherComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var items = _repo.Projects.Find(u => u.ProjetName ==
                                                       ProjectEvalOtherComboBox.SelectedItem.ToString()).ExpertEval.Evals.ToList();
            _projectEvalOtherDataArray = new DataArray(9, 1);
            DataGridOtherProjectEvals.ItemsSource = _projectEvalOtherDataArray.Data.DefaultView;
            for (int i = 0; i < 9; i++)
                _projectEvalOtherDataArray[i][0] = items[i].Val;
            DataGridProjectEvals.CanUserAddRows = false;
        }

        private bool CheckProjectEvals()
        {
            for (int i = 0; i < 9; i++)
                if (_projectEvalDataArray[i][0] <= 0 || _projectEvalDataArray[i][0] > 5)
                    return false;
            return true;
        }

        private void DataGridProjectEvalsMenuItemSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CheckProjectEvals())
                {
                    _curProject.ProjetName = ProjectNameTextBox.Text;
                    _repo.CreateOrUpdate(_curProject);
                    for (int i = 0; i < 9; i++)
                        _curExpertEval.Evals.ElementAt(i).Val = _projectEvalDataArray[i][0];
                    _repo.CreateOrUpdate(_curExpertEval);
                    _repo.Save();
                    TextBoxAns.Text += "\n Сохранено!\n";
                }
                else
                {
                    MessageBox.Show("Value must be between 1 and 5");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "AHTUNG!");
            }

        }

        private void ButtonCompRefresh_Click(object sender, RoutedEventArgs e)
        {
            CompetSetUp();
        }

        private void ButtomCompCalc_Click(object sender, RoutedEventArgs e)
        {
            if (ProjectEvalOtherComboBox.SelectedItem == null) return;

            if (!CheckProjectEvals())
            {
                MessageBox.Show("Value must be between 1 and 5");
                return;
            }

            var otherVals = _repo.Projects[ProjectEvalOtherComboBox.SelectedIndex].ExpertEval.Evals.Select(u => u.Val).ToArray();

            var myVals = new int[9];
            for (int i = 0; i < 9; i++)
                myVals[i] = _projectEvalDataArray[i][0];
            _curProject.Competitivness = Competitiveness.Calculate(myVals, otherVals);
            if (_curProject.Competitivness < 1)
                TextBoxAns.Text = $"A(k) = {_curProject.Competitivness} \nРазработка с технической точки зрения не оправдана.";
            else
                TextBoxAns.Text = $"A(k) = {_curProject.Competitivness} \nРазработка с технической точки зрения оправдана.";
            _key = 0;
            _curProject.ProjetName = ProjectNameTextBox.Text;
            ShowColumnChart();

        }

        private void ButtonExpensesCalc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _curProject.DevCosts = Expenses.Calculate(double.Parse(TextBoxCapSalary.Text),
                    double.Parse(TextBoxSlaveSalary.Text),
                    double.Parse(TextBoxMateralCosts.Text), double.Parse(TextBoxTimeForSwDev.Text),
                    int.Parse(TextBoxEqNum.Text), TextBoxCj.Text.Split(' ').Select(u => double.Parse(u)).ToArray(),
                    TextBoxQj.Text.Split(' ').Select(u => double.Parse(u)).ToArray(),
                    double.Parse(TextBoxRepairRoomCost.Text),
                    double.Parse(TextBoxPackageBuyingCost.Text), double.Parse(TextBoxConLinesBuildCost.Text),
                    double.Parse(TextBoxInformBaseCreationCost.Text), double.Parse(TextBoxPersonalLearningCost.Text));
                TextBoxAns.Text = $"Затраты на разработку проекта составят {_curProject.DevCosts} грн.";
                _key = 1;
                _curProject.ProjetName = ProjectNameTextBox.Text;
                ShowColumnChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "AHTUNG!");
            }
        }

        private void ButtonExpensesSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _curProject.ProjetName = ProjectNameTextBox.Text;
                _curDevCost.Update(double.Parse(TextBoxCapSalary.Text),
                    double.Parse(TextBoxSlaveSalary.Text),
                    double.Parse(TextBoxMateralCosts.Text), double.Parse(TextBoxTimeForSwDev.Text),
                    int.Parse(TextBoxEqNum.Text), TextBoxCj.Text.Split(' ').Select(u => double.Parse(u)).ToArray(),
                    TextBoxQj.Text.Split(' ').Select(u => double.Parse(u)).ToArray(),
                    double.Parse(TextBoxRepairRoomCost.Text),
                    double.Parse(TextBoxPackageBuyingCost.Text), double.Parse(TextBoxConLinesBuildCost.Text),
                    double.Parse(TextBoxInformBaseCreationCost.Text), double.Parse(TextBoxPersonalLearningCost.Text));
                _repo.CreateOrUpdate(_curDevCost);
                _repo.CreateOrUpdate(_curProject);
                _repo.Save();
                TextBoxAns.Text += "\n Сохранено!\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "AHTUNG!");
            }
        }

        private void ButtonCostsCalc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _curProject.OpCosts = Costs.Calculate(int.Parse(TextBoxOpSlaveCount.Text),
                    TextBoxOpTimeT.Text.Split(' ').Select(u => double.Parse(u)).ToArray(),
                    TextBoxOpTimeZ.Text.Split(' ').Select(u => double.Parse(u)).ToArray(),
                    int.Parse(TextBoxOpEqCount.Text),
                    TextBoxOpYearNorm.Text.Split(' ').Select(u => double.Parse(u)).ToArray(),
                    TextBoxOpEqCost.Text.Split(' ').Select(u => double.Parse(u)).ToArray(),
                    TextBoxOpOneEqCount.Text.Split(' ').Select(u => double.Parse(u)).ToArray(),
                    TextBoxOpEqWorkTime.Text.Split(' ').Select(u => double.Parse(u)).ToArray(),
                    TextBoxOpEqPower.Text.Split(' ').Select(u => double.Parse(u)).ToArray());
                TextBoxAns.Text = $"Операционные затраты составят {_curProject.OpCosts} грн.";
                _key = 2;
                _curProject.ProjetName = ProjectNameTextBox.Text;
                ShowColumnChart();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "AHTUNG!");
            }
        }

        private void ButtonCostsSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _curProject.ProjetName = ProjectNameTextBox.Text;
                _curOpCost.Update(int.Parse(TextBoxOpSlaveCount.Text),
                    TextBoxOpTimeT.Text.Split(' ').Select(u => double.Parse(u)).ToArray(),
                    TextBoxOpTimeZ.Text.Split(' ').Select(u => double.Parse(u)).ToArray(),
                    int.Parse(TextBoxOpEqCount.Text),
                    TextBoxOpYearNorm.Text.Split(' ').Select(u => double.Parse(u)).ToArray(),
                    TextBoxOpEqCost.Text.Split(' ').Select(u => double.Parse(u)).ToArray(),
                    TextBoxOpOneEqCount.Text.Split(' ').Select(u => double.Parse(u)).ToArray(),
                    TextBoxOpEqWorkTime.Text.Split(' ').Select(u => double.Parse(u)).ToArray(),
                    TextBoxOpEqPower.Text.Split(' ').Select(u => double.Parse(u)).ToArray());
                _repo.CreateOrUpdate(_curOpCost);
                _repo.CreateOrUpdate(_curProject);
                _repo.Save();
                TextBoxAns.Text += "\n Сохранено!\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "AHTUNG!");
            }

        }

        private void ButtonEcoCalc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _curProject.EcoEffects = SignEcoEffect.Calculate(double.Parse(TextBoxNetCost1.Text),
                    double.Parse(TextBoxNetCost2.Text), double.Parse(TextBoxImpCost1.Text),
                    double.Parse(TextBoxImpCost2.Text));
                if (_curProject.Competitivness <= 0.33)
                    TextBoxAns.Text = $"E(f) = {_curProject.EcoEffects} \nРазработка и внедрение неэффективно.";
                else
                    TextBoxAns.Text = $"E(f) = {_curProject.EcoEffects} \nРазработка и внедрение эффективно.";
                _key = 3;
                _curProject.ProjetName = ProjectNameTextBox.Text;
                ShowColumnChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "AHTUNG!");
            }
        }

        private void ButtonEcoSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _curProject.ProjetName = ProjectNameTextBox.Text;
                _curEcoEffect.Update(double.Parse(TextBoxNetCost1.Text), double.Parse(TextBoxNetCost2.Text),
                    double.Parse(TextBoxImpCost1.Text), double.Parse(TextBoxImpCost2.Text));
                _repo.CreateOrUpdate(_curEcoEffect);
                _repo.CreateOrUpdate(_curProject);
                _repo.Save();
                TextBoxAns.Text += "\n Сохранено!\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "AHTUNG!");
            }
        }
    }
}