using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MDeque
{
    partial class MDeque<T>
    {
        private class MDequeEnumerator : IEnumerator<T>
        {
            private readonly int _version;
            private readonly MDeque<T> _reference;

            private MDequeNode? _node;

            [NotNull]
            public T? Current { get; private set; }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public MDequeEnumerator(MDeque<T> reference)
            {
                _version = reference._version;
                _reference = reference;
                _node = reference._head;
            }

            public bool MoveNext()
            {
                if (_reference._version != _version)
                {
                    throw new InvalidOperationException();
                }
                else if (_node == null)
                {
                    return false;
                }
                else
                {
                    Current = _node.Value;
                    _node = _node.Next;

                    return true;
                }
            }

            public void Reset()
            {
                if (_reference._version != _version)
                {
                    throw new InvalidOperationException();
                }
                else
                {
                    Current = default;
                }
            }

            protected virtual void Dispose(bool disposing) { }

            public void Dispose()
            {
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }
        }
    }
}
