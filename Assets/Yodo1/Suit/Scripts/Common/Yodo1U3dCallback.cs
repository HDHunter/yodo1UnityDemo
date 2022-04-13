using UnityEngine;

/// <summary>
/// Yodo1 u3d callback.
/// </summary>
public class Yodo1U3dCallback : MonoBehaviour
{
    /// <summary>
    /// The parameter(msg) is a json string, containing the result information.
    /// </summary>
    public delegate void onResult(string msg);
}