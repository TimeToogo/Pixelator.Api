using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class utes
    {
        public bool is_even(int number)
        {
            return (Decimal.Divide((decimal)number, (decimal)2) == (decimal)Math.Round(Decimal.Divide((decimal)number, 2)));
        }
    }
}
