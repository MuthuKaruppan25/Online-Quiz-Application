import { createFeatureSelector, createSelector } from '@ngrx/store';
import { AttenderState } from './user.state';

export const selectAttenderState = createFeatureSelector<AttenderState>('attender');

export const selectAttenderProfile = createSelector(
  selectAttenderState,
  (state) => state.profile
);

export const selectAttenderId = createSelector(
  selectAttenderState,
  (state) => state.profile?.guid
);

export const selectAttenderLoading = createSelector(
  selectAttenderState,
  (state) => state.loading
);

export const selectAttenderError = createSelector(
  selectAttenderState,
  (state) => state.error
);
