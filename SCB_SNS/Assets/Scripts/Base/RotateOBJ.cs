using System.Collections;
using System.Collections.Generic;
using UnityEngine;









public class RotateOBJ : MonoBehaviour
{

    public float  rotateSpped=5f;
    private int RandomValue;

    // Start is called before the first frame update
    private void Start() {
      // 随机生成0或1
        int randomNumber = Random.Range(0, 2);

        // 如果randomValue是0，将值设为-1，否则为1
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
