using System.IdentityModel.Tokens;
using System.IO;
using System.Text;
using System.Xml;

namespace IdentityServer3.Saml2Bearer
{
    public interface ISaml2AssertionSerializer
    {
        string ToXml(Saml2Assertion assertion);
    }

    public class Saml2AssertionSerializer : ISaml2AssertionSerializer
    {
        public string ToXml(Saml2Assertion assertion)
        {
            var tokenHandlers = new Saml2SecurityTokenHandler();
            var stringBuilder = new StringBuilder();
            using (var xmlWriter = new XmlTextWriter(new StringWriter(stringBuilder)))
            {
                var token = new Saml2SecurityToken(assertion);
                tokenHandlers.WriteToken(xmlWriter, token);

                return stringBuilder.ToString();
            }
        }
    }

    public interface ISaml2AssertionFactory
    {
        Saml2SecurityToken ToSecurityToken(string xml);
        Saml2SecurityTokenHandler TokenHandler { get; }
    }
}