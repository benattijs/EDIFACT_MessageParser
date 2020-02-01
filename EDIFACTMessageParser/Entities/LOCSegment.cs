using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDIFACTMessageParser.Entities
{
    public class LOCSegment : Segment 
    {
        public LOCSegment(string[] inputElements) : base(inputElements)
        {
            if(base.Tag != SegmentTags.LOC)
            {
                throw new ArgumentException($"Incorrect Segment Tag for {nameof(LOCSegment)} object"); 
            }

        }
    }
}
