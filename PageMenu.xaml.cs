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

namespace SQLTrainerTeach
{
    /// <summary>
    /// Логика взаимодействия для PageMenu.xaml
    /// </summary>
    public partial class PageMenu : Page
    {
        public PageMenu()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.frm.Navigate(new PageTest());
        }

        private void ButQuery_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.frm.Navigate(new PageQuery());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FrameClass.frm.Navigate(new PageConnect());
        }
    }
}
