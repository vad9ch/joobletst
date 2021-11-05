using System;
using System.Collections.Generic;
using System.Text;

namespace joobletst
{
    public class Contains
    {
        public Contains()
        {

        }
        public IEnumerable<string> ContainsS(string str, IEnumerable<string> dict)
        {
            List<string> bag = new List<string>();
            foreach (var st in dict)
            {
                if (str.Contains(st))
                {
                    var tep = false;
                    foreach (var stt in bag)
                    {

                        if (stt.Contains(st))
                        {
                            tep = true;
                        }
                    }
                    if (!tep)
                    {
                        bag.Add(st);
                    }

                }
            }
            if(bag.Count == 0)
            {
                bag.Add(str);
                bag.Add("Не разбивается");
            }


            return bag;
        }
    }
}
