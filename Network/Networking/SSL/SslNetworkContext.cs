using System;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace NetExtender.Network.Networking.SSL
{
    /// <summary>
    ///     SSL context
    /// </summary>
    public class SslNetworkContext
    {
        /// <summary>
        ///     Initialize SSL context with default protocols
        /// </summary>
        public SslNetworkContext()
            : this(SslProtocols.Tls12)
        {
        }

        /// <summary>
        ///     Initialize SSL context with given protocols
        /// </summary>
        /// <param name="protocols">SSL protocols</param>
        public SslNetworkContext(SslProtocols protocols)
        {
            Protocols = protocols;
        }

        /// <summary>
        ///     Initialize SSL context with given protocols, certificate and validation callback
        /// </summary>
        /// <param name="protocols">SSL protocols</param>
        /// <param name="certificate">SSL certificate</param>
        /// <param name="certificateValidationCallback">SSL certificate</param>
        public SslNetworkContext(SslProtocols protocols, X509Certificate certificate, RemoteCertificateValidationCallback certificateValidationCallback = null)
        {
            Protocols = protocols;
            Certificate = certificate;
            CertificateValidationCallback = certificateValidationCallback;
        }

        /// <summary>
        ///     Initialize SSL context with given protocols, certificates collection and validation callback
        /// </summary>
        /// <param name="protocols">SSL protocols</param>
        /// <param name="certificates">SSL certificates collection</param>
        /// <param name="certificateValidationCallback">SSL certificate</param>
        public SslNetworkContext(SslProtocols protocols, X509Certificate2Collection certificates, RemoteCertificateValidationCallback certificateValidationCallback = null)
        {
            Protocols = protocols;
            Certificates = certificates;
            CertificateValidationCallback = certificateValidationCallback;
        }

        /// <summary>
        ///     SSL protocols
        /// </summary>
        public SslProtocols Protocols { get; set; }

        /// <summary>
        ///     SSL certificate
        /// </summary>
        public X509Certificate Certificate { get; set; }

        /// <summary>
        ///     SSL certificates collection
        /// </summary>
        public X509Certificate2Collection Certificates { get; set; }

        /// <summary>
        ///     SSL certificate validation callback
        /// </summary>
        public RemoteCertificateValidationCallback CertificateValidationCallback { get; set; }

        /// <summary>
        ///     Is the client is asked for a certificate for authentication.
        ///     Note that this is only a request - if no certificate is provided, the server still accepts the connection request.
        /// </summary>
        public Boolean ClientCertificateRequired { get; set; }
    }
}