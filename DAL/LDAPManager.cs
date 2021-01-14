using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.DirectoryServices;
using Microsoft.AspNetCore.Http;
using HUS_project.Models;

namespace HUS_project.DAL
{
    public class LDAPManager
    {
        public static List<string> TestLogin(string uniLogin, string pass)
        {
            List<UserLoginDataModel> TestAccounts = new List<UserLoginDataModel>()
            {
                new UserLoginDataModel("teach", "Kode1234"), // ZBC-RIAH-Ansatte
                new UserLoginDataModel("skpstud", "Kode1234"), // ZBC-Ri-skpElev
                new UserLoginDataModel("skpteach", "Kode1234") // ZBC-Ri-skpElev + ZBC-RIAH-Ansatte
            };

            List<string> groups = new List<string>();
            foreach(UserLoginDataModel user in TestAccounts)
            {
                if(uniLogin == user.UNILogin)
                {
                    if(pass == user.Password)
                    {
                        switch (uniLogin)
                        {
                            case "teach":
                                groups.Add("ZBC-RIAH-Ansatte");
                                break;
                            case "skpstud":
                                groups.Add("ZBC-Ri-skpElev");
                                break;
                            case "skpteach":
                                groups.Add("ZBC-RIAH-Ansatte");
                                groups.Add("ZBC-Ri-skpElev");
                                break;
                        }
                    }

                    break;
                }
            }


            return groups;
        }

        public static List<string> AcquireAccessLevel(string uniLogin, string password)
        {
            // Directory Entry - This is the thing which controls how we connect to AD: the path there and our authentication.
            // ConnectionString, username, password - Any valid ZBC user can use their credentials for LDAP,
            // - However! They will still have to be a student or a teacher, as we will test for such further below.
            DirectoryEntry ldapAccess = new DirectoryEntry(
                "LDAP://ldap.efif.dk/ou=zbc,ou=UserAccounts,dc=efif,dc=dk",
                uniLogin,
                password);

            
            string emailFound = "";
            List<string> groups = new List<string>();

            try
            {
                // See if there's a Student with this username, return info if yes
                SearchResult result = AcquireUserFromGroup(uniLogin, "ZBC-Ri-skpElev", ldapAccess);

                if (result != null)
                {
                    // Here we will try to get the user's email address, just to see if the user exists.
                    // "result" itself is never empty, even if it does not find a user. Therefore we have to check if there's any useful info in "mail".
                    if (result.Properties["mail"] != null)
                    {
                        emailFound = result.Properties["mail"][0].ToString();
                    }

                    // User is not a SKP Student if no email was pulled.
                    if (emailFound.Length > 0)
                    {
                        groups.Add("ZBC-Ri-skpElev");
                    }
                }
                
                // Resetting "result" for good measure.
                result = null;
                result = AcquireUserFromGroup(uniLogin, "ZBC-RIAH-Ansatte", ldapAccess);

                if (result != null)
                {
                    if (result.Properties["mail"] != null)
                    {
                        emailFound = result.Properties["mail"][0].ToString();
                    }
                    if (emailFound.Length > 0)
                    {
                        groups.Add("ZBC-RIAH-Ansatte");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return groups;
        }

        /// <summary>
        /// Returns SearchResult for user, if user is in a Group.
        /// </summary>
        /// <param name="uniLogin">User to chekc if in group.</param>
        /// <param name="group">Group to check if user a member of.</param>
        /// <param name="ldapAccess"></param>
        /// <returns></returns>
        static SearchResult AcquireUserFromGroup(string uniLogin, string group, DirectoryEntry ldapAccess)
        {
            DirectorySearcher dsFindUser = new DirectorySearcher(ldapAccess);
            dsFindUser.SearchScope = SearchScope.Subtree;

            // If no properties are specified, it will load them all
            // OBS!!! We Must limit it to what we need in the final product.
            dsFindUser.PropertiesToLoad.Add("mail");
            // Below is a LDAP Syntax Filter. It decides how the search works
            dsFindUser.Filter = string.Format
                (
                    "(&" +
                        "(objectCategory=person)" +
                        "(mail={0})" +
                        "(memberof=CN={1},OU=ZBC,OU=Groups,DC=efif,DC=dk)" +
                    ")",
                    uniLogin + "@zbc.dk", group // This way "uniLogin" will be put where it says "{0}", same for Group for {1}. This is to prevent injections.
                );

            // Not sure why I make a SearchResult and not just directly "return dsFindUser.FindOne();". I think it has to do with error messages.
            SearchResult result = dsFindUser.FindOne();

            return result;
        }
    }
}
