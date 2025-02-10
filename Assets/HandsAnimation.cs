using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandsAnimation : MonoBehaviour
{
    [SerializeField] private InputActionReference gripReference;
    [SerializeField] private InputActionReference triggerReference;
    [SerializeField] private Animator handAnimator;

    private void OnEnable()
    {
        gripReference.action.Enable();
        triggerReference.action.Enable();
    }

    private void OnDisable()
    {
        gripReference.action.Disable();
        triggerReference.action.Disable();
    }

    void Update()
    {
        float triggerValue = triggerReference.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerValue);  // **แก้ "trigger" เป็น "Trigger" ตาม Animator**

        float gripValue = gripReference.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValue);
    }
}


