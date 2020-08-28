namespace Wopr.Core
{
    //should upgrade this to load dynamically from a key in redis with a single config input of the path key to use
    //also need to make it clear which are events and which are data
    public class RedisPaths
    {
        public const string DiscordBackup = "wopr:discord:backup";

        public const string ImageDLMessageWatch = "wopr:imgdl:watch:msg";
        public const string ImageDLChannelWatch = "wopr:imgdl:watch:channel";
        public const string ImageDLAuthorWatch = "wopr:imgdl:watch:author";

        public const string ModelServerInfo = "wopr:discord:model:serverinfo";
        public const string ModelChannel = "wopr:discord:model:channel";
        public const string ModelReaction = "wopr:discord:model:reaction";
        public const string ModelContent = "wopr:discord:model:content";
        public const string ModelUser = "wopr:discord:model:user";
        public const string ModelUserStatus = "wopr:discord:model:userstatus";

        public const string WatchedContent = "wopr:discord:model:watch";
        public const string ImageReady = "wopr:discord:model:imageready";
    }
}