using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EntrySceneUI : MonoBehaviour
{
    private TMP_InputField nameInputField;
    private TMP_Text nameText;
    private TMP_InputField roomInputField;
    private TMP_Text roomText;

    private const string ChatSceneName = "VHChatClient";
    private const string UserName = "UserName";
    private const string RoomName = "RoomName";

    private void Awake() {
        nameInputField = GameObject.Find("InputField_Name").GetComponent<TMP_InputField>();
        nameText = GameObject.Find("NameText").GetComponent<TMP_Text>();
        roomInputField = GameObject.Find("InputField_Room").GetComponent<TMP_InputField>();
        roomText = GameObject.Find("RoomText").GetComponent<TMP_Text>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log(PlayerPrefs.GetString(UserName, "未入力"));
        }
    }

    public void InputNameText() {
        nameText.text = nameInputField.text;
        SaveUserName();
    }
    public void InputRoomText() {
        roomText.text = roomInputField.text;
        SaveRoomName();
    }

    public void EntryChatScene() {
        if (IsFilledText()) {
            SceneManager.LoadScene(ChatSceneName);
        } else {
            Debug.Log("名前が空欄です。");
        }
    }

    private bool IsFilledText() {
        if (!nameInputField.text.Equals("") && !roomInputField.text.Equals("")) {
            return true;
        } else {
            return false;
        }
    }

    private void SaveUserName() {
        PlayerPrefs.SetString(UserName, nameText.text);
    }
    private void SaveRoomName() {
        PlayerPrefs.SetString(RoomName, roomText.text);
    }

}
