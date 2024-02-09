using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.ReplyMarkups;
using VacancySpace;
using System.Net;
using HHmodel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection.Metadata.Ecma335;
//попытка написать бота была на 3ем курсе
namespace telegrambot
{
    //https://dev.to/osempu/build-a-web-api-with-c-asp-net-core-70-jp
    class Program
    {
        static VacanciesList somelist = new VacanciesList();
        private static string token = "5771619188:AAFazCfqGR_VbvCx6ODSAER0DGXKJz15cDc";
        static ITelegramBotClient bot = new TelegramBotClient(token);
        public static int counter = 1;
        public static VacanciesList GetVacansy(string url)
        {
            VacanciesList VacancyLst = new VacanciesList();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.0.0 Safari/537.36 OPR/94.0.0.0");
                var endpoint = new Uri(url);
                var result = client.GetAsync(endpoint).Result;
                var jsn = result.Content.ReadAsStringAsync().Result;
               
                try
                {
                    VacancyLst = JsonConvert.DeserializeObject<VacanciesList>(jsn);
                    Console.WriteLine(VacancyLst.list.Count);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return VacancyLst;
        }
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;
            Console.WriteLine($"{message.Chat.FirstName}    |   {message.Text}");
            if (message != null)
            {
                VacanciesList somelist = GetVacansy($"https://api.hh.ru/vacancies?text={message.Text}");
                await botClient.SendTextMessageAsync(message.Chat, $"You said:\n{message.Text}");
                try
                {
                    if (counter==0)
                    {
                        await botClient.SendTextMessageAsync(message.Chat, somelist.list[counter].name + " " + somelist.list[counter].url + " " + somelist.list[counter].area.name);
                    }
                    if(counter!=0 && message.Text.ToLower().Contains("дальше") || message.Text.ToLower().Contains("next"))
                    {
                        await botClient.SendTextMessageAsync(message.Chat, somelist.list[counter].name + " " + somelist.list[counter].url + " " + somelist.list[counter].area.name);
                    }
                    
                    counter++;
                }
                catch
                {
                    await Console.Out.WriteLineAsync("Неудача");
                }
            }
        }
        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.ReadLine();
        }
    }
}