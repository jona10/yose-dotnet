using System;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using HtmlAgilityPack;
using System.Linq;

namespace Tests.Assertions
{
    public class HtmlNodeAssertions: ReferenceTypeAssertions<HtmlNode, HtmlNodeAssertions>
    {
        public HtmlNodeAssertions(HtmlNode subject)
        {
            Subject = subject;
        }

        protected override string Context { get; } = typeof(HtmlNode).Name;

        public AndWhichConstraint<HtmlNodeAssertions, HtmlNode> BeAnAnchor(string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .ForCondition(Subject.Name.Equals("a"))
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:" + Context + "} to be an anchor{reason}, but was {0}.", Subject.Name);

            return new AndWhichConstraint<HtmlNodeAssertions, HtmlNode>(this, Subject);
        }

        public AndWhichConstraint<HtmlAttributeAssertions, HtmlAttribute> HaveAttribute(string name, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .ForCondition(Subject.Attributes.Contains(name))
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:" + Context + "} to contain an attribute named {0}{reason}, but none was found.", name);

            var attribute = Subject.Attributes[name];

            return new AndWhichConstraint<HtmlAttributeAssertions, HtmlAttribute>(new HtmlAttributeAssertions(attribute), attribute);
        }
    }

    public class HtmlAttributeAssertions : ReferenceTypeAssertions<HtmlAttribute, HtmlAttributeAssertions>
    {
        public HtmlAttributeAssertions(HtmlAttribute subject)
        {
            Subject = subject;
        }

        protected override string Context { get; } = typeof(HtmlAttribute).Name;

        public AndConstraint<HtmlAttributeAssertions> HaveValue(string value, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .ForCondition(Subject.Value.Equals(value))
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:" + Context + "} {0} to have a value of {1}{reason}, but value was {2}.", Subject.Name, value, Subject.Value);

            return new AndConstraint<HtmlAttributeAssertions>(this);
        }
    }
}
