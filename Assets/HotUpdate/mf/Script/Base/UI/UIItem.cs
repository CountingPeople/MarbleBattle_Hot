using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    /// <summary>
    /// UIItem基类
    /// </summary>
    public abstract class UIItem : UIView
    {
        public virtual void SetData(object data,int index=0) { 
        
        }
    }
}
