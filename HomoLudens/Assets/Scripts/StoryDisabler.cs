using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryDisabler : MonoBehaviour
{
    public GameObject StoryCanvas;

    private void Start()
    {
        StartCoroutine(DisableStory());
    }

    IEnumerator DisableStory()
    {
        yield return new WaitForSeconds(8f);

        StoryCanvas.SetActive(false);
    }
}
