﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using ResourceProvisioning.Cli.Application.Models;
using ResourceProvisioning.Cli.Domain.Services;
using ResourceProvisioning.Cli.Infrastructure.Repositories;

namespace ResourceProvisioning.Cli.Application.Commands
{
	[Command(Description = "Applies desired state to the given environment")]
	public sealed class Apply : CliCommand<Task<int>>
	{
		private readonly IBrokerService _broker;

		[Argument(0)]
		public string DesiredStateSource
		{
			get;
			set;
		}

		public Apply(IBrokerService broker)
		{
			_broker = broker;
		}

		public override async Task<int> OnExecuteAsync(CancellationToken cancellationToken = default)
		{
			try
			{
				foreach (var desiredState in await GetDesiredStateData())
				{
					await _broker.ApplyDesiredStateAsync(Guid.Parse(EnvironmentId), desiredState, cancellationToken);
				}
			}
			catch (Exception e)
			{
				return await Task.FromResult(-1);
			}

			return await Task.FromResult(0);
		}

		private async Task<IEnumerable<DesiredState>> GetDesiredStateData()
		{
			if (IsValidJson(DesiredStateSource))
			{
				return new[] { JsonSerializer.Deserialize<DesiredState>(DesiredStateSource) };
			}

			if (!Path.IsPathFullyQualified(DesiredStateSource))
			{
				DesiredStateSource = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, DesiredStateSource));
			}

			if (!Directory.Exists(DesiredStateSource))
			{
				return File.Exists(DesiredStateSource)
					? new[] { JsonSerializer.Deserialize<DesiredState>(File.ReadAllText(DesiredStateSource)) }
					: null;
			}

			var manifestRepository = new ManifestRepository<DesiredState>(new DirectoryInfo(DesiredStateSource));

			return await manifestRepository.GetDesiredStatesByIdAsync(Guid.Parse(EnvironmentId));
		}

		private static bool IsValidJson(string json)
		{
			try
			{
				JsonDocument.Parse(json);
			}
			catch (JsonException e)
			{
				return false;
			}

			return true;
		}
	}
}
