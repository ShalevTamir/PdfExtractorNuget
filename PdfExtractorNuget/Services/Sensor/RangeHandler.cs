using PdfExtractor.Extensions;
using System;
using System.Linq;

namespace PdfExtractor.Services.Sensor
{
    internal class RangeHandler
    {
        private static RangeHandler _instance;
        internal static RangeHandler Instance
        {
            get => _instance ??= new RangeHandler();
            set => _instance = value;
        }
        private RangeHandler() { }
        private const char RANGE_CHAR = '-';
        private const char GREATER_THAN_CHAR = '>';
        private const char LESS_THAN_CHAR = '<';
        private const char EQUALS_CHAR = '=';
        internal double[] TurnToRange(ReadOnlySpan<char> textToTurn)
        {
            try
            {
                if (textToTurn.Contains(GREATER_THAN_CHAR))
                {
                    return HandleInequalityChars(textToTurn);
                }
                else if (textToTurn.Contains(LESS_THAN_CHAR))
                {
                    return HandleInequalityChars(textToTurn);
                }
                else if (textToTurn.Contains(RANGE_CHAR))
                {
                    return HandleRangeChar(textToTurn);
                }
                else
                {
                    return new double[] { double.Parse(textToTurn) };
                }
            }
            catch (Exception e) when (e is FormatException | e is OverflowException)
            {
                 throw new FormatException($"Unable to parse range '{textToTurn.ToString()}'");
            }

        }

        private double[] HandleRangeChar(ReadOnlySpan<char> textToTurn)
        {
            int middleDashIndex = FindMiddleDashIndex(textToTurn);
            ReadOnlySpan<char> lowerRangeNumber = textToTurn.Slice(0, middleDashIndex);
            ReadOnlySpan<char> higherRangeNumber = textToTurn.Slice(middleDashIndex + 1);

            return new double[] {double.Parse(lowerRangeNumber),
                                 double.Parse(higherRangeNumber)};
        }
        
        private double[] HandleInequalityChars(ReadOnlySpan<char> textToTurn)
        {
            char inequalityChar = textToTurn.Contains(GREATER_THAN_CHAR) ? GREATER_THAN_CHAR : LESS_THAN_CHAR;
            
            bool containsEqualsChar = textToTurn[textToTurn.IndexOf(inequalityChar) + 1] == EQUALS_CHAR;
            int equalsOffset = containsEqualsChar ? 1 : 0;
            double numberOfRange = double.Parse(textToTurn[0] == inequalityChar ?
                                                            textToTurn.Slice(1+equalsOffset) :
                                                            textToTurn.Slice(0, textToTurn.Length - 1-equalsOffset));

            if (textToTurn[0] == GREATER_THAN_CHAR || textToTurn[textToTurn.Length - equalsOffset - 1] == LESS_THAN_CHAR)
            {
                if (containsEqualsChar) --numberOfRange;
                return new double[] { numberOfRange,
                                      double.PositiveInfinity };
            }
            else if (textToTurn[0] == LESS_THAN_CHAR || textToTurn[textToTurn.Length - equalsOffset - 1] == GREATER_THAN_CHAR)
            {
                if(containsEqualsChar) ++numberOfRange;
                return new double[] { double.NegativeInfinity,
                                      numberOfRange};
            }
            else throw new FormatException();
        }
        private int FindMiddleDashIndex(ReadOnlySpan<char> requirement)
        {
            int numOfRangeChars = requirement.Count(RANGE_CHAR);

            if (numOfRangeChars > 3 || numOfRangeChars < 1)
                throw new FormatException();

            int middleDashOccurance = numOfRangeChars == 1 ? 1 : requirement[0] == RANGE_CHAR ? 2 : 1;
            return requirement.IndexOfNthOccurence(RANGE_CHAR, middleDashOccurance);
        }

    }
}
