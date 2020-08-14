using System;
using System.IO;

namespace Wopr.Core {
    public class Secrets {
        public string RedisToken { get; set; }
        public string StackToken { get; set; }
        public string DiscordToken { get; set; }


        public static Secrets Load(string secretsDir){
            var secrets = new Secrets();
            var redisTokenPath = Path.Combine(secretsDir, "RedisToken");
            var stackTokenPath = Path.Combine(secretsDir, "StackToken");
            var discordTokenPath = Path.Combine(secretsDir, "DiscordToken");

            if(File.Exists(redisTokenPath))
                secrets.RedisToken = File.ReadAllText(redisTokenPath).Replace(Environment.NewLine, "");
            else
                secrets.RedisToken = Environment.GetEnvironmentVariable("RedisToken");

            if(File.Exists(stackTokenPath))
                secrets.StackToken = File.ReadAllText(stackTokenPath).Replace(Environment.NewLine, "");
            else
                secrets.StackToken = Environment.GetEnvironmentVariable("StackToken");

            if(File.Exists(discordTokenPath))
                secrets.DiscordToken = File.ReadAllText(discordTokenPath).Replace(Environment.NewLine, "");
            else
                secrets.DiscordToken = Environment.GetEnvironmentVariable("DiscordToken");

            return secrets;
        }
    }
}