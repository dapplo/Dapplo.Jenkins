using Dapplo.Log.XUnit;
using Dapplo.Log;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Dapplo.Jenkins.Tests
{
	public class JenkinsTests
	{
		private static readonly LogSource Log = new LogSource();
		private readonly JenkinsApi _jenkinsApi;

		public JenkinsTests(ITestOutputHelper testOutputHelper)
		{
			LogSettings.RegisterDefaultLogger<XUnitLogger>(LogLevels.Verbose, testOutputHelper);
			var testUri = new Uri(Environment.GetEnvironmentVariable("jenkins_test_uri"));
			_jenkinsApi = new JenkinsApi(testUri);
			var username = Environment.GetEnvironmentVariable("jenkins_test_username");
			var apiKey = Environment.GetEnvironmentVariable("jenkins_test_apikey");
			if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(apiKey))
			{
				_jenkinsApi.SetBasicAuthentication(username, apiKey);
			}
			_jenkinsApi.JenkinsHttpSettings.IgnoreSslCertificateErrors = true;
		}

		[Fact]
		public async Task TestOverview()
		{
			await _jenkinsApi.InitializeAsync();

			var overview = await _jenkinsApi.GetOverviewAsync();
			Assert.NotNull(overview);
			Assert.True(overview.Jobs.Count > 0);
			foreach (var job in overview.Jobs)
			{
				var details = await _jenkinsApi.GetJobDetailsAsync(job.Name);
				Assert.NotNull(details.Description);
				Log.Info().WriteLine("{0} - {1}", job.Name, details.Description);
				// One is enough for testing
				break;
			}
		}
	}
}
