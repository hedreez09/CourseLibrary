using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.Profiles
{
	public class CoursesProfile :Profile
	{
		public CoursesProfile()
		{
			CreateMap<API.Entities.Course, Models.CourseDto>();

			//mapping Source to Dest(CourseCreatnDto to courrse)
			CreateMap<Models.CourseForCreationDto, API.Entities.Course>();

			//mapping courseForUpdateDto(sourse) to a course entity(Destination)
			CreateMap<Models.CourseForUpdateDto, API.Entities.Course>();
		}

	}
}
