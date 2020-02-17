/**
 * MagicOnion
 * Copyright (c) Yoshifumi Kawai
 * Copyright (c) Cysharp, Inc.
 * Released under the MIT license
 * https://github.com/Cysharp/MagicOnion/blob/master/LICENSE
 */
using Grpc.Core;
using MagicOnion.Client;
using VHChat.Share.Hubs;
using VHChat.Share.MessagePackObjects;
using UnityEngine;

public class ClientController : MonoBehaviour, IChatHubReciever
{
    private Channel channel;
    private IChathub chathub;

    // Start is called before the first frame update
    async void Start()
    {
        this.channel = new Channel("localhost:55555", ChannelCredentials.Insecure);
        this.chathub = StreamingHubClient.Connect<IChathub, IChatHubReciever>(this.channel, this);

        this.ChatHubExecute();
    }

    async private void OnDestroy() {
        await this.chathub.DisposeAsync();
        await this.channel.ShutdownAsync();
    }

    // Update is called once per frame
    async void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            await this.chathub.SendMessageAsync("こんにちは^^", "smile");
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            await this.chathub.LeaveRoomAsync();
        }
    }

    async void ChatHubExecute() {
        var myProf = new User {
            UserId = "@nemlyc",
            Name = "Motoki",
            Biography = "よろしくお願いします。",
        };

        var opponentId = "@hoge";

        await this.chathub.JoinRoomAsync(myProf, opponentId);
    }

    public void OnJoinRoom(string name, string bio) {
        Debug.Log(
            $"{name}が入室。\n" +
            $"情報 : {bio}"
        );
    }

    public void OnLeaveRoom(string name) {
        Debug.Log($"{name}が退室");
    }

    public void OnSendMessage(string name, string message, string emotion) {
        Debug.Log($"{name} : 「{message}」");
    }
}
