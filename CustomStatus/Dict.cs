namespace CustomStatus
{
    public class Dict
    {
        public static string Years (int count)
        {
            if (count % 100 >= 11 && count % 100 <= 20)
                return count.ToString() + " лет";
            else if (count % 10 == 1)
                return count.ToString() + " год";
            else if (count % 10 >= 2 && count % 10 <= 4)
                return count.ToString() + " года";
            else if (count % 10 >= 5 && count % 10 <= 9 || count % 10 == 0)
                return count.ToString() + " лет";
            return count.ToString();
        }

        public static string Days (int count)
        {
            if (count % 100 >= 11 && count % 100 <= 20)
                return count.ToString() + " дней";
            else if (count % 10 == 1)
                return count.ToString() + " день";
            else if (count % 10 >= 2 && count % 10 <= 4)
                return count.ToString() + " дня";
            else if (count % 10 >= 5 && count % 10 <= 9 || count % 10 == 0)
                return count.ToString() + " дней";
            return count.ToString();
        }

        public static string Hours (int count)
        {
            if (count % 100 >= 11 && count % 100 <= 20)
                return count.ToString() + " часов";
            else if (count % 10 == 1)
                return count.ToString() + " час";
            else if (count % 10 >= 2 && count % 10 <= 4)
                return count.ToString() + " часа";
            else if (count % 10 >= 5 && count % 10 <= 9 || count % 10 == 0)
                return count.ToString() + " часов";
            return count.ToString();
        }

        public static string Minutes (int count)
        {
            if (count % 100 >= 11 && count % 100 <= 20)
                return count.ToString() + " минут";
            else if (count % 10 == 1)
                return count.ToString() + " минуту";
            else if (count % 10 >= 2 && count % 10 <= 4)
                return count.ToString() + " минуты";
            else if (count % 10 >= 5 && count % 10 <= 9 || count % 10 == 0)
                return count.ToString() + " минут";
            return count.ToString();
        }

        public static string Seconds (int count)
        {
            if (count % 100 >= 11 && count % 100 <= 20)
                return count.ToString() + " секунд";
            else if (count % 10 == 1)
                return count.ToString() + " секунду";
            else if (count % 10 >= 2 && count % 10 <= 4)
                return count.ToString() + " секунды";
            else if (count % 10 >= 5 && count % 10 <= 9 || count % 10 == 0)
                return count.ToString() + " секунд";
            return count.ToString();
        }
    }
}
