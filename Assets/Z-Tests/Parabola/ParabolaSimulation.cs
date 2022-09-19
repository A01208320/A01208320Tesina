using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaSimulation : MonoBehaviour {
    public Transform Target;
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;

    public Transform Projectile;
    private Transform myTransform;
    public Vector3 projectileForward;

    public bool end;
    public float maxY;

    void Awake() {
        myTransform = transform;
        end = true;
    }

    private void Update() {
        if (end) {
            StartCoroutine(SimulateProjectile());
        }
    }


    IEnumerator SimulateProjectile() {
        end = false;
        // Short delay added before Projectile is thrown
        yield return new WaitForSeconds(1.5f);

        // Move projectile to the position of throwing object + add some offset if needed.
        Projectile.position = myTransform.position + new Vector3(0, 0.0f, 0);

        // Calculate distance to target
        float target_Distance = Vector3.Distance(Projectile.position, Target.position);
        Debug.Log("Distanicia = " + target_Distance);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = Mathf.Sqrt((target_Distance * gravity) / Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad));
        Debug.Log("Velocidad inicial = " + projectile_Velocity);

        // Extract the X  Y componenent of the velocity
        float Vx = projectile_Velocity * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = projectile_Velocity * Mathf.Sin(firingAngle * Mathf.Deg2Rad);
        Debug.Log("Vx = " + Vx);
        Debug.Log("Vy = " + Vy);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;
        Debug.Log("Tiempo total = " + flightDuration);

        // Rotate projectile to face the target.
        Projectile.rotation = Quaternion.LookRotation(Target.position - Projectile.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration) {
            Vector3 Pos = new Vector3(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);
            projectileForward = Pos.normalized;
            Projectile.Translate(Pos);

            elapse_time += Time.deltaTime;

            yield return null;
        }
        Projectile.position = new Vector3(0, 0, target_Distance);
        end = true;
    }
}
