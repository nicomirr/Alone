using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //LanguageManager
    public string language;

    //Player
    public Vector3 playerPos;
    public Vector3 playerScale;
    public bool lookingFront;
    public bool lookingBack;
    public bool sideMove;
    public bool mustHide;
    public bool mustEscape;
    public bool hasEthansBox;
    public bool songPlayed;

    //Lights
    public SerializableDictionary<string, bool> lightsOn;
    public SerializableDictionary<string, bool> firstLightsStatusCheck;
    public bool lightsOut;

    //Inventory
    public bool isUsingFlashlight;
    public bool isUsingGlass;
    public bool isUsingParentsKey;
    public bool isUsingBathroomDrainStopper;
    public bool isUsingKeyUnderStairs;
    public bool isUsingUmbrella;
    public bool isUsingUmbrellaHandle;
    public bool isUsingKeySister;
    public bool isUsingFirePoker;
    public bool isUsingKeyEntrance;
    public bool glassFilled;
    public bool fillSoundPlayed;    
    public int itemSelected;
    public ClickableObject inventoryItem;
    public bool hasPickedUpFlashlight;
    public bool hasPickedUpFirePoker;
    public bool hasPickedUpSistersKey;
    public bool hasUsedSistersKey;
    public bool hasFlashlight;
    public bool hasGlass;
    public bool hasKeyParents;
    public bool hasDadsNote;
    public bool hasBathroomDrainStopper;
    public bool hasKeyUnderStairs;
    public bool hasUmbrella;
    public bool hasUmbrellaHandle;
    public bool hasKeySister;
    public bool hasFirePoker;
    public bool hasKeyEntrance;
    public bool hasMusicSheet;

    //ClickableObject
    public SerializableDictionary<string, bool> textFirstSetup;
    public SerializableDictionary<string, string> lookAtText;
    public SerializableDictionary<string, string> turnOnText;
    public SerializableDictionary<string, string> turnOffText;
    public SerializableDictionary<string, string> pickupText;
    public SerializableDictionary<string, string> searchText;
    public SerializableDictionary<string, string> readText;
    public SerializableDictionary<string, string> openText;
    public SerializableDictionary<string, string> closeText;
    public SerializableDictionary<string, string> useText;
    public SerializableDictionary<string, string> hasObjectText;
    public SerializableDictionary<string, string> hideText;
    public SerializableDictionary<string, bool> hasObject;
    public SerializableDictionary<string, bool> firstHasObjectStatusCheck;
    public SerializableDictionary<string, bool> canBeSearched;
    public SerializableDictionary<string, bool> canBeUsed;
    public SerializableDictionary<string, bool> firstCanBeSearchedStatusCheck;

    //CanBeOpenObjects
    public SerializableDictionary<string, bool> isOpen;

    //CanBeTurnOnOffObjects
    public SerializableDictionary<string, bool> isTurnedOn;
    public SerializableDictionary<string, bool> canBeTurnedOnOrOff;

    //CanBePickedUpObjects
    public SerializableDictionary<string, bool> hasBeenPickedUp;

    //ScenesInGame
    public bool isFlashback;
    public bool firstPlayerRoomScenePlayed;
    public bool firstDinningRoomScenePlayed;
    public bool firstDinningRoomScene;
    public bool firstKitchenScenePlayed;
    public bool firstKitchenScene;
    public bool firstParentsRoomScenePlayed;
    public bool firstParentsRoomScene;
    public bool secondParentsRoomScenePlayed;
    public bool secondParentsRoomSceneFix;
    public bool firstEntranceScenePlayed;
    public bool firstLivingroomScenePlayed;
    public bool firstPlayersRoomFlashbackScene;
    public bool firstPlayersRoomFlashbackScenePlayed;    
    public bool firstDinningRoomFlashbackScenePlayed;
    public bool firstKitchenFlashbackScenePlayed;
    public bool firstLivingRoomFlashbackScenePlayed;
    public bool firstEntranceFlashbackScenePlayed;
    public bool firstStairs2ndFloorScene;
    public bool firstStairs2ndFloorScenePlayed;
    public bool secondDinningRoomScenePlayed;
    public bool firstEntranceLoopEndingScenePlayed;
    public bool secondEntranceLoopEndingScenePlayed;
    public bool firstStairs2ndFloorLoopEndingScenePlayed;
    public bool firstHallway2ndFloorLoopEndingScenePlayed;
    public bool firstPlayersRoomLoopEndingScenePlayed;
    public bool firstAtticTruthEndingScenePlayed;
    public bool firstPlayersRoomTruthEndingScenePlayed;
    public bool firstParentsRoomTruthEndingScenePlayed;
    public bool secondParentsRoomTruthEndingScenePlayed;
    public bool isEnding;
    public bool loopEnding;
    public bool truthEnding;

    //MouseAppearance
    public SerializableDictionary<string, bool> appearanceChanged;
    public SerializableDictionary<string, bool> cursorBlackened;

    //Doors
    public SerializableDictionary<string, bool> isLocked;
    public SerializableDictionary<string, bool> firstIsLockedStatusCheck;

    //Shower
    public bool showerCoroutineRunning;

    //Plant
    public SerializableDictionary<string, bool> firstPlantCanBeSearchedStatusCheck;
    public SerializableDictionary<string, bool> hasBeenWatered;

    //Plant zoomed 
    public SerializableDictionary<string, int> imageNum;
    public SerializableDictionary<string, bool> plantHasKey;
    public SerializableDictionary<string, bool> firstPlantHasKeyStatusCheck;
    public SerializableDictionary<string, bool> firstImageUpdated;
    public SerializableDictionary<string, bool> secondImageUpdated;

    //Computer
    public bool correctPasswordEntered;    

    //Mouse
    public bool notClickable;
    public bool hasJustusedObject;

    //ObjectsInMouseBehaviour
    public SerializableDictionary<string, bool> canBeWatered;
    public SerializableDictionary<string, bool> firstCanBeWateredStatusCheck;

    //StartingScene
    public string sceneToLoad;

    //AudioSource
    public bool horrorSoundIsPlaying;
    public bool horrorSoundStarted;
    public bool horrorSoundStopped;

    //Respiration
    public bool respirationTutorialShown;

    //Shower
    public bool hasDrainStopper;
    public bool showerKeyPicked;
    public bool waterFilled;

    //PCZoomAnimation
    public bool notepadOpened;

    //Notepad
    public bool notepadBeenOpened;

    //MustHideCounter
    public float timer;
    public bool horrorAudioEnabledMustHideCounter;

    //PlayerRoomTV
    public int timesToStatic;

    //AtticDoor
    public bool atticDoorOpen;

    //RadioButton
    public Vector3 dial1Rotation;
    public Vector3 dial2Rotation;
    public Vector3 dial3Rotation;

    //LibraryPuzzle
    public bool photoAlbumShown;

    //BoxEthan
    public bool boxHasBeenPickedUp;

    public GameData() 
    {
        //LanguageManager
        language = "en";

        //Player
        playerPos = new Vector3(1.85f, -1.27f);
        playerScale = new Vector3(1, 1, 1);
        lookingFront = false;
        lookingBack = false;
        sideMove = false;

        //Lights
        lightsOn = new SerializableDictionary<string, bool>();
        firstLightsStatusCheck = new SerializableDictionary<string, bool>();

        //Inventory
        isUsingFlashlight = false;
        isUsingGlass = false;
        isUsingParentsKey = false;
        isUsingBathroomDrainStopper = false;
        isUsingKeyUnderStairs = false;
        isUsingUmbrella = false;
        isUsingUmbrellaHandle = false;
        glassFilled = false;
        fillSoundPlayed = false;
        itemSelected = 0;        
        hasFlashlight = false;
        hasGlass = false;
        hasKeyParents = false;
        hasDadsNote = false;
        hasBathroomDrainStopper = false;
        hasKeyUnderStairs = false;
        hasUmbrella = false;
        hasUmbrellaHandle = false;

        //ClickableObject
        hasObject = new SerializableDictionary<string, bool>();
        firstHasObjectStatusCheck = new SerializableDictionary<string, bool>();
        canBeSearched = new SerializableDictionary<string, bool>();
        firstCanBeSearchedStatusCheck = new SerializableDictionary<string, bool>();

        //CanBeOpenObjects
        isOpen = new SerializableDictionary<string, bool>();

        //CanBeTurnOnOffObjects
        isTurnedOn = new SerializableDictionary<string, bool>();
        canBeTurnedOnOrOff = new SerializableDictionary<string, bool>();

        //CanBePickedUpObjects
        hasBeenPickedUp = new SerializableDictionary<string, bool> ();

        //ScenesInGame
        isFlashback = false;
        firstPlayerRoomScenePlayed = false;
        firstDinningRoomScene = false;
        firstDinningRoomScenePlayed = false;
        firstKitchenScenePlayed = false;
        firstKitchenScene = false;
        isEnding = false;

        //MouseAppearance
        appearanceChanged = new SerializableDictionary<string, bool>();
        cursorBlackened = new SerializableDictionary<string, bool>();
               
        //Doors
        firstIsLockedStatusCheck = new SerializableDictionary<string, bool>();
        isLocked = new SerializableDictionary<string, bool>();

        //Shower
        showerCoroutineRunning = false;

        //Plant
        hasBeenWatered = new SerializableDictionary<string, bool>();

        //Plant zoomed
        imageNum = new SerializableDictionary<string, int> ();
                                
        //Mouse
        notClickable = false;
        hasJustusedObject = false;
                
        //StartingScene
        sceneToLoad = "PlayersRoom";

    }

}
