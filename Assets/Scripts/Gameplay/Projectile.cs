using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public GameObject impactPrefab;
    public float impactScale;

    public Action Triggered;

    private Transform _target;

    public void LaunchAt(Transform target)
    {
        _target = target;
        StartCoroutine(MoveTowardsTarget());
    }

    private IEnumerator MoveTowardsTarget()
    {
        while (_target != null)
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            float distance = Vector3.Distance(transform.position, _target.position);

            if (distance <= 0.1f)
            {
                if (Triggered != null)
                {
                    Triggered();
                }

                GameObject impact = Instantiate(impactPrefab, _target.position, Quaternion.identity);
                impact.transform.localScale = Vector3.one * impactScale;

                Destroy(gameObject);
                yield break;
            }

            yield return null;
        }
    }
}
