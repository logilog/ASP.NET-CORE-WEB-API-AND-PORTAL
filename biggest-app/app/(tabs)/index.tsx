import React, { useEffect } from 'react';
import { StyleSheet, Platform, View, Button, Alert } from 'react-native';
import { ThemedText } from '@/components/Themed';
import { Link } from 'expo-router';
import { useQuery } from '@tanstack/react-query';
import { apiClient } from '@/lib/apiClient';
import { useAuthStore } from '@/stores/authStore';
import * as Notifications from 'expo-notifications';
import * as Device from 'expo-device';

export default function HomeScreen() {
  const { user, signInDemo, signOut } = useAuthStore();

  const { data, isLoading, error, refetch } = useQuery({
    queryKey: ['public-ping'],
    queryFn: async () => {
      const res = await apiClient.get('/ping');
      return res.data as { message: string };
    },
  });

  useEffect(() => {
    (async () => {
      if (Device.isDevice) {
        const settings = await Notifications.getPermissionsAsync();
        if (!settings.granted) {
          await Notifications.requestPermissionsAsync();
        }
      }
    })();
  }, []);

  return (
    <View style={styles.container}>
      <ThemedText style={{ fontSize: 24, fontWeight: 'bold' }}>Biggest App</ThemedText>
      <ThemedText>Platform: {Platform.OS}</ThemedText>
      <ThemedText>{isLoading ? 'Loading...' : error ? String(error) : data?.message}</ThemedText>

      <View style={{ height: 12 }} />
      {user ? (
        <>
          <ThemedText>Signed in as {user.email}</ThemedText>
          <Button title="Sign out" onPress={signOut} />
        </>
      ) : (
        <Button title="Demo sign in" onPress={signInDemo} />
      )}

      <View style={{ height: 12 }} />
      <Button title="Refetch Ping" onPress={() => refetch()} />

      <View style={{ height: 12 }} />
      <Link href="/profile">
        <ThemedText>Go to Profile</ThemedText>
      </Link>

      <Link href="/settings">
        <ThemedText>Open Settings</ThemedText>
      </Link>

      <Link href="/modal">
        <ThemedText>Open Modal</ThemedText>
      </Link>

      <View style={{ height: 12 }} />
      <Button
        title="Test Local Notification"
        onPress={async () => {
          try {
            await Notifications.scheduleNotificationAsync({
              content: { title: 'Hello from Biggest App', body: 'This is a test notification.' },
              trigger: null,
            });
          } catch (e) {
            Alert.alert('Notification error', String(e));
          }
        }}
      />
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
    gap: 8,
    padding: 16,
  },
});
