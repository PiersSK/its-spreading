using static ThoughtBubble;

public static class PlayerThoughts
{
    private const string ICONFPATH = "SpreadIcons/";
    private const float DEFAULTTIMER = 5f;


    public static Thought WAKEUPTHOUGHT = new Thought(
        "Mmmm finally a day off, let's start the day with some lovely jam on toast",
        ICONFPATH + "jambg",
        DEFAULTTIMER
    );

    public static Thought FIRSTSPREADTHOUGHT = new Thought(
        "Oh my! That shouldn't happen here... I must've been doused with too many spreadheads at work. I need to spread 11 things by midnight to tire them out",
        null,
        DEFAULTTIMER
    );
}
