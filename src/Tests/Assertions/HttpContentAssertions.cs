using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using System.Linq;
using System.Net.Http;

namespace Tests.Assertions
{
    public class HttpContentAssertions : ReferenceTypeAssertions<HttpContent, HttpContentAssertions>
    {
        public HttpContentAssertions(HttpContent content)
        {
            Subject = content;
        }

        protected override string Identifier => "htmlnode";

        public AndConstraint<HttpContentAssertions> Be(string body, string because = "", params object[] becauseArgs)
        {
            var actualBody = Subject.ReadAsStringAsync().Result;
            Execute.Assertion
                .ForCondition(body.Equals(actualBody))
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:" + Context + "} to be {0}{reason}, but was {1}.", body, actualBody);

            return new AndConstraint<HttpContentAssertions>(this);
        }

        public AndConstraint<HttpContentAssertions> Contain(string body, string because = "", params object[] becauseArgs)
        {
            var actualBody = Subject.ReadAsStringAsync().Result;
            Execute.Assertion
                .ForCondition(actualBody.Contains(body))
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:" + Context + "} to contain {0}{reason}, but body was {1}.", body, actualBody);

            return new AndConstraint<HttpContentAssertions>(this);
        }
    }
}
