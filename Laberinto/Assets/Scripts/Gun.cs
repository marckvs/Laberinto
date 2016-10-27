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
    public float gunID;
    public float damage = 1f;

    public Transform spawn;
    public Transform shellEjecutionPoint;
    public Rigidbody shell;
    public AudioSource aud;
    public LayerMask collisionMask;

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

            if (Physics.Raycast(ray, out hit, shotDistance, collisionMask))
            {
                shotDistance = hit.distance;
                if (hit.collider.GetComponent<Entity>())
                {
                    hit.collider.GetComponent<Entity>().TakeDamage(damage);
                }
            }

            nextPossibleShootTime = Time.time + secondsBetweenShots;
            //Debug.DrawRay(ray.origin, ray.direction * shotDistance, Color.green, 1);

            aud.Play();
            
            if (tracer)
            {
                StartCoroutine("RenderTracer", ray.direction * shotDistance);
            }

            Rigidbody newShell = Instantiate(shell, shellEjecutionPoint.position, shellEjecutionPoint.rotation) as Rigidbody;
            newShell.AddForce(shellEjecutionPoint.right * Random.Range(180, 200) + spawn.forward * Random.Range(-100, 100f));
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

    IEnumerator RenderTracer(Vector3 hitPoint)
    {
        tracer.enabled = true;
        tracer.SetPosition(0, spawn.position);
        tracer.SetPosition(1, spawn.position + hitPoint);
        yield return null;
        tracer.enabled = false;
        
    }
}
