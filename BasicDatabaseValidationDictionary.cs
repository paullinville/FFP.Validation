
namespace FFP.Validations
{
    public abstract class BasicDatabaseValidationDictionary : BasicValidationPackage
    {
        public const string Database_Validation = "Database_Validation";
        public BasicDatabaseValidationDictionary()
        {
            GoodGroups.Add(Database_Validation);
        }
    }
}

