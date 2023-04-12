using System;
using System.Threading;

namespace MControl.Win32
{
	internal delegate void HandleChangeEventHandler(string handleType, IntPtr handleValue, int currentHandleCount);

	//internal delegate void HandleChangeEventHandler(string handleType, IntPtr handleValue, int currentHandleCount);

 
	internal sealed class HandleCollector
	{
		// Events
		internal static  event HandleChangeEventHandler HandleAdded;
		internal static  event HandleChangeEventHandler HandleRemoved;

		// Methods
		static HandleCollector()
		{
			HandleCollector.handleTypes = null;
			HandleCollector.handleTypeCount = 0;
		}

		public HandleCollector()
		{
		}

		internal static IntPtr Add(IntPtr handle, int type)
		{
			HandleCollector.handleTypes[type - 1].Add(handle);
			return handle;
		}

		internal static int RegisterType(string typeName, int expense, int initialThreshold)
		{
			lock (typeof(HandleCollector))
			{
				if ((HandleCollector.handleTypeCount == 0) || (HandleCollector.handleTypeCount == HandleCollector.handleTypes.Length))
				{
					HandleCollector.HandleType[] typeArray1 = new HandleCollector.HandleType[HandleCollector.handleTypeCount + 10];
					if (HandleCollector.handleTypes != null)
					{
						Array.Copy(HandleCollector.handleTypes, 0, typeArray1, 0, HandleCollector.handleTypeCount);
					}
					HandleCollector.handleTypes = typeArray1;
				}
				HandleCollector.handleTypes[HandleCollector.handleTypeCount++] = new HandleCollector.HandleType(typeName, expense, initialThreshold);
				return HandleCollector.handleTypeCount;
			}
		}

		internal static IntPtr Remove(IntPtr handle, int type)
		{
			return HandleCollector.handleTypes[type - 1].Remove(handle);
		}
 

		// Fields
		//private static HandleChangeEventHandler HandleAdded;
		//private static HandleChangeEventHandler HandleRemoved;
		private static int handleTypeCount;
		private static HandleCollector.HandleType[] handleTypes;

		// Nested Types
		private class HandleType
		{
			// Methods
			internal HandleType(string name, int expense, int initialThreshHold)
			{
				this.name = name;
				this.initialThreshHold = initialThreshHold;
				this.threshHold = initialThreshHold;
				this.handleCount = 0;
				this.deltaPercent = 100 - expense;
			}

			internal void Add(IntPtr handle)
			{
				bool flag1 = false;
				lock (this)
				{
					this.handleCount++;
					flag1 = this.NeedCollection();
					lock (typeof(HandleCollector))
					{
						if (HandleCollector.HandleAdded != null)
						{
							HandleCollector.HandleAdded(this.name, handle, this.GetHandleCount());
						}
					}
					if (!flag1)
					{
						return;
					}
				}
				if (flag1)
				{
					GC.Collect();
					int num1 = (100 - this.deltaPercent) / 4;
					Thread.Sleep(num1);
				}
			}

			internal int GetHandleCount()
			{
				lock (this)
				{
					return this.handleCount;
				}
			}

 
			internal bool NeedCollection()
			{
				if (this.handleCount > this.threshHold)
				{
					this.threshHold = this.handleCount + ((this.handleCount * this.deltaPercent) / 100);
					return true;
				}
				int num1 = (100 * this.threshHold) / (100 + this.deltaPercent);
				if ((num1 >= this.initialThreshHold) && (this.handleCount < ((int) (num1 * 0.9f))))
				{
					this.threshHold = num1;
				}
				return false;
			}

			internal IntPtr Remove(IntPtr handle)
			{
				lock (this)
				{
					this.handleCount--;
					this.handleCount = Math.Max(0, this.handleCount);
					lock (typeof(HandleCollector))
					{
						if (HandleCollector.HandleRemoved != null)
						{
							HandleCollector.HandleRemoved(this.name, handle, this.GetHandleCount());
						}
					}
					return handle;
				}
			}


			// Fields
			private readonly int deltaPercent;
			private int handleCount;
			private int initialThreshHold;
			internal readonly string name;
			private int threshHold;
		}
	}
 

}
