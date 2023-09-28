using HydrusSharp.Core.Enums;

namespace HydrusSharp.Data.Models.ViewModels
{
    public class SearchPredicateViewModel
    {
        public PredicateType SearchType { get; set; }

        public object[] SearchData { get; set; }

        public bool MustBeTrue { get; set; }
    }
}