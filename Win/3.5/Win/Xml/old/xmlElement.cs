using System;
using System.Collections;

namespace MControl.Xml
{
	///	<summary>
	///	xmlElement is used by xmlReader and xmlWriter for processing.
	///	</summary>
    //[CLSCompliantAttribute(false)]
	public class xmlElement
	{
		public string _strName;
		private	ArrayList _arrayAttribNames, _arrayAttribValues;

		public xmlElement()
		{
			_arrayAttribNames = new ArrayList();
			_arrayAttribValues = new ArrayList();
		}

		public xmlElement(string strName)
		{
			_arrayAttribNames = new ArrayList();
			_arrayAttribValues = new ArrayList();

			setName(strName);
		}

		string helperBuildIndent(int nLevel)
		{
		    System.Text.StringBuilder sb=new System.Text.StringBuilder ();  
			
			for	(int i=0;i<nLevel;i++)
				sb.Append (" ");

			return sb.ToString ();
		}

		public void setName(string strName)
		{
			_strName = strName;
		}

		string getName()
		{
			return _strName;
		}
		
		string getAttribName(int nIndex)
		{
			string strAttribName = "";
			if (nIndex>-1 && nIndex<getAttribCount())
				strAttribName = (string)_arrayAttribNames[nIndex];

			return strAttribName;
		}

		string getAttribValue(int nIndex)
		{
			string strAttribValue = "";
			if (nIndex>-1 && nIndex<_arrayAttribValues.Count)
				strAttribValue = (string)_arrayAttribValues[nIndex];

			return strAttribValue;
		}

		int	getAttribCount()
		{
			return (int)_arrayAttribNames.Count;
		}

		bool findAttrib(string strAttribName)
		{
			bool bFound	= false;
			int	i=0;
			int	nSize =	(int) _arrayAttribNames.Count;
			
			while (i<nSize && !bFound)
			{
				bFound = ((string)_arrayAttribNames[i]==strAttribName);
				i++;
			}

			return bFound;
		}

		public void addAttrib(string strAttribName, string strAttribValue)
		{
			bool bFound	= false;
			int	i=0;
			int	nSize =	(int) _arrayAttribNames.Count;
			
			while (i<nSize && !bFound)
			{
				bFound = ((string)_arrayAttribNames[i]==strAttribName);
				i++;
			}
			
			if (bFound)	// already known
			{
				i--;
				_arrayAttribValues[i] = strAttribValue;
			}
			else
			{
				_arrayAttribNames.Add( strAttribName );
				_arrayAttribValues.Add( strAttribValue	);
			}
		}

		public void write(xmlWriter writer, int nDeltaLevel, bool bIndent, bool bEOL)	// for any kind	of open	tag
		{
			writer.setIndentLevel( writer.getIndentLevel()+nDeltaLevel );
            System.Text.StringBuilder sb=new System.Text.StringBuilder ();  
			if (bIndent)
				sb.Append (helperBuildIndent(writer.getIndentLevel()));
			
			sb.Append ("<");
			sb.Append ( _strName);
			int	i;
			int	nCount = getAttribCount();
	
			for	(i=0;i<nCount;i++)
			{
				sb.Append (" "); // separator between attribute pairs
				sb.Append ( _arrayAttribNames[i]);
				sb.Append ("=\"");
				sb.Append (_arrayAttribValues[i]);
				sb.Append ("\"");
			}

			sb.Append (">");
			if (bEOL)
			sb.Append ("\r\n"); //	ENDL
			
			writer.writeString(	sb.ToString () );
		}

		public void writeEmpty(xmlWriter writer, int nDeltaLevel, bool bIndent, bool bEOL)
		{
			writer.setIndentLevel( writer.getIndentLevel()+nDeltaLevel );

			System.Text.StringBuilder sb=new System.Text.StringBuilder (); 
			if (bIndent)
				helperBuildIndent(writer.getIndentLevel());
			
			sb.Append ("<"); 
			sb.Append (_strName);
			int	i;
			int	nCount = getAttribCount();
			
			for	(i=0;i<nCount;i++)
			{
				sb.Append (" "); // separator between attribute pairs
				sb.Append ( _arrayAttribNames[i]);
				sb.Append ("=\"");
				sb.Append (_arrayAttribValues[i]);
				sb.Append ("\"");
			}

			sb.Append ("></");
			sb.Append ( _strName);
			sb.Append (">");

			if (bEOL)
			sb.Append ("\r\n"); //	ENDL
			writer.writeString(	sb.ToString () );

			writer.setIndentLevel( writer.getIndentLevel()-nDeltaLevel );
		}

		public void writePInstruction(xmlWriter writer, int nDeltaLevel)
		{
			writer.setIndentLevel( writer.getIndentLevel()+nDeltaLevel );

			System.Text.StringBuilder sb=new System.Text.StringBuilder (); 
			
			sb.Append (helperBuildIndent(writer.getIndentLevel()));
			
			sb.Append ("<?");
			sb.Append ( _strName);
			
			int	i;
			int	nCount = getAttribCount();
			
			for	(i=0;i<nCount;i++)
			{
				sb.Append (" "); // separator between attribute pairs
				sb.Append (_arrayAttribNames[i]);
				sb.Append ("=\"");
				sb.Append (_arrayAttribValues[i]);
				sb.Append ("\"");
			}

			sb.Append ("?>");
			sb.Append ("\r\n"); //	ENDL
			
			writer.writeString(	sb.ToString () );
		}

		public void writeClosingTag(xmlWriter writer, int nDeltaLevel, bool bIndent, bool bEOL)
		{
			System.Text.StringBuilder sb=new System.Text.StringBuilder (); 
		
			if (bIndent)
				sb.Append ( helperBuildIndent(writer.getIndentLevel()));

			sb.Append ("</");
			sb.Append ( _strName);
			sb.Append (">");
			if (bEOL)
			sb.Append ("\r\n"); //	ENDL
			writer.writeString(	sb.ToString () );

			writer.setIndentLevel( writer.getIndentLevel()+nDeltaLevel );
		}
	}
}
