using System.Collections;
using UnityEngine;

public class AdventurerAttack : MonoBehaviour
{

    [SerializeField] private float comboResetDelay = 1f;

    private Animator anim;

    public bool isAttack = false;
    public bool isCombo = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            
            Attack();
        }
    }

    public void Attack()
    {
        if (!isAttack)
        {
            isAttack = true;
            anim.SetTrigger("Attack");
        }
        else
        {
            if (!isCombo)
                isCombo = true;
        }
    }

    public void CheckCombo()
    {
        int currentCombo = anim.GetInteger("Combo");

        if (isCombo && currentCombo == 0)
        {
            anim.SetInteger("Combo", 1);
        }
            
        
        else
        {
            Invoke(nameof(ClearCombo), comboResetDelay);
        }
            

    }

    public void ClearCombo()
    {
        CancelInvoke(nameof(ClearCombo));
        isAttack = false;
        isCombo = false;
        anim.SetInteger("Combo", 0);
    }

    IEnumerator DelayClearCombo()
    {
        yield return new WaitForSeconds(comboResetDelay);
        ClearCombo();
    }


}
