import { create } from 'zustand';
import { persist, createJSONStorage } from 'zustand/middleware';
import AsyncStorage from '@react-native-async-storage/async-storage';

interface SettingsState {
  enablePush: boolean;
  enableAnalytics: boolean;
  togglePush: () => void;
  toggleAnalytics: () => void;
}

export const useSettingsStore = create<SettingsState>()(
  persist(
    (set) => ({
      enablePush: true,
      enableAnalytics: true,
      togglePush: () => set((s) => ({ enablePush: !s.enablePush })),
      toggleAnalytics: () => set((s) => ({ enableAnalytics: !s.enableAnalytics })),
    }),
    {
      name: 'settings-storage',
      storage: createJSONStorage(() => AsyncStorage),
    }
  )
);