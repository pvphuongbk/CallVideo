//using Microsoft.AspNetCore.SignalR;
//using System;
//using System.Collections.Concurrent;
//using System.Threading.Tasks;

//public class VideoCallHub1 : Hub
//{
//    // Lưu trữ thông tin cuộc gọi theo VideoCallId
//    private static ConcurrentDictionary<string, (string CallerId, string CalleeId)> ActiveCalls
//        = new ConcurrentDictionary<string, (string, string)>();

//    /// <summary>
//    /// Bắt đầu một cuộc gọi và tạo VideoCallId
//    /// </summary>
//    public async Task StartCall(string calleeConnectionId)
//    {
//        string videoCallId = Guid.NewGuid().ToString();

//        // Lưu thông tin cuộc gọi
//        ActiveCalls[videoCallId] = (CallerId: Context.ConnectionId, CalleeId: calleeConnectionId);

//        // Gửi cho người nhận biết có cuộc gọi đến
//        await Clients.Client(calleeConnectionId).SendAsync("IncomingCall", videoCallId, Context.ConnectionId);
//    }

//    /// <summary>
//    /// Gửi Offer qua VideoCallId
//    /// </summary>
//    public async Task SendOffer(string videoCallId, string offer)
//    {
//        if (ActiveCalls.TryGetValue(videoCallId, out var callInfo))
//        {
//            string targetId = (callInfo.CallerId == Context.ConnectionId) ? callInfo.CalleeId : callInfo.CallerId;
//            await Clients.Client(targetId).SendAsync("ReceiveOffer", videoCallId, offer);
//        }
//    }

//    /// <summary>
//    /// Gửi Answer qua VideoCallId
//    /// </summary>
//    public async Task SendAnswer(string videoCallId, string answer)
//    {
//        if (ActiveCalls.TryGetValue(videoCallId, out var callInfo))
//        {
//            string targetId = (callInfo.CallerId == Context.ConnectionId) ? callInfo.CalleeId : callInfo.CallerId;
//            await Clients.Client(targetId).SendAsync("ReceiveAnswer", videoCallId, answer);
//        }
//    }

//    /// <summary>
//    /// Gửi ICE Candidate qua VideoCallId
//    /// </summary>
//    public async Task SendIceCandidate(string videoCallId, string candidate)
//    {
//        if (ActiveCalls.TryGetValue(videoCallId, out var callInfo))
//        {
//            string targetId = (callInfo.CallerId == Context.ConnectionId) ? callInfo.CalleeId : callInfo.CallerId;
//            await Clients.Client(targetId).SendAsync("ReceiveIceCandidate", videoCallId, candidate);
//        }
//    }

//    /// <summary>
//    /// Kết thúc cuộc gọi
//    /// </summary>
//    public async Task EndCall(string videoCallId)
//    {
//        if (ActiveCalls.TryRemove(videoCallId, out var callInfo))
//        {
//            await Clients.Client(callInfo.CallerId).SendAsync("CallEnded", videoCallId);
//            await Clients.Client(callInfo.CalleeId).SendAsync("CallEnded", videoCallId);
//        }
//    }

//    /// <summary>
//    /// Xử lý khi client disconnect
//    /// </summary>
//    public override async Task OnDisconnectedAsync(Exception exception)
//    {
//        foreach (var kvp in ActiveCalls)
//        {
//            var (caller, callee) = kvp.Value;
//            if (caller == Context.ConnectionId || callee == Context.ConnectionId)
//            {
//                await EndCall(kvp.Key);
//                break;
//            }
//        }

//        await base.OnDisconnectedAsync(exception);
//    }
//}
