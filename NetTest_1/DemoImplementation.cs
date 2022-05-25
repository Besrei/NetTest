using DemoTarget;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DemoImplementation
{
    public class Implementation
    {
        /*Komentarz do zadania: Problemem w takim mapowaniu może być to, gdy każda z osób będzie miała ogromną ilość maili,
         * Zostaniemy z tablelą pełną rekordów mająca wszystkie maile przypisane do pojedynczych imion.
         */
        public static IEnumerable<DemoTarget.PersonWithEmail> Flatten(IEnumerable<DemoSource.Person> people)
        {
            var result = new List<DemoTarget.PersonWithEmail>();

            foreach (var person in people)
            {
                AddPersonWithEmail(result, person);
            }

            return result;
        }

        private static void AddPersonWithEmail(List<PersonWithEmail> result, DemoSource.Person person)
        {
            Regex pattern = new Regex("[0-9a-zA-Z]*");
            var sanitizedName = pattern.Matches(person.Name).Single();
            var sanitizedId = pattern.Matches(person.Id).Single();

            if (sanitizedId == null)
                return;
            if (sanitizedName == null)
                return;

            foreach (var email in person.Emails)
            {
                
                result.Add(new DemoTarget.PersonWithEmail
                {
                    //I'm not sure how the mail should be formatted
                    FormattedEmail = $"{email.Email}{email.EmailType}",
                    SanitizedNameWithId = $"{person.Name}{person.Id}"
                });
            }

            return;
        }
    }
}
