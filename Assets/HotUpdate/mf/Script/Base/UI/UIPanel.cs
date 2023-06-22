using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    /// <summary>
    /// UIPanel基类
    /// </summary>
    public abstract class UIPanel : UIView
    {
        /// <summary>
        /// UI层级
        /// </summary>
        public virtual UILayerEnum LayerEnum => UILayerEnum.Normal;


        internal override void Internal_Close()
        {
            UIModule.Instance.ClosePanel(this);
        }
    }
}
