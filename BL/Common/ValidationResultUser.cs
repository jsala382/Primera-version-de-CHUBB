using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Validation
{
    public class ValidationResultUser
    {
        public bool IsValid { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public int LineNumber { get; set; }
        public ValidationResultUser()
        {
            IsValid = true;
        }

        public void AddError(string errorMessage)
        {
            Errors.Add(errorMessage);
            IsValid = false;
        }
    }
}
