using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace chensFyp.Models
{
    public class Reward
    {
        [Required]
        [Key]
        public int RewardId { set; get; }

        [Required]
        [Display(Name="Name")]
        public string RewardName { set; get; }
        //public string quantity { set; get; }
        [Required]
        [Display(Name = "Point")]
        public int point { set; get; }
        
        


    }
}