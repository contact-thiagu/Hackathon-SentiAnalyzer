using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects
{
   public class SentimentObj
    {
        string sentimentDesc;
        string sentimentStatus;
        int posScore;
        int negScore;
        int sentimentScore;

        public string SentimentDesc
        {
            get
            {
                return sentimentDesc;
            }

            set
            {
                sentimentDesc = value;
            }
        }

        public string SentimentStatus
        {
            get
            {
                return sentimentStatus;
            }

            set
            {
                sentimentStatus = value;
            }
        }

        public int PosScore
        {
            get
            {
                return posScore;
            }

            set
            {
                posScore = value;
            }
        }

        public int NegScore
        {
            get
            {
                return negScore;
            }

            set
            {
                negScore = value;
            }
        }

        public int SentimentScore
        {
            get
            {
                return sentimentScore;
            }

            set
            {
                sentimentScore = value;
            }
        }
    }
}
