using System.ComponentModel.DataAnnotations;

namespace YS.KeyValue
{
    public class CategoryRuleAttribute : RegularExpressionAttribute
    {
        public CategoryRuleAttribute()
            :base("^[a-zA-Z_][a-zA-Z0-9_]*(\\.[a-zA-Z_][a-zA-Z0-9_]*)*$")
        {

        }
    }
}
