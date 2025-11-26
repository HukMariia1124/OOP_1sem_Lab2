using Lab2_Exercise2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab2_WpfApp1
{
    public partial class MainWindow : Window
    {
        private void UpdateClock()
        {
            double secondsAngle = time.Seconds * 6;
            double minutesAngle = time.Minutes * 6 + time.Seconds * 0.1;
            //За одну хвилину стрілка проходить 6°.
            //За одну секунду — 1 / 60 хвилини.
            //6° / 60 секунд = 0.1° за одну секунду.
            double hoursAngle = (time.Hours % 12) * 30 + time.Minutes * 0.5;
            //  За одну годину стрілка проходить 30°.
            //  За одну хвилину — 1 / 60 годин.
            //  30° / 60 хвилин = 0.5° за одну хвилину.

            (HourHand.RenderTransform as RotateTransform)!.Angle = hoursAngle;
            (MinuteHand.RenderTransform as RotateTransform)!.Angle = minutesAngle;
            (SecondHand.RenderTransform as RotateTransform)!.Angle = secondsAngle;

            Time.Text = time.ToString();
        }
        private void GenerateMarksAndNumbers()
        {
            double centerX = DialCanvas.Width / 2;
            double centerY = DialCanvas.Height / 2;
            double radius = centerX - 15;

            for (int i = 0; i < 60; i++)
            {
                double angle = i * 6 * Math.PI / 180;
                double outerX = centerX + Math.Sin(angle) * radius;
                double outerY = centerY - Math.Cos(angle) * radius;

                double length = (i % 5 == 0) ? 15 : 7;
                double innerX = centerX + Math.Sin(angle) * (radius - length);
                double innerY = centerY - Math.Cos(angle) * (radius - length);

                Line mark = new Line
                {
                    X1 = innerX,
                    Y1 = innerY,
                    X2 = outerX,
                    Y2 = outerY,
                    Stroke = Brushes.Black,
                    StrokeThickness = (i % 5 == 0) ? 3 : 1
                };

                DialCanvas.Children.Add(mark);

                if (i % 5 == 0)
                {
                    int number = i / 5;
                    number = number == 0 ? 12 : number;

                    TextBlock text = new TextBlock
                    {
                        Text = number.ToString(),
                        FontSize = 20,
                        FontWeight = FontWeights.Bold
                    };

                    double textRadius = radius - 35;

                    double textX = centerX + Math.Sin(angle) * textRadius - 10;
                    double textY = centerY - Math.Cos(angle) * textRadius - 10;

                    Canvas.SetLeft(text, textX);
                    Canvas.SetTop(text, textY);

                    DialCanvas.Children.Add(text);
                }
            }
        }
        private void AddHour_Click(object sender, RoutedEventArgs e)
        {
            time.AddOneHour();
            UpdateClock();
        }
        private void AddMinute_Click(object sender, RoutedEventArgs e)
        {
            time.AddOneMinute();
            UpdateClock();
        }
        private void AddSecond_Click(object sender, RoutedEventArgs e)
        {
            time.AddOneSecond();
            UpdateClock();
        }
        private void AddSeconds_Click(object sender, RoutedEventArgs e)
        {
            int s;
            if (int.TryParse(SecondsAdd.Text, out s))
            {
                time.Seconds += s;
            }
            else
            {
                MessageBox.Show("Invalid Time", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            UpdateClock();
        }
        private void Constructor_Click(object sender, RoutedEventArgs e)
        {
            int h, m, s;
            if (int.TryParse(Hours.Text, out h) &&
                int.TryParse(Minutes.Text, out m) &&
                int.TryParse(Seconds.Text, out s))
            {
                time.Hours = h;
                time.Minutes = m;
                time.Seconds = s;
            }
            else
            {
                MessageBox.Show("Invalid Time", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            UpdateClock();
        }
        private void ConstructorSec_Click(object sender, RoutedEventArgs e)
        {
            int s;
            if (int.TryParse(Secs.Text, out s))
            {
                time.Hours = 0;
                time.Minutes = 0;
                time.Seconds = s;
            }
            else
            {
                MessageBox.Show("Invalid Time", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            UpdateClock();
        }
        private void ToSecSinceMidnight_Click(object sender, RoutedEventArgs e) => ToSecSinceMidnightText.Text = time.ToSecSinceMidnight().ToString();
        private void WhatLesson_Click(object sender, RoutedEventArgs e) => WhatLessonText.Text = time.WhatLesson();
        private void Difference_Click(object sender, RoutedEventArgs e)
        {
            int h, m, s;
            if (int.TryParse(HoursDiff.Text, out h) &&
                int.TryParse(MinutesDiff.Text, out m) &&
                int.TryParse(SecondsDiff.Text, out s))
            {
                MyTime otherTime = new MyTime(h, m, s);
                DifferenceText.Text = time.Difference(otherTime).ToString();
            }
            else
            {
                MessageBox.Show("Invalid Time", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
