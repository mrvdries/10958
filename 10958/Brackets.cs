using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10958
{
    class Brackets
    {
        private ArrayList list;

        public Brackets()
        {
            list = new ArrayList();
        }

        public ArrayList Generate()
        {
            string[] brackets = new string[10];
            for(int i = 0; i < 10; i++)
            {
                brackets[i] = "";
            }
            brackets[0] = "(";
            for(int j = 2; j < 10; j++)
            {
                string[] temp = (string[])brackets.Clone();
                temp[j] = ")";
                list.Add(temp);
                GenerateRec((string[])temp.Clone(), j);
            }
            return this.list;
        }

        public void GenerateRec(string[] brackets, int end)
        {
            for(int i = end+1; i < 10; i++)
            {
                string[] temp = (string[])brackets.Clone();
                temp[0] += "(";
                temp[i] += ")";
                list.Add(temp);
                if (i < 8)
                {
                    GenerateRec((string[])temp.Clone(), i);
                }
            }
        }

    }
}
