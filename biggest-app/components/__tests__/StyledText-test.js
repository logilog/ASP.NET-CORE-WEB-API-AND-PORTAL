import * as React from 'react';
import renderer, { act } from 'react-test-renderer';

import { MonoText } from '../StyledText';

it.skip(`renders a MonoText with children`, () => {
  let tree;
  act(() => {
    tree = renderer.create(<MonoText>Test</MonoText>).toJSON();
  });
  expect(tree).toBeTruthy();
});
