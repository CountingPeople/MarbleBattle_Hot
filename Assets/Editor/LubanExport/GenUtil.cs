using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;

namespace MarbleBattleEditor
{
    internal static class GenUtils
    {
        internal static readonly string _DOTNET = Application.platform == RuntimePlatform.WindowsEditor ? "dotnet.exe" : "dotnet";

        public static void Gen(string arguments, string cwd)
        {
            var process = _Run(
                _DOTNET,
                arguments,
                cwd,
                true
            );

            UnityEngine.Debug.Log(process.StandardOutput.ReadToEnd());

            AssetDatabase.Refresh();
        }

        private static Process _Run(string exe,
                                    string arguments,
                                    string working_dir = ".",
                                    bool wait_exit = false)
        {
            try
            {
                bool redirect_standard_output = true;
                bool redirect_standard_error = true;
                bool use_shell_execute = false;

                if (Application.platform == RuntimePlatform.WindowsEditor)
                {
                    redirect_standard_output = false;
                    redirect_standard_error = false;
                    use_shell_execute = true;
                }

                if (wait_exit)
                {
                    redirect_standard_output = true;
                    redirect_standard_error = true;
                    use_shell_execute = false;
                }

                ProcessStartInfo info = new ProcessStartInfo
                {
                    FileName = exe,
                    Arguments = arguments,
                    CreateNoWindow = true,
                    UseShellExecute = use_shell_execute,
                    WorkingDirectory = working_dir,
                    RedirectStandardOutput = redirect_standard_output,
                    RedirectStandardError = redirect_standard_error,
                };

                Process process = Process.Start(info);

                if (wait_exit)
                {
                    WaitForExitAsync(process).ConfigureAwait(false);
                }

                return process;
            }
            catch (Exception e)
            {
                throw new Exception($"dir: {Path.GetFullPath(working_dir)}, command: {exe} {arguments}", e);
            }
        }

        private static async Task WaitForExitAsync(this Process self)
        {
            if (!self.HasExited)
            {
                return;
            }

            try
            {
                self.EnableRaisingEvents = true;
            }
            catch (InvalidOperationException)
            {
                if (self.HasExited)
                {
                    return;
                }

                throw;
            }

            var tcs = new TaskCompletionSource<bool>();

            void Handler(object s, EventArgs e) => tcs.TrySetResult(true);

            self.Exited += Handler;

            try
            {
                if (self.HasExited)
                {
                    return;
                }

                await tcs.Task;
            }
            finally
            {
                self.Exited -= Handler;
            }
        }
    }
}