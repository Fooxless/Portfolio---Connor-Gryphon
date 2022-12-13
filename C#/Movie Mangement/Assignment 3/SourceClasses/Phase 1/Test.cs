/*

using System;
using static Member;
using static IMember;
using static MemberCollection;
using static IMemberCollection;

class Test
{
    static void Main(string[] args)
    {
        // Testing Area Phase 2
        
        Console.WriteLine("IsValidNumber");
        Console.WriteLine("10 digits - 0420221181: " + IsValidContactNumber("0420234171"));
        Console.WriteLine("No start 0 - 5420221181: " + IsValidContactNumber("5420726141"));
        Console.WriteLine("11 digits - 04202211812: " + IsValidContactNumber("04232261712"));
        Console.WriteLine("9 digits - 042022118: " + IsValidContactNumber("042627138"));
        Console.WriteLine("non digits - abcdefghi: " + IsValidContactNumber("abcdefghi"));
        Console.WriteLine("-----------------------------------------------------");


        Console.WriteLine("IsValidPin");
        Console.WriteLine("7 digits - 1234567: " + IsValidPin("1234567"));
        Console.WriteLine("6 digits - 123456: " + IsValidPin("123456"));
        Console.WriteLine("5 digits - 12345: " + IsValidPin("12345"));
        Console.WriteLine("4 digits - 1234: " + IsValidPin("1234"));
        Console.WriteLine("3 digits - 123: " + IsValidPin("123"));
        Console.WriteLine("non digits - abcd: " + IsValidPin("abcd"));
        Console.WriteLine("-----------------------------------------------------");


        Console.WriteLine("Add");
        // Member 1
        Member Member1 = new Member("Elon", "Musk");
        Member Member2 = new Member("Mark", "Zuckerberg");
        Member Member3 = new Member("Jeffrey", "Bezos");
        Member Member4 = new Member("Apple", "Musk");

        // Empty Collection
        MemberCollection MemCollection = new MemberCollection(5);
        Console.WriteLine("Empty Collection: \n" + MemCollection.ToString());

        // Add Member 1
        MemCollection.Add(Member1);
        Console.WriteLine("1 Member: \n" + MemCollection.ToString());

        // Add Member 2
        MemCollection.Add(Member2);
        Console.WriteLine("2 Members: \n" + MemCollection.ToString());

        // Add Member 3
        MemCollection.Add(Member3);
        Console.WriteLine("3 Members: \n" + MemCollection.ToString());

        // Add Member 4
        MemCollection.Add(Member4);
        Console.WriteLine("4 Members: \n" + MemCollection.ToString());

        MemberCollection MemCollection2 = new MemberCollection(2);
        Member Member5 = new Member("Bill", "Gates");
        MemCollection2.Add(Member5);
        Console.WriteLine("Second Collection: \n" + MemCollection2.ToString());
        Console.WriteLine("-----------------------------------------------------");


        Console.WriteLine("Delete\n");
        MemCollection.Delete(Member3);
        Console.WriteLine("Delete Jeff: \n" + MemCollection.ToString());
        Console.WriteLine("-----------------------------------------------------");


        Console.WriteLine("Search\n");
        Console.WriteLine($"Is Elon There?\n{ MemCollection.Search(Member1).ToString()}");
        Console.WriteLine($"Is Jeff There?\n{ MemCollection.Search(Member3).ToString()}");
        Console.WriteLine("-----------------------------------------------------");
    }

}

*/