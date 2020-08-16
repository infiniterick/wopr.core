using System;
using System.Threading.Tasks;
using System.Text.Json;
using ServiceStack.Redis;

namespace Wopr.Core{

    public interface IDiscordEventClient { 
        Task SendMessage<T>(T message);
        Task OnConnected();
        Task OnDisconnected(Exception ex);
        Task OnContentReceived(string messageId, NamedEntity channel, NamedEntity author, string content, string attachmentUri);
        Task OnContentUpdated(string originalId, string messageId, NamedEntity channel, NamedEntity author, string content, string attachmentUri);
        Task OnContentDeleted(string originalId, NamedEntity channel);
        Task OnUserUpdated(NamedEntity guild, NamedEntity user, string status, Activity activity);
        Task OnReactionAdded(NamedEntity channel, string messageId, string emote);
        Task OnReactionRemoved(NamedEntity channel, string messageId, string emote);
        
    }

    public class DiscordEventClient : IDiscordEventClient {
        
        private readonly IRedisClientsManager redis;

        public DiscordEventClient(IRedisClientsManager redis){
            this.redis = redis;
        }

        public Task SendMessage<T>(T message){
            try{
                var raw = JsonSerializer.Serialize(message);
                Console.WriteLine("WOPR: " + raw);

                using(var client = this.redis.GetClient()){
                    using(var pipeline = client.CreatePipeline()){
                        pipeline.QueueCommand(c => c.EnqueueItemOnList(RedisPaths.DataFresh, raw));
                        pipeline.QueueCommand(c => c.PublishMessage(RedisPaths.DataReady, "ready"));
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

        public Task OnConnected(){
            var msg = new Connected(){
                Timestamp = DateTime.UtcNow
            };

            return SendMessage(msg);
        }

        public Task OnDisconnected(Exception ex){
            var msg = new Disconnected(){
                Timestamp = DateTime.UtcNow,
                ExtraInfo = ex.Message
            };

            return SendMessage(msg);
        }

        public Task OnContentReceived(string messageId, NamedEntity channel, NamedEntity author, string content, string attachmentUri){
            var msg = new ContentReceived(){
                Timestamp = DateTime.UtcNow,
                MessageId = messageId,
                Channel = channel,
                Author = author,
                Content = content,
                AttatchmentUri = attachmentUri
            };

            return SendMessage(msg);
        }

        public Task OnContentUpdated(string originalId, string messageId, NamedEntity channel, NamedEntity author, string content, string attachmentUri){
            var msg = new ContentUpdated(){
                OriginalId = originalId,
                MessageId = messageId,
                Timestamp = DateTime.UtcNow,
                Channel = channel,
                Author = author,
                Content = content,
                AttatchmentUri = attachmentUri
            };

            return SendMessage(msg);
        }

        public Task OnContentDeleted(string originalId, NamedEntity channel){
            var msg = new ContentDeleted(){
                OriginalId = originalId,
                Timestamp = DateTime.UtcNow,
                Channel = channel
            };

            return SendMessage(msg);
        }

        public Task OnUserUpdated(NamedEntity guild, NamedEntity user, string status, Activity activity){
            var msg = new UserUpdated(){
                Timestamp = DateTime.UtcNow,
                Guild = guild,
                User = user,
                Status = status,
                Activity = activity
            };

            return SendMessage(msg);
        }

        public Task OnReactionAdded(NamedEntity channel, string messageId, string emote){
            var msg = new ReactionAdded(){
                Timestamp = DateTime.UtcNow,
                Channel = channel,
                MessageId = messageId,
                Emote = emote
            };

            return SendMessage(msg);
        }

        public Task OnReactionRemoved(NamedEntity channel, string messageId, string emote){
            var msg = new ReactionRemoved(){
                Timestamp = DateTime.UtcNow,
                Channel = channel,
                MessageId = messageId,
                Emote = emote
            };

            return SendMessage(msg);
        }
    }
}