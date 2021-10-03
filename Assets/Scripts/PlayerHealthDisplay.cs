using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealthDisplay : MonoBehaviour
{
    // configurable fields
    [Header("Health Information")]

    [Range(0, 100)]
    public int playerHealth = 100;

    [Min(1)]
    public int playerHealthMax = 100;

    [Header("Heart Slots")]
    [SerializeField] Image firstHeartSlot;
    [SerializeField] Image secondHeartSlot;
    [SerializeField] Image thirdHeartSlot;
    [SerializeField] Image fourthHeartSlot;
    [SerializeField] Image fifthHeartSlot;

    // images for heart states
    [Header("Heart Images")]
    [SerializeField] Sprite heartImageEmpty;
    [SerializeField] Sprite heartImageHalf;
    [SerializeField] Sprite heartImageFull;

    // totalHeartSegments is total number of hearts * number of segments (half and full) per heart
    int totalHeartSegments = 10;

    // segmentsPerHeart represents the total number of states per heart (half and full)
    int segmentsPerHeart = 2;

    // healthPerSegment will be evaluated at the start using the players configured health
    float healthPerSegment = 0f;

    // orderedHearts represents the list of health slots in the order they appear
    // index 0 = furthest left heart, index 5 = furthest right heart
    Image[] orderedHearts;

    // Start is called before the first frame update
    void Start()
    {
        healthPerSegment = this.playerHealthMax / totalHeartSegments;
        orderedHearts = new Image[] { firstHeartSlot, secondHeartSlot, thirdHeartSlot, fourthHeartSlot, fifthHeartSlot };
    }

    // Update is called once per frame
    void Update()
    {
        updatePlayerHealthDisplay(playerHealth);
    }

    public void updatePlayerHealthDisplay(float currentPlayerHealth)
    {
        float currentSegmentsToDisplay = Mathf.Round(currentPlayerHealth / healthPerSegment);

        // slot 1 (min = 0, max = 2)
        // slot 2 (min = 2, max = 4)
        // slot 3 (min = 4, max = 6)
        // slot 4 (min = 6, max = 8)
        // slot 5 (min = 8, max = 10)
        for (int i = 0, len = orderedHearts.Length; i < len; i++)
        {
            int heartOrder = i + 1; // 0, 2, 4, 
            Image currentHeart = orderedHearts[i];

            int slotMin = i * segmentsPerHeart;
            int slotMax = heartOrder * segmentsPerHeart;
            int slotMed = slotMax - 1;

            if (currentSegmentsToDisplay <= slotMin)
            {
                currentHeart.sprite = heartImageEmpty;
            }
            else if (currentSegmentsToDisplay == slotMed)
            {
                currentHeart.sprite = heartImageHalf;
            }
            else
            {
                currentHeart.sprite = heartImageFull;
            }
        }
    }
}
