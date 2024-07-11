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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiskmagEngineWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static int pos;

        public MainWindow()
        {
            InitializeComponent();
            InitMenu();
        }

        private void InitMenu()
        {
            pos = 0;
            menu.Children.Add(AddArticle("metaphysics", "The Synthesis of Metaphysics and Jungian Personality Theory"));
            menu.Children.Add(AddArticle("article2", "Article 2"));
        }

        private Canvas AddArticle(string name, string content)
        {
            var canvas = new Canvas();
            var article = new Button();
            article.HorizontalAlignment = HorizontalAlignment.Left;
            article.VerticalAlignment = VerticalAlignment.Top;
            article.Click += article_click;
            article.Name = name;
            article.Tag = "component/Texts/" + name + ".rtf";
            article.Content = content;
            Canvas.SetTop(canvas, pos);
            canvas.Children.Add(article);

            pos += 20;

            return canvas;
        }

        private void article_click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag != null)
            {
                OpenArticle((string)button.Tag);
            }
        }

        private void OpenArticle(string path)
        {
            viewer.Document.Blocks.Clear();
            System.IO.FileStream streamToRtfFile = new System.IO.FileStream(path, System.IO.FileMode.Open);
            viewer.Selection.Load(streamToRtfFile, DataFormats.Rtf);
            streamToRtfFile.Close();
            viewer.Visibility = Visibility.Visible;
            HideMenu();
        }
        private void CloseArticle()
        {
            viewer.Visibility = Visibility.Collapsed;
        }


        private void ShowMenu()
        {
            foreach (Canvas canvas in menu.Children)
            {
                foreach (Button button in canvas.Children)
                {
                    button.Visibility = Visibility.Visible;
                }
            }
        }
        private void HideMenu()
        {
            foreach (Canvas canvas in menu.Children)
            {
                foreach (Button button in canvas.Children)
                {
                    button.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            CloseArticle();
            ShowMenu();
        }
    }
}
