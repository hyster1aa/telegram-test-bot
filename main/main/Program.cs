using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.ReplyMarkups;
using VacansySpace;
using System.Net;
using HHmodel;

namespace telegrambot
{
    //https://dev.to/osempu/build-a-web-api-with-c-asp-net-core-70-jp
    class Program
    {
        private static string token = "5771619188:AAFazCfqGR_VbvCx6ODSAER0DGXKJz15cDc";
        static ITelegramBotClient bot = new TelegramBotClient(token);

        public static void GetVacansy(string url)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.0.0 Safari/537.36 OPR/94.0.0.0");
                var endpoint = new Uri(url);
                var result = client.GetAsync(endpoint).Result;
                var jsn = result.Content.ReadAsStringAsync().Result;
            }
        }
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;
            Console.WriteLine($"{message.Chat.FirstName}    |   {message.Text}");
            if (message != null)
            {   
                if (message.Text.ToLower().Contains("привет"))
                {
                    await botClient.SendTextMessageAsync(message.Chat, $"Bye");
                    
                }
                GetVacansy($"https://api.hh.ru/vacancies?text={message.Text}");
                await botClient.SendTextMessageAsync(message.Chat, $"You said:\n{message.Text}");
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