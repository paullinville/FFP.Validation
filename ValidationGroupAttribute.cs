using FFP.CoreUtilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Validations
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ValidationGroupAttribute : Attribute
    {
        public ValidationGroupAttribute(string group)
        {
            lvGroups.Add(group.Trim().ToUpper());
        }

        public ValidationGroupAttribute(string grpLst, char delimiter)
        {
            if (grpLst != null)
                lvGroups.AddToHashSet((from itm in grpLst.Split(delimiter)
                                       select itm.ToUpper().Trim()).Distinct());
        }

        private HashSet<string> lvGroups = new HashSet<string>();
        public HashSet<string> Groups
        {
            get
            {
                return lvGroups;
            }
        }
    }
}

