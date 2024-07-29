// <copyright file="PokerHub.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using PointingPoker.Models.Enums;

namespace PointingPoker.Api.Hubs
{
	/// <summary>
	/// Poker hub.
	/// </summary>
	public class PokerHub : Hub
	{
		private static readonly ConcurrentDictionary<string, (string Username, string TableId)> UserConnections = new ConcurrentDictionary<string, (string, string)>();

		/// <summary>
		/// Joins the table.
		/// </summary>
		/// <param name="tableId">Table to Join.</param>
		/// <param name="username">Username of the user.</param>
		/// <returns>Task.</returns>
		public async Task JoinTable(string tableId, string username)
		{
			if (string.IsNullOrEmpty(username))
			{
				throw new HubException("Username is required.");
			}

			var connectionId = this.Context.ConnectionId;

			// Remove from any existing table
			if (UserConnections.TryGetValue(connectionId, out var existing))
			{
				await this.Groups.RemoveFromGroupAsync(connectionId, existing.TableId);
			}

			UserConnections[connectionId] = (username, tableId);
			await this.Groups.AddToGroupAsync(connectionId, tableId);
		}

		/// <summary>
		/// Leaves the table.
		/// </summary>
		/// <param name="tableId">Table to Leave.</param>
		/// <returns>Task.</returns>
		public async Task LeaveTable(string tableId)
		{
			var connectionId = this.Context.ConnectionId;

			if (UserConnections.TryRemove(connectionId, out var userInfo))
			{
				await this.Groups.RemoveFromGroupAsync(connectionId, userInfo.TableId);
			}
		}

		/// <summary>
		/// Plays the card.
		/// </summary>
		/// <param name="card">Poker Card.</param>
		/// <returns>Task.</returns>
		public async Task PlayCard(PokerCard card)
		{
			var connectionId = this.Context.ConnectionId;

			if (UserConnections.TryGetValue(connectionId, out var userInfo))
			{
				await this.Clients.Group(userInfo.TableId).SendAsync("ReceiveCard", userInfo.Username, card);
			}
		}
	}
}
