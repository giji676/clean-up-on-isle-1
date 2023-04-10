using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
    [SerializeField] private Image progressBar;
    [SerializeField] private Image damageOverlay;

    [SerializeField] private float duration;
    [SerializeField] private float fadeSpeed;
    private float durationTimer;
    
    private void Update() {
        if (damageOverlay.color.a == 0) return;

        // Applys fade onto the damageOverlay image
        durationTimer += Time.deltaTime;
        if (durationTimer > duration) {
            float tempAplha = damageOverlay.color.a;
            tempAplha -= Time.deltaTime * fadeSpeed;
            damageOverlay.color = new Color(damageOverlay.color.r, damageOverlay.color.g, damageOverlay.color.b, tempAplha);
        }
    }

    public void DamageOverlay() {
        durationTimer = 0f;
        damageOverlay.color = new Color(damageOverlay.color.r, damageOverlay.color.g, damageOverlay.color.b, 0.5f);
    }

    public void UpdateProgressBar(int current, int goal) {
        progressBar.fillAmount = Mathf.InverseLerp(0, goal, current);
    }
}