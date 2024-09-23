// <copyright file="User.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PointingPoker.Models
{
    /// <summary>
    /// User class.
    /// </summary>
    /// <param name="username">Username.</param>
    /// <param name="card">Card.</param>
    public class User(string username, string? card = null)
    {
        /// <summary>
        /// Gets or sets Username.
        /// </summary>
        public string Username { get; set; } = username;

        /// <summary>
        /// Gets or sets Card.
        /// </summary>
        public string? Card { get; set; } = card;
    }
}