using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Farmer : ConversableBase
{
    // Ashley found the Regex.Replace method so I can stylize important dialogue

    private void Start()
    {
        InitializeConversable("Farmer Pete");
    }

    public override List<string> GatherConversation(int taskIndex)
    {
        conversationParts.Clear();

        // Based on where the player is in the task system
        switch (taskIndex)
        {
            case 0:
                conversationParts.Add("Finally you've arrived!");
                conversationParts.Add("I need help getting this farm back in order before the rooster crows tomorrow.");
                conversationParts.Add(Regex.Replace("Can you start by clearing up some reeds around the pond? Five reeds should do for now. You'll find the pond just south of the farmhouse.", @"Five reeds", @"<b><i>$0</i></b>"));
                break;

            case 1:
                conversationParts.Add("The pond is just south of the farmhouse, you can't miss it.");
                break;

            case 2:
                conversationParts.Add("Thanks for clearing up those reeds! Those suckers always grow so fast around here.");
                conversationParts.Add("Why don't you throw them in the deposit box next to me to clear up your inventory.");
                break;

            case 3:
                conversationParts.Add("The deposit box is to my left, you can't miss it.");
                break;

            case 4:
                conversationParts.Add(Regex.Replace("The cabbage patch next to the farmhouse should be ready for harvesting. Why don't you grab twenty cabbages for my partner to sell at the market later.", @"twenty cabbages", @"<b><i>$0</i></b>"));
                conversationParts.Add(Regex.Replace("If you've run out of cabbages to harvest, I bet some more will pop up in a minute.", @"pop up in a minute", @"<b><i>$0</i></b>"));
                break;

            case 5:
                conversationParts.Add(Regex.Replace("If you've run out of cabbages to harvest next to the farmhouse, I bet some more will pop up in a minute.", @"pop up in a minute", @"<b><i>$0</i></b>"));
                break;

            case 6:
                conversationParts.Add("Don't forget to throw them in the deposit box for my partner to sell at the market later.");
                break;

            case 7:
                conversationParts.Add("Thanks for your help with the farm today! Why don't you take some time to explore while I think of some other tasks for you.");
                break;

            default:
                conversationParts.Add("Try to enjoy the farm now that you're work is over!");
                break;
        }

        return conversationParts;
    }
}
