// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
using System;
using System.Collections.Generic;
using System.Linq;
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
using UserInterface.Gateways;
using UserInterface.AdventureWorksService;


namespace UserInterface
{
    /// <summary>
    /// Логика взаимодействия для ProductList.xaml
    /// </summary>
    public partial class ProductList : Window
    {

        ProductGateway gateway;


	/// <summary>
        /// Запуск формы записи при включении
        /// </summary>
        public ProductList()
        {
            InitializeComponent();
            gateway = new ProductGateway();
            ProductsListView.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(ProductsListView_MouseDoubleClick);     
        }

	/// <summary>
        /// Выполните привязку результатов gateway.GetCategories() к раскрывающемуся списку "Категория продукта" в верхней части формы.
        /// </summary>
        private void BindCategories()
        {
            CategoryComboBox.ItemsSource = gateway.GetCategories();
            CategoryComboBox.SelectedIndex = 0;
        }

	/// <summary>
        /// Привязка результатов gateway.GetProducts(string ProductName, ProductCategory p) к элементу управления ListView.
        /// </summary>
        private void BindProducts()
        {
            if (CategoryComboBox.SelectedIndex > -1)
            {
                ProductsListView.ItemsSource = gateway.GetProducts(NameTextBox.Text, CategoryComboBox.SelectedItem as ProductCategory);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BindCategories();            
        }

        private void ProductsListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Product p = ProductsListView.SelectedItem as Product;
            ProductView window = new ProductView(gateway);
            window.Closed += new EventHandler(window_Closed);
            window.UpdateProduct(p);
            window.Show();
        }

	/// <summary>
        /// При нажатии кнопки поиска выполните вызов функции BindProducts().
        /// </summary>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            BindProducts();
        }

        private void btnNewProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductView window = new ProductView(gateway);
            window.Closed += new EventHandler(window_Closed);
            window.Show();
        }

	/// <summary>
        /// Выполните вызов gateway.DeleteProduct() для инициации удаления выбранного продукта, если продукт не может быть удален gateway.DeleteProduct 
	/// не возвращает неопределенное значение; ответ показывается пользователю посредством MessageBox.
        /// </summary>
        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            Product p = ProductsListView.SelectedItem as Product;
            if (p != null)
            {                
                string returned = gateway.DeleteProduct(p);
                if (returned != null)
                {
                    MessageBox.Show(returned);
                }
                BindProducts();
            }
        }

	/// <summary>
        /// Когда будет выбрана новая категория, обновите список
        /// </summary>
        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BindProducts();
        } 

        void window_Closed(object sender, EventArgs e)
        {
            BindCategories();           
        }


    }
}
