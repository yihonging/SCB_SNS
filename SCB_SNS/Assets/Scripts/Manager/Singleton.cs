using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    // ��ȡ����ʵ���ľ�̬����
    public static T Instance
    {
        get
        {
            // ���ʵ��Ϊ�գ����ҳ������Ƿ�������е�ʵ��
            if (instance == null)
            {
                instance = FindObjectOfType<T>();

                // ���������û��ʵ��������һ���µĿ�GameObject�����ָ�����͵����
                if (instance == null)
                {
                    GameObject singletonObject = new(typeof(T).Name);
                    instance = singletonObject.AddComponent<T>();
                }
            }

            return instance;
        }
    }

    // ȷ��ֻ��һ��ʵ������
    protected virtual void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this as T;
            
            DontDestroyOnLoad(this); // ȷ���ڳ����л�ʱ���ᱻ����
        }
    }
}