using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JiraApp
{
	using Ninject.Modules;
	using JiraApp.Models;
	public class NinjectRegistrations : NinjectModule
	{
		public override void Load()
		{
			Bind<JiraContext>().ToSelf().InSingletonScope();
		}
	}
}
