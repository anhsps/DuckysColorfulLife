using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch6 : MonoBehaviour
{
    //[SerializeField] private Button redSwitch, greenSwitch, brownSwitch;
    [SerializeField] private Button[] buttons = new Button[3];
    [SerializeField] private bool[] states = { true, true, true };
    [SerializeField] private Sprite[] srs1_1, srs1_2, srs2_1, srs2_2, srs3_1, srs3_2;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i + 1;// fix loi closure; i la bien tham chieu, gt k co dinh
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnButtonClick(int index)
    {
        SoundManager6.instance.PlaySound(5);
        if (!states[index - 1]) return;

        for (int i = 0; i < states.Length; i++)
        {
            if (i != index - 1)
            {
                ChangeProperties(-(i + 1), true);
                states[i] = true;
            }
            else
            {
                ChangeProperties(index, false);
                states[i] = false;
            }
        }
    }

    private void ChangeProperties(int index, bool state)
    {
        string tag = "SR" + Mathf.Abs(index);
        GameObject[] parentObjects = GameObject.FindGameObjectsWithTag(tag);

        foreach (var parent in parentObjects)
        {
            SpriteRenderer[] childSRs = parent.GetComponentsInChildren<SpriteRenderer>();

            foreach (var sr in childSRs)
                sr.sprite = ChangeSprites(sr.sprite, index);

            int layer = parent.gameObject.layer;
            if (layer == LayerMask.NameToLayer("Trap") || layer == LayerMask.NameToLayer("OffTrap"))
            {
                Rigidbody2D rb = parent.GetComponent<Rigidbody2D>();
                if (rb) rb.bodyType = state ? RigidbodyType2D.Dynamic : RigidbodyType2D.Static;
            }

            parent.GetComponent<Collider2D>().enabled = state;
        }
    }

    private Sprite ChangeSprites(Sprite sprite, int index)
    {
        if (index == 1 || index == -1) return ChangeSprite(sprite, index, srs1_1, srs1_2);
        else if (index == 2 || index == -2) return ChangeSprite(sprite, index, srs2_1, srs2_2);
        else if (index == 3 || index == -3) return ChangeSprite(sprite, index, srs3_1, srs3_2);
        return sprite;
    }

    private Sprite ChangeSprite(Sprite sprite, int index, Sprite[] from, Sprite[] to)
    {
        for (int i = 0; i < from.Length; i++)
        {
            if (index > 0)
            {
                if (sprite == from[i]) return to[i];
                else if (sprite == to[i]) return from[i];
            }
            else if (index < 0 && sprite == to[i]) return from[i];
        }
        return sprite;
    }
}
