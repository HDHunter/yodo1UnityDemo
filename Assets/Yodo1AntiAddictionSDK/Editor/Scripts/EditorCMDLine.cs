using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace Yodo1.Editor
{


    public class CmdLine 
	{


		public const string K_FILE_OSX_BASH = "/bin/bash";
		public const string K_FILE_OSX_ZSH = "/bin/zsh";

		public const string K_FILE_OSX_TERMINAL = "/Applications/Utilities/Terminal.app/Contents/MacOS/Terminal";
		public const string K_FILE_OSX_TERMINAL_2 = "/System/Applications/Utilities/Terminal.app/Contents/MacOS/Terminal";

		public const string K_FILE_WIN_CMD = "cmd.exe";
		public const string K_FILE_WIN_EXPLORER = "explorer.exe";
		public const string K_MAC_OPEN_CMD_PATH = "/usr/bin/open";


		public static void Shell(string command , System.Action<string> onResult = null, string fileName = "")
		{
			Thread newThread = new Thread(new ThreadStart(()=>{
					string res = RunShell(command, fileName);
					if(onResult != null) onResult(res);
			}));  
			newThread.Start();  
		}


		/// <summary>
		/// 设置SH文件可执行属性
		/// </summary>
		/// <param name="filePath"></param>
		public static void MakeShellFileExecutable(string filePath)
		{
			System.Diagnostics.Process.Start("chmod", "777 \"" + filePath + "\"");
		}

		/// <summary>
		/// 打开Windows的文件浏览器
		/// </summary>
		/// <param name="path"></param>
		public static void Open(string path)
		{
			UnityEngine.Debug.Log("cmd: open "+ path);

			if(Application.platform == RuntimePlatform.WindowsEditor)
			{
				System.Diagnostics.Process.Start(K_FILE_WIN_EXPLORER, "\""+ path.Replace("/", "\\")+ "\"");
			}
			else if(Application.platform == RuntimePlatform.OSXEditor)
			{
				System.Diagnostics.Process.Start(K_MAC_OPEN_CMD_PATH, "\""+ path.Replace("\\", "/") + "\"");
			}
		}


		


		public static string TerminalPath { get {
				if (File.Exists(K_FILE_OSX_TERMINAL))
				{
					return K_FILE_OSX_TERMINAL;
				}
				if (File.Exists(K_FILE_OSX_TERMINAL_2))
				{
					return K_FILE_OSX_TERMINAL_2;
				}
				return string.Empty;
			}
		}
		public static void Terminal(string shFile, System.Action<string> onResult = null)
		{
			Thread newThread = new Thread(new ThreadStart(()=>{
					string res = RunShell(shFile, TerminalPath);
					if(onResult != null) onResult(res);
			}));  
			newThread.Start(); 
		}


		public static void RunSHFile(string fileName, string path="")
		{
			new Thread(new ThreadStart(()=>{
				string res = Do("/bin/sh", fileName, path);
			})).Start();
		}



		/// <summary>
		/// 执行shell命令
		/// </summary>
		/// <param name="command"></param>
		public static void RunShellCmd(string command)
		{

#if UNITY_EDITOR_OSX

			//检测Temp文件夹
			string shellPath = "/tmp/Unity/shell";
			if(!Directory.Exists(shellPath)) Directory.CreateDirectory(shellPath);

			string cmdName = System.DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd_hhmmss");
			string cmdNameCommand = string.Format("{0}.command",cmdName);
			string cmdPathCommand = Path.Combine(shellPath, cmdNameCommand);

			File.WriteAllText(cmdPathCommand, command);
			MakeShellFileExecutable(cmdPathCommand);

			//打开cmdPathCommand
			string openPath = string.Format("\"{0}\"",cmdPathCommand.Replace("\\", "/"));
			System.Diagnostics.Process.Start("/usr/bin/open", openPath);

			UnityEngine.Debug.Log("call: "+ command);
#endif

		}

		
		public static void Cmd(string command, bool showWindow = true, string dir = "")
		{
			if(showWindow) command = "start \"unity-called-cmd\" \""+ command + "\"";
			Thread t = new Thread(new ThreadStart(()=>{
				RunCmd(command, "", dir);
			}));  
			t.Start();  
		} 





		private static string RunCmd(string batFile, string fileName = "", string dir="")
		{
			if(string.IsNullOrEmpty(fileName)) { fileName = K_FILE_WIN_CMD; }
			string args = "/c " + batFile; 
			return Do(fileName, args, dir);
		}


		private static string RunShell(string shFile, string fileName = "", string dir = "")
		{
			if(string.IsNullOrEmpty(fileName)) { fileName = K_FILE_OSX_BASH; }

			return Do(fileName, shFile, dir);
		}




		public static string Do(string file, string args, string dir="")
		{
			Process p = new Process();
			p.StartInfo.FileName = file;           		//程序名
			p.StartInfo.Arguments = args;    			//程式命令行
			p.StartInfo.UseShellExecute = false;        //Shell的使用
			p.StartInfo.RedirectStandardInput = true;   //重定向输入
			p.StartInfo.RedirectStandardOutput = true; 	//重定向输出
			p.StartInfo.RedirectStandardError = true;   //重定向输出错误
			p.StartInfo.CreateNoWindow = true;          //设置不显示示窗口
			p.StartInfo.WorkingDirectory = dir;
			p.Start(); 
			p.WaitForExit();
			// return p.StandardOutput.ReadToEnd();

			p.Close();

			// System.Console.WriteLine(p.StandardOutput.ReadToEnd());
			return "";
		}

		// [MenuItem("Test/Command Test")]
		static void TestCommand()
		{
			string shFile = Application.dataPath + "/test.sh";
			UnityEngine.Debug.Log(">>> shFile:" + shFile);
			MakeShellFileExecutable(shFile);

			UnityEngine.Debug.Log("--- Run SH File ---");
			RunSHFile(shFile);

			UnityEngine.Debug.Log("--- Terminal ---");
			Terminal(shFile);
		}


	}

}


