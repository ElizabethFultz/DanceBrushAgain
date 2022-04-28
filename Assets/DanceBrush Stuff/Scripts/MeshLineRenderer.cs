using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class MeshLineRenderer : MonoBehaviour
{

	public Material lmat; //defines the material

	private Mesh ml; //instance variable for the mesh

	private Vector3 s; //vector to keep track of previous start point 

	private float lineSize = .1f; //defines the line size

	private bool firstQuad = true; //sees if the current sample is the first quadralateral to be drawn

	private Vector3 lastGoodOrientation;

	void Start()
	{
		ml = GetComponent<MeshFilter>().mesh; //adds the mesh to the ml variable; keeps track of the mesh
		GetComponent<MeshRenderer>().material = lmat; //sets the material of the mesh
	} //end Start



	public void setWidth(float width)
	{
		lineSize = width;
	} //end SetWidth



	//sets the position of the line and the points in the line
	//point is the start point of the previous quadralateral
	//checks to make sure this is not the start of a line and then adds a line and sets firstQuad to false. The start point is then changed to the point that was just added.
	public void AddPoint(Vector3 point)
	{
		if (s != Vector3.zero) 
		{
			AddLine(ml, MakeQuad(s, point, lineSize, firstQuad));
			firstQuad = false;
		}

		s = point;
	} //end AddPoint



	//builds the quadralaterial
	//s is the start point
	//e is the end point
	//w is the width of the line
	//all indicated if this is the first quadralateral or not

	/* Given the start point, end point, width and boolean value. The width is halved to have half the intended width on the top and half on the botton of the quadralateral. 
	 * The Vector3 array q keeps track of the quadralateral. If all is true then four vertices will be created while if it is false only two will be 
	 * created as the other two are part of the previous quadralateral. The vertex points are then defined and q is returned. */
	Vector3[] MakeQuad(Vector3 s, Vector3 e, float w, bool all)
	{
		w = w / 2;

		Vector3[] q;
		if (all)
		{
			q = new Vector3[4];
		}
		else
		{
			q = new Vector3[2];
		}

		Vector3 n = Vector3.Cross(s, e);
		Vector3 l = Vector3.Cross(n, e - s);

		if (l != Vector3.zero)
		{
			lastGoodOrientation = l;
		}
		else
		{
			l = lastGoodOrientation;
		}

		l.Normalize();

		if (all)
		{
			q[0] = transform.InverseTransformPoint(s + l * w);
			q[1] = transform.InverseTransformPoint(s + l * -w);
			q[2] = transform.InverseTransformPoint(e + l * w);
			q[3] = transform.InverseTransformPoint(e + l * -w);
		}
		else
		{
			q[0] = transform.InverseTransformPoint(s + l * w);
			q[1] = transform.InverseTransformPoint(s + l * -w);
		}

		return q;
	} //end MakeQuad
	


	//builds up the mesh
	//m is the mesh used
	//quad is the array for the quadralateral

	/* Checks the number of vertices in the vertices array and then resize the array to add the number of vertices needed for the next qualralateral. The vertices are added to the array and then
	 * is UV mapped which assigns the corridnates to the points in relation to the quadralateral (ex. (0,0), (1,0), (0,1), (1,1)). The triangles that make up the quadralateral are then drawn. */ 
	void AddLine(Mesh m, Vector3[] quad)
	{
		int vl = m.vertices.Length;

		Vector3[] vs = m.vertices;
		vs = resizeVertices(vs, 2 * quad.Length);

		for (int i = 0; i < 2 * quad.Length; i += 2)
		{
			vs[vl + i] = quad[i / 2];
			vs[vl + i + 1] = quad[i / 2];
		}

		Vector2[] uvs = m.uv;
		uvs = resizeUVs(uvs, 2 * quad.Length);

		if (quad.Length == 4)
		{
			uvs[vl] = Vector2.zero;
			uvs[vl + 1] = Vector2.zero;
			uvs[vl + 2] = Vector2.right;
			uvs[vl + 3] = Vector2.right;
			uvs[vl + 4] = Vector2.up;
			uvs[vl + 5] = Vector2.up;
			uvs[vl + 6] = Vector2.one;
			uvs[vl + 7] = Vector2.one;
		} 
		else
		{
			if (vl % 8 == 0)
			{
				uvs[vl] = Vector2.zero;
				uvs[vl + 1] = Vector2.zero;
				uvs[vl + 2] = Vector2.right;
				uvs[vl + 3] = Vector2.right;

			}
			else
			{
				uvs[vl] = Vector2.up;
				uvs[vl + 1] = Vector2.up;
				uvs[vl + 2] = Vector2.one;
				uvs[vl + 3] = Vector2.one;
			}
		} 

		int tl = m.triangles.Length;

		int[] ts = m.triangles;
		ts = resizeTriangles(ts, 12);

		if (quad.Length == 2)
		{
			vl -= 4;
		}

		// front-facing quad (FusedVR)
		ts[tl] = vl;
		ts[tl + 1] = vl + 2;
		ts[tl + 2] = vl + 4;

		ts[tl + 3] = vl + 2;
		ts[tl + 4] = vl + 6;
		ts[tl + 5] = vl + 4;

		// back-facing quad (FusedVR)
		ts[tl + 6] = vl + 5;
		ts[tl + 7] = vl + 3;
		ts[tl + 8] = vl + 1;

		ts[tl + 9] = vl + 5;
		ts[tl + 10] = vl + 7;
		ts[tl + 11] = vl + 3;

		m.vertices = vs;
		m.uv = uvs;
		m.triangles = ts;
		m.RecalculateBounds();
		m.RecalculateNormals();
	} //end AddLine



	//resizes the array of vertices
	Vector3[] resizeVertices(Vector3[] ovs, int ns)
	{
		Vector3[] nvs = new Vector3[ovs.Length + ns];
		for (int i = 0; i < ovs.Length; i++)
		{
			nvs[i] = ovs[i];
		}

		return nvs;
	} //end resizeVertices



	//resizes the UVs
	Vector2[] resizeUVs(Vector2[] uvs, int ns)
	{
		Vector2[] nvs = new Vector2[uvs.Length + ns];
		for (int i = 0; i < uvs.Length; i++)
		{
			nvs[i] = uvs[i];
		}

		return nvs;
	} //end resizeUVs



	//resizes the triangles
	int[] resizeTriangles(int[] ovs, int ns)
	{
		int[] nvs = new int[ovs.Length + ns];
		for (int i = 0; i < ovs.Length; i++)
		{
			nvs[i] = ovs[i];
		}

		return nvs;
	} //end resizeTriangles
}//end class
