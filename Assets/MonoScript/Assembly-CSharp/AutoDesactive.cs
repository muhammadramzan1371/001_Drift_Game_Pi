using UnityEngine;

public class AutoDesactive : MonoBehaviour
{
	public void SetAutoDesactivate(float time)
	{
		CancelInvoke();
		Invoke("Desactive", time);
	}

	private void Desactive()
	{
		base.gameObject.SetActive(false);
	}
}
