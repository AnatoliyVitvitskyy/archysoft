namespace Archysoft.D1.Model.Common
{
    public class BaseFilter
    {
        public string Search { get; set; }
        public string OrderBy { get; set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }

        public string GetTrimSearch()
        {
            return Search.Trim();
        }
    }
}
