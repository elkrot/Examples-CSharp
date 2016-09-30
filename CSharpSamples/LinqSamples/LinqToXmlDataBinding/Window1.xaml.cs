// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.ComponentModel;
using System.Xml.Linq;

namespace LinqToXmlDataBinding {
    public partial class Window1 : Window {
        private Brush previousBrush;

        public Window1() {
            this.InitializeComponent();
        }

        /// <summary>
        /// Сохранение списка MyFavorites при закрытии.
        /// </summary>
        protected override void OnClosing(CancelEventArgs args) {
            XElement myFavorites = (XElement)((ObjectDataProvider)Resources["MyFavoritesList"]).Data;
            myFavorites.Save(@"..\..\data\MyFavorites.xml");
        }

        /// <summary>
        /// Обработчик событий кнопки воспроизведения
        /// </summary>
        void OnPlay(object sender, EventArgs e) {
            videoImage.Visibility = Visibility.Hidden;
            mediaElement.Play();
        }

        /// <summary>
        /// Обработчик событий кнопки остановки
        /// </summary>
        void OnStop(object sender, EventArgs e) {
            mediaElement.Stop();
            videoImage.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Кнопка "Добавить" в обработчике событий добавляет выбранное в данный момент видео в папку "Избранное"
        /// </summary>
        void OnAdd(object sender, EventArgs e) {
            XElement itemsList = (XElement)((ObjectDataProvider)Resources["MyFavoritesList"]).Data;
            itemsList.Add(videoListBox1.SelectedItem as XElement);
        }

        /// <summary>
        /// Кнопка "Удалить" в обработчике событий удаляет выбранное в данный момент видео из папки "Избранное"
        /// </summary>
        void OnDelete(object sender, EventArgs e) {
            XElement selectedItem = (XElement)videoListBox2.SelectedItem;
            if (selectedItem != null) {
                if (selectedItem.PreviousNode != null)
                    this.videoListBox2.SelectedItem = selectedItem.PreviousNode;
                else if (selectedItem.NextNode != null)
                    this.videoListBox2.SelectedItem = selectedItem.NextNode;
                selectedItem.Remove();
            }
        }

        /// <summary>
        /// обработчик событий Searchbox, поиск видео по указанным пользователем параметрам
        /// </summary>
        private void OnKeyUp(object sender, KeyEventArgs e) {
            if (e.Key.Equals(Key.Enter)) {
                ObjectDataProvider objectDataProvider = Resources["MsnVideosList"] as ObjectDataProvider;
                objectDataProvider.MethodParameters[0] = @"http://soapbox.msn.com/rss.aspx?searchTerm=" + searchBox.Text;
                objectDataProvider.Refresh();
            }
        }

        /// <summary>
        /// Обработчики событий для вариантов поиска, перечисленных в списке на первой странице, просто обновляют статический ресурс
        /// "MsnVideosList", записывая в него новый аргумент.
        /// </summary>
        private void OnMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            string content = (string)((Label)sender).Content;
            ObjectDataProvider objectDataProvider = Resources["MsnVideosList"] as ObjectDataProvider;

            switch (content) {
                case "Most Viewed":
                    objectDataProvider.MethodParameters[0] = @"http://soapbox.msn.com/rss.aspx?listId=MostPopular";
                    objectDataProvider.Refresh();
                    break;
                case "Most Recent":
                    objectDataProvider.MethodParameters[0] = @"http://soapbox.msn.com/rss.aspx?listId=MostRecent";
                    objectDataProvider.Refresh();
                    break;
                case "Top Favorites":
                    objectDataProvider.MethodParameters[0] = @"http://soapbox.msn.com/rss.aspx?listId=TopFavorites";
                    objectDataProvider.Refresh();
                    break;
                case "Top Rated":
                    objectDataProvider.MethodParameters[0] = @"http://soapbox.msn.com/rss.aspx?listId=TopRated";
                    objectDataProvider.Refresh();
                    break;
                case "My Favorites":
                    XElement msn = (XElement)objectDataProvider.Data;
                    XElement favorites = (XElement)((ObjectDataProvider)Resources["MyFavoritesList"]).Data;
                    msn.ReplaceAll(favorites.Elements("item"));
                    break;
            }
        }

        /// <summary>
        /// Изменяет цвет ссылок поиска в момент наведения, и после наведения курсора мыши на них, тем самым показывая
        /// , что на них можно нажимать
        /// </summary>
        private void OnMouseEnter(object sender, System.Windows.Input.MouseEventArgs e) {
            Label myLabel = sender as Label;
            previousBrush = myLabel.Foreground;
            myLabel.Foreground = Brushes.Blue;
        }

        private void OnMouseLeave(object sender, System.Windows.Input.MouseEventArgs e) {
            Label myLabel = sender as Label;
            myLabel.Foreground = previousBrush;
        }
    }
}