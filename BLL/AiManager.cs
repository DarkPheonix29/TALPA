using System.Text.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Text;
using BLL.Models;

namespace TALPA_ai_test
{
    public class AiManager
    { 
        public string Url { get; private set; }
        public string Key { get; private set; }
        public List<object> Content { get; private set; }

        public AiManager()
        {
            Url = "https://nietvanrens.pythonanywhere.com/ai";
            Key = "API_KEY";
            Content = new List<object>();
        }

        public void AddModelContent(string content)
        {
            Content.Add(new Dictionary<string, object>
            {
                {"role", "model"},
                {"parts", new List<object>
                    {
                        new Dictionary<string, string> {{"text", content}}
                    }
                }
            });
        }

        public void AddUserContent(string content)
        {
            Content.Add(new Dictionary<string, object>
            {
                {"role", "user"},
                {"parts", new List<object>
                    {
                        new Dictionary<string, string> {{"text", content}}
                    }
                }
            });
        }

        public async Task<string> MakeRequest()
        {
            var data = new Dictionary<string, object>
            {
                {"key", Key},
                {
                    "data", new Dictionary<string, object>
                    {
                        {"contents", Content}
                    }
                }
            };

            string jsonString = System.Text.Json.JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Url);
            request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.SendAsync(request);
            string responseBody = await response.Content.ReadAsStringAsync();
            if (responseBody.Contains("text"))
            {
                var responseBodyObject = (JObject)JsonConvert.DeserializeObject(responseBody);
                string text = responseBodyObject["candidates"][0]["content"]["parts"][0]["text"].Value<string>();
                AddModelContent(text);

				return text;
            }
            else
            {
				return "Error";
            }
		}

		public async Task<List<int>> GetSimilars(List<Suggestion> suggestions, Suggestion suggestion)
		{
			AddUserContent($"Can you compare a new suggestion with an existing list of suggestions and return a list of ids in the format 'ids: 1, 2, 3' of suggestions from the list that are similar or might be supposed to be the same?\nList of suggestions: {JsonConvert.SerializeObject(suggestions, Formatting.Indented)}\nNew suggestion: {JsonConvert.SerializeObject(suggestion, Formatting.Indented)}");
			string result = await MakeRequest();
			try
			{
				List<string> matches = result.Split("ids:")[1].Split(",").ToList();
				matches = matches.Select(item => { MatchCollection matches = Regex.Matches(item, @"\d+"); return string.Join("", matches.Cast<Match>().Select(m => m.Value)); }).ToList();
				List<int> similars = matches.SelectMany(item => Regex.Matches(item, @"\d+").Cast<Match>().Select(m => int.Parse(m.Value))).ToList();
				return similars;
			}
			catch
			{
				return new List<int>();
			}
		}
	}
}
