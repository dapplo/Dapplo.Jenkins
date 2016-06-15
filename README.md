# Dapplo.Jenkins
This is a simple REST based Jenkins client, by using Dapplo.HttpExtension

- Current build status: [![Build status](https://ci.appveyor.com/api/projects/status/p4cp8wq6ldit31cy?svg=true)](https://ci.appveyor.com/project/dapplo/dapplo-jenkins)
- Coverage Status: [![Coverage Status](https://coveralls.io/repos/github/dapplo/Dapplo.Jenkins/badge.svg?branch=master)](https://coveralls.io/github/dapplo/Dapplo.Jenkins?branch=master)
- NuGet package: [![NuGet package](https://badge.fury.io/nu/dapplo.jenkins.svg)](https://badge.fury.io/nu/dapplo.jenkins)

Example usage:
var jenkinsApi = new JenkinsApi(new Uri("your jenkins url"));
jenkinsApi.SetBasicAuthentication("username", "apiKey");
// Needed if crumbIssuer is used
await _jenkinsApi.InitializeAsync();
var overview = await _jenkinsApi.GetOverviewAsync();