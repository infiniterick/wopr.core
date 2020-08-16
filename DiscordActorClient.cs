using System;
using System.Threading.Tasks;
using System.Text.Json;
using ServiceStack.Redis;

namespace Wopr.Core{

    public interface IDiscordActorClient { 
        Task SendMessage<T>(T message);
        Task AddContent(string channelId, string content);
        Task AddReaction(string channelId, string messageId, string emote);
        Task RemoveContent(string channelId, string messageId);
        Task RemoveReaction(string channelId, string messageId, string emote);
        Task RemoveAllReactions(string channelId, string messageId);
        
    }

    public class DiscordActorClient : IDiscordActorClient {
        
        private readonly IRedisClientsManager redis;

        public DiscordActorClient(IRedisClientsManager redis){
            this.redis = redis;
        }

        public Task SendMessage<T>(T message){
            try{
                var raw = JsonSerializer.Serialize(message);
                Console.WriteLine("WOPR: " + raw);

                using(var client = this.redis.GetClient()){
                    using(var pipeline = client.CreatePipeline()){
                        pipeline.QueueCommand(c => c.EnqueueItemOnList(RedisPaths.ControlFresh, raw));
                        pipeline.QueueCommand(c => c.PublishMessage(RedisPaths.ControlReady, "ready"));
                        pipeline.Flush();
                    }
                }
            }
            catch(Exception ex){
                Console.WriteLine("Exception sending message: " + ex.Message);
                throw;
            }

            return Task.CompletedTask;
        }

        public Task AddContent(string channelId, string content){
            var msg = new AddContent(){
                Timestamp = DateTime.UtcNow,
                ChannelId = channelId,
                Content = content
            };

            return SendMessage(msg);
        }

        public Task AddReaction(string channelId, string messageId, string emote){
            var msg = new AddReaction(){
                Timestamp = DateTime.UtcNow,
                ChannelId = channelId,
                MessageId = messageId,
                Emote = emote
            };

            return SendMessage(msg);
        }

        public Task RemoveContent(string channelId, string messageId){
            var msg = new RemoveContent(){
                Timestamp = DateTime.UtcNow,
                ChannelId = channelId,
                MessageId = messageId
            };

            return SendMessage(msg);
        }

        public Task RemoveReaction(string channelId, string messageId, string emote){
            var msg = new RemoveReaction(){
                Timestamp = DateTime.UtcNow,
                ChannelId = channelId,
                MessageId = messageId,
                Emote = emote
            };

            return SendMessage(msg);
        }

        public Task RemoveAllReactions(string channelId, string messageId){
            var msg = new RemoveAllReactions(){
                Timestamp = DateTime.UtcNow,
                ChannelId = channelId,
                MessageId = messageId
            };

            return SendMessage(msg);
        }

   }
}