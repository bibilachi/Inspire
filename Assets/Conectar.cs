using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class Conectar : MonoBehaviour//, IXRGrabTransformer
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private Material material;
    public Material hoverMaterial;
    public Material targetMaterial;
    public Material nextMaterial;

    public GameObject fixedObject;

    private GameObject collisionObject;
    private static Conectar selectedConectar;
    bool flagCollision = false;

    public static List<Conectar> AllConectar = new List<Conectar>();

    public void Start()
    {
        initialPosition = this.transform.position;
        initialRotation = this.transform.rotation;

        material = fixedObject.GetComponent<MeshRenderer>().material;

        AllConectar.Add(this);
        ChangeMaterialToNext();
    }

    public void OnDestroy()
    {
        AllConectar.Remove(this);
    }

    public void OnTriggerEnter(Collider other)
    {
        flagCollision = true;
        if (other.gameObject == fixedObject)
        {
            fixedObject.GetComponent<MeshRenderer>().material = hoverMaterial;
            collisionObject = other.gameObject;
            Debug.Log(this.gameObject.name + " Trigger ON com " + other.gameObject.name);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        flagCollision = false;
        if (other.gameObject == fixedObject)
        {
            fixedObject.GetComponent<MeshRenderer>().material = targetMaterial;
            collisionObject = null;
            Debug.Log(this.gameObject.name + " Trigger OFF com " + other.gameObject.name);
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

    public void SelectedObject()
    {
        MeshRenderer mr = fixedObject.GetComponent<MeshRenderer>();
        mr.enabled = true;
        mr.material = targetMaterial;
        Debug.Log("CHAMOU AQUI: SelectedObject ");
        //GameObject obj = e.interactableObject.transform.gameObject;
        //Conectar con = obj.GetComponent<Conectar>();
        //if (con != null)
        //    selectedConectar = con;
    }


    public static void UngrabObject(SelectExitEventArgs e)
    {
        Debug.Log("CHAMOU AQUI: UngrabObject ");
        selectedConectar.dropObject();
    }

    public void dropObject()
    {
        Debug.Log("CHAMOU AQUI: dropObject ");
        //if (fixedObject != collisionObject)
        //    return;

        //if (AssemblerManager.dropObject(collisionObject))
        //{
        //    this.gameObject.SetActive(false);
        //}
        if (collisionObject == null)
        {
            this.transform.position = initialPosition;
            this.transform.rotation = initialRotation;
            fixedObject.GetComponent<MeshRenderer>().enabled = false;
            Debug.Log($"Objeto {fixedObject.name} voltou ao inicio.");
            return;
        }

        if (collisionObject.name == fixedObject.name && AssemblerManager.dropObject(collisionObject))
        {
            //fixedObject.GetComponent<MeshRenderer>().enabled = true;
            /*fixedObject.GetComponent <BoxCollider>().enabled = false;*/
            this.gameObject.SetActive(false);
            fixedObject.GetComponent<MeshRenderer>().material = material;
            GameObject nextObj = AssemblerManager.NextObject();
            foreach(Conectar obj in AllConectar)
                obj.ChangeMaterialToNext();
            Debug.Log($"Objeto {fixedObject.name} foi acoplado.");
        }
        else
        {
            this.transform.position = initialPosition;
            this.transform.rotation = initialRotation;
            fixedObject.GetComponent<MeshRenderer>().enabled = false;
            Debug.Log($"Objeto {fixedObject.name} voltou ao inicio.");
        }
    }

    public void ChangeMaterialToNext()
    {
        Debug.Log(this.gameObject.name + " " + fixedObject.name + " " + AssemblerManager.NextObject().name);
        if (fixedObject == AssemblerManager.NextObject())
        {
            Debug.Log("Hello");
            MeshRenderer[] mrList = this.gameObject.GetComponents<MeshRenderer>();
            foreach (var mr in mrList)
            {
                Debug.Log("A " + mr.gameObject.name);
                mr.material = nextMaterial;
            }

            mrList = this.gameObject.GetComponentsInChildren<MeshRenderer>();
            foreach (var mr in mrList)
            {
                Debug.Log("A " + mr.gameObject.name);
                mr.material = nextMaterial;
            }
        }
    }
}
