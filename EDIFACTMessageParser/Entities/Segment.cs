using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDIFACTMessageParser 
{
    public class Segment
    {
        public string Tag { get; }
        public List<string> Elements { get; set; }

        public Segment(string[] inputElements)
        { 
            Elements = new List<string>();
            if (inputElements.Length > 0)
            {
                Tag = inputElements[0];
                for (int i = 1; i < inputElements.Length; i++)
                {
                    Elements.Add(inputElements[i]);
                    //Elements.Add("test");
                }

            }
        }

    }
}
