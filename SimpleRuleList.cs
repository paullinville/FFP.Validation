using FFP.BO.Interfaces;
using System.Collections.Generic;

namespace FFP.Validations
{
    public class SimpleValidationList : List<IValidationRule>
    {
        public void AddValidation(IValidationRule Validation)
        {
            this.Add(Validation);
        }
    }
}

