using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAnalayser
{
    class SentimentAnalyserOld
    {
        public Microsoft.Office.Interop.Word.Application app;
        List<string> sentiment;
        List<string> good;
        List<string> bad;
        List<string> common;
        List<string> b = new List<string> { "crazy", "absolutely", "certainly", "complete", "completely", "definitely", "especially", "extremely", "fuckin", "fucking", "hugely", "incredibly", "overwhelmingly", "really", "totally", "very" };
        List<string> negb = new List<string> { "didnot", "didnt", "didn't", "couldnt", "couldnot", "couldn't", "aren't", "arent", "can't", "cannot", "cant", "couldn't", "couldnt", "don't", "dont", "isn't", "isnt", "never", "won't", "wont", "wouldn't", "wouldnt" };

        Dictionary<string, int> dictonary = new Dictionary<string, int>();

        public string process(string sentence)
        {
            setup();
            //bad.AddRange(negb);
            //negb.Clear();
            //string sentence = "I hate my country";
            //var matches = Regex.Matches(sentence, @"((\b[^\s]+\b)((?<=\.\w).)?)");
            sentence = Regex.Replace(sentence, "[^a-zA-Z0-9_]+", " ", RegexOptions.Compiled);
            List<string> s = sentence.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            List<int> senti = s.Where(i => dictonary.ContainsKey(i)).Select(i => dictonary[i]).ToList();
            // MessageBox.Show(((float)senti.Sum() / Math.Sqrt(senti.Count())).ToString());


            s = s.Where(a => !common.Contains(a.ToLower())).ToList();

            int p = 0;
            int n = 0;
            string pword = "";
            int pos = 0;
            int neg = 0;
            bool isNeg = true;
            foreach (string strW in s)
            {
                isNeg = false;

                bool isBorBN = false;
                bool canContinue = generateScore(strW, ref p, ref n, ref  pos, ref  neg, ref  isNeg, ref isBorBN);
                if (canContinue) { if (!isBorBN) { p = 0; n = 0; } continue; }
                object lang = WdLanguageID.wdEnglishUS;

                var infosyn = app.get_SynonymInfo(strW, ref lang).MeaningList;

                foreach (var val in infosyn as Array)
                {
                    canContinue = generateScore(val.ToString(), ref p, ref n, ref  pos, ref  neg, ref  isNeg, ref isBorBN);
                    if (canContinue) { break; }
                }
                if (!isBorBN) { p = 0; n = 0; }

            }


            if ((pos - neg) > 1)
            {
                return "Positive";
            }
            else if ((pos - neg) <= 1 && (pos - neg) >= -1)
            {
                return "Neutral";
            }
            else { return "Negative"; }


        }

        public bool generateScore(string str, ref int p, ref  int n, ref  int pos, ref  int neg, ref  bool isNeg, ref bool isBorBN)
        {
            isBorBN = false;

            if (b.FirstOrDefault(i => string.Compare(str, i, true) == 0) != null) { p = 1; isBorBN = true; return true; };
            if (negb.FirstOrDefault(i => string.Compare(str, i, true) == 0) != null) { n = 1; isBorBN = true; return true; };

            if (good.FirstOrDefault(i => string.Compare(str, i, true) == 0) != null) { pos++; if (n == 0) { pos = pos + p; } pos = pos - n; isNeg = false; return true; };
            if (bad.FirstOrDefault(i => string.Compare(str, i, true) == 0) != null) { neg++; neg = neg + p; neg = neg + n; isNeg = true; return true; };
            return false;
        }

        public void setup()
        {
            good = loadfile(@"d:\app\positive-words.txt", ',');
            bad = loadfile(@"d:\app\negative-words.txt", ',');
            common = loadfile(@"d:\app\common-words.txt", ',');
            sentiment = loadfile(@"d:\app\sentiment-words.txt", '|');
            dictonary = sentiment.ToDictionary(i => i.Split(',')[0], i => int.Parse(i.Split(',')[1]));
        }

        public List<string> loadfile(string filePath, char split)
        {
            StreamReader streamReader = new StreamReader(filePath);
            string text = streamReader.ReadToEnd();

            streamReader.Close();
            return text.Split(split).ToList();
        }


    }


}
