﻿using AutoMapper;
using CourseLibrary.API.Services;
using CourseLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.Controllers
{
	[ApiController]
	[Route("api/authors/{authorId}/courses")]
	public class CoursesController : ControllerBase
	{
		private readonly ICourseLibraryRepository _courseLibraryRepository;
		private readonly IMapper _mapper;

		//Injection of repository through the class constructor
		public CoursesController(ICourseLibraryRepository courseLibraryRepository,
			IMapper mapper)
		{
			//checking if it not null 
			_courseLibraryRepository = courseLibraryRepository ??
				throw new ArgumentNullException(nameof(courseLibraryRepository));
			_mapper = mapper ??
				throw new ArgumentNullException(nameof(mapper));
		}

		[HttpGet]
		public ActionResult<IEnumerable<CourseDto>> GetCoursesForAuthor(Guid authorId)
		{
			if (!_courseLibraryRepository.AuthorExists(authorId))//check if author exist
			{
				return NotFound();
			}

			var coursesForAuthorFromRepo = _courseLibraryRepository.GetCourses(authorId);
			return Ok(_mapper.Map<IEnumerable<CourseDto>>(coursesForAuthorFromRepo));
		}

		[HttpGet("{courseId}")]
		public ActionResult<CourseDto> GetCourseForAuthor(Guid authorId,Guid courseId)
		{
			if (!_courseLibraryRepository.AuthorExists(authorId))//check if author exist
			{
				return NotFound();
			}

			var courseForAuthorFromRepo = _courseLibraryRepository.GetCourse(authorId, courseId);

			if (courseForAuthorFromRepo == null)
			{
				return NotFound();
			}

			return Ok(_mapper.Map<CourseDto>(courseForAuthorFromRepo));
		}
	}
}
