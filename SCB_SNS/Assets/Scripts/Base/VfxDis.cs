using System.Collections;
using System.Collections.Generic;
using UnityEngine;












public class VfxDis : MonoBehaviour
{

    public float _scale;

    private void OnEnable() {
        this.gameObject.SetActive(true);
        transform.localScale=new Vector3(_scale, _scale, _scale);
    }
    // Start is called before the first frame update
    public void DisVFX(float distime)
    {
        Invoke(nameof(disObj),distime);
    }
    void disObj()
    {
        this.gameObject.SetActive(false);
    }

}
