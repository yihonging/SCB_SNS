using UnityEngine;
using System.Collections;
using System;










[ExecuteInEditMode]
public class PaletteSwapLookup : MonoBehaviour
{
	public Texture[] LookupTexture;
	private Texture ColorTexture;
	[SerializeField]
	private int index;

	Material _mat;

	void OnEnable()
	{
		Shader shader = Shader.Find("Hidden/PaletteSwapLookup");
		if (_mat == null)
			_mat = new Material(shader);
    }

	 void Awake()
	{
		index=0;
		ColorTexture=LookupTexture[index];
	}
	 void Update()
	{
		if (Input.GetKeyDown(KeyCode.A) && index < LookupTexture.Length - 1)
    {
        index++;
    }
    else if (Input.GetKeyDown(KeyCode.A) && index == LookupTexture.Length-1 )
    {
        index = 0;
    }

    ColorTexture = LookupTexture[index];
	ApplyPlalette(ColorTexture);
	}



	void OnDisable()
	{
		if (_mat != null)
			DestroyImmediate(_mat);
	}

	public  void ApplyPlalette(Texture palette)
	{

		_mat.SetTexture("_PaletteTex",palette);
	}

	void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		Graphics.Blit(src, dst,  _mat);
	}
}
