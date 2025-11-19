using Lab2_Exercise2;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Constructor_HMS_NormalizesCorrectly()
        {
            MyTime t = new MyTime(25, 61, 3661);
            Assert.Equal("3:02:01", t.ToString());
        }

        [Theory]
        [InlineData(0, "0:00:00")]
        [InlineData(3661, "1:01:01")]
        [InlineData(86399, "23:59:59")]
        [InlineData(86400, "0:00:00")]
        public void Constructor_FromSeconds_Works(int seconds, string expected)
        {
            MyTime t = new MyTime(seconds);
            Assert.Equal(expected, t.ToString());
        }

        [Fact]
        public void ToString_FormatsCorrectly()
        {
            MyTime t = new MyTime(5, 7, 9);
            Assert.Equal("5:07:09", t.ToString());
        }

        [Fact]
        public void ToSecSinceMidnight_WorksCorrectly()
        {
            MyTime t = new MyTime(1, 1, 1);
            Assert.Equal(3661, t.ToSecSinceMidnight());
        }

        [Fact]
        public void AddOneSecond_WrapsCorrectly()
        {
            MyTime t = new MyTime(23, 59, 59);
            t.AddOneSecond();
            Assert.Equal("0:00:00", t.ToString());
        }

        [Fact]
        public void AddOneMinute_WrapsCorrectly()
        {
            MyTime t = new MyTime(23, 59, 30);
            t.AddOneMinute();
            Assert.Equal("0:00:30", t.ToString());
        }

        [Fact]
        public void AddOneHour_WrapsCorrectly()
        {
            MyTime t = new MyTime(23, 10, 20);
            t.AddOneHour();
            Assert.Equal("0:10:20", t.ToString());
        }

        [Theory]
        [InlineData(0, 3600, "1:00:00")]
        [InlineData(23 * 3600 + 59 * 60 + 50, 15, "0:00:05")]
        [InlineData(3600, -3600, "0:00:00")]
        public void AddSeconds_Works(int startSeconds, int add, string expected)
        {
            MyTime t = new MyTime(startSeconds);
            t.AddSeconds(add);
            Assert.Equal(expected, t.ToString());
        }

        [Fact]
        public void Difference_Positive()
        {
            MyTime t1 = new MyTime(2, 0, 0);
            MyTime t2 = new MyTime(1, 0, 0);
            Assert.Equal(3600, t1.Difference(t2));
        }

        [Fact]
        public void Difference_Negative()
        {
            MyTime t1 = new MyTime(1, 0, 0);
            MyTime t2 = new MyTime(2, 0, 0);
            Assert.Equal(-3600, t1.Difference(t2));
        }

        [Fact]
        public void WhatLesson_Lesson1()
        {
            MyTime t = new MyTime(8, 30, 0);
            Assert.Equal("1-а пара.", t.WhatLesson());
        }

        [Fact]
        public void WhatLesson_BreakBetween1and2()
        {
            MyTime t = new MyTime(9, 25, 0);
            Assert.Equal("Перерва між 1-ю та 2-ю парами", t.WhatLesson());
        }

        [Fact]
        public void WhatLesson_BeforeClasses()
        {
            MyTime t = new MyTime(7, 0, 0);
            Assert.Equal("Пари ще не почалися.", t.WhatLesson());
        }

        [Fact]
        public void WhatLesson_AfterClasses()
        {
            MyTime t = new MyTime(18, 0, 0);
            Assert.Equal("Пари вже скінчилися", t.WhatLesson());
        }
    }
}