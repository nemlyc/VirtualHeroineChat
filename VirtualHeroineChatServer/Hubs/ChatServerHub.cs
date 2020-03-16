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
using System.Collections.Generic;

public class ChatServerHub : StreamingHubBase<IChathub, IChatHubReciever>, IChathub
{
    IGroup chatRoom;
    User mySelf;

    public async Task JoinRoomAsync(User user,string roomId) {
        string roomName = roomId;
        this.chatRoom = await this.Group.AddAsync(roomName);
        mySelf = user;
        System.Console.WriteLine($"{user.Name}が{roomId}に入室しました。");
        this.Broadcast(chatRoom).OnJoinRoom(mySelf.Name);
    }

    public async Task LeaveRoomAsync() {
        await chatRoom.RemoveAsync(this.Context);

        this.Broadcast(chatRoom).OnLeaveRoom(mySelf.Name);
    }

    public async Task SendMessageAsync(string message, string emotion) {
        mySelf.message = message;
        mySelf.WithEmotion = emotion;
        
        this.Broadcast(chatRoom).OnSendMessage(mySelf.Name, mySelf.message, mySelf.WithEmotion);
    }

    protected override ValueTask OnDisconnected() {
        return CompletedTask;
    }
}

