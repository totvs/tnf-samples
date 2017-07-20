namespace Tnf.Architecture.Common.ValueObjects
{
    public class ComposeKey<TPrimaryKey, TSecundaryKey>
    {
        public ComposeKey(TPrimaryKey primaryKey, TSecundaryKey secundaryKey)
        {
            PrimaryKey = primaryKey;
            SecundaryKey = secundaryKey;
        }

        public ComposeKey()
        {
        }

        public TPrimaryKey PrimaryKey { get; set; }
        public TSecundaryKey SecundaryKey { get; set; }
    }
}
