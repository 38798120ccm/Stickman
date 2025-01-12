using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject me;
    public CircleCollider2D MyCollider;
    public float strattime = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        MyCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
            StartCoroutine(PlayertounchDestroy(me));
    }

    IEnumerator PlayertounchDestroy(GameObject gameObject)
    {
        yield return new WaitForSeconds(0.3f);
        MyCollider.enabled = false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
