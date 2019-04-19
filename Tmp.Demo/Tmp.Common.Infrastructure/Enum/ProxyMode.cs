using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPE.Common.Infrastructure.Enum
{
    /// <summary>
    /// 创建代理类时采用的模式
    /// </summary>
    public enum ProxyMode
    {
        /// <summary>
        /// 默认模式
        /// </summary>
        Default,

        /// <summary>
        /// 外观模式，只有外观模式的对象才支持事务控制和日志记录
        /// </summary>
        Facade,
    }
}
