package project3;

import java.util.Iterator;

/**
 * <p>
 * A linear collection that supports element insertion and removal at three
 * points: front, middle and back. The name <em>mdeque</em> is short for "double
 * ended queue" (deque) with <em>m</em> for "middle" and is pronounced
 * "em-deck". MDeque has no fixed limits on the number of elements it contains.
 * </p>
 * 
 * <p>
 * The remove operations all return null values if the mdeque is empty. The
 * structure does not allow null as an element.
 * </p>
 * 
 * <p>
 * All {@code pop...}, {@code push...}, and {@code peek...} operations (from all
 * three points of access) are constant time operations.
 * </p>
 * 
 * <p>
 * The <em>middle</em> position is defined as (size+1)/2 when inserting an
 * element in the <em>middle</em> and as (size/2) when retrieving an element
 * from the <em>middle</em>. The position count is zero based.
 * </p>
 * 
 * {@code [A, B, C, D] -- middle element is C, insert at middle would add at index 2
 * (between B and C).}
 * {@code [A, B, C, D, E] -- middle element is C, insert at middle would add at index 3
 * (between C and D).}
 * 
 * @author Ishan Pranav
 * @author Joanna Klukowska
 */
public class MDeque<E> implements Iterable<E> {

    /**
     * Provides a node for the mdeque's linked list.
     * 
     * @author Ishan Pranav
     */
    private class MDequeNode {
        private E value;
        private MDequeNode next;
        private MDequeNode previous;

        /**
         * Initializes a new instance of the {@link MDequeNode} class.
         * 
         * @param value The node data.
         */
        public MDequeNode(E value) {
            this.value = value;
        }

        /**
         * Renders a node unusuable by clears all outstanding references. This method
         * marks the node as ready for garbage disposal.
         */
        public void invalidate() {
            value = null;
            next = null;
            previous = null;
        }
    }

    /**
     * Provides a sequential (front-to-back) iterator for the mdeque.
     * 
     * @author Ishan Pranav
     */
    private class MDequeIterator implements Iterator<E> {
        private MDequeNode current = head;

        /** {@inheritDoc} */
        @Override
        public boolean hasNext() {
            return current != null;
        }

        /** {@inheritDoc} */
        @Override
        public E next() {
            final MDequeNode result = current;

            current = current.next;

            return result.value;
        }
    }

    /**
     * Provides a reverse (back-to-front) iterator for the mdeque.
     */
    private class MDequeReverseIterator implements Iterator<E> {
        private MDequeNode current = tail;

        /** {@inheritDoc} */
        @Override
        public boolean hasNext() {
            return current != head;
        }

        /** {@inheritDoc} */
        @Override
        public E next() {
            final MDequeNode result = current;

            current = current.previous;

            return result.value;
        }
    }

    private int count;
    private MDequeNode head;
    private MDequeNode body;
    private MDequeNode tail;

    /**
     * Returns the number of elements in this mdeque.
     * 
     * @return the number of elements in this mdeque.
     */
    public int size() {
        return count;
    }

    /**
     * Retrieves the first element of this mdeque.
     * 
     * @return the front of this mdeque, or {@code null} if this mdeque is empty
     */
    public E peekFront() {
        if (head == null) {
            return null;
        } else {
            return head.value;
        }
    }

    /**
     * Retrieves the middle element of this mdeque.
     * 
     * @return the middle of this mdeque, or {@code null} if this mdeque is empty
     */
    public E peekMiddle() {
        if (body == null) {
            return null;
        } else {
            return body.value;
        }
    }

    /**
     * Retrieves the back element of this mdeque.
     * 
     * @return the back of this mdeque, or {@code null} if this mdeque is empty
     */
    public E peekBack() {
        if (tail == null) {
            return null;
        } else {
            return tail.value;
        }
    }

    /**
     * Initializes a new instance of the {@link MDeque} class.
     */
    public MDeque() {
    }

    /**
     * Initializes the linked list with a single node.
     * 
     * @param node The initial node.
     */
    private void initialize(MDequeNode node) {
        count = 1;
        head = node;
        body = node;
        tail = node;
    }

    /**
     * Inserts the specified item at the front of this mdeque.
     * 
     * @param item the element to add
     * @throws IllegalArgumentException if {@code item} is {@code null}
     */
    public void pushFront(E item) {
        if (item == null) {
            throw new IllegalArgumentException("Value cannot be null. Argument name: item.");
        } else {
            final MDequeNode node = new MDequeNode(item);

            if (head == null) {
                initialize(node);
            } else {
                node.next = head;
                head.previous = node;
                head = node;

                if (count % 2 == 0) {
                    body = body.previous;
                }

                count++;
            }
        }
    }

    /**
     * Inserts a new node to the linked list in the position preceding an existing
     * node.
     * 
     * @param newNode      the new node, inserted after the existing node
     * @param existingNode the existing node, after which the new node is inserted
     */
    private void addBefore(MDequeNode newNode, MDequeNode existingNode) {
        count++;
        newNode.next = existingNode;
        newNode.previous = existingNode.previous;
        existingNode.previous.next = newNode;
        existingNode.previous = newNode;
    }

