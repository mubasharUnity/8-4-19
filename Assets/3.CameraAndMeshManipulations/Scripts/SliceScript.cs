using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using Plane = EzySlice.Plane;

public class SliceScript : MonoBehaviour
{
    public Transform planeAssist;

    private Transform camera;
    private Plane plane;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.transform;
    }

    public void SliceWithPoints(Vector3 p1, Vector3 p2)
    {
        var dir1 = p1 - camera.position;
        var dir2 = p2 - camera.position;

        var normal = Vector3.Cross(dir1, dir2);//calculate normal of triangle formed by user-points and camera-position
        planeAssist.up = normal.normalized;//planeassist is used by easy-slice to orient plane along object. plane makes cut in sphere

        plane = new Plane();
        plane.Compute(planeAssist);//plane is self is oriented along xz-axis, y-axis is normal
        SliceGameObject(gameObject, plane);
    }

    private void OnDrawGizmos()
    {
        plane.OnDebugDraw();
    }

    private void SliceGameObject(GameObject go, Plane plane)
    {
        GameObject[] pieces = go.SliceInstantiate(plane);
        pieces[0].SetActive(false);
        go.GetComponent<MeshRenderer>().enabled = false;
    }
}
