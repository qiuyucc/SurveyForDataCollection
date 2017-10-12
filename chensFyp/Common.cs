using chensFyp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//answer
namespace chensFyp
{  //initial the answer 
    public class Common
    {
        
        public static List<FeedbackAnswer> GetAnswers()
        {
            List<FeedbackAnswer> list = new List<FeedbackAnswer>();
            list.Add(new FeedbackAnswer() { ID = 1, Name = "Excellent", Css = "check w3" });
            list.Add(new FeedbackAnswer() { ID = 2, Name = "Good", Css = "check w3ls" });
            list.Add(new FeedbackAnswer() { ID = 3, Name = "Neutral", Css = "check wthree" });
            list.Add(new FeedbackAnswer() { ID = 4, Name = "Poor", Css = "check w3_agileits" });
            return list;
        }
    }
}