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
    /// Логика взаимодействия для PageConnect.xaml
    /// </summary>
    public partial class PageConnect : Page
    {
        //string ConnPath = "connections.csv";
        string ConnPathBin = "";
        List<ConnFill> fill = new List<ConnFill>();
        public PageConnect()
        {
            InitializeComponent();
            var file = File.Create("conn new.bin");
            file.Close();
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = Environment.CurrentDirectory;
            bool IsChose = (bool)dlg.ShowDialog();
            if (IsChose)
            {
                ConnPathBin = dlg.FileName;
            }
            else
            {
                MessageBox.Show("Файл не выбран", "Ошибка");
                FrameClass.frm.Navigate(new PageMenu());
                return;
            }
            if (ConnPathBin != "conn new.bin")
            {
                File.Delete("conn new.bin");
            }
            try
            {

                using (BinaryReader sr = new BinaryReader(File.Open(ConnPathBin, FileMode.Open)))
                {
                    int i = 0;
                    while (sr.PeekChar() > -1)
                    {
                        fill.Add(new ConnFill());
                        fill[i].Task = sr.ReadString();
                        fill[i].First1 = sr.ReadString();
                        fill[i].First2 = sr.ReadString();
                        fill[i].First3 = sr.ReadString();
                        fill[i].Second1 = sr.ReadString();
                        fill[i].Second2 = sr.ReadString();
                        fill[i].Second3 = sr.ReadString();
                        fill[i].FirstCorrect = sr.ReadString();
                        fill[i].SecondCorrect = sr.ReadString();
                        i++;
                    }
                }

                //using (StreamReader sr = new StreamReader(ConnPath))
                //{
                //    while (sr.EndOfStream != true)
                //    {
                //        string[] arr = sr.ReadLine().Split(';');
                //        fill.Add(new ConnFill { Task = arr[0], First1 = arr[1], First2 = arr[2], First3 = arr[3], Second1 = arr[4], Second2 = arr[5], Second3 = arr[6], FirstCorrect = arr[7], SecondCorrect = arr[8] });
                //    }
                //}
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
            if (first > 3)
            {
                MessageBox.Show("Три поля уже выбрано", "Ошибка");
                return;
            }
            LBFirst.Items.Add(TBLVFirst.Text);
        }

        private void ButLVSecond_Click(object sender, RoutedEventArgs e)
        {
            second++;
            if(second > 3)
            {
                MessageBox.Show("Три поля уже выбрано", "Ошибка");
                return;
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
            if(LBFirst.SelectedItem == null || LBSecond.SelectedItem == null)
            {
                MessageBox.Show("Не выбраны правильные поля, проверьте ввод", "Ошибка");
                return;
            }
            try
            {
                fill.Add(new ConnFill { Task = TBTask.Text, First1 = LBFirst.Items[0].ToString(), First2 = LBFirst.Items[1].ToString(), First3 = LBFirst.Items[2].ToString(), Second1 = LBSecond.Items[0].ToString(), Second2 = LBSecond.Items[1].ToString(), Second3 = LBSecond.Items[2].ToString(), FirstCorrect = LBFirst.SelectedItem.ToString(), SecondCorrect = LBSecond.SelectedItem.ToString() });
            }
            catch
            {
                MessageBox.Show("Не все данные введены, или введены некорректно, проверьте ввод", "Ошибка");
                return;
            }

            //var file = File.Create(ConnPath);
            //file.Close();

            var file = File.Create(ConnPathBin);
            file.Close();

            foreach(ConnFill item in fill)
            {
                using (BinaryWriter sw = new BinaryWriter(File.Open(ConnPathBin, FileMode.Append)))
                {
                    sw.Write(item.Task);
                    sw.Write(item.First1);
                    sw.Write(item.First2);
                    sw.Write(item.First3);
                    sw.Write(item.Second1);
                    sw.Write(item.Second2);
                    sw.Write(item.Second3);
                    sw.Write(item.FirstCorrect);
                    sw.Write(item.SecondCorrect);
                }
            }
                //using (StreamWriter sw = new StreamWriter(ConnPath, true))
                //{
                //    sw.Write(fill[i].Task + ";");
                //    sw.Write(fill[i].First1 + ";");
                //    sw.Write(fill[i].First2 + ";");
                //    sw.Write(fill[i].First3 + ";");
                //    sw.Write(fill[i].Second1 + ";");
                //    sw.Write(fill[i].Second2 + ";");
                //    sw.Write(fill[i].Second3 + ";");
                //    sw.Write(fill[i].FirstCorrect + ";");
                //    sw.Write(fill[i].SecondCorrect + ";\n");
                //}
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
