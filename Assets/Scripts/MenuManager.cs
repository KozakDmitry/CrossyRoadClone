using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private Text endgameNumber;
    [SerializeField]
    private Image muteIcon;
    [SerializeField]
    private RectTransform menuField;
    [SerializeField]
    private RectTransform recordField;
    [SerializeField]
    private RectTransform endgameField;
    [SerializeField]
    private List<Sprite> muteIcons;
    private float volumeValue;
    private bool muted = false;
    [SerializeField]
    private GameManager gm;
    public void StartGame()
    {
        menuField.DOAnchorPos(new Vector2(2400, 0), 0.25f);
        recordField.gameObject.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void LoseGameChange(Text score)
    {
        endgameNumber.text = score.text;
        endgameField.DOAnchorPos(new Vector2(0, 0), 0.25f);
        
    }

    void Start()
    {
        recordField.gameObject.SetActive(false);
        menuField.DOAnchorPos(new Vector2(0,0), 0.25f);
    }

    public void ChangeSound()
    {
        if (!muted)
        {
            muted = !muted;
            volumeValue = AudioListener.volume;
            AudioListener.volume = 0;
            muteIcon.sprite = muteIcons[1];
        }
        else
        {
            AudioListener.volume = volumeValue;
            muteIcon.sprite = muteIcons[0];
        }

    }


    void Update()
    {
        
    }
}
