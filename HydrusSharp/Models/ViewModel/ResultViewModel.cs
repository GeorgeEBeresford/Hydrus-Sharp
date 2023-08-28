namespace HydrusSharp.Models.ViewModel
{
    public class ResultViewModel
    {
        public object Value { get; set; }
        public string Error { get; set; }

        public static ResultViewModel Success(object value)
        {
            return new ResultViewModel { Value = value };
        }

        public static ResultViewModel Failure(string error)
        {
            return new ResultViewModel { Error = error };
        }
    }
}