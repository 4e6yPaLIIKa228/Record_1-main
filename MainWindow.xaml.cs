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
using System.Data.SQLite;
using Record.Connection;
using System.Data;

namespace Record
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DisplayData();
            CbFill();
            SetFormatting();
            Groubs();
            CbMonth.IsEnabled = false;
        }
        private void SetFormatting()
        {
            //this.DGStudents.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.DGStudents.Columns[0].IsReadOnly = true;
            //this.DGStudents.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            //this.DGStudents.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        public void DisplayData()
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
            {
                try
                {
          //          connection.Open();
          //          string query = $@"SELECT Devices.ID, Types.Class, Titles.Title, Devices.Number, Conditions.Condition ,NumberKabs.NumKab ,Devices.StartWork,Users.Login, Brands.Brand, Models.Model
          //                              FROM Devices JOIN  Types
          //                              ON Devices.IDType = Types.ID
          //                              JOIN  Conditions
          //                              ON Devices.IDCondition = Conditions.ID
          //                              JOIN  NumberKabs
          //                              ON Devices.IDKabuneta = NumberKabs.ID
          //                              JOIN Titles
          //                              ON Devices.IDTitle = Titles.ID
										//JOIN Users
										//ON Devices.IDAddUser = Users.ID
										//JOIN Brands
										//ON Devices.IDBrand = Brands.ID					
										//JOIN Models
										//ON Devices.IDModel = Models.ID;";
          //          SQLiteCommand cmd = new SQLiteCommand(query, connection);
          //          DataTable DT = new DataTable("Devices");
          //          SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
          //          SDA.Fill(DT);
          //          DGStudents.ItemsSource = DT.DefaultView;
          //          cmd.ExecuteNonQuery();
                    //Login.Text = $"Ваш логин: " + Saver.Login;


                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
        }
        public void CbFill()  //Данные для комбобоксов 
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
            {
                try
                {
                    connection.Open();
                    string query1 = $@"SELECT * FROM Groups"; // Группы
                    string query2 = $@"SELECT * FROM Months"; // Типы

                    //----------------------------------------------
                    SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                    SQLiteCommand cmd2 = new SQLiteCommand(query2, connection);

                    //----------------------------------------------
                    SQLiteDataAdapter SDA1 = new SQLiteDataAdapter(cmd1);
                    SQLiteDataAdapter SDA2 = new SQLiteDataAdapter(cmd2);
                    //----------------------------------------------
                    DataTable dt1 = new DataTable("Groups");
                    DataTable dt2 = new DataTable("Months");
                    //----------------------------------------------
                    SDA1.Fill(dt1);
                    SDA2.Fill(dt2);
                    //----------------------------------------------
                    CbGroups.ItemsSource = dt1.DefaultView;
                    CbGroups.DisplayMemberPath = "NameGroup";
                    CbGroups.SelectedValuePath = "ID";
                    //----------------------------------------------
                    CbMonth.ItemsSource = dt2.DefaultView;
                    CbMonth.DisplayMemberPath = "Month";
                    CbMonth.SelectedValuePath = "ID";


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void CbGroups_SelectionChanged(object sender, SelectionChangedEventArgs e) //Группы
        {
            if (CbGroups.SelectedIndex != -1) { CbMonth.IsEnabled = true; } else { CbMonth.IsEnabled = true; }
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                    // MessageBox.Show(CbGroups.DisplayMemberPath.ToString());//нет
                    // MessageBox.Show(CbGroups.Items[CbGroups.SelectedIndex].ToString());// почти
                    String str = ((DataRowView)CbGroups.SelectedItem)["NameGroup"].ToString(); //Вывел
                    bool resultClass = int.TryParse(CbGroups.SelectedValue.ToString(), out Saver.idGroup);
                    // MessageBox.Show(str);
                    Saver.groups = str ;
                    ////MessageBox.Show (Saver.groups);
                   
                    //connection.Open();
                    ////string query = $@"SELECT Groups.ID, Groups.NameGroup, Students.NSM
                    ////            FROM Groups JOIN Students ON Groups.IDStudent = Students.ID 
                    ////            Where Groups.NameGroup = '{str}';";
                    //string query = $@"SELECT Students.NSM,Groups.NameGroup, Months.Month, Traffics.Day1,Traffics.Day2 FROM Students 
                    //            JOIN Months on Traffics.ID = Months.ID
                    //            JOIN Traffics on Students.IDTraffic = Traffics.ID
                    //            JOIN Groups on Students.IDGroup = Groups.ID
                    //            WHERE Groups.NameGroup = '{Saver.groups}' and Traffics.IDMonth = '{Saver.month}';";
                    //SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    ////cmd.Parameters.AddWithValue("@Number", TbNumber.Text);
                    //DataTable DT = new DataTable("Students");
                    //SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                    //SDA.Fill(DT);
                    //DGStudents.ItemsSource = DT.DefaultView;
                    //cmd.ExecuteNonQuery();
                    //Traffics();

                }
            }
            catch (Exception exp) { MessageBox.Show(exp.Message); }
        }
        public void Groubs()//Студенты
        {
            
        }
        public void Traffics()//дни
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                    string query = $@"SELECT Students.NSM,Traffics.Day1 FROM Traffics
                                    JOIN Students On Traffics.IDStudents = Students.ID";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    //cmd.Parameters.AddWithValue("@Number", TbNumber.Text);
                    DataTable DT = new DataTable("Students");
                    SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                    SDA.Fill(DT);
                    DGStudents.ItemsSource = DT.DefaultView;
                    cmd.ExecuteNonQuery();

                }
            }
            catch (Exception exp) { MessageBox.Show(exp.Message); }


            
        }

        private void CbMonth_SelectionChanged(object sender, SelectionChangedEventArgs e) //Месяцы
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                    // MessageBox.Show(CbGroups.DisplayMemberPath.ToString());//нет
                    // MessageBox.Show(CbGroups.Items[CbGroups.SelectedIndex].ToString());// почти
                    String str = ((DataRowView)CbMonth.SelectedItem)["Month"].ToString(); //Вывел
                    bool resultClass = int.TryParse(CbMonth.SelectedValue.ToString(), out Saver.idmonth);
                    Saver.month = str;
                    //MessageBox.Show(Saver.month);

                    //connection.Open();
                    //string query = $@"SELECT Students.NSM,Groups.NameGroup, Months.Month, Traffics.Day1,Traffics.Day2 FROM Students 
                    //            JOIN Months on Traffics.ID = Months.ID
                    //            JOIN Traffics on Students.IDTraffic = Traffics.ID
                    //            JOIN Groups on Students.IDGroup = Groups.ID
                    //            WHERE Groups.NameGroup = '{Saver.groups}' and Traffics.IDMonth = '{Saver.month}';";
                    //SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    ////cmd.Parameters.AddWithValue("@Number", TbNumber.Text);
                    //DataTable DT = new DataTable("Students");
                    //SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                    //SDA.Fill(DT);
                    //DGStudents.ItemsSource = DT.DefaultView;
                    //cmd.ExecuteNonQuery();
                }
            }
            catch (Exception exp) { MessageBox.Show(exp.Message); }
        }

        private void Search()//Поиск
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                     connection.Open();
                    string query = $@"SELECT Students.ID,Students.NSM,Groups.NameGroup, Months.Month, Traffics.Day1,Traffics.Day2,Traffics.Day3,Traffics.Day4,Traffics.Day5,Traffics.Day6,Traffics.Day7,Traffics.Day8,Traffics.Day9,Traffics.Day10,Traffics.Day11,Traffics.Day12,Traffics.Day13,Traffics.Day14,Traffics.Day15,Traffics.Day16,Traffics.Day17,Traffics.Day18,Traffics.Day19,Traffics.Day20,Traffics.Day21,Traffics.Day22,Traffics.Day23,Traffics.Day24,Traffics.Day25,Traffics.Day26,Traffics.Day27,Traffics.Day28,Traffics.Day29,Traffics.Day30,Traffics.Day31  FROM Students  
                                        JOIN Traffics on Students.IDTraffic = Traffics.ID
                                        JOIN Groups on Students.IDGroup = Groups.ID
                                        JOIN Months on Traffics.IDMonth = Months.ID
                                        WHERE  Groups.ID = '{Saver.idGroup}' and Traffics.IDMonth = '{Saver.idmonth}'";//Дописать Дни
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    DataTable DT = new DataTable("Students");
                    SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                    SDA.Fill(DT);
                    DGStudents.ItemsSource = DT.DefaultView;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception exp) 

            { MessageBox.Show(exp.Message); }

        }
        private void BtSearch_Click(object sender, RoutedEventArgs e)//поиск
        {
            if ((CbGroups.SelectedIndex != -1) && (CbMonth.SelectedIndex != -1)) 
            {
                Search();
            }
            else
            {
                MessageBox.Show("Выберите два критерия поиска");
            }
        }

        private void BtSave_Click(object sender, RoutedEventArgs e)//обновляем данные при их изменениях(по кнопке)
        {
            //Stroka();
        }

        private void DGStudents_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)//обновляем данные при их изменениях(enter, ....
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                    if (DGStudents.SelectedItems.Count > 0)
                    {
                        //DGStudents.Columns[1].IsReadOnly = true
                        DataRowView row = (DataRowView)DGStudents.SelectedItems[0];
                        Stroka();
                        TbNumber.Text = row["Day1"].ToString();
                        Saver.D1 = row["Day1"].ToString(); Saver.D2 = row["Day2"].ToString(); Saver.D3 = row["Day3"].ToString(); Saver.D4 = row["Day4"].ToString(); Saver.D5 = row["Day5"].ToString(); Saver.D6 = row["Day6"].ToString(); Saver.D7 = row["Day7"].ToString(); Saver.D8 = row["Day8"].ToString(); Saver.D8 = row["Day8"].ToString(); Saver.D9 = row["Day9"].ToString(); Saver.D10 = row["Day10"].ToString(); Saver.D11 = row["Day11"].ToString(); Saver.D12 = row["Day12"].ToString(); Saver.D13 = row["Day13"].ToString(); Saver.D14 = row["Day14"].ToString(); Saver.D15 = row["Day15"].ToString(); Saver.D16 = row["Day16"].ToString(); Saver.D17 = row["Day17"].ToString(); Saver.D18 = row["Day18"].ToString(); Saver.D19 = row["Day19"].ToString(); Saver.D20 = row["Day20"].ToString(); Saver.D21 = row["Day21"].ToString(); Saver.D22 = row["Day22"].ToString(); Saver.D23 = row["Day23"].ToString(); Saver.D24 = row["Day24"].ToString(); Saver.D25 = row["Day25"].ToString(); Saver.D26 = row["Day26"].ToString(); Saver.D27 = row["Day27"].ToString(); Saver.D28 = row["Day28"].ToString(); Saver.D29 = row["Day29"].ToString();
                        Saver.D30 = row["Day30"].ToString(); Saver.D31 = row["Day31"].ToString();
                        Saver.i = 0;

                        //if (Saver.D1 == "н" | Saver.D1 == "б" | Saver.D1 == "п" | Saver.D1 == "") { /*Saver.i++;*/ MessageBox.Show("Верно1"); } else { MessageBox.Show("Введите 'н' или 'б' или 'п' или ничего"); Search(); }

                        try
                        //for (int i = 0; i < 1; i++;)
                        {
                            // Saver.i = 0;
                            //UpdateInfo(); MessageBox.Show("Успех")
                            connection.Open();
                            if (Saver.D1 == "н" | Saver.D1 == "б" | Saver.D1 == "п" | Saver.D1 == "") {  string query = $@"UPDATE  Traffics SET  Day1 = '{Saver.D1}' WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' "; SQLiteCommand cmd = new SQLiteCommand(query, connection); cmd.ExecuteNonQuery(); } else {MessageBox.Show("Введите 'н' или 'б' или 'п' или ничего"); Search(); }
                            if (Saver.D2 == "н" | Saver.D2 == "б" | Saver.D2 == "п" | Saver.D2 == "") {  string query = $@"UPDATE  Traffics SET  Day2 = '{Saver.D2}' WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' "; SQLiteCommand cmd = new SQLiteCommand(query, connection); cmd.ExecuteNonQuery(); } else { MessageBox.Show("Введите 'н' или 'б' или 'п' или ничего"); Search(); }
                            if (Saver.D3 == "н" | Saver.D3 == "б" | Saver.D3 == "п" | Saver.D3 == "") {  string query = $@"UPDATE  Traffics SET  Day3 = '{Saver.D3}' WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' "; SQLiteCommand cmd = new SQLiteCommand(query, connection); cmd.ExecuteNonQuery(); } else { MessageBox.Show("Введите 'н' или 'б' или 'п' или ничего"); Search(); }
                            if (Saver.D4 == "н" | Saver.D4 == "б" | Saver.D4 == "п" | Saver.D4 == "") {  string query = $@"UPDATE  Traffics SET  Day4 = '{Saver.D4}' WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' "; SQLiteCommand cmd = new SQLiteCommand(query, connection); cmd.ExecuteNonQuery(); } else { MessageBox.Show("Введите 'н' или 'б' или 'п' или ничего"); Search(); }
                            if (Saver.D5 == "н" | Saver.D5 == "б" | Saver.D5 == "п" | Saver.D5 == "") {  string query = $@"UPDATE  Traffics SET  Day5 = '{Saver.D5}' WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' "; SQLiteCommand cmd = new SQLiteCommand(query, connection); cmd.ExecuteNonQuery(); } else { MessageBox.Show("Введите 'н' или 'б' или 'п' или ничего"); Search(); }
                            if (Saver.D6 == "н" | Saver.D6 == "б" | Saver.D6 == "п" | Saver.D6 == "") {  string query = $@"UPDATE  Traffics SET  Day6 = '{Saver.D6}' WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' "; SQLiteCommand cmd = new SQLiteCommand(query, connection); cmd.ExecuteNonQuery(); } else { MessageBox.Show("Введите 'н' или 'б' или 'п' или ничего"); Search(); }
                            if (Saver.D7 == "н" | Saver.D7 == "б" | Saver.D7 == "п" | Saver.D7 == "") {  string query = $@"UPDATE  Traffics SET  Day7 = '{Saver.D7}' WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' "; SQLiteCommand cmd = new SQLiteCommand(query, connection); cmd.ExecuteNonQuery(); } else { MessageBox.Show("Введите 'н' или 'б' или 'п' или ничего"); Search(); }
                            if (Saver.D8 == "н" | Saver.D8 == "б" | Saver.D8 == "п" | Saver.D8 == "") {  string query = $@"UPDATE  Traffics SET  Day8 = '{Saver.D8}' WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' "; SQLiteCommand cmd = new SQLiteCommand(query, connection); cmd.ExecuteNonQuery(); } else { MessageBox.Show("Введите 'н' или 'б' или 'п' или ничего"); Search(); }
                            if (Saver.D9 == "н" | Saver.D9 == "б" | Saver.D9 == "п" | Saver.D9 == "") {  string query = $@"UPDATE  Traffics SET  Day9 = '{Saver.D9}' WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' "; SQLiteCommand cmd = new SQLiteCommand(query, connection); cmd.ExecuteNonQuery(); } else { MessageBox.Show("Введите 'н' или 'б' или 'п' или ничего"); Search(); }
                            if (Saver.D10 == "н" | Saver.D10 == "б" | Saver.D10 == "п" | Saver.D10 == "") {  string query = $@"UPDATE  Traffics SET  Day10 = '{Saver.D10}' WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' "; SQLiteCommand cmd = new SQLiteCommand(query, connection); cmd.ExecuteNonQuery(); } else { MessageBox.Show("Введите 'н' или 'б' или 'п' или ничего"); Search(); }
                            if (Saver.D11 == "н" | Saver.D11 == "б" | Saver.D11 == "п" | Saver.D11 == "") {  string query = $@"UPDATE  Traffics SET  Day11 = '{Saver.D11}' WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' "; SQLiteCommand cmd = new SQLiteCommand(query, connection); cmd.ExecuteNonQuery(); } else { MessageBox.Show("Введите 'н' или 'б' или 'п' или ничего"); Search(); }
                            if (Saver.D12 == "н" | Saver.D12 == "б" | Saver.D12 == "п" | Saver.D12 == "") {  string query = $@"UPDATE  Traffics SET  Day12 = '{Saver.D12}' WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' "; SQLiteCommand cmd = new SQLiteCommand(query, connection); cmd.ExecuteNonQuery(); } else { MessageBox.Show("Введите 'н' или 'б' или 'п' или ничего"); Search(); }
                            if (Saver.D13 == "н" | Saver.D13 == "б" | Saver.D13 == "п" | Saver.D13 == "") {  string query = $@"UPDATE  Traffics SET  Day13 = '{Saver.D13}' WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' "; SQLiteCommand cmd = new SQLiteCommand(query, connection); cmd.ExecuteNonQuery(); } else { MessageBox.Show("Введите 'н' или 'б' или 'п' или ничего"); Search(); }
                            if (Saver.D14 == "н" | Saver.D14 == "б" | Saver.D14 == "п" | Saver.D14 == "") {string query = $@"UPDATE  Traffics SET  Day14 = '{Saver.D14}' WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' "; SQLiteCommand cmd = new SQLiteCommand(query, connection); cmd.ExecuteNonQuery(); } else { MessageBox.Show("Введите 'н' или 'б' или 'п' или ничего"); Search(); }
                            if (Saver.D15 == "н" | Saver.D15 == "б" | Saver.D15 == "п" | Saver.D15 == "") {  string query = $@"UPDATE  Traffics SET  Day15 = '{Saver.D15}' WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' "; SQLiteCommand cmd = new SQLiteCommand(query, connection); cmd.ExecuteNonQuery(); } else { MessageBox.Show("Введите 'н' или 'б' или 'п' или ничего"); Search(); }
                            if (Saver.D16 == "н" | Saver.D16 == "б" | Saver.D16 == "п" | Saver.D16 == "") {  string query = $@"UPDATE  Traffics SET  Day16 = '{Saver.D16}' WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' "; SQLiteCommand cmd = new SQLiteCommand(query, connection); cmd.ExecuteNonQuery(); } else { MessageBox.Show("Введите 'н' или 'б' или 'п' или ничего"); Search(); }
                            if (Saver.D17 == "н" | Saver.D17 == "б" | Saver.D17 == "п" | Saver.D17 == "") {  string query = $@"UPDATE  Traffics SET  Day17 = '{Saver.D17}' WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' "; SQLiteCommand cmd = new SQLiteCommand(query, connection); cmd.ExecuteNonQuery(); } else { MessageBox.Show("Введите 'н' или 'б' или 'п' или ничего"); Search(); }
                            if (Saver.D18 == "н" | Saver.D18 == "б" | Saver.D18 == "п" | Saver.D18 == "") {  string query = $@"UPDATE  Traffics SET  Day18 = '{Saver.D18}' WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' "; SQLiteCommand cmd = new SQLiteCommand(query, connection); cmd.ExecuteNonQuery(); } else { MessageBox.Show("Введите 'н' или 'б' или 'п' или ничего"); Search(); }
                            if (Saver.D19 == "н" | Saver.D19 == "б" | Saver.D19 == "п" | Saver.D19 == "") {  string query = $@"UPDATE  Traffics SET  Day19 = '{Saver.D19}' WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' "; SQLiteCommand cmd = new SQLiteCommand(query, connection); cmd.ExecuteNonQuery(); } else { MessageBox.Show("Введите 'н' или 'б' или 'п' или ничего"); Search(); }
                            if (Saver.D20 == "н" | Saver.D20 == "б" | Saver.D20 == "п" | Saver.D20 == "") {  string query = $@"UPDATE  Traffics SET  Day20 = '{Saver.D20}' WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' "; SQLiteCommand cmd = new SQLiteCommand(query, connection); cmd.ExecuteNonQuery(); } else { MessageBox.Show("Введите 'н' или 'б' или 'п' или ничего"); Search(); }
                            if (Saver.D21 == "н" | Saver.D21 == "б" | Saver.D21 == "п" | Saver.D21 == "") {  string query = $@"UPDATE  Traffics SET  Day21 = '{Saver.D22}' WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' "; SQLiteCommand cmd = new SQLiteCommand(query, connection); cmd.ExecuteNonQuery(); } else { MessageBox.Show("Введите 'н' или 'б' или 'п' или ничего"); Search(); }
                            if (Saver.D22 == "н" | Saver.D22 == "б" | Saver.D22 == "п" | Saver.D22 == "") {  string query = $@"UPDATE  Traffics SET  Day22 = '{Saver.D22}' WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' "; SQLiteCommand cmd = new SQLiteCommand(query, connection); cmd.ExecuteNonQuery(); } else { MessageBox.Show("Введите 'н' или 'б' или 'п' или ничего"); Search(); }
                            if (Saver.D23 == "н" | Saver.D23 == "б" | Saver.D23 == "п" | Saver.D23 == "") { string query = $@"UPDATE  Traffics SET  Day23 = '{Saver.D23}' WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' "; SQLiteCommand cmd = new SQLiteCommand(query, connection); cmd.ExecuteNonQuery(); } else { MessageBox.Show("Введите 'н' или 'б' или 'п' или ничего"); Search(); }
                            if (Saver.D24 == "н" | Saver.D24 == "б" | Saver.D24 == "п" | Saver.D24 == "") { string query = $@"UPDATE  Traffics SET  Day24 = '{Saver.D24}' WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' "; SQLiteCommand cmd = new SQLiteCommand(query, connection); cmd.ExecuteNonQuery(); } else { MessageBox.Show("Введите 'н' или 'б' или 'п' или ничего"); Search(); }
                            if (Saver.D25 == "н" | Saver.D25 == "б" | Saver.D25 == "п" | Saver.D25 == "") {  string query = $@"UPDATE  Traffics SET  Day25 = '{Saver.D25}' WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' "; SQLiteCommand cmd = new SQLiteCommand(query, connection); cmd.ExecuteNonQuery(); } else { MessageBox.Show("Введите 'н' или 'б' или 'п' или ничего"); Search(); }
                            if (Saver.D26 == "н" | Saver.D26 == "б" | Saver.D26 == "п" | Saver.D26 == "") {  string query = $@"UPDATE  Traffics SET  Day26 = '{Saver.D26}' WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' "; SQLiteCommand cmd = new SQLiteCommand(query, connection); cmd.ExecuteNonQuery(); } else { MessageBox.Show("Введите 'н' или 'б' или 'п' или ничего"); Search(); }
                            if (Saver.D27 == "н" | Saver.D27 == "б" | Saver.D27 == "п" | Saver.D27 == "") {  string query = $@"UPDATE  Traffics SET  Day27 = '{Saver.D27}' WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' "; SQLiteCommand cmd = new SQLiteCommand(query, connection); cmd.ExecuteNonQuery(); } else { MessageBox.Show("Введите 'н' или 'б' или 'п' или ничего"); Search(); }
                            if (Saver.D28 == "н" | Saver.D28 == "б" | Saver.D28 == "п" | Saver.D28 == "") {  string query = $@"UPDATE  Traffics SET  Day28 = '{Saver.D28}' WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' "; SQLiteCommand cmd = new SQLiteCommand(query, connection); cmd.ExecuteNonQuery(); } else { MessageBox.Show("Введите 'н' или 'б' или 'п' или ничего"); Search(); }
                            if (Saver.D29 == "н" | Saver.D29 == "б" | Saver.D29 == "п" | Saver.D29 == "") { string query = $@"UPDATE  Traffics SET  Day29 = '{Saver.D29}' WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' "; SQLiteCommand cmd = new SQLiteCommand(query, connection); cmd.ExecuteNonQuery(); } else { MessageBox.Show("Введите 'н' или 'б' или 'п' или ничего"); Search(); }
                            if (Saver.D30 == "н" | Saver.D30 == "б" | Saver.D30 == "п" | Saver.D30 == "") {  string query = $@"UPDATE  Traffics SET  Day30 = '{Saver.D30}' WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' "; SQLiteCommand cmd = new SQLiteCommand(query, connection); cmd.ExecuteNonQuery(); } else { MessageBox.Show("Введите 'н' или 'б' или 'п' или ничего"); Search(); }
                            if (Saver.D31 == "н" | Saver.D31 == "б" | Saver.D31 == "п" | Saver.D31 == "") {  string query = $@"UPDATE  Traffics SET  Day31 = '{Saver.D31}' WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' "; SQLiteCommand cmd = new SQLiteCommand(query, connection); cmd.ExecuteNonQuery(); } else { MessageBox.Show("Введите 'н' или 'б' или 'п' или ничего"); Search(); }
                            //Дописать до 31 дня

                            // if (Saver.D3 == "н" | Saver.D3 == "б" | Saver.D3 == "п" | Saver.D3 == "") { MessageBox.Show("Верно3"); } else {Search();}
                            // if (Saver.D4 == "н" | Saver.D4 == "б" | Saver.D4 == "п" | Saver.D4 == "") { MessageBox.Show("Верно4"); } else {Search();}
                            //if (Saver.D2 == "н" | Saver.D2 == "б" | Saver.D2 == "п" | Saver.D2 == "") { MessageBox.Show("Верно2"); } else { Search(); }
                            //if (Saver.D2 == "н" | Saver.D2 == "б" | Saver.D2 == "п" | Saver.D2 == "") { MessageBox.Show("Верно2"); } else { Search(); }
                            //if (Saver.D2 == "н" | Saver.D2 == "б" | Saver.D2 == "п" | Saver.D2 == "") { MessageBox.Show("Верно2"); } else { Search(); }
                            //if (Saver.D2 == "н" | Saver.D2 == "б" | Saver.D2 == "п" | Saver.D2 == "") { MessageBox.Show("Верно2"); } else { Search(); }
                            //if (Saver.D2 == "н" | Saver.D2 == "б" | Saver.D2 == "п" | Saver.D2 == "") { MessageBox.Show("Верно2"); } else { Search(); }
                            //if (Saver.D2 == "н" | Saver.D2 == "б" | Saver.D2 == "п" | Saver.D2 == "") { MessageBox.Show("Верно2"); } else { Search(); }
                            //if (Saver.D2 == "н" | Saver.D2 == "б" | Saver.D2 == "п" | Saver.D2 == "") { MessageBox.Show("Верно2"); } else { Search(); }
                            //if (Saver.D2 == "н" | Saver.D2 == "б" | Saver.D2 == "п" | Saver.D2 == "") { MessageBox.Show("Верно2"); } else { Search(); }
                            //if (Saver.D2 == "н" | Saver.D2 == "б" | Saver.D2 == "п" | Saver.D2 == "") { MessageBox.Show("Верно2"); } else { Search(); }
                            //if (Saver.D2 == "н" | Saver.D2 == "б" | Saver.D2 == "п" | Saver.D2 == "") { MessageBox.Show("Верно2"); } else { Search(); }
                            //if (Saver.D2 == "н" | Saver.D2 == "б" | Saver.D2 == "п" | Saver.D2 == "") { MessageBox.Show("Верно2"); } else { Search(); }
                            //if (Saver.D2 == "н" | Saver.D2 == "б" | Saver.D2 == "п" | Saver.D2 == "") { MessageBox.Show("Верно2"); } else { Search(); }
                            //if (Saver.D2 == "н" | Saver.D2 == "б" | Saver.D2 == "п" | Saver.D2 == "") { MessageBox.Show("Верно2"); } else { Search(); }
                            //if (Saver.D2 == "н" | Saver.D2 == "б" | Saver.D2 == "п" | Saver.D2 == "") { MessageBox.Show("Верно2"); } else { Search(); }
                            //if (Saver.D2 == "н" | Saver.D2 == "б" | Saver.D2 == "п" | Saver.D2 == "") { MessageBox.Show("Верно2"); } else { Search(); }

                            //if (Saver.D31 == "н" | Saver.D31 == "б" | Saver.D31 == "п" | Saver.D31 == "") { /*Saver.i++;*/ MessageBox.Show("Верно31"); int x = 5; int y = x / 0; } else { MessageBox.Show("Введите 'н' или 'б' или 'п' или ничего"); Search();}
                            //if (Saver.D3 == "н" | Saver.D3 == "б" | Saver.D3 == "п" | Saver.D3 == "") {UpdateInfo(); MessageBox.Show("Успех"); } else { MessageBox.Show("Введите 'н' или 'б' или 'п' или ничего"); Search();}






                            /*|| Saver.D3 != "н" || Saver.D4 != "н" || Saver.D5 != "н" || Saver.D6 != "н" || Saver.D7 != "н" || Saver.D8 != "н" || Saver.D9 != "н" || Saver.D10 != "н" || Saver.D11 != "н" || Saver.D12 != "н" || Saver.D13 != "н" || Saver.D14 != "н" || Saver.D15 != "н" || Saver.D16 != "н" || Saver.D17 != "н" || Saver.D18 != "н" || Saver.D19 != "н" || Saver.D20 != "н" || Saver.D21 != "н" || Saver.D22 != "н" || Saver.D23 != "н" || Saver.D24 != "н" || Saver.D25 != "н" || Saver.D26 != "н" || Saver.D27 != "н" || Saver.D28 != "н" || Saver.D29 != "н" || Saver.D30 != "н" || Saver.D31 != "н"*/
                            //else if (Saver.D1 != "б" || Saver.D2 != "б" || Saver.D3 != "б" || Saver.D4 != "б" || Saver.D5 != "б" || Saver.D6 != "б" || Saver.D7 != "б" || Saver.D8 != "б" || Saver.D9 != "б" || Saver.D10 != "б" || Saver.D11 != "б" || Saver.D12 != "б" || Saver.D13 != "б" || Saver.D14 != "б" || Saver.D15 != "б" || Saver.D16 != "б" || Saver.D17 != "б" || Saver.D18 != "б" || Saver.D19 != "б" || Saver.D20 != "б" || Saver.D21 != "б" || Saver.D22 != "б" || Saver.D23 != "б" || Saver.D24 != "б" || Saver.D25 != "б" || Saver.D26 != "б" || Saver.D27 != "б" || Saver.D28 != "б" || Saver.D29 != "б" || Saver.D30 != "б" || Saver.D31 != "б") { MessageBox.Show("2"); }
                            //else if (Saver.D1 != "п" || Saver.D2 != "п" || Saver.D3 != "п" || Saver.D4 != "п" || Saver.D5 != "п" || Saver.D6 != "п" || Saver.D7 != "п" || Saver.D8 != "п" || Saver.D9 != "п" || Saver.D10 != "п" || Saver.D11 != "п" || Saver.D12 != "п" || Saver.D13 != "п" || Saver.D14 != "п" || Saver.D15 != "п" || Saver.D16 != "п" || Saver.D17 != "п" || Saver.D18 != "п" || Saver.D19 != "п" || Saver.D20 != "п" || Saver.D21 != "п" || Saver.D22 != "п" || Saver.D23 != "п" || Saver.D24 != "п" || Saver.D25 != "п" || Saver.D26 != "п" || Saver.D27 != "п" || Saver.D28 != "п" || Saver.D29 != "н" || Saver.D30 != "п" || Saver.D31 != "п") { MessageBox.Show("Erro"); MessageBox.Show("3"); }
                            connection.Close();
                        }
                        catch
                        { //UpdateInfo(); Search(); MessageBox.Show("Финал"); }
                          //UpdateInfo(); Search(); MessageBox.Show("Финал");
                        }
                    }
                }
            }
            catch (Exception exp) { MessageBox.Show(exp.Message); } 
        }
        public void UpdateInfo()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                 
                    {
                        connection.Open();
                        string query = $@"UPDATE  Traffics 
                        SET  Day1 = '{Saver.D1}',Day2 = '{Saver.D2}',Day3 = '{Saver.D3}',Day4 = '{Saver.D4}',Day5 = '{Saver.D5}',Day6 = '{Saver.D6}',Day7 = '{Saver.D7}',Day7 = '{Saver.D7}',Day8 = '{Saver.D8}',Day9 = '{Saver.D9}',Day10 = '{Saver.D10}',Day11 = '{Saver.D11}',Day12 = '{Saver.D12}',Day13 = '{Saver.D13}',Day14 = '{Saver.D14}',Day15 = '{Saver.D15}',Day16 = '{Saver.D16}',Day17 = '{Saver.D17}',Day18 = '{Saver.D18}',Day19 = '{Saver.D19}',Day20 = '{Saver.D20}',Day21 = '{Saver.D21}',Day22 = '{Saver.D22}',Day23 = '{Saver.D23}',Day24 = '{Saver.D25}',Day26 = '{Saver.D26}',Day27 = '{Saver.D27}',Day28 = '{Saver.D28}',Day29 = '{Saver.D29}',Day30 = '{Saver.D30}',Day31 = '{Saver.D31}'
                        WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' ";//Дописать Дни
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        cmd.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception exp) { MessageBox.Show(exp.Message); }
        }
        public void Stroka()
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))//Узнаем кого выбрали + его айди(в студентах)
            {
                try
                {

                    foreach (var item in DGStudents.SelectedItems.Cast<DataRowView>())
                    {
                        DataRowView row = (DataRowView)DGStudents.SelectedItems[0];
                        Saver.afv = item["NSM"];
                        Saver.NameFirst = row["NSM"].ToString(); 
                        //MessageBox.Show(Saver.NameFirst);
                        //MessageBox.Show(Saver.afv);
                        connection.Open();
                        string query = $@"SELECT ID FROM  Students  WHERE NSM=@NSM ";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        cmd.Parameters.AddWithValue("@NSM", Saver.NameFirst);
                        int countID = Convert.ToInt32(cmd.ExecuteScalar());
                        Saver.IDNSM = countID;
                        //MessageBox.Show('{Saver.IDNSM}');
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
        }

        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
            AddStudents AdSt = new AddStudents();
            AdSt.Show();
        }
    }  
}
