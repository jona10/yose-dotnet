using HtmlAgilityPack;

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
    }
}
