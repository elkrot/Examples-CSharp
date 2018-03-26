using System;
using System.Collections.Generic;

namespace LDAP
{
    internal static class UserTools
    {
        internal static List<Users> Collect()
        {
            return new List<Users>() { new Users() { UserName="ivan",FirstName="Ivan"} };
        }
    }
}