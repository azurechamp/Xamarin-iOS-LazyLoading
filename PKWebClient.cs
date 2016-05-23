using System;
using System.Net;

namespace Lazy
{
	public class PKWebClient:WebClient
	{
		protected override WebRequest GetWebRequest (Uri address)
		{
			var request = base.GetWebRequest (address);
			if (request is HttpWebRequest)
				((HttpWebRequest) request).AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
			return request;
		}
	}
}

