﻿namespace Maid.Manga.API.Controllers
{
	using Maid.Core;
	using Maid.Core.DB;
	using Maid.Manga.DB;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Route("api/mangasource")]
	[ApiController]
	[Authorize]
	public class MangaSourceController : BaseApiController<MangaSource>
	{
		public MangaSourceController(IEntityRepository<MangaSource> entityRepository) : base(entityRepository) {
		}
	}
}