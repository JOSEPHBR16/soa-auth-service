using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Common.Response
{
    public class RequestResult
    {
        public RequestResult()
        {
            BrokenRules = new BrokenRulesCollection();
        }


        public BrokenRulesCollection BrokenRules { get; }

        public static RequestResult WithError(string error)
        {
            var op = new RequestResult();
            op.BrokenRules.Add(error);
            return op;
        }

        public static RequestResult<T> WithError<T>(string error)
        {
            var op = new RequestResult<T>();
            op.BrokenRules.Add(error);
            return op;
        }

        public static RequestResult Success()
        {
            var op = new RequestResult();
            return op;
        }

        public static RequestResult<T> Success<T>(T data)
        {
            var op = new RequestResult<T>()
            {
                Data = data
            };
            return op;
        }

        public bool IsSuccess => BrokenRules.All(r => r.Severity != RuleSeverity.Error);
    }
}
