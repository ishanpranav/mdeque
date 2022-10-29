using System;
using System.Collections;
using System.Collections.Generic;
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
    public class MDeque<T> : ICollection<T>, ICollection
    {
        private object? _syncRoot;

        /// <inheritdoc/>
        public int Count { get; }

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

        /// <inheritdoc/>
        void ICollection<T>.Add(T item)
        {
            AddLast(item);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void CopyTo(T[] array, int arrayIndex)
        {
            CopyTo(array, arrayIndex);
        }

        /// <inheritdoc/>
        public void CopyTo(Array array, int index)
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
        bool ICollection<T>.Remove(T item)
        {
            return false;
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

            }
        }

        /// <summary>
        /// Retrieves the first element of this m-deque.
        /// </summary>
        /// <returns>The front this m-deque, or <see langword="null"/> if this m-deque is empty.</returns>
        public T? PeekFirst()
        {
            return default;
        }

        /// <summary>
        /// Retrieves the middle element of this m-deque.
        /// </summary>
        /// <returns>The middle of this m-deque, or <see langword="null"/> if this m-deque is empty.</returns>
        public T? PeekMiddle()
        {
            return default;
        }

        /// <summary>
        /// Retrieves the last element of this m-deque.
        /// </summary>
        /// <returns>The back this m-deque, or <see langword="null"/> if this m-deque is empty.</returns>
        public T? PeekLast()
        {
            return default;
        }

        /// <summary>
        /// Retrieves and removes the first element of this m-deque.
        /// </summary>
        /// <returns>The front this m-deque, or <see langword="null"/> if this m-deque is empty.</returns>
        public T? RemoveFirst()
        {
            return default;
        }

        /// <summary>
        /// Retrieves and removes the middle element of this m-deque.
        /// </summary>
        /// <returns>The middle of this m-deque, or <see langword="null"/> if this m-deque is empty.</returns>
        public T? RemoveMiddle()
        {
            return default;
        }

        /// <summary>
        /// Retrieves and removes the last element of this m-deque.
        /// </summary>
        /// <returns>The back this m-deque, or <see langword="null"/> if this m-deque is empty.</returns>
        public T? RemoveLast()
        {
            return default;
        }

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
