using Microsoft.Win32;
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
    /// Логика взаимодействия для CreateUpdate.xaml
    /// </summary>
    public partial class CreateUpdate : Page
    {
        Product Pr;
        bool flagup;
        string path;

        public void cbb() 
        {
            cb.ItemsSource = Base.RE.Type.ToList();
            cb.SelectedValuePath = "id";
            cb.DisplayMemberPath = "type1";

            lb.ItemsSource = Base.RE.Material.ToList();
            lb.SelectedValuePath = "id";
            lb.DisplayMemberPath = "Title";
        }

        public CreateUpdate(Product product)
        {
            InitializeComponent();
            cbb();
            flagup = true;
            Pr = product;
            tb.Text = product.Title;
            cb.SelectedIndex = product.id - 1;
            List<ProductMaterial> pm = Base.RE.ProductMaterial.Where(x => x.idProd == product.id).ToList();
            foreach (Material p in lb.Items) 
            {
                if (pm.FirstOrDefault(x => x.idMater == p.id) != null) 
                {
                    lb.SelectedItems.Add(p);
                }
            }
            if (product.Img != null)
            {
                BitmapImage img = new BitmapImage(new Uri(product.Img, UriKind.RelativeOrAbsolute));
                im.Source = img;
            }
            else 
            {
                BitmapImage img = new BitmapImage(new Uri("Res/picture.png", UriKind.RelativeOrAbsolute));
                im.Source = img;
            }
        }


        public CreateUpdate()
        {
            InitializeComponent();
            cbb();
        }

        private void btup_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            path = ofd.FileName;
            string[] array = path.Split('\\');
            path = "\\" + array[array.Length - 2] + "\\" + array[array.Length - 1];
            BitmapImage img = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
            im.Source = img;
        }

        private void btcrup_Click(object sender, RoutedEventArgs e)
        {
            if (flagup == false) 
            {
                Pr = new Product();
            }
            Pr.Title = tb.Text;
            Pr.Img = path;
            Pr.Type = cb.SelectedIndex + 1;

            if (flagup == false) 
            {
                Base.RE.Product.Add(Pr);
            }

            List<ProductMaterial> materials = Base.RE.ProductMaterial.Where(x => x.idProd == Pr.id).ToList();
            if (materials.Count > 0) 
            {
                foreach(ProductMaterial productMaterial in materials)
                {
                    Base.RE.ProductMaterial.Remove(productMaterial);
                }
            }

            foreach (Material m in lb.SelectedItems) 
            {
                ProductMaterial pm = new ProductMaterial()
                {
                    idProd = Pr.id,
                    idMater = m.id
                };
                Base.RE.ProductMaterial.Add(pm);
            }
            Base.RE.SaveChanges();
            MessageBox.Show("Информация добавлена");
        }
    }
}
