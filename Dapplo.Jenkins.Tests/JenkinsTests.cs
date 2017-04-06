#region Dapplo 2017 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2016-2017 Dapplo
// 
// For more information see: http://dapplo.net/
// Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
// This file is part of Dapplo.Jenkins
// 
// Dapplo.Jenkins is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Dapplo.Jenkins is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have a copy of the GNU Lesser General Public License
// along with Dapplo.Jenkins. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#endregion

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
        private readonly IJenkinsClient _jenkinsApi;
        private readonly bool _hasCredentials;

        public JenkinsTests(ITestOutputHelper testOutputHelper)
        {
            LogSettings.RegisterDefaultLogger<XUnitLogger>(LogLevels.Verbose, testOutputHelper);
            var testUriString = Environment.GetEnvironmentVariable("jenkins_test_uri");
            if (string.IsNullOrEmpty(testUriString))
            {
                throw  new ArgumentNullException("jenkins_test_uri");
            }
            var testUri = new Uri(testUriString);
            _jenkinsApi = JenkinsClient.Create(testUri);
            var username = Environment.GetEnvironmentVariable("jenkins_test_username");
            var apiKey = Environment.GetEnvironmentVariable("jenkins_test_apikey");
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(apiKey))
            {
                _hasCredentials = true;
                _jenkinsApi.SetBasicAuthentication(username, apiKey);
            }
            else
            {
                _hasCredentials = false;
            }
            _jenkinsApi.Settings.IgnoreSslCertificateErrors = true;
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
                var details = await _jenkinsApi.Job.GetAsync(job.Name);
                Assert.NotNull(details.Description);
                Log.Info().WriteLine("{0} - {1}", job.Name, details.Description);
                if (_hasCredentials)
                {
                    var xml = await _jenkinsApi.Job.GetXmlAsync(job.Name);
                    Assert.NotNull(xml);
                }
                // One is enough for testing
                break;
            }

            Assert.True(overview.Views.Count > 0);
            foreach (var view in overview.Views)
            {
                var details = await _jenkinsApi.View.GetAsync(view.Name);
                Assert.NotNull(details.Name);
                Log.Info().WriteLine("{0}", details.Name);
                if (_hasCredentials)
                {
                    var xml = await _jenkinsApi.View.GetXmlAsync(view.Name);
                    Assert.NotNull(xml);
                }
                // One is enough for testing
                break;
            }
        }
    }
}
