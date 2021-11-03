using System;
using System.Net.Http;
using System.Threading.Tasks;
using gRPC_v15;
using Grpc.Net.Client;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:5000", new GrpcChannelOptions()
            {
                HttpHandler = GetHttpClientHandler()
            });
            
            // создаем клиента
            var client = new Sonets.SonetsClient(channel);
            while (true)
            {
                Console.Write("Нажмите что-нибудь, чтобы ознакомиться с сонетами Шекспира: ");
                var message = Console.ReadLine();
            
                // обмениваемся сообщениями с сервером
                var reply = await client.GetSonetsAsync(new Request {RequestMessage = message});
                Console.WriteLine(reply.ReplyMessage);
            }
        }

        private static HttpClientHandler GetHttpClientHandler()
        {
            var httpHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            return httpHandler;
        }
    }
}