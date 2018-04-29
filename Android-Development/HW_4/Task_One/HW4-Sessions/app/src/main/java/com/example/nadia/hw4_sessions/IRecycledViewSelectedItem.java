package com.example.nadia.hw4_sessions;

public interface IRecycledViewSelectedItem {
    void onPlayTrackClicked(int position);
    void onPauseTrackClicked();
    void onStopTrackClicked();
    void onFastForewardTrackClicked();
    void onFastBackwardTrackClicked();
}
