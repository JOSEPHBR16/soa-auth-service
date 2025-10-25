using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Common.Response
{
    public class BrokenRulesCollection : List<BrokenRule>
    {
        public int ErrorCount => this.Count(c => c.Severity == RuleSeverity.Error);

        public int WarningCount => this.Count(c => c.Severity == RuleSeverity.Warning);

        public int InformationCount => this.Count(c => c.Severity == RuleSeverity.Information);

        public BrokenRulesCollection(IEnumerable<BrokenRule> brokenRules) : base(brokenRules)
        {
        }

        public BrokenRulesCollection()
        {
        }


        public override string ToString()
            => ToString(Environment.NewLine);

        private string ToString(RuleSeverity severity)
            => ToString(Environment.NewLine, severity);

        private string ToString(string separator)
            => string.Join(separator, this.Select(c => c.Description));

        private string ToString(string separator, RuleSeverity severity)
        {
            var descriptions = this.Where(c => c.Severity == severity).Select(c => c.Description);
            return string.Join(separator, descriptions);
        }

        private string[] ToArray(RuleSeverity severity)
            => this.Where(c => c.Severity == severity)
                    .Select(c => c.Description)
            .ToArray();
    }
}
