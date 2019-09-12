using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS1
{
    class Program
    {
        public static string logFilePath = @"D:\GITHUB\WCFDemo\log.txt";
        public static string logFilePathForTime = @"D:\GITHUB\WCFDemo\logFilePathForTime.txt";

        static void Main(string[] args)
        {
            int totalCount = 1000;
            File.Delete(logFilePath);
            var stopwatch = Stopwatch.StartNew();

            //--------------------------------------------------------------------------------------
            //IEnumerable<int> numbers = Enumerable.Range(1, totalCount);
            //ServiceReference.Service1Client service = new ServiceReference.Service1Client();

            //Parallel.ForEach(numbers, index =>
            //{
            //    makeServiceCall(index, null);
            //});
            //LogText(string.Format("Total Time:{0}", stopwatch.Elapsed.TotalSeconds) , logCountFilePath);

            //--------------------------------------------------------------------------------------
            stopwatch = Stopwatch.StartNew();
            //creating the object of WCF service client
            LogText("Start : " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt"), true);
            var service = new ServiceReference.Service1Client();
            for (int index = 0; index < totalCount; index++)
            {
                makeServiceCallAsync(index, null);
            }
            LogText(string.Format("makeServiceCallAsync Total Time:{0}", stopwatch.Elapsed.TotalSeconds), true);
            LogText("End: " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt"), true);


            ////--------------------------------------------------------------------------------------
            //stopwatch = Stopwatch.StartNew();
            //for (int index = 0; index < totalCount; index++)
            //{
            //    makeServiceCall(index, null);
            //}
            //LogText(string.Format("makeServiceCall Total Time:{0}", stopwatch.Elapsed.TotalSeconds));
            Console.ReadKey();
        }

        public static void AgeCalculator()
        {
            int day, Month, Year, TotalDays;

            //creating the object of WCF service client         
            ServiceReference.Service1Client age = new ServiceReference.Service1Client();

            //assigning the input values to the variables      
            Console.WriteLine("Day");
            day = int.Parse(Console.ReadLine());
            Console.WriteLine("Month");
            Month = int.Parse(Console.ReadLine());
            Console.WriteLine("Year");
            Year = int.Parse(Console.ReadLine());

            //assigning the output value from service Response         
            TotalDays = age.CalculateAge(day, Month, Year);
            Console.Clear();
            Console.WriteLine();
            //assigning the output value to the lable to show user         
            Console.WriteLine("You are Currently " + Convert.ToString(TotalDays) + " days old");
        }

        public static async void makeServiceCallAsync(int index, ServiceReference.Service1Client service)
        {
            //creating the object of WCF service client
            if (service == null)
                service = new ServiceReference.Service1Client();
            StringBuilder text = new StringBuilder();
            text.Append("Start : " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt"));
            var result = await service.GetDataAsync(index);
            text.Append(" " + result);
            text.Append(" End: " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt"));
            LogText(text.ToString());

        }
        public static void makeServiceCall(int index, ServiceReference.Service1Client service)
        {
            //creating the object of WCF service client
            if (service == null)
                service = new ServiceReference.Service1Client();

            StringBuilder text = new StringBuilder();
            text.Append("Start : " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt"));
            text.Append("   " + service.GetData(index));
            text.Append("    End: " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt"));
            LogText(text.ToString());
        }

        private static object locker = new Object();
        public static void LogText(string text, bool isTimeLog = false)
        {
            string logLocation = "";
            if (isTimeLog)
                logLocation = logFilePathForTime;
            else
                logLocation = logFilePath;

            lock (locker)
            {
                text += Environment.NewLine;

                if (File.Exists(logLocation))
                    File.AppendAllText(logLocation, text);
                else
                    File.WriteAllText(logLocation, text);
            }
        }
    }
}
