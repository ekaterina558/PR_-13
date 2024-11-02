using PR__13.ApplicationData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace PR__13.PageMain
{
    /// <summary>
    /// Логика взаимодействия для PageSkladAdd.xaml
    /// </summary>
    public partial class PageSkladAdd : Page
    {   
        private Sklad _currentSklad = new Sklad();
        public PageSkladAdd(Sklad selectedSklad)
        {
            InitializeComponent();
            if (selectedSklad != null)
                _currentSklad= selectedSklad;

            DataContext = _currentSklad;
            ComboStrana.ItemsSource = Sklad13Entities.GetContext().Strana.ToList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder err0rs = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentSklad.naimenov))
                err0rs.AppendLine("Укажите название товара");
            if (_currentSklad.kolichestvo <= 0)
                err0rs.AppendLine("Количество товара не может быть меньше или равно 0");
            if (_currentSklad.cena <= 0)
                err0rs.AppendLine("Цена не может быть меньше или равно 0");
            if (_currentSklad.Strana == null)
                err0rs.AppendLine("Выберите страну");

            if (err0rs.Length > 0)
            {
                MessageBox.Show(err0rs.ToString());
                return;
            }
            //добавление
            if (_currentSklad.id == 0)
                Sklad13Entities.GetContext().Sklad.Add(_currentSklad);

            //обработка вариант выпада/записи данных
            try
            {
                Sklad13Entities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }    
                
        
                
        }

     
    }
}
