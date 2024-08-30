using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static PromptAsset;

public class PromptManager : MonoBehaviour
{
    [SerializeField]
    GameStateAsset MainGame;

    public GameObject ChangeCard, OriginalCard;
    private void Update()
    {
        if (IsAllPromptsExecuted(OriginalCard, ChangeCard, MainGame.Keep, MainGame.Move, MainGame.Lose, MainGame.Add))
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
        ApplyPromptLimits(ChangeCard, MainGame.Keep, MainGame.Move, MainGame.Lose, MainGame.Add);
    }

    public static void ApplyPromptLimits(GameObject pCard, PromptAsset pKeep, PromptAsset pMove, PromptAsset pLose, PromptAsset pAdd)
    {
        List<DraggableSticker> stickers = pCard.GetComponentsInChildren<DraggableSticker>().ToList();

        //Keep prompt should not allow those stickersToChange from being moved
        //Add prompt should also affect stickersToChange already there if and only if they don't have a moveCard prompt.
        List<Affect> immovableStickers = new List<Affect> { pKeep.AffectedSticker};
        List<Affect> affectedStickers = new List<PromptAsset> { pMove, pLose, pAdd}.Select(prompt => prompt.AffectedSticker).ToList();

        List<DraggableSticker> foundStickers = stickers
            .Where(x => IsStickerAffectedByPrompts(immovableStickers, x))
            .ToList();
        foundStickers.AddRange(stickers.Where(x => !IsStickerAffectedByPrompts(affectedStickers, x)));

        foreach (DraggableSticker sticker in foundStickers)
        {
            sticker.enabled = false;
        }
    }

