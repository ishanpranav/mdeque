using System;

namespace MDeque
{
    partial class MDeque<T>
    {
        private class MDequeNode
        {
            private T? _value;

            public T Value
            {
                get
                {
                    if (_value == null)
                    {
                        throw new InvalidOperationException();
                    }
                    else
                    {
                        return _value;
                    }
                }
                set
                {
                    _value = value;
                }
            }

            public MDequeNode? Next { get; set; }
            public MDequeNode? Previous { get; set; }

            public MDequeNode(T value)
            {
                Value = value;
            }

            public void Invalidate()
            {
                _value = default;
                Next = null;
                Previous = null;
            }
        }
    }
}
