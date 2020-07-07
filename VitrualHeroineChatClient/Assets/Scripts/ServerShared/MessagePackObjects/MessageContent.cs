using MessagePack;

namespace VHChat.Share.MessagePackObjects
{
    [MessagePackObject]
    class MessageContent
    {
        [Key(0)]
        public string UserId { get; set; }

        [Key(1)]
        public string Name { get; set; }
        [Key(2)]
        public string Message { get; set; }
        [Key(3)]
        public string WithEmotion { get; set; }
    }
}
