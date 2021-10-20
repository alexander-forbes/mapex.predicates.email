using Moq;
using Notus;
using NUnit.Framework;

namespace Mapex.Predicates.Email.Tests
{
	[TestFixture]
	public class When_calling_matches_on_from_where_specification
	{
		[Test]
		public void It_should_return_false_when_the_metadata_property_is_null()
		{
			var document = new Mock<IDocument>();
			var specification = new FromWhereSpecification();

			Assert.IsFalse(specification.Matches(document.Object));
		}

		[Test]
		public void It_should_return_false_when_the_metadata_does_not_contain_a_from_key()
		{
			var document = new Mock<IDocument>();
			document.Setup(d => d.Metadata).Returns(new Metadata());

			var specification = new FromWhereSpecification();

			Assert.IsFalse(specification.Matches(document.Object));
		}

		[Test]
		public void It_should_return_true_when_the_metadata_from_value_matches_the_from_property_value()
		{
			var document = new Mock<IDocument>();

			document.Setup(d => d.Metadata).Returns(new Metadata
			{
				{"From", "joe@soap.com"}
			});

			var specification = new FromWhereSpecification
			{
				From = "[a-z]@soap.com"
			};

			Assert.IsTrue(specification.Matches(document.Object));
		}

		[Test]
		public void It_should_return_false_when_the_metadata_from_value_does_not_match_the_from_property_value()
		{
			var document = new Mock<IDocument>();

			document.Setup(d => d.Metadata).Returns(new Metadata
			{
				{"From", "joe@soap.com"}
			});

			var specification = new FromWhereSpecification
			{
				From = "jane@doe.com"
			};

			Assert.IsFalse(specification.Matches(document.Object));
		}
	}

	[TestFixture]
	public class When_calling_validate_on_from_where_specification
	{
		[Test]
		public void It_should_return_a_notification_error_when_no_from_value_is_specified()
		{
			var specification = new FromWhereSpecification();
			var notification = new Notification();

			specification.Validate(notification);

			Assert.IsTrue(notification.IncludesError("From value has not been specified."));
		}

		[Test]
		public void It_should_not_return_a_notification_error_when_a_from_value_is_specified()
		{
			var specification = new FromWhereSpecification { From = "joe@soap.com" };
			var notification = new Notification();

			specification.Validate(notification);

			Assert.IsFalse(notification.HasErrors);
		}
	}
}
