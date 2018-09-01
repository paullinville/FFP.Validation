using System;
using System.Collections.Generic;
using FFP.Validations;

namespace FFP.Validations
{
    public class ValidationPackages
    {
        private ValidationPackages()
        {
        }

        private static List<IValidationPackage> lvPackages = new List<IValidationPackage>();

        public static List<IValidationPackage> AllValidationPackages()
        {
            if (UserLevel)
                return ((ISupportsValidationPackages)System.Threading.Thread.CurrentPrincipal).ValidationList();
            else
                return lvPackages;
        }

        public static bool UserLevel
        {
            get
            {
                if (System.Threading.Thread.CurrentPrincipal is ISupportsValidationPackages)
                {
                    ISupportsValidationPackages obj = (ISupportsValidationPackages)System.Threading.Thread.CurrentPrincipal;
                    return obj.UseUserValidationPackages;
                }
                else
                    return false;
            }
        }

        public static void AddValidationPackages(IEnumerable<IValidationPackage> packages)
        {
            if (UserLevel)
                ((ISupportsValidationPackages)System.Threading.Thread.CurrentPrincipal).AddValidationPackages(packages);
            else
                lock (lvPackages)
                {
                    foreach (IValidationPackage itm in packages)
                    {
                        if (!lvPackages.Contains(itm))
                        {
                            lvPackages.Add(itm);
                            itm.Setup();
                        }
                    }
                }
        }

        public static IEnumerable<IBrokenRule> Validate(object bo)
        {

            // BrokenValidationRules broke = new BrokenValidationRules();
            List<IBrokenRule> lst = new List<IBrokenRule>();
            foreach (IValidationPackage pckg in AllValidationPackages())
                lst.AddRange(pckg.CheckRules(bo));
            return lst;

        }

        public static IEnumerable<IRuleDescription> ListValidations(object bo)
        {
            List<IRuleDescription> lst = new List<IRuleDescription>();
            foreach (IValidationPackage pckg in AllValidationPackages())
                lst.AddRange(pckg.PackageRules(bo));
            return lst;
        }

        public static IEnumerable<IRuleDescription> ListValidations(Type bo)
        {
            List<IRuleDescription> lst = new List<IRuleDescription>();
            foreach (IValidationPackage pckg in AllValidationPackages())
                lst.AddRange(pckg.TypesRules(bo));
            return lst;
        }
    }
}
