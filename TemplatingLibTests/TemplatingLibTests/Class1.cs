using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplatingLibTests
{
    public class BusyMan
    {
        public void Slow()
        {
            var nthPrime = FindPrimeNumber(1000); //set higher value for more time
        }

        public long FindPrimeNumber(int n)
        {
            var count = 0;
            long a = 2;
            while (count < n)
            {
                long b = 2;
                var prime = 1; // to check if found a prime
                while (b * b <= a)
                {
                    if (a % b == 0)
                    {
                        prime = 0;
                        break;
                    }
                    b++;
                }
                if (prime > 0)
                    count++;
                a++;
            }
            return --a;
        }
    }
}
