using System.ComponentModel.DataAnnotations;
using YS.Knife;
namespace YS.KeyValue
{
    [OptionsClass]
    public class KeyValueOptions
    {
        [RegularExpression("^[a-zA-Z_][a-zA-Z0-9_]*(\\.[a-zA-Z_][a-zA-Z0-9_]*)*$")]
        public string CategoryPrefix { get; set; }

        public NameStyle TypedCategoryNameStyle { get; set; } = NameStyle.Lower;

    }
    public enum NameStyle
    {
        Default,
        Lower,
        Upper
    }
}
