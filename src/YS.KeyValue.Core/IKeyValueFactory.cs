namespace YS.KeyValue
{
    public interface IKeyValueFactory
    {
        IKeyValueService<T> CreateKeyValue<T>(string name) where T : class;
    }
}
