using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10958
{

    class Command
    {
        private ArrayList ops;

        public Command(string input)
        {
            ops = new ArrayList();
            int pos = 0;
            for (int i = 0; i < input.Length; i++)
            {
                //assuming only single digits as possible input number values
                if (input[i] < '0' || input[i] > '9')
                {
                    ops.Add(input[i]);
                }
                else
                {
                    string num = ""+input[i];
                    if(i+1 < input.Length-1)
                    {
                        while (input[i + 1] >= '0' && input[i + 1] <= '9') //concat
                        {
                            num += input[i + 1];
                            i++;
                        }
                    }
                    ops.Add(Int32.Parse(num));
                    pos++;
                }

            }
        }

        public double Solve()
        {
            int start = ops.LastIndexOf('('); //position last open bracket
            int end = -1;
            if (start >= 0) { end = ops.IndexOf(')', start) - 1; } //position corresponding closing bracket
            while (start != -1)
            {
                end = SolveOperations('|', start, end);
                end = SolveOperations('^', start, end);
                end = SolveOperations('*', start, end);
                end = SolveOperations('/', start, end);
                end = SolveOperations('+', start, end);
                end = SolveOperations('-', start, end);
                if (start > -1) { ops.RemoveAt(start); }
                if (end > -1) {
                    ops.RemoveAt(end);
                }
                start = ops.LastIndexOf('('); //position last open bracket
                if (start >= 0) { end = ops.IndexOf(')', start) - 1; } //position corresponding closing bracket
            }
            SolveOperations('|');
            SolveOperations('^');
            SolveOperations('*');
            SolveOperations('/');
            SolveOperations('+');
            SolveOperations('-');
            return Convert.ToDouble(ops[0]);
        }

        public int SolveOperations(char op, int start, int end)
        {
            int pos = -1;
            if (start == -1 && end == -1) { pos = ops.LastIndexOf(op); }
            else { pos = ops.LastIndexOf(op, end); if (pos < start) { pos = -1; } }
            while (pos != -1)
            {
                double val = -1;
                switch (op) {
                    case '|': val = Convert.ToDouble("" + ops[pos - 1] + ops[pos + 1]); break;
                    case '^': val = Math.Pow(Convert.ToDouble(ops[pos - 1]), Convert.ToDouble(ops[pos + 1])); break;
                    case '*': val = Convert.ToDouble(ops[pos - 1]) * Convert.ToDouble(ops[pos + 1]); break;
                    case '/': val = Convert.ToDouble(ops[pos - 1]) / Convert.ToDouble(ops[pos + 1]); break;
                    case '+': val = Convert.ToDouble(ops[pos - 1]) + Convert.ToDouble(ops[pos + 1]); break;
                    case '-': val = Convert.ToDouble(ops[pos - 1]) - Convert.ToDouble(ops[pos + 1]); break;
                }
                ops.RemoveAt(pos); //remove operation
                ops.RemoveAt(pos); //remove following value
                end -= 2;
                ops[pos - 1] = val;
                if (start == -1 && end == -1) { pos = ops.LastIndexOf(op); }
                else {
                    pos = ops.LastIndexOf(op, end);
                    if (pos < start) { pos = -1; }
                }
            }
            return end;
        }

        public void SolveOperations(char op)
        {
            int pos = ops.LastIndexOf(op);
            while (pos != -1)
            {
                double val = -1;
                switch (op)
                {
                    case '^': val = Math.Pow(Convert.ToDouble(ops[pos - 1]), Convert.ToDouble(ops[pos + 1])); break;
                    case '*': val = Convert.ToDouble(ops[pos - 1]) * Convert.ToDouble(ops[pos + 1]); break;
                    case '/': val = Convert.ToDouble(ops[pos - 1]) / Convert.ToDouble(ops[pos + 1]); break;
                    case '+': val = Convert.ToDouble(ops[pos - 1]) + Convert.ToDouble(ops[pos + 1]); break;
                    case '-': val = Convert.ToDouble(ops[pos - 1]) - Convert.ToDouble(ops[pos + 1]); break;
                }
                ops.RemoveAt(pos); //remove operation
                ops.RemoveAt(pos); //remove following value
                ops[pos - 1] = val;
                pos = ops.LastIndexOf(op);
            }
        }

        public void Print()
        {
            string print = "";
            foreach(Object obj in ops)
            {
                print += obj.ToString();
            }
            Console.WriteLine(print);
        }

        public void Print(int start, int end)
        {
            string print = "";
            for (int i=start+1;i<end;i++)
            {
                print += ops[i].ToString();
            }
            Console.WriteLine(print);
        }

    }
}
