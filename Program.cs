﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructureVisualization
{
    class Program
    {
        static void Main(string[] args)
        {
           
            //binary tree
            {
                BinaryTree testTree = new BinaryTree();
                Random rnd = new Random();
                for (int i = 0; i < 50; i++) testTree.Insert(rnd.Next(1, 10000));
                //testTree.DisplayTree();

                List<String> testWhiteList1 = new List<String>() {"Data"};
                List<String> testBlackList1 = new List<string>() { "duplicateData" };
                DataStructureVisualizer.Visualize(testTree, testWhiteList1, testBlackList1);
            }

            
            //double linked list for testing
            {
                DoubleLinkedList testDoubleList = new DoubleLinkedList();
                testDoubleList.Insert("1");
                testDoubleList.Insert("2");
                testDoubleList.Insert("3");
                testDoubleList.Insert("4");
                testDoubleList.Insert("5");

                //DataStructureVisualizer.Visualize(testDoubleList);
                List<String> testList3 = new List<String>() { "Title" };
                DataStructureVisualizer.Visualize(testDoubleList, testList3);
            }
           

            //tests with skip list implementation 
            {

                CSKicksCollection.SkipList<int> testSkipList = new CSKicksCollection.SkipList<int>();
                Random rnd2 = new Random();
                for (int i = 0; i < 2; i++) testSkipList.Add(rnd2.Next(1, 10000));

                var SLWrapper = new CSKicksCollection.Wrapper<CSKicksCollection.SkipList<int>>(testSkipList);

                List<String> testWL1 = new List<String>() {"Levels"};
                List<String> testBL1 = new List<string>() {"maxLevels", "Value", "value", "Next", "next" };

                DataStructureVisualizer.Visualize(SLWrapper, testWL1, testBL1);
            }
            


            //test with Person object
            {
                Person herbert = new Person("Herbert", 56);
                herbert.Spouse = new Person("Karin", 56);

                Person david = new Person("David", 22);
                Person fabian = new Person("Fabian", 24);

                herbert.kids.Add(david);
                herbert.kids.Add(fabian);

                Person alex = new Person("Alex", 24);
                Person nico = new Person("Nico", 20);

                david.friends.Add(alex);
                david.friends.Add(nico);

                herbert.friends.Add(alex);

                Person testPerson = new Person("David", 22);

                IEnumerable<string> whiteList = new List<string>() {"Age", "Name", "friends"};
                IEnumerable<string> blackList = new List<string>() { "friends" };
                DataStructureVisualizer.Visualize(testPerson);
                DataStructureVisualizer.Visualize(testPerson, whiteList);
                DataStructureVisualizer.Visualize(testPerson, whiteList, blackList);


                PersonDB personDB = new PersonDB();
                personDB.Data = new Person[3];
                personDB.Data[0] = herbert;
                personDB.Data[1] = alex;
                personDB.Data[2] = nico;

                DataStructureVisualizer.Visualize(personDB);

            }

            //test with queue
            {
                Queue<Person> testQueue = new Queue<Person>();

                var hans = new Person("Hans", 22);
                var david = new Person("David", 22);
                var felix = new Person("Felix", 21);
                var susi = new Person("Susi", 20);
                hans.Spouse = susi;
                susi.friends.Add(david);
                susi.friends.Add(felix);
                david.friends.Add(felix);
                david.friends.Add(hans);
                david.friends.Add(susi);


                testQueue.Enqueue(david);
                testQueue.Enqueue(felix);
                testQueue.Enqueue(hans);


                List<string> queueWL = new List<string>() { "_array" };
                DataStructureVisualizer.Visualize(testQueue);

            }
            
            //test with Collections.List
            {
                List<int> testlist = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
                List<string> whitelist = new List<string>() { "_items" };
                DataStructureVisualizer.Visualize(testlist, whitelist);
            }

            //test with array
            {
                int[] testArr = new int[10];
                for (int i = 0; i < 10; i++) testArr[i] = i;

                DataStructureVisualizer.Visualize(testArr);

            }




            Console.WriteLine("done");
            Console.ReadLine();
        }
    }
}
