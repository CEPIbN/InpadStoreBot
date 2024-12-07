using InpadBotService.HelpButton;
using InpadBotService;
using Telegram.Bot;
using InpadBotService.DatasFuncs;

internal class HelpReportRengaPluginState : IState
{
	public string Message { get; } = "plugin";
	private readonly ITelegramBotClient _botClient;
	public HelpReportRengaPluginState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		//if (request.Update.CallbackQuery is not { } query) return;
		//if (query.Message is not { } message) return;
		Console.WriteLine("Start Execute command");

		context.data.AddPair(Message, context.CurrentMessage);
		//DataBuilder.UpdateData(context, Message);   // ���������� �������� �������� � Data

		context.SetState(new HelpReportRengaLicenseState(_botClient));

		return await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "������� ������������ ����, ������� � ��� ����.",
			request.QueryId
		);
	}
}

internal class HelpReportRengaLicenseState : IState
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "license";

	public HelpReportRengaLicenseState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		//if (request.Update.Message is null) return;
		Console.WriteLine("Start Execute command");

		context.UpdateData(Message, context.CurrentMessage);
        //DataBuilder.UpdateData(context, Message);   // ���������� ������������� ����� � Data

		context.SetState(new HelpReportRengaVersionState(_botClient));

		return await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "�������� ������ Renga, � ������� �� ���������.",
			request.QueryId
		);
	}
}

internal class HelpReportRengaVersionState : IState
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "rengaVersion";

	public HelpReportRengaVersionState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		//if (request.Update.Message is null) return;
		Console.WriteLine("Start Execute command");

		context.UpdateData(Message, context.CurrentMessage);
		//DataBuilder.UpdateData(context, Message);   // ���������� ������������� ����� � Data

		context.SetState(new HelpReportNumberBuildState(_botClient));

		return await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "�������� ����� ������ ��������, ������� �� ������������.",
			request.QueryId
		);
	}
}

