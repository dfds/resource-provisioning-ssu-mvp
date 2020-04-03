using System;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Entities;

namespace ResourceProvisioning.Abstractions.Rules
{
	interface IRule
	{
		Task Execute<T>(T entity) where T : IEntity;

		Boolean IsMatch<T>(T entity) where T : IEntity;
	}
}
