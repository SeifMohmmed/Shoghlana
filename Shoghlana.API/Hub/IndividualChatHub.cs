﻿using Microsoft.AspNetCore.SignalR;
using Shoghlana.Application.DTOs;
namespace Shoghlana.API.Services.Implementations;
public class IndividualChatHub : Hub
{
    private readonly ChatServices _chatService;

    public IndividualChatHub(ChatServices chatService)
    {
        _chatService = chatService;
    }

    public override async Task OnConnectedAsync()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, "come2chat");
        await Clients.Caller.SendAsync("UserConnected");
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, "come2chat");

        var user =
            _chatService.GetUserByConnectionID(Context.ConnectionId);

        _chatService.RemoveUserFromList(user);

        await DisplayOnlineUsers();

        await base.OnDisconnectedAsync(exception);
    }

    public async Task AddUserConnectionId(string name)
    {
        _chatService.AddUserConnectionID(name, Context.ConnectionId);

        await DisplayOnlineUsers();
    }

    public async Task ReceiveMessage(MessageDTO message)
    {
        await Clients.Groups("come2chat").SendAsync("NewMessage", message);
    }

    public async Task CreatePrivateChat(MessageDTO message)
    {
        string privateGroupName =
            GetPrivateGroupName(message.From, message.To);

        await Groups.AddToGroupAsync(Context.ConnectionId, privateGroupName);

        var toConnectionId =
            _chatService.GetConnectionByUser(message.To);

        //opening Private Chatbox For other End Users
        await Clients.Client(toConnectionId).SendAsync("openPrivateChat", message);
    }

    public async Task ReceivePrivateMessage(MessageDTO message)
    {
        string privateGroupName =
            GetPrivateGroupName(message.From, message.To);

        await Clients.Group(privateGroupName).SendAsync("NewPrivateMessage", message);
    }

    public async Task RemovePrivateChat(string From, string To)
    {
        string privateGroupName = GetPrivateGroupName(From, To);

        await Clients.Group(privateGroupName).SendAsync("ClosePrivateChat");

        await Groups.RemoveFromGroupAsync(Context.ConnectionId, privateGroupName);

        var toConnectionId = _chatService.GetConnectionByUser(To);

        //await Groups.AddToGroupAsync(toConnectionId , privateGroupName);

        await Groups.RemoveFromGroupAsync(toConnectionId, privateGroupName);

    }

    private async Task DisplayOnlineUsers()
    {
        var onlineUsers = _chatService.GetOnlineUsers();
        await Clients.Groups("come2chat").SendAsync("OnlineUsers", onlineUsers);
    }

    private string GetPrivateGroupName(string From, string To)
    {
        var stringCompare = string.Compare(From, To) < 0;
        return stringCompare ? $"{From}-{To}" : $"{To}-{From}";
    }
}
