using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static T _instance;
    public static bool HasInstance => _instance != null;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError(
                    $"[Singleton<{typeof(T).Name}>] Instance is NULL. " +
                    $"Ensure exactly one instance exists before accessing Instance."
                );
            }
            return _instance;
        }
    }
    protected bool IsPersistent = false;

    protected virtual void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this as T;

        if (IsPersistent)
        {
            DontDestroyOnLoad(gameObject);
        }

        OnSingletonAwake();
    }
    protected virtual void OnSingletonAwake() { }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
