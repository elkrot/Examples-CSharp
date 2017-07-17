using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        Get();

        Console.WriteLine("I am a free thread!");
        Console.ReadLine();
    }

    static async void Get()
    {
        HttpClient client = new HttpClient();

        HttpResponseMessage response = await 
            client.GetAsync("http://mohamadpc/SimpleApp/default.aspx");

        response.EnsureSuccessStatusCode();

        Console.WriteLine(await response.Content.ReadAsStringAsync());
    }

}

