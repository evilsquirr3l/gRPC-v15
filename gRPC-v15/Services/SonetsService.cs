using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace gRPC_v15
{
    public class SonetsService : Sonets.SonetsBase
    {
        private string GetRandomSonetFromFile(string filename)
        {
            var lines = File.ReadLines(filename).ToList();
            var randomNumber = new Random().Next(lines.Count);

            return lines[randomNumber];
        }

        public override Task<Reply> GetSonets(Request request, ServerCallContext context)
        {
            var randomSonet = GetRandomSonetFromFile("./sonets.txt");
            return Task.FromResult(new Reply
            {
                ReplyMessage = "Вы взяли сонет Шекспира: " + randomSonet
            });
        }
    }
}