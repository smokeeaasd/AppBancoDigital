using System;

namespace AppBancoDigital
{
	public sealed class AppServices
	{
		public static event EventHandler OnLogRequest;
		public static event EventHandler OnNotificationRequest;

		public static void SendLogRequest(string Tag, string Message)
		{
			OnLogRequest?.Invoke(new LogData(Tag, Message), EventArgs.Empty);
		}

		public static void SendNotificationRequest(NotificationData data)
		{
			OnNotificationRequest?.Invoke(data, EventArgs.Empty);
		}
	}

	public class LogData
	{
		public string Tag { get; set; }
		public string Message { get; set; }

		public LogData(string tag, string message)
		{
			Tag = tag;
			Message = message;
		}
	}

	public class NotificationData
	{
		private static int id = 0;
		public NotificationData(string channelId, string title, string message, string channelName, string description)
		{
			id++;
			NotificationId = id;
			ChannelId = channelId;
			Title = title;
			Message = message;
			ChannelName = channelName;
			Description = description;
		}

		public int NotificationId { get; }
		public string ChannelId { get; }
		public string Title { get; }
		public string Message { get; }
		public string ChannelName { get; }
		public string Description { get; }
	}
}