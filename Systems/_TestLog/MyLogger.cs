using UnityEngine;

/// <summary>
/// ビルドの対象外にするログ
/// </summary>
public class MyLogger : MonoBehaviour
{
    public static void Log(string log)
    {
#if UNITY_EDITOR
        Debug.Log("88MyLogger88" + log);
#endif
    }
    public static void LogWarning(string log)
    {
#if UNITY_EDITOR
        Debug.LogWarning("88MyLogger88" + log);
#endif
    }
    public static void LogError(string log)
    {
#if UNITY_EDITOR
        Debug.LogError("88MyLogger88" + log);
#endif
    }
}
