using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab2_WpfApp1
{
    public partial class MainWindow : Window
    {
        private void UpdateAdditionMultiplicationDetResults()
        {
            MatrixAAddition.Text = matrixA != null ? "Matrix A:\n" + matrixA.ToString() : "Matrix A:\nis not initialized.";
            MatrixBAddition.Text = matrixB != null ? "Matrix B:\n" + matrixB.ToString() : "Matrix B:\nis not initialized.";
            if (matrixA != null && matrixB != null)
            {
                try
                {
                    ResultAddition.Text = "Result:\n" + (matrixA + matrixB).ToString();
                }
                catch (Exception ex)
                {
                    ResultAddition.Text = "Result:\nCannot perform addition.\n" + ex.Message;
                }
            }
            else
            {
                ResultAddition.Text = "Result:\nCannot perform addition.\nOne or both matrices are not initialized.";
            }

            MatrixAMultiplication.Text = matrixA != null ? "Matrix A:\n" + matrixA.ToString() : "Matrix A:\nis not initialized.";
            MatrixBMultiplication.Text = matrixB != null ? "Matrix B:\n" + matrixB.ToString() : "Matrix B:\nis not initialized.";
            if (matrixA != null && matrixB != null)
            {
                try
                {
                    ResultMultiplication.Text = "Result:\n" + (matrixA * matrixB).ToString();
                }
                catch (Exception ex)
                {
                    ResultMultiplication.Text = "Result:\nCannot perform multiplication.\n" + ex.Message;
                }
            }
            else
            {
                ResultMultiplication.Text = "Result:\nCannot perform multiplication.\nOne or both matrices are not initialized.";
            }

            try
            {
                MatrixADet.Text = matrixA != null ? "Matrix A Determinant: " + matrixA.CalcDeterminant() : "Matrix A is not initialized.";
            }
            catch (Exception ex)
            {
                MatrixADet.Text = "Matrix A Determinant:\n" + ex.Message;
            }

            try
            {
                MatrixBDet.Text = matrixB != null ? "Matrix B Determinant: " + matrixB.CalcDeterminant() : "Matrix B is not initialized.";
            }
            catch (Exception ex)
            {
                MatrixBDet.Text = "Matrix B Determinant:\n" + ex.Message;
            }
        }
        private void MatrixAGetTransponedCopy_Click(object sender, RoutedEventArgs e) =>
            MatrixATransponedCopy.Text = matrixA != null ? "Matrix A:\n" + matrixA.GetTransponedCopy().ToString() : "Matrix A:\nis not initialized.";
        private void MatrixAGetTransponeMe_Click(object sender, RoutedEventArgs e)
        {
            if (matrixA != null)
            {
                matrixA.TransponeMe();
                MatrixATransponeMe.Text = "Matrix A:\n" + matrixA.ToString();
                UpdateAdditionMultiplicationDetResults();
            }
            else
            {
                MatrixATransponeMe.Text = "Matrix A:\nis not initialized.";
            }
        }
        private void MatrixBGetTransponedCopy_Click(object sender, RoutedEventArgs e) =>
            MatrixBTransponedCopy.Text = matrixB != null ? "Matrix B:\n" + matrixB.GetTransponedCopy().ToString() : "Matrix B:\nis not initialized.";
        private void MatrixBGetTransponeMe_Click(object sender, RoutedEventArgs e)
        {
            if (matrixB != null)
            {
                matrixB.TransponeMe();
                MatrixBTransponeMe.Text = "Matrix B:\n" + matrixB.ToString();
                UpdateAdditionMultiplicationDetResults();
            }
            else
            {
                MatrixBTransponeMe.Text = "Matrix B:\nis not initialized.";
            }
        }
    }
}
