using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;

public class TextUtils : Editor
{
	/// <summary>
	/// Gets the text.
	/// </summary>
	/// <returns>The text.</returns>
	/// <param name="path">Path.</param>
	public static string GetText (string path)
	{
		StreamReader streamReader = new StreamReader (path);
		string text = streamReader.ReadToEnd ();
		streamReader.Close ();

		return text;
	}

	/// <summary>
	/// Writes the below.
	/// </summary>
	/// <returns><c>true</c>, if below was writed, <c>false</c> otherwise.</returns>
	/// <param name="filePath">File path.</param>
	/// <param name="below">Below.</param>
	/// <param name="text">Text.</param>
	public static bool WriteBelow (string filePath, string below, string text)
	{
		StreamReader streamReader = new StreamReader (filePath);
		string text_all = streamReader.ReadToEnd ();
		streamReader.Close ();

		int beginIndex = text_all.LastIndexOf (below);
		if (beginIndex == -1) {
			Debug.LogError (filePath + "中没有找到标致" + below);
			return false; 
		}

		if (text_all.IndexOf (text) == -1) {
			int endIndex = beginIndex + below.Length;

			text_all = text_all.Substring (0, endIndex) + "\n" + text + /*"\n" +*/ text_all.Substring (endIndex);

			StreamWriter streamWriter = new StreamWriter (filePath);
			streamWriter.Write (text_all);
			streamWriter.Close ();
			return true;
		}
		return false;
	}

	/// <summary>
	/// Writes the front.
	/// </summary>
	/// <returns><c>true</c>, if front was writed, <c>false</c> otherwise.</returns>
	/// <param name="filePath">File path.</param>
	/// <param name="front">Front.</param>
	/// <param name="text">Text.</param>
	public static bool WriteFront (string filePath, string front, string text)
	{
		StreamReader streamReader = new StreamReader (filePath);
		string text_all = streamReader.ReadToEnd ();
		streamReader.Close ();

		int beginIndex = text_all.IndexOf (front);
		if (beginIndex == -1) {
			Debug.LogError (filePath + "中没有找到标致" + front);
			return false; 
		}

		if (text_all.IndexOf (text) == -1) {

			text_all = text_all.Substring (0, beginIndex) + "\n" + text + "\n\n" + text_all.Substring (beginIndex);

			StreamWriter streamWriter = new StreamWriter (filePath);
			streamWriter.Write (text_all);
			streamWriter.Close ();
			return true;
		}
		return false;
	}

	/// <summary>
	/// Replace the specified filePath, below and newText.
	/// </summary>
	/// <param name="filePath">File path.</param>
	/// <param name="below">Below.</param>
	/// <param name="newText">New text.</param>
	public static bool Replace (string filePath, string below, string newText)
	{
		bool bRet = false;
		StreamReader streamReader = new StreamReader (filePath);
		string text_all = streamReader.ReadToEnd ();
		streamReader.Close ();

		int beginIndex = text_all.IndexOf (below);

		StreamWriter streamWriter = null;
		if (beginIndex != -1) {
			text_all = text_all.Replace (below, newText);
			streamWriter = new StreamWriter (filePath);
			streamWriter.Write (text_all);
			streamWriter.Close ();
			bRet = true;
		}
		return bRet;
	}

	/// <summary>
	/// Regexs the match replace.
	/// </summary>
	/// <returns><c>true</c>, if match replace was regexed, <c>false</c> otherwise.</returns>
	/// <param name="filePath">File path.</param>
	/// <param name="regexStr">Regex string.</param>
	/// <param name="newText">New text.</param>
	public static bool RegexMatchReplace (string filePath, string regexStr, string newText)
	{
		bool bRet = false;
		StreamReader streamReader = new StreamReader (filePath);
		string text_all = streamReader.ReadToEnd ();
		streamReader.Close ();

		Match mstr = Regex.Match (text_all, regexStr);  
		string objectStr = mstr.Groups [1].Value.ToString (); 
		if (string.IsNullOrEmpty (objectStr) == false) {
			text_all = text_all.Replace (objectStr, newText);
			StreamWriter streamWriter = new StreamWriter (filePath);
			streamWriter.Write (text_all);
			streamWriter.Close ();
			bRet = true;
		}
		return bRet;
	}
}
