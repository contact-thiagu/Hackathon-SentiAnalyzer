using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessObjects;
using BusinessLayer;

namespace SAnalayser
{
    enum Previous
    { 
        GOOD,BAD,B,BNG,C,NONE
    }
    
    class SentimentAnalyser
    {

        public Microsoft.Office.Interop.Word.Application app;
        List<string> sentiment;
        public List<string> good;
        public List<string> bad;
        public List<string> common;
        List<string> b = new List<string> {"exact","very","exactly","biggest","crazy","absolutely","certainly","complete","completely","definitely","especially","extremely","fuckin","fucking","hugely","incredibly","overwhelmingly","really","totally","very"};
        List<string> negb = new List<string> { "don", "doesn't", "don't", "doesn", "didnot", "didnt", "didn't", "couldnt", "couldnot", "couldn't", "aren't", "arent", "arenot", "can't", "cannot", "cant", "couldn't", "couldnt", "don't", "dont", "isn't", "isnt", "never", "won't", "wont", "wouldn't", "wouldnt" };
        public SentimentObj obj;
        Dictionary<string, int> dictonary = new Dictionary<string, int>();
        
        public string process(string sentence)
        {
            obj = new SentimentObj();
           
            //bad.AddRange(negb);
            //negb.Clear();
            //string sentence = "I hate my country";
            //var matches = Regex.Matches(sentence, @"((\b[^\s]+\b)((?<=\.\w).)?)");
            sentence = Regex.Replace(sentence, "[^a-zA-Z0-9_]+", " ", RegexOptions.Compiled);
            List<string> s = sentence.Split(new char[] { ' ', '\t' },StringSplitOptions.RemoveEmptyEntries).ToList();
            List<int> senti = s.Where(i => dictonary.ContainsKey(i)).Select(i => dictonary[i]).ToList();
            // MessageBox.Show(((float)senti.Sum() / Math.Sqrt(senti.Count())).ToString());

            
            //s = s.Where(a => !common.Contains(a.ToLower())).ToList();
         
            int pos = 0;
            int neg = 0;
           
            Previous previousValue = Previous.NONE; 
            foreach (string strW in s)
            {

                if (common.Any(a => string.Compare(strW, a, true)==0))
                {
                    previousValue = Previous.C;
                    continue;
                }

                
               
                bool canContinue = generateScore(strW, ref  pos, ref  neg, ref previousValue);
                if (canContinue) { continue; }
                object lang = WdLanguageID.wdEnglishUS;

                var infosyn = app.get_SynonymInfo(strW, ref lang).MeaningList;
                Array iArr = infosyn as Array;
                bool none = true;
                foreach (var val in iArr)
                {
                
                    canContinue = generateScore(val.ToString(), ref  pos, ref  neg, ref previousValue);
                    if (canContinue) { none = false; break; }
                }

                if (none) { previousValue = Previous.NONE; }
                
            }

            string status = "";

            if ((pos - neg) > 1)
            {
                status= "Positive";
            }
            else if ((pos - neg) == 1 || (pos - neg) == 0)
            {
                status= "Neutral";
            }
            else { status= "Negative"; }

            
            obj.SentimentDesc = sentence;
            obj.PosScore = pos;
            obj.NegScore = neg;
            obj.SentimentScore = pos - neg;
            obj.SentimentStatus = status;

            return status;           
            
            
        }

        public bool generateScore(string str, ref  int pos, ref  int neg,ref Previous previousValue)
        {
           

            if (b.FirstOrDefault(i => string.Compare(str, i, true) == 0) != null) 
            {
                if (previousValue == Previous.BAD || previousValue == Previous.BNG)
                {
                    neg++;
                    previousValue = Previous.BNG;
                }
                else
                {
                    previousValue = Previous.B;
                }
                return true; 
            }


            if (negb.FirstOrDefault(i => string.Compare(str, i, true) == 0) != null) 
            { 
                previousValue = Previous.BNG;
                neg++;
                return true; 
            }

            if (good.FirstOrDefault(i => string.Compare(str, i, true) == 0) != null) 
            {
                if (previousValue == Previous.B) { pos++; }
                else if (previousValue == Previous.BAD || previousValue == Previous.BNG) { neg++; }
                else { pos++; }

                previousValue = Previous.GOOD; 
                return true; 
            }

            if (bad.FirstOrDefault(i => string.Compare(str, i, true) == 0) != null) 
            {
                if (previousValue == Previous.B || previousValue == Previous.BNG) { neg++; }
                else if (previousValue == Previous.GOOD) { neg++; pos--; }
                previousValue = Previous.BAD; 
                neg++; 
                
                return true; 
            }

            
            return false;
        }

        public void setup()
        {
             good = loadfile(@"d:\app\positive-words.txt",',');
             bad = loadfile(@"d:\app\negative-words.txt", ',');
             common = loadfile(@"d:\app\common-words.txt", ',');
             sentiment = loadfile(@"d:\app\sentiment-words.txt", '|');
             dictonary= sentiment.ToDictionary(i => i.Split(',')[0], i => int.Parse(i.Split(',')[1]));
        }

        public List<string> loadfile(string filePath,char split)
        {
            StreamReader streamReader = new StreamReader(filePath);
            string text = streamReader.ReadToEnd();
            
            streamReader.Close();
            return text.Split(split).ToList(); 
        }

        
    }

    
}