    public static bool IsAllPromptsExecuted(GameObject pOriginalCard, GameObject pChangedCard, PromptAsset pKeep, PromptAsset pMove, PromptAsset pLose, PromptAsset pAdd)
    {
        List<Tuple<string, float, float>> originalCardSerialized; 
        List<Tuple<string, float, float>> changedCardSerialized;
        bool hasNoChangesBeenMade = CardComparer.CompareCards(pOriginalCard, pChangedCard, out originalCardSerialized, out changedCardSerialized);
        if (hasNoChangesBeenMade)
            return false;
        //Loses
        int originalNumOfLoseStickers = originalCardSerialized
                                                       .Where(sticker => sticker.Item1.Contains(pLose.AffectedSticker.ToSafeString()) &&
                                                       !sticker.Item1.Contains(pKeep.AffectedSticker.ToSafeString()) 
)
                                                       .Count();
        int changedNumOfLoseStickers = changedCardSerialized
                                                       .Where(sticker => sticker.Item1.Contains(pLose.AffectedSticker.ToSafeString()))
                                                       .Count();

        if (originalNumOfLoseStickers != 0 && originalNumOfLoseStickers <= changedNumOfLoseStickers)
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
                                                        .Where(sticker =>
                                                            !sticker.Item1.Contains(pAdd.AffectedSticker.ToSafeString()) &&
                                                            !sticker.Item1.Contains(pKeep.AffectedSticker.ToSafeString()) &&
                                                            sticker.Item1.Contains(pMove.AffectedSticker.ToSafeString()))
                                                        .ToArray();
        Tuple<string,float,float>[] changedStickers = changedCardSerialized
                                                        .Where(sticker => sticker.Item1.Contains(pMove.AffectedSticker.ToSafeString()))
                                                        .ToArray();
        if(moveableStickers.Length == 1 && pLose.AffectedSticker == pMove.AffectedSticker)
        {
            return true;
        }

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

    public static void ApplyPrompts(GameObject pCardOne, GameObject pCardTwo, GameStateAsset pGameState)
    {
        GeneratePrompts(pGameState, out PromptAsset keepCard, out PromptAsset loseCard, out PromptAsset moveCard, out PromptAsset addCard);
        ApplyPromptToCard(pCardOne, pGameState, keepCard, moveCard, addCard, loseCard );
        ApplyPromptToCard(pCardTwo, pGameState, keepCard, moveCard, addCard, loseCard );

        pGameState.OpponentAdd = addCard;
        pGameState.OpponentMove = moveCard;
        pGameState.OpponentKeep = addCard;
        pGameState.OpponentLose = loseCard;
    }

    static void ApplyPromptToCard(GameObject pCard, GameStateAsset pGameState, PromptAsset keepCard, PromptAsset loseCard, PromptAsset moveCard, PromptAsset addCard)
    {
        List<DraggableSticker> stickersToChange = pCard
                                                  .GetComponentsInChildren<DraggableSticker>()
                                                  .Where(x => keepCard.AffectedSticker != x.stickerAsset.color || keepCard.AffectedSticker != x.stickerAsset.shape)
                                                  .ToList();

        //Remove
        DraggableSticker stickerToRemove = stickersToChange
                                                .Where(sticker =>
                                                        sticker.stickerAsset.color == loseCard.AffectedSticker ||
                                                        sticker.stickerAsset.shape == loseCard.AffectedSticker)
                                                .FirstOrDefault();
        if (stickerToRemove != null)
        {
            stickersToChange.Remove(stickerToRemove);
            Destroy(stickerToRemove.gameObject);
        }

        //Move 
        List<DraggableSticker> stickersToMove = stickersToChange
                                                .Where(sticker =>
                                                        sticker.stickerAsset.color == moveCard.AffectedSticker ||
                                                        sticker.stickerAsset.shape == moveCard.AffectedSticker)
                                                .ToList();

        if (stickersToMove.Count > 0)
        {
            for (int i = 0; i < stickersToMove.Count; i++)
            {
                DraggableSticker stickerToMove = stickersToMove[i];
                stickerToMove.gameObject.transform.localPosition = Vector3.zero + Vector3.one * (0.1f * i);
            }
        }

        //Add Move
        //Find a sticker position from available stickers
        if (stickersToChange.Count > 0)
        {
            Instantiate(stickersToChange.First(), pCard.transform);
        }
        else
        {
            GameObject sticker = Instantiate(pGameState.StickerPrefab, pCard.transform);
            Sprite loadStickerSprite = Resources
                                            .LoadAll<StickerAsset>("StickerAssets")
                                            .Where(asset =>
                                                asset.color == addCard.AffectedSticker ||
                                                asset.shape == addCard.AffectedSticker)
                                            .First().stickerSprite;

            sticker.GetComponent<Image>().sprite = loadStickerSprite;
            sticker.transform.position = new Vector3(0.5f, 0.2f);
        }
    }



    public void GenerateAndAssignPrompts()
    {
        GenerateAndAssignPrompts(MainGame);
    }

    public static void GenerateAndAssignPrompts(GameStateAsset pGameState)
    {
        GeneratePrompts(pGameState, out PromptAsset keep, out PromptAsset lose, out PromptAsset move, out PromptAsset add);
        pGameState.Keep = keep;
        pGameState.Lose = lose;
        pGameState.Add = add;
        pGameState.Move = move;
    }

    public static void GeneratePrompts(GameStateAsset pGameState ,out PromptAsset pKeep, out PromptAsset pLose, out PromptAsset pMove, out PromptAsset pAdd)
    {
        //Keep
        List<PromptAsset> availableKeepcards = pGameState.AllPromptAssets
                                    .Where(x => x.PromptType == PromptAsset.Type.Keep)
                                    .ToList();
        PromptAsset keep = GetRandomPrompt(availableKeepcards);

        //Lose
        List<PromptAsset> availableLoseCards = pGameState.AllPromptAssets
                                    .Where(x => x.PromptType == PromptAsset.Type.Lose)
                                    .Where(x => !keep.IncompatiblePrompts.Contains(x))
                                    .ToList();
        PromptAsset lose = GetRandomPrompt(availableLoseCards);

        //Add
        List<PromptAsset> availableAddCards = pGameState.AllPromptAssets
                                    .Where(x => x.PromptType == PromptAsset.Type.Add)
                                    .Where(x => !keep.IncompatiblePrompts.Contains(x))
                                    .Where(x => !lose.IncompatiblePrompts.Contains(x))
                                    .ToList();
        PromptAsset add = GetRandomPrompt(availableAddCards);

        //Move
        List<PromptAsset> availableMoveCards = pGameState.AllPromptAssets
                                    .Where(x => x.PromptType == PromptAsset.Type.Move)
                                    .Where(x => !keep.IncompatiblePrompts.Contains(x))
                                    .ToList();
        PromptAsset move = GetRandomPrompt(availableMoveCards);

        pKeep = keep;
        pLose = lose;
        pMove = move;
        pAdd = add;
    }

    public static PromptAsset GetRandomPrompt(List<PromptAsset> availableCards)
    {
        int randomIndex = UnityEngine.Random.Range(0, availableCards.Count);
        return availableCards[randomIndex];
    }


    //Split the sticker asset name
    //check if it's in the list of stickersToChange

    public static bool IsStickerAffectedByPrompts(List<Affect> prompts, DraggableSticker sticker)
    {
        Affect color = sticker.stickerAsset.color;
        Affect shape = sticker.stickerAsset.shape;
        return prompts.Contains(color) || prompts.Contains(shape);
    }




}
