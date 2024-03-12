using static ThoughtBubble;

public static class PlayerThoughts
{
    public static string ICONFPATH = "SpreadIcons/";
    private const float DEFAULTTIMER = 5f;

    // GAME PROGRESS //
    public static Thought FirstSpread = new Thought(
        "Oh my! That shouldn't happen here... I must've been doused with too many spreadheads at work. I need to spread 11 things by midnight to tire them out",
        null,
        DEFAULTTIMER
    );

    public static Thought LastSpread = new Thought(
        "Hey! I think I've finally spread enough to detox, maybe it's time to go to bed!",
        null,
        DEFAULTTIMER
    );

    // SPREAD EVENTS //
    // 1. Jam
    public static Thought WakeUp = new Thought(
        "Mmmm finally a day off, let's start the day with some lovely jam on toast",
        ICONFPATH + "jambg",
        DEFAULTTIMER
    );

    // 2. Virus
    public static Thought WatchingNews = new Thought(
        "Oh spaghettios, they're considering a lockdown in response to the spreading of the gigglepox pandemic.",
        ICONFPATH + "virusbg",
        DEFAULTTIMER
    );

    // 3. Fire
    public static Thought StartFirstFire = new Thought(
        "It dances so beautifully.",
        ICONFPATH + "firebg",
        DEFAULTTIMER
    );
    
    public static Thought PutOutFire = new Thought(
        "My soul feels lighter now.",
        ICONFPATH + "fire",
        DEFAULTTIMER
    );

    // 4. Religion
    public static Thought ReadDarrenPamphlet = new Thought(
        "This book is interesting, Magic Darren is quite impressive. Maybe I should tell someone else.",
        ICONFPATH + "religionbg",
        DEFAULTTIMER
    );

    // 5. Vines
    public static Thought InspectDeadVines = new Thought(
        "These vines used to be spread all over the bathroom, now look at them...",
        ICONFPATH + "vinesbg",
        DEFAULTTIMER
    );

    public static Thought WateredVinesOnce = new Thought(
        "Well, I'll give you some more in a while, hang in there.",
        ICONFPATH + "vinesbg",
        DEFAULTTIMER
    );

    // 6. Man
    // TODO: Make sure the duration syncs with event property
    public static Thought ManspreadFirstThought = new Thought(
        "Maybe just this once...",
        ICONFPATH + "menbg",
        3f
    );

    public static Thought ManspreadSecondThought = new Thought(
        "I mean, I'm all alone...",
        ICONFPATH + "menbg",
        3f
    );

    public static Thought ManspreadThirdThought = new Thought(
        "Getting comfortable can't hurt, right?",
        ICONFPATH + "menbg",
        3f
    );

    // 7. Tune
    public static Thought PlayingPiano = new Thought(
        "The show theme always gets stuck in my head.",
        ICONFPATH + "tunebg",
        DEFAULTTIMER
    );

    // 8. Wings
    public static Thought BookDanceClass = new Thought(
        "I've always wanted to try this since it became legal again, but it's so scary...",
        ICONFPATH + "wingsbg",
        DEFAULTTIMER
    );

    // 9. Confetti
    // TODO: Sync this with text check in spread event
    public static Thought ConfettiHasSpread = new Thought(
        "God, the confetti is really getting everywhere It's... Oh",
        ICONFPATH + "confettibg",
        3f
    );


    // 10. Gossip
    public static Thought FoundFirstGossip = new Thought(
        "Hmm, juicy goss, wonder who I could tell about this.",
        ICONFPATH + "rumorbg",
        DEFAULTTIMER
    );

    public static Thought FoundSecondGossip = new Thought(
        "More juicy goss.",
        ICONFPATH + "rumorbg",
        DEFAULTTIMER
    );

    // 11. Love
    public static Thought SuccessfulSisCall = new Thought(
        "Those tickets sounded important to her, she shouldn't miss out.",
        ICONFPATH + "lovebg",
        DEFAULTTIMER
    );

    public static Thought BookShowTickets = new Thought(
        "Sis will love these! I should call her when she gets back from work to tell her the good news",
        ICONFPATH + "lovebg",
        DEFAULTTIMER
    );


}
