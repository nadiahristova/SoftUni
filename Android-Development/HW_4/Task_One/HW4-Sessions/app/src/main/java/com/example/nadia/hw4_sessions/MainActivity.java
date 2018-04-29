package com.example.nadia.hw4_sessions;

import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.ServiceConnection;
import android.os.Bundle;
import android.os.IBinder;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.GridLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.View;
import android.widget.Toast;

import com.example.nadia.hw4_sessions.models.viewModels.SongInfo;

import java.util.ArrayList;
import java.util.Observer;

public class MainActivity extends AppCompatActivity implements IRecycledViewSelectedItem, IServiceCommunication {

    Intent mediaPlayerServiceIntent;
    final Context mCtx = this;

    private RecyclerView mRecyclerView;
    private RecyclerView.Adapter mAdapter;
    private RecyclerView.LayoutManager mLayoutManager;
    private ArrayList<SongInfo> mData;

    MediaPlayerService mediaPlayerService;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        populateData();

        mRecyclerView = (RecyclerView) findViewById(R.id.recycleView);
        mLayoutManager = new GridLayoutManager(this, 1);
        mRecyclerView.setLayoutManager(mLayoutManager);

        mAdapter = new RecyclerViewAdapter(mData, this);
        mRecyclerView.setAdapter(mAdapter);

        mediaPlayerServiceIntent = new Intent(this, MediaPlayerService.class);
        bindService(mediaPlayerServiceIntent,conn, Context.BIND_AUTO_CREATE);
        startService(mediaPlayerServiceIntent);
    }

    private void populateData() {
        this.mData = new ArrayList<SongInfo>() {
            {
                {
                    add(new SongInfo("http://s18.postimg.org/9ori00nk9/download.jpg", R.raw.jess_mills, "Jess Mills - End Credits (M3llo Remix)", "Jess Mills & M3llo", "Unknown", 3));
                    add(new SongInfo("http://s16.postimg.org/cozxnfzcl/hqdefault.jpg", R.raw.mckennitt, "Skellig", "Loreena McKennitt", "The Book of Secrets", 3));
                    add(new SongInfo("https://i.ytimg.com/vi/SAGIRof4U9Y/maxresdefault.jpg", R.raw.really_slow_motion, "Beyond Our Dreams", "Really Slow Motion", "Unknown", 3));
                    add(new SongInfo("https://f4.bcbits.com/img/a0530625186_10.jpg", R.raw.ziu_and_nyanara, "Unforgettable", "Ziu & Nyanara", "Unknown", 3));
                }
            }
        };
    }

    @Override
    public void onPlayTrackClicked(int position) {
        mediaPlayerService.play(mData.get(position).getTrackPath());
    }

    @Override
    public void onPauseTrackClicked() {
        mediaPlayerService.pausePlayer();
    }

    @Override
    public void onStopTrackClicked() {
        mediaPlayerService.stopPlayer();
    }

    @Override
    public void onFastForewardTrackClicked(){
        mediaPlayerService.fastForewardPlayer();
    }

    public void onFastBackwardTrackClicked(){
        mediaPlayerService.fastBackwardPlayer();
    }

    ServiceConnection conn = new ServiceConnection() {
        @Override
        public void onServiceConnected(ComponentName name, IBinder service) {
            mediaPlayerService = ((MediaPlayerService.MediaPlayerServiceBinder)service).getService();

            mediaPlayerService.setServiceCallback(MainActivity.this);
        }

        @Override
        public void onServiceDisconnected(ComponentName name) {
            Toast.makeText(mCtx,"Connection to the service is no more.",Toast.LENGTH_SHORT).show();
        }
    };


//    public void onServiceStarted(View view)
//    {
//        mediaPlayerServiceIntent = new Intent(this, MediaPlayerService.class);
//
//        //firstServiceIntent.putExtra("Data","Hello Service");
//
//        bindService(mediaPlayerServiceIntent,conn, Context.BIND_AUTO_CREATE);
//
//        startService(mediaPlayerServiceIntent);
//    }
//
//    public void onServiceStopped(View view)
//    {
//        if(mediaPlayerServiceIntent == null)
//            mediaPlayerServiceIntent = new Intent(this, MediaPlayerService.class);
//
//        unbindService(conn);
//        stopService(mediaPlayerServiceIntent);
//    }

    @Override
    public void onServiceCustomInvocation() {
        Toast.makeText(mCtx,"This event come from Service callback",Toast.LENGTH_SHORT).show();
    }

    @Override
    protected void onDestroy() {
        if(mediaPlayerServiceIntent == null)
            mediaPlayerServiceIntent = new Intent(this, MediaPlayerService.class);

        unbindService(conn);
        stopService(mediaPlayerServiceIntent);

        super.onDestroy();
    }
}
