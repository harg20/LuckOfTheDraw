using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKFootSolver : MonoBehaviour
{
    public Transform body;
    public float footspacing = 0;
    public float stepDistance = 1;
    [SerializeField] IKFootSolver otherFoot = default;
    public float stepHeight = 1;
    public float speed = .1f;
    public Vector3 forwardAdjust;
    float lerp = 0;
    Vector3 newPosition;
    Vector3 oldPosition;
    Vector3 currentPosition;
    // Start is called before the first frame update
    void Start()
    {
        //footspacing = transform.localPosition.x;
        currentPosition = newPosition = oldPosition = transform.position;
        //currentNormal = newNormal = oldNormal = transform.up;
        lerp = 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = currentPosition;

        Ray ray = new Ray(body.position + (forwardAdjust) + (body.right * footspacing), Vector3.down);
        ray.origin = new Vector3(ray.origin.x, 0, ray.origin.z);
        Debug.DrawRay(body.position + (forwardAdjust) + (body.right * footspacing), Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit info, 10,3))
        {
            if (Vector3.Distance(newPosition, info.point) > stepDistance && !otherFoot.IsMoving() && lerp >= 1)
            {

                lerp = 0;
                newPosition = info.point;
            }
        }
        if (lerp < 1)
        {
            Vector3 footPosition = Vector3.Lerp(oldPosition, newPosition, lerp);
            footPosition.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;
            currentPosition = footPosition;
            lerp += Time.deltaTime * speed;

        }
        else
        {
            oldPosition = newPosition;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(newPosition,.5f);
    }

    public bool IsMoving()
    {
        return lerp < 1;
    }
}
