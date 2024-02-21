using Microsoft.EntityFrameworkCore;
using practic8_2.Data;
using practic8_2.Models;
using practic8_2.Views;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace practic8_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow :Window
    {
        private int totalRecords = 0;
        public MainWindow ()
        {
            InitializeComponent();
            var context = new ShopContext();
            var categories = context.ProductCategories.ToList();
            categories.Insert(0, new ProductCategory { CategoryId = 0, CategoryName = "Все категории" });
            filterComboBox.ItemsSource = categories;
            productsListView.ItemsSource = LoadData();
            LoadTotalRecords();
            UpdateProductsList();
        }
        private List<Product> LoadData ()
        {
            using (var context = new ShopContext())
            {
                var productList = context.Products
                    .Include(c => c.ProductCategory)
                    .ToList();

                return productList;
            }
        }
        private void SearchTextBox_OnTextChanged (object sender, TextChangedEventArgs e) => UpdateProductsList();
        private void FilterComboBox_OnSelectionChanged (object sender, SelectionChangedEventArgs e) => UpdateProductsList();
        private void SortingComboBox_OnSelectionChanged (object sender, SelectionChangedEventArgs e) => UpdateProductsList();
        private void ButtonAddProduct_OnClick (object sender, RoutedEventArgs e)
        {
            AddProductWindow addProductWindow = new AddProductWindow();
            addProductWindow.ShowDialog();
            LoadTotalRecords();
            UpdateProductsList();
        }
        private void UpdateProductsList ()
        {
            using (var context = new ShopContext())
            {
                var productList = LoadData().AsQueryable();

                // Поиск 
                var searchQuery = searchTextBox.Text;
                if (!string.IsNullOrWhiteSpace(searchQuery))
                {
                    productList = productList.Where(p => p.Name.Contains(searchQuery));
                }

                // Фильтрация
                var selectedManufacturer = (ProductCategory) filterComboBox.SelectedItem;
                if (selectedManufacturer != null && selectedManufacturer.CategoryId != 0)
                {
                    productList = productList.Where(p => p.CategoryId == selectedManufacturer.CategoryId);
                }

                // Сортировка
                switch (sortingComboBox.SelectedIndex)
                {
                    case 1:
                        productList = productList.OrderBy(p => p.Price); // По возрастанию 
                        break;
                    case 2:
                        productList = productList.OrderByDescending(p => p.Price); // По убыванию
                        break;
                }

                var filteredList = productList.ToList();
                productsListView.ItemsSource = filteredList;

                totalRecordsTextBlock.Text = $"Показано {filteredList.Count} из {totalRecords} записей";
            }
        }
        private void deleteProduct (object sender, RoutedEventArgs e)
        {
            Product? selectedProduct = productsListView.SelectedItem as Product;
            if (selectedProduct != null)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить продукт?", "Удаление продукта", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    using (var context = new ShopContext())
                    {
                        var productDelete = context.Products.FirstOrDefault(p => p.ProductId == selectedProduct.ProductId);
                        if (productDelete != null)
                        {
                            context.Products.Remove(productDelete);
                            context.SaveChanges();
                        }
                    }
                    LoadTotalRecords();
                    UpdateProductsList();
                }
            }
        }
        private void updateProduct (object sender, RoutedEventArgs e)
        {
            Product? selectedProduct = productsListView.SelectedItem as Product;
            if (selectedProduct is not null)
            {
                var window = new AddProductWindow(selectedProduct, true);
                window.ShowDialog();
                UpdateProductsList();
            }
        }
        private void LoadTotalRecords ()
        {
            using (var context = new ShopContext())
            {
                totalRecords = context.Products.Count();
            }
        }
    }
}