using Lab2_Exercise2;
using Lab2_WpfApp1.Models;
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
using System.Windows.Threading;

namespace Lab2_WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GenerateMarksAndNumbers();

            UpdateClock();
        }

        public MyTime time = new MyTime(0,0,0);
        public MyMatrix matrixA;
        public MyMatrix matrixB;
        private void TabControl_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (MainTabControl.SelectedItem == CreateMatrix)
            {
                ResultA.Text = $"{matrixA}";
                if (matrixA != null)
                {
                    int rows = matrixA.getHeight(), cols = matrixA.getWidth();
                    for (int j = 0; j < cols; j++)
                    {
                        for (int i = 0; i < rows; i++)
                        {
                            string elementName = $"ElementA{i + 1}{j + 1}";
                            var textBox = MatrixAGrid.Children
                                .OfType<TextBox>()
                                .FirstOrDefault(tb => tb.Name == elementName);

                            textBox.Text = matrixA[i, j].ToString();
                        }
                    }
                }
                GetA_TextChanged(sender, new RoutedEventArgs());
                ASetIndexRow.Text = "";
                ASetIndexCol.Text = "";
                ASetIndexRow.Text = "";
                ResultB.Text = $"{matrixB}";
                if (matrixB != null)
                {
                    int rows = matrixB.getHeight(), cols = matrixB.getWidth();
                    for (int j = 0; j < cols; j++)
                    {
                        for (int i = 0; i < rows; i++)
                        {
                            string elementName = $"ElementB{i + 1}{j + 1}";
                            var textBox = MatrixBGrid.Children
                                .OfType<TextBox>()
                                .FirstOrDefault(tb => tb.Name == elementName);

                            textBox.Text = matrixB[i, j].ToString();
                        }
                    }
                }
                GetB_TextChanged(sender, new RoutedEventArgs());
                BSetIndexRow.Text = "";
                BSetIndexCol.Text = "";
                BSetIndexRow.Text = "";
            }
            else if (MainTabControl.SelectedItem == MatrixOperations)
            {
                UpdateAdditionMultiplicationDetResults();
                MatrixATransponedCopy.Text = "Matrix A:";
                MatrixATransponeMe.Text = "Matrix A:";
                MatrixBTransponedCopy.Text = "Matrix B:";
                MatrixBTransponeMe.Text = "Matrix B:";
            }
        }
    }
}