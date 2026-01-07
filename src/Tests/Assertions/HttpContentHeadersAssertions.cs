using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using System.Linq;
using System.Net.Http.Headers;

namespace Tests.Assertions
{
    public class HttpContentHeadersAssertions : ReferenceTypeAssertions<HttpContentHeaders, HttpContentHeadersAssertions>
    {
        public HttpContentHeadersAssertions(HttpContentHeaders headers)
        {
            Subject = headers;
        }

        protected override string Identifier { get; } = "htmlnode";

        public AndConstraint<StringAssertions> Contain(string header, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .ForCondition(Subject.Contains(header))
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:" + Context + "} to have header {0}{reason}, but none was found.", header);

            var headerContent = Subject.GetValues(header).First();

            return new AndConstraint<StringAssertions>(new StringAssertions(headerContent));
        }
    }
}
