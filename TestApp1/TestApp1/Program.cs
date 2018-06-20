using System;
using System.Collections.Generic;
using System.Linq;

namespace TestApp1
{
    class Program
    {
        static void Main()
        {
            List<int> list = new List<int>() { 1, 2, 3 };
            int ele = list.ElementAtOrDefault(5);
        }

        #region Delegate
        void Delegate()
        {
            MathDel d = Sum;
            Print += printHelperFun;
            Console.WriteLine(d(5, 5));
            d = Diff;
            Console.WriteLine(d.Method);
        }

        int printHelperFun()
        {
            Console.WriteLine($"you are adding...");
            return 0;
        }

        int DelAsParam(MathDel d)
        {
            return d(9, 9);
        }

        public delegate int MathDel(int a, int b);
        public delegate int PrintHelper();

        public event PrintHelper Print;
        public int Sum(int a, int b)
        {
            Print?.Invoke();
            return a + b;
        }
        public int Diff(int a, int b) => a > b ? a - b : b - a;
        #endregion

        #region Yield

        static bool isFibonacci(int n)
        {
            bool isFibonacci = false;
            foreach (int f in Fibonacci())
            {
                if (f > n) return false;
                if (f == n) return true;
            }
            return isFibonacci;
        }

        static IEnumerable<int> Fibonacci()
        {
            int n1 = 0;
            int n2 = 1;

            yield return 1;
            while (true)
            {
                int n = n1 + n2;
                n1 = n2;
                n2 = n;
                yield return n;
            }
        }
        static void Yield1()
        {
            //
            // Compute two with the exponent of 30.
            //
            IEnumerable<(int numberResult, int exponentNum)> list = ComputePower(2, 30);
            foreach (var value in list)
            {
                Console.Write(value.numberResult);
                Console.WriteLine(value.exponentNum);
            }
            Console.WriteLine();
        }

        static void Yield2()
        {
            //
            // Compute two with the exponent of 30.
            //
            IEnumerable<int> list = FilterWithYield();
            foreach (var value in list)
            {
                Console.Write(value);
            }
            Console.WriteLine();
        }
        static IEnumerable<int> FilterWithYield()
        {
            List<int> MyList = new List<int>() { 1, 2, 3, 4, 5 };
            foreach (int i in MyList)
            {
                if (i > 3) yield return i;
            }
        }
        public static IEnumerable<(int, int)> ComputePower(int number, int exponent)
        {
            int exponentNum = 0;
            int numberResult = 1;
            //
            // Continue loop until the exponent count is reached.
            //
            while (exponentNum < exponent)
            {
                //
                // Multiply the result.
                //
                numberResult *= number;
                exponentNum++;
                //
                // Return the result with yield.
                //
                yield return (numberResult, exponentNum);
            }
        }
        #endregion

        #region Tuple
        static (int, int) GetPrice()
        {
            return (4, 5);
        }
        #endregion

        #region Anonymous Method
        delegate void Show();
        static void Demo(out int a)
        {
            string name = "Rohit";
            a = 10;
            Show p = delegate ()
            {
                /// "a" is not accessible - Anonymous method can not access ref or out param
                Console.WriteLine($"hello {name}");
            };
            p.Invoke();
            ParamDemo(p);
        }

        /// <summary>
        /// Anonymous method as param
        /// </summary>
        /// <param name="s"></param>
        static void ParamDemo(Show s) => s.Invoke();
        #endregion

        #region Func & Action & Predicate
        void Func()
        {
            Func<int, bool> isEven = a => (a % 2 == 0);

            bool res = isEven(10);
            res = isEven(7);
        }

        void Action()
        {
            Action<int> printCurrency = a => Console.WriteLine($"{a:c}");
            printCurrency(5000);
        }

        void Predicate()
        {
            Predicate<string> isUpperCase = a => a.Equals(a.ToUpper());
            isUpperCase("SHRADHA");
            isUpperCase("joshi");
        }
        #endregion

        #region Anonymous type
        static string Main3()
        {
            try
            {
                throw new Exception();
                var type = new
                {
                    name = "rohit singh",
                };

                DoSomethig(type);
            }
            catch (Exception)
            {
            }
            return "";
        }

