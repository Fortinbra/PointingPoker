// <copyright file="ChatHub.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.SignalR;

namespace PointingPoker.Api.Hubs
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements should be documented
	public class ChatHub : Hub
#pragma warning restore SA1600 // Elements should be documented
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
	{
		/// <summary>
		/// Sends messages to all clients.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="message">The message.</param>
		/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		public async Task SendMessage(string user, string message)
		{
			await this.Clients.All.SendAsync("ReceiveMessage", user, message);
		}
	}
}
