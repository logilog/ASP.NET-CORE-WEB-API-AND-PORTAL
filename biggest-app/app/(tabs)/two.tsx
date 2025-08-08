import React from 'react';
import { FlatList, RefreshControl, View } from 'react-native';
import { ThemedText } from '@/components/Themed';
import { useQuery } from '@tanstack/react-query';
import { apiClient } from '@/lib/apiClient';

export default function FeedScreen() {
  const { data, isFetching, refetch } = useQuery({
    queryKey: ['feed'],
    queryFn: async () => {
      const res = await apiClient.get('/items');
      return res.data as { id: string; title: string }[];
    },
  });

  return (
    <FlatList
      data={data ?? []}
      keyExtractor={(item) => item.id}
      refreshControl={<RefreshControl refreshing={isFetching} onRefresh={refetch} />}
      ItemSeparatorComponent={() => <View style={{ height: 12 }} />}
      contentContainerStyle={{ padding: 16 }}
      renderItem={({ item }) => <ThemedText>{item.title}</ThemedText>}
      ListEmptyComponent={<ThemedText>No items</ThemedText>}
    />
  );
}
