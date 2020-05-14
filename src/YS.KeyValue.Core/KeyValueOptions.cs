using YS.Knife;
namespace YS.KeyValue
{
    [OptionsClass]
    public class KeyValueOptions
    {
        [CategoryRule]
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
