using DemoSource;
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
                AddNewPair(groups, accounts, email, result);
            });

            return result;
        }

        private static void AddNewPair(IEnumerable<Group> groups, IEnumerable<Account> accounts, string email, List<(Account, Person)> result)
        {
            var account = accounts.FirstOrDefault(a => a.EmailAddress?.Email == email);
            var person = groups.SelectMany(g => g.People).FirstOrDefault(p => p.Emails.Any(e => e.Email == email));

            result.Add(new(account, person));
        }
    }
}
