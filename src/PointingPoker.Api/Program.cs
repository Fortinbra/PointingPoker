// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.ResponseCompression;
using PointingPoker.Api.Hubs;

namespace PointingPoker.Api
{
	/// <summary>
	/// The main entry point for the application.
	/// </summary>
	public class Program
	{
		/// <summary>
		/// The main method, which is the entry point of the application.
		/// </summary>
		/// <param name="args">The command-line arguments.</param>
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers();

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddSignalR();
			builder.Services.AddResponseCompression(opt =>
			{
				opt.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
			});

			var app = builder.Build();
			app.UseResponseCompression();
			app.MapHub<PokerHub>("/pokerHub");

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
				app.UseWebAssemblyDebugging();
			}
			else
			{
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseBlazorFrameworkFiles();
			app.UseStaticFiles();

			app.UseAuthorization();

			app.MapControllers();
			app.MapFallbackToFile("index.html");
			app.Run();
		}
	}
}
