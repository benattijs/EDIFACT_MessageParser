using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDIFACTMessageParser
{
    public static class Parser
    {
        /*
        * Message sample
        UNA :+.? '
        UNB +UNOC:3+2021000969+4441963198+180525:1225+3VAL2MJV6EH9IX+KMSV7HMD+CUSDECU-IE++1++1'
        UNH +EDIFACT+CUSDEC:D:96B:UN:145050'
        BGM +ZEM:::EX+09SEE7JPUV5HC06IC6+Z'
        LOC +17+IT044100'
        LOC +18+SOL'
        LOC +35+SE'
        LOC +36+TZ'
        LOC +116+SE003033'
        DTM +9:20090527:102'
        DTM +268:20090626:102'
        DTM +182:20090527:102'
        */
        static char ElementDelimiter = '+';

        public static List<Segment> ParseAllElements(string message)
        {
            List<Segment> allSegments = new List<Segment>();

            using (StringReader reader = new StringReader(message))
            {
                string segment;
                while ((segment = reader.ReadLine()?.Trim()) != null)
                {
                    if(segment.Length>0 && segment.IndexOf(ElementDelimiter) > -1)
                    {
                        string segmentTag = segment.Substring(0, segment.IndexOf(ElementDelimiter));
                        switch (segmentTag)
                        {
                            case Segments.LOC:
                                var response = ParseLOCSegment(segment);
                                allSegments.Add(response);
                                break;
                            default:
                                //Not Implemented. Ignoring all other types of Segments. 
                                //If a new segment Tag needs to be returned, the type needs to be added in the case and a specific method to parse that segment needs to be added
                                break;
                        }

                    }
                    
                }
            }

            return allSegments;
        }

        private static Segment ParseLOCSegment(string segment)
        {
            //Logics specific for parcing LOC Segments
            Segment response = new Segment(segment.Split(ElementDelimiter));
            
            return response;
        }

    }
}
