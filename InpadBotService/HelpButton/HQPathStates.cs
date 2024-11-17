using InpadBotService.DatasFuncs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace InpadBotService.HelpButton;

internal class HelpQuestionPluginState : IState
{
    public string Message { get; } = "helpByDownload";
    private readonly ITelegramBotClient _botClient;
    public HelpQuestionPluginState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        if (request.Update.CallbackQuery is not { } query) return;
        if (query.Message is not { } message) return;
        Console.WriteLine("Start Execute command");

        DataBuilder.UpdateData(context, Message);   // ���������� �������� �������� � Data

        var pairs = new[] {
            ("Revit 2019", "Revit2019"),
            ("Revit 2020", "Revit2020"),
            ("Revit 2021", "Revit2021"),
            ("Revit 2022", "Revit2022"),
            ("Revit 2023", "Revit2023"),
            ("Revit 2024", "Revit2024"),
            ("Revit 2025", "Revit2025")
            };
        var inlineKeyboardMarkup = InlineKeyboardBuilder.Build(pairs);

        await _botClient.AnswerCallbackQuery(
            query.Id);

        await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "�������� ������ Revit, � ������� ��������� ������.",
            replyMarkup: inlineKeyboardMarkup
        );

        context.SetState(new HelpQuestionVersionRevitState(_botClient));
    }
}

internal class HelpQuestionVersionRevitState : IState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "HelpQuestionVersionRevitState";

    public HelpQuestionVersionRevitState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        if (request.Update.CallbackQuery is not { } query) return;
        if (query.Message is not { } message) return;
        Console.WriteLine("Start Execute command");

        DataBuilder.UpdateData(context, Message);   // ���������� ������ � Data

        await _botClient.AnswerCallbackQuery(
            query.Id);

        await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "������� ������������ ����, ������� � ��� ����."
        );

        context.SetState(new HelpQuestionLicenseState(_botClient));
    }
}

internal class HelpQuestionLicenseState : IState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "HelpQuestionLicenseState";

    public HelpQuestionLicenseState (ITelegramBotClient client)
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

        context.SetState(new HelpQuestionNumberBuildState(_botClient));
    }
}

internal class HelpQuestionNumberBuildState : IState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "HelpQuestionNumberBuildState";

    public HelpQuestionNumberBuildState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        if (request.Update.Message is null) return;
        Console.WriteLine("Start Execute command");

        DataBuilder.UpdateData(context, Message);   // ���������� ������ ������ � Data

        await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "������� ��� ������."
        );

        context.SetState(new HelpQuestionGetQuestionState(_botClient));
    }
}

internal class HelpQuestionGetQuestionState : IState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "HelpQuestionGetQuestionState";

    public HelpQuestionGetQuestionState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
		if (request.Update.Message is null) return;
		Console.WriteLine("Start Execute command");

        DataBuilder.UpdateData(context, Message);   // ���������� ������� � Data

        var pairs = new[] {
            ("��������� ����", "Send"),
            ("�� ���������� ����", "Dont send")
            };
        var inlineKeyboardMarkup = InlineKeyboardBuilder.Build(pairs);

        await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "���������� ���� ����.",
            replyMarkup: inlineKeyboardMarkup
        );

        context.SetState(new DistributorState<ISendingFileState>(
            context.ServiceProvider.GetServices<ISendingFileState>()));
    }
}