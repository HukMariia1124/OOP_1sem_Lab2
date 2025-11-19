namespace Lab2_Exercise2
{
    public class MyTime
    {
        private int hours;
        private int minutes;
        private int seconds;

        private void Normalize()
        {
            minutes += seconds / 60;
            seconds = (seconds % 60 + 60) % 60;

            hours += minutes / 60;
            minutes = (minutes % 60 + 60) % 60;

            hours = (hours % 24 + 24) % 24;
        }

        public MyTime(int hours, int minutes, int seconds)
        {
            this.hours = hours;
            this.minutes = minutes;
            this.seconds = seconds;
            Normalize();
        }

        public MyTime(int secs)
        {
            hours = secs / 3600;
            minutes = secs / 60 % 60;
            seconds = secs % 60;
            Normalize();
        }

        public override string ToString() => $"{hours}:{minutes:D2}:{seconds:D2}";
        public int ToSecSinceMidnight() => hours * 3600 + minutes * 60 + seconds;

        public void AddOneSecond()
        {
            seconds++;
            Normalize();
        }

        public void AddOneMinute()
        {
            minutes++;
            Normalize();
        }

        public void AddOneHour()
        {
            hours++;
            Normalize();
        }

        public void AddSeconds(int s)
        {
            seconds += s;
            Normalize();
        }

        public int Difference(MyTime time) => this.ToSecSinceMidnight() - time.ToSecSinceMidnight();

        static readonly (int Start, int End)[] schedule =
        {
           (new MyTime(8, 0, 0).ToSecSinceMidnight(), new MyTime(9, 20, 0).ToSecSinceMidnight()),
           (new MyTime(9, 40, 0).ToSecSinceMidnight(), new MyTime(11, 0, 0).ToSecSinceMidnight()),
           (new MyTime(11, 20, 0).ToSecSinceMidnight(), new MyTime(12, 40, 0).ToSecSinceMidnight()),
           (new MyTime(13, 0, 0).ToSecSinceMidnight(), new MyTime(14, 20, 0).ToSecSinceMidnight()),
           (new MyTime(14, 40, 0).ToSecSinceMidnight(), new MyTime(16, 0, 0).ToSecSinceMidnight()),
           (new MyTime(16, 10, 0).ToSecSinceMidnight(), new MyTime(17, 30, 0).ToSecSinceMidnight())
        };

        public string WhatLesson()
        {
            int secs = this.ToSecSinceMidnight();

            for (int i = 0; i < schedule.Length; i++)
            {
                if (secs >= schedule[i].Start && secs < schedule[i].End)
                {
                    return $"{i + 1}-а пара.";
                }

                if (i < schedule.Length - 1 && secs >= schedule[i].End && secs < schedule[i + 1].Start)
                {
                    return $"Перерва між {i + 1}-ю та {i + 2}-ю парами";
                }
            }

            if (secs < schedule[0].Start)
            {
                return "Пари ще не почалися.";
            }

            return "Пари вже скінчилися";
        }
    }
}
