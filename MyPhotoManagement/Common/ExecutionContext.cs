using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace MyPhotoManagement.Common
{
    public class ExecutionContext
    {
        [ThreadStatic]
        private static ExecutionContext current;

        public IDictionary<string, object> Items { get; internal set; }

        internal ExecutionContext()
        {
            this.Items = new Dictionary<string, object>();
        }

        public T GetValue<T>(string name, T defaultValue = default(T))
        {
            object value;
            if (this.Items.TryGetValue(name, out value))
            {
                return (T)value;
            }
            return defaultValue;
        }

        public void SetValue(string name, object value)
        {
            this.Items[name] = value;
        }

        /// <summary>
        /// 当前执行上下文
        /// </summary>
        public static ExecutionContext Current
        {
            get { return current; }
            internal set { current = value; }
        }

        public DependentContext DepedentClone()
        {
            return new DependentContext(this);
        }
    }

    [Serializable]
    public class DependentContext : ExecutionContext
    {
        public Thread OriginalThread { get; private set; }

        public DependentContext(ExecutionContext context)
        {
            OriginalThread = Thread.CurrentThread;
            this.Items = new Dictionary<string, object>(context.Items);
        }
    }
}