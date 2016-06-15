using Dapplo.HttpExtensions;
using Dapplo.Jenkins.Entities;
using Dapplo.LogFacade;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dapplo.Jenkins
{
	/// <summary>
	///     Jenkins API, using Dapplo.HttpExtensions
	/// </summary>
	public class JenkinsApi
	{
		private static readonly LogSource Log = new LogSource();

		/// <summary>
		///     Store the specific HttpBehaviour
		///     This contains a IHttpSettings and also some additional logic for making a HttpClient which works with Jenkins
		/// </summary>
		private readonly IHttpBehaviour _behaviour;

		private CrumbIssuer _crumbIssuer;

		private string _user;
		private string _apiToken;

		/// <summary>
		/// The IHttpSettings used for every request, made available so they can be modified
		/// </summary>
		public IHttpSettings JenkinsHttpSettings { get; private set; }

		/// <summary>
		///     Create the JenkinsApi object, here the HttpClient is configured
		/// </summary>
		/// <param name="baseUri">Base URL, e.g. https://yourjenkinsserver</param>
		/// <param name="httpSettings">IHttpSettings or null for default</param>
		public JenkinsApi(Uri baseUri, IHttpSettings httpSettings = null)
		{
			if (baseUri == null)
			{
				throw new ArgumentNullException(nameof(baseUri));
			}
			JenkinsBaseUri = baseUri;

			JenkinsHttpSettings = httpSettings ?? HttpExtensionsGlobals.HttpSettings.ShallowClone();
			JenkinsHttpSettings.PreAuthenticate = true;

			_behaviour = new HttpBehaviour
			{
				HttpSettings = JenkinsHttpSettings,
				OnHttpRequestMessageCreated = httpMessage =>
				{
					if (_crumbIssuer != null)
					{
						httpMessage?.Headers.TryAddWithoutValidation(_crumbIssuer.CrumbRequestField, _crumbIssuer.Crumb);
					}
					if (!string.IsNullOrEmpty(_user) && _apiToken != null)
					{
						httpMessage?.SetBasicAuthorization(_user, _apiToken);
					}
					return httpMessage;
				}
			};
		}


		/// <summary>
		///     The base URI for your JIRA server
		/// </summary>
		public Uri JenkinsBaseUri { get; }

		/// <summary>
		///     Set Basic Authentication for the current client
		/// </summary>
		/// <param name="user">username</param>
		/// <param name="apiToken">the API token</param>
		public void SetBasicAuthentication(string user, string apiToken)
		{
			_user = user;
			_apiToken = apiToken;
		}


		/// <summary>
		/// This retrieves the crumbIssuer, which makes it possible to start calling the API
		/// </summary>
		/// <param name="cancellationToken">CancellationToken</param>
		public async Task InitializeAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (_crumbIssuer == null)
			{
				try {
					_behaviour.MakeCurrent();
					_crumbIssuer = await JenkinsBaseUri.AppendSegments("crumbIssuer", "api", "json").GetAsAsync<CrumbIssuer>(cancellationToken).ConfigureAwait(false);
					Log.Info().WriteLine("Using crumb header for XSS prevention.");
				}
				catch (Exception ex)
				{
					// No crumbIssuer available, this is probably not active, so skip it
					Log.Info().WriteLine("CrumbIssuer not found, probably not active (this might be okay). Error was: " + ex.Message);
				}
			}
		}

		/// <summary>
		/// Retrieve the Jenkins overview (jobs etc)
		/// </summary>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>ProjectList</returns>
		public async Task<Overview> GetOverviewAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			_behaviour.MakeCurrent();
			return await JenkinsBaseUri.AppendSegments("api", "json").GetAsAsync<Overview>(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Delete the Jenkins job
		/// </summary>
		/// <param name="jobName">Name of the job to delete</param>
		/// <param name="cancellationToken">CancellationToken</param>
		public async Task DeleteJobAsync(string jobName, CancellationToken cancellationToken = default(CancellationToken))
		{
			_behaviour.MakeCurrent();
			await JenkinsBaseUri.AppendSegments("job", jobName, "doDelete").PostAsync(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Delete the Jenkins View
		/// </summary>
		/// <param name="viewName">Name of the job to delete</param>
		/// <param name="cancellationToken">CancellationToken</param>
		public async Task DeleteViewAsync(string viewName, CancellationToken cancellationToken = default(CancellationToken))
		{
			_behaviour.MakeCurrent();
			await JenkinsBaseUri.AppendSegments("view", viewName, "doDelete").PostAsync(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Retrieve the config.xml for a job
		/// </summary>
		/// <param name="jobName">Name of the job to get the config.xml for</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>config.xml</returns>
		public async Task<XDocument> GetJobXMLAsync(string jobName, CancellationToken cancellationToken = default(CancellationToken))
		{
			_behaviour.MakeCurrent();
			return await JenkinsBaseUri.AppendSegments("job", jobName, "config.xml").GetAsAsync<XDocument>(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Retrieve the config.xml for a view
		/// </summary>
		/// <param name="viewName">Name of the view to retrieve the config.xml for</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>config.xml</returns>
		public async Task<XDocument> GetViewXMLAsync(string viewName, CancellationToken cancellationToken = default(CancellationToken))
		{
			_behaviour.MakeCurrent();
			return await JenkinsBaseUri.AppendSegments("view", viewName, "config.xml").GetAsAsync<XDocument>(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Post the config.xml to create a new job
		/// </summary>
		/// <param name="jobName">Name of the job to create</param>
		/// <param name="xml">XDocument with config.xml for the job</param>
		/// <param name="cancellationToken">CancellationToken</param>
		public async Task CreateJobAsync(string jobName, XDocument xml, CancellationToken cancellationToken = default(CancellationToken))
		{
			_behaviour.MakeCurrent();
			await JenkinsBaseUri.AppendSegments("createItem").ExtendQuery("name", jobName).PostAsync(xml, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Post the config.xml to create a new job
		/// </summary>
		/// <param name="viewName">Name of the view to create</param>
		/// <param name="xml">XDocument with config.xml for the view</param>
		/// <param name="cancellationToken">CancellationToken</param>
		public async Task CreateViewAsync(string viewName, XDocument xml, CancellationToken cancellationToken = default(CancellationToken))
		{
			_behaviour.MakeCurrent();
			await JenkinsBaseUri.AppendSegments("createView").ExtendQuery("name", viewName).PostAsync(xml, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Retrieve the details for a job 
		/// </summary>
		/// <param name="jobName">Name of the job</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>JobDetails</returns>
		public async Task<JobDetails> GetJobDetailsAsync(string jobName, CancellationToken cancellationToken = default(CancellationToken))
		{
			_behaviour.MakeCurrent();
			return await JenkinsBaseUri.AppendSegments("job", jobName, "api","json").GetAsAsync<JobDetails>(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Retrieve the details for a view
		/// </summary>
		/// <param name="viewName">Name of the view to get the details for</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>ViewDetails</returns>
		public async Task<ViewDetails> GetViewDetailsAsync(string viewName, CancellationToken cancellationToken = default(CancellationToken))
		{
			_behaviour.MakeCurrent();
			return await JenkinsBaseUri.AppendSegments("view", viewName, "api", "json").GetAsAsync<ViewDetails>(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Enable a job
		/// </summary>
		/// <param name="jobName">Name of the job to enable</param>
		/// <param name="cancellationToken">CancellationToken</param>
		public async Task EnableJobAsync(string jobName, CancellationToken cancellationToken = default(CancellationToken))
		{
			_behaviour.MakeCurrent();
			await JenkinsBaseUri.AppendSegments("job", jobName, "enable").PostAsync(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Diable a job
		/// </summary>
		/// <param name="jobName">Name of the job to disable</param>
		/// <param name="cancellationToken">CancellationToken</param>
		public async Task DisableJobAsync(string jobName, CancellationToken cancellationToken = default(CancellationToken))
		{
			_behaviour.MakeCurrent();
			await JenkinsBaseUri.AppendSegments("job", jobName, "disable").PostAsync(cancellationToken).ConfigureAwait(false);
		}
	}
}
