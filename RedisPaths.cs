namespace Wopr.Core
{
    //should upgrade this to load dynamically from a key in redis with a single config input of the path key to use
    //also need to make it clear which are events and which are data
    public class RedisPaths
    {
        public const string ControlReady = "wopr:discord:control:ready";
        public const string ControlFresh = "wopr:discord:control:fresh";
        public const string ControlProcessed = "wopr:discord:control:processed";

        public const string DataReady = "wopr:discord:data:ready";
        public const string DataFresh = "wopr:discord:data:fresh";
        public const string DataProcessed = "wopr:discord:data:processed";
        public const string DataProcessing = "wopr:discord:data:processing";
        public const string DataDead = "wopr:discord:data:dead";
        public const string DataArchive = "wopr:discord:data:archive";


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