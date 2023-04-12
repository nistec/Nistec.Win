using System;
using System.Data;
using System.Collections;


namespace mControl.WinCtl.Controls
{

	public class Relation
	{
		private Relation()
		{
			throw new  ArgumentException("CanNotUse empty relation");
		}

		public Relation(string parentColumnName,string childColumnName)
			:this(new string[]{ parentColumnName},new string[]{ childColumnName})
		{

		}

		public Relation(string[] parentColumnsName,string[] childColumnsName)
			:this(parentColumnsName,childColumnsName,null)
		{}

		public Relation(string[] parentColumnsName,string[] childColumnsName,string foreignKey)
			:this(parentColumnsName,childColumnsName,foreignKey,null)
		{}

		public Relation(string[] parentColumnsName,string[] childColumnsName,string foreignKey,ITableMapping  tbleMapping)
		{
			if (parentColumnsName.Length != childColumnsName.Length)
			{
				throw  new ArgumentException("DataRelation_KeyLengthMismatch");
			}
			
			_ParentColumnsName=parentColumnsName;
			_ChildColumnsName=childColumnsName;
			_ForiegnKey=foreignKey;
			tableMapping=tbleMapping;
		}

		public static Relation[] DataRelationConvert(DataSet ds)
		{

			System.Data.DataRelationCollection relations=ds.Relations;
			if(relations==null || relations.Count==0)
			{
				return null;
			}
			Relation[] rels=new Relation[ds.Relations.Count];
			string[] colsP=null;
			string[] colsC=null;

			for(int i=0 ;i < rels.Length;i++)
			{
				colsP=new string[relations[i].ParentColumns.Length];
				for(int j=0 ;j < relations[i].ParentColumns.Length;j++)
				{
					colsP[j]=relations[i].ParentColumns[j].ColumnName;
				}
				colsC=new string[relations[i].ChildColumns.Length];
				for(int j=0 ;j < relations[i].ChildColumns.Length;j++)
				{
					colsC[j]=relations[i].ParentColumns[j].ColumnName;
				}

				rels[i]=new Relation(colsP,colsC);
				//rels[i].RelationName=ds.Tables[i].TableName;
				//rels[i].commandSelect=
			}
			return rels;
		}

		public string[] ParentColumnsName
		{
			get{return _ParentColumnsName;}
		}
		public string[] ChildColumnsName
		{
			get{return _ChildColumnsName;}
		}
		public string RelationName
		{
			get{return _RelationName;}
		}
		public string ForiegnKey
		{
			get{return _ForiegnKey;}
		}
		public string CommandSelect
		{
			get{return _commandSelect;}
		}
		public ITableMapping TableMapping
		{
			get{return tableMapping;}
		}
//		public Compute[] ComputeList
//		{
//			get{return _ComputeList;}
//			set{_ComputeList=value;}
//		}

		internal string _RelationName;
		internal string[] _ParentColumnsName;
		internal string[] _ChildColumnsName;
		internal string _ForiegnKey;
		internal string _commandSelect;
		internal System.Data.ITableMapping  tableMapping; //=new TableMapping();
		
//		internal Compute[] _ComputeList;
//
//		public void ComuteExpresion(DataView dv, string filter)
//		{
//			if(dv==null || ComputeList == null)return ;
//            
//			foreach(Compute c in ComputeList)
//			{
//				object res=dv.Table.Compute(c._expresion,filter);
//				c._result=res;
//			}
//		}

	}


	public class RelationCollection : CollectionBase
	{

		public void Add(Relation rl)
		{
			base.List.Add(rl);
		}

		public Relation	this [int index]
		{
			get{return (Relation) base.List[index] as Relation;}
		}
		public Relation	this [string relationName]
		{
			get
			{
				int i=0;
				foreach(Relation r in this.List)
				{
					if(r._RelationName.Equals(relationName))
					{break;}
					i++;
				}
				return (Relation) base.List[i] as Relation;
			}
		}

	}


	public class Compute
	{
		private Compute()
		{
			throw new  ArgumentException("CanNotUse empty Compute");
		}

		public Compute(string expression,string name)
		{
			if (expression.Length == 0)
			{
				throw  new ArgumentException("Compute expression can not be empy");
			}
			_expression=expression;
			_name=name;
		}

		public object ComuteExpression(DataView dv,string expression,string filter)
		{
			if(dv==null)return null;
			_result=  dv.Table.Compute(expression,filter);
			return _result;
		}

		public string ComputeName
		{
			get{return _name;}
		}
		public string Expression
		{
			get{return _expression;}
		}
		public object Result
		{
			get{return _result;}
		}

		internal string _name;
		internal string _expression;
		internal object _result;
	}



}
