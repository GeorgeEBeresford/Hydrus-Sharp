namespace HydrusSharp.Data.Models.ViewModels
{
    public class TagViewModel
    {
        public int TagId { get; set; }
        public int NamespaceId { get; set; }
        public string Namespace { get;set; }
        public int SubTagId { get; set; }
        public string SubTag { get; set; }
    }
}