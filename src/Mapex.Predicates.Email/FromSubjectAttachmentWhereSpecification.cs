using System.Text.RegularExpressions;

using Notus;

namespace Mapex.Predicates.Email
{
	public class FromSubjectAttachmentWhereSpecification : FromSubjectWhereSpecification
	{
		public string Attachment { get; set; }

		public override bool Matches(IDocument document)
		{
			return base.Matches(document) &&
				   document.Metadata.ContainsKey("Attachment") &&
				   new Regex(Attachment).IsMatch(document.Metadata["Attachment"]);
		}

		public override void Validate(Notification notification)
		{
			base.Validate(notification);

			if (string.IsNullOrEmpty(Attachment))
				notification.AddError("Attachment value has not been specified.");
		}
	}
}
