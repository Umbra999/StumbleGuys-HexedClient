using Hexed.Wrappers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hexed.HexedServer
{
    internal class ServerHandler
    {
        private static string Key = "";
        private static string HWID = "";

        public static async Task Init()
        {
            Logger.Log("Authenticating...");

            if (!Directory.Exists("Hexed")) Directory.CreateDirectory("Hexed");
            if (!File.Exists("Hexed\\Key.Hexed"))
            {
                Logger.LogWarning("Enter Key:");
                string NewKey = Console.ReadLine();
                File.WriteAllText("Hexed\\Key.Hexed", Encryption.ToBase64(NewKey));
            }

            Key = Encryption.FromBase64(File.ReadAllText("Hexed\\Key.Hexed"));
            HWID = Encryption.GetHWID();

            if (!await IsValidKey())
            {
                Logger.LogError("Key is not Valid");
                await Task.Delay(3000);
                Environment.Exit(0);
            }
        }

        private static async Task<string> FetchTime()
        {
            HttpClient Client = new HttpClient(new HttpClientHandler { UseCookies = false });
            Client.DefaultRequestHeaders.Add("User-Agent", "Hexed");

            HttpRequestMessage Payload = new HttpRequestMessage(HttpMethod.Get, Encryption.FromBase64("aHR0cDovLzYyLjY4Ljc1LjUyOjk5OS9TZXJ2ZXIvVGltZQ=="));
            HttpResponseMessage Response = await Client.SendAsync(Payload);
            if (Response.IsSuccessStatusCode) return await Response.Content.ReadAsStringAsync();
            return null;
        }

        private static async Task<bool> IsValidKey()
        {
            string Timestamp = await FetchTime();

            HttpClient Client = new HttpClient(new HttpClientHandler { UseCookies = false });
            Client.DefaultRequestHeaders.Add("User-Agent", "Hexed");

            HttpRequestMessage Payload = new HttpRequestMessage(HttpMethod.Post, Encryption.FromBase64("aHR0cDovLzYyLjY4Ljc1LjUyOjk5OS9TZXJ2ZXIvSXNWYWxpZA=="))
            {
                Content = new StringContent(JsonConvert.SerializeObject(new { Auth = Encryption.EncryptAuthKey(Key, Timestamp, "XD6V", HWID) }), Encoding.UTF8, "application/json")
            };

            HttpResponseMessage Response = await Client.SendAsync(Payload);

            if (Response.IsSuccessStatusCode)
            {
                return Convert.ToBoolean(await Response.Content.ReadAsStringAsync());
            }
            return false;
        }
    }
}
