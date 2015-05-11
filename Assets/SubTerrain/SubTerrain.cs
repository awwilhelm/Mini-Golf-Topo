//
// This script allows you to apply up to 4 substances on a Unity terrain using a Bump Specular shader.
// It uses a workaround involving Render to Texture to bake the diffuse map in real time in the editor,
// allowing you to tweak the substances applied on the terrain in the editor - NOT IN-GAME... YET :)
// Code based on the terrain script by Chris Morris of Six Times Nothing (http://www.sixtimesnothing.com)
//

using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
using System.IO;
#endif
using System;

[ExecuteInEditMode]
public class SubTerrain: MonoBehaviour {

	public ProceduralMaterial Substance0;
	public ProceduralMaterial Substance1;
	public ProceduralMaterial Substance2;
	public ProceduralMaterial Substance3;

	private Texture Bump0;
	private Texture Bump1;
	private Texture Bump2;
	private Texture Bump3;

	private Texture2D Dif0;
	private Texture2D Dif1;
	private Texture2D Dif2;
	private Texture2D Dif3;

	public float Spec0;
	public float Spec1;
	public float Spec2;
	public float Spec3;

	public float Mix = 0;
	public float MixScale = 6;

	public bool Mix0 = false;
	public bool Mix1 = false;
	public bool Mix2 = false;
	public bool Mix3 = false;

	//Auto Update booleans
	public bool upd0 = false;
	public bool upd1 = false;
	public bool upd2 = false;
	public bool upd3 = false;

	//
	//This function apply the Normal maps to each splat channel through the modified terrain shader.
	//
	void Start() {

		ApplyTextures ();
	}

	public void ApplyTextures () {

		if (Substance0 != null)
		{
			Bump0 = Substance0.GetTexture ("_BumpMap");
			if(Bump0)
				Shader.SetGlobalTexture("_BumpMap0", Bump0);
			Shader.SetGlobalFloat("_Spec0", Spec0);
			if (Mix0)
				Shader.SetGlobalFloat("_Mix0", Mix);
		}

		if (Substance1 != null)
		{
			Bump1 = Substance1.GetTexture ("_BumpMap");
			if(Bump1)
				Shader.SetGlobalTexture("_BumpMap1", Bump1);
			Shader.SetGlobalFloat("_Spec1", Spec1);
			if (Mix1)
				Shader.SetGlobalFloat("_Mix1", Mix);
		}

		if (Substance2 != null)
		{
			Bump2 = Substance2.GetTexture ("_BumpMap");
			if(Bump2)
				Shader.SetGlobalTexture("_BumpMap2", Bump2);
			Shader.SetGlobalFloat("_Spec2", Spec2);
			if (Mix2)
				Shader.SetGlobalFloat("_Mix2", Mix);
		}

		if (Substance3 != null)
		{
			Bump3 = Substance3.GetTexture ("_BumpMap");
			if(Bump3)
				Shader.SetGlobalTexture("_BumpMap3", Bump3);
			Shader.SetGlobalFloat("_Spec3", Spec3);
			if (Mix3)
				Shader.SetGlobalFloat("_Mix3", Mix);
		}

		//Texture Blending - Comment this if you don't need it.
		Shader.SetGlobalFloat("_Mix", Mix);
		Shader.SetGlobalFloat("_MixScale", MixScale);
	}

	//
	// This function creates a RenderToTexture for the diffuse map and stores it as a png in the Resources folders.
	//
	#if UNITY_EDITOR
	public void RenderDiffuse (ProceduralMaterial Diffuse, int slot) {

		GameObject rtObject = (GameObject)Instantiate(Resources.Load("RTTSubTerrain"));
		Camera cam = rtObject.transform.Find("RTTSubCamera").GetComponent<Camera>();

		// Create Render To Texture
		Vector2 TexSize = new Vector2((float)Math.Pow(2,Diffuse.GetProceduralVector("$outputsize").x), (float)Math.Pow(2,Diffuse.GetProceduralVector("$outputsize").y));
		RenderTexture rt = new RenderTexture((int)TexSize.x, (int)TexSize.y, 32);
		cam.targetTexture = rt;
		rtObject.transform.Find("RTTSubPlane").GetComponent<Renderer>().sharedMaterial.mainTexture = Diffuse.GetTexture ("_MainTex");
		cam.Render();
        RenderTexture.active = rt;
		Texture2D dif = new Texture2D((int)TexSize.x, (int)TexSize.y, TextureFormat.ARGB32, false);
		dif.ReadPixels(new Rect(0, 0, (int)TexSize.x, (int)TexSize.y), 0, 0);

		// Clean up
		cam.targetTexture = null;
		RenderTexture.active = null; // added to avoid errors
		DestroyImmediate(rt,true);
		DestroyImmediate(rtObject,true);

		// Write Diffuse map as PNG
      	byte[] bytes = dif.EncodeToPNG();
        string filename = Application.dataPath + "/SubTerrain/Resources/" + gameObject.name+"_splat" + slot.ToString() + ".png";
        System.IO.File.WriteAllBytes(filename, bytes);
        AssetDatabase.ImportAsset( "Assets/SubTerrain/Resources/" + gameObject.name+"_splat" + slot.ToString() + ".png" );

		DestroyImmediate(dif,true);

	}

	public static string getSceneName() {
		return System.IO.Path.GetFileNameWithoutExtension(Application.dataPath + "/" + EditorApplication.currentScene);
	}

	//
	// The functions in update are called only when the "Auto Update" switches are checked and one of the substances
	// is being updated - Note that using Auto Update can be slow due to the generation of the png image in real time.
	//
	void Update () {

		if (upd0 && (Substance0.isProcessing))
			RenderDiffuse(Substance0,0);
		if (upd1 && (Substance1.isProcessing))
			RenderDiffuse(Substance1,1);
		if (upd2 && (Substance2.isProcessing))
			RenderDiffuse(Substance2,2);
		if (upd3 && (Substance3.isProcessing))
			RenderDiffuse(Substance3,3);

	}
	#endif
}

