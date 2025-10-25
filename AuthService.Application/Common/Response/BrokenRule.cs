using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Common.Response
{
    public class BrokenRule
    {
        public string RuleName { get; set; }

        public string Description { get; set; }

        public RuleSeverity Severity { get; set; }

        public override string ToString()
        {
            return Description;
        }

        public static implicit operator BrokenRule(string error)
        {
            return new BrokenRule
            {
                Description = error,
                Severity = RuleSeverity.Error
            };
        }
    }
}
