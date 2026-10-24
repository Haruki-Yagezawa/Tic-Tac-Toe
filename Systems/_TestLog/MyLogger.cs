using UnityEngine;

/// <summary>
/// ビルドの対象外にするログ
/// </summary>
public class MyLogger : MonoBehaviour
{
    public static void Log(string log)
    {
#if UNITY_EDITOR
        Debug.Log("MyLog |===| " + log);
#endif
    }
    public static void LogWarning(string log)
    {
#if UNITY_EDITOR
        Debug.LogWarning("MyLog |===| " + log);
#endif
    }
    public static void LogError(string log)
    {
#if UNITY_EDITOR
        Debug.LogError("MyLog |===| " + log);
#endif
    }
}
