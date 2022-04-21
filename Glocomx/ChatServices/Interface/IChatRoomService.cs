using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glocomx.ChatServices.Interface
{
    interface IChatRoomService
    {
        Task<string> CreateRoom(string connectionId, string roomId);

        Task<string> GetRoomForConnectionId(string connectionId);
    }
}
