using DataFetchingAndSqLiteManupilation.HelperMethods;
using DataFetchingAndSqLiteManupilation.Models;

AppSettings dataFromJsonFile = (System.Text.Json.JsonSerializer.Deserialize<AppSettings>(File.ReadAllText("../../../appSettings.json")));

new ApiRequest().GetToken(dataFromJsonFile);



