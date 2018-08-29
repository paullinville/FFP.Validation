using System;
using System.Collections.Generic;
using FFP.Validations;

public class RulePackages
{
    private RulePackages()
    {
    }

    private static List<IValidationPackage> lvPackages = new List<IValidationPackage>();

    public static List<IValidationPackage> Packages()
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

    public static void AddRulePackages(IEnumerable<IValidationPackage> packages)
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

    public static void CheckRules(IValidatedItem bo)
    {
        foreach (IValidationPackage pckg in Packages())
            pckg.CheckBusinessValidations(bo);
    }

    public static IEnumerable<IValidationRule> ListRules(IValidatedItem bo)
    {
        List<IValidationRule> lst = new List<IValidationRule>();
        foreach (IValidationPackage pckg in Packages())
            lst.AddRange(pckg.ValidationRules(bo));
        return lst;
    }

    //todo fix
    //public static IEnumerable<IValidationRule> ListRules(Type bo)
    //{
    //    List<IValidationRule> lst = new List<IValidationRule>();
    //    foreach (IValidationPackage pckg in Packages())
    //        lst.AddRange(pckg.ValidationRules(bo));
    //    return lst;
    //}
}
