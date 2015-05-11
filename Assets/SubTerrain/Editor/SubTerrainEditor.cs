//
// This is the Custom GUI for the SubTerrain script
//
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(SubTerrain))]
public class SubTerrainEditor : Editor {

	public SubTerrain Myscript;


	void Start () {
	}

	public override void OnInspectorGUI () {

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Substances","",GUILayout.Width(110));
		EditorGUILayout.LabelField("Manual","",GUILayout.Width(43));
		EditorGUILayout.LabelField("Auto","",GUILayout.Width(28));
		EditorGUILayout.LabelField("Spec","",GUILayout.Width(30));
		EditorGUILayout.LabelField("Mix","",GUILayout.Width(25));
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		((SubTerrain)target).Substance0 = (ProceduralMaterial)EditorGUILayout.ObjectField(((SubTerrain)target).Substance0, typeof(ProceduralMaterial),false,GUILayout.Width(110));
		if(GUILayout.Button("Upd 0",GUILayout.ExpandWidth(false)))
		{
			((SubTerrain)target).RenderDiffuse(((SubTerrain)target).Substance0,0);
			reimport(0);
		}
		((SubTerrain)target).upd0 = EditorGUILayout.Toggle(((SubTerrain)target).upd0,GUILayout.Width(28));
		((SubTerrain)target).Spec0 = EditorGUILayout.Slider(((SubTerrain)target).Spec0,0,1,GUILayout.Width(30));
		((SubTerrain)target).Mix0 = EditorGUILayout.Toggle(((SubTerrain)target).Mix0,GUILayout.Width(28));

		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		((SubTerrain)target).Substance1 = (ProceduralMaterial)EditorGUILayout.ObjectField(((SubTerrain)target).Substance1, typeof(ProceduralMaterial),false,GUILayout.Width(110));
		if(GUILayout.Button("Upd 1",GUILayout.ExpandWidth(false)))
		{
			((SubTerrain)target).RenderDiffuse(((SubTerrain)target).Substance1,1);
			reimport(1);
		}
		((SubTerrain)target).upd1 = EditorGUILayout.Toggle(((SubTerrain)target).upd1,GUILayout.Width(28));
		((SubTerrain)target).Spec1 = EditorGUILayout.Slider(((SubTerrain)target).Spec1,0,1,GUILayout.Width(30));
		((SubTerrain)target).Mix1 = EditorGUILayout.Toggle(((SubTerrain)target).Mix1,GUILayout.Width(28));

		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		((SubTerrain)target).Substance2 = (ProceduralMaterial)EditorGUILayout.ObjectField(((SubTerrain)target).Substance2, typeof(ProceduralMaterial),false,GUILayout.Width(110));
		if(GUILayout.Button("Upd 2",GUILayout.ExpandWidth(false)))
		{
			((SubTerrain)target).RenderDiffuse(((SubTerrain)target).Substance2,2);
			reimport(2);
		}
		((SubTerrain)target).upd2 = EditorGUILayout.Toggle(((SubTerrain)target).upd2,GUILayout.Width(28));
		((SubTerrain)target).Spec2 = EditorGUILayout.Slider(((SubTerrain)target).Spec2,0,1,GUILayout.Width(30));
		((SubTerrain)target).Mix2 = EditorGUILayout.Toggle(((SubTerrain)target).Mix2,GUILayout.Width(28));
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		((SubTerrain)target).Substance3 = (ProceduralMaterial)EditorGUILayout.ObjectField(((SubTerrain)target).Substance3, typeof(ProceduralMaterial),false,GUILayout.Width(110));
		if(GUILayout.Button("Upd 3",GUILayout.ExpandWidth(false)))
		{
			((SubTerrain)target).RenderDiffuse(((SubTerrain)target).Substance3,3);
			reimport(3);
		}
		((SubTerrain)target).upd3 = EditorGUILayout.Toggle(((SubTerrain)target).upd3,GUILayout.Width(28));
		((SubTerrain)target).Spec3 = EditorGUILayout.Slider(((SubTerrain)target).Spec3,0,1,GUILayout.Width(30));
		((SubTerrain)target).Mix3 = EditorGUILayout.Toggle(((SubTerrain)target).Mix3,GUILayout.Width(28));
		EditorGUILayout.EndHorizontal();

		//Texture Blending - Comment this if you don't need it.
		((SubTerrain)target).Mix = EditorGUILayout.Slider("Blending",((SubTerrain)target).Mix,0,1);
		((SubTerrain)target).MixScale = EditorGUILayout.Slider("Blending Scale",((SubTerrain)target).MixScale,0,1);

		if (GUI.changed)
			((SubTerrain)target).ApplyTextures ();


	}

	void reimport(int slot) {
		AssetDatabase.ImportAsset( "Assets/SubTerrain/Resources/" + target.name + "_splat" + slot.ToString() + ".png" );
	}

}
