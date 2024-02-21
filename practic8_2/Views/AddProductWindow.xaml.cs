using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using practic8_2.Data;
using practic8_2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using System.Windows.Shapes;

namespace practic8_2.Views
{
    /// <summary>
    /// Логика взаимодействия для AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        private Product currentProduct;
        private bool update = false;
        private string originalPathImage = null;
        public AddProductWindow()
        {
            InitializeComponent();
            currentProduct = new Product();
            DataContext = currentProduct;
            currentProduct.PhotoUrl = "нет";
            buttonAddOrUpdate.Content = "Добавить";
            TextBoxproductName.Focus();
            window.Title = "Добавление продукта";

        }
        public AddProductWindow (Product product, bool update)
        {
            InitializeComponent();
            currentProduct = product;
            this.update = update;
            DataContext = currentProduct;
            buttonAddOrUpdate.Content = "Изменить";
            TextBoxproductName.Focus();
            window.Title = "Изменение продукта";
        }
        private void loadWindow (object sender, RoutedEventArgs e)
        {
            using (var context = new ShopContext())
            {
                categoriesComboBox.ItemsSource = context.ProductCategories.ToList();
                categoriesComboBox.DisplayMemberPath = "CategoryName";
                categoriesComboBox.SelectedItem = context.ProductCategories.FirstOrDefault(c => c.CategoryId == currentProduct.CategoryId);
            }
               
        }
        private void updatePhoto (object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Изображения (*.png;*.jpg)|*.png;*.jpg",
                Title = "Выбор изображения"
            };
            if (dialog.ShowDialog() == true)
            {
                if (!string.IsNullOrEmpty(originalPathImage) && originalPathImage != "")
                {
                    var oldImagePath = System.IO.Path.Combine(Environment.CurrentDirectory, "Images", originalPathImage);
                    if (File.Exists(oldImagePath))
                    {
                        File.Delete(oldImagePath);
                    }
                }
                var newPhotoName = System.IO.Path.GetRandomFileName() + ".png";
                var selectedPhoto = dialog.FileName;
                var newPhotoFullName = System.IO.Path.Combine(Environment.CurrentDirectory, "Images", newPhotoName);

                if (!IsImageSizeValid(selectedPhoto))
                {
                    MessageBox.Show("Пожалуйста, выберите другое изображение.");
                    return;
                }

                File.Copy(selectedPhoto, newPhotoFullName);
                currentProduct.PhotoUrl = newPhotoName;

                DataContext = null;
                DataContext = currentProduct;
            }
        }
        private bool IsImageSizeValid (string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            return fileInfo.Length <= 2097152;
        }
        private void addUpdateProduct (object sender, RoutedEventArgs e)
        {
            // создаем контекст валидации
            var validationContext = new ValidationContext(currentProduct);
            // список ошибок
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            // вызываем метод класс Validator - TryValidateObject
            // true для полной проверки
            var isValid = Validator.TryValidateObject(currentProduct, validationContext, results, true);

            // если объект не валидный
            if (!isValid)
            {
                errorsLabel.Content = string.Empty;

                foreach (var error in results)
                {
                    errorsLabel.Content += error.ErrorMessage + "\r\n";
                }
                return;
            }
            using (var context = new ShopContext())
            {
                if (update) // изменение
                {
                    context.Products.Update(currentProduct);
                }
                else
                {
                    context.Entry(currentProduct).State = EntityState.Added;
                }
                context.SaveChanges();
            }

            originalPathImage = currentProduct.PhotoUrl;
            Close();
        }
        private void ButtonCancel_OnClick (object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void sliderDiscount_ValueChanged (object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            textBoxDiscountValue.Text = Math.Round(sliderDiscount.Value / 100, 2).ToString();
            currentProduct.Discount = Convert.ToDouble(Math.Round(sliderDiscount.Value / 100, 2));
        }
    }
}
