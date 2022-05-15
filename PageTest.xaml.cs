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
    /// Логика взаимодействия для PageTest.xaml
    /// </summary>
    public partial class PageTest : Page
    {
        List<TestFill> fill = new List<TestFill>();
        string QuestPath = "quests.csv";
        public PageTest()
        {
            InitializeComponent();
            try
            {
                using (StreamReader sr = new StreamReader(QuestPath))
                {
                    while (sr.EndOfStream != true)
                    {
                        string[] arr = sr.ReadLine().Split(';');
                        fill.Add(new TestFill { Quest = arr[0], Answer1 = arr[1], Answer2 = arr[2], Answer3 = arr[3], Answer4 = arr[4] });
                    }
                }
            }
            catch
            {
                MessageBox.Show("Файл может быть поврежден", "Ошибка");
            }
            LVQuest.ItemsSource = fill;
        }

        bool IsEdit = false;
        int QuestNum = 0;
        private void ButCreate_Click(object sender, RoutedEventArgs e)
        {
            if (IsEdit)
            {
                fill.Remove(fill[QuestNum]);
                IsEdit = false;
            }

            fill.Add(new TestFill { Quest = TBQuest.Text, Answer1 = TBAns1.Text, Answer2 = TBAns2.Text, Answer3 = TBAns3.Text, Answer4 = TBAns4.Text });

            var file = File.Create(QuestPath);
            file.Close();
            
            for(int i = 0; i < fill.Count; i++)
            {
                using (StreamWriter sw = new StreamWriter(QuestPath, true))
                {
                    sw.Write(fill[i].Quest + ";");
                    sw.Write(fill[i].Answer1 + ";");
                    sw.Write(fill[i].Answer2 + ";");
                    sw.Write(fill[i].Answer3 + ";");
                    sw.Write(fill[i].Answer4 + ";\n");
                }
            }
            MessageBox.Show("Записано", "Тест");
            LVQuest.Items.Refresh();
        }

        private void ButEdit_Click(object sender, RoutedEventArgs e)
        {
            IsEdit = true;
            QuestNum = LVQuest.SelectedIndex;
            TBQuest.Text = fill[QuestNum].Quest;
            TBAns1.Text = fill[QuestNum].Answer1;
            TBAns2.Text = fill[QuestNum].Answer2;
            TBAns3.Text = fill[QuestNum].Answer3;
            TBAns4.Text = fill[QuestNum].Answer4;
        }
    }
}
