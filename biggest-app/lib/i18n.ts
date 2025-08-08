import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';
import * as Localization from 'expo-localization';

const resources = {
  en: {
    translation: {
      hello: 'Hello',
      welcome: 'Welcome to Biggest App',
    },
  },
} as const;

if (!i18n.isInitialized) {
  i18n
    .use(initReactI18next)
    .init({
      resources,
      lng: Localization.getLocales()[0]?.languageTag ?? 'en',
      fallbackLng: 'en',
      interpolation: { escapeValue: false },
    })
    .catch(() => {});
}

export default i18n;