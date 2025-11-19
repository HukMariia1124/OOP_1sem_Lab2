using Lab2_WpfApp1.Models;
using Xunit;

public class MyMatrixTests
{
    [Fact]
    public void Constructor_From2DArray_CreatesCorrectMatrix()
    {
        double[,] arr = {{ 1, 2 },{ 3, 4 }};

        var m = new MyMatrix(arr);

        Assert.Equal(2, m.Height);
        Assert.Equal(2, m.Width);
        Assert.Equal(1, m[0, 0]);
        Assert.Equal(2, m[0, 1]);
        Assert.Equal(3, m[1, 0]);
        Assert.Equal(4, m[1, 1]);
    }
    [Fact]
    public void Constructor_FromJaggedArray_CreatesCorrectMatrix()
    {
        double[][] jagged = new double[][]
        {
                new double[] { 1, 2, 3 },
                new double[] { 4, 5, 6 }
        };

        var m = new MyMatrix(jagged);

        Assert.Equal(2, m.Height);
        Assert.Equal(3, m.Width);
        Assert.Equal(1, m[0, 0]);
        Assert.Equal(2, m[0, 1]);
        Assert.Equal(3, m[0, 2]);
        Assert.Equal(4, m[1, 0]);
        Assert.Equal(5, m[1, 1]);
        Assert.Equal(6, m[1, 2]);
    }

    [Fact]
    public void Constructor_FromJaggedArray_ThrowsIfNotRectangular()
    {
        double[][] jagged = new double[][]
        {
                new double[] { 1, 2 },
                new double[] { 3 }
        };

        Assert.Throws<ArgumentException>(() => new MyMatrix(jagged));
    }
    [Fact]
    public void Constructor_FromStringArray_CreatesCorrectMatrix()
    {
        string[] rows =
        {
                "1 2 3",
                "4 5 6"
            };

        var m = new MyMatrix(rows);

        Assert.Equal(2, m.Height);
        Assert.Equal(3, m.Width);
        Assert.Equal(1, m[0, 0]);
        Assert.Equal(2, m[0, 1]);
        Assert.Equal(3, m[0, 2]);
        Assert.Equal(4, m[1, 0]);
        Assert.Equal(5, m[1, 1]);
        Assert.Equal(6, m[1, 2]);
    }

    [Fact]
    public void Constructor_FromStringArray_ThrowsOnDifferentCols()
    {
        string[] rows =
        {
                "1 2",
                "3 4 5"
            };

        Assert.Throws<ArgumentException>(() => new MyMatrix(rows));
    }
    [Fact]
    public void Constructor_CopyConstructor_CreatesDeepCopy()
    {
        double[,] arr = { { 1, 2 }, { 3, 4 } };

        var original = new MyMatrix(arr);
        var copy = new MyMatrix(original);

        Assert.Equal(4, copy[1, 1]);

        original[1, 1] = 999;

        Assert.Equal(4, copy[1, 1]);
    }
    [Fact]
    public void Constructor_FromSingleString_CreatesCorrectMatrix()
    {
        string data = "1 2 3\n4 5 6";

        var m = new MyMatrix(data);

        Assert.Equal(2, m.Height);
        Assert.Equal(3, m.Width);
        Assert.Equal(1, m[0, 0]);
        Assert.Equal(2, m[0, 1]);
        Assert.Equal(3, m[0, 2]);
        Assert.Equal(4, m[1, 0]);
        Assert.Equal(5, m[1, 1]);
        Assert.Equal(6, m[1, 2]);
    }
    [Fact]
    public void Constructor_FromSingleString_ThrowsIfNotRectangular()
    {
        string data = "1 2\n3 4 5";

        Assert.Throws<ArgumentException>(() => new MyMatrix(data));
    }

    [Fact]
    public void Addition()
    {
        var m1 = new MyMatrix(new double[,] { { 1, 2 }, { 3, 4 } });
        var m2 = new MyMatrix(new double[,] { { 5, 6 }, { 7, 8 } });

        var sum = m1 + m2;

        Assert.Equal(6, sum[0, 0]);
        Assert.Equal(8, sum[0, 1]);
        Assert.Equal(10, sum[1, 0]);
        Assert.Equal(12, sum[1, 1]);
    }

    [Fact]
    public void Multiplication()
    {
        var a = new MyMatrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
        var b = new MyMatrix(new double[,] { { 7, 8 }, { 9, 10 }, { 11, 12 } });

        var result = a * b;

        Assert.Equal(58, result[0, 0]);
        Assert.Equal(64, result[0, 1]);
        Assert.Equal(139, result[1, 0]);
        Assert.Equal(154, result[1, 1]);
    }

    [Fact]
    public void Transpose()
    {
        var m = new MyMatrix(new double[,] { { 1, 2 }, { 3, 4 } });
        var t = m.GetTransponedCopy();

        Assert.Equal(1, t[0, 0]);
        Assert.Equal(3, t[0, 1]);
        Assert.Equal(2, t[1, 0]);
        Assert.Equal(4, t[1, 1]);
    }

    [Fact]
    public void Determinant_2x2()
    {
        var m = new MyMatrix(new double[,] { { 1, 2 }, { 3, 4 } });
        Assert.Equal(-2, m.CalcDeterminant(), 5);
    }

    [Fact]
    public void Determinant_3x3()
    {
        var m = new MyMatrix(new double[,] { { 6, 1, 1 }, { 4, -2, 5 }, { 2, 8, 7 } });
        Assert.Equal(-306, m.CalcDeterminant(), 5);
    }

    [Fact]
    public void Determinant_Cached_DoesNotRecompute()
    {
        var m = new MyMatrix(new double[,] { { 2, 4 }, { 1, 3 } });

        double d1 = m.CalcDeterminant();
        double d2 = m.CalcDeterminant();

        Assert.Equal(d1, d2);
    }

    [Fact]
    public void Determinant_CacheInvalidates_WhenMatrixChanges()
    {
        var m = new MyMatrix(new double[,] { { 2, 4 }, { 1, 3 } });

        double d1 = m.CalcDeterminant();

        m[0, 0] = 10;

        double d2 = m.CalcDeterminant();

        Assert.NotEqual(d1, d2);
    }
}
