﻿using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Identity;
using Shoghlana.Core.Models;
using System.Collections.Concurrent;

namespace Shoghlana.EF.Hubs;
public class NotificationHub : Hub
{
    private static ConcurrentDictionary<string, string> userConnectionMap = new ConcurrentDictionary<string, string>();
    private readonly UserManager<ApplicationUser> _userManager;

    public NotificationHub(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public override async Task OnConnectedAsync()
    {
        //Retrive User Information
        var user = await _userManager.GetUserAsync(Context.User);
        if (user != null)
        {
            userConnectionMap[user.Id] = Context.ConnectionId;
        }
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        //Retrive User Information
        var user = await _userManager.GetUserAsync(Context.User);
        if (user != null)
        {
            userConnectionMap.TryRemove(user.Id, out _);
        }
        await base.OnDisconnectedAsync(exception);
    }

    public static string GetUserConnectionId(string userId)
    {
       userConnectionMap.TryGetValue(userId, out var connectionId);
        return connectionId;
    }
}
