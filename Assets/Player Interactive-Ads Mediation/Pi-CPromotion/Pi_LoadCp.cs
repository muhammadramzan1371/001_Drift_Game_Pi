using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class Pi_LoadCp : MonoBehaviour
{
    public int CpID;
    public RawImage YourRawImage;
    string link;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("removeads") == 1)
        {
            gameObject.SetActive(false);
        }

        YourRawImage.enabled = false;
        
        if (Pi_cpManager.instance._links[CpID].CpTexture != null)
        {
            YourRawImage.texture = Pi_cpManager.instance._links[CpID].CpTexture;
            YourRawImage.enabled = true;
            YourRawImage.GetComponent<Button>().interactable = true;
            YourRawImage.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            StartCoroutine(GetTextureOnline());
        }

        if (Pi_cpManager.instance._links[CpID].Link != null)
        {
            link = Pi_cpManager.instance._links[CpID].Link;
        }
        else
        {
            StartCoroutine(GetTextOnline());
        }
    }

    IEnumerator GetTextureOnline()
    {
        UnityWebRequest loadings = UnityWebRequestTexture.GetTexture(Pi_cpManager.instance._links[CpID].ImageUrl);

        yield return loadings.SendWebRequest();

        if (loadings.isDone)
        {
            if (!loadings.isNetworkError)
            {
                Pi_cpManager.instance._links[CpID].CpTexture = ((DownloadHandlerTexture)loadings.downloadHandler).texture != null ? ((DownloadHandlerTexture)loadings.downloadHandler).texture : ((DownloadHandlerTexture)loadings.downloadHandler).texture;
                YourRawImage.texture = Pi_cpManager.instance._links[CpID].CpTexture;
                YourRawImage.enabled = true;
                YourRawImage.GetComponent<Button>().interactable = true;
                YourRawImage.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    IEnumerator GetTextOnline()
    {
        for (int i = 0; i < Pi_cpManager.instance._links.Length; i++)
        {
            UnityWebRequest loadings = UnityWebRequest.Get(Pi_cpManager.instance._links[CpID].TextUrl);
            yield return loadings.SendWebRequest();
            if (loadings.isDone)
            {
                if (!loadings.isNetworkError)
                {
                    Debug.Log(loadings.downloadHandler.text);
                    Pi_cpManager.instance._links[CpID].Link = loadings.downloadHandler.text;
                }
            }
        }
    }


    public void OpenLink()
    {
        Debug.Log("link is " + Pi_cpManager.instance._links[CpID].Link);
        Application.OpenURL(Pi_cpManager.instance._links[CpID].Link);
       
    }
}
