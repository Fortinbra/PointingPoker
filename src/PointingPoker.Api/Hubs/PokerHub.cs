// <copyright file="PokerHub.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.SignalR;
using PointingPoker.Models.Enums;

namespace PointingPoker.Api.Hubs
{
	/// <summary>
	/// Poker hub.
	/// </summary>
	public class PokerHub : Hub
	{
		/// <summary>
		/// Joins the table.
		/// </summary>
		/// <param name="tableId">Table to Join.</param>
		/// <returns>Task.</returns>
		public async Task JoinTable(string tableId)
		{
			await this.Groups.AddToGroupAsync(this.Context.ConnectionId, tableId);
		}

		/// <summary>
		/// Leaves the table.
		/// </summary>
		/// <param name="tableId">Table to Leave.</param>
		/// <returns>Task.</returns>
		public async Task LeaveTable(string tableId)
		{
			await this.Groups.RemoveFromGroupAsync(this.Context.ConnectionId, tableId);
		}

		/// <summary>
		/// Plays the card.
		/// </summary>
		/// <param name="tableId">Table Group.</param>
		/// <param name="user">User name.</param>
		/// <param name="card">Poker Card.</param>
		/// <returns>Task.</returns>
		public async Task PlayCard(string tableId, string user, PokerCard card)
		{
			await this.Clients.Group(tableId).SendAsync("ReceiveCard", this.Context.ConnectionId, card);
		}
	}
}
