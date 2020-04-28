using AutoMapper;
using CourseLibrary.API.Services;
using CourseLibrary.Helpers;
using CourseLibrary.Model;
using CourseLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.Controllers
{
	[ApiController]
	[Route("api/authorcollections")]//route uri for collections of author
	public class AuthorCollectionsController : ControllerBase
	{
		private readonly ICourseLibraryRepository _courseLibraryRepository;
		private readonly IMapper _mapper;

		public AuthorCollectionsController(ICourseLibraryRepository
			courseLibraryRepository, IMapper mapper)
		{
			_courseLibraryRepository = courseLibraryRepository ??
				   throw new ArgumentNullException(nameof(courseLibraryRepository));
			_mapper = mapper ??
				throw new ArgumentNullException(nameof(mapper));

		}


		[HttpGet("({ids})", Name = "GetAuthorCollection")]
		public IActionResult GetAuthorCollection(
			[FromRoute]
			[ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
		{
			if (ids == null)
			{
				return BadRequest();
			}

			var authorEntities = _courseLibraryRepository.GetAuthors(ids);

			if (ids.Count() != authorEntities.Count())
			{
				return BadRequest();
			}

			var authorToReturn = _mapper.Map<IEnumerable<AuthorDto>>(authorEntities);

			return Ok(authorToReturn);
		}

		[HttpPost]
		public ActionResult<IEnumerable<AuthorDto>> CreateAuthorCollection(
			IEnumerable<AuthorForCreationDto> authorCollection)
		{
			var authorEntities = _mapper.Map<IEnumerable<API.Entities.Author>>(authorCollection);
			foreach (var author in authorEntities) 
			{
				_courseLibraryRepository.AddAuthor(author); 
			}

			_courseLibraryRepository.Save();


			var authorCollectionToReturn = _mapper.Map<IEnumerable<AuthorDto>>(authorEntities);
			var idsAsString = string.Join(",", authorCollectionToReturn.Select(a => a.Id));
			return CreatedAtRoute("GetAuthorCollection", 
				new { ids = idsAsString }, authorCollectionToReturn);
		}

		[HttpOptions]
		public IActionResult GetAuthorsOptions()
		{
			Response.Headers.Add("Allow", "GET, OPTION,POST");
			return Ok();
		}
	}
}
