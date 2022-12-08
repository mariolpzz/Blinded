
using System.Collections;
using UnityEngine;

/*
Some notes
----------
The object this script is assigned to should be at world position (0,0,0).
You will lose normal/height map data in the current state of the script.
If materials all use different shaders, they will all be converted to the shader specified.
If you have an enormous amount of objects, sometimes the triangles get mixed up.
This can probably be resolved by creating a hierarchy of parents to segregate the combining of objects.
Objects to combine must be dynamic, not static.
*/

public class CombineChildrenToOneBatch : MonoBehaviour
{
	public string ShaderToUse = "Standard";
	public bool CreateMeshCollider = false;
	public Color materialColor = Color.white;

	void Start()
	{
		CombineTextures();
		CombineMeshes();

		if (CreateMeshCollider)
			gameObject.AddComponent<MeshCollider>();

		GetComponent<Renderer>().material.color = materialColor;
	}

	void CombineTextures()
	{
		Texture2D newTexture;
		Material newMaterial = new Material(Shader.Find(ShaderToUse));
		Component[] filters = GetComponentsInChildren(typeof(MeshFilter));

		Texture2D[] textures = new Texture2D[filters.Length];

		// Retrieve each texture
		for (int i = 0; i < filters.Length; i++)
			textures[i] = (Texture2D)filters[i].gameObject.GetComponent<Renderer>().material.mainTexture;

		newTexture = new Texture2D(1024, 1024);
		// This will be our new texture atlas
		Rect[] uvs = newTexture.PackTextures(textures, 0, 1024);

		// Store our texture atlas on our new material
		newMaterial.mainTexture = newTexture;

		Vector2[] uva, uvb;
		for (int j = 0; j < filters.Length; j++)
		{
			// Change each child material to the new material
			filters[j].gameObject.GetComponent<Renderer>().material = newMaterial;
			// Get original texture coordinates
			uva = ((MeshFilter)filters[j]).mesh.uv;
			// Allocate array of UV coordinates
			uvb = new Vector2[uva.Length];
			for (int k = 0; k < uva.Length; k++)
			{
				// Return the new UVs after scaling then offseting.
				uvb[k] = new Vector2((uva[k].x * uvs[j].width) + uvs[j].x, (uva[k].y * uvs[j].height) + uvs[j].y);
			}
				
			// Assign new UVs
			((MeshFilter)filters[j]).mesh.uv = uvb;
		}
	}

	void CombineMeshes()
	{
		ArrayList materials = new ArrayList();
		ArrayList combineInstanceArrays = new ArrayList();
		MeshFilter[] meshFilters = gameObject.GetComponentsInChildren<MeshFilter>();

		foreach (MeshFilter meshFilter in meshFilters)
		{
			MeshRenderer meshRenderer = meshFilter.GetComponent<MeshRenderer>();

			if (!meshRenderer ||
				!meshFilter.sharedMesh ||
				meshRenderer.sharedMaterials.Length != meshFilter.sharedMesh.subMeshCount)
				continue;

			for (int s = 0; s < meshFilter.sharedMesh.subMeshCount; s++)
			{
				// Make sure we don't already have this material
				int materialArrayIndex = Contains(materials, meshRenderer.sharedMaterials[s].name);
				if (materialArrayIndex == -1)
				{
					materials.Add(meshRenderer.sharedMaterials[s]);
					materialArrayIndex = materials.Count - 1;
				}
				combineInstanceArrays.Add(new ArrayList());

				// Create a new instance
				CombineInstance combineInstance = new CombineInstance();
				combineInstance.transform = meshRenderer.transform.localToWorldMatrix;
				combineInstance.subMeshIndex = s;
				combineInstance.mesh = meshFilter.sharedMesh;
				// Then add it to our collection of instances
				(combineInstanceArrays[materialArrayIndex] as ArrayList).Add(combineInstance);
			}
		}

		// Create a mesh filter and renderer
		MeshFilter meshFilterCombine = gameObject.GetComponent<MeshFilter>();
		if (meshFilterCombine == null)
			meshFilterCombine = gameObject.AddComponent<MeshFilter>();

		MeshRenderer meshRendererCombine = gameObject.GetComponent<MeshRenderer>();
		if (meshRendererCombine == null)
			meshRendererCombine = gameObject.AddComponent<MeshRenderer>();

		Mesh[] meshes = new Mesh[materials.Count];
		CombineInstance[] combineInstances = new CombineInstance[materials.Count];

		// Collect all of the instances you want to combine
		for (int m = 0; m < materials.Count; m++)
		{
			CombineInstance[] combineInstanceArray = (combineInstanceArrays[m] as ArrayList).ToArray(typeof(CombineInstance)) as CombineInstance[];
			meshes[m] = new Mesh();
			meshes[m].CombineMeshes(combineInstanceArray, true, true);

			combineInstances[m] = new CombineInstance();
			combineInstances[m].mesh = meshes[m];
			combineInstances[m].subMeshIndex = 0;
		}

		// The final combination of all meshes
		meshFilterCombine.sharedMesh = new Mesh();
		meshFilterCombine.sharedMesh.CombineMeshes(combineInstances, false, false);

		foreach (Mesh oldMesh in meshes)
		{
			oldMesh.Clear();
			Destroy(oldMesh);
		}

		// Assign Materials
		Material[] materialsArray = materials.ToArray(typeof(Material)) as Material[];
		meshRendererCombine.materials = materialsArray;

		foreach (Transform child in gameObject.transform)
			Destroy(child.gameObject);
	}

	private int Contains(ArrayList searchList, string searchName)
	{
		for (int i = 0; i < searchList.Count; i++)
			if (((Material)searchList[i]).name == searchName)
				return i;

		return -1;
	}
}