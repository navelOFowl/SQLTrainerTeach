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
    /// Логика взаимодействия для PageConnect.xaml
    /// </summary>
    public partial class PageConnect : Page
    {
        string ConnPath = "connections.csv";
        List<ConnFill> fill = new List<ConnFill>();
        public PageConnect()
        {
            InitializeComponent();
            try
            {
                using (StreamReader sr = new StreamReader(ConnPath))
                {
                    while (sr.EndOfStream != true)
                    {
                        string[] arr = sr.ReadLine().Split(';');
                        fill.Add(new ConnFill { Task = arr[0], First1 = arr[1], First2 = arr[2], First3 = arr[3], Second1 = arr[4], Second2 = arr[5], Second3 = arr[6], FirstCorrect = arr[7], SecondCorrect = arr[8] });
                    }
                }
            }
            catch
            {
                MessageBox.Show("Файл может быть поврежден", "Ошибка");
            }
            LVConn.ItemsSource = fill;
        }

        int first = 0;
        int second = 0;
        private void ButLVFirst_Click(object sender, RoutedEventArgs e)
        {
            first++;
            if (first == 3)
            {
                MessageBox.Show("Три поля уже выбрано", "Ошибка");
            }
            LBFirst.Items.Add(TBLVFirst.Text);
        }

        private void ButLVSecond_Click(object sender, RoutedEventArgs e)
        {
            second++;
            if(second == 3)
            {
                MessageBox.Show("Три поля уже выбрано", "Ошибка");
            }
            LBSecond.Items.Add(TBLVSecond.Text);
        }

        private void ButDelFirst_Click(object sender, RoutedEventArgs e)
        {
            LBFirst.Items.Remove(LBFirst.SelectedItem);
            first--;
        }

        private void ButDelSecond_Click(object sender, RoutedEventArgs e)
        {
            LBSecond.Items.Remove(LBSecond.SelectedItem);
            second--;
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
            try
            {
                fill.Add(new ConnFill { Task = TBTask.Text, First1 = LBFirst.Items[0].ToString(), First2 = LBFirst.Items[1].ToString(), First3 = LBFirst.Items[2].ToString(), Second1 = LBSecond.Items[0].ToString(), Second2 = LBSecond.Items[1].ToString(), Second3 = LBSecond.Items[2].ToString(), FirstCorrect = LBFirst.SelectedItem.ToString(), SecondCorrect = LBSecond.SelectedItem.ToString() });
            }
            catch
            {
                MessageBox.Show("Не выбраны правильные поля, проверьте ввод", "Ошибка");
            }

            var file = File.Create(ConnPath);
            file.Close();

            for (int i = 0; i < fill.Count; i++)
            {
                using (StreamWriter sw = new StreamWriter(ConnPath, true))
                {
                    sw.Write(fill[i].Task + ";");
                    sw.Write(fill[i].First1 + ";");
                    sw.Write(fill[i].First2 + ";");
                    sw.Write(fill[i].First3 + ";");
                    sw.Write(fill[i].Second1 + ";");
                    sw.Write(fill[i].Second2 + ";");
                    sw.Write(fill[i].Second3 + ";");
                    sw.Write(fill[i].FirstCorrect + ";");
                    sw.Write(fill[i].SecondCorrect + ";\n");
                }
            }
            MessageBox.Show("Записано", "Связи");
            LVConn.Items.Refresh();
        }

        private void ButEdit_Click(object sender, RoutedEventArgs e)
        {
            LBFirst.Items.Clear();
            LBSecond.Items.Clear();
            IsEdit = true;
            QuestNum = LVConn.SelectedIndex;
            TBTask.Text = fill[QuestNum].Task;
            LBFirst.Items.Add(fill[QuestNum].First1);
            LBFirst.Items.Add(fill[QuestNum].First2);
            LBFirst.Items.Add(fill[QuestNum].First3);
            LBSecond.Items.Add(fill[QuestNum].Second1);
            LBSecond.Items.Add(fill[QuestNum].Second2);
            LBSecond.Items.Add(fill[QuestNum].Second3);
            
            for(int i = 0; i < LBFirst.Items.Count; i++)
            {
                if (LBFirst.Items[i].ToString() == fill[QuestNum].FirstCorrect)
                {
                    LBFirst.SelectedIndex = i;
                }
            }

            for (int i = 0; i < LBSecond.Items.Count; i++)
            {
                if (LBSecond.Items[i].ToString() == fill[QuestNum].SecondCorrect)
                {
                    LBSecond.SelectedIndex = i;
                }
            }
        }
    }
}
