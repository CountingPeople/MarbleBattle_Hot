using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Framework.Editor
{
    internal class TranDto
    {
        public string ComOriginalName { get; set; }
        public string ParentPath { get; set; }
        public Transform Tran { get; set; }
    }

}
