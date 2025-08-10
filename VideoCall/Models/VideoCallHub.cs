using Microsoft.AspNetCore.SignalR;
using System;
using VideoCall.CommonCode;
using VideoCall.DataAccess.Entities;

public class VideoCallHub : Hub
{
    //public static List<string> ConnectedUsers = new List<string>();
    public static HashSet<UserCall> ConnectedUsers = new HashSet<UserCall>();
    private readonly ISessionManager _SessionManag;
    public VideoCallHub(ISessionManager sessionManager)
    {
        _SessionManag = sessionManager;
    }
    private async Task<UserCall> CheckUserCall()
    {
        var currentUser = _SessionManag.GetUserCall();
        if (currentUser.Id == Guid.Empty)
        {
            // Nếu không có user thì không cho kết nối
            return null;
        }

        if (!ConnectedUsers.Select(x => x.Id).Contains(currentUser.Id))
        {
            currentUser.CallId = Context.ConnectionId;
            currentUser.Status = true;
            ConnectedUsers.Add(currentUser);
        }
        else
        {
            ConnectedUsers.FirstOrDefault(x => x.Id == currentUser.Id).CallId = Context.ConnectionId;
            ConnectedUsers.FirstOrDefault(x => x.Id == currentUser.Id).Status = true;
        }
        return currentUser;
    }
    public override async Task OnConnectedAsync()
    {
        var user = await CheckUserCall();
        if(user==null)
        {
            await Clients.Caller.SendAsync("RedirectToLogin");
        }    
        // Gửi ID của mình
        await Clients.Caller.SendAsync("ReceiveYourId", user.CallId);

        // Gửi danh sách user đang online cho người mới
        await Clients.Caller.SendAsync("ReceiveUserList", ConnectedUsers);

        // Thông báo với tất cả có user mới
        await Clients.AllExcept(Context.ConnectionId).SendAsync("UserConnected", user);

        await base.OnConnectedAsync();
    }
    public override async Task OnDisconnectedAsync(Exception exception)
    {
        //var user = await CheckUserCall();

        ConnectedUsers.RemoveWhere(x=>x.CallId== Context.ConnectionId);
        await Clients.All.SendAsync("UserDisconnected", Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }

    public async Task CancelCall(string toConnectionId)
    {
        await Clients.Client(toConnectionId).SendAsync("CallCancelled", Context.ConnectionId);
    }
    public async Task CallUser(string connectionId)
    {
        await Clients.Client(connectionId).SendAsync("ReceiveCall", Context.ConnectionId);
    }

    public async Task AcceptCall(string callerConnectionId)
    {
        var calleeConnectionId = Context.ConnectionId;
        await Clients.Client(callerConnectionId).SendAsync("CallAccepted", calleeConnectionId);
    }



    public async Task SendOffer(string targetConnectionId, string offer)
    {
        await Clients.Client(targetConnectionId).SendAsync("ReceiveOffer", ConnectedUsers.FirstOrDefault(x => x.CallId == Context.ConnectionId), offer);
    }

    public async Task SendAnswer(string targetConnectionId, string answer)
    {
        ConnectedUsers.FirstOrDefault(x => x.CallId == Context.ConnectionId).Status = false;
        ConnectedUsers.FirstOrDefault(x => x.CallId == targetConnectionId).Status = false;
        //await Clients.Caller.SendAsync("ReceiveUserList", ConnectedUsers);
        await Clients.All.SendAsync("UpdateUserList", ConnectedUsers);
        await Clients.Client(targetConnectionId).SendAsync("ReceiveAnswer", Context.ConnectionId, answer);
    }

    public async Task SendIceCandidate(string targetConnectionId, string candidate)
    {
        await Clients.Client(targetConnectionId).SendAsync("ReceiveIceCandidate", Context.ConnectionId, candidate);
    }
    public async Task EndCall(Guid id)
    {
        var callId = ConnectedUsers.FirstOrDefault(x => x.Id == id).CallId;
        // Thông báo cho người còn lại
        await Clients.OthersInGroup(callId).SendAsync("CallEnded");
    }

}