using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCalc
{
    internal interface InterfaceOperations
    {
        decimal DoOperation(decimal val1, decimal val2);
    }
    public class Sum : InterfaceOperations
    {
        public decimal DoOperation(decimal val1, decimal val2) => val1 + val2;
    }
    public class  Substraction : InterfaceOperations
    {
        public decimal DoOperation(decimal val1, decimal val2) => val1 - val2;
    }
    public class Multiplication : InterfaceOperations 
    {
        public decimal DoOperation(decimal val1, decimal val2) => val1 * val2;
    }
    public class Division : InterfaceOperations
    {
        public decimal DoOperation(decimal val1, decimal val2) => val1 / val2;
    }

}
