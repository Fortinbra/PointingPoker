// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.ResponseCompression;
using PointingPoker.Api.Hubs;

namespace PointingPoker.Api
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements should be documented
	public class Program
#pragma warning restore SA1600 // Elements should be documented
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
	{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements should be documented
		public static void Main(string[] args)
#pragma warning restore SA1600 // Elements should be documented
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers();

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddSignalR();
			builder.Services.AddResponseCompression(opt =>
			{
				opt.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(["application/octet-stream"]);
			});
			var app = builder.Build();
			app.UseResponseCompression();
			app.MapHub<ChatHub>("/chatHub");

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
