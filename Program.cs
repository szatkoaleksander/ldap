using System;
using System.DirectoryServices.AccountManagement;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using Novell.Directory.Ldap;

namespace LDAP
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var conn = new LdapConnection();

                conn.Connect("uid=amebi,ou=services,dc=asi.wroclaw.pl", LdapConnection.DEFAULT_PORT);

                       
                //conn.Bind(LdapConnection.Ldap_V3, $"tramwaj.asi.wroclaw.pl\\{user.Username}", user.Password);
            
                Console.WriteLine("true");
            }
            catch (LdapException ex)
            {
                Console.WriteLine(ex);
            }   
        }
    }
}
