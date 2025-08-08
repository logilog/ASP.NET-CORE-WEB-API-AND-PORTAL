import React from 'react';
import { Controller, useForm } from 'react-hook-form';
import { View, TextInput, Button } from 'react-native';
import { ThemedText } from '@/components/Themed';
import { useAuthStore } from '@/stores/authStore';

interface ProfileFormValues {
  displayName: string;
}

export default function ProfileScreen() {
  const { user, updateProfile } = useAuthStore();
  const { control, handleSubmit } = useForm<ProfileFormValues>({
    defaultValues: { displayName: user?.displayName ?? '' },
  });

  const onSubmit = (values: ProfileFormValues) => updateProfile(values);

  return (
    <View style={{ flex: 1, padding: 16, gap: 12 }}>
      <ThemedText style={{ fontSize: 20, fontWeight: 'bold' }}>Profile</ThemedText>
      <ThemedText>Email: {user?.email ?? 'Guest'}</ThemedText>
      <Controller
        control={control}
        name="displayName"
        render={({ field: { onChange, value } }) => (
          <TextInput
            placeholder="Display name"
            value={value}
            onChangeText={onChange}
            style={{ borderWidth: 1, borderColor: '#ccc', padding: 12, borderRadius: 8 }}
          />
        )}
      />
      <Button title="Save" onPress={handleSubmit(onSubmit)} />
    </View>
  );
}