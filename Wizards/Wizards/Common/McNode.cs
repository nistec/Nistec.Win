using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Nistec.WinForms;

namespace Nistec.Wizards
{

    //public class ToolBarCollection : List<McToolBar>
    //{

    //    public McToolBar Find(object node)
    //    {
    //        nodeToFind = node;
    //        return this.Find(FindNode);
    //    }
    //    private static object nodeToFind;

    //    private static bool FindNode(object node)
    //    {
    //        return node.Equals(nodeToFind);
    //    }

    //    public static bool CompareNode(McNode node1, McNode node2)
    //    {
    //        return node1.Equals(node2);
    //    }
    //}

    public class McNodeCollection : List<McNode>
    {

        public McNode Find(object node)
        {
            nodeToFind = node;
            return this.Find(FindNode);
        }
        private static object nodeToFind;

        private static bool FindNode(object node)
        {
            return node.Equals(nodeToFind);
        }

        public static bool CompareNode(McNode node1, McNode node2)
        {
            return node1.Equals(node2);
        }
    }

    public class McNode
    {
        public readonly int Index;
        public readonly int PageIndex;
        public readonly int BarIndex;
        public readonly string Name;
        public readonly object Value;

        private TreeNode _Node;

        public TreeNode Node
        {
            get { return _Node; }
            set { _Node = value; }
        }

        private string[] _Param;

        public string[] Param
        {
            get { return _Param; }
            set { _Param = value; }
        }

        public McNode(string Name, object Value, int Index, int PageIndex, int BarIndex)
        {
            this.Index = Index;
            this.PageIndex = PageIndex;
            this.BarIndex = BarIndex;
            this.Name = Name;
            this.Value = Value;
            _Node = null;
            _Param = null;
        }

        public McNode(string Name, object Value)
        {
            this.Index = -1;
            this.PageIndex = -1;
            this.BarIndex = -1;
            this.Name = Name;
            this.Value = Value;
            _Node = null;
            _Param = null;
        }

        public bool IsEmpty
        {
            get { return string.IsNullOrEmpty(this.Name); }
        }

        //public McNode Empty
        //{
        //    get { return new McNode(); }
        //}

        public override string ToString()
        {
            return this.Name;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(string))
            {
                return obj.ToString() == this.Name;
            }
            if (obj.GetType() == typeof(int))
            {
                return Convert.ToInt32(obj) == this.Index;
            }
            if (obj.GetType() == typeof(McNode))
            {
                return ((McNode)obj).Name == this.Name;
            }
            return false;

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
   
        public McNode Clone()
        {
            McNode node = new McNode(this.Name,this.Value,this.Index,this.PageIndex,this.BarIndex);
            node.Param = this.Param;
            node.Node = this.Node;
            return node;
        }
    }
}
