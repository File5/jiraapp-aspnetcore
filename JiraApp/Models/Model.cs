using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JiraApp.Models
{
	using Microsoft.EntityFrameworkCore;

	public class JiraContext : DbContext
	{
		public JiraContext(DbContextOptions<JiraContext> dbContext)
			: base(dbContext) { }

		public DbSet<Role> Roles { get; set; }
		public DbSet<Person> Persons { get; set; }
		public DbSet<Status> Statuses { get; set; }
		public DbSet<Project> Projects { get; set; }
		public DbSet<Epic> Epics { get; set; }
		public DbSet<Task> Tasks { get; set; }
	}

	public class Role
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Salary { get; set; }
	}
	public class Person
	{
		public int Id { get; set; }
		public string Pass { get; set; }
		public string FullName { get; set; }
		public DateTime DateOfBirth { get; set; }

		public int RoleId { get; set; }
		public Role Role { get; set; }
	}
	public class Status
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}
	public class Project
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		public int PersonId { get; set; }
		public Person Person { get; set; }

		public ICollection<Epic> Epics { get; set; }
		public ICollection<Task> Tasks { get; set; }
	}
	public class Epic
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public int ProjectId { get; set; }
		public Project Project { get; set; }

		public ICollection<Task> Tasks { get; set; }
	}
	public class Task
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime ClosedAt { get; set; }

		public int? EpicId { get; set; }
		public Epic Epic { get; set; }

		public int ProjectId { get; set; }
		public Project Project { get; set; }

		public int StatusId { get; set; }
		public Status Status { get; set; }

		public int? PersonId { get; set; }
		public Person Person { get; set; }
	}
	//*/
}
