/**
 * MessagePack for C#
 * Copyright (c) 2017 Yoshifumi Kawai and contributors
 * Released under the MIT license
 * https://github.com/neuecc/MessagePack-CSharp/blob/master/LICENSE
 */
using MessagePack;

namespace VHChat.Share.MessagePackObjects
{
    [MessagePackObject]
    public class User
    {
        [Key(0)]
        public string UserId { get; set; }

        [Key(1)]
        public string Name { get; set; }

        [Key(2)]
        public string message { get; set; }

        [Key(3)]
        public string WithEmotion { get; set; }
    }
}