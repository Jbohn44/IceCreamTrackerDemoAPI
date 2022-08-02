using DomainModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class ExternalAuth : IExternalAuth
    {
        public string Provider { get; set; }
        public string IdToken { get; set; }
    }
}
