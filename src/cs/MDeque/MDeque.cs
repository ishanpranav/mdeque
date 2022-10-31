using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading;

namespace MDeque
{
    /// <summary>
    /// A linear collection that supports element insertion and removal at three points: front, middle and back. The name <em>m-deque</em> is short for "double ended queue" (deque) with <em>m</em> for "middle" and is pronounced "em-deck". M-deque has no fixed limits on the number of elements it contains.
    /// </summary>
    /// <remarks>
    /// <para>The remove operations all return <see langword="null"/> values if the m-deque is empty. The structure does not allow <see langword="null"/> as an element.</para>
    /// <para>All <c>pop...</c>, <c>push...</c>, and <c>peek...</c> operations (from all three points of access) are constant time operations.</para>
    /// <para>The <em>middle</em> position is defined as (size+1)/2. For m-deques with odd sizes, this is the actual middle element. For the m-deques with even sizes, this is the element, one past the middle of that elements. The position count is zero-based.</para>
    /// </remarks>
    /// <typeparam name="T">The type of elements held in this m-deque</typeparam>
    public partial class MDeque<T> : ICollection, ICollection<T>, IReadOnlyCollection<T>
    {
        private object? _syncRoot;
        private MDequeNode? _head;
        private MDequeNode? _body;
        private MDequeNode? _tail;

        /// <summary>
        /// Gets the first element of this m-deque.
        /// </summary>
        /// <value>The front this m-deque, or <see langword="null"/> if this m-deque is empty.</value>
        public T? First
        {
            get
            {
                if (_head == null)
                {
                    return default;
                }
                else
                {
                    return _head.Value;
                }
            }
        }

        /// <summary>
        /// Gets the middle element of this m-deque.
        /// </summary>
        /// <value>The middle of this m-deque, or <see langword="null"/> if this m-deque is empty.</value>
        public T? Center
        {
            get
            {
                if (_body == null)
                {
                    return default;
                }
                else
                {
                    return _body.Value;
                }
            }
        }

        /// <summary>
        /// Gets the last element of this m-deque.
        /// </summary>
        /// <value>The back this m-deque, or <see langword="null"/> if this m-deque is empty.</value>
        public T? Last
        {
            get
            {
                if (_tail == null)
                {
                    return default;
                }
                else
                {
                    return _tail.Value;
                }
            }
        }

        /// <inheritdoc/>
        public int Count { get; private set; }

        /// <inheritdoc/>
        bool ICollection<T>.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        /// <inheritdoc/>
        bool ICollection.IsSynchronized
        {
            get
            {
                return false;
            }
        }

