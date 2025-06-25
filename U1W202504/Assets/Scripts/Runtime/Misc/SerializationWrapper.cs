using System;

namespace U1W
{
    [Serializable]
    public class SerializationWrapper<T>
    {
        public T value;

        public SerializationWrapper(T value)
        {
            this.value = value;
        }
    }
}
