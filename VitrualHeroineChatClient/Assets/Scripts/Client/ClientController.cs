/**
 * MagicOnion
 * Copyright (c) Yoshifumi Kawai
 * Copyright (c) Cysharp, Inc.
 * Released under the MIT license
 * https://github.com/Cysharp/MagicOnion/blob/master/LICENSE
 * 
 * UniVRM
 * Copyright (c) 2018 DWANGO Co., Ltd. for UniVRM
 * Copyright (c) 2018 ousttrue for UniGLTF, UniHumanoid
 * Copyright (c) 2018 Masataka SUMI for MToon
 * https://github.com/vrm-c/UniVRM/blob/master/LICENSE.txt
 */
using Grpc.Core;
using MagicOnion.Client;
using VHChat.Share.Hubs;
using VHChat.Share.MessagePackObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRM;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class ClientController : MonoBehaviour, IChatHubReciever
{
    private Channel channel;
    private IChathub chathub;

    private User me;

    [SerializeField]
    private VRMBlendShapeProxy proxy;
    [SerializeField]
    private GameObject messagePrefab;
    private TMP_Text messagePrefab_content;
    [SerializeField]
    private Transform Parent;

    private TMP_InputField messageInputField;
    private TMP_Text messageText;
    private TMP_Dropdown dropdown;

    private Dictionary<int, BlendShapePreset> blendShapeMap = new Dictionary<int, BlendShapePreset>() {
        {0, BlendShapePreset.Neutral },
        {1, BlendShapePreset.Fun },
        {2, BlendShapePreset.Angry },
        {3, BlendShapePreset.Sorrow },
        {4, BlendShapePreset.Joy }
    };

    void Awake() {
        messageInputField = GameObject.Find("MessageInput").GetComponent<TMP_InputField>();
        messageText = GameObject.Find("MessageText").GetComponent<TMP_Text>();
        dropdown = GameObject.Find("Dropdown").GetComponent<TMP_Dropdown>();
    }

    // Start is called before the first frame update
    async void Start()
    {
        this.channel = new Channel("localhost:55555", ChannelCredentials.Insecure);
        this.chathub = StreamingHubClient.Connect<IChathub, IChatHubReciever>(this.channel, this);

        me = new User {
            Name = PlayerPrefs.GetString("UserName"),
        };
        await this.chathub.JoinRoomAsync(me, PlayerPrefs.GetString("RoomName", "none"));
    }

    async private void OnDestroy() {
        await this.chathub.DisposeAsync();
        await this.channel.ShutdownAsync();
    }
    public void OnJoinRoom(string name) {
        Debug.Log($"{name}が入室しました。");
    }

    public void OnLeaveRoom(string name) {
        Debug.Log($"{name}が退室しました。");
    }

    public void OnSendMessage(string name, string message, string emotion) {
        //Debug.Log($"{name} : 「{message}」");
        var createdPrefab = Instantiate(messagePrefab, Vector3.zero, Quaternion.identity);
        createdPrefab.transform.parent = Parent;

        messagePrefab_content = createdPrefab.transform.GetComponentInChildren<TMP_Text>();
        messagePrefab_content.text = $"[ {name} ] : {message}";

        ResetEmotion();
        proxy.ImmediatelySetValue(ConvertBlendShapeKey(emotion), 1);
        Debug.Log($"表情が「{emotion}」に設定された");
    }

    private BlendShapePreset ConvertBlendShapeKey(string key) {
        switch (key) {
            case "Neutral":
                return BlendShapePreset.Neutral;
            case "Fun":
                return BlendShapePreset.Fun;
            case "Angry":
                return BlendShapePreset.Angry;
            case "Sorrow":
                return BlendShapePreset.Sorrow;
            case "Joy":
                return BlendShapePreset.Joy;
            default:
                Debug.Log("未登録の表情です。");
                return BlendShapePreset.Neutral;
        }
    }

    private void ResetEmotion() {
        foreach(var blendShape in blendShapeMap) {
            proxy.ImmediatelySetValue(blendShape.Value, 0);
        }
    }

    // Update is called once per frame
    async void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            await this.chathub.LeaveRoomAsync();
            Debug.Log("退室しました。");
            SceneManager.LoadScene("EntryScene");
        }
    }

    public void InputMessageText() {
        messageText.text = messageInputField.text;
    }

    public async void OnClickSendMessageButton() {
        var message = GetMessageText();
        var emotion = GetEmotion(dropdown.value);
        //Debug.Log($"{message} , {emotion}");
        await this.chathub.SendMessageAsync(message, emotion);
    }

    private string GetMessageText() {
        return messageText.text;
    }

    private string GetEmotion(int index) {
        return blendShapeMap[index].ToString();
    }

}
