using UnityEngine;
using System.Collections;
using ExitGames.Client.Photon.Chat;

public class TestChat : MonoBehaviour, IChatClientListener {

	public ChatClient chatClient;
	public string ChatAppId;
	public string UserName;

	public string[] ChannelsToJoinOnConnect;    // set in inspector. Demo channels to join automatically.
	public int HistoryLengthToFetch;            // set in inspector. Up to a certain degree, previously sent messages can be fetched for context
	private ChatChannel selectedChannel;


	IEnumerator Start ()
	{
		chatClient = new ChatClient( this );
		chatClient.Connect(ChatAppId, "1.0", new AuthenticationValues(this.UserName));

		while (true) {
			this.chatClient.PublishMessage(ChannelsToJoinOnConnect[0], "says '15sec'.");
			yield return new WaitForSeconds (15.05f);
		}
	}



	// Update is called once per frame
	public void Update () {
		if (this.chatClient != null)
		{
			this.chatClient.Service();  // make sure to call this regularly! it limits effort internally, so calling often is ok!
		}
	}
	public void OnApplicationQuit()
	{
		if (this.chatClient != null)
		{
			this.chatClient.Disconnect();
		}
	}

	public void OnGetMessages( string channelName, string[] senders, object[] messages )
	{
		string msgs = "";
		for ( int i = 0; i < senders.Length; i++ )
		{
			msgs += senders[i] + "=" + messages[i] + ", ";
		}
		Debug.Log( "OnGetMessages: " + channelName + "(" + senders.Length + ") > " + msgs );
	}

	public void OnPrivateMessage( string sender, object message, string channelName )
	{
		ChatChannel ch = this.chatClient.PrivateChannels[ channelName ];
		foreach ( object msg in ch.Messages )
		{
			Debug.Log( msg );
		}
	}

	public void DebugReturn(ExitGames.Client.Photon.DebugLevel level, string message)
	{
		if (level == ExitGames.Client.Photon.DebugLevel.ERROR)
		{
			UnityEngine.Debug.LogError(message);
		}
		else if (level == ExitGames.Client.Photon.DebugLevel.WARNING)
		{
			UnityEngine.Debug.LogWarning(message);
		}
		else
		{
			UnityEngine.Debug.Log(message);
		}
	}
	
	public void OnConnected()
	{
		if (this.ChannelsToJoinOnConnect != null && this.ChannelsToJoinOnConnect.Length > 0)
		{
			this.chatClient.Subscribe(this.ChannelsToJoinOnConnect, this.HistoryLengthToFetch);
		}
		
//		this.chatClient.AddFriends(new string[] {"tobi", "ilya"});          // Add some users to the server-list to get their status updates
//		this.chatClient.SetOnlineStatus(ChatUserStatus.Online);             // You can set your online state (without a mesage).
	}
	
	public void OnDisconnected()
	{
	}
	public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
	{
		// this is how you get status updates of friends.
		// this demo simply adds status updates to the currently shown chat.
		// you could buffer them or use them any other way, too.
		
//		ChatChannel activeChannel = this.selectedChannel;
//		if (activeChannel != null)
//		{
//			activeChannel.Add("info", string.Format("{0} is {1}. Msg:{2}", user, status, message));
//		}
//		
//		Debug.LogWarning("status: " + string.Format("{0} is {1}. Msg:{2}", user, status, message));
	}
	public void OnSubscribed(string[] channels, bool[] results)
	{
		// in this demo, we simply send a message into each channel. This is NOT a must have!
		foreach (string channel in channels)
		{
			this.chatClient.PublishMessage(channel, "says 'hi'."); // you don't HAVE to send a msg on join but you could.
		}
	}
	
	public void OnUnsubscribed(string[] channels)
	{
	}
	public void OnChatStateChange(ChatState state)
	{
	}
}
