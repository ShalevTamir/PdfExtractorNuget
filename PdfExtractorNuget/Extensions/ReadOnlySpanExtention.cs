using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PdfExtractor.Extensions
{
    internal static class ReadOnlySpanExtention
    {
        internal static ReadOnlySpan<char> Remove(this ReadOnlySpan<char> span, char charToRemove)
        {

            if (span == null) throw new ArgumentNullException($"{nameof(span)} can't be null");

            StringBuilder stringBuilder = new StringBuilder();
            foreach (char charInSpan in span)
            {
                if (charInSpan != charToRemove)
                    stringBuilder.Append(charInSpan);
            }
            return stringBuilder.ToString().AsSpan();
        }
        internal static bool EqualsExt(this ReadOnlySpan<char> span, char equalsTo)
        {
            if (span.Length != 1) return false;
            return span[0] == equalsTo;
        }
        internal static bool EqualsExt(this ReadOnlySpan<char> span, ICollection<char> collectionToCompare)
        {
            if (span.Length != collectionToCompare.Count) return false;
            int spanIndex = 0;
            foreach (var item in collectionToCompare)
            {
                if (span[spanIndex] != item) return false;
                spanIndex++;
            }
            return true;
        }
        internal static ReadOnlySpan<char> Replace(this ReadOnlySpan<char> span, char charToReplace, char charToReplaceWith)
        {
            if (span == null) throw new ArgumentNullException($"{nameof(span)} can't be null");

            StringBuilder stringBuilder = new StringBuilder();
            foreach (char charInSpan in span)
            {
                if (charInSpan == charToReplace)
                    stringBuilder.Append(charToReplaceWith);
                else
                    stringBuilder.Append(charInSpan);
            }
            return stringBuilder.ToString().AsSpan();
        }
        internal static ReadOnlySpan<char> Filter(this ReadOnlySpan<char> span, char charToFilter)
        {
            return Filter(span, new char[] { charToFilter });
        }
        internal static ReadOnlySpan<char> Filter(this ReadOnlySpan<char> span, IEnumerable<char> charsToRemove)
        {
            if (span == null || charsToRemove == null) throw new ArgumentException("Arguments can't be null");
            if (!charsToRemove.Any()) return span;

            StringBuilder stringBuilder = new StringBuilder();

            foreach (char characterToCheck in span)
                if (!charsToRemove.Contains(characterToCheck))
                    stringBuilder.Append(characterToCheck);

            return stringBuilder.ToString().AsSpan();
        }
        internal static ReadOnlySpan<char> RemoveDuplicateChar(this ReadOnlySpan<char> span, char duplicateChar)
        {
            if (span == null) throw new ArgumentException("Argument span can't be null");

            StringBuilder stringBuilder = new StringBuilder(span.ToString());
            for (int i = 1; i < stringBuilder.Length; i++)
            {
                if (stringBuilder[i - 1] == stringBuilder[i] && stringBuilder[i - 1] == duplicateChar)
                {
                    stringBuilder.Remove(i - 1, 1);
                    i--;
                }
            }

            return stringBuilder.ToString().AsSpan();
        }
        internal static int Count(this ReadOnlySpan<char> span, IEnumerable<char> charsToCount)
        {
            if (span == null) throw new ArgumentException("Argument span can't be null");
            int count = 0;

            foreach (char c in span)
                if (charsToCount.Contains(c)) count++;

            return count;
        }

        internal static int IndexOfNthOccurence(this ReadOnlySpan<char> span, IEnumerable<char> charsToLookFor, int occuranceNumber)
        {
            int countOccurences = 0;
            int currentIndex = 0;
            foreach (char character in span)
            {
                if (charsToLookFor.Contains(character)) countOccurences++;
                if (countOccurences == occuranceNumber) return currentIndex;
                currentIndex++;
            }
            return -1;
        }

        internal static bool Contains(this ReadOnlySpan<char> span, IEnumerable<char> charsToCheck)
        {
            foreach (char c in span)
                if (charsToCheck.Contains(c)) return true;
            return false;
        }



    }
}
