using System;
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
    public GameObject OriginalCard;
    private void Update()
    {
        if (IsAllPromptsExecuted(OriginalCard, ChangeCard, MainGame.Move, MainGame.Lose, MainGame.Add))
        {
            ChangeCard.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            ChangeCard.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    public void ValidatePrompt()
    {
        ApplyPrompts(ChangeCard, MainGame.Keep, MainGame.Move, MainGame.Lose, MainGame.Add);
    }

    public void ApplyPrompts(GameObject pCard, PromptAsset pKeep, PromptAsset pMove, PromptAsset pLose, PromptAsset pAdd)
    {
        List<DraggableSticker> stickers = pCard.GetComponentsInChildren<DraggableSticker>().ToList();

        //Keep prompt should not allow those stickers from being moved
        //Add prompt should also affect stickers already there if and only if they don't have a move prompt.
        List<string> immovableStickers = new List<string> { pKeep.AffectedSticker.ToSafeString() };

        if (pMove.AffectedSticker != pAdd.AffectedSticker)
        {
            immovableStickers.Add(pAdd.AffectedSticker.ToSafeString());
        }


        List<string> affectedStickers = new List<PromptAsset> { pMove, pLose}.Select(prompt => prompt.AffectedSticker.ToSafeString()).ToList();

        List<DraggableSticker> foundStickers = stickers
            .Where(x => IsStickerAffectedByPrompts(immovableStickers, x))
            .ToList();
        foundStickers.AddRange(stickers.Where(x => !IsStickerAffectedByPrompts(affectedStickers, x)));

        foreach (DraggableSticker sticker in foundStickers)
        {
            sticker.enabled = false;
        }
    }

    public bool IsAllPromptsExecuted(GameObject pOriginalCard, GameObject pChangedCard, PromptAsset pMove, PromptAsset pLose, PromptAsset pAdd)
    {
        List<Tuple<string, float, float>> originalCardSerialized; 
        List<Tuple<string, float, float>> changedCardSerialized;
        bool hasNoChangesBeenMade = CardComparer.CompareCards(pOriginalCard, pChangedCard, out originalCardSerialized, out changedCardSerialized);
        if (hasNoChangesBeenMade)
            return false;
        //Loses
        int originalNumOfLoseStickers = originalCardSerialized
                                                       .Where(sticker => sticker.Item1.Contains(pLose.AffectedSticker.ToSafeString()))
                                                       .Count();
        int changedNumOfLoseStickers = changedCardSerialized
                                                       .Where(sticker => sticker.Item1.Contains(pLose.AffectedSticker.ToSafeString()))
                                                       .Count();

        if (originalNumOfLoseStickers <= changedNumOfLoseStickers)
            return false;



        //Adds
        int originalNumOfAddStickers = originalCardSerialized
                                                        .Where(sticker => sticker.Item1.Contains(pAdd.AffectedSticker.ToSafeString()))
                                                        .Count();
        int changedNumOfAddStickers = changedCardSerialized
                                                        .Where(sticker => sticker.Item1.Contains(pAdd.AffectedSticker.ToSafeString()))
                                                        .Count();

        if (originalNumOfAddStickers >= changedNumOfAddStickers)
            return false;


        //Moves
        Tuple<string,float,float>[] moveableStickers = originalCardSerialized
                                                        .Where(sticker => sticker.Item1.Contains(pMove.AffectedSticker.ToSafeString()))
                                                        .ToArray();
        Tuple<string,float,float>[] changedStickers = changedCardSerialized
                                                        .Where(sticker => sticker.Item1.Contains(pMove.AffectedSticker.ToSafeString()))
                                                        .ToArray();
        if (moveableStickers.Length > changedStickers.Length) 
        {
            return false;
        }

        for (int i = 0; i< moveableStickers.Length; i++)
        {
            if (moveableStickers[i].Item2 == changedStickers[i].Item2 &&
                moveableStickers[i].Item3 == changedStickers[i].Item3)
                return false;
        }

        
        return true;
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
        int randomIndex = UnityEngine.Random.Range(0, availableCards.Count);
        return availableCards[randomIndex];
    }


    //Split the sticker asset name
    //check if it's in the list of stickers

    public bool IsStickerAffectedByPrompts(List<string> prompts, DraggableSticker sticker)
    {
        string[] stickerNames = sticker.stickerAsset.name.Split('_');
        return prompts.Contains(stickerNames[0]) || prompts.Contains(stickerNames[1]);
    }




}
