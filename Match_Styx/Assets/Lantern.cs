using UnityEditor.Build;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    public GameObject ChainJoint;
    public GameObject LanternJoint;

    public SpriteRenderer LanternImage;
    public Sprite LitLanternSprite;

    public GameCycle Game;

    bool _isLit;
    bool _isEntered;
    // Start is called before the first frame update
    void Awake()
    {
        AnimateLantern();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isEntered && !_isLit && Input.GetKeyDown(KeyCode.T))
        {
            LightLantern();
        }
    }

  

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            _isEntered = true;
            if (!LeanTween.isTweening(ChainJoint) && !LeanTween.isTweening(LanternJoint))
            {
                AnimateLantern(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            _isEntered = false;
        }
    }

    public void LightLantern()
    {
        if (_isLit)
            return;

        LanternImage.sprite = LitLanternSprite;
        LanternImage.color = new Color(1, 0.6f, 0.27f);
        _isLit = true;

        Game.OnWarmZoneExpanded.Invoke();
    }

    void AnimateLantern(bool pIsFromStart = false)
    {
        if (pIsFromStart)
        {
            LeanTween
                .rotateZ(ChainJoint, -15, 2)
                .setOnComplete
                (() => 
                    {
                        LeanTween
                            .rotateZ(ChainJoint, 15, 2)
                            .setLoopPingPong(3)
                            .setOnComplete
                            (() =>
                            {
                                LeanTween.rotateZ(ChainJoint, 0, 1);
                            }
                            );
                    }
                );
            LeanTween
                .rotateZ(LanternJoint, -25, 1.2f)
                .setDelay(0.4f)
                .setOnComplete
                (() =>
                {
                    LeanTween
                    .rotateZ(LanternJoint, 25, 2)
                    .setLoopPingPong(3)
                    .setOnComplete
                    (() =>
                    {
                        LeanTween.rotateZ(LanternJoint, 0, 2);
                    }
                    );
                }
                );
        }
        else
        {
            LeanTween
                .rotateZ(ChainJoint, 10, 2)
                .setLoopPingPong(3)
                .setOnComplete
                (() =>
                    {
                        LeanTween.rotateZ(ChainJoint, 0, 1);
                    }
                );
            LeanTween
                .rotateZ(LanternJoint, 25, 2)
                .setLoopPingPong(3)
                .setDelay(1.3f)
                .setOnComplete
                (() =>
                    {
                        LeanTween.rotateZ(LanternJoint, 0, 2);
                    }
                );

        }
    }
}
