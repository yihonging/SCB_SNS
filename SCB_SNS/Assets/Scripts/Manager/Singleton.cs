using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    // 获取单例实例的静态方法
    public static T Instance
    {
        get
        {
            // 如果实例为空，查找场景中是否存在已有的实例
            if (instance == null)
            {
                instance = FindObjectOfType<T>();

                // 如果场景中没有实例，创建一个新的空GameObject并添加指定类型的组件
                if (instance == null)
                {
                    GameObject singletonObject = new(typeof(T).Name);
                    instance = singletonObject.AddComponent<T>();
                }
            }

            return instance;
        }
    }

    // 确保只有一个实例存在
    protected virtual void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this as T;
            
            DontDestroyOnLoad(this); // 确保在场景切换时不会被销毁
        }
    }
}