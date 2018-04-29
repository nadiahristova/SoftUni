package com.example.nadia.hw4_sessions;

import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.TextView;

import com.example.nadia.hw4_sessions.models.viewModels.SongInfo;
import com.example.nadia.hw4_sessions.utils.DownloadImageTask;

import java.util.ArrayList;

public class RecyclerViewAdapter extends RecyclerView.Adapter<RecyclerViewAdapter.ViewHolder>{

    private ArrayList<SongInfo> mAdapterData;
    public static IRecycledViewSelectedItem mListener;

    @Override
    public ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.track_layout, parent, false);

        ViewHolder vh = new ViewHolder(view);

        return vh;
    }

    @Override
    public void onBindViewHolder(ViewHolder holder, int position) {
        if (holder != null){
            holder.linkData(mAdapterData.get(position));
            holder.setItemPosition(position);
        }
    }

    @Override
    public int getItemCount() {
        return mAdapterData.size();
    }

    public static class ViewHolder extends RecyclerView.ViewHolder implements View.OnClickListener {
        TextView songTitle;
        TextView songArtistNAlbum;
        ImageView imageView;
        ImageButton playBtn;
        ImageButton pauseBtn;
        ImageButton stopBtn;
        ImageButton fastFBtn;
        ImageButton fastBBtn;

        int position;

        public void linkData(SongInfo song){
            if (songTitle != null)
                songTitle.setText(song.getName());

            if (songArtistNAlbum != null)
                songArtistNAlbum.setText("Artist: " + song.getArtist() + ", Album: " + song.getAlbum());

            if (imageView != null)
                new DownloadImageTask(imageView).execute(song.getImageSrc());
        }

        public void setItemPosition(int position){
            this.position = position;
        }

        public ViewHolder(View itemView){
            super(itemView);

            songTitle = (TextView) itemView.findViewById(R.id.trackName);
            songArtistNAlbum = (TextView) itemView.findViewById(R.id.trackArtistNtrackAlbum);
            imageView = (ImageView) itemView.findViewById(R.id.trackImage);
            playBtn = (ImageButton) itemView.findViewById(R.id.playBtn);
            pauseBtn = (ImageButton) itemView.findViewById(R.id.pauseBtn);
            stopBtn = (ImageButton) itemView.findViewById(R.id.stopBtn);
            fastFBtn = (ImageButton) itemView.findViewById(R.id.fastFBtn);
            fastBBtn = (ImageButton) itemView.findViewById(R.id.fastBBtn);

            playBtn.setOnClickListener(this);
            pauseBtn.setOnClickListener(this);
            stopBtn.setOnClickListener(this);
            fastFBtn.setOnClickListener(this);
            fastBBtn.setOnClickListener(this);
        }

        @Override
        public void onClick(View view) {
            switch (view.getId()){
                case R.id.playBtn:
                    mListener.onPlayTrackClicked(position);
                    break;
                case R.id.pauseBtn:
                    mListener.onPauseTrackClicked();
                    break;
                case R.id.stopBtn:
                    mListener.onStopTrackClicked();
                    break;
                case R.id.fastFBtn:
                    mListener.onFastForewardTrackClicked();
                    break;
                case R.id.fastBBtn:
                    mListener.onFastBackwardTrackClicked();
                    break;
            }
        }
    }

    public RecyclerViewAdapter(ArrayList<SongInfo> data, IRecycledViewSelectedItem listener){
        this.mAdapterData = data;
        this.mListener = listener;
    }
}
