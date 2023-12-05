using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class Conectar : MonoBehaviour//, IXRGrabTransformer
{
    public GameObject fixedObject;

    private GameObject collisionObject;
    private static Conectar selectedConectar;
    bool flagCollision = false;

    public void OnCollisionEnter(Collision collision)
    {
        flagCollision = true;
        collisionObject = collision.gameObject;

        

        if (collisionObject.name == fixedObject.name)
        {
            fixedObject.GetComponent<MeshRenderer>().enabled = true;
            /*fixedObject.GetComponent <BoxCollider>().enabled = false;*/
            this.gameObject.SetActive(false);
            Debug.Log($"Objeto {fixedObject.name} foi acoplado.");

        }


    }

    public void OnCollisionExit(Collision collision)
    {
        /*flagCollision = false;
        collisionObject = null;*/
    }

    //void OnLink(XRGrabInteractable grabInteractable) { }
    //void OnGrab(XRGrabInteractable grabInteractable) 
    //{ 
    //    grabInteractable.
    //}
    //void OnGrabCountChanged(XRGrabInteractable grabInteractable, Pose targetPose, Vector3 localScale) { }
    //void Process(XRGrabInteractable grabInteractable, XRInteractionUpdateOrder.UpdatePhase updatePhase, ref Pose targetPose, ref Vector3 localScale) { }
    //void OnUnlink(XRGrabInteractable grabInteractable) { }

    public static void SelectedObject(SelectEnterEventArgs e)
    {
        Debug.Log("CHAMOU AQUI: SelectedObject ");
        GameObject obj = e.interactableObject.transform.gameObject;
        Conectar con = obj.GetComponent<Conectar>();
        if (con != null)
            selectedConectar = con;
    }


    public static void UngrabObject(SelectExitEventArgs e)
    {
        Debug.Log("CHAMOU AQUI: UngrabObject ");
        selectedConectar.dropObject();
    }

    public void dropObject()
    {
        Debug.Log("CHAMOU AQUI: dropObject ");
        if (fixedObject != collisionObject)
            return;

        if (AssemblerManager.dropObject(collisionObject))
        {
            this.gameObject.SetActive(false);
        }
    }
}
