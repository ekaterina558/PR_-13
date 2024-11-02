using PR__13.ApplicationData;
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

namespace PR__13.PageMain
{
    /// <summary>
    /// Логика взаимодействия для PageSklad.xaml
    /// </summary>
    public partial class PageSklad : Page
    {
        private Sklad _currentSklad = new Sklad();

        public PageSklad(Sklad selectedSklad)
        {
            InitializeComponent();

            if (selectedSklad != null)
                _currentSklad = selectedSklad;

            DataContext = _currentSklad;
            DtGridTovar.ItemsSource = Sklad13Entities.GetContext().Sklad.ToList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentSklad.naimenov))
                errors.AppendLine("Укажите название товара");
            if (_currentSklad.kolichestvo <= 0)
                errors.AppendLine("Количество товара не может быть меньше или равно 0");
            if (_currentSklad.cena <= 0)
                errors.AppendLine("Цена не может быть меньше или равна 0");
            if (_currentSklad.Strana == null)
                errors.AppendLine("Выберите страну");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentSklad.id == 0)
                Sklad13Entities.GetContext().Sklad.Add(_currentSklad);

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

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Sklad13Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            DtGridTovar.ItemsSource = Sklad13Entities.GetContext().Sklad.ToList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrameMain.Navigate(new PageSkladAdd((sender as Button).DataContext as Sklad));
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            var tovarForRemoving = DtGridTovar.SelectedItems.Cast<Sklad>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующее {tovarForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) ;
            {
                try
                {
                    Sklad13Entities.GetContext().Sklad.RemoveRange(tovarForRemoving);
                    Sklad13Entities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    DtGridTovar.ItemsSource = Sklad13Entities.GetContext().Sklad.ToList();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message.ToString());
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrameMain.Navigate(new PageSkladAdd(null));
        }
    }
}
