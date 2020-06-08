using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuto_CamSwitch : MonoBehaviour

    public gameObject CM_vcam1_Main;
    public gameObject CM_vcam1_Statue;
    public gameObject CM_vcam1_DysonSphere;
    public gameObject CM_vcam1_Sundial;
    public gameObject CM_vcam1_Arch;
    public gameObject CM_vcam1_Stele;
public gameObject CM_vcam1_Temple;

// Update is called once per frame
void Update()
    {
        if (Gamepad.current.buttonSouth.wasPressedThisFrame) // Dysonsphere
    {
        CM_vcam_DysonSphere.SetActive(true);
        CM_vcam_Main.SetActive(false);
        CM_vcam_Statue.SetActive(false);
        CM_vcam_Sundial.SetActive(false);
        CM_vcam_Arch.SetActive(false);
        CM_vcam_Stele.SetActive(false);
        CM_vcam_Temple.SetActive(false);
        }
   
    if (Gamepad.current.buttonEast.wasPressedThisFrame) // Statue
    { 
        CM_vcam_DysonSphere.SetActive(false);
        CM_vcam_Main.SetActive(false);
        CM_vcam_Statue.SetActive(true);
        CM_vcam_Sundial.SetActive(false);
        CM_vcam_Arch.SetActive(false);
        CM_vcam_Stele.SetActive(false);
        CM_vcam_Temple.SetActive(false);
        }

    if (Gamepad.current.buttonNorth.wasPressedThisFrame) // Stele
    {
        CM_vcam_DysonSphere.SetActive(false);
        CM_vcam_Main.SetActive(false);
        CM_vcam_Statue.SetActive(false);
        CM_vcam_Sundial.SetActive(false);
        CM_vcam_Arch.SetActive(false);
        CM_vcam_Stele.SetActive(true);
        CM_vcam_Temple.SetActive(false);
        }
}
