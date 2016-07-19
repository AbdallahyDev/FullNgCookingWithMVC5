using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FullNgCookingWithMVC5.ViewModels.Comments
{
    public class CommentViewModel
    {
        public int Mark { get; set; }
        public string Title { get; set; }    
        public string CommentBody { get; set; } 
        public int RecetteId { get; set; }  
    }
}