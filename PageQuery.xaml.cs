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
        string QueryPath = "taskqueries.csv";
        List<TaskQueryFill> fill = new List<TaskQueryFill>();
        public PageQuery()
        {
            InitializeComponent();
            try
            {
                using (StreamReader sr = new StreamReader(QueryPath))
                {
                    while (sr.EndOfStream != true)
                    {
                        string[] arr = sr.ReadLine().Split(';');
                        fill.Add(new TaskQueryFill { Task = arr[0], Query = arr[1] });
                    }
                }
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

            fill.Add(new TaskQueryFill { Task = TBTask.Text, Query = TBQuery.Text });

            var file = File.Create(QueryPath);
            file.Close();

            for (int i = 0; i < fill.Count; i++)
            {
                using (StreamWriter sw = new StreamWriter(QueryPath, true))
                {
                    sw.Write(fill[i].Task + ";");
                    sw.Write(fill[i].Query + ";\n");
                }
            }
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
