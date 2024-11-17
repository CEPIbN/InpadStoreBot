using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace InpadBotService.HelpButton;

internal class HQPluginState : IState
{
    public string Message { get; } = "helpByDownload";
    private readonly ITelegramBotClient _botClient;
    public HQPluginState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        if (request.Update.CallbackQuery is not { } query) return;
        if (query.Message is not { } message) return;
        Console.WriteLine("Start Execute command");

        // ���������� �������� �������� � Data
        var pairs = new[] {
            ("Revit 2019", "Revit2019"),
            ("Revit 2020", "Revit2020"),
            ("Revit 2021", "Revit2021"),
            ("Revit 2022", "Revit2022"),
            ("Revit 2023", "Revit2023"),
            ("Revit 2024", "Revit2024"),
            ("Revit 2025", "Revit2025")
            };
        var builder = new InlineKeyboardBuilder(3, 2, pairs); 
        var inlineKeyboardMarkup = builder.Build();

        await _botClient.AnswerCallbackQuery(
            query.Id);

        await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "�������� ������ Revit, � ������� ��������� ������.",
            replyMarkup: inlineKeyboardMarkup
        );

        context.SetState(new HQVersionRevitState(_botClient));
    }
}

internal class HQVersionRevitState : IState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "";

    public HQVersionRevitState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        if (request.Update.CallbackQuery is not { } query) return;
        if (query.Message is not { } message) return;
        Console.WriteLine("Start Execute command");
        // ���������� ������ � Data
        await _botClient.AnswerCallbackQuery(
            query.Id);

        await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "������� ������������ ����, ������� � ��� ����."
        );

        context.SetState(new HQLicenseState(_botClient));
    }
}

internal class HQLicenseState : IState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "";

    public HQLicenseState (ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        if (request.Update.Message is null) return;
        Console.WriteLine("Start Execute command");
        // ���������� ������������� ����� � Data

        await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "�������� ����� ������ ��������, ������� �� ������������."
        );

        context.SetState(new HQNumberBuildState(_botClient));
    }
}

internal class HQNumberBuildState : IState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "";

    public HQNumberBuildState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        if (request.Update.Message is null) return;
        Console.WriteLine("Start Execute command");
        // ���������� ������ ������ � Data

        await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "������� ��� ������."
        );

        context.SetState(new HQGetQuestionState(_botClient));
    }
}

internal class HQGetQuestionState : IState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "";

    public HQGetQuestionState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        if (request.Update.Message is null) return;
        Console.WriteLine("Start Execute command");
        // ���������� ������ ������ � Data

		Console.WriteLine("Start Execute command");
        // ���������� ������� � Data
        var pairs = new[] {
            ("��������� ����", "Send"),
            ("�� ���������� ����", "Dont send")
            };
        var builder = new InlineKeyboardBuilder(2, 1, pairs);
        var inlineKeyboardMarkup = builder.Build();

        await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "���������� ���� ����."
        );
    }
}

internal class HQFinalState : IState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "";

    public HQFinalState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        if (request.Update.Message is null) return;
        Console.WriteLine("Start Execute command");
        // ����� ��������� ����(���� ����) � Data � ��������� Data � ������������

        await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "������ ������ ��� ������� ������ ����������, � ��������� ����� � ���� �������� ����������."
		);
    }
}