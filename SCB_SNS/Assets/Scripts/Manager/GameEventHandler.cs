using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;






public class GameEventHandler :MonoBehaviour
{

    public static UnityEvent OnPlayerDeath = new UnityEvent();
    public static ExpsionRangeEvent OnexpsionRange= new ExpsionRangeEvent();//�����¼�
    public static UnityEvent OnCombo=new UnityEvent();

    public static void PlayerDeath()
    {
        OnPlayerDeath?.Invoke();
    }
    public class ExpsionRangeEvent:UnityEvent<Vector3>{}
    public static void Combo()
    {
        OnCombo?.Invoke();
    }
}
