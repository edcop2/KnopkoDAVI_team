using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
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
using WpfApp1.Models;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly AppDbContext _context;
        private readonly Project _curProject;



        private ObservableCollection<Slave> _projectSlaves;
        private ObservableCollection<Slave> _exploitSlaves;
        private ObservableCollection<Material> _projectMaterial;
        private ObservableCollection<Equip> _totalEquips;
        private ObservableCollection<TwoColumnView> _totalCosts;
        private ObservableCollection<TwoColumnView> _exploitCosts;
        private ObservableCollection<Project> _otherProjects;
        private ObservableCollection<ThreeColumnView> _ecoEffect;
        private ObservableCollection<QualityAttributeView> _qualityAttributeView;






        public MainWindow()
        {
            InitializeComponent();
            _context = AppDbContext.Create();
            //     _repo = new ProjectRepo(context);
            _curProject = new Project();
            //SetUp();
            //ShowColumnChart();

            SlaveDataGridSetUp();
            _projectSlaves = new ObservableCollection<Slave>();
            SlaveDataGrid.ItemsSource = _projectSlaves;

            ExploitSlaveDataGridSetUp();
            _exploitSlaves = new ObservableCollection<Slave>();
            ExploitSlaveDataGrid.ItemsSource = _exploitSlaves;

            MaterialDataGridSetUp();
            _projectMaterial = new ObservableCollection<Material>();
            MaterialDataGrid.ItemsSource = _projectMaterial;

            EquipDataGridSetup();
            _totalEquips = new ObservableCollection<Equip>();
            EquipDataGrid.ItemsSource = _totalEquips;

            TotalCostDataGridSetup();
            _totalCosts = new ObservableCollection<TwoColumnView>();
            TotalCostsDataGrid.ItemsSource = _totalCosts;

            ExploitCostDataGridSetup();
            _exploitCosts = new ObservableCollection<TwoColumnView>();
            ExploitCostsDataGrid.ItemsSource = _exploitCosts;

            EcoEffectDataGridSetup();
            _ecoEffect = new ObservableCollection<ThreeColumnView>();
            EcoEffectDataGrid.ItemsSource = _ecoEffect;
            EcoEffectDataGrid.CanUserAddRows = false;
            EcoEffectDataGrid.AutoGenerateColumns = false;



            _otherProjects = new ObservableCollection<Project>(_context.Projects);
            OtherProjectComboBox.ItemsSource = _otherProjects;

            QADataGridSetup();
            _qualityAttributeView = new ObservableCollection<QualityAttributeView>(QualityAttributeView.DefaultData());
            QualityDataGrid.ItemsSource = _qualityAttributeView;
            QualityDataGrid.CanUserAddRows = false;
            QualityDataGrid.AutoGenerateColumns = false;

        }


        #region GridSetups



        public void QADataGridSetup()
        {
            var textCol = new DataGridTextColumn();
            var b = new Binding("Title");
            textCol.Binding = b;
            textCol.Header = "Показатели качества";
            QualityDataGrid.Columns.Add(textCol);

            textCol = new DataGridTextColumn();
            b = new Binding("Coef");
            textCol.Binding = b;
            textCol.Header = "Коэффициент весомости, B(j)";
            QualityDataGrid.Columns.Add(textCol);

            textCol = new DataGridTextColumn();
            b = new Binding("MyValue");
            textCol.Binding = b;
            textCol.Header = "X(j) проекта";
            QualityDataGrid.Columns.Add(textCol);

            textCol = new DataGridTextColumn();
            b = new Binding("MyQu");
            textCol.Binding = b;
            textCol.Header = "B(j) * X(j) проекта";
            QualityDataGrid.Columns.Add(textCol);

            textCol = new DataGridTextColumn();
            b = new Binding("OtherValue");
            textCol.Binding = b;
            textCol.Header = "X(j) аналога";
            QualityDataGrid.Columns.Add(textCol);

            textCol = new DataGridTextColumn();
            b = new Binding("OtherQu");
            textCol.Binding = b;
            textCol.Header = "B(j) * X(j) аналога";
            QualityDataGrid.Columns.Add(textCol);
        }


        public void EcoEffectDataGridSetup()
        {
            var textCol = new DataGridTextColumn();
            var b = new Binding("First");
            textCol.Binding = b;
            textCol.Header = "Характеристика";
            EcoEffectDataGrid.Columns.Add(textCol);

            textCol = new DataGridTextColumn();
            b = new Binding("Second");
            textCol.Binding = b;
            textCol.Header = "Продукт-аналог (базовый)";
            EcoEffectDataGrid.Columns.Add(textCol);

            textCol = new DataGridTextColumn();
            b = new Binding("Third");
            textCol.Binding = b;
            textCol.Header = "Разрабатываемый продукт";
            EcoEffectDataGrid.Columns.Add(textCol);
        }



        public void EquipDataGridSetup()
        {
            var textCol = new DataGridTextColumn();
            var b = new Binding("Title");
            textCol.Binding = b;
            textCol.Header = "Оборудование";
            EquipDataGrid.Columns.Add(textCol);

            textCol = new DataGridTextColumn();
            b = new Binding("Price");
            textCol.Binding = b;
            textCol.Header = "Стоимость, грн.";
            EquipDataGrid.Columns.Add(textCol);

            textCol = new DataGridTextColumn();
            b = new Binding("Count");
            textCol.Binding = b;
            textCol.Header = "Кол-во, шт.";
            EquipDataGrid.Columns.Add(textCol);

            textCol = new DataGridTextColumn();
            b = new Binding("DayNorm");
            textCol.Binding = b;
            textCol.Header = "Норматив среднесуточной загрузки, час./день";
            EquipDataGrid.Columns.Add(textCol);

            textCol = new DataGridTextColumn();
            b = new Binding("WorkTime");
            textCol.Binding = b;
            textCol.Header = "Время работы, час.";
            EquipDataGrid.Columns.Add(textCol);

            textCol = new DataGridTextColumn();
            b = new Binding("AmoKoef");
            textCol.Binding = b;
            textCol.Header = "Норма годовых амортизационных отчислений";
            EquipDataGrid.Columns.Add(textCol);

            textCol = new DataGridTextColumn();
            b = new Binding("ElectroPower");
            textCol.Binding = b;
            textCol.Header = "Мощность, кВт";
            EquipDataGrid.Columns.Add(textCol);

            textCol = new DataGridTextColumn();
            b = new Binding("RepairNorm");
            textCol.Binding = b;
            textCol.Header = "Норматив затрат на ремонт";
            EquipDataGrid.Columns.Add(textCol);


            textCol = new DataGridTextColumn();
            b = new Binding("LoadCoef");
            textCol.Binding = b;
            textCol.Header = "Коэффициент загрузки, дней/год";
            EquipDataGrid.Columns.Add(textCol);

            textCol = new DataGridTextColumn();
            b = new Binding("Total");
            textCol.Binding = b;
            textCol.Header = "Затраты на оборудование, грн.";
            EquipDataGrid.Columns.Add(textCol);

            textCol = new DataGridTextColumn();
            b = new Binding("TotalAmo");
            textCol.Binding = b;
            textCol.Header = "Амортизационные отчисления, грн.";
            EquipDataGrid.Columns.Add(textCol);

            textCol = new DataGridTextColumn();
            b = new Binding("TotalElectro");
            textCol.Binding = b;
            textCol.Header = "Затраты на силовую энергию, грн.";
            EquipDataGrid.Columns.Add(textCol);
            textCol = new DataGridTextColumn();

            b = new Binding("TotalRepair");
            textCol.Binding = b;
            textCol.Header = "Затраты на ремонт, грн.";
            EquipDataGrid.Columns.Add(textCol);
        }

        public void TotalCostDataGridSetup()
        {
            var textCol = new DataGridTextColumn();
            var b = new Binding("First");
            textCol.Binding = b;
            textCol.Header = "Статьи затрат";
            TotalCostsDataGrid.Columns.Add(textCol);

            textCol = new DataGridTextColumn();
            b = new Binding("Second");
            textCol.Binding = b;
            textCol.Header = "Сумма, грн";
            TotalCostsDataGrid.Columns.Add(textCol);
        }

        public void ExploitCostDataGridSetup()
        {
            var textCol = new DataGridTextColumn();
            var b = new Binding("First");
            textCol.Binding = b;
            textCol.Header = "Статьи затрат";
            ExploitCostsDataGrid.Columns.Add(textCol);

            textCol = new DataGridTextColumn();
            b = new Binding("Second");
            textCol.Binding = b;
            textCol.Header = "Сумма, грн";
            ExploitCostsDataGrid.Columns.Add(textCol);
        }

        public void MaterialDataGridSetUp()
        {
            var textCol = new DataGridTextColumn();
            var b = new Binding("Title");
            textCol.Binding = b;
            textCol.Header = "Материалы";
            MaterialDataGrid.Columns.Add(textCol);

            textCol = new DataGridTextColumn();
            b = new Binding("Measure");
            textCol.Binding = b;
            textCol.Header = "Ед. измерения";
            MaterialDataGrid.Columns.Add(textCol);

            textCol = new DataGridTextColumn();
            b = new Binding("Count");
            textCol.Binding = b;
            textCol.Header = "Требуемое кол-во";
            MaterialDataGrid.Columns.Add(textCol);

            textCol = new DataGridTextColumn();
            b = new Binding("Price");
            textCol.Binding = b;
            textCol.Header = "Цена за единицу, грн.";
            MaterialDataGrid.Columns.Add(textCol);


            textCol = new DataGridTextColumn();
            b = new Binding("Total");
            textCol.Binding = b;
            textCol.Header = "Сумма, грн.";
            MaterialDataGrid.Columns.Add(textCol);



        }

        public void ExploitSlaveDataGridSetUp()
        {
            var textCol = new DataGridTextColumn();
            var b = new Binding("Position");
            textCol.Binding = b;
            textCol.Header = "Должность";
            ExploitSlaveDataGrid.Columns.Add(textCol);

            textCol = new DataGridTextColumn();
            b = new Binding("Salary");
            textCol.Binding = b;
            textCol.Header = "Оклад, грн.";
            ExploitSlaveDataGrid.Columns.Add(textCol);

            textCol = new DataGridTextColumn();
            b = new Binding("OneDaySalary");
            textCol.Binding = b;
            textCol.Header = "Средняя ставка, грн.";
            ExploitSlaveDataGrid.Columns.Add(textCol);

            textCol = new DataGridTextColumn();
            b = new Binding("ExploitDays");
            textCol.Binding = b;
            textCol.Header = "Затраты времени на эксплуатацию, чел./дней";
            ExploitSlaveDataGrid.Columns.Add(textCol);

            textCol = new DataGridTextColumn();
            b = new Binding("ExploitPayment");
            textCol.Binding = b;
            textCol.Header = "ОЗП, грн.";
            ExploitSlaveDataGrid.Columns.Add(textCol);
        }


        public void SlaveDataGridSetUp()
        {
            var textCol = new DataGridTextColumn();
            var b = new Binding("Position");
            textCol.Binding = b;
            textCol.Header = "Должность";
            SlaveDataGrid.Columns.Add(textCol);

            textCol = new DataGridTextColumn();
            b = new Binding("Salary");
            textCol.Binding = b;
            textCol.Header = "Оклад, грн.";
            SlaveDataGrid.Columns.Add(textCol);

            textCol = new DataGridTextColumn();
            b = new Binding("OneDaySalary");
            textCol.Binding = b;
            textCol.Header = "Средняя ставка, грн.";
            SlaveDataGrid.Columns.Add(textCol);

            textCol = new DataGridTextColumn();
            b = new Binding("WorkDays");
            textCol.Binding = b;
            textCol.Header = "Затраты времени на разработку, чел./дней";
            SlaveDataGrid.Columns.Add(textCol);

            textCol = new DataGridTextColumn();
            b = new Binding("TotalPayment");
            textCol.Binding = b;
            textCol.Header = "ОЗП, грн.";
            SlaveDataGrid.Columns.Add(textCol);
        }


        #endregion


        //#region OLD



        //private void ShowColumnChart()
        //{
        //    var valueList = new List<KeyValuePair<string, double>>();
        //    if (_key == 0)
        //    {
        //        ColumnChart.Title = "Сравнение проектов по конкурентноспособности";
        //        if (_curProject.Competitivness != null)
        //            valueList.Add(new KeyValuePair<string, double>(_curProject.ProjetName, _curProject.Competitivness.Value));
        //        foreach (var project in _repo.Projects.FindAll(u => u.Competitivness != null && u.Id != _curProject.Id))
        //            valueList.Add(new KeyValuePair<string, double>(project.ProjetName, project.Competitivness.Value));
        //    }
        //    else if (_key == 1)
        //    {
        //        ColumnChart.Title = "Сравнение проектов по затртам на разработку";
        //        if (_curProject.DevCosts != null)
        //            valueList.Add(new KeyValuePair<string, double>(_curProject.ProjetName, _curProject.DevCosts.Value));
        //        foreach (var project in _repo.Projects.FindAll(u => u.DevCosts != null && u.Id != _curProject.Id))
        //            valueList.Add(new KeyValuePair<string, double>(project.ProjetName, project.DevCosts.Value));
        //    }
        //    else if (_key == 2)
        //    {
        //        ColumnChart.Title = "Сравнение проектов по эксплуатационым затратам";
        //        if (_curProject.OpCosts != null)
        //            valueList.Add(new KeyValuePair<string, double>(_curProject.ProjetName, _curProject.OpCosts.Value));
        //        foreach (var project in _repo.Projects.FindAll(u => u.OpCosts != null && u.Id != _curProject.Id))
        //            valueList.Add(new KeyValuePair<string, double>(project.ProjetName, project.OpCosts.Value));
        //    }
        //    else if (_key == 3)
        //    {
        //        ColumnChart.Title = "Сравнение проектов по по показателю экономическог эффекта";
        //        if (_curProject.EcoEffects != null)
        //            valueList.Add(new KeyValuePair<string, double>(_curProject.ProjetName, _curProject.EcoEffects.Value));
        //        foreach (var project in _repo.Projects.FindAll(u => u.EcoEffects != null && u.Id != _curProject.Id))
        //            valueList.Add(new KeyValuePair<string, double>(project.ProjetName, project.EcoEffects.Value));
        //    }
        //    ColumnChart.DataContext = valueList;
        //}




        //private void SetUp()
        //{
        //    _curDevCost = _curProject.DevCost;
        //    _curExpertEval = _curProject.ExpertEval;
        //    _curOpCost = _curProject.OpCost;
        //    _curEcoEffect = _curProject.EcoEffect;
        //    CompetSetUp();
        //}

        //private void CompetSetUp()
        //{
        //    ProjectNameTextBox.Text = _curProject.ProjetName;
        //    _projectEvalDataArray = new DataArray(9, 1);
        //    DataGridProjectEvals.ItemsSource = _projectEvalDataArray.Data.DefaultView;
        //    if (_curExpertEval.Any())
        //    {
        //        for (int i = 0; i < 9; i++)
        //            _projectEvalDataArray[i][0] = _curExpertEval.Evals.ElementAt(i).Val;
        //    }
        //    else
        //    {
        //        for (int i = 0; i < 9; i++)
        //            _projectEvalDataArray[i][0] = 0;
        //    }
        //    DataGridProjectEvals.CanUserAddRows = false;
        //    var otherProjects = _repo.Projects;
        //    ProjectEvalOtherComboBox.ItemsSource = otherProjects;
        //}

        //private void ProjectEvalOtherComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var items = _repo.Projects.Find(u => u.ProjetName ==
        //                                               ProjectEvalOtherComboBox.SelectedItem.ToString()).ExpertEval.Evals.ToList();
        //    _projectEvalOtherDataArray = new DataArray(9, 1);
        //    DataGridOtherProjectEvals.ItemsSource = _projectEvalOtherDataArray.Data.DefaultView;
        //    for (int i = 0; i < 9; i++)
        //        _projectEvalOtherDataArray[i][0] = items[i].Val;
        //    DataGridProjectEvals.CanUserAddRows = false;
        //}

        //private bool CheckProjectEvals()
        //{
        //    for (int i = 0; i < 9; i++)
        //        if (_projectEvalDataArray[i][0] <= 0 || _projectEvalDataArray[i][0] > 5)
        //            return false;
        //    return true;
        //}

        //private void DataGridProjectEvalsMenuItemSave_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (CheckProjectEvals())
        //        {
        //            _curProject.ProjetName = ProjectNameTextBox.Text;
        //            _repo.CreateOrUpdate(_curProject);
        //            for (int i = 0; i < 9; i++)
        //                _curExpertEval.Evals.ElementAt(i).Val = _projectEvalDataArray[i][0];
        //            _repo.CreateOrUpdate(_curExpertEval);
        //            _repo.Save();
        //            TextBoxAns.Text += "\n Сохранено!\n";
        //        }
        //        else
        //        {
        //            MessageBox.Show("Value must be between 1 and 5");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), "AHTUNG!");
        //    }

        //}

        //private void ButtonCompRefresh_Click(object sender, RoutedEventArgs e)
        //{
        //    CompetSetUp();
        //}

        //private void ButtomCompCalc_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ProjectEvalOtherComboBox.SelectedItem == null) return;

        //    if (!CheckProjectEvals())
        //    {
        //        MessageBox.Show("Value must be between 1 and 5");
        //        return;
        //    }

        //    var otherVals = _repo.Projects[ProjectEvalOtherComboBox.SelectedIndex].ExpertEval.Evals.Select(u => u.Val).ToArray();

        //    var myVals = new int[9];
        //    for (int i = 0; i < 9; i++)
        //        myVals[i] = _projectEvalDataArray[i][0];
        //    _curProject.Competitivness = Competitiveness.Calculate(myVals, otherVals);
        //    if (_curProject.Competitivness < 1)
        //        TextBoxAns.Text = $"A(k) = {_curProject.Competitivness} \nРазработка с технической точки зрения не оправдана.";
        //    else
        //        TextBoxAns.Text = $"A(k) = {_curProject.Competitivness} \nРазработка с технической точки зрения оправдана.";
        //    _key = 0;
        //    _curProject.ProjetName = ProjectNameTextBox.Text;
        //    ShowColumnChart();

        //}

        //private void ButtonExpensesCalc_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        _curProject.DevCosts = Expenses.Calculate(double.Parse(TextBoxCapSalary.Text),
        //            double.Parse(TextBoxSlaveSalary.Text),
        //            double.Parse(TextBoxMateralCosts.Text), double.Parse(TextBoxTimeForSwDev.Text),
        //            int.Parse(TextBoxEqNum.Text), TextBoxCj.Text.Split(' ').Select(u => double.Parse(u)).ToArray(),
        //            TextBoxQj.Text.Split(' ').Select(u => double.Parse(u)).ToArray(),
        //            double.Parse(TextBoxRepairRoomCost.Text),
        //            double.Parse(TextBoxPackageBuyingCost.Text), double.Parse(TextBoxConLinesBuildCost.Text),
        //            double.Parse(TextBoxInformBaseCreationCost.Text), double.Parse(TextBoxPersonalLearningCost.Text));
        //        TextBoxAns.Text = $"Затраты на разработку проекта составят {_curProject.DevCosts} грн.";
        //        _key = 1;
        //        _curProject.ProjetName = ProjectNameTextBox.Text;
        //        ShowColumnChart();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "AHTUNG!");
        //    }
        //}

        //private void ButtonExpensesSave_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        _curProject.ProjetName = ProjectNameTextBox.Text;
        //        _curDevCost.Update(double.Parse(TextBoxCapSalary.Text),
        //            double.Parse(TextBoxSlaveSalary.Text),
        //            double.Parse(TextBoxMateralCosts.Text), double.Parse(TextBoxTimeForSwDev.Text),
        //            int.Parse(TextBoxEqNum.Text), TextBoxCj.Text.Split(' ').Select(u => double.Parse(u)).ToArray(),
        //            TextBoxQj.Text.Split(' ').Select(u => double.Parse(u)).ToArray(),
        //            double.Parse(TextBoxRepairRoomCost.Text),
        //            double.Parse(TextBoxPackageBuyingCost.Text), double.Parse(TextBoxConLinesBuildCost.Text),
        //            double.Parse(TextBoxInformBaseCreationCost.Text), double.Parse(TextBoxPersonalLearningCost.Text));
        //        _repo.CreateOrUpdate(_curDevCost);
        //        _repo.CreateOrUpdate(_curProject);
        //        _repo.Save();
        //        TextBoxAns.Text += "\n Сохранено!\n";
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "AHTUNG!");
        //    }
        //}

        //private void ButtonCostsCalc_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        _curProject.OpCosts = Costs.Calculate(int.Parse(TextBoxOpSlaveCount.Text),
        //            TextBoxOpTimeT.Text.Split(' ').Select(u => double.Parse(u)).ToArray(),
        //            TextBoxOpTimeZ.Text.Split(' ').Select(u => double.Parse(u)).ToArray(),
        //            int.Parse(TextBoxOpEqCount.Text),
        //            TextBoxOpYearNorm.Text.Split(' ').Select(u => double.Parse(u)).ToArray(),
        //            TextBoxOpEqCost.Text.Split(' ').Select(u => double.Parse(u)).ToArray(),
        //            TextBoxOpOneEqCount.Text.Split(' ').Select(u => double.Parse(u)).ToArray(),
        //            TextBoxOpEqWorkTime.Text.Split(' ').Select(u => double.Parse(u)).ToArray(),
        //            TextBoxOpEqPower.Text.Split(' ').Select(u => double.Parse(u)).ToArray());
        //        TextBoxAns.Text = $"Операционные затраты составят {_curProject.OpCosts} грн.";
        //        _key = 2;
        //        _curProject.ProjetName = ProjectNameTextBox.Text;
        //        ShowColumnChart();

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "AHTUNG!");
        //    }
        //}

        //private void ButtonCostsSave_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        _curProject.ProjetName = ProjectNameTextBox.Text;
        //        _curOpCost.Update(int.Parse(TextBoxOpSlaveCount.Text),
        //            TextBoxOpTimeT.Text.Split(' ').Select(u => double.Parse(u)).ToArray(),
        //            TextBoxOpTimeZ.Text.Split(' ').Select(u => double.Parse(u)).ToArray(),
        //            int.Parse(TextBoxOpEqCount.Text),
        //            TextBoxOpYearNorm.Text.Split(' ').Select(u => double.Parse(u)).ToArray(),
        //            TextBoxOpEqCost.Text.Split(' ').Select(u => double.Parse(u)).ToArray(),
        //            TextBoxOpOneEqCount.Text.Split(' ').Select(u => double.Parse(u)).ToArray(),
        //            TextBoxOpEqWorkTime.Text.Split(' ').Select(u => double.Parse(u)).ToArray(),
        //            TextBoxOpEqPower.Text.Split(' ').Select(u => double.Parse(u)).ToArray());
        //        _repo.CreateOrUpdate(_curOpCost);
        //        _repo.CreateOrUpdate(_curProject);
        //        _repo.Save();
        //        TextBoxAns.Text += "\n Сохранено!\n";
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "AHTUNG!");
        //    }

        //}

        //private void ButtonEcoCalc_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        _curProject.EcoEffects = SignEcoEffect.Calculate(double.Parse(TextBoxNetCost1.Text),
        //            double.Parse(TextBoxNetCost2.Text), double.Parse(TextBoxImpCost1.Text),
        //            double.Parse(TextBoxImpCost2.Text));
        //        if (_curProject.Competitivness <= 0.33)
        //            TextBoxAns.Text = $"E(f) = {_curProject.EcoEffects} \nРазработка и внедрение неэффективно.";
        //        else
        //            TextBoxAns.Text = $"E(f) = {_curProject.EcoEffects} \nРазработка и внедрение эффективно.";
        //        _key = 3;
        //        _curProject.ProjetName = ProjectNameTextBox.Text;
        //        ShowColumnChart();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "AHTUNG!");
        //    }
        //}

        //private void ButtonEcoSave_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        _curProject.ProjetName = ProjectNameTextBox.Text;
        //        _curEcoEffect.Update(double.Parse(TextBoxNetCost1.Text), double.Parse(TextBoxNetCost2.Text),
        //            double.Parse(TextBoxImpCost1.Text), double.Parse(TextBoxImpCost2.Text));
        //        _repo.CreateOrUpdate(_curEcoEffect);
        //        _repo.CreateOrUpdate(_curProject);
        //        _repo.Save();
        //        TextBoxAns.Text += "\n Сохранено!\n";
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "AHTUNG!");
        //    }
        //}


        //#endregion



        private void OtherCostsSetup(out double repairCost, out double packetCost, out double lineCost,
            out double slaveTrainingCost, out double electroCost, out double machineTime, out double machineTimePrice,
            out double addPayment, out double socPayment, out double invoicesKoef)
        {
            if (!double.TryParse(TextBoxRepairСost.Text, out repairCost))
            {
                MessageBox.Show("Введите правильно стоимость ремонта");
            }

            if (repairCost < 0)
            {
                MessageBox.Show("Введите правильно стоимость ремонта");
            }

            if (!double.TryParse(TextBoxPacketСost.Text, out packetCost))
            {
                MessageBox.Show("Введите правильно стоимость приобритения типовых разработок");
            }

            if (packetCost < 0)
            {
                MessageBox.Show("Введите правильно стоимость приобритения типовых разработок");
            }


            if (!double.TryParse(TextBoxLineСost.Text, out lineCost))
            {
                MessageBox.Show("Введите правильно стоимость прокладки линий связи");
            }

            if (lineCost < 0)
            {
                MessageBox.Show("Введите правильно стоимость прокладки линий связи");
            }


            if (!double.TryParse(TextBoxSlaveTrainingСost.Text, out slaveTrainingCost))
            {
                MessageBox.Show("Введите правильно стоимость обучения кадров");
            }

            if (slaveTrainingCost < 0)
            {
                MessageBox.Show("Введите правильно стоимость обучения кадров");
            }


            if (!double.TryParse(TextBoxElectroСost.Text, out electroCost))
            {
                MessageBox.Show("Введите правильно стоимость электроэнергии");
            }

            if (electroCost < 0)
            {
                MessageBox.Show("Введите правильно стоимость электроэнергии");
            }


            if (!double.TryParse(TextBoxMachineTime.Text, out machineTime))
            {
                MessageBox.Show("Введите правильно необходимое машинное время");
            }

            if (machineTime < 0)
            {
                MessageBox.Show("Введите правильно необходимое машинное время");
            }


            if (!double.TryParse(TextBoxMachineTimePrice.Text, out machineTimePrice))
            {
                MessageBox.Show("Введите правильно стоимость часа машинного времени");
            }

            if (machineTimePrice < 0)
            {
                MessageBox.Show("Введите правильно стоимость часа машинного времени");
            }


            if (!double.TryParse(TextBoxAddPayment.Text, out addPayment))
            {
                MessageBox.Show("Введите правильно коэффициент доп. з/п");
            }

            if (addPayment < 0)
            {
                MessageBox.Show("Введите правильно коэффициент доп. з/п");
            }


            if (!double.TryParse(TextBoxSocPayment.Text, out socPayment))
            {
                MessageBox.Show("Введите правильно коэффициент соц. выплат");
            }

            if (socPayment < 0)
            {
                MessageBox.Show("Введите правильно коэффициент соц. выплат");
            }

            if (!double.TryParse(TextBoxInvoicesKOef.Text, out invoicesKoef))
            {
                MessageBox.Show("Введите правильно коэффициент накладных выплат");
            }

            if (invoicesKoef < 0)
            {
                MessageBox.Show("Введите правильно коэффициент накладных выплат");
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double repairCost, packetCost, lineCost, slaveTrainingCost, electroCost, machineTime, machineTimePrice, addPayment, socPayment, invoicesKoef;
            OtherCostsSetup(out repairCost, out packetCost, out lineCost, out slaveTrainingCost, out electroCost,
                out machineTime, out machineTimePrice, out addPayment, out socPayment, out invoicesKoef);

            try
            {
                _totalCosts.Clear();
                _exploitCosts.Clear();


                var implementCosts = repairCost + packetCost + lineCost + slaveTrainingCost + _totalEquips.Sum(u => u.Total);
                var machineTimeCosts = machineTimePrice * machineTime;

                _totalCosts.Add(TwoColumnView.Create("Основная заработная плата",
                    Math.Round(_projectSlaves.Sum(u => u.TotalPayment), 2).ToString()));
                _totalCosts.Add(TwoColumnView.Create("Дополнительная заработная плата",
                    Math.Round(_projectSlaves.Sum(u => u.TotalPayment) * addPayment, 2).ToString()));
                _totalCosts.Add(TwoColumnView.Create("Отчисления на социальные нужды",
                    Math.Round(_projectSlaves.Sum(u => u.TotalPayment) * socPayment, 2).ToString()));
                _totalCosts.Add(TwoColumnView.Create("Затраты на материалы",
                    Math.Round(_projectMaterial.Sum(u => u.Total), 2).ToString()));
                _totalCosts.Add(TwoColumnView.Create("Затраты на машинное время",
                    Math.Round(machineTimeCosts, 2).ToString()));
                var invoices = _totalCosts.Sum(u => double.Parse(u.Second)) * invoicesKoef;
                _totalCosts.Add(TwoColumnView.Create("Накладные расходы", Math.Round(invoices, 2).ToString()));
                var devTotal = _totalCosts.Sum(u => double.Parse(u.Second));
                _totalCosts.Add(TwoColumnView.Create("Вложения на проэктирование", Math.Round(devTotal, 2).ToString()));
                _totalCosts.Add(TwoColumnView.Create("Вложения на реализацию проекта", Math.Round(implementCosts, 2).ToString()));
                _curProject.CapCost = devTotal + implementCosts;
                _totalCosts.Add(TwoColumnView.Create("ИТОГО", Math.Round(implementCosts + devTotal, 2).ToString()));

                _exploitCosts.Add(TwoColumnView.Create("Основная и доп. з/п с отчислениями",
                    Math.Round(_exploitSlaves.Sum(u => u.ExploitPayment) * (1 + addPayment + socPayment), 2)
                        .ToString()));
                _exploitCosts.Add(TwoColumnView.Create("Амортизационные отчисления",
                    Math.Round(_totalEquips.Sum(u => u.TotalAmo), 2).ToString()));
                _exploitCosts.Add(TwoColumnView.Create("Затраты на электроэнергию",
                    Math.Round(_totalEquips.Sum(u => u.TotalElectro), 2).ToString()));
                _exploitCosts.Add(TwoColumnView.Create("Затраты на текущий ремонт",
                    Math.Round(_totalEquips.Sum(u => u.TotalRepair), 2).ToString()));
                _exploitCosts.Add(TwoColumnView.Create("Затраты на материалы",
                    Math.Round(_totalEquips.Sum(u => u.Price) * 0.01, 2).ToString()));
                _exploitCosts.Add(TwoColumnView.Create("Накладные расходы", Math.Round(invoices, 2).ToString()));
                var exploitTotal = _exploitCosts.Sum(u => double.Parse(u.Second));
                _exploitCosts.Add(TwoColumnView.Create("ИТОГО", Math.Round(exploitTotal, 2).ToString()));

                TotalCostsDataGrid.Items.Refresh();
                ExploitCostsDataGrid.Items.Refresh();

                _curProject.ImpCost = implementCosts;
                _curProject.DevCost = devTotal;
                _curProject.ExploitCost = exploitTotal;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "AHTUNG!");
            }
        }


        private void SlaveDataGridRefreshButton_Click(object sender, RoutedEventArgs e)
        {
            SlaveDataGrid.Items.Refresh();
            SlaveTotalTextBox.Text = Math.Round(_projectSlaves.Sum(u => u.TotalPayment), 2).ToString();
            DateTime startDate;
            if (DateTime.TryParse(TextBoxDateStart.Text, out startDate))
            {
                var endDate = startDate.AddDays(_projectSlaves.Max(u => u.WorkDays));
                TextBoxDateEnd.Text = endDate.ToShortDateString();
            }
        }
        private void ExploitSlaveDataGridRefreshButton_Click(object sender, RoutedEventArgs e)
        {
            ExploitSlaveDataGrid.Items.Refresh();
            ExploitSlaveTotalTextBox.Text = Math.Round(_exploitSlaves.Sum(u => u.ExploitPayment), 2).ToString();
        }

        private void MaterialDataGridRefreshButton_Click(object sender, RoutedEventArgs e)
        {
            MaterialDataGrid.Items.Refresh();
            MaterialTotalTextBox.Text = Math.Round(_projectMaterial.Sum(u => u.Total), 2).ToString();
        }

        private void SlaveDataGridAddRowButton_Click(object sender, RoutedEventArgs e)
        {
            _projectSlaves.Add(new Slave());
            SlaveDataGrid.Items.Refresh();
        }

        private void ExploitSlaveDataGridAddRowButton_Click(object sender, RoutedEventArgs e)
        {
            _exploitSlaves.Add(new Slave());
            ExploitSlaveDataGrid.Items.Refresh();
        }

        private void MaterialDataGridAddRowButton_Click(object sender, RoutedEventArgs e)
        {
            _projectMaterial.Add(new Material());
            MaterialDataGrid.Items.Refresh();

        }

        private void EquipDataGridRefreshButton_Click(object sender, RoutedEventArgs e)
        {
            EquipDataGrid.Items.Refresh();
            EquipTotalTextBox.Text = Math.Round(_totalEquips.Sum(u => u.Total), 2).ToString();
        }

        private void EquipDataGridAddRowButton_Click(object sender, RoutedEventArgs e)
        {
            _totalEquips.Add(new Equip());
            EquipDataGrid.Items.Refresh();
        }

        private void ButtonGlobalSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _curProject.ProjetName = ProjectNameTextBox.Text;
                var qualityAttributes =
                    _qualityAttributeView.Select(u => QualityAttribute.Create(u.Title, u.Coef, u.MyValue));
                _context.Projects.AddOrUpdate(_curProject);
                _context.QualityAttributes.RemoveRange(
                    _context.QualityAttributes.Where(u => u.ProjectId == _curProject.Id));
                _context.QualityAttributes.AddRange(qualityAttributes);
                _context.Equips.RemoveRange(_context.Equips.Where(u => u.ProjectId == _curProject.Id));
                _context.Equips.AddRange(_totalEquips);
                _context.Materials.RemoveRange(_context.Materials.Where(u => u.ProjectId == _curProject.Id));
                _context.Materials.AddRange(_projectMaterial);
                _context.Slaves.RemoveRange(_context.Slaves.Where(u => u.ProjectId == _curProject.Id));
                _context.Slaves.AddRange(_projectSlaves);
                _context.Slaves.AddRange(_exploitSlaves);
                _context.SaveChanges();
                MessageBox.Show("Успешно сохранено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("При сохранении возникла ошибка!");
            }
        }

        private void EcoEffectButon_Click(object sender, RoutedEventArgs e)
        {
            if (OtherProjectComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите проект-аналог!");
                return;
            }
            try
            {
                var otherProject = _context.Projects.ToList()[OtherProjectComboBox.SelectedIndex];
                if (otherProject != null)
                {
                    _ecoEffect.Clear();
                    _ecoEffect.Add(ThreeColumnView.Create("Себестоимость (текущие эксплуатационные затраты), грн",
                        Math.Round(otherProject.ExploitCost, 2).ToString(),
                        Math.Round(_curProject.ExploitCost, 2).ToString()));
                    _ecoEffect.Add(ThreeColumnView.Create("Суммарные затраты, связанные с внедрением проекта, грн",
                        Math.Round(otherProject.CapCost, 2).ToString(),
                        Math.Round(_curProject.CapCost, 2).ToString()));
                    var myOneCost = _curProject.ExploitCost + 0.33 * _curProject.CapCost;
                    var otherOneCost = otherProject.ExploitCost + 0.33 * otherProject.CapCost;
                    _ecoEffect.Add(ThreeColumnView.Create("Приведенные затраты на единицу работ, грн",
                        Math.Round(otherOneCost, 2).ToString(),
                        Math.Round(myOneCost, 2).ToString()));
                    var ecoEffect = otherOneCost * _curProject.ProjectTechLevel - myOneCost;
                    EcoEffectTextBox.Text = Math.Round(ecoEffect, 2).ToString();
                    var ecoTime = otherOneCost / ecoEffect;
                    EcoTimeTextBox.Text = Math.Round(ecoTime, 2).ToString();
                    if (ecoTime != 0)
                    {
                        var actualEcoEffect = 1.0 / ecoTime;
                        ActualEcoEffectTextBox.Text = Math.Round(actualEcoEffect, 2).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "AHTUNG!");
            }



        }

        private void QualityDataGridRefreshButton_Click(object sender, RoutedEventArgs e)
        {
            if (_qualityAttributeView.Sum(u => u.Coef) != 1)
            {
                MessageBox.Show("Сумма всех коэффициентов должна быть ровна 1");
                return;
            }
            QualityDataGrid.Items.Refresh();
            var myQa = _qualityAttributeView.Sum(u => u.MyQu);
            var otherQa = _qualityAttributeView.Sum(u => u.OtherQu);
            QualityTotalProjectTextBox.Text = myQa.ToString();
            QualityTotalOtherTextBox.Text = otherQa.ToString();
            if (otherQa != 0)
            {
                var techLevel = myQa / otherQa;
                QualityTotalTextBox.Text = Math.Round(techLevel, 2).ToString();
                _curProject.ProjectTechLevel = techLevel;
            }
        }

        private void QualityDataGridLoadButton_Click(object sender, RoutedEventArgs e)
        {
            if (OtherProjectComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите проект-аналог!");
                return;
            }
            var otherProject = _context.Projects.ToList()[OtherProjectComboBox.SelectedIndex];
            if (otherProject != null && otherProject.QualityAttributes.Count != 0)
            {
                for (int i = 0; i < 9; i++)
                {
                    _qualityAttributeView[i].OtherValue = otherProject.QualityAttributes.ElementAt(i).MyValue;
                }
                QualityDataGrid.Items.Refresh();
            }
        }

        private void MenuItemMaterialDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var index = MaterialDataGrid.SelectedIndex;
                MaterialDataGrid.CommitEdit();
                _projectMaterial.RemoveAt(index);
                MaterialDataGrid.Items.Refresh();
            }
            catch { }
        }

        private void MenuItemEquipDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var index = EquipDataGrid.SelectedIndex;
                EquipDataGrid.CommitEdit();
                _totalEquips.RemoveAt(index);
                EquipDataGrid.Items.Refresh();
            }
            catch { }
        }

        private void MenuItemEmployeeDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var index = SlaveDataGrid.SelectedIndex;
                SlaveDataGrid.CommitEdit();
                _projectSlaves.RemoveAt(index);
                SlaveDataGrid.Items.Refresh();
            }
            catch { }
        }

        private void MenuItemExploitEmployeeDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var index = ExploitSlaveDataGrid.SelectedIndex;
                ExploitSlaveDataGrid.CommitEdit();
                _exploitSlaves.RemoveAt(index);
                ExploitSlaveDataGrid.Items.Refresh();
            }
            catch { }
        }
    }
}