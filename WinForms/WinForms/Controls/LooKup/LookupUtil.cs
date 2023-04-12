using System;
using System.Collections;
using System.Threading;
using System.Collections.Generic;


using System.Runtime.Remoting;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using Nistec.Win;

namespace Nistec.WinForms
{

	#region LookupColumnDisply

	internal class LookupColumnDisply
	{
		//IMcLookUp Mc;
		internal int[] m_ColWidths;
		internal string[] m_ColCaption;

		internal LookupColumnDisply()//IMcLookUp ctl)
		{
			//Mc=ctl;
		}

		internal int GetColumnsCaptionWidth(string value,bool setArrays)
		{
			try
			{
				string[] strlist=value.Split(';');
				int cnt=strlist.Length;
				if(cnt==0)
				{
					return 0;
				}
				int[] results=new int[2];
				int orderType=-1;
				if(cnt>=2)
				{
					results[0]=Types.ToInt(strlist[0],int.MinValue);
					results[1]=Types.ToInt(strlist[1],int.MinValue);

					if(results[0]==int.MinValue && results[1]==int.MinValue)
					{
						throw new InvalidCastException("Unrecognized Format");
					}
					else if(results[0]==int.MinValue )
					{
						orderType=2;//Caption;Width
					}
					else if(results[1]==int.MinValue )
					{
						orderType=3;//Width;Caption
					}
					else
					{
						orderType=1;//Width;//Width
					}
				}
				else
				{
					return -1;
				}
	
				string[] strWidth=null;
				string[] strCaption=null;
				int[] intWidth=null;
		
				if(orderType==2)//Caption;Width
				{
					strCaption=GetSplitArray(value,0,1);
					strWidth=GetSplitArray(value,1,1);
					intWidth=ConvertArrayToInt(strWidth);
					//SetColumns(strCaption,strCaption,intWidth);
					if(setArrays)
						SetColumnsInternal(strCaption,intWidth);
				}
		
				else if(orderType==3)//Width;Caption
				{
					strWidth=GetSplitArray(value,0,1);
					strCaption=GetSplitArray(value,1,1);
					intWidth=ConvertArrayToInt(strWidth);
					//SetColumns(strCaption,strCaption,intWidth);
					if(setArrays)
						SetColumnsInternal(strCaption,intWidth);
				}
				else //orderType=0;//Width;//Width
				{
					strWidth=GetSplitArray(value,0,0);
					intWidth=ConvertArrayToInt(strWidth);
					//SetColumns(intWidth);
					if(setArrays)
						this.m_ColWidths=intWidth;
				}
				return orderType;
			}
			catch(Exception ex)
			{
				MsgBox.ShowError(ex.Message);
				return -1;
			}
		}

		private string[] GetSplitArray(string value,int mode,int interval)
		{
			string[] strlist=value.Split(';');
			int cnt=strlist.Length;
			if(cnt==0)
			{
				return null;
			}
			string[] strRes=new string[cnt/(interval+1)];
			int j=0;

			for(int i=mode;i<cnt;i++)
			{
				strRes[j]=strlist[i];
				j++;
				i+=interval;
			}
			return strRes;
		}

		private int[] ConvertArrayToInt(string[] value)
		{
			int[] intWidths=new int[value.Length];
			for(int i=0;i< value.Length;i++)
			{
				int res=int.Parse(value[i]);
				if(res<0 || res > 1000)
				{
					throw new InvalidCastException("Value must be between 0 and 1000");
				}
				intWidths[i]=res;
			}
			return intWidths;
		}

		private int[] ParseColumnsWidth(string value)
		{
			string[] strWidths=value.Split(';');
			int cnt=strWidths.Length;
			int[] intWidths=new int[cnt];
			try
			{
				for(int i=0;i< strWidths.Length;i++)
				{
					//intWidths[i]=Types.StringToInt(strWidths[i],-1);
					int res=int.Parse(strWidths[i]);
					if(res<0 || res > 1000)
					{
						throw new InvalidCastException("Value must be between 0 and 1000");
					}
					intWidths[i]=res;
				}
				return intWidths;
			}
			catch(Exception ex)
			{
				MsgBox.ShowError(ex.Message);
				return null;
			}
		}

		private void SetColumnsInternal(string[]Caption,int[] ColumnWidth )
		{
			this.m_ColWidths=ColumnWidth;
			this.m_ColCaption=Caption;
		}

	}


	#endregion

 	
}
