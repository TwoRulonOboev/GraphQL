using System.Text.Json;
using System.Text.RegularExpressions;

namespace GraphQL.Model.ExtendObjects
{
    [ExtendObjectType(typeof(Node))]
    public class NodeExtensions
    {
        private readonly IDataLoader _dataLoader;
        public NodeExtensions(IDataLoader dataLoader)
        {
            _dataLoader = dataLoader;
        }

        [BindMember(nameof(Node.Data))]
        public async Task<JsonElement> GetData([Parent] Node node)
        {
            Regex regex = new Regex("^http[s]?://.+[.].+", RegexOptions.IgnoreCase);

            if (!regex.IsMatch(node.Data)) return JsonDocument.Parse($"\"{node.Data}\"").RootElement;

            string json = await _dataLoader.GetStringAsync(node.Data);

            return JsonDocument.Parse(json).RootElement;
        }
    }
}
