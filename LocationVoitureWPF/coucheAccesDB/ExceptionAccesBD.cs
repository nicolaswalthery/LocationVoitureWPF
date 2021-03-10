using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationVoitureWPF.coucheAccesBD
{
    public class ExceptionAccesBD : Exception
    {
        public string details { get; private set; }
        public ExceptionAccesBD(string cause, string details) : base(cause)
        {
            this.details = details;
        }
    }
}
