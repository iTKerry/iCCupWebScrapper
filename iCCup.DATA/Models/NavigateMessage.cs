﻿namespace iCCup.DATA.Models
{
    public class NavigateMessage
    {
        public NavigateTo NavigateTo { get; set; }
    }

    public enum NavigateTo
    {
        Search,
        Profile,
        GameDetails,
        Forward,
        Back
    }
}
