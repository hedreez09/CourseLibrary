using CourseLibrary.API.Services;
using CourseLibrary.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using CourseLibrary.Helpers;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourseLibrary.ResourceParameters;

namespace CourseLibrary.Controllers
{
	[ApiController]
	[Route("api/authors")]
	public class AuthorsController : ControllerBase
	{
		private readonly ICourseLibraryRepository _courseLibraryRepository;
		private readonly IMapper _mapper;
		

		//Injection of repository through the class constructor
		public AuthorsController(ICourseLibraryRepository courseLibraryRepository, 
			IMapper mapper)
		{
			//checking if it not null 
			_courseLibraryRepository = courseLibraryRepository ??
				throw new ArgumentNullException(nameof(courseLibraryRepository));
			_mapper = mapper ??
				throw new ArgumentNullException(nameof(mapper));
		}

		//To Get list of Authors
		[HttpGet()]
		[HttpHead]
		public ActionResult<IEnumerable<AuthorDto>> GetAuthors(
			[FromQuery] AuthorsResourceParameters authorsResourceParameters)
		{
			// Return authors and to do that were turn it from the ICourseLibraryRepository
			var authorsFromRepo = 
				_courseLibraryRepository.GetAuthors(authorsResourceParameters);


			//var authors = new  List<AuthorDto>(); //Initializing a dto object 
;			return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo));  
		}

		//To get a single author
		[HttpGet("{authorid}")]
		public IActionResult GetAuthor(Guid authorId)
		{
			var authorFromRepo = _courseLibraryRepository.GetAuthor(authorId);

			if (authorFromRepo == null)
			{
				return NotFound();
			}

			return  Ok(_mapper.Map<AuthorDto>(authorFromRepo));
		} 

	}
}
