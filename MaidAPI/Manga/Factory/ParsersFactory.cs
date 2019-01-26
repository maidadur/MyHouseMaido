﻿namespace MaidAPI.Manga
{
	using Microsoft.Extensions.DependencyInjection;
	using System;

	public interface IParsersFactory
	{
		IMangaParser GetParser(string serviceName);
	}

	public class ParsersFactory : IParsersFactory
	{
		public IMangaParser GetParser(string serviceName) {
			if (serviceName == "fanfox") {
				return new FanFoxParser();
			}
			return null;
		}
	}
}
