using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour
{

    public enum GunType
    {
        Semi, Burst, Auto
    }
    public GunType gunType;
    public float rpm;

    public Transform spawn;
    public AudioSource audio;
    private LineRenderer tracer;

    private float secondsBetweenShots;
    private float nextPossibleShootTime;

    void Start()
    {
        secondsBetweenShots = 60 / rpm;
        if (GetComponent<LineRenderer>())
        {
            tracer = GetComponent<LineRenderer>();
        }
    }

    public void Shoot()
    {
        if (CanShoot())
        {
            Ray ray = new Ray(spawn.position, spawn.forward);
            RaycastHit hit;
            float shotDistance = 20;
            if (Physics.Raycast(ray, out hit, shotDistance))
            {
                shotDistance = hit.distance;
            }

            nextPossibleShootTime = Time.time + secondsBetweenShots;
            Debug.DrawRay(ray.origin, ray.direction * shotDistance, Color.green, 1);

            audio.Play();
            
            if (tracer)
            {
                StartCoroutine("RenderTracer", ray.direction * shotDistance);
            }
        }
    }

    public void ShootContinuous()
    {
        if (gunType == GunType.Auto)
        {
            Shoot();
        }
        //else if(Input.GetButtonDown)
    }

    private bool CanShoot()
    {
        bool canShoot = true;

        if (Time.time < nextPossibleShootTime) canShoot = false;

        return canShoot;
    }

    IEnumerator RenderTracer()
    {
        tracer.enabled = true;
        tracer.SetPosition(0, spawn.position);
        tracer.SetPosition(1, spawn.position + );
        yield return null;
        tracer.enabled = false;
        
    }
}
