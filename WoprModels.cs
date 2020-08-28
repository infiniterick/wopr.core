using System;

namespace Wopr.Core
{

    public interface IWoprMessage{
        string MessageType {get; set;}
        DateTime Timestamp {get; set;}
    }

    public interface IChannelFilterable {
        string ChannelId { get; }
    }

    public interface IMessageFilterable {
        string MessageId { get; }
    }

    public interface IAuthorFilterable {
        string AuthorId { get; }
    }

    //Named entities are things with a stable id, and unstable display name (Channels, Authors, etc)
    public class NamedEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public NamedEntity() { }
        public NamedEntity(ulong id, string name) : this(id.ToString(), name) { }
        public NamedEntity(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }

    public class Activity
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Details { get; set; }
    }

    public class Connected : IWoprMessage
    {
        public Connected() { MessageType = "Connected"; }
        public string MessageType { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class Disconnected : IWoprMessage
    {
        public Disconnected() { MessageType = "Disconnected"; }
        public string MessageType { get; set; }
        public DateTime Timestamp { get; set; }
        public string ExtraInfo { get; set; }
    }

    public class ContentReceived : IWoprMessage, IChannelFilterable, IAuthorFilterable
    {
        public ContentReceived() { MessageType = "ContentReceived"; }
        public string MessageType { get; set; }
        public DateTime Timestamp { get; set; }

        public string MessageId { get; set; }
        public string ChannelId {get {return this.Channel?.Id;}}
        public string AuthorId {get {return this.Author?.Id;}}
        public NamedEntity Channel { get; set; }
        public NamedEntity Author { get; set; }
        public string Content { get; set; }
        public string AttatchmentUri { get; set; }
    }

    public class ContentUpdated : IWoprMessage, IChannelFilterable, IAuthorFilterable
    {
        public ContentUpdated() { MessageType = "ContentUpdated"; }
        public string MessageType { get; set; }
        public DateTime Timestamp { get; set; }

        //original allways seems to match message in testing
        public string OriginalId { get; set; }
        public string MessageId { get; set; }
        public string ChannelId {get {return this.Channel?.Id;}}
        public string AuthorId {get {return this.Author?.Id;}}
        public NamedEntity Channel { get; set; }
        public NamedEntity Author { get; set; }
        public string Content { get; set; }
        public string AttatchmentUri { get; set; }
    }

    public class ContentDeleted: IWoprMessage, IChannelFilterable
    {
        public ContentDeleted() { MessageType = "ContentDeleted"; }
        public string MessageType { get; set; }
        public DateTime Timestamp { get; set; }
        public string OriginalId { get; set; }
        public string ChannelId {get {return this.Channel?.Id;}}
        public NamedEntity Channel { get; set; }
    }

    public class ReactionAdded: IWoprMessage, IChannelFilterable, IMessageFilterable
    {
        public ReactionAdded() { MessageType = "ReactionAdded"; }
        public string MessageType { get; set; }
        public DateTime Timestamp { get; set; }
        public NamedEntity Channel { get; set; }
        public string MessageId { get; set; }
        public string ChannelId {get {return this.Channel?.Id;}}
        public string Emote { get; set; }
    }

    public class ReactionRemoved: IWoprMessage, IChannelFilterable, IMessageFilterable
    {
        public ReactionRemoved() { MessageType = "ReactionRemoved"; }
        public string MessageType { get; set; }
        public DateTime Timestamp { get; set; }
        public NamedEntity Channel { get; set; }
        public string MessageId { get; set; }
        public string ChannelId {get {return this.Channel?.Id;}}
        public string Emote { get; set; }
    }

    public class UserUpdated: IWoprMessage, IAuthorFilterable
    {
        public UserUpdated() { MessageType = "UserUpdated"; }
        public string MessageType { get; set; }
        public DateTime Timestamp { get; set; }
        public NamedEntity Guild { get; set; }
        public NamedEntity User { get; set; }
        public string Status { get; set; }
        public Activity Activity { get; set; }

        public string AuthorId {get {return this.User?.Id;}}
    }

    public class ImageDownloaded: IWoprMessage, IChannelFilterable, IMessageFilterable
    {
        public ImageDownloaded() { MessageType = "ImageDownloaded"; }
        public string MessageType { get; set; }
        public DateTime Timestamp { get; set; }
        
        public string ChannelId {get; set;}
        public string MessageId {get; set;}
    }


    //Outgoing discord command messages

    public class AddContent: IWoprMessage
    {
        public AddContent() { MessageType = "AddContent"; }
        public string MessageType { get; set; }
        public DateTime Timestamp { get; set; }
        public string ChannelId { get; set; }
        public string Content { get; set; }
    }

    public class RemoveContent: IWoprMessage
    {
        public RemoveContent() { MessageType = "RemoveContent"; }
        public string MessageType { get; set; }
        public DateTime Timestamp { get; set; }
        public string ChannelId { get; set; }
        public string MessageId { get; set; }
    }

    public class AddReaction: IWoprMessage
    {
        public AddReaction() { MessageType = "AddReaction"; }
        public string MessageType { get; set; }
        public DateTime Timestamp { get; set; }
        public string ChannelId { get; set; }
        public string MessageId { get; set; }
        public string Emote { get; set; }
    }

    public class RemoveReaction
    {
        public RemoveReaction() { MessageType = "RemoveReaction"; }
        public string MessageType { get; set; }
        public DateTime Timestamp { get; set; }
        public string ChannelId { get; set; }
        public string MessageId { get; set; }
        public string Emote { get; set; }
    }

    public class RemoveAllReactions
    {
        public RemoveAllReactions() { MessageType = "RemoveAllReactions"; }
        public string MessageType { get; set; }
        public DateTime Timestamp { get; set; }
        public string ChannelId { get; set; }
        public string MessageId { get; set; }
    }
}