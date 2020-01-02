using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSteel : MonoBehaviour {

    MeshRenderer platMesh;
    BoxCollider platCol;

	void Start ()
    {
        platMesh = GetComponent<MeshRenderer>();
        platCol = GetComponent<BoxCollider>();

        if (!platCol.isTrigger)
            platCol.enabled = false;

        platMesh.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        platMesh.enabled = true;
        platCol.enabled = true;
    }
}
