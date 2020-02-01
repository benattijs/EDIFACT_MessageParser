# EDIFACT Message Parser

## Background:
Taking the following EDIFACT message text, write some code to parse out all the LOC segments and populate an array with the 2nd and 3rd element of each segment.  

Note:  the ‘+’ is an element delimiter
```
UNA:+.? '
UNB+UNOC:3+2021000969+4441963198+180525:1225+3VAL2MJV6EH9IX+KMSV7HMD+CUSDECU-IE++1++1'
UNH+EDIFACT+CUSDEC:D:96B:UN:145050'
BGM+ZEM:::EX+09SEE7JPUV5HC06IC6+Z'
LOC+17+IT044100'
LOC+18+SOL'
LOC+35+SE'
LOC+36+TZ'
LOC+116+SE003033'
DTM+9:20090527:102'
DTM+268:20090626:102'
DTM+182:20090527:102'
```


### Input:
The input expected for the main Parse method is a string containing the message to be parsed.

### Output:
The output is an Array containing the elements of the "LOC" segments.

### Edge Cases and Assumptions 
1. Considered the fact that the message would have other segments then "LOC"
2. Considered that the message could be received in blank.
3. Considered that the code might eventually progress into parsing more than just "LOC" segments.
4. Considered that the output is generic to provide the flexibility to return more segments in the future. 
5. Considered that to add a new tag to be parsed, it would only be necessary to add code related to the new tag without having to modify the LOC rules.
6. Considered that some of the Test Scenarios might fail depending on the new implementations of new tags to be parsed.

## Code documentation
Project is divided and 2 projects:

### 1. EDIFACTMessageParser
This is the Main Library to be used to Parse a EDIFACT Message.

#### EDIFACTMessageParser/Parser.cs
Contains the logic that receives a message and parses the indivitual segments and return a generic `List<Segment>` that contains each a Tag corresponding to the Segment Tag information and all the Elements of the Segment.

#### EDIFACTMessageParser/Entities/Segment.cs
Contains the Object to represent a generic type of Segment.

#### EDIFACTMessageParser/Entities/LOCSegment.cs
Contains the Object to represent a LOC type of Segment. The constructor contains logic to check that the correct Type of object is being created for the appropriate Segment Tag. An `ArgumentException` will be thrown if the SegmentTag is different than LOC.

#### EDIFACTMessageParser/SegmentTags.cs
Class to standardize the Segment Tags supported.


### 2. EDIFACTMessageParserUnitTests
Contains the Unit Tests created to validate the main Library. 

#### EDIFACTMessageParserUnitTests/ParserTests.cs 
Contains the test cases and edge cases.
