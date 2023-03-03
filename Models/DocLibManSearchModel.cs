namespace DocLibMan.Models
{
    public class DocLibManSearchModel
    {
        public string SearchText { get; set; }

        public IList<DocLibManDocument> SearchResults { get; set; }
    }
}
