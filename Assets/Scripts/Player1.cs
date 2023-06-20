using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player1 : PlayableCharacter
{
    [SerializeField] private Animator m_batAnimation;
    [SerializeField] private PlayableCharacterData m_checkBatUser;
    // For skills animation
    private bool skillPressed;
    private bool canUseSkill;
    private bool isAtDistance;
    // Inventory
    [SerializeField] private UI_Inventory uiInventory;
    private Inventory inventory;
    void Start()
    {
       
        canUseSkill = true;
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);

    }
    // Skill usage and cooldowns
    // Skills and skills animation cooldowns
    private void UseSkill()
    {
        if (Input.GetKeyDown(KeyCode.E) && canUseSkill)
        {
            skillPressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && !canUseSkill)
        {
            Debug.Log("Cannot use that now!");
        }
        else
        {
            skillPressed = false;
        }
    }
    private IEnumerator WaitToMove()
    {
        canMove = false;
        canRotate = false;
        yield return new WaitForSeconds(2.3f);
        canMove = true;
        canRotate = true;
    }
    private IEnumerator AbilityCooldown()
    {
        canUseSkill = false;
        yield return new WaitForSeconds(4);
        canUseSkill = true;
    }
    //Specific skills
    //P1 Skills
    public void DistanceChecker(bool m_canUseBat)
    {
        isAtDistance = m_canUseBat;
    }
    public void UseBat()
    {
        if (m_checkBatUser.isBatUser && isAtDistance && skillPressed)
        {
            Debug.Log("Bonk");
            m_batAnimation.SetBool("isUsingSkill", true);
            StartCoroutine(AbilityCooldown());
            StartCoroutine(WaitToMove());
        }
        else if (m_checkBatUser.isBatUser && !isAtDistance && skillPressed)
        {
            Debug.Log("I need to be behind a creature!");

        }
        else
        {
            m_batAnimation.SetBool("isUsingSkill", false);
        }
    }
    protected override void OnUpdating()
    {
        UseBat();
        UseSkill();
    }
}
