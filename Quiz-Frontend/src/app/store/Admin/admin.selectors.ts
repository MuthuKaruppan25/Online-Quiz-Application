import { createFeatureSelector, createSelector } from '@ngrx/store';
import { AdminState } from './admin.state';

export const selectAdminState = createFeatureSelector<AdminState>('admin');

export const selectAdminProfile = createSelector(
  selectAdminState,
  (state) => state.profile
);

export const selectAdminId = createSelector(
  selectAdminState,
  (state) => state.profile?.guid
);

export const selectAdminLoading = createSelector(
  selectAdminState,
  (state) => state.loading
);

export const selectAdminError = createSelector(
  selectAdminState,
  (state) => state.error
);
