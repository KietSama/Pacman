using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour
{

    public GameObject go;

    float magnitube = 0.1f;

    public void OnClick()
    {
        //SpecialEffectsHelper.Instance.Explosion(go.GetComponent<Transform>().position);

        StartCoroutine(Shake(0.5f));

        //Destroy(go);
    }

    IEnumerator Shake(float duration)
    {
        float elapsed = 0f;

        Vector3 OriginCamPos = Camera.main.transform.position;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float PercentComplete = elapsed / duration;
            float damper = 1f - Mathf.Clamp(4f * PercentComplete - 3f, 0f, 1f);

            // map value to [-1,1]

            float x = Random.value * 2f - 1f;
            float y = Random.value * 2f - 1f;

            x *= magnitube * damper;
            y *= magnitube * damper;

            Camera.main.transform.position = new Vector3(x + OriginCamPos.x, y + OriginCamPos.y, OriginCamPos.z);

            yield return null;
        }

        Camera.main.transform.position = OriginCamPos;
    }
}
