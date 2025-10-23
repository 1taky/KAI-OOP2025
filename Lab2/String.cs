using System;

namespace Lab2
{
    public class MyString : IComparable<MyString>
    {
        public string Value { get; set; }
        public int Length => Value.Length;

        public MyString(string value)
        {
            Value = value;
        }

        public bool ContainsSubstring(string substring)
        {
            return Value.Contains(substring);
        }

        public void InsertSubstring(int index, string substring)
        {
            if (index < 0 || index > Value.Length)
                throw new ArgumentOutOfRangeException(nameof(index));
            Value = Value.Insert(index, substring);
        }

        public void ReplaceSubstring(string oldSub, string newSub)
        {
            Value = Value.Replace(oldSub, newSub);
        }

        public override string ToString()
        {
            return $"{Value} (Довжина: {Length})";
        }

        public int CompareTo(MyString other)
        {
            if (other == null) return 1;
            return this.Length.CompareTo(other.Length);
        }
    }
}

