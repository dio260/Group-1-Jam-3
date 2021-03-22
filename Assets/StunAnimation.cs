using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunAnimation : MonoBehaviour
{
    private Color originalColor;
    
    private EnemyAI ai;
    private TextMesh textMesh;
    [SerializeField] private float textMovementSpeed;
    private float yOffset;
    private void Awake()
    {
        ai = GetComponentInParent<EnemyAI>();
        textMesh = GetComponent<TextMesh>();
        originalColor = textMesh.color;
        yOffset = transform.localPosition.y;
    }

    private void FixedUpdate()
    {
        Vector3 aiPosition = ai.GetTarget().position - new Vector3(0, ai.GetTarget().position.y, 0);
        Vector3 transformPosition = transform.position - new Vector3(0, transform.position.y, 0);
        Vector3 direction = (aiPosition - transformPosition).normalized;

        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;


        transform.rotation = Quaternion.Euler(0, angle - 180, 0);
    }

    private void OnEnable()
    {
        textMesh.color -= new Color(0, 0, 0, 1);
        transform.localPosition = new Vector3(0, yOffset, 0);
        StartCoroutine(Animation());
    }

    private void OnDisable()
    {
        StopCoroutine(Animation());
        textMesh.color = originalColor;
    }

    private IEnumerator Animation()
    {
        float GetValue(float x)
        {
            return Mathf.Abs(2 * x - 1);
        }

        float stunTime = ai.GetStunTimeSetting();

        while (ai.StunTime > 0)
        {
            float value = GetValue(ai.StunTime / stunTime);

            textMesh.color = originalColor - new Color(0, 0, 0, value);

            textMesh.transform.position += new Vector3(0, textMovementSpeed * Time.fixedDeltaTime, 0);

            yield return new WaitForSeconds(1f / 60f);
        }

        textMesh.color = originalColor - new Color(0, 0, 0, 1);
    }
}
