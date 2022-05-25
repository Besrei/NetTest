
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
        [InlineData("Jamal", "123", "jamal.jamal.jamal@compot.pl","doda")]
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

    }
}