    /**
     * Inserts the specified item in the middle of this mdeque.
     * 
     * @param item the element to add
     * @throws IllegalArgumentException if {@code item} is {@code null}
     */
    public void pushMiddle(E item) {
        if (item == null) {
            throw new IllegalArgumentException("Value cannot be null. Argument name: item.");
        } else {
            final MDequeNode node = new MDequeNode(item);

            if (body == null) {
                initialize(node);
            } else {
                if (count % 2 == 0) {
                    addBefore(node, body);
                } else if (body.next == null) {
                    pushBack(node);
                } else {
                    addBefore(node, body.next);
                }

                body = node;
            }
        }
    }

    /**
     * Inserts the specified node at the back of this mdeque.
     * 
     * @param node The node.
     */
    private void pushBack(MDequeNode node) {
        count++;
        tail.next = node;
        node.previous = tail;
        tail = node;
    }

    /**
     * Inserts the specified item at the back of this mdeque.
     * 
     * @param item the element to add
     * @throws IllegalArgumentException if {@code item} is {@code null}
     */
    public void pushBack(E item) {
        if (item == null) {
            throw new IllegalArgumentException("Value cannot be null. Argument name: item.");
        } else {
            final MDequeNode node = new MDequeNode(item);

            if (tail == null) {
                initialize(node);
            } else {
                pushBack(node);
            }

            if (count % 2 == 0) {
                body = body.next;
            }
        }
    }

    /**
     * Retrieves and removes the first element of this mdeque.
     * 
     * @return the front of this mdeque, or {@code null} if this mdeque is empty
     */
    public E popFront() {
        if (head == null) {
            return null;
        } else {
            if (head.next == null) {
                return clear();
            } else {
                final E result = head.value;
                final MDequeNode removed = head;

                head = head.next;
                head.previous = null;

                if (count % 2 == 1) {
                    body = body.next;
                }

                count--;

                removed.invalidate();

                return result;
            }
        }
    }

    /**
     * Retrieves and removes the middle element of this mdeque.
     * 
     * @return the middle of this mdeque, or {@code null} if this mdeque is empty
     */
    public E popMiddle() {
        if (body == null) {
            return null;
        } else if (body.previous == null) {
            return popFront();
        } else if (body.next == null) {
            return popBack();
        } else {
            final E result = body.value;
            final MDequeNode removed = body;

            body.previous.next = body.next;
            body.next.previous = body.previous;

            if (count % 2 == 0) {
                body = body.previous;
            } else {
                body = body.next;
            }

            count--;

            removed.invalidate();

            return result;
        }
    }

    /**
     * Retrieves and removes the back element of this mdeque.
     * 
     * @return the back of this mdeque, or {@code null} if this mdeque is empty
     */
    public E popBack() {
        if (tail == null) {
            return null;
        } else {
            if (body.previous == null) {
                return clear();
            } else {
                final E result = tail.value;
                final MDequeNode removed = tail;

                tail = tail.previous;
                tail.next = null;

                if (count % 2 == 0) {
                    body = body.previous;
                }

                count--;

                removed.invalidate();

                return result;
            }
        }
    }

    /**
     * <p>
     * Truncates the mdeque's linked list and returns the first element.
     * </p>
     * 
     * <p>
     * This method is private because it is as a auxiliary method for other parts of
     * the public interface.
     * </p>
     * 
     * <p>
     * The method runs in constant time. It should not be used to clear a list with
     * more than three elements since it does not iteratively invalidate all of the
     * nodes. The method guarantees that the first, last, and middle elements are
     * invalidated and resets the data structure to its initial state.
     * </p>
     * 
     * <p>
     * Precondition: the linked list contains between 0 and 3 elements, inclusive.
     * </p>
     * 
     * @return the front of the mdeque before clearing
     */
    private E clear() {
        final E result = head.value;

        head.invalidate();
        body.invalidate();
        tail.invalidate();

        count = 0;
        head = null;
        body = null;
        tail = null;

        return result;
    }

    /**
     * Returns an iterator over the elements in this mdeque in proper sequence. The
     * elements will be returned in order from front to back.
     * 
     * @return an iterator over the elements in this mdeque in proper sequence
     */
    @Override
    public Iterator<E> iterator() {
        return new MDequeIterator();
    }

    /**
     * Returns an iterator over the elements in this mdeque in reverse sequential
     * order. The elements will be returned in order from back to front.
     * 
     * @return an iterator over the elements in this mdeque in reverse sequence
     */
    public Iterator<E> reverseIterator() {
        return new MDequeReverseIterator();
    }

    /**
     * Returns a string representation of this mdeque.
     * 
     * The string representation consists of a list of the collection's elements in
     * the order they are returned by its iterator, enclosed in square brackets
     * ({@code "[]"}). Adjacent elements are separated by the characters
     * {@code ", "} (comma and space).
     * 
     * @return a string representation of this mdeque
     */
    @Override
    public String toString() {
        // Implementation restriction: this method must be implemented using recursion

        // The toString() method wraps recursive toString(StringBuilder, Iterator<E>) method

        final StringBuilder result = new StringBuilder();
        final MDeque<E> copy = new MDeque<E>();

        for (E item : this) {
            copy.pushFront(item);
        }

        result.append('[');

        toString(result, iterator());

        return result.toString();
    }

    /**
     * Recursively prepares a string representation of this mdeque.
     * 
     * @param builder  the string builder
     * @param iterator an iterator over the elements of the mdeque
     */
    private void toString(StringBuilder builder, Iterator<E> iterator) {
        if (iterator.hasNext()) {
            builder.append(iterator.next());

            if (iterator.hasNext()) {
                builder.append(", ");
            }

            toString(builder, iterator);
        } else {
            builder.append(']');
        }
    }
}
