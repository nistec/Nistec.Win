namespace Nistec.Charts
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Reflection;
    using System.Security.Permissions;
    using System.Web;
    using System.Web.UI;

    /// <summary>Represents a collection of <see cref="T:ChartHotSpot"></see> objects inside an <see cref="T:ChartImageMap"></see> control. This class cannot be inherited.</summary>
    [Editor("System.Web.UI.Design.WebControls.HotSpotCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), AspNetHostingPermission(SecurityAction.LinkDemand, Level=AspNetHostingPermissionLevel.Minimal)]
    [Serializable]
    public sealed class ChartHotSpotCollection : ChartStateManagedCollection
    {
        private static readonly Type[] knownTypes = new Type[] { typeof(ChartCircleHotSpot), typeof(ChartRectangleHotSpot), typeof(ChartPolygonHotSpot) };

        /// <summary>Appends a specified <see cref="T:ChartHotSpot"></see> object to the end of the <see cref="T:ChartHotSpotCollection"></see> collection.</summary>
        /// <returns>The index at which the object was added to the collection.</returns>
        /// <param name="spot">The <see cref="T:ChartHotSpot"></see> object to append to the collection. </param>
        public override int Add(ChartHotSpot spot)
        {
            return ((IList) this).Add(spot);
        }

        protected override object CreateKnownType(int index)
        {
            switch (index)
            {
                case 0:
                    return new ChartCircleHotSpot();

                case 1:
                    return new ChartRectangleHotSpot();

                case 2:
                    return new ChartPolygonHotSpot();
            }
            throw new ArgumentOutOfRangeException("HotSpotCollection_InvalidTypeIndex");
        }

        protected override Type[] GetKnownTypes()
        {
            return knownTypes;
        }

        /// <summary>Inserts a specified <see cref="T:ChartHotSpot"></see> object into the <see cref="T:ChartHotSpotCollection"></see> collection at the specified index location.</summary>
        /// <param name="spot">The <see cref="T:ChartHotSpot"></see> object to add to the collection. </param>
        /// <param name="index">The array index at which to add the <see cref="T:ChartHotSpot"></see> object. </param>
        public override void Insert(int index, ChartHotSpot spot)
        {
            ((IList) this).Insert(index, spot);
        }

        protected override void OnValidate(object o)
        {
            base.OnValidate(o);
            if (!(o is ChartHotSpot))
            {
                throw new ArgumentException("HotSpotCollection_InvalidType");
            }
        }

        /// <summary>Removes the specified <see cref="T:ChartHotSpot"></see> object from the <see cref="T:ChartHotSpotCollection"></see> collection.</summary>
        /// <param name="spot">The <see cref="T:ChartHotSpot"></see> object to remove from the collection. </param>
        public void Remove(ChartHotSpot spot)
        {
            ((IList) this).Remove(spot);
        }

        /// <summary>Removes the <see cref="T:ChartHotSpot"></see> object at the specified index location from the collection.</summary>
        /// <param name="index">The array index from which to remove the <see cref="T:ChartHotSpot"></see> object. </param>
        public override void RemoveAt(int index)
        {
            ((IList) this).RemoveAt(index);
        }

        protected override void SetDirtyObject(object o)
        {
            ((ChartHotSpot)o).SetDirty();
        }

        /// <summary>Gets a reference to the <see cref="T:ChartHotSpot"></see> object at the specified index in the <see cref="T:ChartHotSpotCollection"></see> collection.</summary>
        /// <returns>The <see cref="T:ChartHotSpot"></see> object at the specified index in the <see cref="T:ChartHotSpotCollection"></see> collection.</returns>
        /// <param name="index">The ordinal index value that specifies the location of the <see cref="T:ChartHotSpot"></see> object in the collection. </param>
        public ChartHotSpot this[int index]
        {
            get
            {
                return (ChartHotSpot)this[index];
            }
        }
    }
}

