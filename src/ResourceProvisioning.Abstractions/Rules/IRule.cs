using System;
using ResourceProvisioning.Abstractions.Entities;

namespace ResourceProvisioning.Abstractions.Rules
{
	public interface IRule
	{
		void Execute<T>(T entity) where T : IEntity;

		Boolean IsMatch<T>(T entity) where T : IEntity;
	}
}
