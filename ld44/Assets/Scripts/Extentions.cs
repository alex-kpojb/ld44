using UnityEngine;

public static class Rigidbody2DExt
{
    //for 2D AddExplosionForce(explosionStrength, this.transform.position, 5);
    public static void AddExplosionForce(this Rigidbody2D rb, float explosionForce, Vector2 explosionPosition, float explosionRadius, float upwardsModifier = 0.0F, ForceMode2D mode = ForceMode2D.Force)
    {
        var explosionDir = rb.position - explosionPosition;
        var explosionDistance = explosionDir.magnitude;

        if (upwardsModifier == 0)
            explosionDir /= explosionDistance;
        else
        {
            explosionDir.y += upwardsModifier;
            explosionDir.Normalize();
        }

        var n = explosionDistance/ explosionRadius;

        var t = (1 - n);
        var force = Mathf.Lerp(0, explosionForce, t) * explosionDir;

        

        rb.AddForce(force, mode);
    }
}