using System;
using System.Reflection;

namespace RiggVar.Rgg
{
    public class TEnumSet
    {
        private int count = 0;
        private Type? enumType;
        private bool[] b = new bool[0];

        public TEnumSet(Type t)
        {
            if (t.GetTypeInfo().IsEnum)
            {
                count = High(t) - Low(t);
                enumType = t;
                b = new bool[count];
            }
        }
        public void Assign(object source)
        {
            if (source is TEnumSet)
            {
                TEnumSet f = (TEnumSet)source;
                count = f.count;
                enumType = f.enumType;
                b = new bool[count];
                for (int i = 0; i < count; i++)
                {
                    b[i] = f.b[i];
                }
            }
        }
        public void Clear()
        {
            for (int i = 0; i < count; i++)
            {
                b[i] = false;
            }
        }
        public int Count => count;
        public bool IsEmpty
        {
            get
            {
                for (int i = 0; i < count; i++)
                {
                    if (b[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        public static int Low(Type t)
        {
            return 0;
        }
        public static int High(Type t)
        {
            return Enum.GetValues(t).Length;
        }
        public bool IsMember(int i)
        {
            if ((i >= 0) && (i < count))
            {
                return b[i];
            }
            else
            {
                return false;
            }
        }
        public void Exclude(int i)
        {
            if ((i >= 0) && (i < count))
            {
                b[i] = false;
            }
        }
        public void Include(int i)
        {
            if ((i >= 0) && (i < count))
            {
                b[i] = true;
            }
        }
    }
}
