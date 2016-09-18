using HtmlAgilityPack;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Tests.Assertions
{
    public static partial class AssertionExtensions
    {
        public static HtmlNodeAssertions Should(this HtmlNode node)
        {
            return new HtmlNodeAssertions(node);
        }

        public static HtmlAttributeAssertions Should(this HtmlAttribute node)
        {
            return new HtmlAttributeAssertions(node);
        }

        public static HttpResponseMessageAssertions Should(this HttpResponseMessage message)
        {
            return new HttpResponseMessageAssertions(message);
        }

        public static HttpContentAssertions Should(this HttpContent content)
        {
            return new HttpContentAssertions(content);
        }

        public static HttpContentHeadersAssertions Should(this HttpContentHeaders headers)
        {
            return new HttpContentHeadersAssertions(headers);
        }
    }
}
