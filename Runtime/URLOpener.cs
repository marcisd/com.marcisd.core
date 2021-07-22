using System;
using UnityEngine;

/*===============================================================
Project:	MSD - Core
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		25/07/2019 14:41
===============================================================*/

namespace MSD
{
	[CreateAssetMenu(menuName = "MSD/Core/URL Opener", order = 500)]
	public class URLOpener : ScriptableObject
	{
		internal const string LOG_ERROR_INVALIDURL = "Not a valid URL.";
		internal const string LOG_WARNING_NOTSECUREURL = "Not a secure URL.";

		[TextArea]
		[SerializeField]
		private string _urlString = string.Empty;

		public bool IsValidURL => Uri != null;
		public bool IsSecure => IsValidURL && Uri?.Scheme == Uri.UriSchemeHttps;

		private Uri _uri;
		public Uri Uri {
			get {
				if (_uri?.AbsoluteUri == _urlString) { return _uri; }

				bool isUriCreationSucccess = Uri.TryCreate(_urlString, UriKind.Absolute, out _uri);
				if (!isUriCreationSucccess) { Debugger.LogError(LOG_ERROR_INVALIDURL); }
				return _uri;
			}
		}
		
		public void Open()
		{
			if (IsValidURL) { Application.OpenURL(Uri.AbsoluteUri); }
		}
	}
}
