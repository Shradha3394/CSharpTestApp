using System;
using System.Collections.Generic;

namespace TestApp1
{
    class Program
    {
        static void Main()
        {
            Test.Main1();
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

        //Covariance : allows to use a derived class where a base class is expected
        public delegate Small covarDel(Big mc);

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
    }

    #endregion
}
