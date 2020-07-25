using System.Collections;
using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{
    public NPC_Manager manager;
    public GameObject textBox;

    private string displayText;
    [SerializeField] float displayDelayTime = 2f;
    [SerializeField] float textDelayTime = 0.02f;

    private bool isRunningDayCycle = false;
    public TextMeshProUGUI dialogue;

    private void Start()
    {
        textBox.SetActive(false);
    }

    void Update()
    {
        // wait for players input
        if (Input.GetKeyDown(KeyCode.E) && !isRunningDayCycle)
        {
            // when input has been triggered begin running through the simulation
            DisplayNPCDayCycle();
        }
        else if (Input.GetKeyDown(KeyCode.E) && isRunningDayCycle)
        {
            // when input has been triggered begin running through the simulation
            StopNPCDayCycle();
        }
    }

    private void DisplayNPCDayCycle()
    {
        isRunningDayCycle = true;
        textBox.SetActive(true);

        // Get text from NPC Manager
        displayText = manager.GrabNextAction();

        // Type out text from NPC Manager
        StartCoroutine(TypeWritterEffect(displayText));
    }

    private void StopNPCDayCycle()
    {
        isRunningDayCycle = false;
        textBox.SetActive(false);

        StopAllCoroutines();
        dialogue.text = "";
    }

    public IEnumerator TypeWritterEffect(string _text)
    {
        dialogue.text = manager.gameObject.name + " ";

        foreach (char letter in _text.ToCharArray())
        {
            dialogue.text += letter;
            yield return new WaitForSeconds(textDelayTime);
        }

        yield return new WaitForSeconds(displayDelayTime);

        DisplayNPCDayCycle();
    }
}
