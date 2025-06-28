import { createReducer, on } from '@ngrx/store';
import * as AdminActions from './admin.actions';
import { initialAdminState } from './admin.state';

export const AdminReducer = createReducer(
  initialAdminState,

  on(AdminActions.loadAdminProfile, (state) => ({
    ...state,
    loading: true,
    error: null,
  })),

  on(AdminActions.loadAdminProfileSuccess, (state, { profile }) => ({
    ...state,
    profile,
    loading: false,
    error: null,
  })),

  on(AdminActions.loadAdminProfileFailure, (state, { error }) => ({
    ...state,
    loading: false,
    error,
  }))
);
