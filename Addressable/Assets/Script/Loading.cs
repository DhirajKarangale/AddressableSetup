using UnityEngine;

public class Loading : PersistentSingleton<Loading>
{
    [SerializeField] UnityEngine.UI.Slider slider;
    [SerializeField] TMPro.TMP_Text txtProgress;


    internal void UpdateProgress(float val)
    {
        txtProgress.text = val + "%";
        slider.value = val;
    }
}