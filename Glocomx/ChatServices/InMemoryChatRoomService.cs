using Glocomx.ChatServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glocomx.ChatServices
{
    public class InMemoryChatRoomService : IChatRoomService
	{
		private readonly Dictionary<string, ChatRoom> _roomInfo = new Dictionary<string, ChatRoom>();

		public Task<string> CreateRoom(string connectionId, string streamId)
		{
			_roomInfo[streamId] = new ChatRoom
			{
				HostConnectionId = connectionId
			};

			return Task.FromResult(streamId);
		}

		public Task<string> GetRoomForConnectionId(string streamId)
		{
			var foundRoom = _roomInfo.FirstOrDefault(
				x => x.Key == streamId);

			if (foundRoom.Key == "")
				throw new ArgumentException("Invalid connection ID");

			return Task.FromResult(foundRoom.Key);
		}
	}
}
