
using System;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace Nistec.Printing.View.Web
{
   
        internal class McCacheReport
        {
            private Cache _cache;
            private TimeSpan _var1;
            private TimeSpan _var2;
            private Random _var3;
            internal static readonly string mtd1111 = "McCacheReport";

            internal McCacheReport(Cache var4, TimeSpan var5, TimeSpan var2)
            {
                if (var4 == null)
                {
                    throw new ArgumentNullException("reportCache");
                }
                this._cache = var4;
                this._var1 = var5;
                this._var2 = var2;
                this._var3 = new Random();
            }

            protected virtual void mtd1118(string var9, object var10, CacheItemRemovedReason var11)
            {
            }
            //mtd1120
            internal Report GetReport(string var6)
            {
                if (var6 == null)
                {
                    return null;
                }
                mtd1121 mtd = (mtd1121)this._cache[var6];
                if (mtd == null)
                {
                    throw new ApplicationException("Report not cached");
                }
                return mtd.mtd268;
            }
            //mtd1122
            internal bool Contains(string var6)
            {
                if (var6 == null)
                {
                    return false;
                }
                object obj2 = this._cache[var6];
                return (obj2 != null);
            }
            //mtd1123
            internal string Add(Report var7)
            {
                if (var7 == null)
                {
                    throw new ArgumentNullException("Report");
                }
                StringBuilder builder = new StringBuilder();
                builder.Append(var7.GetType().FullName).Append(';');
                long num = DateTime.Now.ToFileTime();
                builder.Append(num.ToString()).Append(';');
                builder.Append(this._var3.Next(0, 0x7fffffff).ToString());
                mtd1121 mtd = new mtd1121(var7, builder.ToString());
                this._cache.Insert(mtd.mtd1124, mtd, null, Cache.NoAbsoluteExpiration, this._var1, CacheItemPriority.Normal, new CacheItemRemovedCallback(this.mtd1118));
                return mtd.mtd1124;
            }

            internal static void mtd1125(HttpContext var8, TimeSpan var5, TimeSpan var2)
            {
                McCacheReport mtd = null;
                if (var8 == null)
                {
                    throw new ArgumentNullException("context");
                }
                try
                {
                    mtd = var8.Application[mtd1111] as McCacheReport;
                }
                catch (Exception)
                {
                }
                if (mtd == null)
                {
                    mtd = new McCacheReport(var8.Cache, var5, var2);
                    var8.Application.Lock();
                    var8.Application[mtd1111] = mtd;
                    var8.Application.UnLock();
                }
            }
        }

        internal class mtd1121
        {
            private Report _var0;
            private string _var1;

            internal mtd1121(Report var0, string var1)
            {
                this._var0 = var0;
                this._var1 = var1;
            }

            internal string mtd1124
            {
                get
                {
                    return this._var1;
                }
            }

            internal Report mtd268
            {
                get
                {
                    return this._var0;
                }
            }
        }
}
