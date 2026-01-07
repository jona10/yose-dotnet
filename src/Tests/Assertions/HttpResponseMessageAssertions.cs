using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using System.Net;
using System.Net.Http;

namespace Tests.Assertions
{
    public class HttpResponseMessageAssertions : ReferenceTypeAssertions<HttpResponseMessage, HttpResponseMessageAssertions>
    {
        public HttpResponseMessageAssertions(HttpResponseMessage response) : base(response)
        {
        }

        protected override string Identifier => "htmlnode";

        public AndConstraint<HttpResponseMessageAssertions> BeOk(string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .ForCondition(Subject.StatusCode == HttpStatusCode.OK)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:" + Identifier + "} to be OK{reason}, but was {0}.", Subject.StatusCode);

            return new AndConstraint<HttpResponseMessageAssertions>(this);
        }
    }
}
