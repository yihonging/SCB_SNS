using System.Collections;
using System.Collections.Generic;
using UnityEngine;









public class RotateOBJ : MonoBehaviour
{

    public float  rotateSpped=5f;
    private int RandomValue;

    // Start is called before the first frame update
    private void Start() {
      // �������0��1
        int randomNumber = Random.Range(0, 2);

        // ���randomValue��0����ֵ��Ϊ-1������Ϊ1
        RandomValue = (randomNumber == 0) ? -1 : 1;
    }
    // Update is called once per frame
    protected virtual void Update()
    {
      RotateObj();
    }

   protected void RotateObj()
    {
        Vector3 rotation = new Vector3(0,0, rotateSpped*RandomValue );
        transform.rotation *= Quaternion.Euler(rotation);
    }
}
