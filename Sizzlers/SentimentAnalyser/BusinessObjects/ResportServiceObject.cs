using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace BusinessObjects
{
    [DataContract]
    public class ResportServiceObject
    {
        private string sentiment_status = "";
        private int iSentiment_count = 0;
        [DataMember]
        public int SentimentCount
        {
            get { return iSentiment_count; }
            set { iSentiment_count = value; }
        }
        [DataMember]
        public string SentimentStatus
        {
            get { return sentiment_status; }
            set { sentiment_status = value; }
        }

    }
    
    
}
