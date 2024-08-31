using System;
using System.Collections.Generic;

namespace Utils.ExpressionEval
{
    public static class ExpressionEval
    {
        public static int Eval(string str, Dictionary<string,int> values)
        {
            var split = str.Split(' ');
            var a = split[0];
            var op = split[1];
            var b = split[2];

            if (values.ContainsKey(a))
            {
                a = values[a].ToString();
            }
            if (values.ContainsKey(b))
            {
                b = values[b].ToString();
            }

            if (Int32.TryParse(a, out var aVal) && Int32.TryParse(b, out var bValue))
            {
                return Calculate(aVal, bValue,op);
            }
            throw new Exception($"Couldn't evaluate: {str}");
        }

        private static int Calculate(int aValue, int bValue, string op)
        {
            switch (op)
            {
                case "+":
                    return aValue + bValue;
                case "-":
                    return aValue - bValue;
                case "*":
                    return aValue * bValue;
                case "/":
                    return aValue / bValue;
            }
            return 0;
        }
    }
}
