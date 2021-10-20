using System.Text.RegularExpressions;
using Notus;

namespace Mapex.Predicates.Email
{
	public class FromSubjectWhereSpecification : FromWhereSpecification
	{
		public string Subject { get; set; }

		public override bool Matches(IDocument document)
		{
			return base.Matches(document) &&
				document.Metadata.ContainsKey("Subject") &&
				   new Regex(Subject).IsMatch(document.Metadata["Subject"]);
		}

		public override void Validate(Notification notification)
		{
			base.Validate(notification);

			if (string.IsNullOrEmpty(Subject))
				notification.AddError("Subject value has not been specified.");
		}
	}
}
