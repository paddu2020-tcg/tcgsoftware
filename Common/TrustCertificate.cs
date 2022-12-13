using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace YamahaApp.Common
{
	/// <summary>
	/// Summary description for TrustCertificate.
	/// </summary>
	public class TrustAllCertificatePolicy: System.Net.ICertificatePolicy
	{
		public TrustAllCertificatePolicy()
		{
		}

		public bool CheckValidationResult(ServicePoint sp,
			X509Certificate cert,WebRequest req, int problem)
		{
			return true;
		}

	} 
}
