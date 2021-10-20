using System;
using System.Text.RegularExpressions;
using Mapex.Specifications;
using Notus;

namespace Mapex.Predicates.Email
{
	public class FromWhereSpecification : IWhereSpecification
	{
		public string From { get; set; }

		public virtual bool Matches(IDocument document)
		{
			if (document == null)
				throw new ArgumentNullException(nameof(document));

			return document.Metadata != null && 
			       document.Metadata.ContainsKey("From") && 
			       new Regex(From).IsMatch(document.Metadata["From"]);
		}

		public virtual void Validate(Notification notification)
		{
			if (string.IsNullOrEmpty(From))
				notification.AddError("From value has not been specified.");
		}
	}
}
