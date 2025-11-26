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
        private void UpdateMatrixAGrid()
        {
            if (RowsA == null || ColsA == null || MatrixAGrid == null) return;
            int rows, cols;
            if (!int.TryParse(RowsA.Text, out rows) || !int.TryParse(ColsA.Text, out cols) || rows <= 0 || cols <= 0)
            {
                MessageBox.Show("Please enter valid size for the matrix.",
                    "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MatrixAGrid.Children.Clear();
            MatrixAGrid.Rows = rows;
            MatrixAGrid.Columns = cols;

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    TextBox tb = new TextBox
                    {
                        Name = $"ElementA{r + 1}{c + 1}",
                        MinWidth = 50,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                    };
                    MatrixAGrid.Children.Add(tb);
                }
            }
        }

        private void RowsA_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateMatrixAGrid();
        }

        private void ColsA_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateMatrixAGrid();
        }
        private void GenerateA_Click(object sender, RoutedEventArgs e)
        {
            int rows = int.Parse(RowsA.Text), cols = int.Parse(ColsA.Text);

            double[,] tempMatrix = new double[rows, cols];
            bool flag = true;
            for (int j = 0; j < cols; j++)
            {
                for (int i = 0; i < rows; i++)
                {
                    string elementName = $"ElementA{i + 1}{j + 1}";
                    var textBox = MatrixAGrid.Children
                        .OfType<TextBox>()
                        .FirstOrDefault(tb => tb.Name == elementName);

                    if (!double.TryParse(textBox.Text, out double value))
                    {
                        textBox.Text = "";
                        flag = false;
                        continue;
                    }

                    tempMatrix[i, j] = value;
                }
            }
            if (!flag)
            {
                tempMatrix = null;
                return;
            }
            matrixA = new MyMatrix(tempMatrix);
            ResultA.Text = $"{matrixA}";
            GetA_TextChanged(sender, new RoutedEventArgs());
        }

        private void GetA_TextChanged(object sender, RoutedEventArgs e)
        {
            bool rowValid = false, colValid = false;
            try
            {
                if (matrixA == null)
                    throw new InvalidOperationException("MatrixA is not initialized.");

                rowValid = int.TryParse(AGetIndexRow.Text, out int row) && row >= 1 && row <= matrixA.getHeight();
                colValid = int.TryParse(AGetIndexCol.Text, out int col) && col >= 1 && col <= matrixA.getWidth();

                if (!rowValid || !colValid)
                    throw new IndexOutOfRangeException("Index is out of range.");

                AGetElement.Text = matrixA.GetElement(row - 1, col - 1).ToString();
            }
            catch (IndexOutOfRangeException)
            {
                if (!rowValid)
                    AGetIndexRow.Text = "";
                if (!colValid)
                    AGetIndexCol.Text = "";
                AGetElement.Text = "";
            }
            catch (Exception)
            {
                AGetIndexRow.Text = "";
                AGetIndexCol.Text = "";
                AGetElement.Text = "";
            }
        }
        private void SetA_TextChanged(object sender, RoutedEventArgs e)
        {
            bool rowValid = false, colValid = false;
            try
            {
                if (matrixA == null)
                    throw new InvalidOperationException("MatrixA is not initialized.");
                rowValid = int.TryParse(ASetIndexRow.Text, out int row) && row >= 1 && row <= matrixA.getHeight();
                colValid = int.TryParse(ASetIndexCol.Text, out int col) && col >= 1 && col <= matrixA.getWidth();

                if (!rowValid || !colValid)
                    throw new IndexOutOfRangeException("Index is out of range.");
                if (!double.TryParse(ASetElement.Text, out double elem))
                    throw new Exception("Invalid value.");
                matrixA.SetElement(row - 1, col - 1, elem);
                string elementName = $"ElementA{row}{col}";
                var textBox = MatrixAGrid.Children
                    .OfType<TextBox>()
                    .FirstOrDefault(tb => tb.Name == elementName);
                textBox.Text = elem.ToString();
                GenerateA_Click(sender, new RoutedEventArgs());
                GetA_TextChanged(sender, new RoutedEventArgs());
            }
            catch (IndexOutOfRangeException)
            {
                if (!rowValid)
                    ASetIndexRow.Text = "";
                if (!colValid)
                    ASetIndexCol.Text = "";
                ASetElement.Text = "";
            }
            catch (InvalidOperationException)
            {
                ASetIndexRow.Text = "";
                ASetIndexCol.Text = "";
                ASetElement.Text = "";
            }
            catch (Exception)
            {
                ASetElement.Text = "";
            }
        }
        private void UpdateMatrixBGrid()
        {
            if (RowsB == null || ColsB == null || MatrixBGrid == null) return;
            int rows, cols;
            if (!int.TryParse(RowsB.Text, out rows) || !int.TryParse(ColsB.Text, out cols) || rows <= 0 || cols <= 0)
            {
                MessageBox.Show("Please enter valid size for the matrix.",
                    "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MatrixBGrid.Children.Clear();
            MatrixBGrid.Rows = rows;
            MatrixBGrid.Columns = cols;

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    TextBox tb = new TextBox
                    {
                        Name = $"ElementB{r + 1}{c + 1}",
                        MinWidth = 50,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                    };
                    MatrixBGrid.Children.Add(tb);
                }
            }
        }

        private void RowsB_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateMatrixBGrid();
        }

        private void ColsB_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateMatrixBGrid();
        }
        private void GenerateB_Click(object sender, RoutedEventArgs e)
        {
            int rows = int.Parse(RowsB.Text), cols = int.Parse(ColsB.Text);

            double[,] tempMatrix = new double[rows, cols];
            bool flag = true;
            for (int j = 0; j < cols; j++)
            {
                for (int i = 0; i < rows; i++)
                {
                    string elementName = $"ElementB{i + 1}{j + 1}";
                    var textBox = MatrixBGrid.Children
                        .OfType<TextBox>()
                        .FirstOrDefault(tb => tb.Name == elementName);

                    if (!double.TryParse(textBox.Text, out double value))
                    {
                        textBox.Text = "";
                        flag = false;
                        continue;
                    }

                    tempMatrix[i, j] = value;
                }
            }
            if (!flag)
            {
                tempMatrix = null;
                return;
            }
            matrixB = new MyMatrix(tempMatrix);
            ResultB.Text = $"{matrixB}";
            GetB_TextChanged(sender, new RoutedEventArgs());
        }
        private void GetB_TextChanged(object sender, RoutedEventArgs e)
        {
            bool rowValid = false, colValid = false;
            try
            {
                if (matrixB == null)
                    throw new InvalidOperationException("MatrixB is not initialized.");

                rowValid = int.TryParse(BGetIndexRow.Text, out int row) && row >= 1 && row <= matrixB.getHeight();
                colValid = int.TryParse(BGetIndexCol.Text, out int col) && col >= 1 && col <= matrixB.getWidth();

                if (!rowValid || !colValid)
                    throw new IndexOutOfRangeException("Index is out of range.");

                BGetElement.Text = matrixB.GetElement(row - 1, col - 1).ToString();
            }
            catch (IndexOutOfRangeException)
            {
                if (!rowValid)
                    BGetIndexRow.Text = "";
                if (!colValid)
                    BGetIndexCol.Text = "";
                BGetElement.Text = "";
            }
            catch (Exception)
            {
                BGetIndexRow.Text = "";
                BGetIndexCol.Text = "";
                BGetElement.Text = "";
            }
        }
        private void SetB_TextChanged(object sender, RoutedEventArgs e)
        {
            bool rowValid = false, colValid = false;
            try
            {
                if (matrixB == null)
                    throw new InvalidOperationException("MatrixB is not initialized.");
                rowValid = int.TryParse(BSetIndexRow.Text, out int row) && row >= 1 && row <= matrixB.getHeight();
                colValid = int.TryParse(BSetIndexCol.Text, out int col) && col >= 1 && col <= matrixB.getWidth();

                if (!rowValid || !colValid)
                    throw new IndexOutOfRangeException("Index is out of range.");
                if (!double.TryParse(BSetElement.Text, out double elem))
                    throw new Exception("Invalid value.");
                matrixB.SetElement(row - 1, col - 1, elem);
                string elementName = $"ElementB{row}{col}";
                var textBox = MatrixBGrid.Children
                    .OfType<TextBox>()
                    .FirstOrDefault(tb => tb.Name == elementName);
                textBox.Text = elem.ToString();
                GenerateB_Click(sender, new RoutedEventArgs());
                GetB_TextChanged(sender, new RoutedEventArgs());
            }
            catch (IndexOutOfRangeException)
            {
                if (!rowValid)
                    BSetIndexRow.Text = "";
                if (!colValid)
                    BSetIndexCol.Text = "";
                BSetElement.Text = "";
            }
            catch (InvalidOperationException)
            {
                BSetIndexRow.Text = "";
                BSetIndexCol.Text = "";
                BSetElement.Text = "";
            }
            catch (Exception)
            {
                BSetElement.Text = "";
            }
        }
    }
}