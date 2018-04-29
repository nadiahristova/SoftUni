package com.example.nadia.hw4_broadcast;

import android.content.BroadcastReceiver;
import com.example.nadia.hw4_broadcast.models.bindingModels.*;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.BatteryManager;
import android.widget.Toast;

import java.util.Date;
import java.util.Map;

public class BatteryChangedReceiver extends BroadcastReceiver {
    private IntentFilter ifilter;

    public BatteryChangedReceiver() {
        super();

        ifilter = new IntentFilter(Intent.ACTION_BATTERY_CHANGED);
    }

    @Override
    public void onReceive(Context context, Intent intent) {
        Toast.makeText(context,"Yeah I am called !!!",Toast.LENGTH_SHORT).show();

        Intent batteryStatus = context.registerReceiver(null, ifilter);
        int batteryLevel = batteryStatus.getIntExtra(BatteryManager.EXTRA_LEVEL, -1);

        BroadCastHandler.getInstance().updateValue(new MapTuple<>(new Date(), batteryLevel));
    }
}