using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace chensFyp.Models
{  //transfer data between view and controller
    public class FeedbackViewModel
    {
        public string Comment { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public int? Select { get; set; }
        public List<FeedbackAnswer> Answers { get; set; }
    }
}