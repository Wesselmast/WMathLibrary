namespace WMath {

    // I added this WMath thing because I liked it, 
    // I know polynomials are way better for approximating these values
    // I did use two polynomials for my log function just because I wanted to try that too!

    public partial class WMath {
        public const double PI = 3.14159265359;
        public const double E = 2.71828182846;

        //this all works, but I'd be stupid to calculate these values realtime, it's just gonna lower the performance

        //private static double? pi = null;
        //private static double? e = null;

        //public static double PI {
        //    get {
        //        if (pi == null) {
        //            pi = 3;
        //            bool flag = true;
        //            for (int i = 2; i < 100; i += 2) {
        //                double diff = 4.0 / (i * (i + 1) * (i + 2));
        //                pi += flag ? diff : -diff;
        //                flag = !flag;
        //            }
        //        }
        //        return pi.Value;
        //    }
        //}

        //public static double E {
        //    get {
        //        if (e == null) e = Exp(1);
        //        return e.Value;
        //    }
        //}

        public static double Sqrt(double n) {
            return Root(n, 2.0);
        }

        public static double Sqr(double n) {
            return Pow(n, 2.0);
        }

        public static double Root(double n, double th) {
            if (th == 0) {
                throw new System.Exception("0th root is not defined");
            }
            if (n == 0) return 0;
            if (n < 0) return double.NaN;
            return Exp(Log(Abs(n)) / th);
        }

        public static double Pow(double n, double exponent) {
            if (n == 0) return 0;
            else if (exponent == 1) return n;
            else if (exponent == 0) return 1;
            return Exp(Log(Abs(n)) * exponent);
        }

        //this will diverge with giant numbers, still need to find a clean way of adapting to that
        public static double Exp(double n) {
            double fact = 1.0;
            double result = 1.0;

            for (int i = 1; i < 30; i++) {
                fact *= n;
                fact /= i;
                result += fact;
            }
            return result;
        }

        public static double Abs(double n) {
            if (n < 0) return -n;
            return n;
        }

        public static int Factorial(int n) {
            if (n == 0) return 1;
            int product = n;
            while (--n > 0) {
                product *= n;
            }
            return product;
        }

        public static double Log(double n) {
            if (n < 0) {
                throw new System.Exception("Trying to take log of a negative number");
            }
            else if (n == 0) {
                return double.MinValue;
            }
            else if (n < 1.0) {
                return -8.6731532 +
                      (129.946172 +
                     (-558.971892 +
                      (843.967330 -
                       409.109529 *
                               n) *
                               n) *
                               n) *
                               n;
            }
            int log2 = MSB((int)n);
            int divisor = 1 << log2;
            double normalized = n / divisor;
            double result = -1.7417939 +
                            (2.8212026 +
                           (-1.4699568 +
                           (0.44717955 -
                           0.056570851 *
                           normalized) *
                           normalized) *
                           normalized) *
                           normalized;
            return result + (log2 * 0.69314718);
        }

        public static int MSB(int n) {
            if (n <= 0) return 0;
            int result = -1;
            for (int i = n; n > 0; n >>= 1) {
                result++;
            }
            return result;
        }
    }
}
