namespace Model.Comments
{
    public class Comment
    {
        public int Id { get; set; }
        //[Required(ErrorMessage = "{0} est obligatoire.")]
        //[StringLength(50, MinimumLength = 5, ErrorMessage = "{0} doit être compris entre {2} et {1} characteres.")]
        public string Title { get; set;}
        public int Mark { get; set; }
        public string CommentBody { get; set;}   
        public int  RecetteId { get; set;}   
        public string UserId { get; set; }        
    }
}