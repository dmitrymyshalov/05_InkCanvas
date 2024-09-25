using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;

namespace InkCanvasEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const double OPACITY_UNCHECKED = 0.5;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItemOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "Файлы InkCanvas (*.strokes)|*.strokes"
            };
            if (dlg.ShowDialog() == true)
            {
                using (FileStream fs = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read))
                {
                    inkCanvas.Strokes = new StrokeCollection(fs);
                }
            }
        }

        private void MenuItemSaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog
            {
                Filter = "Файлы InkCanvas (*.strokes)|*.strokes"
            };
            if (dlg.ShowDialog() == true)
            {
                using (FileStream fs = new FileStream(dlg.FileName, FileMode.Create))
                {
                    inkCanvas.Strokes.Save(fs);
                }
            }
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ComboBoxolor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (inkCanvas != null)
            {
                string tool = (string)((sender as ComboBox).SelectedItem as ComboBoxItem).Tag;
                ProcessToolSelection(tool);
            }
        }

        private void RadioButtonPencil_Checked(object sender, RoutedEventArgs e)
        {
            if (inkCanvas != null)
            {
                inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            }
        }

        private void RadioButtonEraser_Checked(object sender, RoutedEventArgs e)
        {
            inkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
        }

        private void ProcessToolSelection(string tool)
        {
            switch (tool)
            {
                case "Black":
                    inkCanvas.DefaultDrawingAttributes.Color = Colors.Black;
                    break;
                case "Red":
                    inkCanvas.DefaultDrawingAttributes.Color = Colors.Red;
                    break;
                case "Green":
                    inkCanvas.DefaultDrawingAttributes.Color = Colors.Green;
                    break;
                case "Blue":
                    inkCanvas.DefaultDrawingAttributes.Color = Colors.Blue;
                    break;
                case "Yellow":
                    inkCanvas.DefaultDrawingAttributes.Color = Colors.Yellow;
                    break;
            }
        }
    }
}