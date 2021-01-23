using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handlers.UseCases
{
    public class Result<TValue>
    {
        internal Result(bool success, TValue value)
        {
            Value = value;
            IsSuccessful = success;
        }

        internal Result(bool success)
        {
            IsSuccessful = success;
        }

        public TValue Value { get; }
        public bool IsSuccessful { get; }

        public static Result<TValue> Success(TValue value) => new Result<TValue>(true, value);
        public static Result<TValue> Fail() => new Result<TValue>(false);
    }
}
