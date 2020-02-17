/**
 * MagicOnion
 * Copyright (c) Yoshifumi Kawai
 * Copyright (c) Cysharp, Inc.
 * Released under the MIT license
 * https://github.com/Cysharp/MagicOnion/blob/master/LICENSE
 */
using MagicOnion.Server.Hubs;
using VHChat.Share.Hubs;
using VHChat.Share.MessagePackObjects;
using System.Threading.Tasks;

public class ChatServerHub : StreamingHubBase<IChathub, IChatHubReciever>, IChathub
{
    IGroup chatRoom;
    User mySelf;

    public async Task JoinRoomAsync(User host, string opponentId) {
        const string roomName = "Room";
        this.chatRoom = await this.Group.AddAsync(roomName);

        mySelf = host;

        this.Broadcast(chatRoom).OnJoinRoom(mySelf.Name, mySelf.Biography);
    }

    public async Task LeaveRoomAsync() {
        await chatRoom.RemoveAsync(this.Context);

        this.Broadcast(chatRoom).OnLeaveRoom(mySelf.Name);
    }

    public async Task SendMessageAsync(string message, string emotion) {
        var messageContent = new MessageContent();
        SettingMeta(messageContent, mySelf);
        messageContent.Message = message;
        messageContent.WithEmotion = emotion;
        
        this.Broadcast(chatRoom).OnSendMessage(messageContent.Name, messageContent.Message, messageContent.WithEmotion);
    }

    public Task GenerateIdAsync() {
        throw new System.NotImplementedException();
    }

    protected override ValueTask OnDisconnected() {
        return CompletedTask;
    }

    void SettingMeta(MessageContent messageContent, User profile) {
        messageContent.UserId = profile.UserId;
        messageContent.Name = profile.Name;
    }

}

