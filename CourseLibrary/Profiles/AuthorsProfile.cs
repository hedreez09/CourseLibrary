using AutoMapper;
using CourseLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.Profiles
{
	public class AuthorsProfile :Profile
	{
		public AuthorsProfile()
		{
			//This help in Concatenating all the data tha need
			//to be concatenated by  using ForMember()
			CreateMap<API.Entities.Author, Model.AuthorDto>()
				.ForMember(
					dest => dest.Name,
					opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
				.ForMember(
				dest => dest.Age, //the GetCurrentAge is gotten from Helpers class
				opt => opt.MapFrom(src => src.DateOfBirth.GetCurrentAge()));
			
			//This help to map Dto with the model entity
			CreateMap<Models.AuthorForCreationDto, API.Entities.Author>(); 
		}
	}
}
