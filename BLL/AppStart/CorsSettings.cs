using System.Collections.Generic;

namespace BLL.AppStart
{
    public class CorsSettings
    {
        public IEnumerable<string> Origins { get; } = new List<string>();
        public IEnumerable<string> Headers { get; } = new List<string>();
        public IEnumerable<string> Methods { get; } = new List<string>();
    }
}
