using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PromptValidator : MonoBehaviour
{
    [SerializeField]
    GameStateAsset MainGame;

    public GameObject ChangeCard;


    public void ValidatePrompt()
    {
        ApplyPrompts(ChangeCard, MainGame.Keep, MainGame.Move, MainGame.Lose, MainGame.Add);
    }


    public void ApplyPrompts(GameObject pCard, PromptAsset pKeep, PromptAsset pMove, PromptAsset pLose, PromptAsset pAdd)
    {
        List<DraggableSticker> stickers = pCard.GetComponentsInChildren<DraggableSticker>().ToList();
        List<string> keepStickers = new List<string> { pKeep.AffectedSticker.ToString() };
        List<string> affectedStickers = new List<PromptAsset> { pMove, pLose, pAdd}.Select(prompt => nameof(prompt.AffectedSticker)).ToList();


        List<DraggableSticker> foundStickers = stickers
            .Where(x => IsStickerAffectedByPrompts(keepStickers, x))
            .ToList();
        foundStickers.AddRange(stickers.Where(x => !IsStickerAffectedByPrompts(affectedStickers, x)));

        foreach (DraggableSticker sticker in foundStickers)
        {
            sticker.enabled = false;
        }

    }

    public void GeneratePrompts()
    {
        //Keep
        List<PromptAsset> availableKeepcards = MainGame.AllPromptAssets
                                    .Where(x => x.PromptType == PromptAsset.Type.Keep)
                                    .ToList();
        PromptAsset keep = GetRandomPrompt(availableKeepcards);

        //Lose
        List<PromptAsset> availableLoseCards = MainGame.AllPromptAssets
                                    .Where(x => x.PromptType == PromptAsset.Type.Lose)
                                    .Where(x => !keep.IncompatiblePrompts.Contains(x))
                                    .ToList();
        PromptAsset lose = GetRandomPrompt(availableLoseCards);

        //Add
        List<PromptAsset> availableAddCards = MainGame.AllPromptAssets
                                    .Where(x => x.PromptType == PromptAsset.Type.Add)
                                    .Where(x => !keep.IncompatiblePrompts.Contains(x))
                                    .Where(x => !lose.IncompatiblePrompts.Contains(x))
                                    .ToList();
        PromptAsset add = GetRandomPrompt(availableAddCards);

        //Move
        List<PromptAsset> availableMoveCards = MainGame.AllPromptAssets
                                    .Where(x => x.PromptType == PromptAsset.Type.Move)
                                    .Where(x => !keep.IncompatiblePrompts.Contains(x))
                                    .ToList();
        PromptAsset move = GetRandomPrompt(availableMoveCards);

        MainGame.Keep = keep;
        MainGame.Lose = lose;
        MainGame.Add = add;
        MainGame.Move = move;
    }

    public PromptAsset GetRandomPrompt(List<PromptAsset> availableCards)
    {
        int randomIndex = Random.Range(0, availableCards.Count);
        return availableCards[randomIndex];
    }


    //Split the sticker asset name
    //check if it's in the list of stickers

    public bool IsStickerAffectedByPrompts(List<string> prompts, DraggableSticker sticker)
    {
        string[] stickerNames = sticker.name.Split('_');
        return prompts.Contains(stickerNames[0]) || prompts.Contains(stickerNames[1]);
    }




}
