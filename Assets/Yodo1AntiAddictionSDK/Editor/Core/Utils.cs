using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;



namespace Yodo1.Editor
{
	public class Utils 
	{

		
		#region Environment Variables
		public static string FormatPath(string path)
		{
			return path.Replace("\\", "/"); // 统一路径的样式
		}

		public static string CurrentDate (string format = "yyyy-MM-dd - hh:mm:ss")
		{
			return System.DateTime.Now.ToLocalTime().ToString(format);
		}

		public static string GetBundleId()
		{
			string bundleId = "";
			// if(target == BuildTarget.NoTarget) target = EditorUserBuildSettings.activeBuildTarget;


#if UNITY_2017 || UNITY_2017_OR_NEWER
			bundleId = Application.identifier;
#endif			
			
			return bundleId;
			
		}


		public static string GetAppName()
		{
			string appName = PlayerSettings.productName.Replace(" ", "");
			if(string.IsNullOrEmpty(appName))
			{
				appName = Directory.GetParent(Application.dataPath).Name;	
			}
			return appName;
		}

		/// <summary>
		/// 转换成AssetPath
		/// </summary>
		/// <param name="fullPath"></param>
		public static string ToAssetPath(string fullPath)
		{
			string key = "Assets";
			if(fullPath.Contains(key))
			{
				int idx = fullPath.IndexOf(key);
				return fullPath.Substring(idx);
			}
			Debug.LogFormat("<color=red>Path is not under Unity assets path...</color>");
			return fullPath;
			
		}


		/// <summary>
		/// Get Unity ProjectID 
		/// </summary>
		/// <returns></returns>
		public static string GetProjectID()
		{
			string path = Path.Combine(Directory.GetParent(Application.dataPath).FullName, "ProjectSettings/ProjectSettings.asset");
			if(File.Exists(path))
			{
				string[] lens = File.ReadAllLines(path);
				foreach(var s in lens)
				{
					if(s.Contains("productGUID"))
					{
						return s.Substring(s.LastIndexOf(":") + 1).Replace("\t", "").Replace(" ","");
					}
				}
			}

			return "";
		}



		public static string GUID { get { return System.Guid.NewGuid().ToString();  }}



		#endregion



		#region BUILD TOOLS

		/// <summary>
		/// Export EnvVars
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <param name="usingQM"></param>
		/// <returns></returns>
		public static string ExportENVKeyValueLine(string key, string value, bool usingQM = false)
		{
			string qm = usingQM? "\"" : ""; 
			string val = qm + value + qm;
			return string.Format("export {0}={1}", key, val);
		}


		public static void SaveAndRefresh()
		{
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}


		#endregion


		#region FILE OPRATION


		public static void EnsureParentDir(string filePath) { EnsurePath(Directory.GetParent(filePath).FullName); }
		public static void EnsurePath(string path) { if(!Directory.Exists(path)) Directory.CreateDirectory(path); }

		public static void CopyFolder(string from, string to, string exceptions = ".meta")
		{
			CopyFolder(new DirectoryInfo(from), to, exceptions);
		}

		/// <summary>
		/// Copy whole folder
		/// </summary>
		/// <param name="dirInfo"></param>
		/// <param name="to"></param>
		/// <param name="exceptions"></param>
		public static void CopyFolder(DirectoryInfo dirInfo, string to, string exceptions = ".meta")
		{
			List<string> exp = new List<string>(exceptions.Split(','));
	
			foreach (var dir in dirInfo.GetDirectories())
			{
				string toD = Path.Combine(to , dir.Name);
				if(!Directory.Exists(toD)) Directory.CreateDirectory(toD);
				CopyFolder(dir, toD, exceptions);
			}

			// List<string> log = new List<string>();
			// log.Add("---- Move File List ----");
			EditorUtility.ClearProgressBar();
			for(var i = 0; i <dirInfo.GetFiles().Length; i++)
			{
				FileInfo f =  dirInfo.GetFiles()[i];
				if(!string.IsNullOrEmpty(exceptions) && exp.Contains( f.Extension)) { continue; }
				string toF = Path.Combine(to, f.Name);

				FileInfo toFI = new FileInfo(toF);
				if(!toFI.Directory.Exists) Directory.CreateDirectory(toFI.Directory.FullName); 
				File.Copy(f.FullName, toF, true);

				// Debug.LogFormat("{0} ->\n<color=white>{1}</color>", f.FullName, toF);
				float p = (float)i/dirInfo.GetFiles().Length;
				EditorUtility.DisplayProgressBar("Copy Files", "File: "+ toF, p);

				// log.Add("Copy: " + f.FullName);
				// log.Add("to: " + toF);
				// log.Add("\t");
			}
			EditorUtility.ClearProgressBar();
			// File.WriteAllLines(Path.Combine(BC.Workspace, "Temp/move_file_log.txt"), log.ToArray());
		}

