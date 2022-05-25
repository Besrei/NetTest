using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTest2
{
    internal class ToDo
    {
        public IEnumerable<(DemoSource.Account, DemoSource.Person)>
            MatchPersonToAccount(
            IEnumerable<DemoSource.Group> groups,
            IEnumerable<DemoSource.Account> accounts,
            IEnumerable<string> emails)
        {
            var result = new List<(DemoSource.Account, DemoSource.Person)> ();

            Parallel.ForEach(emails, email =>
            {
                DemoSource.Person personWithEmail = groups.SelectMany(m => m.People).Where(n => n.Emails.Any(o => o.Email == email)).FirstOrDefault();
                DemoSource.Account accountWithPersonEmail = accounts.Where(m => m.EmailAddress.Email == email).FirstOrDefault();

                result.Add( new( accountWithPersonEmail, personWithEmail));
            });
            throw new NotImplementedException();
        }
    }
}
