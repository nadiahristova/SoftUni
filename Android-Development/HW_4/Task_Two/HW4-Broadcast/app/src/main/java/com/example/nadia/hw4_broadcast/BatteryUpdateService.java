package com.example.nadia.hw4_broadcast;

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
import java.util.Calendar;
import java.util.Collection;
import java.util.Date;
import java.util.Map;
import java.util.NavigableMap;
import java.util.SortedMap;
import java.util.TreeMap;


public class BatteryUpdateService extends Service {
    IBinder binder = new BatteryUpdateServiceBinder();
    IServiceCommunication callback;
    SortedMap<Date, Integer> dataHolder = new TreeMap<>();

    private int lastPercentageRecord;

    public void setServiceCallback(IServiceCommunication callback)
    {
        this.callback = callback;

        if(callback != null)
        {
            callback.onServiceCustomInvocation();
        }
    }


    public class BatteryUpdateServiceBinder extends Binder
    {
        BatteryUpdateService getService()
        {
            return BatteryUpdateService.this;
        }
    }

    @Nullable
    @Override
    public IBinder onBind(Intent intent) {
        return binder;
    }

    @Override
    public void onCreate() {
    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {
        Toast.makeText(this,"Battery Update Service has Started.", Toast.LENGTH_LONG).show();

        return super.onStartCommand(intent, flags, startId);
    }

    @Override
    public void onDestroy() {
        super.onDestroy();
        Toast.makeText(this,"Battery Update Service is Destroyed.",Toast.LENGTH_SHORT).show();

        dataHolder.clear();
    }

    public void recordChange(Map.Entry<Date,Integer> detactedChange) {
        if (lastPercentageRecord != detactedChange.getValue()) {
            lastPercentageRecord = detactedChange.getValue();
            dataHolder.put(detactedChange.getKey(), detactedChange.getValue());
        }
    }

    public int returnBatteryChangesForThePastHour() {
        int recordedChange = 0;
        if (dataHolder.size() > 1) {
            Calendar cal = Calendar.getInstance();
            cal.setTime(new Date());
            cal.add(Calendar.HOUR, -1);
            Date oneHourBack = cal.getTime();
            dataHolder.put(oneHourBack, 0);
            NavigableMap<Date, Integer> subTree = ((TreeMap<Date, Integer>)dataHolder).tailMap(oneHourBack, false);

            int battLvlAtTheBeginning = subTree.firstEntry().getValue();
            int battLvlAtTheEnd = subTree.lastEntry().getValue();

            return battLvlAtTheBeginning - battLvlAtTheEnd;
        }

        return recordedChange;
    }
}
