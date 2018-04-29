package com.example.nadia.hw4_broadcast;

import android.app.ActivityManager;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.ServiceConnection;
import android.os.Bundle;
import android.os.IBinder;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.View;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.example.nadia.hw4_broadcast.models.bindingModels.MapTuple;

import java.util.Date;
import java.util.Observable;
import java.util.Observer;

public class MainActivity extends AppCompatActivity implements Observer, IServiceCommunication, View.OnClickListener {

    BatteryChangedReceiver receiver;
    BroadCastHandler handler;

    Intent batteryUpdateServiceIntent;
    BatteryUpdateService batteryUpdateService;

    final Context mCtx = this;
    public boolean isServiceRunning = false;

    Button mStopServiceBtn;
    Button mCheckBatteryLvl;
    TextView mCaptionViewText;
    TextView mDisplayViewText;

    ServiceConnection conn = new ServiceConnection() {
        @Override
        public void onServiceConnected(ComponentName name, IBinder service) {
            batteryUpdateService = ((BatteryUpdateService.BatteryUpdateServiceBinder)service).getService();

            batteryUpdateService.setServiceCallback(MainActivity.this);
        }

        @Override
        public void onServiceDisconnected(ComponentName name) {
            Toast.makeText(mCtx,"Connection to the service is no more.",Toast.LENGTH_SHORT).show();
        }
    };

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        mStopServiceBtn = (Button) findViewById(R.id.stopService);
        mStopServiceBtn.setOnClickListener(this);
        mCheckBatteryLvl = (Button) findViewById(R.id.refreshBtn);
        mCheckBatteryLvl.setOnClickListener(this);
        mCaptionViewText = (TextView) findViewById(R.id.batteryChangeViewText);
        mDisplayViewText = (TextView) findViewById(R.id.displayBatteryChange);

        handler = BroadCastHandler.getInstance();
        BroadCastHandler.getInstance().addObserver(this);
        registerForBroadcast();

        batteryUpdateServiceIntent = new Intent(this, BatteryUpdateService.class);
        bindService(batteryUpdateServiceIntent, conn, Context.BIND_AUTO_CREATE);
        startService(batteryUpdateServiceIntent);
    }

    @Override
    protected void onResume() {
        super.onResume();

        loadBatteryLvlDataInTextView();
    }

    private void loadBatteryLvlDataInTextView() {
        if(batteryUpdateService != null && isServiceRunning) {
            int deltaBatteryLvl = batteryUpdateService.returnBatteryChangesForThePastHour();
            mDisplayViewText.setText(Integer.toString(deltaBatteryLvl) + " %");
        }
    }

    @Override
    public void update(Observable observable, Object obj) {
        boolean currStateOfService = isMyServiceRunning(BatteryUpdateService.class);

        if(currStateOfService != isServiceRunning){
            isServiceRunning = currStateOfService;
            changeStopServiceBtnState();
        }

        if(obj instanceof MapTuple && batteryUpdateService != null && isServiceRunning){
            batteryUpdateService.recordChange((MapTuple)obj);
        }
    }

    private void changeStopServiceBtnState() {
        mStopServiceBtn.setEnabled(isServiceRunning);
        if(isServiceRunning){
            mStopServiceBtn.setText(R.string.destroy_service_caption);
        } else{
            mStopServiceBtn.setText(R.string.destroyed_service_caption);
        }
    }

    public void registerForBroadcast()
    {
        receiver = new BatteryChangedReceiver();

        IntentFilter filter = new IntentFilter();
        filter.addAction("android.intent.action.BATTERY_CHANGED");

        registerReceiver(receiver,filter);
    }

    public void unregisterForBroadcast()
    {
        if(receiver != null)
            unregisterReceiver(receiver);
    }

    @Override
    protected void onDestroy() {
        unregisterForBroadcast();

        super.onDestroy();
    }

    @Override
    public void onServiceCustomInvocation() {

    }

    private boolean isMyServiceRunning(Class<?> serviceClass) {
        ActivityManager manager = (ActivityManager) getSystemService(Context.ACTIVITY_SERVICE);
        for (ActivityManager.RunningServiceInfo service : manager.getRunningServices(Integer.MAX_VALUE)) {
            if (serviceClass.getName().equals(service.service.getClassName())) {
                return true;
            }
        }

        return false;
    }

    @Override
    public void onClick(View view) {
        switch (view.getId()){
            case R.id.stopService:{
                unbindService(conn);
                stopService(batteryUpdateServiceIntent);
                isServiceRunning = isMyServiceRunning(BatteryUpdateService.class);

                changeStopServiceBtnState();
            }
            case R.id.refreshBtn:
                loadBatteryLvlDataInTextView();
            break;
        }
    }
}
