using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Framework.Editor
{
    public class FrameworkConfig : ScriptableObject
    {
        //TemplateRef template_ref = ScriptableObject.CreateInstance<TemplateRef>();
        //public TextAsset UIDescription;
        public TextAsset ViewTemplate;

        public string PanelPath = "";
        public string ItemPath = "";
        public string PanelGenPath = "";
        public string ItemGenPath = "";
        public string PanelScriptPath = "";
        public string ItemScriptPath = "";

        //public List<ExportDto> UIExports = new List<ExportDto>();
        [HideInInspector]
        [NonSerialized]
        public Dictionary<string, string> UIExportDic = new Dictionary<string, string>()
        {
            {"btn_", "Button" },
            {"img_", "Image" },
            {"raw_", "RawImage" },
            {"txt_", "Text" },
            {"txtp_", "TextPlus" },
            {"inp_", "InputField" },
            {"srect_", "ScrollRect" },
            {"sbar_", "Scrollbar" },
            {"tog_", "Toggle" },
            {"sli_", "Slider" },
            {"drop_", "Dropdown" },
            {"can_", "Canvas" },
            {"go_", "GameObject" },
            {"tran_", "Transform" },
            {"rtran_", "RectTransform" }
        };

        internal bool CheckPath()
        {
            return ViewTemplate != null && !string.IsNullOrWhiteSpace(PanelPath) && !string.IsNullOrWhiteSpace(ItemPath) && !string.IsNullOrWhiteSpace(PanelGenPath) && !string.IsNullOrWhiteSpace(ItemGenPath) && !string.IsNullOrWhiteSpace(PanelScriptPath) && !string.IsNullOrWhiteSpace(ItemScriptPath);
        }
        //[Serializable]
        //public class ExportDto
        //{
        //    public string Key;
        //    public string Value;
        //}
        //public Dictionary<string, string> UIExportDic;
    }
}
