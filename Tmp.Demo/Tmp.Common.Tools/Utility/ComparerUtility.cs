using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmp.Common.Tools.Utility
{
    public class ComparerUtility<T, V> : IEqualityComparer<T>
    {
        private Func<T, V> keySelector;
        private IEqualityComparer<V> comparer;

        /// <summary>
        /// 初始化比较  
        /// </summary>
        /// <param name="keySelector">对象</param>
        /// <param name="comparer">比较方法</param>
        public ComparerUtility(Func<T, V> keySelector, IEqualityComparer<V> comparer)
        {
            this.keySelector = keySelector;
            this.comparer = comparer;
        }

        public ComparerUtility(Func<T, V> keySelector)
            : this(keySelector, EqualityComparer<V>.Default)
        { }

        /// <summary>
        /// 比较两个对象
        /// </summary>
        /// <param name="x">A</param>
        /// <param name="y">B</param>
        /// <returns></returns>
        public bool Equals(T x, T y)
        {
            return comparer.Equals(keySelector(x), keySelector(y));
        }

        /// <summary>
        /// 获取指定对象的哈希代码
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(T obj)
        {
            return comparer.GetHashCode(keySelector(obj));
        }
    }
}
