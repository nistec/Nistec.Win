using System;
using System.Collections.Generic;
using System.Text;

namespace Nistec.GridView
{
    /// <summary>
    /// GridRelation
    /// </summary>
    public struct GridRelation
    {
        /// <summary>
        /// PrimaryTableName
        /// </summary>
        public string PrimaryTableName;
        /// <summary>
        /// ParentTableName
        /// </summary>
        public string ParentTableName;
        /// <summary>
        /// ChiledTableName
        /// </summary>
        public string ChiledTableName;
        /// <summary>
        /// RelationName
        /// </summary>
        public string RelationName;
        /// <summary>
        /// GridRelation ctor
        /// </summary>
        /// <param name="primaryTableName"></param>
        /// <param name="relationName"></param>
        /// <param name="parentTableName"></param>
        /// <param name="chiledTableName"></param>
        public GridRelation(string primaryTableName,string relationName,string parentTableName,string chiledTableName)
        {
            PrimaryTableName = primaryTableName;
            RelationName=relationName;
            ParentTableName = parentTableName;
            ChiledTableName = chiledTableName;
        }
    }
}
