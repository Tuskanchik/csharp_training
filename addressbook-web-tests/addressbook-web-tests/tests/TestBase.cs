using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WebAddressbookTests
{
    public class TestBase
    {
        protected ApplicationManager app;
        
        [SetUp]
        public void SetupApplicationManager()
        {
            app = ApplicationManager.GetInstance();
        }

        [TearDown]
        public void TeardownTest()
        {
            app.Auth.Logout();
        }

        public static Random rnd = new Random();
        public static string GenerateRandomString(int max)
        {
            int l = Convert.ToInt32(rnd.NextDouble() * max);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < l; i++)
            {
                builder.Append(Convert.ToChar(32 + Convert.ToInt32(rnd.NextDouble() * 223)));
            }
            return builder.ToString();
        }

        public static string GenerateRandomPhone()
        {
            int part1 = rnd.Next(900, 1000);
            int part2 = rnd.Next(100, 1000);
            int part3 = rnd.Next(10, 100);
            int part4 = rnd.Next(10, 100);

            int formFactor = rnd.Next(1, 5);

            if (formFactor == 1)
            {
                return $"+7 {part1} {part2} {part3} {part4}";
            }
            if (formFactor == 2)
            {
                return $"8 ({part1}) {part2} {part3} {part4}";
            }
            if (formFactor == 3)
            {
                return $"+7{part1}{part2}{part3}{part4}";
            }
            if (formFactor == 4)
            {
                return $"+7-{part1}-{part2}{part3}{part4}";
            }
            return $"+7 ({part1}) {part2}-{part3}-{part4}";
        }
    }
}
