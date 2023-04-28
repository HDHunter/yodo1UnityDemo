using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yodo1EditorUtils
{
    /// <summary>
    /// 验证KEY的有效性
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static bool IsVaildValue(string key)
    {
        if (string.IsNullOrEmpty(key)) return false;
        if (key.Contains("[") || key.Contains("]")) return false;
        if (key.Contains("请输入") || key.Contains("正确")) return false;
        return true;
    }
}
