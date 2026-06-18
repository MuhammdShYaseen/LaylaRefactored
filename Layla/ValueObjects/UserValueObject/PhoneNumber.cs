using Layla.DomainEvents.Domain.Common;
using Layla.DomainEvents.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Layla.ValueObjects.UserValueObject
{
    public class PhoneNumber : ValueObject
    {
        private static readonly Regex PhoneRegex = new(@"^\+[1-9]\d{7,14}$", RegexOptions.Compiled);

        public string Value { get; }

        private PhoneNumber(string value)
        {
            Value = value;
        }

        public static PhoneNumber Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainValidationException("Phone number is required.");

            value = value.Trim();

            if (!PhoneRegex.IsMatch(value))
                throw new DomainValidationException("Invalid phone number format.");

            return new PhoneNumber(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
}
