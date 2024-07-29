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
		private static readonly ConcurrentDictionary<string, ConcurrentDictionary<string, string>> TableUsers = new ConcurrentDictionary<string, ConcurrentDictionary<string, string>>();

		/// <summary>
		/// Joins the table.
		/// </summary>
		/// <param name="tableId">Table to join.</param>
		/// <param name="username">Username of the user.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		/// <exception cref="HubException">Thrown when the username is null or empty.</exception>
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
				if (TableUsers.TryGetValue(existing.TableId, out var users))
				{
					users.TryRemove(connectionId, out _);
				}
			}

			UserConnections[connectionId] = (username, tableId);
			await this.Groups.AddToGroupAsync(connectionId, tableId);

			var tableUsers = TableUsers.GetOrAdd(tableId, _ => new ConcurrentDictionary<string, string>());
			tableUsers[connectionId] = username;

			await this.Clients.Group(tableId).SendAsync("UserJoined", username);
		}

		/// <summary>
		/// Leaves the table.
		/// </summary>
		/// <param name="tableId">Table to leave.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		public async Task LeaveTable(string tableId)
		{
			var connectionId = this.Context.ConnectionId;

			if (UserConnections.TryRemove(connectionId, out var userInfo))
			{
				await this.Groups.RemoveFromGroupAsync(connectionId, userInfo.TableId);
				if (TableUsers.TryGetValue(userInfo.TableId, out var users))
				{
					users.TryRemove(connectionId, out _);
				}

				await this.Clients.Group(userInfo.TableId).SendAsync("UserLeft", userInfo.Username);
			}
		}

		/// <summary>
		/// Gets the users in the specified table.
		/// </summary>
		/// <param name="tableId">The table identifier.</param>
		/// <returns>An array of usernames in the specified table.</returns>
		public Task<string[]> GetUsersInTable(string tableId)
		{
			if (TableUsers.TryGetValue(tableId, out var users))
			{
				return Task.FromResult(users.Values.ToArray());
			}

			return Task.FromResult(Array.Empty<string>());
		}

		/// <summary>
		/// Plays the card.
		/// </summary>
		/// <param name="card">Poker card.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
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
