using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : AbilityBase
{
    public Transform player;
    private PlayerMovement test;

    private Camera cam;
    private float rayRange;

    private Vector3 aimPos;

    public override void SetUpAbility()
    {
        cam = Camera.main;
        rayRange = Mathf.Abs(cam.transform.position.z);
        test = player.GetComponent<PlayerMovement>();
    }

    public override void TriggerAbility()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUpAbility();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            aimPos = GetAimPosition();
            Dash(aimPos);
        }
    }

    private Vector3 GetAimPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.Log(ray.origin.z);
        float depth = ray.origin.z;
        Vector3 endOfRay = ray.origin + ray.direction * depth;
        Debug.DrawRay(ray.origin,  ray.direction * depth, Color.green);

        // Calculate angle between ray and player to get dash direction
        Vector3 dir = player.position - endOfRay;
        dir.z = 0;
        Debug.Log(dir.y);
        //Debug.Log(endOfRay.z);
        return dir;
    }

    private void Dash(Vector3 dir)
    {
       test.AddImpact(dir);

    }
}
