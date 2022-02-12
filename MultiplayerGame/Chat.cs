using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour, IChatClientListener
{
    [HideInInspector] public string m_Username;
    public ChatClient m_ChatClient;
    public InputField m_InputField;
    public Text m_ChatContet;


    #region Unity

    private void Start()
    {
        m_ChatClient = new ChatClient(this);
    }

    private void Update()
    {
        m_ChatClient.Service();
    }

    #endregion

    #region PunChat

    public void DebugReturn(DebugLevel level, string message)
    {
        Debug.Log("Chat - " + level + " - " + message);
    }

    public void OnChatStateChange(ChatState state)
    {
        Debug.Log("Chat - OnChatStateChang" + state);
    }

    public void OnConnected()
    {
        Debug.Log("Chat - User" + m_Username + "is connected");
        m_ChatClient.Subscribe(PhotonNetwork.CurrentRoom.Name, creationOptions: new ChannelCreationOptions() { PublishSubscribers = true });
    }

    public void OnDisconnected()
    {
        Debug.Log("Chat - User" + m_Username + "is disconnected");
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        ChatChannel currentChat;
        if(m_ChatClient.TryGetChannel(PhotonNetwork.CurrentRoom.Name, out currentChat))
        {
            m_ChatContet.text = currentChat.ToStringMessages();
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        for (int i = 0; i < channels.Length; i++)
        {
            if (results[i])
            {
                Debug.Log("Chat - Subscribe to " + channels[i] + "channel");
                m_ChatClient.PublishMessage(PhotonNetwork.CurrentRoom.Name, "says hi!");
            }
        }
    }

    public void OnUnsubscribed(string[] channels)
    {

    }

    public void OnUserSubscribed(string channel, string user)
    {

    }

    public void OnUserUnsubscribed(string channel, string user)
    {

    }

    #endregion

    #region Messages

    public void SnedMessage()
    {
        if (m_InputField.text == "")
            return;

        m_ChatClient.PublishMessage(PhotonNetwork.CurrentRoom.Name, m_InputField.text);
        m_InputField.text = "";
    }

    #endregion

}
