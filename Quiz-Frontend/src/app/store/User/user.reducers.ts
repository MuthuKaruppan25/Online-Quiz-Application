import { createReducer, on } from '@ngrx/store';
import * as AttenderActions from './user.actions';
import { initialAttenderState } from './user.state';

export const AttenderReducer = createReducer(
  initialAttenderState,

  on(AttenderActions.loadAttenderProfile, (state) => ({
    ...state,
    loading: true,
    error: null,
  })),

  on(AttenderActions.loadAttenderProfileSuccess, (state, { profile }) => ({
    ...state,
    profile,
    loading: false,
    error: null,
  })),

  on(AttenderActions.loadAttenderProfileFailure, (state, { error }) => ({
    ...state,
    loading: false,
    error,
  }))
);
