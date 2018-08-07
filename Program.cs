using System;
using System.Linq;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using Novell.Directory.Ldap;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace LDAP
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            string ldapHost = configuration["ldapHost"];
            string appServiceDn = configuration["AppServiceDn"];
            string appServicePassword = configuration["AppServicePassword"];
            string searchBase = configuration["searchBase"];
            string searchFilterTemplate = configuration["SearchFilter"];

            string firstNameAttribute = configuration["FirstNameAttribute"];
            string lastNameAttribute = configuration["LastNameAttribute"];
            string usernameAttribute = configuration["UsernameAttribute"];

            Console.Write("Enter username: ");
            var username = Console.ReadLine();
            Console.Write("Enter password: ");
            var password = ConsoleExtensions.ReadPassword();

            using (var connection = new LdapConnection())
            {
                connection.ConnectionTimeout = 10000;
                try
                {
                    connection.Connect(ldapHost, LdapConnection.DEFAULT_PORT);
                    connection.Bind(appServiceDn, appServicePassword);

                    var searchFilter = string.Format(searchFilterTemplate, username);
                    var result = connection.Search(searchBase, LdapConnection.SCOPE_SUB, searchFilter,
                    new string[] { },
                false
            );

                    var user = result.next();
                    if (user != null)
                    {
                        connection.Bind(user.DN, password);
                        if (connection.Bound)
                        {
                            var loggedUser = new User
                            {
                                DisplayName = $"{user.getAttribute(firstNameAttribute).StringValue} {user.getAttribute(lastNameAttribute).StringValue}",
                                Username = user.getAttribute(usernameAttribute).StringValue
                            };

                            Console.WriteLine($"Logged as {loggedUser.Username} ({loggedUser.DisplayName})");
                            connection.Disconnect();
                        }
                    }
                }
                catch (LdapException ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
