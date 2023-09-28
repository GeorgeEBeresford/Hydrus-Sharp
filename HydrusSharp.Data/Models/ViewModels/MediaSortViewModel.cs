using HydrusSharp.Core.Enums;

namespace HydrusSharp.Data.Models.ViewModels
{
    public class MediaSortViewModel
    {
        public string[] Namespaces { get; set; }
        public SortFilesBy? SystemSortType { get; set; }
    }
}