        static void DoSomethig(dynamic param)
        {
            Console.WriteLine(param.name);
        }
        #endregion
    }

    #region Generic
    class System<T>
    {
        T t;
        public System(T t)
        {
            this.t = t;
        }
        internal T test()
        {
            global::System.Console.WriteLine("");
            return t;
        }
    }

    class DriveGeneric : System<int>
    {
        public DriveGeneric(int t) : base(t)
        {
        }
    }
    #endregion

    #region Covariance and Contravariance
    class Small { internal void SmallMethod() => Console.WriteLine("small"); }

    class Big : Small { internal void BigMethod() => Console.WriteLine("big"); }

    class Test
    {
        // Correct
        Small s1 = new Small();
        Small s2 = new Big();

        Big b1 = new Big();
        // Big b1 = new Small(); - Incorret - A drived class can not hold a base class

        public delegate Small covarDel(Big mc);

        #region Covariance : allows to use a derived class where a base class is expected
        static Big Method1(Big bg)
        {
            Console.WriteLine("Method1");
            bg.BigMethod();
            bg.SmallMethod();
            return new Big();
        }
        internal static void Main1()
        {
            covarDel del = Method1;

            Small sm = del(new Big());
            sm.SmallMethod();
        }
        #endregion

        #region Contravariane is applied to parameters. Parameter of a base class to be assigned to a delegate that expects parameter of a derived class
        static Small Method2(Small sm)
        {
            Console.WriteLine("Method2");
            return new Small();
        }

        void Main2()
        {
            covarDel del = Method2;
            Small sm = del(new Big());
        }
        #endregion
    }

    #endregion

    #region Extension Method
    public static class StringExtension
    {
        public static string ToTitleCase(this string str)
        {
            return new System.Globalization.CultureInfo("en-US", false).TextInfo.ToTitleCase(str);
        }
    }
    #endregion

    #region LINQ
    public class Linq
    {
        static IList<Student> studentList = new List<Student>() {
            new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
            new Student() { StudentID = 2, StudentName = "Steve",  Age = 15 } ,
            new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
            new Student() { StudentID = 4, StudentName = "Ram" , Age = 18 } ,
            new Student() { StudentID = 5, StudentName = "Ron" , Age = 15 }
        };

        internal static void Filter()
        {
            #region Where
            var stuList = from s in studentList
                          where s.Age > 18
                          select s;
            stuList = studentList.Where(s => s.Age > 18).ToList();
            #endregion

            #region OfType
            List<dynamic> list = new List<dynamic>() { "", 2, true, 4 };
            var filteredList = list.OfType<int>().ToList();
            #endregion
        }

        internal static void Group()
        {
            var group = studentList.GroupBy(s => s.Age);

            foreach (var item in group)
            {
                int age = item.Key;
                foreach (var item1 in item)
                {
                    Student s = item1;
                }
            }
        }

        internal static void Sorting()
        {
            var stuList = from s in studentList
                          orderby s.Age descending
                          select s;

            stuList = from s in studentList
                      orderby s.Age ascending, s.StudentID descending
                      select s;

            var stuList1 = studentList.OrderBy(s => s.StudentName).ThenByDescending(s =>s.StudentID).ToList<Student>();
        }
    }

    internal class Student
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public int Age { get; set; }
    }
    #endregion
    
    // GroupBy Example
    //public static MvcHtmlString GetSchoolsList(List<School> schoolList)
    //{
    //    var schoolHtml = new StringBuilder();
    //    var results = schoolList.OrderBy(x => x.SchoolLevelId).GroupBy(x=> x.SchoolLevelId).Select(g => new { Schools = g.Where(x => !string.IsNullOrEmpty(x.SchoolName)).Select(x => x.SchoolName).ToList() });
    //    foreach (var data in results.ToList())
    //    {
    //        schoolHtml.Append($"<p> {string.Join(", ", data.Schools)} </p>");
    //    }
    //   return schoolHtml.ToMvcHtmlString();
    //}
}
