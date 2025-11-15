using Godot;
using Microsoft.AspNetCore.SignalR.Client;
using System.Text.Json;
using System.Threading.Tasks;

public partial class NetworkManager : Node
{
	private HubConnection _connection;

	public MessageEventHandler MessageHandler { get; private set; }
	public UserCountEventHandler UserCountHandler { get; private set; }
	public UserJoinedEventHandler UserJoinedHandler { get; private set; }
	public UserLeftEventHandler UserLeftHandler { get; private set; }
	public MessageSender MessageSender { get; private set; }

	public override void _Ready()
	{
		_connection = (
			new HubConnectionBuilder()
			.WithUrl(GetServerUrl(), options =>{
				options.Headers.Add("X-Godot-Client-Key", GetXGodotClientKey());
			})
			.Build()
		);

		MessageHandler = new MessageEventHandler(_connection);
		UserCountHandler = new UserCountEventHandler(_connection);
		UserJoinedHandler = new UserJoinedEventHandler(_connection);
		UserLeftHandler = new UserLeftEventHandler(_connection);
		MessageSender = new MessageSender(_connection);

		Task.Run(async () =>
		{
			try
			{
				GD.Print("Attempting to connect to SignalR.");
				await _connection.StartAsync();
				GD.Print("SignalR Connection established.");
			}
			catch (System.Exception e)
			{
				GD.PrintErr($"Could not connect to SignalR: {e.Message}");
			}
		});
	}

	private string GetServerUrl()
	{
		if (OS.HasFeature("debug"))
		{
			return "http://localhost:8080/chatHub";
		}

		using (var file = FileAccess.Open(
			"res://client_secret.json",
			FileAccess.ModeFlags.Read
		))
		{
			using (JsonDocument document = JsonDocument.Parse(
				file.GetAsText()
			))
			{
				return (
					document
					.RootElement
					.GetProperty("ChatSignalServerUrl")
					.GetString()
				);
			}
		}
	}

	private string GetXGodotClientKey()
	{
		if (OS.HasFeature("debug"))
		{
			return "X_GODOT_CLIENT_KEY_DEV";
		}

		using (var file = FileAccess.Open(
			"res://client_secret.json",
			FileAccess.ModeFlags.Read
		))
		{
			using (JsonDocument document = JsonDocument.Parse(
				file.GetAsText()
			))
			{
				return (
					document
					.RootElement
					.GetProperty("XGodotClientKey")
					.GetString()
				);
			}
		}
	}

	public override void _ExitTree()
	{
		_connection?.StopAsync();
	}
}
