namespace MovieManage
{


    //CAB301 assessment 1 - 2022
    //The implementation of MemberCollection ADT
    using System;
    using System.Linq;


    public class MemberCollection : IMemberCollection
    {
        // Fields
        private int capacity;
        private int count;
        private Member[] members; //make sure members are sorted in dictionary order

        // Properties

        // get the capacity of this member colllection 
        // pre-condition: nil
        // post-condition: return the capacity of this member collection and this member collection remains unchanged
        public int Capacity { get { return capacity; } }

        // get the number of members in this member colllection 
        // pre-condition: nil
        // post-condition: return the number of members in this member collection and this member collection remains unchanged
        public int Number { get { return count; } }

        // get the member array
        public Member[] Members { get { return members; } }



        // Constructor - to create an object of member collection 
        // Pre-condition: capacity > 0
        // Post-condition: an object of this member collection class is created

        public MemberCollection(int capacity)
        {
            if (capacity > 0)
            {
                this.capacity = capacity;
                members = new Member[capacity];
                count = 0;
            }
        }

        // check if this member collection is full
        // Pre-condition: nil
        // Post-condition: return ture if this member collection is full; otherwise return false.
        public bool IsFull()
        {
            return count == capacity;
        }

        // check if this member collection is empty
        // Pre-condition: nil
        // Post-condition: return ture if this member collection is empty; otherwise return false.
        public bool IsEmpty()
        {
            return count == 0;
        }

        // Add a new member to this member collection
        // Pre-condition: this member collection is not full
        // Post-condition: a new member is added to the member collection and the members are sorted in ascending order by their full names;
        // No duplicate will be added into this the member collection
        public void Add(IMember member)
        {
            // To be implemented by students in Phase 1
            if (!IsFull())
            {
                this.members[count] = (Member)member;
                count++;

                int min;
                for (int i = 0; i <= (count - 2); i++)
                {
                    min = i;
                    for (int j = (i + 1); j <= (count - 1); j++)
                        if (this.members[j].CompareTo(this.members[min]) == -1)
                            min = j;
                    //swap members[i] and members[min]
                    member = this.members[i];
                    this.members[i] = this.members[min];
                    this.members[min] = (Member)member;
                }
            }
        }

        // Remove a given member out of this member collection
        // Pre-condition: nil
        // Post-condition: the given member has been removed from this member collection, if the given meber was in the member collection
        public void Delete(IMember aMember)
        {
            // To be implemented by students in Phase 1
            if (count == 0) {
                return;
            }

            Member[] temp = new Member[members.Length];

            int i;
            for (i = 0; i < count;) {
                if (members[i].CompareTo(aMember) != 0) {
                    temp[i] = members[i];
                    i++;
                } else {
                    count--;
                    break;
                }
            }

            for (int j = i; j < count; j++) {
                temp[j] = members[j + 1];
            }

            members = temp;
        }

        // Search a given member in this member collection 
        // Pre-condition: nil
        // Post-condition: return true if this memeber is in the member collection; return false otherwise; member collection remains unchanged
        public bool Search(IMember member)
        {
            // To be implemented by students in Phase 1
            double l = 0;
            double r = count - 1;
            while (l <= r)
            {
                int m = (int)Math.Floor((l + r) / 2);
                if (member.CompareTo(this.members[m]) == 0)
                {
                    return true;
                }
                else if (member.CompareTo(this.members[m]) == -1)
                {
                    r = m - 1;
                }
                else
                {
                    l = m + 1;
                }

            }
            return false;
        }

        public IMember SearchMem(IMember member)
        {
            // To be implemented by students in Phase 1
            double l = 0;
            double r = count - 1;
            while (l <= r)
            {
                int m = (int)Math.Floor((l + r) / 2);
                if (member.CompareTo(this.members[m]) == 0)
                {
                    return this.members[m];
                }
                else if (member.CompareTo(this.members[m]) == -1)
                {
                    r = m - 1;
                }
                else
                {
                    l = m + 1;
                }

            }
            return null;
        }
        // Remove all the members in this member collection
        // Pre-condition: nil
        // Post-condition: no member in this member collection 
        public void Clear()
        {
            for (int i = 0; i < count; i++)
            {
                this.members[i] = null;
            }
            count = 0;
        }

        // Return a string containing the information about all the members in this member collection.
        // The information includes last name, first name and contact number in this order
        // Pre-condition: nil
        // Post-condition: a string containing the information about all the members in this member collection is returned
        public string ToString()
        {
            string s = "";
            for (int i = 0; i < count; i++)
                s = s + members[i].ToString() + "\n";
            return s;
        }


    }

}