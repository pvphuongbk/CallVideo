//using Microsoft.AspNetCore.SignalR;
//using System;

//public class VideoCallHub : Hub
//{
//    //public static List<string> ConnectedUsers = new List<string>();
//    public static HashSet<string> ConnectedUsers = new HashSet<string>();

//    public override async Task OnConnectedAsync()
//    {
//        ConnectedUsers.Add(Context.ConnectionId);

//        // Gửi ID của mình
//        await Clients.Caller.SendAsync("ReceiveYourId", Context.ConnectionId);

//        // Gửi danh sách user đang online cho người mới
//        await Clients.Caller.SendAsync("ReceiveUserList", ConnectedUsers);

//        // Thông báo với tất cả có user mới
//        await Clients.AllExcept(Context.ConnectionId).SendAsync("UserConnected", Context.ConnectionId);

//        await base.OnConnectedAsync();
//    }
//    //private static Dictionary<string, string> ConnectedUsers = new Dictionary<string, string>();

//    public async Task CancelCall(string toConnectionId)
//    {
//        await Clients.Client(toConnectionId).SendAsync("CallCancelled", Context.ConnectionId);
//    }
//    public async Task CallUser(string connectionId)
//    {
//        await Clients.Client(connectionId).SendAsync("ReceiveCall", Context.ConnectionId);
//    }

//    public async Task AcceptCall(string callerConnectionId)
//    {
//        var calleeConnectionId = Context.ConnectionId;
//        await Clients.Client(callerConnectionId).SendAsync("CallAccepted", calleeConnectionId);
//    }

//    public override async Task OnDisconnectedAsync(Exception exception)
//    {
//        ConnectedUsers.Remove(Context.ConnectionId);
//        await Clients.All.SendAsync("UserDisconnected", Context.ConnectionId);
//        await base.OnDisconnectedAsync(exception);
//    }

//    public async Task SendOffer(string targetConnectionId, string offer)
//    {
//        await Clients.Client(targetConnectionId).SendAsync("ReceiveOffer", Context.ConnectionId, offer);
//    }

//    public async Task SendAnswer(string targetConnectionId, string answer)
//    {
//        await Clients.Client(targetConnectionId).SendAsync("ReceiveAnswer", Context.ConnectionId, answer);
//    }

//    public async Task SendIceCandidate(string targetConnectionId, string candidate)
//    {
//        await Clients.Client(targetConnectionId).SendAsync("ReceiveIceCandidate", Context.ConnectionId, candidate);
//    }
//}