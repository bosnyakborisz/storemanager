using System;
using System.Windows;
using System.Windows.Controls;
using StoreManager.Models;

namespace StoreManager.UserControls
{
    public partial class ProductView : UserControl
    {
        Product selectedProduct;

        public ProductView()
        {
            InitializeComponent();
            ReadDatabase();
        }

        private void ReadDatabase()
        {
            tbName.Text = "";
            tbBrand.Text = "";
            tbCategory.Text = "";
            tbPrice.Text = "";
            tbQuality.Text = "";
            tbOrigin.Text = "";
            tbDescription.Text = "";

            var repo = new GenericRepository<Product>(App.databasePath);
            dgProducts.ItemsSource = repo.GetAll();

            btnAdd.Visibility = Visibility.Visible;
            btnUpdate.Visibility = Visibility.Hidden;
            btnDelete.Visibility = Visibility.Hidden;
        }

        private void dgProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgProducts.SelectedItem != null)
            {
                selectedProduct = (Product)dgProducts.SelectedItem;
                tbName.Text = selectedProduct.Name;
                tbBrand.Text = selectedProduct.Brand;
                tbCategory.Text = selectedProduct.Category;
                tbPrice.Text = selectedProduct.Price.ToString();
                tbQuality.Text = selectedProduct.Quality;
                tbOrigin.Text = selectedProduct.Origin;
                tbDescription.Text = selectedProduct.Description;

                btnAdd.Visibility = Visibility.Hidden;
                btnUpdate.Visibility = Visibility.Visible;
                btnDelete.Visibility = Visibility.Visible;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Product newProduct = new Product
            {
                Name = tbName.Text,
                Brand = tbBrand.Text,
                Category = tbCategory.Text,
                Price = double.TryParse(tbPrice.Text, out double pr) ? pr : 0.0,
                Quality = tbQuality.Text,
                Origin = tbOrigin.Text,
                Description = tbDescription.Text
            };

            var repo = new GenericRepository<Product>(App.databasePath);
            repo.insert(newProduct);
            ReadDatabase();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (selectedProduct == null) return;

            selectedProduct.Name = tbName.Text;
            selectedProduct.Brand = tbBrand.Text;
            selectedProduct.Category = tbCategory.Text;
            selectedProduct.Price = double.TryParse(tbPrice.Text, out double pr) ? pr : selectedProduct.Price;
            selectedProduct.Quality = tbQuality.Text;
            selectedProduct.Origin = tbOrigin.Text;
            selectedProduct.Description = tbDescription.Text;

            var repo = new GenericRepository<Product>(App.databasePath);
            repo.update(selectedProduct);
            ReadDatabase();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (selectedProduct == null) return;

            var repo = new GenericRepository<Product>(App.databasePath);
            repo.delete(selectedProduct);
            ReadDatabase();
        }
    }
}