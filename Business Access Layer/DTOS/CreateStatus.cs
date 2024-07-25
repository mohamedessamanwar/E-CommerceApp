using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.DTOS
{
    public class CreateStatus <T> where T : class
    {
        public string Massage {  get; set; }
        public T? Value { get; set; }
        public bool Status { get; set; }
    }
}
