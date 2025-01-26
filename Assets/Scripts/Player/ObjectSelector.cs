using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class ObjectSelector : MonoBehaviour
{
    public Material selectionMaterial;
    public Material highlightMaterial;

    private Material originalMaterialSelection;
    private Material originalMaterialHighlight;
    private Transform selection;
    private Transform highlight;
    private RaycastHit raycastHit;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        manageHighlightObject();
        manageSelectObject();
    }

    public void manageHighlightObject()
    {
        if (highlight != null && highlight!= selection)
        {
            highlight.GetComponent<MeshRenderer>().material = originalMaterialHighlight;
            highlight = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
        {
            highlight = raycastHit.transform;
            if (highlight.CompareTag("Selectable") && highlight != selection)
            {
                if (highlight.GetComponent<MeshRenderer>().material != highlightMaterial)
                {
                    originalMaterialHighlight = highlight.GetComponent<MeshRenderer>().material;
                    highlight.GetComponent<MeshRenderer>().material = highlightMaterial;
                }
            }
        }
        else
        {
            highlight = null;
        }
    }

    public void manageSelectObject()
    {

        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if(highlight)
            {
                if(selection != null)
                {
                    selection.GetComponent<MeshRenderer>().material = originalMaterialSelection;
                }
                selection = raycastHit.transform;
                if(selection.GetComponent<MeshRenderer>().material != selectionMaterial)
                {
                    originalMaterialSelection = originalMaterialHighlight;
                    selection.GetComponent<MeshRenderer>().material = selectionMaterial;
                    Debug.Log("Selected");
                }
                highlight = null;
            }
            else //Cuando se da click en un espacio vacio o sin objeto seleccionable
            {
                if(selection)
                {
                    selection.GetComponent<MeshRenderer>().material = originalMaterialSelection;
                    selection = null;
                    Debug.Log("UnSelected");
                }
            }
        }
    }

}
