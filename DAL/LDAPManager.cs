using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.DirectoryServices;
using Microsoft.AspNetCore.Http;
using HUS_project.Models.ViewModels;

using System.Diagnostics;


namespace HUS_project.DAL
{
    public class LDAPManager
    {
        /// <summary>
        /// TESTING: For trying out various levels of accesses. Meant to simulate GetAccessResponses();
        /// </summary>
        /// <param name="uniLogin"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        internal List<string> TestLogin(string uniLogin, string pass)
        {
            List<UserLoginDataModel> TestAccounts = new List<UserLoginDataModel>()
            {
                new UserLoginDataModel("teach", "Kode1234"), // zbc-riah-Data-IT
                new UserLoginDataModel("skpstud", "Kode1234"), // ZBC-Ri-skpElev
                new UserLoginDataModel("skpteach", "Kode1234"), // ZBC-Ri-skpElev + zbc-riah-Data-IT
                new UserLoginDataModel("dude", "Kode1234") // NO ACCESS
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
                                groups.Add("zbc-riah-Data-IT");
                                break;
                            case "skpstud":
                                groups.Add("ZBC-Ri-skpElev");
                                break;
                            case "skpteach":
                                groups.Add("zbc-riah-Data-IT");
                                groups.Add("ZBC-Ri-skpElev");
                                break;
                        }
                    }
                    else
                    {
                        groups.Add("FEJL: (testlogin) Adgangskode forkert");
                    }
                }
            }
            return groups;
        }

        /// <summary>
        /// This function acquires all the relevant AD group memberships which the user attempting to login has, as well as any errors encountered along the way.
        /// </summary>
        /// <param name="uniLogin"></param>
        /// <param name="password"></param>
        /// <returns>All relevant memberships in "List<string>", as well as any errors encountered in this endeavour.</returns>
        internal List<string> GetAccessResponses(string uniLogin, string password)
        {
            // Directory Entry - This is the thing which controls how we connect to AD: the path there and our authentication.
            // ConnectionString, username, password - Any valid ZBC user can use their credentials for LDAP,
            // - However! They will still have to be a SKP student or a teacher, as we will test for such further below.
            DirectoryEntry ldapAccess = new DirectoryEntry(
                "LDAP://ldap.efif.dk/ou=zbc,ou=UserAccounts,dc=efif,dc=dk",
                uniLogin,
                password);

            List<string> reponses = new List<string>();

            try
            {
                // See if there's a SKP Student with this username, return info if yes
                if(ConfirmUserMembership(uniLogin, "ZBC-Ri-skpElev", ldapAccess))
                {
                    reponses.Add("ZBC-Ri-skpElev");
                }

                // Now going to do the same, to see if there's a ZBC Ringsted Employee with this uniLogin.
                if(ConfirmUserMembership(uniLogin, "zbc-riah-Data-IT", ldapAccess))
                {
                    reponses.Add("zbc-riah-Data-IT");
                }
            }
            catch (Exception e)
            {
                // This is a very janky way of doing error handling, and I am justly ashamed of it, but it works. Hopefully to be touched up on later.
                reponses.Add("FEJL: " + e.Message);
            }

            return reponses;
        }

        /// <summary>
        /// True if user is in the specified Group.
        /// </summary>
        /// <param name="uniLogin">User to check if in group.</param>
        /// <param name="group">Group to check if user a member of.</param>
        /// <param name="ldapAccess"></param>
        /// <returns></returns>
        bool ConfirmUserMembership(string uniLogin, string group, DirectoryEntry ldapAccess)
        {
            // A "DirectorySearcher" can search a directory for all kinds of AD objects. We are trying to find a user.
            DirectorySearcher dsFindUser = new DirectorySearcher(ldapAccess);
            dsFindUser.SearchScope = SearchScope.Subtree;

            // If no properties are specified, it will load them all - However, common sense dictates we limit it to what we need. "username" in our case.
            dsFindUser.PropertiesToLoad.Add("sAMAccountName");

            // Below is a LDAP Syntax Filter. It decides how the search works. It looks for a Person whose username is UniLogin and is a member of "group"
            dsFindUser.Filter = string.Format
                (
                    "(&" +
                        "(objectCategory=person)" +
                        "(sAMAccountName={0})" +
                        "(memberof=CN={1},OU=ZBC,OU=Groups,DC=efif,DC=dk)" +
                    ")",
                    uniLogin, group
                // ^ This way 'uniLogin' will be put where it says {0}, same for 'group' for {1}.
                );

            // This is where the program tries to find in AD what we set it to look for. See the comments for the Filter above.
            SearchResult result = dsFindUser.FindOne();

            if (result != null && result.Properties["sAMAccountName"] != null)
            {
                // Here we will try to get the user's username, just to see if the user exists.
                // "result" itself is almost never null, even if it does not find a user. Therefore we have to check if there's any useful info in "sAMAccountName".
                string userFound = result.Properties["sAMAccountName"][0].ToString();

                // User is not a member of the group, if user's username was not found.
                if (userFound == uniLogin)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