        /// <inheritdoc/>
        object ICollection.SyncRoot
        {
            get
            {
                if (_syncRoot == null)
                {
                    Interlocked.CompareExchange(ref _syncRoot, new object(), comparand: null);
                }

                return _syncRoot;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MDeque{T}"/> class.
        /// </summary>
        public MDeque() { }

        private void Initialize(MDequeNode node)
        {
            _head = node;
            _body = node;
            _tail = node;

            Count = 1;
        }

        /// <summary>
        /// Inserts the specified item at the front of this m-deque.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="ArgumentException"><paramref name="item"/> is <see langword="null"/>.</exception>
        public void AddFirst(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            else
            {
                MDequeNode node = new MDequeNode(item);

                if (_head == null)
                {
                    Initialize(node);
                }
                else
                {
                    Contract.Assert(_body != null);

                    node.Next = _head;
                    _head.Previous = node;
                    _head = node;

                    if (Count % 2 == 0)
                    {
                        _body = _body.Previous;
                    }

                    Count++;
                }
            }
        }

        private void AddBefore(MDequeNode newNode, MDequeNode existingNode)
        {
            Contract.Assert(existingNode.Previous != null);

            newNode.Next = existingNode;
            newNode.Previous = existingNode.Previous;
            existingNode.Previous.Next = newNode;
            existingNode.Previous = newNode;

            Count++;
        }

        /// <summary>
        /// Inserts the specified item in the middle of this m-deque.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="ArgumentException"><paramref name="item"/> is <see langword="null"/>.</exception>
        public void AddCenter(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            else
            {
                MDequeNode node = new MDequeNode(item);

                if (_body == null)
                {
                    Initialize(node);
                }
                else
                {
                    if (Count % 2 == 0)
                    {
                        AddBefore(node, _body);
                    }
                    else
                    {
                        MDequeNode? existingNode = _body.Next;

                        if (existingNode == null)
                        {
                            AddLast(node);
                        }
                        else
                        {
                            AddBefore(node, existingNode);
                        }
                    }

                    _body = node;
                }
            }
        }

        private void AddLast(MDequeNode node)
        {
            Contract.Assert(_tail != null);

            _tail.Next = node;
            node.Previous = _tail;
            _tail = node;
            Count++;
        }

        /// <summary>
        /// Inserts the specified item at the back of this m-deque.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="ArgumentException"><paramref name="item"/> is <see langword="null"/>.</exception>
        public void AddLast(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            else
            {
                MDequeNode node = new MDequeNode(item);

                if (_tail == null)
                {
                    Initialize(node);
                }
                else
                {
                    Contract.Assert(_body != null);

                    AddLast(node);

                    if (Count % 2 == 0)
                    {
                        _body = _body.Next;
                    }
                }
            }
        }

        /// <summary>
        /// Retrieves and removes the first element of this m-deque.
        /// </summary>
        /// <returns>The front this m-deque, or <see langword="null"/> if this m-deque is empty.</returns>
        public T? RemoveFirst()
        {
            if (_head == null)
            {
                return default;
            }
            else if (_head.Next == null)
            {
                return Truncate();
            }
            else
            {
                Contract.Assert(_body != null);

                T result = _head.Value;
                MDequeNode removed = _head;

                _head = _head.Next;
                _head.Previous = null;

                if (Count % 2 == 1)
                {
                    _body = _body.Next;
                }

                Count--;

                removed.Invalidate();

                return result;
            }
        }

        /// <summary>
        /// Retrieves and removes the middle element of this m-deque.
        /// </summary>
        /// <returns>The middle of this m-deque, or <see langword="null"/> if this m-deque is empty.</returns>
        public T? RemoveCenter()
        {
            if (_body == null)
            {
                return default;
            }
            else if (_body.Previous == null)
            {
                return RemoveFirst();
            }
            else if (_body.Next == null)
            {
                return RemoveLast();
            }
            else
            {
                T result = _body.Value;
                MDequeNode removed = _body;

                _body.Previous.Next = _body.Next;
                _body.Next.Previous = _body.Previous;

                if (Count % 2 == 0)
                {
                    _body = _body.Previous;
                }
                else
                {
                    _body = _body.Next;
                }

                Count--;

                removed.Invalidate();

                return result;
            }
        }

        /// <summary>
        /// Retrieves and removes the last element of this m-deque.
        /// </summary>
        /// <returns>The back this m-deque, or <see langword="null"/> if this m-deque is empty.</returns>
        public T? RemoveLast()
        {
            if (_tail == null)
            {
                return default;
            }
            else if (_tail.Previous == null)
            {
                return Truncate();
            }
            else
            {
                Contract.Assert(_body != null);

                T result = _tail.Value;
                MDequeNode removed = _tail;

                _tail = _tail.Previous;
                _tail.Next = null;

                if (Count % 2 == 0)
                {
                    _body = _body.Previous;
                }

                Count--;

                removed.Invalidate();

                return result;
            }
        }

        /// <inheritdoc/>
        void ICollection<T>.Add(T item)
        {
            AddLast(item);
        }

        private T Truncate()
        {
            Contract.Assert(_head != null);
            Contract.Assert(_head == _body);
            Contract.Assert(_body == _tail);

            T truncated = _head.Value;

            _head.Invalidate();
            _body.Invalidate();
            _tail.Invalidate();

            _head = null;
            _body = null;
            _tail = null;

            Count = 0;

            return truncated;
        }

        /// <inheritdoc/>
        public void Clear()
        {
            MDequeNode? current = _head;

            while (current != null)
            {
                MDequeNode node = current;

                current = current.Next;

                node.Invalidate();
            }

            _head = null;
            _body = null;
            _tail = null;
            Count = 0;
        }

        /// <inheritdoc/>
        public bool Contains(T item)
        {
            foreach (T value in this)
            {
                if (item == null)
                {
                    if (value == null)
                    {
                        return true;
                    }
                }
                else if (item.Equals(value))
                {
                    return true;
                }
            }

            return false;
        }

        /// <inheritdoc/>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentOutOfRangeException(nameof(array));
            }
            else
            {
                int i = 0;

                foreach (T item in this)
                {
                    array[arrayIndex + i] = item;

                    i++;
                }
            }
        }

        /// <inheritdoc/>
        void ICollection.CopyTo(Array array, int index)
        {
            if (array == null)
            {
                throw new ArgumentOutOfRangeException(nameof(array));
            }
            else
            {
                int i = 0;

                foreach (T item in this)
                {
                    array.SetValue(item, index + i);

                    i++;
                }
            }
        }

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator()
        {
            for (MDequeNode? current = _head; current != null; current = current.Next)
            {
                yield return current.Value;
            }
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc/>
        bool ICollection<T>.Remove(T item)
        {
            if (_head == null)
            {
                return false;
            }
            else
            {
                Contract.Assert(_body != null);
                Contract.Assert(_tail != null);

                if (item == null)
                {
                    if (_head.Value == null)
                    {
                        return RemoveFirst() != null;
                    }
                    else if (_body.Value == null)
                    {
                        return RemoveCenter() != null;
                    }
                    else if (_tail.Value == null)
                    {
                        return RemoveLast() != null;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (item.Equals(_head.Value))
                {
                    return RemoveFirst() != null;
                }
                else if (item.Equals(_body.Value))
                {
                    return RemoveCenter() != null;
                }
                else if (item.Equals(_tail.Value))
                {
                    return RemoveLast() != null;
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return '[' + string.Join(separator: ", ", this.Select(item =>
            {
                if (item == null)
                {
                    return "null";
                }
                else if (ReferenceEquals(this, item))
                {
                    return string.Format(format: "this {0}", GetType());
                }
                else
                {
                    return item.ToString();
                }
            })) + ']';
        }
    }
}
