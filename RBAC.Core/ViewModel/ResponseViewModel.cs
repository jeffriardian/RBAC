using System.Diagnostics.CodeAnalysis;

namespace RBAC.Core.ViewModel
{
    [ExcludeFromCodeCoverage]
    public partial class ResponseViewModel<T>
    {
        public int StatusCode { set; get; }
        public string Message { set; get; }
        public List<T> Data { set; get; }
        public MetaViewModel Meta { get; set; }

        public static implicit operator ResponseViewModel<T>(List<ExampleViewModel> v)
        {
            throw new NotImplementedException();
        }
    }

    [ExcludeFromCodeCoverage]
    public partial class MetaViewModel
    {
        public int TotalPages { get; set; }
        public int TotalItem { get; set; }
    }
}
