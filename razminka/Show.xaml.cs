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

namespace razminka
{
    /// <summary>
    /// Логика взаимодействия для Show.xaml
    /// </summary>
    public partial class Show : Page
    {
        public Show()
        {
            InitializeComponent();
            LSH.ItemsSource = Base.RE.Product.ToList();
        }

        private void btdel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы хотите удалить элемент?","Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Button bt = (Button)sender;
                int index = Convert.ToInt32(bt.Uid);
                Product pr = Base.RE.Product.FirstOrDefault(x => x.id == index);
                Base.RE.Product.Remove(pr);
                Base.RE.SaveChanges();
                FrameC.framem.Navigate(new Show());
            }
        }

       

        private void btnupd_Click(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            int index = Convert.ToInt32(bt.Uid);
            Product product= Base.RE.Product.FirstOrDefault(x => x.id == index);
            FrameC.framem.Navigate(new CreateUpdate(product));
        }

        private void mkm_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            List<ProductMaterial> pm = Base.RE.ProductMaterial.Where(x => x.idProd == index).ToList();
            string str = "";
            foreach (ProductMaterial prm in pm) 
            {
                str += prm.Material.Title + ", ";
            }
            tb.Text = "Материалы: " + str.Substring(0, str.Length -2);
        }

       

        private void ty_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            List<Type> types = Base.RE.Type.Where(x => x.id == index).ToList();
            string str = "";
            foreach (Type tc in types)
            {
                str += tc.type1;
            }

            tb.Text = str;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FrameC.framem.Navigate(new CreateUpdate());
        }
    }
}
