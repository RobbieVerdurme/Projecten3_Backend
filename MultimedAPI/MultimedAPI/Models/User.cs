using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultimedAPI.Models
{
    public class User
    {

        #region Properties

        public string FirstName { get; set; }

        public string FamilyName { get; set; }


        #endregion

        #region Constructors

        public User()
        {
        }

        public User(string firstName, string familyName)
        {
            FirstName = firstName;
            FamilyName = familyName;
        }



        #endregion
    }
}