		public static void CopyFile(string from, string to, bool overwrite = true)
		{
			if(!File.Exists(from))
			{
				Debug.LogError(">> File not exsited at: "+ from);
				return;
			}

			if(File.Exists(to))
			{
				if(!overwrite)
				{
					Debug.LogError(">> Destiny File exsited, but can't replace. -> at: "+ to);
					return;
				}
				else
				{
					File.Delete(to);
				}
			}


			// Ensure parent
			string toDir = Directory.GetParent(to).FullName;
			EnsurePath(toDir);

			
			File.Copy(Path.GetFullPath(from),Path.GetFullPath(to), overwrite);

		}

		/// <summary>
		/// Move Files
		/// </summary>
		/// <param name="from"></param>
		/// <param name="to"></param>
		/// <param name="overwrite"></param>
		public static void MoveFile(string from, string to, bool overwrite = true)
		{
			CopyFile(from, to, overwrite);
			File.Delete(from);
		}




		public static string Combine(string path1, string path2)
		{
			string p = Path.Combine(path1, path2);
			return p;
		}

		public static string FixedPath(string path)
		{
			string fixedPath = path.Replace("\\", "/");
#if UNITY_EDITOR_WIN			
			fixedPath = path.Replace("/", "\\");
#endif
			return fixedPath;
		}


		public static string FullPath(string path)
		{
			return Path.GetFullPath(path);
		}

		public static bool FileExsits(string filePath)
		{
			return File.Exists(filePath);
		}

		public static bool DirExsits(string dirPath)
		{
			return Directory.Exists(dirPath);
		}



		public static bool ValidateDirectory(string dirPath)
		{
			if(string.IsNullOrEmpty(dirPath)) return false;
			return Directory.Exists(dirPath);
		}

		/// <summary>
		/// Find some spec path
		/// </summary>
		/// <param name="rootDir"></param>
		/// <param name="partten"></param>
		/// <param name="isFile"></param>
		/// <returns></returns>
		public static string[] FindFilePathAt(string rootDir, string partten, bool isFile = true)
		{
			List<string> buff =new List<string>();


			if(ValidateDirectory(rootDir))
			{
				DirectoryInfo dir = new DirectoryInfo(rootDir);

				if(isFile)
				{
					foreach(var f in dir.GetFiles())
					{
						if(f.Name.Equals(partten))
						{
							buff.Add(f.FullName);
						}
					}

					foreach(var d in dir.GetDirectories())
					{
						string[] t = FindFilePathAt(d.FullName, partten, isFile);
						if(t!= null && t.Length > 0) buff.AddRange(t);
					}
				}
				else
				{
					foreach(var d in dir.GetDirectories())
					{
						if(d.Name.Equals(partten))
						{
							buff.Add(d.FullName);
						}

						string[] t = FindFilePathAt(d.FullName, partten, isFile);
						if(t!= null && t.Length > 0) buff.AddRange(t);
						
					}
				}

			} 



			return buff.ToArray();
		}

		public static string[] GetDirectories(string rootDir)
		{
			return Directory.GetDirectories(rootDir);
		}



		public static void WriteFile(string path, string content)
		{
			if(string.IsNullOrEmpty(path)) return;
			EnsurePath(Directory.GetParent(path).FullName);
			File.WriteAllText(path, content);
		}

		public static void WriteFileLines(string path, string[] lines)
		{
			if(string.IsNullOrEmpty(path)) return;
			EnsurePath(Directory.GetParent(path).FullName);
			File.WriteAllLines(path, lines);
		}

		public static void WriteFileLines(string path, List<string> lines)
		{
			if(string.IsNullOrEmpty(path)) return;
			EnsurePath(Directory.GetParent(path).FullName);
			File.WriteAllLines(path, lines.ToArray());
		}

		/// <summary>
		/// Write byte[] into file
		/// </summary>
		/// <param name="path"></param>
		/// <param name="bytes"></param>
		public static bool WriteFile(string path, byte[] bytes)
		{
			FileStream fs = null;
			try
			{
				fs = new FileStream(path, FileMode.OpenOrCreate);
				fs.Write(bytes, 0, bytes.Length);
			}
			catch(System.Exception e)
			{
				Debug.LogError(e.Message);
				return false;
			}
			finally
			{
				if (fs != null) fs.Close();	
			}
			return true;
		}

		public static string OpenSelectFolder(string title, string folder, string defaultName)
		{
			return EditorUtility.OpenFolderPanel(title, folder, defaultName);
		}


		public static string OpenSelectFile(string title, string directory, string extension)
		{
			return EditorUtility.OpenFilePanel(title, directory, extension);
		} 



		#endregion



	}
}



