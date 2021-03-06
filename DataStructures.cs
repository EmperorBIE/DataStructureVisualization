﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace DataStructureVisualization
{
    public class ShowData : Attribute
    {
    }

    public class Student
    {
        public int Age {get; set;}
        public string Name {get; set;}

        public Student(int age, string name)
        {
            Age = age;
            Name = name;
        }
    }

    public class Node
    {
        //public static int someId = 0;

        public int Data { get; set; }

        public int duplicateData { get; set; }

        public Node Left { get; set; }

        public Node Right { get; set; }

        public Node(int data)
        {
            this.Data = data;
            this.duplicateData = 10;
        }
    }

    public class BinaryTree
    {
        private Node _root;
        public BinaryTree()
        {
            _root = null;
        }
        public void Insert(int data)
        {
            // 1. If the tree is empty, return a new, single node 
            if (_root == null)
            {
                _root = new Node(data);
                return;
            }
            // 2. Otherwise, recur down the tree 
            InsertRec(_root, new Node(data));
        }
        private void InsertRec(Node root, Node newNode)
        {
            if (root == null)
                root = newNode;

            if (newNode.Data < root.Data)
            {
                if (root.Left == null)
                    root.Left = newNode;
                else
                    InsertRec(root.Left, newNode);

            }
            else
            {
                if (root.Right == null)
                    root.Right = newNode;
                else
                    InsertRec(root.Right, newNode);
            }
        }
        private void DisplayTree(Node root)
        {
            if (root == null) return;

            DisplayTree(root.Left);
            System.Console.Write(root.Data + " ");
            DisplayTree(root.Right);
        }
        public void DisplayTree()
        {
            DisplayTree(_root);
        }

    }

    class PersonDB
    {
        private int personCount = 5;
        public Person[] Data { get; set; }
    }


    public class SingleLink
    {
        public int Value { get; set; }
        public SingleLink NextLink { get; set; }

        public SingleLink(int val)
        {
            Value = val;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }



    public class SinglyLinkedList
    {
        private SingleLink _first;
        public bool IsEmpty
        {
            get
            {
                return _first == null;
            }
        }
        public SinglyLinkedList()
        {
            _first = null;
        }

        public SingleLink Insert(int val)
        {
            // Creates a link, sets its link to the first item and then makes this the first item in the list.
            SingleLink link = new SingleLink(val);
            link.NextLink = _first;
            _first = link;
            return link;
        }
    }

    //from https://yetanotherchris.dev/csharp/linked-list-and-double-linked-list-in-csharp/
    public class DoubleLink
    {
        public int Value { get; set; }
        public DoubleLink PreviousLink { get; set; }
        public DoubleLink NextLink { get; set; }

        public DoubleLink(int val)
        {
            Value = val;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    public class DoublyLinkedList
    {
        private DoubleLink _first;
        public bool IsEmpty
        {
            get
            {
                return _first == null;
            }
        }
        public DoublyLinkedList()
        {
            _first = null;
        }

        public DoubleLink Insert(int val)
        {
            // Creates a link, sets its link to the first item and then makes this the first item in the list.
            DoubleLink link = new DoubleLink(val);
            link.NextLink = _first;
            if (_first != null)
                _first.PreviousLink = link;
            _first = link;
            return link;
        }

        public DoubleLink Delete()
        {
            // Gets the first item, and sets it to be the one it is linked to
            DoubleLink temp = _first;
            if (_first != null)
            {
                _first = _first.NextLink;
                if (_first != null)
                    _first.PreviousLink = null;
            }
            return temp;
        }


        public override string ToString()
        {
            DoubleLink currentLink = _first;
            StringBuilder builder = new StringBuilder();
            while (currentLink != null)
            {
                builder.Append(currentLink);
                currentLink = currentLink.NextLink;
            }
            return builder.ToString();
        }


        ///// New operations
        public void InsertAfter(DoubleLink link, int val)
        {
            if (link == null) return;
            DoubleLink newLink = new DoubleLink(val);
            newLink.PreviousLink = link;
            // Update the 'after' link's next reference, so its previous points to the new one
            if (link.NextLink != null)
                link.NextLink.PreviousLink = newLink;
            // Steal the next link of the node, and set the after so it links to our new one
            newLink.NextLink = link.NextLink;
            link.NextLink = newLink;
        }
    }

    class StringWrapper
    {
        [ShowData]
        public String content;

        public StringWrapper(String s)
        {
            this.content = s;
        }
    }

    class Person
    {
        public String Name { get; }
        public int Age { get; }
        public Person Spouse { get; set; }
        public List<Person> kids;
        public List<Person> friends;

        private Person() { }

        public Person(String name, int age)
        {
            this.Name = name;
            this.Age = age;
            this.kids = new List<Person>();
            this.friends = new List<Person>();
        }
    }

    public struct KeyValue<K, V>
    {
        public K Key { get; set; }
        public V Value { get; set; }
    }


    public class FixedSizeGenericHashTable<K, V>
    {
        private readonly int size;
        private readonly LinkedList<KeyValue<K, V>>[] items;

        public FixedSizeGenericHashTable(int size)
        {
            this.size = size;
            items = new LinkedList<KeyValue<K, V>>[size];
        }

        protected int GetArrayPosition(K key)
        {
            int position = key.GetHashCode() % size;
            return Math.Abs(position);
        }

        public V Find(K key)
        {
            int position = GetArrayPosition(key);
            LinkedList<KeyValue<K, V>> linkedList = GetLinkedList(position);
            foreach (KeyValue<K, V> item in linkedList)
            {
                if (item.Key.Equals(key))
                {
                    return item.Value;
                }
            }

            return default(V);
        }

        public void Add(K key, V value)
        {
            int position = GetArrayPosition(key);
            LinkedList<KeyValue<K, V>> linkedList = GetLinkedList(position);
            KeyValue<K, V> item = new KeyValue<K, V>() { Key = key, Value = value };
            linkedList.AddLast(item);
        }

        public void Remove(K key)
        {
            int position = GetArrayPosition(key);
            LinkedList<KeyValue<K, V>> linkedList = GetLinkedList(position);
            bool itemFound = false;
            KeyValue<K, V> foundItem = default(KeyValue<K, V>);
            foreach (KeyValue<K, V> item in linkedList)
            {
                if (item.Key.Equals(key))
                {
                    itemFound = true;
                    foundItem = item;
                }
            }

            if (itemFound)
            {
                linkedList.Remove(foundItem);
            }
        }

        protected LinkedList<KeyValue<K, V>> GetLinkedList(int position)
        {
            LinkedList<KeyValue<K, V>> linkedList = items[position];
            if (linkedList == null)
            {
                linkedList = new LinkedList<KeyValue<K, V>>();
                items[position] = linkedList;
            }

            return linkedList;
        }

        //from https://stackoverflow.com/questions/625947/what-is-an-example-of-a-hashtable-implementation-in-c
    }

    //from https://simpledevcode.wordpress.com/2014/09/16/avl-tree-in-c/
    public class AVL
    {
        class Node
        {
            public int data;
            public Node left;
            public Node right;
            public Node(int data)
            {
                this.data = data;
            }
        }
        Node root;
        public AVL()
        {
        }
        public void Add(int data)
        {
            Node newItem = new Node(data);
            if (root == null)
            {
                root = newItem;
            }
            else
            {
                root = RecursiveInsert(root, newItem);
            }
        }
        private Node RecursiveInsert(Node current, Node n)
        {
            if (current == null)
            {
                current = n;
                return current;
            }
            else if (n.data < current.data)
            {
                current.left = RecursiveInsert(current.left, n);
                current = balance_tree(current);
            }
            else if (n.data > current.data)
            {
                current.right = RecursiveInsert(current.right, n);
                current = balance_tree(current);
            }
            return current;
        }
        private Node balance_tree(Node current)
        {
            int b_factor = balance_factor(current);
            if (b_factor > 1)
            {
                if (balance_factor(current.left) > 0)
                {
                    current = RotateLL(current);
                }
                else
                {
                    current = RotateLR(current);
                }
            }
            else if (b_factor < -1)
            {
                if (balance_factor(current.right) > 0)
                {
                    current = RotateRL(current);
                }
                else
                {
                    current = RotateRR(current);
                }
            }
            return current;
        }
        public void Delete(int target)
        {//and here
            root = Delete(root, target);
        }
        private Node Delete(Node current, int target)
        {
            Node parent;
            if (current == null)
            { return null; }
            else
            {
                //left subtree
                if (target < current.data)
                {
                    current.left = Delete(current.left, target);
                    if (balance_factor(current) == -2)//here
                    {
                        if (balance_factor(current.right) <= 0)
                        {
                            current = RotateRR(current);
                        }
                        else
                        {
                            current = RotateRL(current);
                        }
                    }
                }
                //right subtree
                else if (target > current.data)
                {
                    current.right = Delete(current.right, target);
                    if (balance_factor(current) == 2)
                    {
                        if (balance_factor(current.left) >= 0)
                        {
                            current = RotateLL(current);
                        }
                        else
                        {
                            current = RotateLR(current);
                        }
                    }
                }
                //if target is found
                else
                {
                    if (current.right != null)
                    {
                        //delete its inorder successor
                        parent = current.right;
                        while (parent.left != null)
                        {
                            parent = parent.left;
                        }
                        current.data = parent.data;
                        current.right = Delete(current.right, parent.data);
                        if (balance_factor(current) == 2)//rebalancing
                        {
                            if (balance_factor(current.left) >= 0)
                            {
                                current = RotateLL(current);
                            }
                            else { current = RotateLR(current); }
                        }
                    }
                    else
                    {   //if current.left != null
                        return current.left;
                    }
                }
            }
            return current;
        }
        public void Find(int key)
        {
            if (Find(key, root).data == key)
            {
                Console.WriteLine("{0} was found!", key);
            }
            else
            {
                Console.WriteLine("Nothing found!");
            }
        }
        private Node Find(int target, Node current)
        {

            if (target < current.data)
            {
                if (target == current.data)
                {
                    return current;
                }
                else
                    return Find(target, current.left);
            }
            else
            {
                if (target == current.data)
                {
                    return current;
                }
                else
                    return Find(target, current.right);
            }

        }
        public void DisplayTree()
        {
            if (root == null)
            {
                Console.WriteLine("Tree is empty");
                return;
            }
            InOrderDisplayTree(root);
            Console.WriteLine();
        }
        private void InOrderDisplayTree(Node current)
        {
            if (current != null)
            {
                InOrderDisplayTree(current.left);
                Console.Write("({0}) ", current.data);
                InOrderDisplayTree(current.right);
            }
        }
        private int max(int l, int r)
        {
            return l > r ? l : r;
        }
        private int getHeight(Node current)
        {
            int height = 0;
            if (current != null)
            {
                int l = getHeight(current.left);
                int r = getHeight(current.right);
                int m = max(l, r);
                height = m + 1;
            }
            return height;
        }
        private int balance_factor(Node current)
        {
            int l = getHeight(current.left);
            int r = getHeight(current.right);
            int b_factor = l - r;
            return b_factor;
        }
        private Node RotateRR(Node parent)
        {
            Node pivot = parent.right;
            parent.right = pivot.left;
            pivot.left = parent;
            return pivot;
        }
        private Node RotateLL(Node parent)
        {
            Node pivot = parent.left;
            parent.left = pivot.right;
            pivot.right = parent;
            return pivot;
        }
        private Node RotateLR(Node parent)
        {
            Node pivot = parent.left;
            parent.left = RotateRR(pivot);
            return RotateLL(parent);
        }
        private Node RotateRL(Node parent)
        {
            Node pivot = parent.right;
            parent.right = RotateLL(pivot);
            return RotateRR(parent);
        }
    }


    //====================================================
    //| Downloaded From                                  |
    //| Visual C# Kicks - http://vckicks.110mb.com/      |
    //| License - http://vckicks.110mb.com/license.html  |
    //====================================================


    namespace CSKicksCollection
    {
        public class Wrapper<T>
        {
            private T InternalObj { get; set; }

            public Wrapper(T sl)
            {
                InternalObj = sl;
            }

        }


        /// <summary>
        /// The basic data block of a Skip List
        /// </summary>
        class SkipListNode<T> : IDisposable
            where T : IComparable
        {
            private T value;
            private SkipListNode<T> next;
            private SkipListNode<T> previous;
            private SkipListNode<T> above;
            private SkipListNode<T> below;

            public virtual T Value
            {
                get { return value; }
                set { this.value = value; }
            }

            public virtual SkipListNode<T> Next
            {
                get { return next; }
                set { next = value; }
            }

            public virtual SkipListNode<T> Previous
            {
                get { return previous; }
                set { previous = value; }
            }

            public virtual SkipListNode<T> Above
            {
                get { return above; }
                set { above = value; }
            }

            public virtual SkipListNode<T> Below
            {
                get { return below; }
                set { below = value; }
            }

            public SkipListNode(T value)
            {
                this.Value = value;
            }

            public void Dispose()
            {
                value = default(T);
                next = null;
                previous = null;
                above = null;
                previous = null;
            }

            public virtual bool IsHeader()
            {
                return this.GetType() == typeof(SkipListNodeHeader<T>);
            }

            public virtual bool IsFooter()
            {
                return this.GetType() == typeof(SkipListNodeFooter<T>);
            }
        }

        /// <summary>
        /// Represents a Skip List node that is the header of a level
        /// </summary>
        class SkipListNodeHeader<T> : SkipListNode<T>
            where T : IComparable
        {
            public SkipListNodeHeader()
                : base(default(T))
            {
            }
        }

        /// <summary>
        /// Represents a Skip List node that is the footer of a level
        /// </summary>
        class SkipListNodeFooter<T> : SkipListNode<T>
            where T : IComparable
        {
            public SkipListNodeFooter()
                : base(default(T))
            {
            }
        }

        class SkipList<T> : ICollection<T>
            where T : IComparable
        {
            internal SkipListNode<T> topLeft;
            internal SkipListNode<T> bottomLeft;
            internal Random random;
            private int levels;
            private int size;
            private int maxLevels = int.MaxValue;

            public virtual int Levels
            {
                get { return levels; }
            }

            public virtual int MaxLevels
            {
                get { return maxLevels; }
                set { maxLevels = value; }
            }

            public virtual int Count
            {
                get { return size; }
            }

            public virtual bool IsReadOnly
            {
                get { return false; }
            }

            public virtual SkipListNode<T> Head
            {
                get { return bottomLeft; }
            }

            public SkipList()
            {
                topLeft = getEmptyLevel(); //create an empty level
                bottomLeft = topLeft;
                levels = 1; //update the level count
                size = 0; //no elements added
                random = new Random(); //used for adding new values
            }

            /// <summary>
            /// Creates an empty level with a header and footer node
            /// </summary>
            protected SkipListNode<T> getEmptyLevel()
            {
                SkipListNode<T> negativeInfinity = new SkipListNodeHeader<T>();
                SkipListNode<T> positiveInfinity = new SkipListNodeFooter<T>();

                negativeInfinity.Next = positiveInfinity;
                positiveInfinity.Previous = negativeInfinity;

                return negativeInfinity;
            }

            /// <summary>
            /// Randomly determines how many levels to add
            /// </summary>
            protected int getRandomLevels()
            {
                int newLevels = 0;
                while (random.Next(0, 2) == 1 && newLevels < maxLevels) //1 is heads, 0 is tails
                {
                    newLevels++;
                }
                return newLevels;
            }

            /// <summary>
            /// Removes all the empty levels leftover in the Skip List
            /// </summary>
            protected void clearEmptyLevels()
            {
                if (this.levels > 1) //more than one level, don't want to remove bottom level
                {
                    SkipListNode<T> currentNode = this.topLeft;

                    while (currentNode != this.bottomLeft) //do not remove the bottom level
                    {
                        if (currentNode.IsHeader() && currentNode.Next.IsFooter())
                        {
                            SkipListNode<T> belowNode = currentNode.Below;

                            //Remove the empty level

                            //Update pointers
                            topLeft = currentNode.Below;

                            //Remove links
                            currentNode.Next.Dispose();
                            currentNode.Dispose();

                            //Update counters
                            this.levels--;

                            currentNode = belowNode; //scan down
                        }
                        else
                            break; //a single non-emtpy level means the rest of the levels are not empty
                    }
                }
            }

            /// <summary>
            /// Add a value to the Skip List
            /// </summary>
            public virtual void Add(T value)
            {
                int valueLevels = getRandomLevels(); //determine height of value's tower

                //Add levels to entire list if necessary
                int newLevelCount = valueLevels - this.levels; //number of levels missing
                while (newLevelCount > 0)
                {
                    //Create new level
                    SkipListNode<T> newLevel = getEmptyLevel();

                    //Link down
                    newLevel.Below = this.topLeft;
                    this.topLeft.Above = newLevel;
                    this.topLeft = newLevel; //update reference to most top-left node

                    //Update counters
                    newLevelCount--;
                    this.levels++;
                }

                //Insert the value in the proper position, creating as many levels as was randomly determined
                SkipListNode<T> currentNode = this.topLeft;
                SkipListNode<T> lastNodeAbove = null; //keeps track of the upper-level nodes in a tower
                int currentLevel = this.levels - 1;

                while (currentLevel >= 0 && currentNode != null)
                {
                    if (currentLevel > valueLevels) //too high on the list, nothing would be added to this level
                    {
                        currentNode = currentNode.Below; //scan down
                        currentLevel--; //going one level lower
                        continue; //skip adding to this level
                    }

                    //Add the value to the current level

                    //Find the biggest value on the current level that is less than the value to be added
                    while (currentNode.Next != null)
                    {
                        if (!currentNode.Next.IsFooter() && currentNode.Next.Value.CompareTo(value) < 0) //smaller
                            currentNode = currentNode.Next; //keep scanning across
                        else
                            break; //the next node would be bigger than the value

                    }

                    //Insert the value right after the node found
                    SkipListNode<T> newNode = new SkipListNode<T>(value);
                    newNode.Next = currentNode.Next;
                    newNode.Previous = currentNode;
                    newNode.Next.Previous = newNode;
                    currentNode.Next = newNode;

                    //Link down/up the tower
                    if (lastNodeAbove != null) //is this node part of a tower?
                    {
                        lastNodeAbove.Below = newNode;
                        newNode.Above = lastNodeAbove;
                    }
                    lastNodeAbove = newNode; //start/continue tower

                    //Scan down
                    currentNode = currentNode.Below;
                    currentLevel--;
                }

                this.size++; //update count
            }

            /// <summary>
            /// Returns the first node whose value matches the input value
            /// </summary>
            public virtual SkipListNode<T> Find(T value)
            {
                SkipListNode<T> foundNode = this.topLeft;

                //Look for the highest-level node with an element value matching the parameter value
                while (foundNode != null && foundNode.Next != null)
                {
                    if (!foundNode.Next.IsFooter() && foundNode.Next.Value.CompareTo(value) < 0) //next node's value is still smaller
                        foundNode = foundNode.Next; //keep scanning across
                    else
                    {
                        if (!foundNode.Next.IsFooter() && foundNode.Next.Value.Equals(value)) //value found
                        {
                            foundNode = foundNode.Next;
                            break;
                        }
                        else
                            foundNode = foundNode.Below; //element not in this level, scan down
                    }
                }

                return foundNode;
            }

            /// <summary>
            /// Returns the lowest node on the first tower to match the input value
            /// </summary>
            public virtual SkipListNode<T> FindLowest(T value)
            {
                SkipListNode<T> valueNode = this.Find(value);
                return this.FindLowest(valueNode);
            }

            /// <summary>
            /// Returns the lowest node on the first tower to match the input value
            /// </summary>
            public virtual SkipListNode<T> FindLowest(SkipListNode<T> valueNode)
            {
                if (valueNode == null)
                    return null;
                else
                {
                    //Scan down to the lowest level
                    while (valueNode.Below != null)
                    {
                        valueNode = valueNode.Below;
                    }
                    return valueNode;
                }
            }

            /// <summary>
            /// Returns the highest node on the first tower to match the input value
            /// </summary>
            public virtual SkipListNode<T> FindHighest(T value)
            {
                SkipListNode<T> valueNode = this.Find(value);
                return this.FindHighest(valueNode);
            }

            /// <summary>
            /// Returns the highest node on the first tower to match the input value
            /// </summary>
            public virtual SkipListNode<T> FindHighest(SkipListNode<T> valueNode)
            {
                if (valueNode == null)
                    return null;
                else
                {
                    //Scan up to the highest level
                    while (valueNode.Above != null)
                    {
                        valueNode = valueNode.Above;
                    }
                    return valueNode;
                }
            }

            /// <summary>
            /// Returns whether a value exists in the Skip List
            /// </summary>
            public virtual bool Contains(T value)
            {
                return (this.Find(value) != null);
            }

            /// <summary>
            /// Removes a value or node from the Skip List
            /// </summary>
            public virtual bool Remove(T value)
            {
                SkipListNode<T> valueNode = this.FindHighest(value);
                return this.Remove(valueNode);
            }

            /// <summary>
            /// Removes a value or node from the Skip List
            /// </summary>
            public virtual bool Remove(SkipListNode<T> valueNode)
            {
                if (valueNode == null)
                    return false;
                else
                {
                    //Make sure node is top-level node in it's tower
                    if (valueNode.Above != null)
                        valueNode = this.FindHighest(valueNode);

                    //---Delete nodes going down the tower
                    SkipListNode<T> currentNodeDown = valueNode;
                    while (currentNodeDown != null)
                    {
                        //Remove right-left links
                        SkipListNode<T> previousNode = currentNodeDown.Previous;
                        SkipListNode<T> nextNode = currentNodeDown.Next;

                        //Link the previous and next nodes to each other
                        previousNode.Next = nextNode;
                        nextNode.Previous = previousNode;

                        SkipListNode<T> belowNode = currentNodeDown.Below; //scan down
                        currentNodeDown.Dispose(); //unlink previous

                        currentNodeDown = belowNode;
                    }

                    //update counter
                    this.size--;

                    //Clean up the Skip List by removing levels that are now empty
                    this.clearEmptyLevels();

                    return true;
                }
            }

            /// <summary>
            /// Removes all values in the Skip List
            /// </summary>
            public virtual void Clear()
            {
                SkipListNode<T> currentNode = this.Head;

                while (currentNode != null)
                {
                    SkipListNode<T> nextNode = currentNode.Next; //save reference to next node

                    if (!currentNode.IsHeader() && !currentNode.IsFooter())
                    {
                        this.Remove(currentNode);
                    }

                    currentNode = nextNode;
                }
            }

            /// <summary>
            /// Copies the values of the Skip List to an array
            /// </summary>
            public virtual void CopyTo(T[] array)
            {
                CopyTo(array, 0);
            }

            /// <summary>
            /// Copies the values of the Skip List to an array
            /// </summary>
            public virtual void CopyTo(T[] array, int startIndex)
            {
                IEnumerator<T> enumerator = this.GetEnumerator();

                for (int i = startIndex; i < array.Length; i++)
                {
                    if (enumerator.MoveNext())
                        array[i] = enumerator.Current;
                    else
                        break;
                }
            }

            /// <summary>
            /// Gets the number of levels of a value in the Skip List
            /// </summary>
            public virtual int GetHeight(T value)
            {
                SkipListNode<T> valueNode = this.FindLowest(value);
                return this.GetHeight(valueNode);
            }

            /// <summary>
            /// Gets the number of levels of a value in the Skip List
            /// </summary>
            public virtual int GetHeight(SkipListNode<T> valueNode)
            {
                int height = 0;
                SkipListNode<T> currentNode = valueNode;

                //Move all the way down to the bottom first
                while (currentNode.Below != null)
                {
                    currentNode = currentNode.Below;
                }

                //Count going back up to the top
                while (currentNode != null)
                {
                    height++;
                    currentNode = currentNode.Above;
                }

                return height;
            }

            /// <summary>
            /// Gets the enumerator for the Skip List
            /// </summary>
            public IEnumerator<T> GetEnumerator()
            {
                return new SkipListEnumerator(this);
            }

            /// <summary>
            /// Gets the enumerator for the Skip List
            /// </summary>
            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            /// <summary>
            /// Enumerator for a Skip List. Scans across the lowest level of a Skip List.
            /// </summary>
            internal class SkipListEnumerator : IEnumerator<T>
            {
                private SkipListNode<T> current;
                private SkipList<T> skipList;

                public SkipListEnumerator(SkipList<T> skipList)
                {
                    this.skipList = skipList;
                }

                public T Current
                {
                    get { return current.Value; }
                }

                object IEnumerator.Current
                {
                    get { return this.Current; }
                }

                public void Dispose()
                {
                    current = null;
                }

                public void Reset()
                {
                    current = null;
                }

                public bool MoveNext()
                {
                    if (current == null)
                        current = this.skipList.Head.Next; //Head is header node, start after
                    else
                        current = current.Next;

                    if (current != null && current.IsFooter())
                        current = null; //end of list

                    return (current != null);
                }
            }
        }

    }
}
