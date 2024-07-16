// Moss Limpert 07/16/24
// https://www.youtube.com/watch?v=mz9xfDQ4FCk

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class MovePanel : MonoBehaviour
{
    [SerializeField]
    GameObject panel;

    private Animator animator;

    private void Start()
    {
        animator = panel.GetComponent<Animator>();
    }

    private void OnGUI()
    {
        if (Input.GetButton("Cancel")) TogglePanel(false);
    }

    // activates the animator transition to open the panel
    public void TogglePanel(bool state)
    {
        if (animator != null)
        { 
            animator.SetBool("Open", state);
        }
    }

}
