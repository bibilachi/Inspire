using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class ConectarGrab : XRGrabInteractable//, IXRGrabTransformer
{
    private Transform initialPosition;

    public GameObject fixedObject;

    private GameObject collisionObject;
    private static Conectar selectedConectar;
    bool flagCollision = false;

    public void Start()
    {
        initialPosition = this.transform;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!flagCollision)
            return;

        if (collisionObject.name == fixedObject.name)
        {
            fixedObject.GetComponent<MeshRenderer>().enabled = true;
            /*fixedObject.GetComponent <BoxCollider>().enabled = false;*/
            this.gameObject.SetActive(false);
            Debug.Log($"Objeto {fixedObject.name} foi acoplado.");
        }
        else
        {
            this.transform.position = initialPosition.position;
            this.transform.eulerAngles = initialPosition.eulerAngles;
            Debug.Log($"Objeto {fixedObject.name} voltou ao inicio.");
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        flagCollision = true;
        collisionObject = other.gameObject;
        Debug.Log(this.gameObject.name + " Trigger ON com " + other.gameObject.name);
    }

    public void OnTriggerExit(Collider other)
    {
        flagCollision = true;
        Debug.Log(this.gameObject.name + " Trigger OFF com " + other.gameObject.name);
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
