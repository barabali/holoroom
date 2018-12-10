using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Academy.HoloToolkit.Unity;

namespace HoloToolkit.Unity.SpatialMapping
{
public class SendMEshes : MonoBehaviour {

		public float scanTime= 15;
		private float lastUpdate;
	// Use this for initialization
	void Start () {
			lastUpdate = Time.time;
			// Register for the MakePlanesComplete event.
			SurfaceMeshesToPlanes.Instance.MakePlanesComplete += SendMeshes;
	}
	
	// Update is called once per frame
	void Update () {
			/*if (((Time.time - lastUpdate) > scanTime))
			{
				SendMeshes();
				//sent = true;
			}*/
	}

	// Called by GazeGestureManager when the user performs a Select gesture
	void OnSelect()
	{
	}

	/// <summary>
	/// Sends the spatial mapping surfaces from the HoloLens to a remote system running the Unity editor.
	/// </summary>
	private void SendMeshes(object source, System.EventArgs args)
	{
			#if !UNITY_EDITOR && UNITY_WSA
			Debug.Log ("Entered sendmeshes");
			List<GameObject> vertical = new List<GameObject> ();
			vertical =	SurfaceMeshesToPlanes.Instance.GetActivePlanes(PlaneTypes.Wall);
			List<Mesh> meshesToSend = new List<Mesh>();

			foreach (GameObject plane in vertical) {
				Mesh viewedModel;
				MeshFilter viewedModelFilter = (MeshFilter)plane.GetComponent("MeshFilter");
				viewedModel=viewedModelFilter.sharedMesh;

				Mesh clone = new Mesh();
				List<Vector3> verts = new List<Vector3>();
				verts.AddRange(viewedModel.vertices);

				for(int vertIndex=0; vertIndex < verts.Count; vertIndex++)
				{
					verts[vertIndex] = viewedModelFilter.transform.TransformPoint(verts[vertIndex]);
				}
				clone.SetVertices(verts); 
				clone.SetTriangles(viewedModel.triangles, 0);
				meshesToSend.Add(clone);
			}
			byte[] serialized = SimpleMeshSerializer.Serialize(meshesToSend);
			RemoteMeshSource.Instance.SendData(serialized);

		/*List<MeshFilter> MeshFilters = SpatialMappingManager.Instance.GetMeshFilters();
		for (int index = 0; index < MeshFilters.Count; index++)
		{
		List<Mesh> meshesToSend = new List<Mesh>();
		MeshFilter filter = MeshFilters[index];
		Mesh source = filter.sharedMesh;
		Mesh clone = new Mesh();
		List<Vector3> verts = new List<Vector3>();
		verts.AddRange(source.vertices);

		for(int vertIndex=0; vertIndex < verts.Count; vertIndex++)
		{
		verts[vertIndex] = filter.transform.TransformPoint(verts[vertIndex]);
		}

		

		clone.SetVertices(verts); 
		clone.SetTriangles(source.triangles, 0);
		meshesToSend.Add(clone);
		byte[] serialized = SimpleMeshSerializer.Serialize(meshesToSend);
		RemoteMeshSource.Instance.SendData(serialized);
		}*/
		#endif
	}
}
}