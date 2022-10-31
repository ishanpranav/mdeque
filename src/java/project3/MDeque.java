package java.project3;

import java.util.Iterator;

public class MDeque<E> implements Iterable<E> {
    private class MDequeNode {
        private E value;
        private MDequeNode next;
        private MDequeNode previous;

        public MDequeNode(E value) {
            this.value = value;
        }

        public void invalidate() {
            value = null;
            next = null;
            previous = null;
        }
    }

    private class MDequeIterator implements Iterator<E> {
        private MDequeNode current = head;

        @Override
        public boolean hasNext() {
            return current != null;
        }

        @Override
        public E next() {
            MDequeNode result = current;

            current = current.next;

            return result.value;
        }
    }

    private int count;
    private MDequeNode head;
    private MDequeNode body;
    private MDequeNode tail;

    public int size() {
        return count;
    }

    public E peekFront() {
        if (head == null) {
            return null;
        } else {
            return head.value;
        }
    }

    public E peekMiddle() {
        if (body == null) {
            return null;
        } else {
            return body.value;
        }
    }

    public E peekBack() {
        if (tail == null) {
            return null;
        } else {
            return tail.value;
        }
    }

    public MDeque() {
    }

    private void initialize(MDequeNode node) {
        count = 1;
        head = node;
        body = node;
        tail = node;
    }

    public void pushFront(E item) {
        if (item == null) {
            throw new IllegalArgumentException("Value cannot be null. Argument name: item.");
        } else {
            MDequeNode node = new MDequeNode(item);

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

    private void addBefore(MDequeNode newNode, MDequeNode existingNode) {
        count++;
        newNode.next = existingNode;
        newNode.previous = existingNode.previous;
        existingNode.previous.next = newNode;
        existingNode.previous = newNode;
    }

    public void addCenter(E item) {
        if (item == null) {
            throw new IllegalArgumentException("Value cannot be null. Argument name: item.");
        } else {
            MDequeNode node = new MDequeNode(item);

            if (body == null) {
                initialize(node);
            } else {
                if (count % 2 == 0) {
                    addBefore(node, body);
                } else if (body.next == null) {
                    addLast(node);
                } else {
                    addBefore(node, body.next);
                }

                body = node;
            }
        }
    }

    private void addLast(MDequeNode node) {
        count++;
        tail.next = node;
        node.previous = tail;
        tail = node;
    }

    public void addLast(E item) {
        if (item == null) {
            throw new IllegalArgumentException("Value cannot be null. Argument name: item.");
        } else {
            MDequeNode node = new MDequeNode(item);

            if (tail == null) {
                initialize(node);
            } else {
                addLast(node);
            }

            if (count % 2 == 0) {
                body = body.next;
            }
        }
    }

    public E popFront() {
        if (head == null) {
            return null;
        } else {
            if (head.next == null) {
                return clear();
            } else {
                E result = head.value;
                MDequeNode removed = head;

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

    public E popCenter() {
        if (body == null) {
            return null;
        } else {
            if (body.previous == null) {
                return popFront();
            } else if (body.next == null) {
                return popBack();
            } else {
                E result = body.value;
                MDequeNode removed = body;

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
    }

    public E popBack() {
        if (tail == null) {
            return null;
        } else {
            if (body.previous == null) {
                return clear();
            } else {
                E result = tail.value;
                MDequeNode removed = tail;

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

    private E clear() {
        E result = head.value;

        head.invalidate();
        body.invalidate();
        tail.invalidate();

        count = 0;
        head = null;
        body = null;
        tail = null;

        return result;
    }

    @Override
    public Iterator<E> iterator() {
        return new MDequeIterator();
    }
}
