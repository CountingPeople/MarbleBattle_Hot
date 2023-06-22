using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using System;
using System.Reflection;

namespace MarbleBattleEditor
{
    [AttributeUsage(AttributeTargets.Field)]
    internal class CommandAttribute : Attribute
    {
        public readonly string option;
        public readonly bool new_line;

        public CommandAttribute(string option, bool new_line = true)
        {
            this.option = option;
            this.new_line = new_line;
        }
    }

    public class LubanExportConfig
    {
        [Command("-j")]
        public string job = "cfg --";

        [Command("-d")]
        public string define_xml = "Defines/__root__.xml";

        [Command("--input_data_dir")]
        public string input_data_dir = "Datas";

        // output
        [Command("--output_data_dir")]
        public string output_data_dir;

        [Command("--output_code_dir")]
        public string output_code_dir;

        [Command("--gen_types")]
        public string genType = "code_cs_unity_bin,data_bin";

        [Command("-s")]
        public string service = "all";

        private string dllPath;
        private string pwd;

        public LubanExportConfig(string excelDir)
        {
            output_data_dir = Application.dataPath + "/Resources/DataTable/" + "Data";
            output_code_dir = Application.dataPath + "/Resources/DataTable/" + "Code";
            pwd = excelDir;
            dllPath = excelDir + "/Tools/Luban.ClientServer/Luban.ClientServer.dll";
        }

        // export
        public void Gen() { GenUtils.Gen(_GetCommand(), pwd); }

        // preview
        public string Preview()
        {
            return $"{GenUtils._DOTNET} {_GetCommand()}";
        }

        private string _GetCommand()
        {
            string line_end = Application.platform == RuntimePlatform.WindowsEditor ? " ^" : " \\";

            StringBuilder sb = new StringBuilder();

            sb.Append(dllPath);

            var fields = GetType().GetFields();

            foreach (var field_info in fields)
            {
                var command = field_info.GetCustomAttribute<CommandAttribute>();

                if (command is null)
                {
                    continue;
                }

                var value = field_info.GetValue(this)?.ToString();

                // 当前值为空 或者 False, 或者 None(Enum 默认值)
                // 则继续循环
                if (string.IsNullOrEmpty(value) || string.Equals(value, "False") || string.Equals(value, "None"))
                {
                    continue;
                }

                if (string.Equals(value, "True"))
                {
                    value = string.Empty;
                }

                value = value.Replace(", ", ",");

                sb.Append($" {command.option} {value} ");

                if (command.new_line)
                {
                    sb.Append($"{line_end} \n");
                }
            }

            return sb.ToString();
        }

    }

    public class ExcelConfig
    {
        public string ExcelDir;
    }
}