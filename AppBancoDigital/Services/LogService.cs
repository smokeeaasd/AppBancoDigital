using System;

namespace AppBancoDigital
{
	public class LogService
	{
		public event EventHandler OnLogRequest;

		public static LogService Instance = new LogService();

		public void Send(string Tag, string Message)
		{
			OnLogRequest?.Invoke(new LogData(Tag, Message), EventArgs.Empty);
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
}