using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public PlayerController pc;

    private Vector3 lastPlayerPosition;
    private float distanceToMove;

	void Start ()
    {
        pc = FindObjectOfType<PlayerController>();
        lastPlayerPosition = pc.transform.position;
    }

	void Update ()
    {
        distanceToMove = pc.transform.position.x - lastPlayerPosition.x;

        transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);

        lastPlayerPosition = pc.transform.position;
	}
}
