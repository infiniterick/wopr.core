using System;

namespace Wopr.Core
{
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

    public class Connected
    {
        public Connected() { MessageType = "Connected"; }
        public string MessageType { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class Disconnected
    {
        public Disconnected() { MessageType = "Disconnected"; }
        public string MessageType { get; set; }
        public DateTime Timestamp { get; set; }
        public string ExtraInfo { get; set; }
    }

    public class ContentReceived
    {
        public ContentReceived() { MessageType = "ContentReceived"; }
        public string MessageType { get; set; }
        public DateTime Timestamp { get; set; }

        public string MessageId { get; set; }
        public NamedEntity Channel { get; set; }
        public NamedEntity Author { get; set; }
        public string Content { get; set; }
        public string AttatchmentUri { get; set; }
    }

    public class ContentUpdated
    {
        public ContentUpdated() { MessageType = "ContentUpdated"; }
        public string MessageType { get; set; }
        public DateTime Timestamp { get; set; }

        //original allways seems to match message in testing
        public string OriginalId { get; set; }
        public string MessageId { get; set; }
        public NamedEntity Channel { get; set; }
        public NamedEntity Author { get; set; }
        public string Content { get; set; }
        public string AttatchmentUri { get; set; }
    }

    public class ContentDeleted
    {
        public ContentDeleted() { MessageType = "ContentDeleted"; }
        public string MessageType { get; set; }
        public DateTime Timestamp { get; set; }
        public string OriginalId { get; set; }
        public NamedEntity Channel { get; set; }
    }

    public class ReactionAdded
    {
        public ReactionAdded() { MessageType = "ReactionAdded"; }
        public string MessageType { get; set; }
        public DateTime Timestamp { get; set; }
        public NamedEntity Channel { get; set; }
        public string MessageId { get; set; }
        public string Emote { get; set; }
    }

    public class ReactionRemoved
    {
        public ReactionRemoved() { MessageType = "ReactionRemoved"; }
        public string MessageType { get; set; }
        public DateTime Timestamp { get; set; }
        public NamedEntity Channel { get; set; }
        public string MessageId { get; set; }
        public string Emote { get; set; }
    }

    public class UserUpdated
    {
        public UserUpdated() { MessageType = "UserUpdated"; }
        public string MessageType { get; set; }
        public DateTime Timestamp { get; set; }
        public NamedEntity Guild { get; set; }
        public NamedEntity User { get; set; }
        public string Status { get; set; }
        public Activity Activity { get; set; }
    }

    public class AddText
    {
        public AddText() { MessageType = "AddText"; }
        public string MessageType { get; set; }
        public NamedEntity Channel { get; set; }
        public string Text { get; set; }
    }

    public class AddReaction
    {
        public AddReaction() { MessageType = "AddReaction"; }
        public string MessageType { get; set; }
        public string ChannelId { get; set; }
        public string MessageId { get; set; }
        public string Reaction { get; set; }
    }
}