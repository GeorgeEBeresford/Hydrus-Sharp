using HydrusSharp.Enums;

namespace HydrusSharp.Models.ViewModel
{
    public class SearchPredicateViewModel
    {
        public PredicateType SearchType { get; set; }

        public object[] SearchData { get; set; }

        public bool MustBeTrue { get; set; }
    }
}