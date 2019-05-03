using System.Collections.Generic;

namespace ghumpo.model.Mobile
{
    public class FormatListApi<T>
    {
        public string message { get; set; }
        public IList<T> data { get; set; }
    }

    public class FormatApi<T>
    {
        public string message { get; set; }
        public T data { get; set; }
    }
}