using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace BFT
{
    public static class StringUtils
    {
        public static bool IsNullOrEmpty(this string toCheck)
        {
            return System.String.IsNullOrEmpty(toCheck);
        }

        public static string Directorized(this string directory)
        {
            if (!directory.EndsWith("/"))
            {
                return directory + "/";
            }

            return directory;
        }

        public static string AsFolder(this string filePath)
        {
            return Path.GetDirectoryName(filePath);
        }

        public static string Italic(this string str)
        {
            return new StringBuilder().Append("<i>").Append(str).Append("</i>").ToString();
        }

        public static string Bold(this string str)
        {
            return new StringBuilder().Append("<b>").Append(str).Append("</b>").ToString();
        }

        public static string Colored(this string str, Color color)
        {
            return new StringBuilder().Append("<color=").Append(color.ToHtml()).Append(">").Append(str).Append("</color>")
                .ToString();
        }

        public static string Highlight(this string item, string searchTerms,
            string highlightPrefix = "<b><color=#008293>", string highlightSuffix = "</color></b>")
        {
            bool[] lettersToHighlight = new bool[item.Length];

            foreach (var term in searchTerms.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries))
                HighlightWord(item, term, lettersToHighlight);

            var sb = new StringBuilder();
            var chars = item.ToCharArray();
            for (int index = 0; index < chars.Length; index++)
            {
                bool previousHighlighted = index != 0 && lettersToHighlight[index - 1];
                bool currentHighlighted = lettersToHighlight[index];
                if (!previousHighlighted && currentHighlighted)
                    sb.Append(highlightPrefix);
                if (previousHighlighted && !currentHighlighted)
                    sb.Append(highlightSuffix);
                sb.Append(chars[index]);
                if (index == chars.Length - 1 && currentHighlighted)
                    sb.Append(highlightSuffix);
            }

            return sb.ToString();
        }

        public static void HighlightWord(this string item, string term, bool[] lettersToHighlight)
        {
            var index = item.IndexOf(term, StringComparison.OrdinalIgnoreCase);
            if (index != -1)
            {
                for (int i = index; i < index + term.Length; i++)
                    lettersToHighlight[i] = true;
                return;
            }

            var initials = CamelCaseFor(item);
            if (initials.StartsWith(term.ToUpper()))
            {
                lettersToHighlight[0] = true;
                int count = term.Length - 1;
                var chars = item.ToCharArray();
                for (int i = 1; i < chars.Length; i++)
                {
                    if (count <= 0)
                        return;
                    var character = chars[i];
                    if (char.IsUpper(character))
                    {
                        lettersToHighlight[i] = true;
                        count--;
                    }
                }

                return;
            }

            var match = Regex.Match(item, RegexFor(term), RegexOptions.IgnoreCase);
            if (match.Success)
            {
                for (int i = match.Index; i < match.Index + match.Length; i++)
                    lettersToHighlight[i] = true;
            }
        }

        public static IEnumerable<string> OrderStringsBySimilarities(IEnumerable<string> strings,
            bool ignoreMaxPenalties = false, params string[] searchTerms)
        {
            Dictionary<string, int> penalties = new Dictionary<string, int>();

            var enumerable = strings as string[] ?? strings.ToArray();
            foreach (string s in enumerable)
            {
                int penalty = ComputePenalty(s, searchTerms);
                if (ignoreMaxPenalties && penalty == Int32.MaxValue)
                    continue;
                penalties.Add(s, penalty);
            }

            return enumerable.Where(x => penalties.ContainsKey(x)).OrderBy(x => penalties[x]);
        }

        public static int ComputePenalty(this string item, params string[] searchTerms)
        {
            var penalty = 0;
            var split = item.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            foreach (var searchTerm in searchTerms)
            {
                if (searchTerm.StartsWith(":"))
                    continue;
                var penaltyFor = PenaltyFor(split, searchTerm);
                if (penaltyFor == Int32.MaxValue)
                    return Int32.MaxValue;
                penalty += penaltyFor;
            }

            return penalty;
        }

        public static int PenaltyFor(string[] splitFileName, string searchTerm)
        {
            foreach (var itemInPath in splitFileName)
                if (itemInPath == searchTerm)
                    return 0;
            foreach (var itemInPath in splitFileName)
                if (System.String.Equals(itemInPath, searchTerm, StringComparison.OrdinalIgnoreCase))
                    return 1;
            foreach (var itemInPath in splitFileName)
                if (itemInPath.StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase))
                    return 5;
            foreach (var itemInPath in splitFileName)
                if (itemInPath.ToLower().StartsWith(searchTerm.ToLower(), StringComparison.OrdinalIgnoreCase))
                    return 6;
            foreach (var itemInPath in splitFileName)
                if (itemInPath.ToLower().IndexOf(searchTerm.ToLower(), StringComparison.Ordinal) > 0)
                    return 10;
            foreach (var itemInPath in splitFileName)
                if (itemInPath.ToLower().IndexOf(searchTerm.ToLower(), StringComparison.OrdinalIgnoreCase) > 0)
                    return 11;

            return splitFileName.Min(x => PenaltyFor(x, searchTerm));
        }

        public static int PenaltyFor(this string item, string searchTerm)
        {
            System.String initials = CamelCaseFor(item);
            if (initials == searchTerm.ToUpper())
                return 20;
            if (initials.StartsWith(searchTerm.ToUpper()))
                return 22;

            if (searchTerm.Contains("*") || searchTerm.Contains("?") &&
                searchTerm.ToCharArray().Any(x => x != ' ' && x != '?' && x != '*'))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("^.*");
                RegexFor(searchTerm, sb);
                sb.Append(".*$");

                if (Regex.IsMatch(item, sb.ToString()))
                    return 30;
                if (Regex.IsMatch(item, sb.ToString(), RegexOptions.IgnoreCase))
                    return 31;
            }

            return Int32.MaxValue;
        }

        public static int LevenshteinDistance(this string s, string other)
        {
            int n = s.Length;
            int m = other.Length;
            int[,] d = new int[n + 1, m + 1];

            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (other[j - 1] == s[i - 1]) ? 0 : 1;

                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }

            return d[n, m];
        }


        private static string CamelCaseFor(string text)
        {
            var initials = new StringBuilder();
            initials.Append(text[0].ToString().ToUpper());
            for (int index = 1; index < text.ToCharArray().Length; index++)
            {
                var character = text.ToCharArray()[index];
                if (char.IsUpper(character))
                    initials.Append(character);
            }

            return initials.ToString();
        }

        public static string RegexFor(this string searchTerm)
        {
            StringBuilder sb = new StringBuilder();
            RegexFor(searchTerm, sb);
            return sb.ToString();
        }

        public static void RegexFor(this string searchTerm, StringBuilder sb)
        {
            foreach (var character in searchTerm.ToCharArray())
            {
                if (character == '?')
                    sb.Append('.');
                else if (character == '*')
                    sb.Append(".*");
                else
                    sb.Append(Regex.Escape(character.ToString()));
            }
        }
    }
}
