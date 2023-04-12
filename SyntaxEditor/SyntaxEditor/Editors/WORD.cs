using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;

namespace Nistec.SyntaxEditor
{

    public class Words : List<WORD>
    {
        private int maxWidth;
        WordComparer compare;

        public int MaxWidth
        {
            get { return maxWidth; }
        }

        //public List<WORD> WordList;

        public Words()
        {
            maxWidth=0;
            compare = new WordComparer();
        }

        public new void Add(WORD w)
        {
            base.Add(w);
            maxWidth = Math.Max(maxWidth, w.Width);
        }

        public void AddRange(WORD[] w)
        {
            base.AddRange(w);
            ComputeWidth();
        }
        public void AddRange(Words w)
        {
            base.AddRange(w);
            maxWidth = Math.Max(maxWidth, w.MaxWidth);
        }
        public void Add(string s, MemberType type)
        {
            WORD w = new WORD(s, type);
            base.Add(w);
            maxWidth = Math.Max(maxWidth, w.Width);
        }

        public void AddRange(string[] list, MemberType type)
        {
            foreach (string s in list)
            {
                WORD w = new WORD(s, type);
                base.Add(w);
                maxWidth = Math.Max(maxWidth, w.Width);
            }
        }

        public void AddRange(string[] list, MemberType type,string startsWith)
        {
            foreach (string s in list)
            {
                if (s.StartsWith(startsWith))
                {
                    WORD w = new WORD(s, type);
                    base.Add(w);
                    maxWidth = Math.Max(maxWidth, w.Width);
                }
            }
        }

        public void AddRange(string[] list)
        {
            foreach (string s in list)
            {
                WORD w = new WORD(s, MemberType.Method);
                base.Add(w);
                maxWidth = Math.Max(maxWidth, w.Width);
            }
        }

        public Words GetWords(string word)
        {
            Words intelliList = new Words();
            int maxW = 0;
            foreach (WORD o in this)
            {
                if (o.Text.ToLower().StartsWith(word.ToLower()))
                {
                    intelliList.Add(o);
                    maxW = Math.Max(maxW, o.Width);
                }
            }
            intelliList.maxWidth = maxW;
            return intelliList;
        }
        public new void Clear()
        {
            base.Clear();
            this.maxWidth = 0;
        }
        public new void Sort()
        {
            base.Sort(compare);
        }

        public int ComputeWidth()
        {
            int maxW = 0;
            foreach (WORD o in this)
            {
                maxW = Math.Max(maxW, o.Width);
            }
            maxWidth = maxW;
            return maxWidth;
        }

        public class WordComparer : IComparer<WORD>
        {

            public int Compare(WORD wx, WORD wy)
            {
                string x = wx.Text;
                string y = wy.Text;

                if (x == null)
                {
                    if (y == null)
                    {
                        // If x is null and y is null, they're
                        // equal. 
                        return 0;
                    }
                    else
                    {
                        // If x is null and y is not null, y
                        // is greater. 
                        return -1;
                    }
                }
                else
                {
                    // If x is not null...
                    //
                    if (y == null)
                    // ...and y is null, x is greater.
                    {
                        return 1;
                    }
                    else
                    {
                        return x.CompareTo(y);
                    }
                }
            }

        }

    }

 

    public struct WORD
    {
        public const int chW = 6;
        public const int iconSpace = 40;
        
        public readonly string Text;
        //public int Position;
        public readonly int Length;
        public readonly MemberType MemberType;
        public readonly int Width;

        public WORD(string text):this(text,MemberType.Method)
        {
        }

        public WORD(string text,MemberType type)
        {
            Text = text;
            MemberType = type;
            Length = text.Length;
            Width = (Length * chW) + iconSpace;
        }

        public override string ToString()
        {
            //string s = "Type = " + MemberType + ", Text = " + Text + ", Position = " + Position + ", Length = " + Length + "\n";
            //return s;
            return Text;// "Type = " + MemberType + ", Text = " + Text + "\n";

        }
    }

    public enum IntelliMode
    {
        None,
        Auto,
        Manual
    }

    public enum MemberType
    {
        Method,
        Property,
        Field,
        Member,
        Class,
        Event,
        Reference
    }

    public enum SyntaxMode
    {
        CSharp,
        VBNET,
        CPP,
        ASPX,
        SQL,
        XML,
        Custom
    }
  
}
