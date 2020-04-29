using CourseLibrary.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.Models
{
	[CourseTitleMustBeDifferentFromDescriptionAttibute(
		ErrorMessage = "Title must be different from description")]
	public abstract class CourseForManipulationDto
	{
		[Required(ErrorMessage = "You should fill out a title.")]
		[MaxLength(100, ErrorMessage = "The title shouldn't be more than 100 characters")]
		public string Title { get; set; }

		[MaxLength(1500, ErrorMessage = "The description shouldn't be more that 1500 character")]
		public virtual string Description { get; set; }
	}
}
