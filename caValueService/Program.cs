using libValueService;
using Microsoft.VisualBasic;

namespace caValueService
{
    internal class Program
    {
        static void Main(string[] args)
        {
          ValueService vs = new ValueService();
          Console.WriteLine(vs.GetDisplayValue(156.56m, 0));
        }
    }
}