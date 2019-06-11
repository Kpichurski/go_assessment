using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GeneralKnowledge.Test.App.Tests
{
    /// <summary>
    /// Basic string manipulation exercises
    /// </summary>
    public class StringTests : ITest
    {
        public void Run()
        {
            // TODO:
            // Complete the methods below

            AnagramTest();
            GetUniqueCharsAndCount();
        }

        private void AnagramTest()
        {
            var word = "stop";
            var possibleAnagrams = new string[] { "test", "tops", "spin", "post", "mist", "step" };

                foreach (var possibleAnagram in possibleAnagrams)
            {
                Console.WriteLine(string.Format("{0} > {1}: {2}", word, possibleAnagram, possibleAnagram.IsAnagram(word)));
            }
        }

        private void GetUniqueCharsAndCount()
        {
            var word = "xxzwxzyzzyxwxzyxyzyxzyxzyzyxzzz";
             
               var countWord = word.GroupBy(c => c)
                   .OrderBy(c => c.Key)
                   .ToDictionary(grp => grp.Key, grp => grp.Count());
            foreach (var character in countWord)
            {
                Console.WriteLine(character.Key + ": " + character.Value);
            }
        }
    }

    public static class StringExtensions
    {
        public static bool IsAnagram(this string a, string b)
        {
            a = string.Concat(a.OrderBy(x => x));
            b = string.Concat(b.OrderBy(y => y));
            if (a == b)
                return true;

            return false;
        }
    }
}
