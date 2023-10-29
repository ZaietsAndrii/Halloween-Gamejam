using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSuitBoss1 : MonoBehaviour
{
    public float delay;
    private Animator animator;
    private GameObject player;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        transform.LookAt(player.transform.position);
    }

    private IEnumerator WaitUntilStartAnimation()
    {
        yield return new WaitForSeconds(delay);
        animator.SetTrigger("Attack");
    }
}
