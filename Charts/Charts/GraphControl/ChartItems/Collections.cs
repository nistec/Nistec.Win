using System;
using System.Collections.Generic;
using System.Text;

namespace Nistec.Charts
{
    [Serializable]
    public class KeyItemCollection : List<KeyItem>
    {

        public int Add(string name)
        {
            base.Add(new KeyItem(name));
            return base.Count - 1;
        }

        public void AddRange(KeyItem[] items)
        {
            base.AddRange(items);
        }
        public void AddRange(string[] items)
        {
            foreach (string s in items)
                Add(s);
        }
    }
    [Serializable]
    public class ImageItemCollection : List<ImageItem>
    {

        public int Add(string name)
        {
            base.Add(new ImageItem(name));
            return base.Count - 1;
        }

        public void AddRange(ImageItem[] items)
        {
            base.AddRange(items);
        }
        public void AddRange(string[] items)
        {
            foreach (string s in items)
                Add(s);
        }
    }
    [Serializable]
    public class DataItemCollection : List<DataItem>
    {

        public int Add(string name)
        {
            base.Add(new DataItem(name));
            return base.Count - 1;
        }

        public void AddRange(DataItem[] items)
        {
            base.AddRange(items);
        }
        public void AddRange(string[] items)
        {
            foreach(string s in items)
            Add(s);
        }

    }
    [Serializable]
    public class ColorItemCollection : List<ColorItem>
    {
        public int Add(string name)
        {
            base.Add(new ColorItem(name));
            return base.Count - 1;
        }

        public void AddRange(ColorItem[] items)
        {
            base.AddRange(items);
        }
        public void AddRange(string[] items)
        {
            foreach (string s in items)
                Add(s);
        }
    }
}
