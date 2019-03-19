using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace NativeCodeChallenge
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Sum(new List<int> { 1, 2, 3 }));
            Console.WriteLine(Sum(new List<int> { 2, 2, 4 }));
            Console.WriteLine(Sum(new List<int> { -4, 2 }));
            string response;
            new HttpRequestSender().MakeRequest("http://google.com",5000,out response);

            new NumberPrinter().PrintNumbers(new List<int>() { 1, 2, 3, 4, 5, 6, 7 });
            Console.ReadLine();
        }

        public static long? Sum(List<int> numbers)
        {
            if (numbers == null)
                return null;
            return numbers.Sum(i => i % 2 == 0 ? (long)i : 0);
        }

        
    }
}
