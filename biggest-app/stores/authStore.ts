import { create } from 'zustand';
import { persist, createJSONStorage } from 'zustand/middleware';
import AsyncStorage from '@react-native-async-storage/async-storage';

export interface AuthUser {
  id: string;
  email: string;
  displayName?: string;
}

interface AuthState {
  user: AuthUser | null;
  signInDemo: () => void;
  signOut: () => void;
  updateProfile: (update: Partial<AuthUser>) => void;
}

export const useAuthStore = create<AuthState>()(
  persist(
    (set) => ({
      user: null,
      signInDemo: () =>
        set({ user: { id: 'demo', email: 'demo@biggest.app', displayName: 'Demo User' } }),
      signOut: () => set({ user: null }),
      updateProfile: (update) =>
        set((state) => ({ user: state.user ? { ...state.user, ...update } : state.user })),
    }),
    {
      name: 'auth-storage',
      storage: createJSONStorage(() => AsyncStorage),
      partialize: (state) => ({ user: state.user }),
    }
  )
);