using System.Collections;
using System.Collections.Generic;
using UnityEngine;








public class SpriteShadow : MonoBehaviour
{
    public Vector2 offset = new Vector2(.15f, -.15f);
    private SpriteRenderer sprRndCaster;
    private SpriteRenderer sprRndShadow;
    private Transform trasCaster;
    private Transform TransShadow;
    // public Material Shadow;
    public Color ShadowBlack;
    // Use this for initialization
    void Start()
    {
        ShadowBlack.a=1f;
        trasCaster = gameObject.transform;
        TransShadow = new GameObject().transform;
        TransShadow.parent = trasCaster;
        TransShadow.gameObject.name = "shadow";
        // transform .localRotation=Quaternion.identity;
        sprRndCaster = gameObject.GetComponent<SpriteRenderer>();
        sprRndShadow = TransShadow.gameObject.AddComponent<SpriteRenderer>();
        sprRndShadow.sortingLayerName = sprRndCaster.sortingLayerName;
        sprRndShadow.sortingOrder = sprRndCaster.sortingOrder - 1;
        sprRndShadow.color = ShadowBlack;
        // sprRndShadow.material = Shadow;
    }
    // Update is called once per frame
    private void LateUpdate()
    {
        TransShadow.position = new Vector2(trasCaster.position.x + offset.x, trasCaster.position.y + offset.y);
		TransShadow .rotation = new Quaternion (0,0,0,0);
		TransShadow.localScale = new Vector3 (1, 1, 1);
        sprRndShadow.sprite = sprRndCaster.sprite;
    }
}
