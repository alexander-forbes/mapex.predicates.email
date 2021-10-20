using Moq;
using Notus;
using NUnit.Framework;

namespace Mapex.Predicates.Email.Tests
{
	[TestFixture]
	public class When_calling_matches_on_from_subject_attachment_where_specification
	{
		[Test]
		public void It_should_return_false_if_the_document_metadata_property_is_null()
		{
			var specification = new FromSubjectAttachmentWhereSpecification();

			var document = new Mock<IDocument>();

			Assert.IsFalse(specification.Matches(document.Object));
		}

		[Test]
		public void It_should_return_false_if_the_document_metadata_property_does_not_contain_a_subject_key()
		{
			var specification = new FromSubjectAttachmentWhereSpecification { From = "joe@soap.com", Attachment = "my-file.txt" };

			var document = new Mock<IDocument>();
			document.Setup(d => d.Metadata).Returns(new Metadata { { "From", "joe@soap.com" }, { "Attachment", "my-file.txt" } });

			Assert.IsFalse(specification.Matches(document.Object));
		}

		[Test]
		public void It_should_return_false_if_the_document_metadata_property_does_not_contain_a_from_key()
		{
			var specification = new FromSubjectAttachmentWhereSpecification { Subject = "subject", Attachment = "my-file.txt" };

			var document = new Mock<IDocument>();
			document.Setup(d => d.Metadata).Returns(new Metadata { { "Subject", "subject" }, { "Attachment", "my-file.txt" } });

			Assert.IsFalse(specification.Matches(document.Object));
		}

		[Test]
		public void It_should_return_false_if_the_document_metadata_property_does_not_contain_an_attachment_key()
		{
			var specification = new FromSubjectAttachmentWhereSpecification { From = "joe@soap.com", Subject = "subject" };

			var document = new Mock<IDocument>();
			document.Setup(d => d.Metadata).Returns(new Metadata { { "From", "joe@soap.com" }, { "Subject", "subject" } });

			Assert.IsFalse(specification.Matches(document.Object));
		}

		[Test]
		public void It_should_return_false_when_the_from_does_not_match_the_from_property_value()
		{
			var specification = new FromSubjectAttachmentWhereSpecification { From = "joe@soap.com", Subject = "subject", Attachment = "my-file.txt" };

			var document = new Mock<IDocument>();
			document.Setup(d => d.Metadata).Returns(new Metadata { { "From", "jane@doe.com" }, { "Subject", "subject" }, { "Attachment", "my-file.txt" } });

			Assert.IsFalse(specification.Matches(document.Object));
		}

		[Test]
		public void It_should_return_false_when_the_subject_does_not_match_the_subject_property_value()
		{
			var specification = new FromSubjectAttachmentWhereSpecification { From = "joe@soap.com", Subject = "subject", Attachment = "my-file.txt" };

			var document = new Mock<IDocument>();
			document.Setup(d => d.Metadata).Returns(new Metadata { { "From", "joe@soap.com" }, { "Subject", "other" }, { "Attachment", "my-file.txt" } });

			Assert.IsFalse(specification.Matches(document.Object));
		}

		[Test]
		public void It_should_return_false_when_the_attachment_does_not_match_the_attachment_property_value()
		{
			var specification = new FromSubjectAttachmentWhereSpecification { From = "joe@soap.com", Subject = "subject", Attachment = "my-file.txt" };

			var document = new Mock<IDocument>();
			document.Setup(d => d.Metadata).Returns(new Metadata { { "From", "joe@soap.com" }, { "Subject", "other" }, { "Attachment", "my-other-file.txt" } });

			Assert.IsFalse(specification.Matches(document.Object));
		}

		[Test]
		public void It_should_return_true_when_the_subject_and_from_and_attachment_match_the_subject_and_from_and_attachment_property_values()
		{
			var specification = new FromSubjectAttachmentWhereSpecification { From = "joe@soap.com", Subject = "subject", Attachment = "my-file.txt" };

			var document = new Mock<IDocument>();
			document.Setup(d => d.Metadata).Returns(new Metadata { { "From", "joe@soap.com" }, { "Subject", "subject" }, { "Attachment", "my-file.txt" } });

			Assert.IsTrue(specification.Matches(document.Object));
		}
	}

	[TestFixture]
	public class When_calling_validate_on_from_subject_attachment_where_specification
	{
		[Test]
		public void It_should_return_a_notification_error_when_no_from_value_is_specified()
		{
			var specification = new FromSubjectAttachmentWhereSpecification();
			var notification = new Notification();

			specification.Validate(notification);

			Assert.IsTrue(notification.IncludesError("From value has not been specified."));
		}

		[Test]
		public void It_should_return_a_notification_error_when_no_subject_value_is_specified()
		{
			var specification = new FromSubjectAttachmentWhereSpecification();
			var notification = new Notification();

			specification.Validate(notification);

			Assert.IsTrue(notification.IncludesError("Subject value has not been specified."));
		}

		[Test]
		public void It_should_return_a_notification_error_when_no_attachment_value_is_specified()
		{
			var specification = new FromSubjectAttachmentWhereSpecification();
			var notification = new Notification();

			specification.Validate(notification);

			Assert.IsTrue(notification.IncludesError("Attachment value has not been specified."));
		}

		[Test]
		public void It_should_not_add_a_notification_error_when_the_from_and_subject_and_attachment_has_been_specified()
		{
			var specification = new FromSubjectAttachmentWhereSpecification
			{
				From = "joe@soap.com",
				Subject = "Subject",
				Attachment = "my-file.txt"
			};

			var notification = new Notification();

			specification.Validate(notification);

			Assert.IsFalse(notification.HasErrors);
		}
	}
}
