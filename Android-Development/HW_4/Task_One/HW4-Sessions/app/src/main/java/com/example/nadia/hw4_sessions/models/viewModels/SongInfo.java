package com.example.nadia.hw4_sessions.models.viewModels;

public class SongInfo {
    private String imageSrc;
    private int trackPath;
    private String name;
    private String artist;
    private String album;
    private float duration;

    public String getImageSrc() {
        return imageSrc;
    }

    public int getTrackPath() {
        return trackPath;
    }

    public String getName() {
        return name;
    }

    public String getArtist() {
        return artist;
    }

    public String getAlbum() {
        return album;
    }

    public float getDuration() {
        return duration;
    }

    public SongInfo(String imageSrc, int trackPath, String name, String artist, String album, float duration) {
        this.imageSrc = imageSrc;
        this.trackPath = trackPath;
        this.name = name;
        this.artist = artist;
        this.album = album;
        this.duration = duration;

    }
}
