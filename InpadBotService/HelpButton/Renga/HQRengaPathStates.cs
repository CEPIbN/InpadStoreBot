using InpadBotService.HelpButton;
using InpadBotService;
using Telegram.Bot;
using InpadBotService.DatasFuncs;

internal class HQRengaPluginState : IState
{
	public string Message { get; } = "helpByDownload";
	private readonly ITelegramBotClient _botClient;
	public HQRengaPluginState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		if (request.Update.CallbackQuery is not { } query) return;
		if (query.Message is not { } message) return;
		Console.WriteLine("Start Execute command");

        DataBuilder.UpdateData(context, Message);  // ���������� �������� �������� � Data

        await _botClient.AnswerCallbackQuery(
			query.Id);

		await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "������� ������������ ����, ������� � ��� ����."
		);

		context.SetState(new HQRengaLicenseState(_botClient));
	}
}

internal class HQRengaLicenseState : IState
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "";

	public HQRengaLicenseState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		if (request.Update.Message is null) return;
		Console.WriteLine("Start Execute command");

        DataBuilder.UpdateData(context, Message);   // ���������� ������������� ����� � Data

        await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "�������� ������ Renga, � ������� �� ���������."
		);

		context.SetState(new HQRengaVersionState(_botClient));
	}
}

internal class HQRengaVersionState : IState
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "";

	public HQRengaVersionState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		if (request.Update.Message is null) return;
		Console.WriteLine("Start Execute command");

        DataBuilder.UpdateData(context, Message);   // ���������� ������������� ����� � Data

        await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "�������� ����� ������ ��������, ������� �� ������������."
		);

		context.SetState(new HQNumberBuildState(_botClient));
	}
}

