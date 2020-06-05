using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OAuthByWebProgrammer.JwtRequirement
{
    public interface IEmployeNumbersOfYears
    {
        int GetName(string name);
    }
    public class EmployeNumbersOfYears : IEmployeNumbersOfYears
    {
        public int GetName(string name)
        {
            if (name == "Sample1")
            {
                return 14;
            }
            return 10;
        }
    }
}
