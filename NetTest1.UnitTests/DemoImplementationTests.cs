
using FluentAssertions;
using NetTest1;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NetTest1.UnitTests
{

    public class DemoImplementationTests
    {
        private readonly DemoImplementation.Implementation _demoImplementation;

        public DemoImplementationTests()
        {
            _demoImplementation = new DemoImplementation.Implementation();
        }

        [Fact]
        public void AddPersonWithEmail_ShouldCreatePersonWithEmail_WhenAllParametersAreValid()
        {
            //Arrange
            string name = "Jamal";
            string id = "123321";

            string email = "jamal@jamal.com";
            string emailType = "open";

            var emailAddresses = new List<DemoSource.EmailAddress>()
            {
                new DemoSource.EmailAddress { Email = email, EmailType = emailType }
            };

            var person = new List<DemoSource.Person>()
            {
                new DemoSource.Person { Name = name, Id = id, Emails = emailAddresses }
            } as IEnumerable<DemoSource.Person>;

            var result = new List<DemoTarget.PersonWithEmail>()
            {
                new DemoTarget.PersonWithEmail {  FormattedEmail = $"{email}{emailType}", SanitizedNameWithId = $"{name}{id}" }
            };

            //Act
            var methodResult = DemoImplementation.Implementation.Flatten(person).ToList();

            //Assert
            methodResult.Should().BeEquivalentTo(result);
        }

        [Theory]
        [InlineData("", "", "", "")]
        [InlineData("zdd##", "&&@", "email.com", "sdada")]
        [InlineData("  ", "1233", "eeeme", "openo")]
        [InlineData("Jamal", "123", "&(*A", "dodo")]
        [InlineData("Jamal", "123", "jamal.jamal.jamal@compot.pl", "doda")]
        public void AddPersonWithEmail_ShouldNotCreatePersonWithEmail_WhenParametersAreInvalid(string name, string id, string email, string emailType)
        {

            //Arrange
            var emailAddresses = new List<DemoSource.EmailAddress>()
            {
                new DemoSource.EmailAddress { Email = email, EmailType = emailType }
            };

            var person = new List<DemoSource.Person>()
            {
                new DemoSource.Person { Name = name, Id = id, Emails = emailAddresses }
            } as IEnumerable<DemoSource.Person>;

            var result = new List<DemoTarget.PersonWithEmail>()
            {
                new DemoTarget.PersonWithEmail {  FormattedEmail = $"{email}{emailType}", SanitizedNameWithId = $"{name}{id}" }
            };

            //Act
            var methodResult = DemoImplementation.Implementation.Flatten(person).ToList();

            //Assert
            methodResult.Should().NotBeEquivalentTo(result);
        }

        [Fact]
        public void AddPeopleWithEmail_ShouldCreatePeopleWithEmail_WhenAllParametersAreValid()
        {
            //Arrange
            string name1 = "Jamal";
            string name2 = "Tomek";

            string id1 = "123321";
            string id2 = "321123";

            string email11 = "jamal@jamal.com";
            string email12 = "jamal@jamal.pl";
            string email21 = "tomek@tomek.com";
            string email22 = "tomek@tomek.pl";

            string emailType = "open";

            var emailAddresses1 = new List<DemoSource.EmailAddress>()
            {
                new DemoSource.EmailAddress { Email = email11, EmailType = emailType },
                new DemoSource.EmailAddress { Email = email12, EmailType = emailType }
            };

            var emailAddresses2 = new List<DemoSource.EmailAddress>()
            {
                new DemoSource.EmailAddress { Email = email21, EmailType = emailType },
                new DemoSource.EmailAddress { Email = email22, EmailType = emailType }
            };

            var person = new List<DemoSource.Person>()
            {
                new DemoSource.Person { Name = name1, Id = id1, Emails = emailAddresses1 },
                new DemoSource.Person { Name = name2, Id = id2, Emails = emailAddresses2 }
            } as IEnumerable<DemoSource.Person>;

            var result = new List<DemoTarget.PersonWithEmail>()
            {
                new DemoTarget.PersonWithEmail {  FormattedEmail = $"{email11}{emailType}", SanitizedNameWithId = $"{name1}{id1}" },
                new DemoTarget.PersonWithEmail {  FormattedEmail = $"{email12}{emailType}", SanitizedNameWithId = $"{name1}{id1}" },
                new DemoTarget.PersonWithEmail {  FormattedEmail = $"{email21}{emailType}", SanitizedNameWithId = $"{name2}{id2}" },
                new DemoTarget.PersonWithEmail {  FormattedEmail = $"{email22}{emailType}", SanitizedNameWithId = $"{name2}{id2}" }
            };

            //Act
            var methodResult = DemoImplementation.Implementation.Flatten(person).ToList();

            //Assert
            methodResult.Should().BeEquivalentTo(result);
        }

        [Theory]
        [InlineData("Jamal", "1223", "jamal@dada.pl", "jamal@dodo.pl", "",  "", "tomek.@wp.pl", "Toto@o2.pl", "emailType")]
        public void AddPeopleWithEmail_ShouldNotCreatePersonWithEmail_WhenParametersAreInvalid(string name1, string id1, string email11, string email12, string name2, string id2, string email21, string email22, string emailType)
        {
            var emailAddresses1 = new List<DemoSource.EmailAddress>()
            {
                new DemoSource.EmailAddress { Email = email11, EmailType = emailType },
                new DemoSource.EmailAddress { Email = email12, EmailType = emailType }
            };

            var emailAddresses2 = new List<DemoSource.EmailAddress>()
            {
                new DemoSource.EmailAddress { Email = email21, EmailType = emailType },
                new DemoSource.EmailAddress { Email = email22, EmailType = emailType }
            };

            var person = new List<DemoSource.Person>()
            {
                new DemoSource.Person { Name = name1, Id = id1, Emails = emailAddresses1 },
                new DemoSource.Person { Name = name2, Id = id2, Emails = emailAddresses2 }
            } as IEnumerable<DemoSource.Person>;

            var result = new List<DemoTarget.PersonWithEmail>()
            {
                new DemoTarget.PersonWithEmail {  FormattedEmail = $"{email11}{emailType}", SanitizedNameWithId = $"{name1}{id1}" },
                new DemoTarget.PersonWithEmail {  FormattedEmail = $"{email12}{emailType}", SanitizedNameWithId = $"{name1}{id1}" },
                new DemoTarget.PersonWithEmail {  FormattedEmail = $"{email21}{emailType}", SanitizedNameWithId = $"{name2}{id2}" },
                new DemoTarget.PersonWithEmail {  FormattedEmail = $"{email22}{emailType}", SanitizedNameWithId = $"{name2}{id2}" }
            };

            //Act
            var methodResult = DemoImplementation.Implementation.Flatten(person).ToList();

            //Assert
            methodResult.Should().NotBeEquivalentTo(result);
        }
    }
}
