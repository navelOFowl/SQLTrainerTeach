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

namespace SQLTrainerTeach
{
    /// <summary>
    /// Логика взаимодействия для PageQuery.xaml
    /// </summary>
    public partial class PageQuery : Page
    {
        //string QueryPath = "taskqueries.csv";
        string QueryPathBin = "";
        List<TaskQueryFill> fill = new List<TaskQueryFill>();
        public PageQuery()
        {
            InitializeComponent();
            var file = File.Create("query new.bin");
            file.Close();
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = Environment.CurrentDirectory;
            bool IsChose = (bool)dlg.ShowDialog();
            if (IsChose)
            {
                QueryPathBin = dlg.FileName;
            }
            else
            {
                MessageBox.Show("Файл не выбран", "Ошибка");
                FrameClass.frm.Navigate(new PageMenu());
                return;
            }
            if (QueryPathBin != "query new.bin")
            {
                File.Delete("query new.bin");
            }
            try
            {
                using (BinaryReader sr = new BinaryReader(File.Open(QueryPathBin, FileMode.Open)))
                {
                    int i = 0;
                    while (sr.PeekChar() > -1)
                    {
                        fill.Add(new TaskQueryFill());
                        fill[i].Task = sr.ReadString();
                        fill[i].Query = sr.ReadString();
                        i++;
                        
                    }
                }
                //using (StreamReader sr = new StreamReader(QueryPath))
                //{
                //    while (sr.EndOfStream != true)
                //    {
                //        string[] arr = sr.ReadLine().Split(';');
                //        fill.Add(new TaskQueryFill { Task = arr[0], Query = arr[1] });
                //    }
                //}
            }
            catch
            {
                MessageBox.Show("Файл может быть поврежден", "Ошибка");
            }
            LVQuery.ItemsSource = fill;
        }

        bool IsEdit = false;
        int TaskNum = 0;
        private void ButCreate_Click(object sender, RoutedEventArgs e)
        {
            if (IsEdit)
            {
                fill.Remove(fill[TaskNum]);
                IsEdit = false;
            }
            try
            {
                fill.Add(new TaskQueryFill { Task = TBTask.Text, Query = TBQuery.Text });
            }
            catch
            {
                MessageBox.Show("Не все данные введены, или введены некорректно, проверьте ввод", "Ошибка");
                return;
            }
            //var file = File.Create(QueryPathBin);
            //file.Close();

            var file = File.Create(QueryPathBin);
            file.Close();

            foreach (TaskQueryFill item in fill)
            {
                using (BinaryWriter sw = new BinaryWriter(File.Open(QueryPathBin, FileMode.Append)))
                {
                    sw.Write(item.Task);
                    sw.Write(item.Query);
                }
            }

            //for (int i = 0; i < fill.Count; i++)
            //{
            //    using (StreamWriter sw = new StreamWriter(QueryPathBin, true))
            //    {
            //        sw.Write(fill[i].Task + ";");
            //        sw.Write(fill[i].Query + ";\n");
            //    }
            //}
            MessageBox.Show("Записано", "Запросы");
            LVQuery.Items.Refresh();
        }

        private void ButEdit_Click(object sender, RoutedEventArgs e)
        {
            IsEdit = true;
            TaskNum = LVQuery.SelectedIndex;
            TBTask.Text = fill[TaskNum].Task;
            TBQuery.Text = fill[TaskNum].Query;
        }
    }
}
