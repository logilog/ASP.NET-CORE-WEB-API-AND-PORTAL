import React, { PropsWithChildren } from 'react';
import * as Notifications from 'expo-notifications';

Notifications.setNotificationHandler({
  handleNotification: async () => ({
    shouldShowAlert: true,
    shouldPlaySound: false,
    shouldSetBadge: false,
    // iOS specific legacy fields to satisfy broad typings
    shouldShowBanner: true,
    shouldShowList: true,
  }),
});

export function RootProviders({ children }: PropsWithChildren) {
  return <>{children}</>;
}