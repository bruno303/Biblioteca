using Newtonsoft.Json;

namespace Biblioteca.Utils
{
    public class JsonUtils<TTipo>
    {
        public string ObjectToJson(TTipo objeto)
        {
            return JsonConvert.SerializeObject(objeto);
        }

        public TTipo JsonToObject(string json)
        {
            return JsonConvert.DeserializeObject<TTipo>(json);
        }
    }
}
