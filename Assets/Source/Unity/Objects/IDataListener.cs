

using Source.Resources;

namespace Source.Unity.Objects
{
    public interface IDataListener
    {
        public void ListenToData();
        public void propertyChange(string property);
    }
}