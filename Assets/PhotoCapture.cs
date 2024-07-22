using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class PhotoCapture: MonoBehaviour
{
    public Button captureButton;
    public Button shareButton;
    public Image displayImage;
    private string screenshotPath;

    void Start()
    {
        captureButton.onClick.AddListener(CaptureScreenshot);
        shareButton.onClick.AddListener(ShareScreenshot);
    }

    void CaptureScreenshot()
    {
        StartCoroutine(CaptureScreenshotCoroutine());
    }

    IEnumerator CaptureScreenshotCoroutine()
    {
        yield return new WaitForEndOfFrame();
        screenshotPath = Path.Combine(Application.persistentDataPath, "screenshot.png");
        ScreenCapture.CaptureScreenshot(screenshotPath);
        yield return new WaitForSeconds(0.5f); // Wait for the screenshot to be saved

        if (File.Exists(screenshotPath))
        {
            byte[] imageBytes = File.ReadAllBytes(screenshotPath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageBytes);
            displayImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
        else
        {
            Debug.LogError("Screenshot capture failed.");
            File.Delete(screenshotPath); // Delete the file if capture failed
        }
    }

    void ShareScreenshot()
    {
        if (File.Exists(screenshotPath))
        {
            // Use Cross Platform Native Plugins or any other sharing plugin to share the image
            //Share.ShareImage(screenshotPath, "Check out my screenshot!");
        }
        else
        {
            Debug.LogError("No screenshot available to share.");
        }
    }
}