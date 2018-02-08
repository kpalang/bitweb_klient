using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace BitWebKlient {
	class Program {
		private static readonly HttpClient client = new HttpClient();
		private static Dictionary<string, string> values = new Dictionary<string, string>();
		private static int kordused = 3;

		static void Main(string[] args) {		
			
			string result = Calculate().Result;			
			Console.WriteLine(result);

			Console.WriteLine("Vajutage suvalist klahvi, et aken sulgeda...");
			Console.ReadKey();
		}

		private static async Task<string> Calculate() {
			int num1 = 0;
			int num2 = 0;
			string op = String.Empty;

			string value;

			while (kordused-- > 0) {
				Console.WriteLine("Sisesta num1");
				value = Console.ReadLine();
				if (int.TryParse(value, out num1)) {
					Console.WriteLine("Num1 = " + num1);
					break;
				}
				else {
					Console.WriteLine("Väärtus " + value + " ei sobi numbriks!");
				}
			}

			kordused = 3;
			while (kordused-- > 0) {
				Console.WriteLine("Sisesta num2");
				value = Console.ReadLine();
				if (int.TryParse(value, out num2)) {
					Console.WriteLine("Num2 = " + num2);
					break;
				}
				else {
					Console.WriteLine("Väärtus " + value + " ei sobi numbriks!");
				}
			}

			kordused = 3;
			while (kordused-- > 0) {
				Console.WriteLine("Sisesta operaator (sum / sub / prod / div)");
				value = Console.ReadLine();
				if (value.Equals("sum")
					|| value.Equals("sub")
					|| value.Equals("prod")
					|| value.Equals("div")) {
					op = value;
					break;
				}
				else {
					Console.WriteLine("Sellist operaatorit ei ole");
				}
			}

			values.Add("num1", num1.ToString());
			values.Add("num2", num2.ToString());
			values.Add("op", op);

			string json = JsonConvert.SerializeObject(values);
			var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
			var response = await client.PostAsync("http://localhost:8080/calculate", content);
			var responseString = await response.Content.ReadAsStringAsync();

			return responseString;
		}
	}
}
