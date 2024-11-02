using PR__13.ApplicationData;
using PR__13.PageMain;
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

namespace PR__13
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppConnect.modelOdb = new Sklad13Entities(); //соединение с БД
            AppFrame.FrameMain = FrmMain; //загрузка фрейма со стартом
            FrmMain.Navigate(new PageSklad(null)); // страница PageSklad();

        }

       
    }
}
