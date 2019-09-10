using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS1
{
    class Program
    {
        static void Main(string[] args)
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
            Console.ReadLine();
        }
    }
}
