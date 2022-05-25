using System.Collections.Generic;

namespace DemoSource
{
    public class Person
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<EmailAddress> Emails { get; set; }
    }

    public class EmailAddress
    {
        public string Email { get; set; }

        public string EmailType { get; set; }
    }
}
