using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Core.Models.Operation
{
    public class Result
    {
        public bool Succeeded { get; set; }
        public List<Error> Errors { get; set; } = new List<Error>();
    }
}
