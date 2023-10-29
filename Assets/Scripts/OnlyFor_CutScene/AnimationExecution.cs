using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationExecution : MonoBehaviour
{
    public Collider DeathTrigger;
    public Animator boss;
    private PlayerController player;

    public GameObject uiPanel;
    public GameObject uiSpit;
    public GameObject uiBloodScreen;
    public GameObject uiNameOfGame;

    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip screamSound;
    [SerializeField] private AudioClip spitSound;
    [SerializeField] private AudioClip textAppearedSound;
    [SerializeField] private AudioSource music;

    private float fallDuration;
    private float fallStep;
    private float currentPlayerRotationX;
    private bool hitDone;
    
    private void Start()
    {
        player = GetComponent<PlayerController>();
        player.canAttack = false;
        fallStep = 2f;
        fallDuration = 45f;
        currentPlayerRotationX = player.transform.localRotation.x;
        print("kek");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(hitDone) { return; }

        if (other.tag == DeathTrigger.tag)
        {
            boss.SetTrigger("Attack");
            player.canMove = false;
            GetSlowWorld();
        }
        else if (other.tag == "EnemyWeapon")
        {
            hitDone = true;
            Time.timeScale = 1f;
            music.enabled = false;
            player.enabled = false;

            StartCoroutine(DeathFalling());
            uiPanel.SetActive(true);
            StartCoroutine(Dying());

        }
    }

    private void GetSlowWorld()
    {
        Time.timeScale = 0.4f;
        music.pitch = 0.6f;
    }

    private IEnumerator DeathFalling()
    {
        print(Time.deltaTime + "   " + currentPlayerRotationX + "   " + fallDuration);
        if (fallDuration <= 0f) { yield return null; }
        else
        {
            yield return new WaitForSeconds(0.00001f);
            fallDuration -= 1;
            currentPlayerRotationX -= fallStep;
            transform.localRotation = Quaternion.Euler(currentPlayerRotationX, 0, 0);
            StartCoroutine(DeathFalling());
        }
    }

    private IEnumerator Dying()
    {
        yield return new WaitForSeconds(0.1f);
        print("lol");
        SoundManager.instance.PlaySound(hitSound);
        uiBloodScreen.SetActive(true);

        yield return new WaitForSeconds(3f); // waiting

        SoundManager.instance.PlaySound(spitSound);
        yield return new WaitForSeconds(1.4f);
        uiSpit.SetActive(true);

        yield return new WaitForSeconds(1.5f); // waiting
        
        uiNameOfGame.SetActive(true);
        SoundManager.instance.PlaySound(textAppearedSound);
        Cursor.lockState = CursorLockMode.None;

    }

    //private IEnumerator Dying(GameObject _uiElement, float _wait, AudioClip _clip)
    //{
    //    yield return new WaitForSeconds(_wait);
    //    _uiElement.SetActive(true);
    //    SoundManager.instance.PlaySound(_clip);
    //}
}
