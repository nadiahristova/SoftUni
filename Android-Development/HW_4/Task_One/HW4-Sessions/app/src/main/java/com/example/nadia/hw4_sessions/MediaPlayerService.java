package com.example.nadia.hw4_sessions;

import android.app.Service;
import android.content.Intent;
import android.media.AudioManager;
import android.media.MediaPlayer;
import android.os.Binder;
import android.os.IBinder;
import android.os.PowerManager;
import android.support.annotation.Nullable;
import android.widget.Toast;

import java.io.IOException;
import java.io.InputStream;


public class MediaPlayerService extends Service {
    private final int rewindMs = 10000;

    IBinder binder = new MediaPlayerServiceBinder();
    IServiceCommunication callback;
    MediaPlayer player;

    private boolean isPaused = false;
    private int length = 0;

    public void setServiceCallback(IServiceCommunication callback)
    {
        this.callback = callback;


        if(callback != null)
        {
            callback.onServiceCustomInvocation();
        }

    }


    public class MediaPlayerServiceBinder extends Binder
    {
        MediaPlayerService getService()
        {
            return MediaPlayerService.this;
        }
    }

    @Nullable
    @Override
    public IBinder onBind(Intent intent) {
        return binder;
    }

    @Override
    public void onCreate() {
        player = new MediaPlayer();

        initMediaPlayerPreference();
    }

    private void initMediaPlayerPreference() {
        player.setWakeMode(getApplicationContext(), PowerManager.PARTIAL_WAKE_LOCK);

        player.setAudioStreamType(AudioManager.STREAM_MUSIC);
    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {
        Toast.makeText(this,"Media Player Service has Started.", Toast.LENGTH_LONG).show();

        return super.onStartCommand(intent, flags, startId);
    }

    @Override
    public void onDestroy() {
        super.onDestroy();
        Toast.makeText(this,"Media Player Service is Destroyed.",Toast.LENGTH_SHORT).show();

        if(player != null)
            player.stop();
    }

    public void play(int trackResource){
        if (player.isPlaying()){}
            player.stop();

        player = MediaPlayer.create(this, trackResource);
        player.start();
    }

    public void pausePlayer(){
        if (player.isPlaying()){
            player.pause();
            length = player.getCurrentPosition();
        } else{
            player.seekTo(length);
            player.start();
        }
    }

    public void stopPlayer(){
        if (player.isPlaying())
            player.stop();
    }

    public void fastForewardPlayer(){
        if (player.isPlaying()){
            player.pause();
            length = player.getCurrentPosition();
            length += rewindMs;

            if (length >= player.getDuration()){
                player.stop();
            }
            else{
                player.seekTo(length);
                player.start();
            }
        }
    }

    public void fastBackwardPlayer(){
        if (player.isPlaying()){
            player.pause();
            length = player.getCurrentPosition();
            length -= rewindMs;

            if (length <= 0){
                player.stop();
            }
            else{
                player.seekTo(length);
                player.start();
            }
        }
    }
}
