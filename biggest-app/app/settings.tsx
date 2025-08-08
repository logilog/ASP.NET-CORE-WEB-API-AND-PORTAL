import React from 'react';
import { View, Switch } from 'react-native';
import { ThemedText } from '@/components/Themed';
import * as Application from 'expo-application';
import * as Device from 'expo-device';
import { useSettingsStore } from '@/stores/settingsStore';

export default function SettingsScreen() {
  const { enablePush, togglePush, enableAnalytics, toggleAnalytics } = useSettingsStore();

  return (
    <View style={{ flex: 1, padding: 16, gap: 12 }}>
      <ThemedText style={{ fontSize: 20, fontWeight: 'bold' }}>Settings</ThemedText>

      <View style={{ flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center' }}>
        <ThemedText>Push Notifications</ThemedText>
        <Switch value={enablePush} onValueChange={togglePush} />
      </View>

      <View style={{ flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center' }}>
        <ThemedText>Analytics</ThemedText>
        <Switch value={enableAnalytics} onValueChange={toggleAnalytics} />
      </View>

      <ThemedText>App: {Application.applicationName} v{Application.nativeApplicationVersion}</ThemedText>
      <ThemedText>Device: {Device.brand} {Device.modelName}</ThemedText>
    </View>
  );
}